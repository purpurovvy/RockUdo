using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using DatabaseContext;
using AssignKudosContext.UseCases;
using AssignKudosContext.Repositories;
using KudosSystemRepositories.AssignKudos;
using EmailGateway.Configurations;
using AssignKudosContext.Gateways;
using EmailGateway.AssignKudos;
using EkudosAPI.Configurations;
using System.IO;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authentication.AzureAD.UI;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;

namespace EkudosAPI
{
    public class Startup
    {
        private const string KeyVaultAccessApplicationClientIdEnvVar = "KVA_CLIENT_ID";
        private const string KeyVaultAccessApplicationClientSecretEnvVar = "KVA_CLIENT_SECRET";
        private const string KeyVaultAccessIdEnvVar = "KEY_VAULT_IDENTIFIER";
        public Startup(IHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddEnvironmentVariables()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            if (env.IsEnvironment("Development2"))
            {
                builder.AddUserSecrets<Startup>();
            }
            else
            {
                var stageOneConfig = builder.Build();
                var clientId = stageOneConfig.GetValue<string>(KeyVaultAccessApplicationClientIdEnvVar);
                var clientSecret = stageOneConfig.GetValue<string>(KeyVaultAccessApplicationClientSecretEnvVar);
                var keyVaultIdentifier = stageOneConfig.GetValue<string>(KeyVaultAccessIdEnvVar);
                var keyVaultUri = $"https://{keyVaultIdentifier}.vault.azure.net/";
                builder.AddAzureKeyVault(keyVaultUri, clientId, clientSecret);
            }
            Configuration = builder.Build();
        }
        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.AddDbContext<EkudosContext>
                (options => options.UseSqlServer(Configuration["Database:ConnectionString"]));
            services.AddTransient<ISmtpConfiguration>(x => Configuration.GetSection("Smtp").Get<SmtpConfiguration>());
            services.AddTransient<IAvailableEmailList>(
                x => new AvailableEmailList(Configuration
                    .GetSection("Smtp")
                    .Get<SmtpConfiguration>()
                    .DigitalGroup)
            );

            services.AddTransient<IManageKudosService, ManageKudosService>();
            services.AddTransient<IReaderKudosService, ReaderKudosService>();
            services.AddTransient<EmailCheckerService>();
            services.AddTransient<ISaveKudosRepository, ManageKudosRepositories>();
            services.AddTransient<IGetKudosRepository, ManageKudosRepositories>();
            services.AddTransient<ISendNotificationsService, EmailSenderService>();

            services.AddAuthentication(AzureADDefaults.BearerAuthenticationScheme)
                .AddAzureADBearer(options => Configuration.Bind("AzureAd", options));

            services.AddMvc(option => {
                var policy = new AuthorizationPolicyBuilder()
                                    .RequireAuthenticatedUser()
                                    .Build();
                option.Filters.Add(new AuthorizeFilter(policy));
                option.EnableEndpointRouting = false;
            });

            services.AddCors(options =>
            {
                var origins = Configuration.GetSection("Cors:Origins").Get<string[]>();

                options.AddPolicy("AllowSpecificOrigin",
                    builder => builder
                        .WithOrigins(origins)
                        .AllowAnyMethod()
                        //WithHeaders("Authorization")
                       .AllowAnyHeader()
                    );
            });
        }
       
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors("AllowSpecificOrigin");
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseMvc();
            app.UseStaticFiles();
        }
    }
}
