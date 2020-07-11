using System.Threading;
using System.Threading.Tasks;
using Ghoul.Application.Model.Commands;
using Ghoul.Persistence.Model;
using Ghoul.Persistence.Repository.Interface;
using MediatR;

namespace Ghoul.Application.Service.Handlers.Commands {
    public class CreateBuildCommandHandler : IRequestHandler<CreateBuildCommand, string>
    {
        private readonly IRepository<Build> _buildRepository;

        public CreateBuildCommandHandler(IRepository<Build> buildRepository)
        {
            _buildRepository = buildRepository;
        }

        public Task<string> Handle(CreateBuildCommand request, CancellationToken cancellationToken)
        {
            var build = new Build() { Name = request.Name };
            _buildRepository.Insert(build);
            return Task.FromResult(build.ID);
        }
    }
}