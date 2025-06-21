using LivriaBackend.commerce.Application.Internal.CommandServices;
using LivriaBackend.commerce.Application.Internal.QueryServices;
using LivriaBackend.commerce.Domain.Repositories;
using LivriaBackend.commerce.Domain.Model.Services;
using LivriaBackend.commerce.Infrastructure.Repositories;
using LivriaBackend.Shared.Domain.Repositories;
using LivriaBackend.Shared.Infrastructure.Persistence.EFC.Configuration;
using LivriaBackend.users.Interfaces.REST.Transform;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using AutoMapper;

using LivriaBackend.users.Application.Internal.CommandServices;
using LivriaBackend.users.Application.Internal.QueryServices;
using LivriaBackend.users.Domain.Model.Repositories;
using LivriaBackend.users.Domain.Model.Services;
using LivriaBackend.users.Infrastructure.Repositories;
using LivriaBackend.users.Domain.Model.Aggregates;

using LivriaBackend.users.Interfaces.ACL;
using LivriaBackend.users.Application.ACL;

using LivriaBackend.communities.Domain.Repositories; 
using LivriaBackend.communities.Domain.Model.Services;
using LivriaBackend.communities.Infrastructure.Repositories;
using LivriaBackend.communities.Application.Internal.CommandServices; 
using LivriaBackend.communities.Application.Internal.QueryServices;
using LivriaBackend.communities.Interfaces.REST.Transform;

using LivriaBackend.notifications.Domain.Model.Repositories;
using LivriaBackend.notifications.Infrastructure.Repositories;
using LivriaBackend.notifications.Domain.Model.Services;
using LivriaBackend.notifications.Application.Internal.CommandServices;
using LivriaBackend.notifications.Application.Internal.QueryServices;
using LivriaBackend.notifications.Interfaces.REST.Transform;

using LivriaBackend.commerce.Interfaces.REST.Transform;

using System.Globalization;

using System.Reflection;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

using Microsoft.AspNetCore.Diagnostics; 
using System.Text.Json; 
using LivriaBackend.Shared.ErrorHandling; 
using Microsoft.AspNetCore.Http; 

var builder = WebApplication.CreateBuilder(args);

/* Localization Start */
builder.Services.AddLocalization();
var localizationOptions = new RequestLocalizationOptions();
var supportedCultures = new[]
{
    new CultureInfo("en-US"),
    new CultureInfo("es-ES")
};
localizationOptions.SupportedCultures = supportedCultures;
localizationOptions.SupportedUICultures = supportedCultures;
localizationOptions.SetDefaultCulture("en-US");
localizationOptions.ApplyCurrentCultureToResponseHeaders = true;
/* Localization End */

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

if (string.IsNullOrEmpty(connectionString))
    throw new InvalidOperationException("Database connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySQL(connectionString));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();



builder.Services.AddScoped<IUserClientRepository, UserClientRepository>();
builder.Services.AddScoped<IUserAdminRepository, UserAdminRepository>();

builder.Services.AddScoped<ICommunityRepository, CommunityRepository>();
builder.Services.AddScoped<IPostRepository, PostRepository>();
builder.Services.AddScoped<IUserCommunityRepository, UserCommunityRepository>();

builder.Services.AddScoped<INotificationRepository, NotificationRepository>();

builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
builder.Services.AddScoped<ICartItemRepository, CartItemRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>(); 
builder.Services.AddScoped<IOrderItemRepository, OrderItemRepository>(); 


builder.Services.AddScoped<IBookCommandService, BookCommandService>();
builder.Services.AddScoped<IBookQueryService, BookQueryService>();


builder.Services.AddScoped<IUserClientCommandService, UserClientCommandService>();
builder.Services.AddScoped<IUserClientQueryService, UserClientQueryService>();

builder.Services.AddScoped<IUserAdminCommandService, UserAdminCommandService>();
builder.Services.AddScoped<IUserAdminQueryService, UserAdminQueryService>();


builder.Services.AddScoped<ICommunityCommandService, CommunityCommandService>();
builder.Services.AddScoped<ICommunityQueryService, CommunityQueryService>();
builder.Services.AddScoped<IPostCommandService, PostCommandService>();
builder.Services.AddScoped<IPostQueryService, PostQueryService>();
builder.Services.AddScoped<IUserCommunityCommandService, UserCommunityCommandService>();


builder.Services.AddScoped<INotificationCommandService, NotificationCommandService>();
builder.Services.AddScoped<INotificationQueryService, NotificationQueryService>();


builder.Services.AddScoped<IReviewCommandService, ReviewCommandService>();
builder.Services.AddScoped<IReviewQueryService, ReviewQueryService>();

builder.Services.AddScoped<ICartItemCommandService, CartItemCommandService>();
builder.Services.AddScoped<ICartItemQueryService, CartItemQueryService>();

builder.Services.AddScoped<IOrderCommandService, OrderCommandService>();
builder.Services.AddScoped<IOrderQueryService, OrderQueryService>();

builder.Services.AddScoped<IRecommendationQueryService, RecommendationQueryService>();


builder.Services.AddAutoMapper(
    typeof(UsersMappingProfile).Assembly,
    typeof(CommunitiesMappingProfile).Assembly,
    typeof(MappingNotification).Assembly,
    typeof(MappingCommerce).Assembly
);



builder.Services.AddScoped<IUserClientContextFacade, UserClientContextFacade>();

builder.Services.AddControllers()
    .AddDataAnnotationsLocalization(options =>
    {
        options.DataAnnotationLocalizerProvider = (type, factory) =>
            factory.Create(typeof(LivriaBackend.Shared.Resources.SharedResource));
    });


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.EnableAnnotations();
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Livria API",
        Version = "v1",
        Description = "API for book management in Livria"
    });
});

var app = builder.Build();

app.UseRequestLocalization(localizationOptions);

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<AppDbContext>();
        context.Database.EnsureCreated();

        var defaultUserAdmin = await context.UserAdmins.FirstOrDefaultAsync(ua => ua.Id == 0);

        if (defaultUserAdmin == null)
        {
            var userAdmin = new LivriaBackend.users.Domain.Model.Aggregates.UserAdmin(
                "Super Administrador",
                "admin_default",
                "admin@livria.com",
                "hashed_password_admin_123",
                true,
                "0000"
            );

            var idProperty = userAdmin.GetType().GetProperty("Id");
            if (idProperty != null && idProperty.CanWrite)
            {
                idProperty.SetValue(userAdmin, 0);
            }
            else
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError("Could not set ID 0 for default UserAdmin using reflection. Check User.Id setter access.");
            }

            context.UserAdmins.Add(userAdmin);
            await context.SaveChangesAsync();
            Console.WriteLine("UserAdmin por defecto creado con éxito (ID 1).");
        }
        else
        {
            Console.WriteLine("UserAdmin por defecto (ID 1) ya existe en la base de datos.");
        }
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Ocurrió un error al inicializar el UserAdmin por defecto o la base de datos.");
    }
}


app.UseExceptionHandler(appBuilder =>
{
    appBuilder.Run(async context =>
    {
        var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
        var exception = exceptionHandlerPathFeature?.Error;

        if (exception is ArgumentException argEx && argEx.ParamName == "language")
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest; 
            context.Response.ContentType = "application/json";

            var errorResponse = new ErrorResponse
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Validation Error",
                Detail = argEx.Message, 
                TraceId = context.TraceIdentifier
            };

            await context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));
        }
        else if (exception is ArgumentException genreEx && genreEx.ParamName == "genre")
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest; // 400 Bad Request
            context.Response.ContentType = "application/json";

            var errorResponse = new ErrorResponse
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Validation Error",
                // The message from the Book class already lists the allowed genres
                Detail = genreEx.Message.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)[0],
                TraceId = context.TraceIdentifier
            };

            await context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));
        }
        else
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError; 
            context.Response.ContentType = "application/json";

            var errorResponse = new ErrorResponse
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "An error occurred",
                Detail = "An unexpected error occurred. Please try again later.",
                TraceId = context.TraceIdentifier
            };
            
            var logger = appBuilder.ApplicationServices.GetRequiredService<ILogger<Program>>();
            logger.LogError(exception, "An unhandled exception occurred.");

            await context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));
        }
    });
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();

app.Run();