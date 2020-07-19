using MediatR;

namespace Ghoul.Application.Model.Commands {
    public class StopBuildRunCommand : IRequest {
        public string BuildID { get; set; }
        public string RunID { get; set; }
        
        public StopBuildRunCommand(string buildID, string runID)
        {
            BuildID = buildID;
            RunID = runID;
        }
    }
}