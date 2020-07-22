using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Aio.Application.Model.Commands;
using Aio.Domain.Entity.Build;
using Aio.Domain.Service.Interface;
using Aio.Persistence.Model;
using Aio.Persistence.Repository.Interface;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Aio.Application.Service.Handlers.Commands {
    public class CreateStepCommandHandler : IRequestHandler<CreateStepCommand, string>
    {
        private readonly ILogger<CreateStepCommandHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IBuildDomainService _buildService;
        private readonly IRepository<BuildPersistenceModel> _buildRepository;

        public CreateStepCommandHandler(
            ILogger<CreateStepCommandHandler> logger,
            IMapper mapper,
            IBuildDomainService buildService,
            IRepository<BuildPersistenceModel> buildRepository
        )
        {
            _logger = logger;
            _mapper = mapper;
            _buildService = buildService;
            _buildRepository = buildRepository;
        }

        public Task<string> Handle(CreateStepCommand request, CancellationToken cancellationToken)
        {
            // Retrieve
            var buildPersistenceModel = _buildRepository.Find(request.BuildID);

            // Validate
            if (buildPersistenceModel == null)
                throw new ArgumentException($"Cannot find build with id \"{request.BuildID}\"");
            
            // Convert to domain entity
            var buildDomainEntity = _mapper.Map<BuildDomainEntity>(buildPersistenceModel);

            // Add step to build domain entity
            buildDomainEntity = _buildService.AppendNewStepToBuild(buildDomainEntity, request.Name);

            // Convert domain entity to persistence model
            buildPersistenceModel = _mapper.Map<BuildPersistenceModel>(buildDomainEntity);

            // Store persistence model
            _buildRepository.Update(buildPersistenceModel);

            return Task.FromResult(request.Name);
        }
    }
}