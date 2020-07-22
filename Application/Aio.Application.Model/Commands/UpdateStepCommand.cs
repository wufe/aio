using System.Collections.Generic;
using MediatR;

namespace Aio.Application.Model.Commands {
    public class UpdateStepCommand : IRequest {
        public string BuildID { get; set; }
        public int StepIndex { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public string CommandExecutable { get; set; }
        public string CommandArguments { get; set; }
        public IEnumerable<string> EnvironmentVariables { get; set; } = new List<string>();
        public string WorkingDirectory { get; set; }
        public bool FireAndForget { get; set; }
        public bool HaltOnError { get; set; }

        public static UpdateStepCommand FromBuild(string buildID, int stepIndex) {
            return new UpdateStepCommand() {
                BuildID = buildID,
                StepIndex = stepIndex
            };
        }
    }
}