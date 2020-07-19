using Ghoul.Application.Model.Build;
using MediatR;

namespace Ghoul.Application.Model.Queries {
    public class GetWaitingStepQuery : IRequest<GetWaitingStepQueryResponse> {
        public string BuildID { get; set; }

        public GetWaitingStepQuery(string buildID)
        {
            BuildID = buildID;
        }
    }

    public class GetWaitingStepQueryResponse {
        public int StepIndex { get; set; }
        public BuildStepApplicationModel Step { get; set; }

        public GetWaitingStepQueryResponse(BuildStepApplicationModel step, int stepIndex)
        {
            Step = step;
            StepIndex = stepIndex;
        }
    }
}