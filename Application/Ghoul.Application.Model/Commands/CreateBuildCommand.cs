using MediatR;

namespace Ghoul.Application.Model.Commands {

    public class CreateBuildCommand : IRequest<string> {
        public string Name { get; private set; }

        public CreateBuildCommand(string name)
        {
            Name = name;
        }
    }
}