using System.Collections.Generic;

namespace Ghoul.Application.Model.Build {
    public class BuildStepApplicationModel {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public string CommandExecutable { get; set; }
        public string CommandArguments { get; set; }
        public IDictionary<string, string> EnvironmentVariables { get; set; }
        public string WorkingDirectory { get; set; }
        public bool FireAndForget { get; set; }
        public bool HaltOnError { get; set; }
    }
}