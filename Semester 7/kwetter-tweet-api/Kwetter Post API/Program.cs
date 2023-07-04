using Google.Cloud.Storage.V1;
using Kwetter_Post_API.Core.Interfaces;
using Kwetter_Post_API.Core.Services;
using Kwetter_Post_API.DAL.Context;
using Kwetter_Post_API.DAL.Interfaces;
using Kwetter_Post_API.DAL.Services;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Kwetter_Post_API.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Database
var connectionString = builder.Configuration.GetConnectionString("KwetterDB");
builder.Services.AddDbContext<KwetterContext>(options =>
    options.UseNpgsql(connectionString));

// Dependency injection
// ######################### //
// Service layer
builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<IGoogleCloudStorageService, GoogleCloudStorageService>();

// Data layer
builder.Services.AddScoped<IPostAccess, PostAccess>();
// ######################### //


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

// Event bus
builder.Services.AddMassTransit(config =>
{
    config.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host(builder.Configuration["EventBusSettings:Host"], "/", h => {
            h.Username(builder.Configuration["EventBusSettings:Username"]);
            h.Password(builder.Configuration["EventBusSettings:Password"]);
        });
    });
});

// CORS
//string[] origins = builder.Configuration["AllowedOrigins"].Split(",");
builder.Services.AddCors(policyBuilder =>
    policyBuilder.AddDefaultPolicy(policy =>
        policy.AllowAnyOrigin().AllowAnyHeader())
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

//app.MapCommentEndpoints();

app.Run();