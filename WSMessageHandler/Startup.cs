using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WSMessageHandlerCore.Entities;
using System;
using WSMessageHandler.WebSocketHook;
using Swashbuckle.AspNetCore.Swagger;

namespace WSMessageHandler
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            //Adding MVC
            services.AddMvc();

            //Adding WebSocket Manager
            services.AddWebSocketManager();

            //Adding DI
            services.AddDepencendyInjection();

            //Adding configuration
            services.Configure<AppConfiguration>(options => Configuration.GetSection("AppConfiguration").Bind(options));

            //Adding swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "WebSocket Message Handler", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            app.UseWebSockets();

            app.MapWebSocketManager("/v1/ws", serviceProvider.GetService<WebSocketMessageHandler>());

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebSocket Message Handler");
            });
        }
    }
}
