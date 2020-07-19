using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Ghoul.Application.Model.Commands;
using Ghoul.Domain.Entity.Build;
using Ghoul.Persistence.Model;
using Ghoul.Persistence.Repository.Interface;
using MediatR;

namespace Ghoul.Application.Service.Handlers.Commands {
    public class EndStepCommandHandler : IRequestHandler<EndStepCommand>
    {
        private readonly IMapper _mapper;
        private readonly IRepository<BuildPersistenceModel> _buildRepository;

        public EndStepCommandHandler(
            IMapper mapper,
            IRepository<BuildPersistenceModel> buildRepository
        )
        {
            _mapper = mapper;
            _buildRepository = buildRepository;
        }

        public Task<Unit> Handle(EndStepCommand request, CancellationToken cancellationToken)
        {
            // Retrieval
            var buildPersistenceModel = _buildRepository.Find(request.BuildID);
            if (buildPersistenceModel == null)
                throw new Exception($"Build with id \"{request.BuildID}\" does not exist.");
            if (request.StepIndex >= buildPersistenceModel.Steps.Count())
                throw new Exception($"Step ad index {request.StepIndex} does not exist.");

            // Conversion
            var buildDomainEntity = _mapper.Map<BuildDomainEntity>(buildPersistenceModel);

            // Business logic
            BuildStepDomainEntity.StepOutcome outcome = BuildStepDomainEntity.StepOutcome.Unknown;
            switch (request.Outcome) {
                case StepOutcome.Success:
                    outcome = BuildStepDomainEntity.StepOutcome.Success;
                    break;
                
                case StepOutcome.Fail:
                    outcome = BuildStepDomainEntity.StepOutcome.Fail;
                    break;
                case StepOutcome.Unknown:
                default:
                    outcome = BuildStepDomainEntity.StepOutcome.Unknown;
                    break;
            }

            buildDomainEntity.StopStep(request.StepIndex, outcome);

            // Conversion
            buildPersistenceModel = _mapper.Map<BuildPersistenceModel>(buildDomainEntity);

            // Persist
            _buildRepository.Update(buildPersistenceModel);

            return Task.FromResult(new Unit());
        }
    }
}