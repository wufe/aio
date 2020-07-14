using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Ghoul.Application.Model;
using Ghoul.Application.Model.Build;
using Ghoul.Application.Model.Queries;
using Ghoul.Persistence.Model;
using Ghoul.Persistence.Repository.Interface;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ghoul.Application.Service.Handlers.Queries {
    public class GetAllBuildsQueryHandler : IRequestHandler<GetAllBuildsQuery, BaseBuildApplicationModel[]>
    {
        private readonly ILogger<GetAllBuildsQueryHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IReadRepository<BuildPersistenceModel> _buildRepository;

        public GetAllBuildsQueryHandler (
            ILogger<GetAllBuildsQueryHandler> logger,
            IMapper mapper,
            IReadRepository<BuildPersistenceModel> buildRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _buildRepository = buildRepository;
        }

        public Task<BaseBuildApplicationModel[]> Handle(GetAllBuildsQuery request, CancellationToken cancellationToken)
        {
            var buildApplicationModels = _buildRepository
                .FindAll()
                .ProjectTo<BaseBuildApplicationModel>(_mapper.ConfigurationProvider);

            _logger.LogTrace(buildApplicationModels.ToString());

            return Task.FromResult(buildApplicationModels.ToArray());
        }
    }
}