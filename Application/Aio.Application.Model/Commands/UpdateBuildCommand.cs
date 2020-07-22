using MediatR;

namespace Aio.Application.Model.Commands {
    public class UpdateBuildCommand : IRequest {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public string RepositoryURL { get; set; }
        public string RepositoryTrigger { get; set; }

        public static UpdateBuildCommand FromBuildID(string id) {
            return new UpdateBuildCommand() {
                ID = id
            };
        }
    }
}