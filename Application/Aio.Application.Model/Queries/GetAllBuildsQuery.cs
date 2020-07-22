using System.Collections.Generic;
using Aio.Application.Model.Build;
using MediatR;

namespace Aio.Application.Model.Queries {
    public class GetAllBuildsQuery : IRequest<BaseBuildApplicationModel[]> {
        
    }
}