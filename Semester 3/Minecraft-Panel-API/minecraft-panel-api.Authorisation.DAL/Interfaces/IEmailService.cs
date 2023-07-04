using System.Threading.Tasks;

namespace minecraft_panel_api.Authorisation.DAL.Interfaces
{
    public interface IEmailService
    {
        public Task RequestPasswordReset(string email);
        public Task EmailConfirmation(string email, string token);
    }
}