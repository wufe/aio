using Aio.Application.Model.Build;
using MediatR;

namespace Aio.Application.Model.Queries {
    public class GetBuildQuery : IRequest<BuildApplicationModel> {
        public string ID { get; private set; }

        public GetBuildQuery(string id)
        {
            ID = id;
        }
    }
}