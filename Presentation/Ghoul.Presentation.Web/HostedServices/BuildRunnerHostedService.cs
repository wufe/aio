using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Ghoul.Application.Model.Build;
using Ghoul.Application.Model.Commands;
using Ghoul.Application.Model.Queries;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Stateless;

namespace Ghoul.Presentation.Web.HostedServices {
    public class BuildRunnerHostedService : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly BuildRunnerStateMachine _buildRunner;

        public BuildRunnerHostedService(
            IServiceProvider serviceProvider
        )
        {
            _serviceProvider = serviceProvider;
            _buildRunner = new BuildRunnerStateMachine(serviceProvider);
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _buildRunner.Start();
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }

    public enum BuildRunnerState {
        Idle,
        SearchingForNewBuild,
        WaitingForNewSearch,
        RunningBuild,
        SearchingForNewStep,
        RunEnded,
        RunningStep,
    }
    public enum BuildRunnerTrigger {
        SearchForNewBuild,
        NoBuildFound,
        FoundNewBuild,
        SearchForNewStep,
        NoStepFound,
        FoundNewStep,
        HaltBuild
    }

    public class BuildRunnerStateMachine {
        private readonly IServiceProvider _serviceProvider;
        private readonly StateMachine<BuildRunnerState, BuildRunnerTrigger> _stateMachine;

        private GetIdleRunQueryResponse _currentBuildRun = null;
        private GetWaitingStepQueryResponse _currentStep = null;

        public BuildRunnerStateMachine(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

            _stateMachine = new StateMachine<BuildRunnerState, BuildRunnerTrigger>(BuildRunnerState.Idle);
            _stateMachine.OnTransitioned((transition) => {
                using (var scope = serviceProvider.CreateScope()) {
                    var logger = scope.ServiceProvider.GetRequiredService<ILogger<BuildRunnerStateMachine>>();
                    logger.LogTrace($"{transition.Trigger}: {transition.Source} -> {transition.Destination}");
                }
            });

            _stateMachine.Configure(BuildRunnerState.Idle)
                .Permit(BuildRunnerTrigger.SearchForNewBuild, BuildRunnerState.SearchingForNewBuild);
            
            _stateMachine.Configure(BuildRunnerState.SearchingForNewBuild)
                .OnEntry(SearchForNewBuild)
                .Permit(BuildRunnerTrigger.NoBuildFound, BuildRunnerState.WaitingForNewSearch)
                .Permit(BuildRunnerTrigger.FoundNewBuild, BuildRunnerState.RunningBuild);

            _stateMachine.Configure(BuildRunnerState.WaitingForNewSearch)
                .OnEntry(WaitForNewSearch)
                .Permit(BuildRunnerTrigger.SearchForNewBuild, BuildRunnerState.SearchingForNewBuild);

            _stateMachine.Configure(BuildRunnerState.RunningBuild)
                .OnEntry(RunBuild)
                .Permit(BuildRunnerTrigger.SearchForNewBuild, BuildRunnerState.SearchingForNewBuild)
                .Permit(BuildRunnerTrigger.SearchForNewStep, BuildRunnerState.SearchingForNewStep);

            _stateMachine.Configure(BuildRunnerState.SearchingForNewStep)
                .OnEntry(SearchForNewStep)
                .Permit(BuildRunnerTrigger.NoStepFound, BuildRunnerState.RunEnded)
                .Permit(BuildRunnerTrigger.FoundNewStep, BuildRunnerState.RunningStep);

            _stateMachine.Configure(BuildRunnerState.RunEnded)
                .OnEntry(StopBuild)
                .Permit(BuildRunnerTrigger.SearchForNewBuild, BuildRunnerState.WaitingForNewSearch);

            _stateMachine.Configure(BuildRunnerState.RunningStep)
                .OnEntry(RunStep)
                .Permit(BuildRunnerTrigger.SearchForNewStep, BuildRunnerState.SearchingForNewStep)
                .Permit(BuildRunnerTrigger.HaltBuild, BuildRunnerState.RunEnded);
        }

        public void Start() {
            if (_stateMachine.State == BuildRunnerState.Idle) {
                _stateMachine.Fire(BuildRunnerTrigger.SearchForNewBuild);
            }
        }
        
        private void SearchForNewBuild() {
            Task.Run(async () => {

                using (var scope = _serviceProvider.CreateScope()) {
                    var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                    _currentBuildRun = await mediator.Send(new GetIdleRunQuery());
                }

                if (_currentBuildRun == null) {
                    _stateMachine.Fire(BuildRunnerTrigger.NoBuildFound);
                } else {
                    _stateMachine.Fire(BuildRunnerTrigger.FoundNewBuild);
                }
                
            });
        }

        private void WaitForNewSearch()
        {
            Task.Run(async () => {
                await Task.Delay(5000);
                _stateMachine.Fire(BuildRunnerTrigger.SearchForNewBuild);
            });
        }

        private void RunBuild() {
            Task.Run(async () => {
                using (var scope = _serviceProvider.CreateScope()) {
                    var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                    try {
                        await mediator.Send(new StartBuildRunCommand(_currentBuildRun.Build.ID, _currentBuildRun.Run.ID));
                        _stateMachine.Fire(BuildRunnerTrigger.SearchForNewStep);
                    } catch (Exception) {
                        // print out error ?
                        _stateMachine.Fire(BuildRunnerTrigger.SearchForNewBuild);
                    }
                }
            });
        }

        private void SearchForNewStep() {
            Task.Run(async () => {
                using (var scope = _serviceProvider.CreateScope()) {
                    var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                    _currentStep = await mediator.Send(new GetWaitingStepQuery(_currentBuildRun.Build.ID));
                    
                    if (_currentStep == null) {
                        _stateMachine.Fire(BuildRunnerTrigger.NoStepFound);
                    } else {
                        _stateMachine.Fire(BuildRunnerTrigger.FoundNewStep);
                    }
                }
            });
        }

        private void StopBuild() {
            Task.Run(async () => {
                using (var scope = _serviceProvider.CreateScope()) {
                    var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                    await mediator.Send(new StopBuildRunCommand(_currentBuildRun.Build.ID, _currentBuildRun.Run.ID));

                    _stateMachine.Fire(BuildRunnerTrigger.SearchForNewBuild);
                }
            });
        }
    
        private void RunStep() {
            Task.Run(async () => {
                using (var scope = _serviceProvider.CreateScope()) {
                    var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                    await mediator.Send(new RunStepCommand(_currentBuildRun.Build.ID, _currentStep.StepIndex));

                    await mediator.Send(new AddRunLogCommand(_currentBuildRun.Run.ID, $"#region {_currentStep.Step.Name}", LogType.Stdout));

                    // run the command
                    var buildCommand = new BuildCommand(_currentBuildRun.Build, _currentStep, _currentBuildRun.Run, async (message, messageType) => {
                        await mediator.Send(new AddRunLogCommand(_currentBuildRun.Run.ID, message, messageType == CommandLogType.Stdout ? LogType.Stdout : LogType.Stderr));
                    });
                    var exitCode = await buildCommand.Start();

                    StepOutcome outcome;
                    switch (exitCode) {
                        case -1:
                            outcome = StepOutcome.Unknown;
                            break;
                        case 0:
                            outcome = StepOutcome.Success;
                            break;
                        default:
                            outcome = StepOutcome.Fail;
                            break;
                    }

                    await mediator.Send(new EndStepCommand(_currentBuildRun.Build.ID, _currentStep.StepIndex, outcome));

                    await mediator.Send(new AddRunLogCommand(_currentBuildRun.Run.ID, $"#endregion ({exitCode})", LogType.Stdout));

                    if (outcome == StepOutcome.Fail && _currentStep.Step.HaltOnError) {
                        _stateMachine.Fire(BuildRunnerTrigger.HaltBuild);
                    } else {
                        _stateMachine.Fire(BuildRunnerTrigger.SearchForNewStep);
                    }
                }
            });
        }
    }

    public static class BuildRunnerExtensions {
        public static void AddBuildRunner(this IServiceCollection services) {
            services.AddHostedService<BuildRunnerHostedService>();
        }
    }

    public class BuildCommand {
        public BuildApplicationModel Build { get; private set; }
        public GetWaitingStepQueryResponse StepContainer { get; private set; }
        public RunApplicationModel Run { get; private set; }
        public Func<string, CommandLogType, Task> OnLog { get; private set; }

        public BuildCommand(BuildApplicationModel build, GetWaitingStepQueryResponse stepContainer, RunApplicationModel run, Func<string, CommandLogType, Task> onLog)
        {
            Build = build;
            StepContainer = stepContainer;
            Run = run;
            OnLog = onLog;
        }

        public async Task<int> Start() {
            var exitCode = -1;
            await Task.Run(() => {
                var processInfo = new ProcessStartInfo()
                {
                    UseShellExecute = false,
                    FileName = StepContainer.Step.CommandExecutable,
                    Arguments = GetArguments(),
                    WorkingDirectory = StepContainer.Step.WorkingDirectory,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true
                };
                foreach (var envVariable in StepContainer.Step.EnvironmentVariables)
                    processInfo.EnvironmentVariables[envVariable.Split('=')[0]] = envVariable.Split('=')[1];
                    
                var process = Process.Start(processInfo);

                if (!StepContainer.Step.FireAndForget) {
                    process.BeginOutputReadLine();
                    process.BeginErrorReadLine();
                    process.OutputDataReceived += (sender, e) =>
                    {
                        if (!String.IsNullOrEmpty(e.Data))
                            OnLog(e.Data, CommandLogType.Stdout);
                    };
                    process.ErrorDataReceived += (sender, e) =>
                    {
                        if (!String.IsNullOrEmpty(e.Data))
                            OnLog(e.Data, CommandLogType.Stderr);
                    };
                    process.WaitForExit();
                    exitCode = process.ExitCode;
                    process.Close();
                }
                
            });
            return exitCode;
        }

        private string GetArguments() {
            return StepContainer.Step.CommandArguments
                .Replace("{run_id}", Run.ID)
                .Replace("{uuid}", Run.ID)
                .Replace("{build_id}", Build.ID)
                .Replace("{step_name}", StepContainer.Step.Name)
                .Replace("{step_id}", StepContainer.StepIndex.ToString());
        }
    }

    public enum CommandLogType {
        Stdout,
        Stderr
    }
}