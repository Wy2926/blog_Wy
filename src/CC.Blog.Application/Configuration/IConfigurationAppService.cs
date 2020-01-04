using System.Threading.Tasks;
using CC.Blog.Configuration.Dto;

namespace CC.Blog.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
