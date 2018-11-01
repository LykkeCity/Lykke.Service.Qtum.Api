using JetBrains.Annotations;
using Lykke.Logs.Loggers.LykkeSlack;
using Lykke.Sdk;
using Lykke.Sdk.Health;
using Lykke.Sdk.Middleware;
using Lykke.Service.Qtum.Api.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using AsyncFriendlyStackTrace;
using Lykke.Common.Api.Contract.Responses;

namespace Lykke.Service.Qtum.Api
{
    [UsedImplicitly]
    public class Startup
    {
        private readonly LykkeSwaggerOptions _swaggerOptions = new LykkeSwaggerOptions
        {
            ApiTitle = "QtumApi API",
            ApiVersion = "v1"
        };

        [UsedImplicitly]
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            return services.BuildServiceProvider<AppSettings>(options =>
            {
                options.SwaggerOptions = _swaggerOptions;

                options.Logs = logs =>
                {
                    logs.AzureTableName = "QtumApiLog";
                    logs.AzureTableConnectionStringResolver = settings => settings.QtumApiService.Db.LogsConnString;

                    // TODO: You could add extended logging configuration here:
                    /* 
                    logs.Extended = extendedLogs =>
                    {
                        // For example, you could add additional slack channel like this:
                        extendedLogs.AddAdditionalSlackChannel("Qtum.Api", channelOptions =>
                        {
                            channelOptions.MinLogLevel = LogLevel.Information;
                        });
                    };
                    */
                };

                // TODO: Extend the service configuration
                /*
                options.Extend = (sc, settings) =>
                {
                    sc
                        .AddOptions()
                        .AddAuthentication(MyAuthOptions.AuthenticationScheme)
                        .AddScheme<MyAuthOptions, KeyAuthHandler>(MyAuthOptions.AuthenticationScheme, null);
                };
                */

                // TODO: You could add extended Swagger configuration here:
                /*
                options.Swagger = swagger =>
                {
                    swagger.IgnoreObsoleteActions();
                };
                */
            });
        }

        [UsedImplicitly]
        public void Configure(IApplicationBuilder app)
        {
            app.UseLykkeConfiguration(options =>
            {
                options.SwaggerOptions = _swaggerOptions;
                options.DefaultErrorHandler = ex => new ErrorResponse {ErrorMessage = ex.ToAsyncString()};

                // TODO: Configure additional middleware for eg authentication or maintenancemode checks
                /*
                options.WithMiddleware = x =>
                {
                    x.UseMaintenanceMode<AppSettings>(settings => new MaintenanceMode
                    {
                        Enabled = settings.MaintenanceMode?.Enabled ?? false,
                        Reason = settings.MaintenanceMode?.Reason
                    });
                    x.UseAuthentication();
                };
                */
            });
        }
    }
}
