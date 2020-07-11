using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Ghoul.Application.Model;
using Ghoul.Application.Model.Queries;
using Ghoul.Persistence.Model;
using Ghoul.Persistence.Repository.Interface;
using MediatR;

namespace Ghoul.Application.Service.Handlers.Queries {
    public class GetAllBuildsQueryHandler : IRequestHandler<GetAllBuildsQuery, BuildApplicationModel[]>
    {
        private readonly IMapper _mapper;
        private readonly IReadRepository<BuildPersistenceModel> _buildRepository;

        public GetAllBuildsQueryHandler (
            IMapper mapper,
            IReadRepository<BuildPersistenceModel> buildRepository)
        {
            _mapper = mapper;
            _buildRepository = buildRepository;
        }

        public Task<BuildApplicationModel[]> Handle(GetAllBuildsQuery request, CancellationToken cancellationToken)
        {
            var buildPersistenceModels = _buildRepository.FindAll();
            var buildApplicationModels = _mapper.Map<BuildApplicationModel[]>(buildPersistenceModels);

            return Task.FromResult(buildApplicationModels);
        }
    }
}