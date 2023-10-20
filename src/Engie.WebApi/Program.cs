using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using Engie.Business.Implementations;
using Engie.Business.mappers;
using Engie.Domain.Interfaces;
using System.Text.Json.Serialization;

namespace Engie.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers().AddJsonOptions(options =>
            {   
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                options.JsonSerializerOptions.IgnoreNullValues = true;
            }); 
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
            .ConfigureContainer<ContainerBuilder>(containerBuilder =>
            {
                var configuration = builder.Configuration;
                
                containerBuilder.RegisterType<ProductionPlanService>().As<IProductionPlanService>().InstancePerLifetimeScope();
                containerBuilder.Register(c =>
                {
                    var context = c.Resolve<IComponentContext>();
                    var config = context.Resolve<MapperConfiguration>();
                    return config.CreateMapper(context.Resolve);
                }).As<IMapper>().InstancePerLifetimeScope();
                containerBuilder.Register(_ => new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile<ProductionPlanMapper>();
                })).AsSelf().SingleInstance();
            });
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}