using MediatR;

namespace Ghoul.Application.Model.Commands {
    public class DeleteStepCommand : IRequest {
        public string BuildID { get; set; }
        public int StepIndex { get; set; }

        public DeleteStepCommand(string buildID, int stepIndex)
        {
            BuildID = buildID;
            StepIndex = stepIndex;
        }
    }
}