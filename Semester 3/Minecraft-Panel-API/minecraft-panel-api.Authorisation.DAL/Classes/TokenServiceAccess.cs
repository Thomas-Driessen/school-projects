using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.Extensions.Configuration;
using minecraft_panel_api.Authorisation.DAL.Interfaces;

namespace minecraft_panel_api.Authorisation.DAL.Classes
{
    public class TokenServiceAccess : ITokenServiceAccess
    {
        private readonly IdentityServerTools _serverTools;
        private readonly IConfiguration _configuration;

        public TokenServiceAccess(IdentityServerTools serverTools, IConfiguration configuration)
        {
            _serverTools = serverTools;
            _configuration = configuration;
        }
        
        public async Task<string> IssueJwtToken(string id, string username, string role)
        {
            string token = await _serverTools.IssueJwtAsync(
                3600,
                _configuration["TokenIssuer"],
                new List<Claim> {
                    new Claim(JwtClaimTypes.JwtId, new Random().Next(1, 1000).ToString().Sha256()),
                    new Claim(JwtClaimTypes.Subject, id),
                    new Claim(JwtClaimTypes.Id, id),
                    new Claim(JwtClaimTypes.Name, username),
                    new Claim(JwtClaimTypes.Role, role),
                    new Claim(JwtClaimTypes.Scope, "api1"),
                    new Claim(JwtClaimTypes.Scope, "interaction"),
                    new Claim(JwtClaimTypes.Audience, "api1"),
                    new Claim(JwtClaimTypes.Audience, "interaction"),
                    new Claim(JwtClaimTypes.ClientId, "clientpasswordtest"),
                    //new Claim(JwtClaimTypes.Issuer, "http://mc-statistics-api-auth.herokuapp.com"),
                    //new Claim(JwtClaimTypes.Issuer, "http://localhost:5011"),
                    new Claim(JwtClaimTypes.AuthenticationMethod, "pwd"),
                    new Claim(JwtClaimTypes.AuthenticationTime, DateTime.UtcNow.ToEpochTime().ToString()),
                    new Claim(JwtClaimTypes.IssuedAt, DateTime.UtcNow.ToEpochTime().ToString()),
                    //new Claim(JwtClaimTypes.IdentityProvider, "local")
                });

            return token;
        }
    }
}