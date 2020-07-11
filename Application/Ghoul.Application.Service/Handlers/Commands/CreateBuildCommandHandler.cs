using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Ghoul.Application.Model.Commands;
using Ghoul.Domain.Service.Interface;
using Ghoul.Persistence.Model;
using Ghoul.Persistence.Repository.Interface;
using MediatR;

namespace Ghoul.Application.Service.Handlers.Commands {
    public class CreateBuildCommandHandler : IRequestHandler<CreateBuildCommand, string>
    {
        private readonly IMapper _mapper;
        private readonly IBuildDomainService _buildService;
        private readonly IRepository<BuildPersistenceModel> _buildRepository;

        public CreateBuildCommandHandler(
            IMapper mapper,
            IBuildDomainService buildService,
            IRepository<BuildPersistenceModel> buildRepository)
        {
            _mapper = mapper;
            _buildService = buildService;
            _buildRepository = buildRepository;
        }

        public Task<string> Handle(CreateBuildCommand request, CancellationToken cancellationToken)
        {
            // Validate
            var buildAlreadyExists = _buildRepository.FindAll(x => x.Name.ToLower() == request.Name.ToLower()).Any();
            if (buildAlreadyExists)
                throw new ArgumentException($"Build named \"{request.Name}\" already exists.");

            // Create domain entity
            var buildDomainEntity = _buildService.CreateBuild(request.Name);

            // Convert domain entity to persistence model
            var buildPersistenceModel = _mapper.Map<BuildPersistenceModel>(buildDomainEntity);

            // Store persistence model
            _buildRepository.Insert(buildPersistenceModel);
            return Task.FromResult(buildPersistenceModel.ID);
        }
    }
}