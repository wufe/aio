using Ghoul.Application.Model.Build;
using MediatR;

namespace Ghoul.Application.Model.Queries {
    public class GetIdleRunQuery : IRequest<GetIdleRunQueryResponse> {}

    public class GetIdleRunQueryResponse {
        public BuildApplicationModel Build { get; set; }
        public RunApplicationModel Run { get; set; }

        public GetIdleRunQueryResponse(BuildApplicationModel build, RunApplicationModel run)
        {
            Build = build;
            Run = run;
        }
    }
}