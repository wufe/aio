using MediatR;

namespace Ghoul.Application.Model.Queries {
    public class GetBuildQuery : IRequest<BuildApplicationModel> {
        public string ID { get; private set; }

        public GetBuildQuery(string id)
        {
            ID = id;
        }
    }
}