using System.Collections.Generic;
using System.Linq;
using Ghoul.Domain.Entity.Build;
using Ghoul.Domain.Entity.Build.Containers;

namespace Ghoul.Domain.Repository.Interface.Build
{
    public interface IBuildRepository
    {
        bool BuildExistsByName(string name);
        bool BuildExistsByID(string name);
        BuildRunContainer GetNextIdleRun();
    }
}