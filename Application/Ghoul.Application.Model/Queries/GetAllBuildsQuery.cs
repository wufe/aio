using System.Collections.Generic;
using Ghoul.Application.Model.Build;
using MediatR;

namespace Ghoul.Application.Model.Queries {
    public class GetAllBuildsQuery : IRequest<BuildBaseApplicationModel[]> {
        
    }
}