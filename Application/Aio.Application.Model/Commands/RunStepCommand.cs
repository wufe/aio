using MediatR;

namespace Aio.Application.Model.Commands {
    public class RunStepCommand : IRequest {
        public string BuildID { get; set; }
        public int StepIndex { get; set; }

        public RunStepCommand(string buildID, int stepIndex)
        {
            BuildID = buildID;
            StepIndex = stepIndex;
        }
    }
}