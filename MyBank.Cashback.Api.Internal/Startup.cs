using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using MyBank.Cashback.Infra.Ioc;
using MyBank.Infra.Generics.Providers;
using Serilog;
using Swashbuckle.AspNetCore.Swagger;

namespace MyBank.Cashback.Api.Internal
{
    public class Startup
    {
        private string ProdParam { get; }

        public Startup(IConfiguration configuration)
        {
            LoggerConfigurationProvider.Provides();
            Configuration = configuration;
            ProdParam = Configuration.GetValue<string>("prod");
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var configuration = Configuration;

            Console.WriteLine($"ProdParam: {ProdParam}");

            if (string.IsNullOrEmpty(ProdParam))
            {
                string projectPath = AppDomain.CurrentDomain.BaseDirectory.Split(new String[] { @"bin\" }, StringSplitOptions.None)[0];
                System.Console.WriteLine($"projectPath in Api ConfigureServices: {projectPath}");
                configuration = new ConfigurationBuilder()
                    .SetBasePath(projectPath)
                    .AddJsonFile("appsettings.json")
                    .Build();
            }

            var connectionString = configuration?.GetConnectionString("MyBank_Cashback") ?? "";
            System.Console.WriteLine($"connectionString in Api ConfigureServices: {connectionString}");

            services.AddSingleton<IConfiguration>(configuration);

            services.Configure<KestrelServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });

            services.Configure<IISServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });

            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MyBank API", Version = "v1" });
            });

            services.AddMyBankInfrastructure(configuration);
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

            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "MyBank API V1");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseMyBankConfiguration();
        }
    }
}
