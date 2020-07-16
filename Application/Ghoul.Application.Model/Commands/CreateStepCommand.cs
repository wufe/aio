using MediatR;

namespace Ghoul.Application.Model.Commands {
    public class CreateStepCommand : IRequest<string> {
        public string BuildID { get; set; }
        public string Name { get; set; }
        public static CreateStepCommand FromBuild(string buildID) {
            return new CreateStepCommand() {
                BuildID = buildID
            };
        }
    }
}