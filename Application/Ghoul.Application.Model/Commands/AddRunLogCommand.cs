using MediatR;

namespace Ghoul.Application.Model.Commands {
    public class AddRunLogCommand : IRequest {
        public string RunID { get; set; }
        public string Log { get; set; }
        public LogType LogType { get; set; }

        public AddRunLogCommand(string runID, string log, LogType logType)
        {
            RunID = runID;
            Log = log;
            LogType = logType;
        }
    }

    public enum LogType {
        Stdout,
        Stderr
    }
}