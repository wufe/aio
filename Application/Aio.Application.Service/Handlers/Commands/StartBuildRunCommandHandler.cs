using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Aio.Application.Model.Commands;
using Aio.Domain.Entity.Build;
using Aio.Domain.Service.Interface;
using Aio.Persistence.Model;
using Aio.Persistence.Repository.Interface;
using MediatR;

namespace Aio.Application.Service.Handlers.Commands {
    public class StartBuildRunCommandHandler : IRequestHandler<StartBuildRunCommand>
    {
        private readonly IMapper _mapper;
        private readonly IRepository<BuildPersistenceModel> _buildRepository;
        private readonly IRepository<RunPersistenceModel> _runRepository;
        private readonly IBuildDomainService _buildService;

        public StartBuildRunCommandHandler(
            IMapper mapper,
            IRepository<BuildPersistenceModel> buildRepository,
            IRepository<RunPersistenceModel> runRepository,
            IBuildDomainService buildService
        )
        {
            _mapper = mapper;
            _buildRepository = buildRepository;
            _runRepository = runRepository;
            _buildService = buildService;
        }

        public Task<Unit> Handle(StartBuildRunCommand request, CancellationToken cancellationToken)
        {
            // Retrieval
            var buildPersistenceModel = _buildRepository.Find(request.BuildID);
            if (buildPersistenceModel == null)
                throw new ArgumentNullException($"Build with id \"{request.BuildID}\" does not exist.");

            var runPersistenceModel = _runRepository.Find(request.RunID);
            if (runPersistenceModel == null)
                throw new ArgumentNullException($"Run with id \"{request.RunID}\" does not exist.");

            // Conversion to domain entities
            var buildDomainEntity = _mapper.Map<BuildDomainEntity>(buildPersistenceModel);
            var runDomainEntity = _mapper.Map<RunDomainEntity>(runPersistenceModel);

            // Business logic
            var buildRunContainer = _buildService.StartBuildRun(buildDomainEntity, runDomainEntity);

            // Conversion to persistence models
            buildPersistenceModel = _mapper.Map<BuildPersistenceModel>(buildRunContainer.Build);
            runPersistenceModel = _mapper.Map<RunPersistenceModel>(buildRunContainer.Run);

            // Persist
            _buildRepository.Update(buildPersistenceModel);
            _runRepository.Update(runPersistenceModel);

            return Task.FromResult(new Unit());
        }
    }
}