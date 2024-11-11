using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace cluster.Api
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

            var app = builder.Build();

            app.UseSwagger();

            app.MapGet("/", () => "Hello!");
            app.MapGet("/(id)", (string id) => "El id es: ");

            app.UseSwaggerUI();

     

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
