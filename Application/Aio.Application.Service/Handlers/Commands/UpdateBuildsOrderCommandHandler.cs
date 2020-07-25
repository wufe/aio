using System.Threading;
using System.Threading.Tasks;
using Aio.Application.Model.Commands;
using Aio.Domain.Entity.Build;
using Aio.Domain.Service.Interface;
using Aio.Persistence.Model;
using Aio.Persistence.Repository.Interface;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;

namespace Aio.Application.Service.Handlers.Commands {
    public class UpdateBuildsOrderCommandHandler : IRequestHandler<UpdateBuildsOrderCommand>
    {
        private readonly IMapper _mapper;
        private readonly IRepository<BuildPersistenceModel> _buildRepository;
        private readonly IBuildDomainService _buildService;

        public UpdateBuildsOrderCommandHandler(
            IMapper mapper,
            IRepository<BuildPersistenceModel> buildRepository,
            IBuildDomainService buildService
        )
        {
            _mapper = mapper;
            _buildRepository = buildRepository;
            _buildService = buildService;
        }

        public Task<Unit> Handle(UpdateBuildsOrderCommand request, CancellationToken cancellationToken)
        {
            // Domain logic
            var buildDomainEntities = _buildService.UpdateBuildsOrder(request.StartIndex, request.EndIndex);

            // Conversion to persistence models
            var buildPersistenceModels = _mapper.Map<BuildPersistenceModel[]>(buildDomainEntities);

            // Persist
            foreach (var model in buildPersistenceModels)
                _buildRepository.Update(model);

            return Task.FromResult(new Unit());
        }
    }
}