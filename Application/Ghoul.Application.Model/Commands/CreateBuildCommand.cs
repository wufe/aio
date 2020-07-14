using MediatR;

namespace Ghoul.Application.Model.Commands {

    // Command lato applicativo
    //
    // Il COMANDO/QUERY (evento) implementa IRequest<Tipo di ritorno dell'evento>
    public class CreateBuildCommand : IRequest<string> { // <--- MediatR.IRequest
        // anche qui name e repositoryurl
        public string Name { get; private set; }
        public string RepositoryURL { get; private set; }

        public CreateBuildCommand(string name, string repositoryURL = null)
        {
            Name = name;
            RepositoryURL = repositoryURL;
        }
    }
}