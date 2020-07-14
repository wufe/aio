using MediatR;

namespace Ghoul.Application.Model.Commands {

    public class CreateBuildCommand : IRequest<string> {
        public string Name { get; private set; }
        public string RepositoryURL { get; private set; }

        public CreateBuildCommand(string name, string repositoryURL = null)
        {
            Name = name;
            RepositoryURL = repositoryURL;
        }
    }
}