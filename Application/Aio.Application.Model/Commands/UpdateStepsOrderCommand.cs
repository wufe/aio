using MediatR;

namespace Aio.Application.Model.Commands {
    public class UpdateStepsOrderCommand : IRequest {
        public string BuildID { get; protected set; }
        public int StartIndex { get; protected set; }
        public int EndIndex { get; protected set; }

        public UpdateStepsOrderCommand(string buildID)
        {
            BuildID = buildID;
        }
    }
}