using System.Collections.Generic;

namespace Ghoul.Domain.Entity.Build {

    public enum BuildStepStatus {
        Idle,
        Waiting,
        Running,
        Error
    }

    public class BuildStepDomainEntity {
        public string ID { get; private set; }
        public string Name { get; set; }
        public BuildStepStatus Status { get; set; }
        public string CommandExecutable { get; private set; }
        public string CommandArguments { get; private set; }
        public IDictionary<string, string> EnvironmentVariables { get; private set; }
        public string WorkingDirectory { get; private set; }
        public bool FireAndForget { get; private set; }
        public bool HaltOnError { get; private set; }
    }
}