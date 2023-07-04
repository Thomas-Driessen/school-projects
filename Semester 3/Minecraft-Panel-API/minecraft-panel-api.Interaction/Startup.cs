using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Logging;
using Microsoft.OpenApi.Models;
using minecraft_panel_api.Interaction.Classes;
using minecraft_panel_api.Interaction.Controllers;
using minecraft_panel_api.Interaction.DAL.Classes;
using minecraft_panel_api.Interaction.DAL.Context;
using minecraft_panel_api.Interaction.DAL.Interfaces;
using minecraft_panel_api.Interaction.Hubs;
using RestSharp;

namespace minecraft_panel_api.Interaction
{
    public class Startup
    {
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSignalR();
            
            services.AddDbContext<MinecraftPluginContext>(opt => opt.UseMySql(Configuration.GetConnectionString("DBConnection")));
            
            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                    builder =>
                    {
                        builder.WithOrigins(Configuration["PluginBaseUrl"], "", "")                       
                            .AllowCredentials()
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .SetIsOriginAllowed((host) => true);
                    });
            });

            IdentityModelEventSource.ShowPII = true; //Add this line

            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    //options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                })
                .AddIdentityServerAuthentication("Bearer", options =>
                {
                    options.Authority = Configuration["AuthenticationUrl"];
                    options.ApiSecret = "api1";
                    options.ApiName = "interaction";
                    options.RequireHttpsMetadata = false;
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("ApiScope", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("scope", "interaction");
                });
            });
            
            /*
            services.TryAddEnumerable(
                ServiceDescriptor.Singleton<IPostConfigureOptions<JwtBearerOptions>, 
                    ConfigureJwtBearerOptions>());
            */
            services.AddSwaggerGen(swagger =>
            {
                swagger.SwaggerDoc("v1", new OpenApiInfo { Title = "Interaction API" });
            });
            services.AddSwaggerGenNewtonsoftSupport();

            services.AddScoped<IPlayerDbAccess, PlayerDbAccess>();
            services.AddScoped<IChatDbAccess, ChatDbAccess>();
            services.AddScoped<IRestClient, RestClient>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            app.UseRouting();
            
            app.UseCors(MyAllowSpecificOrigins);
            
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Minecraft Panel API Interaction");
            });

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<SocketTestHub>("/chathub");
            });
        }
    }
}