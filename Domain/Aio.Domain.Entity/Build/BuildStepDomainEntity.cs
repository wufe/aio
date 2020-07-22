using System;
using System.Collections.Generic;

namespace Aio.Domain.Entity.Build {

    public enum BuildStepStatus {
        Idle,
        Waiting,
        Running,
        Done,
        Error,
        Unreachable,    // When a previous step throws an error
    }

    public class BuildStepDomainEntity {

        public enum StepOutcome
        {
            Success,
            Fail,
            Unknown
        }

        public string Name { get; set; }
        public BuildStepStatus Status { get; set; }
        public string CommandExecutable { get; private set; }
        public string CommandArguments { get; private set; }
        public IDictionary<string, string> EnvironmentVariables { get; private set; }
        public string WorkingDirectory { get; private set; }
        public bool FireAndForget { get; private set; }
        public bool HaltOnError { get; private set; }

        public static BuildStepDomainEntity CreateNew(string name) {
            return new BuildStepDomainEntity() {
                Name = name,
                CommandArguments = "",
                CommandExecutable = "",
                EnvironmentVariables = new Dictionary<string, string>(),
                FireAndForget = false,
                HaltOnError = true,
                Status = BuildStepStatus.Idle,
                WorkingDirectory = ""
            };
        }

        public BuildStepDomainEntity SetCommand(string executable, string arguments) {
            CommandExecutable = executable;
            CommandArguments = arguments;
            return this;
        }

        public BuildStepDomainEntity SetEnvironmentVariables(IDictionary<string, string> environmentVariables) {
            EnvironmentVariables = environmentVariables;
            return this;
        }

        public BuildStepDomainEntity SetWorkingDirectory(string workingDirectory) {
            WorkingDirectory = workingDirectory;
            return this;
        }

        internal BuildStepDomainEntity Run()
        {
            if (Status != BuildStepStatus.Waiting)
                throw new Exception($"Step was not expecting to be ran.");
            Status = BuildStepStatus.Running;
            return this;
        }

        internal BuildStepDomainEntity Stop(StepOutcome outcome)
        {
            if (Status != BuildStepStatus.Running)
                throw new Exception($"Step was not expecting to be stopped: it was not running.");
            switch (outcome) {
                case StepOutcome.Fail:
                    Status = BuildStepStatus.Error;
                    break;
                case StepOutcome.Success:
                case StepOutcome.Unknown:
                    Status = BuildStepStatus.Done;
                    break;
            }
            return this;
        }

        public BuildStepDomainEntity SetFireAndForget(bool fireAndForget) {
            FireAndForget = fireAndForget;
            return this;
        }

        public BuildStepDomainEntity SetHaltOnError(bool haltOnError) {
            HaltOnError = haltOnError;
            return this;
        }

        public BuildStepDomainEntity WaitForRun() {
            Status = BuildStepStatus.Waiting;
            return this;
        }

        public BuildStepDomainEntity BuildStopped() {
            if (Status == BuildStepStatus.Waiting)
                Status = BuildStepStatus.Unreachable;
            return this;
        }
    }
}