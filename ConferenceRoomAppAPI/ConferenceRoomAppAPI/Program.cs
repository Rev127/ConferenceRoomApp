using ConferenceRoomAppAPI.Data.Context;
using ConferenceRoomAppAPI.Services.Interfaces;
using ConferenceRoomAppAPI.Services.Services;
using ConferenceRoomAppAPI.Services;
using Microsoft.EntityFrameworkCore;
using ConferenceRoomAppAPI.Data.Models;
using ConferenceRoomAppAPI.Services.Tools;
using ConferenceRoomAppAPI.Middlewares;

namespace ConferenceRoomAppAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();
            builder.Services.AddIdentityApiEndpoints<Users>()
                .AddEntityFrameworkStores<ConferenceRoomContext>();

            // Configure database connection
            var connectionString = builder.Configuration.GetConnectionString("DBConection");

            builder.Services.AddDbContext<ConferenceRoomContext>(options =>
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

            // Register services
            builder.Services.AddScoped<ICurrentUserServices, CurrentUserServices>();
            builder.Services.AddScoped<IHallsServices, HallsService>();
            builder.Services.AddScoped<IOrdersServices, OrderService>();
            builder.Services.AddScoped<IHallServices, HallsServices>();
            builder.Services.AddScoped<IUserServices, UserService>();
            builder.Services.AddScoped<IOrderReports, OrderService>();

            // Register discount rules and price calculator
            builder.Services.AddTransient<IPriceDiscountRules, MorningDiscountRule>();
            builder.Services.AddTransient<IPriceDiscountRules, EveningDiscountRule>();
            builder.Services.AddTransient<IPriceDiscountRules, PeakTimeDiscountRule>();
            builder.Services.AddTransient<IPriceDiscountRules, StandartDiscountRule>();
            builder.Services.AddTransient<IPriceCalculator, PriceCalculator>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.MapIdentityApi<Users>();

                app.UseSwaggerUI();
            }

            app.UseMiddleware<ExeptionHandingMiddlewsres>();

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
