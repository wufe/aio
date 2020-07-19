using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Ghoul.Application.Model.Build;
using Ghoul.Application.Model.Queries;
using Ghoul.Persistence.Model;
using Ghoul.Persistence.Repository.Interface;
using MediatR;

namespace Ghoul.Application.Service.Handlers.Queries {
    public class GetLatestRunQueryHandler : IRequestHandler<GetLatestRunQuery, RunApplicationModel>
    {
        private readonly IMapper _mapper;
        private readonly IReadRepository<RunPersistenceModel> _runRepository;

        public GetLatestRunQueryHandler(
            IMapper mapper,
            IReadRepository<RunPersistenceModel> runRepository
        )
        {
            _mapper = mapper;
            _runRepository = runRepository;
        }

        public Task<RunApplicationModel> Handle(GetLatestRunQuery request, CancellationToken cancellationToken)
        {
            var latestRunPersistenceModel = _runRepository
                .FindAll(r => r.BuildID == request.BuildID)
                .OrderByDescending(r => r.StartedAt)
                .FirstOrDefault();

            return Task.FromResult(_mapper.Map<RunApplicationModel>(latestRunPersistenceModel));
        }
    }
}