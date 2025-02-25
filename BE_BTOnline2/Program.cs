using Autofac.Extensions.DependencyInjection;
using Autofac;
using BE_BTOnline2.DB;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace BE_BTOnline2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<AppDbContext>(opt =>
            {
                opt.UseNpgsql(builder.Configuration.GetConnectionString("MyConnection"));
            });
            // Use Autofac as Service Provider
            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

            builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
            {
                // Register AppDbContext with Autofac
                containerBuilder.RegisterType<AppDbContext>()
                                .AsSelf()
                                .WithParameter("options", new DbContextOptionsBuilder<AppDbContext>()
                                    .UseNpgsql(builder.Configuration.GetConnectionString("MyConnection"))
                                    .Options)
                                .InstancePerLifetimeScope();

                // Automatically register all services ending with "Service"
                containerBuilder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                                .Where(t => t.Name.EndsWith("Services"))
                                .AsImplementedInterfaces()
                                .InstancePerLifetimeScope();
            });
            builder.Services.AddControllers()
               .AddNewtonsoftJson(options =>
               {
                   options.SerializerSettings.DateFormatString = "dd-MM-yyyy";
               });
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
