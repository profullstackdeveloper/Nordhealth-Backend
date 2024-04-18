using Application.Interfaces;
using Application.Services;
using Domain.Interfaces;
using Infrastructure.Repositories;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;


namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Configure services
            ConfigureServices(builder.Services, builder.Configuration);

            var app = builder.Build();
         

            // Configure the HTTP request pipeline
            Configure(app, app.Environment);
            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.Run();
        }

        private static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            // Add controllers
            services.AddControllers();

            // Register application services
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IContactService, ContactService>();
            services.AddScoped<ITaskService, TaskService>();

            // Register repositories
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IContactRepository, ContactRepository>();
            services.AddScoped<ITaskRepository, TaskRepository>();

            // Retrieve the connection string from appsettings.json using configuration
            string connectionString = configuration.GetConnectionString("DefaultConnection");

            // Add DbContext with the retrieved connection string
            services.AddDbContext<TaskDbContext>(options =>
                options.UseSqlServer(connectionString)
            );

            // Add Swagger for API documentation
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Task Project API", Version = "v1" });
            });
        }
        private static void Configure(WebApplication app, IWebHostEnvironment env)
        {
            // Enable Swagger in development environment
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Task Project API v1");
                });
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();

            // Map controllers
            app.MapControllers();
        }
    }
}
