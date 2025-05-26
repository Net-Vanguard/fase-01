using FCG.Application.Interfaces;
using FCG.Application.Mappings;
using FCG.Application.Services;
using FCG.Domain.Interfaces;
using FCG.Infrastructure.Persistence;
using FCG.Infrastructure.Repositories;
using FCG.WebAPI.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Adiciona Application Services e Repositories
builder.Services.AddScoped<IUserAppService, UserAppService>();
builder.Services.AddScoped<IGameAppService, GameAppService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IGameAppService, GameAppService>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IGameRepository, GameRepository>();
builder.Services.AddScoped<IUserGameRepository, UserGameRepository>();

// Adiciona AutoMapper
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<DomainToDtoMappingProfile>();
});

// Adiciona Controllers
builder.Services.AddControllers().AddJsonOptions(opts =>
{
    // Converte TODOS os enums para strings no JSON
    opts.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

// Configuração do Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    // Define o esquema de segurança JWT
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Insira o token JWT assim: 'Bearer {seu_token}'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT"
    });

    // Exige o Bearer em todas as operações
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
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

// lê a connection string
var conn = builder.Configuration.GetConnectionString("DefaultConnection");

// registra o DbContext com MySQL 
builder.Services.AddDbContext<FcgDbContext>(options => options.UseMySQL(conn));

// 1) Defina o JWT Bearer como o esquema padrão de autenticação
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"])),
            ValidateIssuer = true,
            ValidIssuer = configuration["Jwt:Issuer"],
            ValidateAudience = true,
            ValidAudience = configuration["Jwt:Audience"],
            ValidateLifetime = true
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Administrator"));
});

var app = builder.Build();

// Middleware
app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseAuthentication();
app.UseAuthorization();

// Habilita Swagger em Development e Production (opcional)
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "FCG API V1");
        c.EnablePersistAuthorization();
    });
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();