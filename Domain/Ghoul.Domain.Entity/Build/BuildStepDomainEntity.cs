using System.Collections.Generic;

namespace Ghoul.Domain.Entity.Build {

    public enum BuildStepStatus {
        Idle,
        Waiting,
        Running,
        Error
    }

    public class BuildStepDomainEntity {
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

        public BuildStepDomainEntity SetStatus(BuildStepStatus status) {
            Status = status;
            return this;
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

        public BuildStepDomainEntity SetFireAndForget(bool fireAndForget) {
            FireAndForget = fireAndForget;
            return this;
        }

        public BuildStepDomainEntity SetHaltOnError(bool haltOnError) {
            HaltOnError = haltOnError;
            return this;
        }
    }
}