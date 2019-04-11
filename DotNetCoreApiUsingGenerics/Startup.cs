using DotNetCoreApiUsingGenerics.Configurations;
using DotNetCoreApiUsingGenerics.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Azure.Cosmos.Table;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace DotNetCoreApiUsingGenerics
{
    public class Startup
    {
        private IConfiguration Configuration;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var x = Configuration.GetSection("Storage");
            services.Configure<StorageConfiguration>(Configuration.GetSection("Storage"));
            services.AddSingleton<CloudTableClient>(provider =>
            {
                var azureConnectionString = provider.GetService<IOptions<StorageConfiguration>>().Value
                    .AzureStorageConnectionString;
                var storageAccount =  CloudStorageAccount.Parse(azureConnectionString);
                var tableClient = storageAccount.CreateCloudTableClient(new TableClientConfiguration());
                return tableClient;
            });
            services.AddScoped(typeof(ITableRepository<>), typeof(TableRepository<>));
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
