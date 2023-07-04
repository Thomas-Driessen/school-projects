using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using minecraft_panel_api.Authorisation.Controllers;
using minecraft_panel_api.Authorisation.DAL.Interfaces;
using minecraft_panel_api.Authorisation.DAL.Models;
using Moq;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using Xunit;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace minecraft_panel_api.Authorisation.Tests
{
    public class IdentityControllerTest : IClassFixture<WebApplicationFactory<minecraft_panel_api.Authorisation.Startup>>
    {
        private HttpClient Client { get; }

        public IdentityControllerTest(WebApplicationFactory<minecraft_panel_api.Authorisation.Startup> fixture)
        {
            Client = fixture.CreateClient();
        }
        
        [Fact]
        public async Task IdentityLoginShouldReturnNotFound()
        {
            JObject jObject = new JObject();
            jObject.Add("Email", "Test");
            jObject.Add("Password", "Test");
            var content = new StringContent(jObject.ToString(), Encoding.UTF8, "application/json");
            var result = Client.PostAsync("/identity/login", content).Result;

            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
        }
        
        // See if the login method returns a token
        [Theory]
        [MemberData(nameof(TestUserLoginDataDataSet))]
        public async void TestUserLogin(LoginModel loginModel, JwtSecurityToken expectedTokenValues, string expectedName)
        {
            //Arange
            var userManagerMock = GetMockUserManager();
            var identityServerInteractionMock = new Mock<IIdentityServerInteractionService>();
            var eventServiceMock = new Mock<IEventService>();
            UserDbAccessMock userDbAccessMock = new UserDbAccessMock();
            IdentityServiceAccessMock identityServiceAccessMock = new IdentityServiceAccessMock();
            TokenServiceAccessMock tokenServiceAccessMock = new TokenServiceAccessMock();
            IdentityController userController = new IdentityController(userManagerMock.Object, identityServerInteractionMock.Object, eventServiceMock.Object, userDbAccessMock, tokenServiceAccessMock, identityServiceAccessMock);
            
            //Act
            ActionResult<string> result = await userController.Login(loginModel);
            OkObjectResult okObjectResult = (OkObjectResult) result.Result;
            Dictionary<string, string> actualResponseContent = (Dictionary<string, string>) okObjectResult.Value;
            JwtSecurityToken decodedTokenResult = new JwtSecurityTokenHandler().ReadJwtToken(actualResponseContent.First().Value);

            //Assert
            Assert.Equal(expectedTokenValues.Claims.FirstOrDefault(x => x.Value == expectedName).Value, decodedTokenResult.Claims.FirstOrDefault(x => x.Value == expectedName).Value);
        }

        public static IEnumerable<object[]> TestUserLoginDataDataSet()
        {
            List<object[]> result = new List<object[]>();

            string token = "";
            
            string expectedName = "John Doe";
            
            JwtSecurityToken decodedToken = new JwtSecurityTokenHandler().ReadJwtToken(token);

            LoginModel loginModel = new LoginModel()
            {
                Email = "test@test.nl",
                Password = "Test"
            };
            
            result.Add(new object[] { loginModel, decodedToken, expectedName });

            return result;
        }

        public class IdentityServiceAccessMock : IIdentityServiceAccess
        {
            public async Task<SignInResult> SignInUserWithPassword(string username, string password, bool isPersistant,
                bool lockoutOnFailure)
            {
                return SignInResult.Success;
            }

            public Task<IdentityResult> ResetPasswordWithEmail(IdentityUser identityUser, string token, string password)
            {
                throw new NotImplementedException();
            }

            public Task<IdentityResult> ResetPassword(IdentityUser identityUser, string token, string password)
            {
                throw new NotImplementedException();
            }

            public Task<IdentityResult> ResetEmailWithEmail(IdentityUser identityUser, string email, string token)
            {
                throw new NotImplementedException();
            }

            public Task<string> GenerateEmailResetToken(IdentityUser identityUser, string newEmail)
            {
                throw new NotImplementedException();
            }

            public Task<string> GeneratePasswordResetToken(IdentityUser identityUser)
            {
                throw new NotImplementedException();
            }

            public Task<string> GenerateEmailConfirmationToken(IdentityUser identityUser)
            {
                throw new NotImplementedException();
            }

            public Task<IdentityResult> ConfirmEmail(IdentityUser identityUser, string token)
            {
                throw new NotImplementedException();
            }
        }
        
        public class TokenServiceAccessMock : ITokenServiceAccess
        {
            public async Task<string> IssueJwtToken(string id, string username, string role)
            {
                return "";
            }
        }
        
        public class UserDbAccessMock : IUserDbAccess
        {
            public Task<User> GetUserById(User user)
            {
                throw new NotImplementedException();
            }

            public Task<User> GetUserByEmail(string email)
            {
                throw new NotImplementedException();
            }

            public Task<IdentityUser> GetIdentityUser(IdentityUser identityUser)
            {
                throw new NotImplementedException();
            }

            public Task<IdentityUser> GetIdentityUserById(string id)
            {
                throw new NotImplementedException();
            }

            public Task<IdentityUser> GetIdentityUserById(IdentityUser identityUser)
            {
                throw new NotImplementedException();
            }

            public async Task<IdentityUser> GetIdentityUserByName(string username)
            {
                throw new NotImplementedException();
            }

            public async Task<IdentityUser> GetIdentityUserByEmail(string email)
            {
                IdentityUser identityUser = new IdentityUser
                {
                    UserName = "John Doe"
                };
                
                return identityUser;
            }

            public Task<IdentityRole> GetIdentityRoleById(IdentityRole identityRole)
            {
                throw new NotImplementedException();
            }

            public Task<IdentityResult> RegisterIdentityUser(IdentityUser identityUser, string password)
            {
                throw new NotImplementedException();
            }

            public Task<IdentityResult> RegisterUser(IdentityUser identityUser, User user)
            {
                throw new NotImplementedException();
            }

            public async Task<IList<string>> GetUserRoles(IdentityUser identityUser)
            {
                IList<string> roleList = new List<string>();
                roleList.Add("Default");

                return roleList;
            }

            public Task<IdentityResult> SetUserRole(IdentityUser identityUser, IdentityRole identityRole)
            {
                throw new NotImplementedException();
            }

            public Task<IdentityResult> CreateRole(IdentityRole identityRole)
            {
                throw new NotImplementedException();
            }

            public Task<int> UpdateUser(User user)
            {
                throw new NotImplementedException();
            }
        }
        
        private Mock<UserManager<IdentityUser>> GetMockUserManager()
        {
            var userStoreMock = new Mock<IUserStore<IdentityUser>>();
            return new Mock<UserManager<IdentityUser>>(
                userStoreMock.Object, null, null, null, null, null, null, null, null);
        }
        
        private Mock<SignInManager<IdentityUser>> GetMockSignInManager()
        {
            var contextAccessor = new Mock<IHttpContextAccessor>();
            var userPrincipalFactory = new Mock<IUserClaimsPrincipalFactory<IdentityUser>>();
            Mock<SignInManager<IdentityUser>> mockApiSignInManager = new Mock<SignInManager<IdentityUser>>(GetMockUserManager().Object,
                contextAccessor.Object, userPrincipalFactory.Object, null, null, null);

            return mockApiSignInManager;
        }
    }
}