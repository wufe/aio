using Ghoul.Application.Model.Interface;

namespace Ghoul.Application.Service.Interface
{
    public interface IBuildApplicationService
    {
        void CreateBuild(ICreateBuildModel model);
    }
}