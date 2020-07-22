using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Aio.Application.Model.Commands;
using Aio.Domain.Entity.Build;
using Aio.Persistence.Model;
using Aio.Persistence.Repository.Interface;
using MediatR;

namespace Aio.Application.Service.Handlers.Commands {
    public class UpdateStepCommandHandler : IRequestHandler<UpdateStepCommand>
    {
        private readonly IMapper _mapper;
        private readonly IRepository<BuildPersistenceModel> _buildRepository;

        public UpdateStepCommandHandler(
            IMapper mapper,
            IRepository<BuildPersistenceModel> buildRepository
        )
        {
            _mapper = mapper;
            _buildRepository = buildRepository;
        }

        public Task<Unit> Handle(UpdateStepCommand request, CancellationToken cancellationToken)
        {
            // Parsing of request
            IDictionary<string, string> environmentVariables;
            try {
                environmentVariables = request.EnvironmentVariables
                    .ToDictionary(env => env.Split('=')[0], env => env.Split('=')[1]);
            } catch (Exception e) {
                throw new ArgumentException("Could not parse environment variables.", e);
            }

            if (!Enum.TryParse<BuildStepStatus>(request.Status, out var stepStatus)) {
                throw new ArgumentException("Could not parse step status.");
            }

            // Retrieval
            var buildPersistenceModel = _buildRepository.Find(request.BuildID);

            // Validation
            if (buildPersistenceModel == null)
                throw new ArgumentNullException($"Build with ID \"{request.BuildID}\" not found.");
            if (buildPersistenceModel.Steps.Count() <= request.StepIndex)
                throw new ArgumentNullException($"Step with index {request.StepIndex} for build with ID \"{request.BuildID}\" not found.");

            // Conversion to domain entity
            var buildDomainEntity = _mapper.Map<BuildDomainEntity>(buildPersistenceModel);

            // Update
            buildDomainEntity
                .WithStepAt(request.StepIndex)
                .SetCommand(request.CommandExecutable, request.CommandArguments)
                .SetEnvironmentVariables(environmentVariables)
                .SetWorkingDirectory(request.WorkingDirectory)
                .SetFireAndForget(request.FireAndForget)
                .SetHaltOnError(request.HaltOnError);

            // Conversion to persistence model
            buildPersistenceModel = _mapper.Map<BuildPersistenceModel>(buildDomainEntity);

            // Save
            _buildRepository.Update(buildPersistenceModel);

            return Task.FromResult(new Unit());
        }
    }
}