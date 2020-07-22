using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Aio.Application.Model;
using Aio.Application.Model.Build;
using Aio.Application.Model.Queries;
using Aio.Persistence.Model;
using Aio.Persistence.Repository.Interface;
using MediatR;

namespace Aio.Application.Service.Handlers.Queries {
    public class GetBuildQueryHandler: IRequestHandler<GetBuildQuery, BuildApplicationModel> {
        private readonly IMapper _mapper;
        private readonly IReadRepository<BuildPersistenceModel> _buildRepository;

        public GetBuildQueryHandler(
            IMapper mapper,
            IReadRepository<BuildPersistenceModel> buildRepository
        )
        {
            _mapper = mapper;
            _buildRepository = buildRepository;
        }

        public Task<BuildApplicationModel> Handle(GetBuildQuery request, CancellationToken cancellationToken)
        {
            var buildPersistenceModel = _buildRepository.Find(request.ID);
            if (buildPersistenceModel == null)
                return Task.FromResult<BuildApplicationModel>(null);

            var buildApplicationModel = _mapper.Map<BuildApplicationModel>(buildPersistenceModel);
            return Task.FromResult(buildApplicationModel);
        }
    }
}