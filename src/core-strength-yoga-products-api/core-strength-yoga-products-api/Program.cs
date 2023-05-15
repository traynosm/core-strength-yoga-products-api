
using core_strength_yoga_products_api.Data;
using core_strength_yoga_products_api.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace core_strength_yoga_products_api
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

            var connectionString = builder.Configuration
                .GetConnectionString("CoreStrengthYogaProductsApi") ??
                throw new InvalidOperationException("Connection string 'CoreStrengthYogaProductsApiConnection' not found.");

            builder.Services.AddDbContext<CoreStrengthYogaProductsApiDbContext>(options =>
            options
                .UseLazyLoadingProxies()
                .UseSqlite(connectionString));

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                SeedData.Initialize(services);
                SeeDataDepartments.Initialize(services);
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                var context = services.GetRequiredService<CoreStrengthYogaProductsApiDbContext>();
                context.Database.EnsureCreated();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}