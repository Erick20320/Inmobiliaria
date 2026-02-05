using Inmobiliaria.API.Middleware;
using Inmobiliaria.Application;
using Inmobiliaria.Application.Abstractions.Security;
using Inmobiliaria.Application.Abstractions.Services;
using Inmobiliaria.Persistence;
using Inmobiliaria.Persistence.Seed;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Application layers
builder.Services.AddServiceApplication(); // Mediator y handlers
builder.Services.AddPersistenceServices(builder.Configuration); // Repositorios + DbContext de app

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = IdentityConstants.BearerScheme;
    options.DefaultChallengeScheme = IdentityConstants.BearerScheme;
})
.AddJwtBearer(IdentityConstants.BearerScheme, options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
        ValidAudience = builder.Configuration["JwtSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey
        (
            System.Text.Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Secret"]!)
        )
    };
});

// Configuración Swagger / OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API Inmobiliaria",
        Version = "v1",
        Description = "API para Inmobiliaria"
    });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Autorizacion",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Ingrese el token JWT con el prefijo 'Bearer'"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

var app = builder.Build();

#region Seed

//Descomentar para habilitar el seed y agregar los using necesarios

using (var scope = app.Services.CreateScope())
{
    var connectionFactory = scope.ServiceProvider.GetRequiredService<IDbConnectionFactory>();
    var passwordHasher = scope.ServiceProvider.GetRequiredService<IPasswordHasher>();

    await DbInitializer.SeedAsync(connectionFactory, passwordHasher);
}

#endregion

// Configure the HTTP request pipeline.

app.UseExceptionHandler();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI();

// Redirigir "/" a Swagger UI
app.MapGet("/", contexto =>
{
    contexto.Response.Redirect("/swagger/index.html", permanent: false);
    return Task.CompletedTask;
});

app.MapControllers();

app.Run();
