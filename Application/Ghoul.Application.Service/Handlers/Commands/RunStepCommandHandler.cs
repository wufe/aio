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
    public class RunStepCommandHandler : IRequestHandler<RunStepCommand>
    {
        private readonly IMapper _mapper;
        private readonly IRepository<BuildPersistenceModel> _buildRepository;

        public RunStepCommandHandler(
            IMapper mapper,
            IRepository<BuildPersistenceModel> buildRepository
        )
        {
            _mapper = mapper;
            _buildRepository = buildRepository;
        }

        public Task<Unit> Handle(RunStepCommand request, CancellationToken cancellationToken)
        {
            // Retrieval
            var buildPersistenceModel = _buildRepository.Find(request.BuildID);
            if (buildPersistenceModel == null)
                throw new Exception($"Build with id \"{request.BuildID}\" does not exist.");
            if (request.StepIndex >= buildPersistenceModel.Steps.Count())
                throw new Exception($"Build does not contain a step at index {request.StepIndex}.");

            // Conversion
            var buildDomainEntity = _mapper.Map<BuildDomainEntity>(buildPersistenceModel);

            // Business logic
            buildDomainEntity.RunStep(request.StepIndex);

            // Conversion
            buildPersistenceModel = _mapper.Map<BuildPersistenceModel>(buildDomainEntity);

            // Persist
            _buildRepository.Update(buildPersistenceModel);

            return Task.FromResult(new Unit());
        }
    }
}