using Ghoul.Application.Model.Build;
using MediatR;

namespace Ghoul.Application.Model.Commands {
    public class EnqueueRunCommand : IRequest<string> {
        public string BuildID { get; set; }

        public EnqueueRunCommand(string buildID)
        {
            BuildID = buildID;
        }
    }
}