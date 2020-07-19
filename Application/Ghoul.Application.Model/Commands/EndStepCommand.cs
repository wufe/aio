using MediatR;

namespace Ghoul.Application.Model.Commands {

    public enum StepOutcome {
        Success,
        Fail,
        Unknown
    }

    public class EndStepCommand : IRequest {
        public string BuildID { get; set; }
        public int StepIndex { get; set; }
        public StepOutcome Outcome { get; set; }

        public EndStepCommand(string buildID, int stepIndex, StepOutcome outcome)
        {
            BuildID = buildID;
            StepIndex = stepIndex;
            Outcome = outcome;
        }
    }
}