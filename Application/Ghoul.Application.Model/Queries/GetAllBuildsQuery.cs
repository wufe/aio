using System.Collections.Generic;
using Ghoul.Application.Model.Build;
using MediatR;

namespace Ghoul.Application.Model.Queries {

    // E' una query (evento) vuoto
    // Serve solo a far capire al mediator che voglio eseguire proprio la query su tutte le build

    public class GetAllBuildsQuery : IRequest<BaseBuildApplicationModel[]> {
        
    }
}