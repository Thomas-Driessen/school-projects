using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using minecraft_panel_api.Authorisation.DAL.Interfaces;

namespace minecraft_panel_api.Authorisation.DAL.Classes
{
    public class EmailService : IEmailService
    {
        private readonly IUserDbAccess _userDbAccess;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;
        
        public EmailService(IUserDbAccess userDbAccess, UserManager<IdentityUser> userManager, IConfiguration configuration)
        {
            _userDbAccess = userDbAccess;
            _userManager = userManager;
            _configuration = configuration;
        }
        
        public async Task RequestPasswordReset(string email)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email)) throw new NullReferenceException("The supplied email was null!");
                IdentityUser identityUser = await _userDbAccess.GetIdentityUserByEmail(email);

                string resetToken = await _userManager.GeneratePasswordResetTokenAsync(identityUser);
                string resetUrl = "/#/resetpassword?token=" + HttpUtility.UrlEncode(resetToken) + "&email=" + identityUser.Email;
                string emailMessage = "<html><body>Your password reset link: <a href=" + resetUrl + ">here</a></body><html>";
                MailAddress from = new MailAddress("noreply@mcpanel.com");
                MailAddress to = new MailAddress(identityUser.Email);
                MailMessage mailMessage = new MailMessage(from, to);
                mailMessage.Body = emailMessage;
                mailMessage.IsBodyHtml = true;
                mailMessage.Subject = "Password reset";


                //SMTPClient is niet meer reccomended
                var client = new SmtpClient("smtp.mailtrap.io", 2525)
                {
                        Credentials = new NetworkCredential(_configuration.GetSection("MailTrapCredentials").GetSection("Username").Value,
                            _configuration.GetSection("MailTrapCredentials").GetSection("Password").Value),
                    EnableSsl = true
                };
                client.Send(mailMessage);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        
        public async Task EmailConfirmation(string email, string token)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email)) throw new NullReferenceException("The supplied email was null!");

                string resetUrl = "/#/confirmemail?token=" + HttpUtility.UrlEncode(token);
                string emailMessage = "<html><body>Your email confirmation link: <a href=" + resetUrl + ">here</a></body><html>";
                MailAddress from = new MailAddress("noreply@mcpanel.com");
                MailAddress to = new MailAddress(email);
                MailMessage mailMessage = new MailMessage(from, to);
                mailMessage.Body = emailMessage;
                mailMessage.IsBodyHtml = true;
                mailMessage.Subject = "Email reset";


                //SMTPClient is niet meer reccomended
                var client = new SmtpClient("smtp.mailtrap.io", 2525)
                {
                    Credentials = new NetworkCredential(_configuration.GetSection("MailTrapCredentials").GetSection("Username").Value,
                        _configuration.GetSection("MailTrapCredentials").GetSection("Password").Value),
                    EnableSsl = true
                };
                client.Send(mailMessage);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}