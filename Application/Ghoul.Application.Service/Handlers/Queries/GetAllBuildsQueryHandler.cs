using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ghoul.Application.Model.Queries;
using Ghoul.Persistence.Model;
using Ghoul.Persistence.Repository.Interface;
using MediatR;

namespace Ghoul.Application.Service.Handlers.Queries {
    public class GetAllBuildsQueryHandler : IRequestHandler<GetAllBuildsQuery, string[]>
    {
        private readonly IReadRepository<Build> _buildRepository;

        public GetAllBuildsQueryHandler(IReadRepository<Build> buildRepository)
        {
            _buildRepository = buildRepository;
        }

        public Task<string[]> Handle(GetAllBuildsQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_buildRepository.FindAll().Select(x => x.Name).ToArray());
        }
    }
}