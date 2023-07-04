using System.Threading.Tasks;
using IdentityServer4.Services;

namespace minecraft_panel_api.Authorisation.DAL.Interfaces
{
    public interface ITokenServiceAccess
    {
        public Task<string> IssueJwtToken(string id, string username, string role);
    }
}