using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Aio.Application.Model.Commands;
using Aio.Domain.Entity.Build;
using Aio.Persistence.Model;
using Aio.Persistence.Repository.Interface;
using MediatR;

namespace Aio.Application.Service.Handlers.Commands {
    public class UpdateBuildCommandHandler : IRequestHandler<UpdateBuildCommand>
    {
        private readonly IMapper _mapper;
        private readonly IRepository<BuildPersistenceModel> _buildRepository;

        public UpdateBuildCommandHandler(
            IMapper mapper,
            IRepository<BuildPersistenceModel> buildRepository
        )
        {
            _mapper = mapper;
            _buildRepository = buildRepository;
        }

        public Task<Unit> Handle(UpdateBuildCommand request, CancellationToken cancellationToken)
        {
            // Parsing of request
            if (!Enum.TryParse<BuildStatus>(request.Status, out var buildStatus))
                throw new ArgumentException("Could not parse build status.");

            // Retrieval
            var buildPersistenceModel = _buildRepository.Find(request.ID);

            // Validation
            if (buildPersistenceModel == null)
                throw new ArgumentNullException($"Build with ID \"{request.ID}\" not found.");

            // Conversion to domain entity
            var buildDomainEntity = _mapper.Map<BuildDomainEntity>(buildPersistenceModel);

            // Update
            buildDomainEntity
                .SetName(request.Name)
                .SetRepository(request.RepositoryURL, request.RepositoryTrigger);

            // Conversion to persistence model
            buildPersistenceModel = _mapper.Map<BuildPersistenceModel>(buildDomainEntity);

            // Save
            _buildRepository.Update(buildPersistenceModel);

            return Task.FromResult(new Unit());
        }
    }
}