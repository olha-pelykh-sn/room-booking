using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using room_booking_backend.Data;
using room_booking_backend.Exeptions;
using room_booking_backend.Sevices.Interfaces;
using room_booking_backend.Sevices.Implementations;
using room_booking_backend.Repository.Abstraction;
using room_booking_backend.Repository.Implementation;
using room_booking_backend.Mappings;
using FluentValidation;
using FluentValidation.AspNetCore;

namespace room_booking_backend
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.ReferenceHandler =
                        System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
                });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title       = "Room Booking API",
                    Version     = "v1",
                    Description = "REST API for room booking. " +
                                  "Supports room management (CRUD) and booking creation " +
                                  "with automatic cost calculation based on time of day.",
                    Contact = new OpenApiContact
                    {
                        Name = "Room Booking Team"
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);

                options.UseInlineDefinitionsForEnums();
            });

            builder.Services.AddFluentValidationAutoValidation();
            builder.Services.AddValidatorsFromAssemblyContaining<Program>();

            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddProblemDetails(configure =>
            {
                configure.CustomizeProblemDetails = context =>
                {
                    context.ProblemDetails.Extensions.TryAdd("requestId", context.HttpContext.TraceIdentifier);
                };
            });
            builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

            builder.Services.AddAutoMapper(typeof(RoomProfile), typeof(BookingProfile));
            builder.Services.AddScoped<IRoomRepository,    RoomRepository>();
            builder.Services.AddScoped<IRoomService,       RoomService>();
            builder.Services.AddScoped<IBookingRepository, BookingRepository>();
            builder.Services.AddScoped<IBookingService,    BookingService>();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Room Booking API v1");
                    options.RoutePrefix = string.Empty; // Swagger at root: https://localhost:{port}/
                    options.DocumentTitle = "Room Booking API";
                    options.DisplayRequestDuration();
                    options.EnableDeepLinking();
                });
            }

            app.UseExceptionHandler();
            app.UseStatusCodePages();
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            await app.RunAsync();
        }
    }
}
