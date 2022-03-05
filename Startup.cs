    using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using TestTask.Models;
using Amazon.Runtime;
using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using TestTask.Client;
using TestTask.DataBase;

namespace TestTask
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            var credentials = new BasicAWSCredentials(config.awskeyid, config.awskey);
            var configs = new AmazonDynamoDBConfig()
            {
                RegionEndpoint = RegionEndpoint.USEast2
            };
            var client = new AmazonDynamoDBClient(credentials, configs);
           // services.AddScoped<IDynamoDbContext<DBList>>(provider => new DynamoDbContext<DBList>(client));

            services.AddSingleton<IAmazonDynamoDB>(client);
           
            services.AddSingleton<IDynamoDBContext, DynamoDBContext>();
            services.AddSingleton<IDynamoDBClient, DynamoClient>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
