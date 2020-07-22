using System.Threading;
using System.Threading.Tasks;
using Aio.Application.Model.Commands;
using Aio.Persistence.Model;
using Aio.Persistence.Repository.Interface;
using MediatR;

namespace Aio.Application.Service.Handlers.Commands {
    public class DeleteBuildCommandHandler : IRequestHandler<DeleteBuildCommand>
    {
        private readonly IRepository<BuildPersistenceModel> _buildRepository;

        public DeleteBuildCommandHandler(
            IRepository<BuildPersistenceModel> buildRepository
        )
        {
            _buildRepository = buildRepository;
        }

        public Task<Unit> Handle(DeleteBuildCommand request, CancellationToken cancellationToken)
        {
            _buildRepository.Remove(request.ID);

            return Task.FromResult(new Unit());
        }
    }
}