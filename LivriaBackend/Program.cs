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

using LivriaBackend.IAM.Domain.Repositories; 

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
using LivriaBackend.Shared.Domain.Exceptions; 


using LivriaBackend.IAM.Domain.Model.Commands;
using LivriaBackend.IAM.Interfaces.REST.Controllers;
using LivriaBackend.IAM.Infrastructure.Persistence.Repositories;
using LivriaBackend.IAM.Application.Internal.OutboundServices;
using LivriaBackend.IAM.Infrastructure.Tokens.JWT.Configuration;
using LivriaBackend.IAM.Infrastructure.Tokens.JWT.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using LivriaBackend.IAM.Application.Internal.CommandServices;
using LivriaBackend.IAM.Domain.Model.Aggregates;

var builder = WebApplication.CreateBuilder(args);

/* JSON WEB TOKEN START */
builder.Services.Configure<TokenSettings>(builder.Configuration.GetSection("TokenSettings"));

builder.Services.AddScoped<ITokenService, TokenService>();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration.GetSection("TokenSettings:Secret").Value)),
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
    });
/* JSON WEB TOKEN END */

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

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(RegisterCommand).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(RegisterUserClientCompositeCommandHandler).Assembly);
    
    cfg.RegisterServicesFromAssembly(typeof(LoginAdminCommand).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(LoginAdminCommandHandler).Assembly);
});

// Add Authorization
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminAccess", policy =>
    {
        policy.RequireRole("Admin");
    });
});

// Add CORS Policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllPolicy",
        policy => policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

if (string.IsNullOrEmpty(connectionString))
    throw new InvalidOperationException("Database connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySQL(connectionString));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


// Repositorios
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
builder.Services.AddScoped<IIdentityRepository, IdentityRepository>();


// Servicios de Comandos
builder.Services.AddScoped<IBookCommandService, BookCommandService>();
builder.Services.AddScoped<IUserClientCommandService, UserClientCommandService>();
builder.Services.AddScoped<IUserAdminCommandService, UserAdminCommandService>();
builder.Services.AddScoped<ICommunityCommandService, CommunityCommandService>();
builder.Services.AddScoped<IPostCommandService, PostCommandService>();
builder.Services.AddScoped<IUserCommunityCommandService, UserCommunityCommandService>(); 
builder.Services.AddScoped<INotificationCommandService, NotificationCommandService>();
builder.Services.AddScoped<IReviewCommandService, ReviewCommandService>();
builder.Services.AddScoped<ICartItemCommandService, CartItemCommandService>();
builder.Services.AddScoped<IOrderCommandService, OrderCommandService>();


// Servicios de Consultas
builder.Services.AddScoped<IBookQueryService, BookQueryService>();
builder.Services.AddScoped<IUserClientQueryService, UserClientQueryService>();
builder.Services.AddScoped<IUserAdminQueryService, UserAdminQueryService>();
builder.Services.AddScoped<ICommunityQueryService, CommunityQueryService>();
builder.Services.AddScoped<IPostQueryService, PostQueryService>();
builder.Services.AddScoped<INotificationQueryService, NotificationQueryService>();
builder.Services.AddScoped<IReviewQueryService, ReviewQueryService>();
builder.Services.AddScoped<ICartItemQueryService, CartItemQueryService>();
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
    
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            },
            Array.Empty<string>()
        }
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
        
        var identityRepository = services.GetRequiredService<IIdentityRepository>();
        var unitOfWork = services.GetRequiredService<IUnitOfWork>();
        var logger = services.GetRequiredService<ILogger<Program>>();
        
        const string defaultAdminUsername = "admin_default";
        const string defaultAdminPassword = "0000";
        const string defaultAdminEmail = "admin@livria.com";
        const string defaultAdminDisplayName = "Super Administrador";
        const string defaultAdminSecurityPin = "0000";
        
        var existingIdentity = await identityRepository.GetByUsernameAsync(defaultAdminUsername);

        if (existingIdentity == null)
        {
            Console.WriteLine($"Creando UserAdmin por defecto y su Identity asociada para el usuario '{defaultAdminUsername}'...");
            
            var userAdmin = new LivriaBackend.users.Domain.Model.Aggregates.UserAdmin(
                defaultAdminDisplayName,
                defaultAdminUsername,
                defaultAdminEmail,
                true, 
                defaultAdminSecurityPin
            );
            
            var userAdminIdProperty = userAdmin.GetType().GetProperty("Id");
            if (userAdminIdProperty != null && userAdminIdProperty.CanWrite)
            {
                userAdminIdProperty.SetValue(userAdmin, 0); 
            }
            else
            {
                logger.LogError("No se pudo establecer el ID 0 para el UserAdmin por defecto usando reflection. Verifica el setter de User.Id.");
            }
            
            context.UserAdmins.Add(userAdmin);
            await context.SaveChangesAsync(); 

            
            
            var userAdminIdForIdentity = userAdmin.Id; 
            
            var identity = new Identity(
                0, 
                defaultAdminUsername,
                defaultAdminPassword
            );
            
            var identityUserIdProperty = identity.GetType().GetProperty("UserId");
            if (identityUserIdProperty != null && identityUserIdProperty.CanWrite)
            {
                identityUserIdProperty.SetValue(identity, userAdminIdForIdentity);
            }
            else
            {
                logger.LogError("No se pudo establecer UserId para la Identity por defecto usando reflection. Verifica el setter de Identity.UserId o su constructor.");
            }
            
            await identityRepository.AddAsync(identity);
            await unitOfWork.CompleteAsync(); 

            Console.WriteLine($"UserAdmin '{defaultAdminUsername}' (ID: {userAdmin.Id}) y su Identity asociada (ID: {identity.Id}) creados con éxito.");
        }
        else
        {
            Console.WriteLine($"La Identity del administrador por defecto para '{defaultAdminUsername}' ya existe en la base de datos.");
        }
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Ocurrió un error al inicializar el UserAdmin por defecto y su Identity en la base de datos.");
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
                Detail = genreEx.Message.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)[0],
                TraceId = context.TraceIdentifier
            };

            await context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));
        }
        
        else if (exception is DuplicateEntityException dupEx)
        {
            context.Response.StatusCode = StatusCodes.Status409Conflict; 
            context.Response.ContentType = "application/json";

            var errorResponse = new ErrorResponse
            {
                Status = StatusCodes.Status409Conflict,
                Title = "Conflict",
                Detail = dupEx.Message,
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

if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        c.RoutePrefix = "swagger";
    });
}

app.UseHttpsRedirection();

// Apply CORS Policy
app.UseCors("AllowAllPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();