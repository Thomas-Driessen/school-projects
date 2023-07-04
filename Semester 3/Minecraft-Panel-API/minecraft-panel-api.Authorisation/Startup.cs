using System;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using minecraft_panel_api.Authorisation.DAL.Classes;
using minecraft_panel_api.Authorisation.DAL.Context;
using minecraft_panel_api.Authorisation.DAL.Interfaces;

namespace minecraft_panel_api.Authorisation
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

            services.AddDbContext<MinecraftPluginContext>(opt => opt.UseMySql(Configuration.GetConnectionString("DBConnection")));

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<MinecraftPluginContext>()
                .AddDefaultTokenProviders();
            
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
            
            var key = new RsaSecurityKey(RSA.Create());
            SigningCredentials signingCredentials = new SigningCredentials(key, SecurityAlgorithms.RsaSha256);
            
            services.AddIdentityServer(options =>
                {
                    options.IssuerUri = Configuration["TokenIssuer"];
                })
                .AddConfigurationStore(options =>
                {
                    options.ConfigureDbContext = opt =>
                        opt.UseMySql(Configuration.GetConnectionString("IdentityServer.Configuration"),
                            sql => sql.MigrationsAssembly("IdentityServer"));
                })
                .AddOperationalStore(options =>
                {
                    options.ConfigureDbContext = opt =>
                        opt.UseMySql(Configuration.GetConnectionString("IdentityServer.Configuration"),
                            sql => sql.MigrationsAssembly("IdentityServer"));
                })
                .AddAspNetIdentity<IdentityUser>()
                //.AddSigningCredential(signingCredentials)
                .AddDeveloperSigningCredential();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("ApiScope", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("scope", "api1");
                });
            });

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
                    options.ApiName = "api1";
                    options.RequireHttpsMetadata = false;
                })
                .AddCookie("MyCookie", options =>
                {
                    options.ExpireTimeSpan = new TimeSpan(0, 1, 0, 0);
                });
            
            services.AddSwaggerGen(swagger =>
            {
                swagger.SwaggerDoc("v1", new OpenApiInfo { Title = "Authorization" });
            });
            services.AddSwaggerGenNewtonsoftSupport();

            services.AddScoped<IUserDbAccess, UserDbAccess>();
            services.AddScoped<ITokenServiceAccess, TokenServiceAccess>();
            services.AddScoped<IIdentityServiceAccess, IdentityServiceAccess>();
            services.AddScoped<IEmailService, EmailService>();
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
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Minecraft Panel API Authorization");
            });
            
            // Identity server
            app.UseIdentityServer();
            
            app.UseAuthorization();
            
            // NOTE: 'UseAuthentication' is not needed, since 'UseIdentityServer' adds the authentication middleware
            app.UseAuthentication();
            
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}