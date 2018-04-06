using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using WSMessageHandler.WebSocketHook;
using WSMessageHandlerCore.Interfaces;
using WSMessageHandlerCore.Repositories;

namespace WSMessageHandler
{
    public static class ExtensionsMethods
    {
        public static IApplicationBuilder MapWebSocketManager(this IApplicationBuilder app,
                                                             PathString path,
                                                             WebSocketHandler handler)
        {
            return app.Map(path, (_app) => _app.UseMiddleware<WebSocketManagerMiddleware>(handler));
        }

        public static IServiceCollection AddWebSocketManager(this IServiceCollection services)
        {
            services.AddTransient<WebSocketConnectionManager>();

            foreach (var type in Assembly.GetEntryAssembly().ExportedTypes)
            {
                if (type.GetTypeInfo().BaseType == typeof(WebSocketHandler))
                {
                    services.AddSingleton(type);
                }
            }

            return services;
        }

        public static IServiceCollection AddDepencendyInjection(this IServiceCollection services)
        {
            services.AddTransient<IBrokerRepository, ActiveMQRepository>();
            return services;
        }
    }
}
