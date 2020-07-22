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
    public class DeleteStepCommandHandler : IRequestHandler<DeleteStepCommand>
    {
        private readonly IMapper _mapper;
        private readonly IRepository<BuildPersistenceModel> _buildRepository;

        public DeleteStepCommandHandler(
            IMapper mapper,
            IRepository<BuildPersistenceModel> buildRepository
        )
        {
            _mapper = mapper;
            _buildRepository = buildRepository;
        }

        public Task<Unit> Handle(DeleteStepCommand request, CancellationToken cancellationToken)
        {

            // Retrieval
            var buildPersistenceModel = _buildRepository.Find(request.BuildID);
            if (buildPersistenceModel == null)
                throw new Exception($"Build with id \"{request.BuildID}\" does not exist.");

            // Conversion
            var buildDomainEntity = _mapper.Map<BuildDomainEntity>(buildPersistenceModel);

            // Business logic
            buildDomainEntity.RemoveStep(request.StepIndex);

            // Conversion
            buildPersistenceModel = _mapper.Map<BuildPersistenceModel>(buildDomainEntity);

            // Persist
            _buildRepository.Update(buildPersistenceModel);


            return Task.FromResult(new Unit());
        }
    }
}