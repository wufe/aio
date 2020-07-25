using MediatR;

namespace Aio.Application.Model.Commands {
    public class UpdateBuildsOrderCommand : IRequest {
        public int StartIndex { get; set; }
        public int EndIndex { get; set; }
    }
}