using GraphQL.Server;
using GraphQL.Server.Ui.Playground;
//using GraphQL.Server.Ui.Voyager;
//using HotChocolate.AspNetCore.Serialization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Shopping.Aggregator.GraphQL.Queries;
using Shopping.Aggregator.GraphQL.Schemas;
using Shopping.Aggregator.Repositories;
using Shopping.Aggregator.Services;
using System;

namespace Shopping.Aggregator
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
            services.AddHttpClient<ICatalogService, CatalogService>(c => c.BaseAddress = new Uri(Configuration.GetValue<string>("ApiSettings:CatalogUrl")));
            services.AddHttpClient<IBasketService, BasketService>(c => c.BaseAddress = new Uri(Configuration.GetValue<string>("ApiSettings:BasketUrl")));
            services.AddHttpClient<IOrderService, OrderService>(c => c.BaseAddress = new Uri(Configuration.GetValue<string>("ApiSettings:OrderingUrl")));

            services.AddScoped<IShoppingRepository, ShoppingRepository>();
            services.AddScoped<ShoppingQuery>();
            services.AddScoped<ShoppingSchema>();

            services.AddGraphQL()
                    .AddSystemTextJson()
                    .AddGraphTypes(typeof(ShoppingSchema), ServiceLifetime.Scoped);

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Shopping.Aggregator", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Shopping.Aggregator v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseGraphQL<ShoppingSchema>();
            app.UseGraphQLPlayground(options: new GraphQLPlaygroundOptions());

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });            

            //app.UseGraphQLVoyager(new VoyagerOptions()
            //{
            //    GraphQLEndPoint = "/graphql"
            //}, "/graphql-ui");
        }
    }
}
