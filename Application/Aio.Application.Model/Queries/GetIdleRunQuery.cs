using Aio.Application.Model.Build;
using MediatR;

namespace Aio.Application.Model.Queries {
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