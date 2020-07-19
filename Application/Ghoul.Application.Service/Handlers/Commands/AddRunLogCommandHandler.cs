using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Ghoul.Application.Model.Commands;
using Ghoul.Domain.Entity.Build;
using Ghoul.Persistence.Model;
using Ghoul.Persistence.Repository.Interface;
using MediatR;

namespace Ghoul.Application.Service.Handlers.Commands {
    public class AddRunLogCommandHandler : IRequestHandler<AddRunLogCommand>
    {
        private readonly IMapper _mapper;
        private readonly IRepository<RunPersistenceModel> _runRepository;

        public AddRunLogCommandHandler(
            IMapper mapper,
            IRepository<RunPersistenceModel> runRepository
        )
        {
            _mapper = mapper;
            _runRepository = runRepository;
        }

        public Task<Unit> Handle(AddRunLogCommand request, CancellationToken cancellationToken)
        {
            // Retrieval
            var runPersistenceModel = _runRepository.Find(request.RunID);
            if (runPersistenceModel == null)
                throw new Exception($"Run with id \"{request.RunID}\" does not exist.");

            // Conversion
            var runDomainEntity = _mapper.Map<RunDomainEntity>(runPersistenceModel);

            // Business logic
            RunDomainEntity.RunLogType logType = RunDomainEntity.RunLogType.Stdout;
            switch (request.LogType) {
                case LogType.Stderr:
                    logType = RunDomainEntity.RunLogType.Stderr;
                    break;
                case LogType.Stdout:
                default:
                    logType = RunDomainEntity.RunLogType.Stdout;
                    break;
            }

            runDomainEntity.AddLog(request.Log, logType);

            // Conversion
            runPersistenceModel = _mapper.Map<RunPersistenceModel>(runDomainEntity);

            // Persist
            _runRepository.Update(runPersistenceModel);

            return Task.FromResult(new Unit());
        }
    }
}