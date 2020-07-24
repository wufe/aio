using System;
using System.Threading;
using System.Threading.Tasks;
using Aio.Application.Model.Commands;
using Aio.Domain.Entity.Build;
using Aio.Persistence.Model;
using Aio.Persistence.Repository.Interface;
using AutoMapper;
using MediatR;

namespace Aio.Application.Service.Handlers.Commands {
    public class UpdateStepsOrderCommandHandler : IRequestHandler<UpdateStepsOrderCommand>
    {
        private readonly IMapper _mapper;
        private readonly IRepository<BuildPersistenceModel> _buildRepository;

        public UpdateStepsOrderCommandHandler(
            IMapper mapper,
            IRepository<BuildPersistenceModel> buildRepository
        )
        {
            _mapper = mapper;
            _buildRepository = buildRepository;
        }
        public Task<Unit> Handle(UpdateStepsOrderCommand request, CancellationToken cancellationToken)
        {
            // Retrieval
            var buildPersistenceModel = _buildRepository.Find(request.BuildID);
            if (buildPersistenceModel == null)
                throw new ArgumentNullException($"Build with id \"{request.BuildID}\" does not exist.");

            // Conversion to domain entity
            var buildDomainEntity = _mapper.Map<BuildDomainEntity>(buildPersistenceModel);

            // Business logic
            buildDomainEntity.UpdateStepsOrder(request.StartIndex, request.EndIndex);

            // Conversion to persistence model
            buildPersistenceModel = _mapper.Map<BuildPersistenceModel>(buildDomainEntity);

            // Persist
            _buildRepository.Update(buildPersistenceModel);

            return Task.FromResult(new Unit());
        }
    }
}