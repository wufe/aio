using Ghoul.Application.Model.Build;
using MediatR;

namespace Ghoul.Application.Model.Queries {
    public class GetLatestRunQuery : IRequest<RunApplicationModel> {
        public string BuildID { get; set; }
        public static GetLatestRunQuery FromBuild(string buildID) {
            return new GetLatestRunQuery() {
                BuildID = buildID
            };
        }
    }
}