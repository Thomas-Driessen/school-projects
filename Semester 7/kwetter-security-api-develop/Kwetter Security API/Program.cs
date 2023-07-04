using System.Text;
using Kwetter_Security_API.Core.Interfaces;
using Kwetter_Security_API.Core.Services;
using Kwetter_Security_API.Dal.Context;
using Kwetter_Security_API.Dal.Interfaces;
using Kwetter_Security_API.Dal.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Dependency Injection
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserAccess, UserAccess>();

// Database
var connectionString = builder.Configuration.GetConnectionString("KwetterDB");
builder.Services.AddDbContext<KwetterContext>(options =>
    options.UseNpgsql(connectionString));

// Add Authentication services
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.Authority = builder.Configuration["Keycloack:Authority"];
    options.Audience = builder.Configuration["Keycloack:Audience"];
    options.MetadataAddress = builder.Configuration["Keycloack:MetadataAddress"];
    options.RequireHttpsMetadata = Convert.ToBoolean(builder.Configuration["Keycloack:HttpsMetadata"]);
});      

// CORS
string[] origins = builder.Configuration["AllowedOrigins"].Split(",");
builder.Services.AddCors(policyBuilder =>
    policyBuilder.AddDefaultPolicy(policy =>
        policy.WithOrigins(origins).AllowAnyHeader())
);

var app = builder.Build();

app.UseCors();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();