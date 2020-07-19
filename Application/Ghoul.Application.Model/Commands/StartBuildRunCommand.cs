using MediatR;

namespace Ghoul.Application.Model.Commands {
    public class StartBuildRunCommand : IRequest {

        public string BuildID { get; set; }
        public string RunID { get; set; }

        public StartBuildRunCommand(string buildID, string runID)
        {
            BuildID = buildID;
            RunID = runID;
        }

    }
}