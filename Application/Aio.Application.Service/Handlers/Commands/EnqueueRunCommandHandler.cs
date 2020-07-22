using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Aio.Application.Model.Build;
using Aio.Application.Model.Commands;
using Aio.Domain.Service.Interface;
using Aio.Persistence.Model;
using Aio.Persistence.Repository.Interface;
using MediatR;

namespace Aio.Application.Service.Handlers.Commands {
    public class EnqueueRunCommandHandler : IRequestHandler<EnqueueRunCommand, string>
    {
        private readonly IMapper _mapper;
        private readonly IRepository<RunPersistenceModel> _runRepository;
        private readonly IBuildDomainService _buildService;

        public EnqueueRunCommandHandler(
            IMapper mapper,
            IRepository<RunPersistenceModel> runRepository,
            IBuildDomainService buildService
        )
        {
            _mapper = mapper;
            _runRepository = runRepository;
            _buildService = buildService;
        }

        public Task<string> Handle(EnqueueRunCommand request, CancellationToken cancellationToken)
        {
            // Business logic
            var runDomainEntity = _buildService.CreateRun(request.BuildID);

            // Conversion
            var runPersistenceModel = _mapper.Map<RunPersistenceModel>(runDomainEntity);

            // Persist
            _runRepository.Insert(runPersistenceModel);

            return Task.FromResult(runPersistenceModel.ID);
        }
    }
}