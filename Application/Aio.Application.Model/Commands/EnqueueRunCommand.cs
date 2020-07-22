using Aio.Application.Model.Build;
using MediatR;

namespace Aio.Application.Model.Commands {
    public class EnqueueRunCommand : IRequest<string> {
        public string BuildID { get; set; }

        public EnqueueRunCommand(string buildID)
        {
            BuildID = buildID;
        }
    }
}