using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Ghoul.Application.Model.Commands;
using Ghoul.Domain.Entity.Build;
using Ghoul.Domain.Service.Interface;
using Ghoul.Persistence.Model;
using Ghoul.Persistence.Repository.Interface;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ghoul.Application.Service.Handlers.Commands {
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
            // Validate
            var buildPersistenceModel = _buildRepository.Find(request.BuildID);
            if (buildPersistenceModel == null)
                throw new ArgumentException($"Cannot find build with id \"{request.BuildID}\"");
            if (buildPersistenceModel.Steps.Any(step => step.Name.ToLower() == request.Name.ToLower()))
                throw new ArgumentException($"A step named \"{request.Name}\" has already been created.");

            // Conversion to domain entity
            var buildDomainEntity = _mapper.Map<BuildDomainEntity>(buildPersistenceModel);

            // Update domain entity
            buildDomainEntity.AppendNewStep(request.Name);

            // Convert domain entity to persistence model
            buildPersistenceModel = _mapper.Map<BuildPersistenceModel>(buildDomainEntity);

            // Store persistence model
            _buildRepository.Update(buildPersistenceModel);

            return Task.FromResult(request.Name);
        }
    }
}