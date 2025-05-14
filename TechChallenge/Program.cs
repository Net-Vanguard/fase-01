using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TechChallenge.Infra.Data;
using TechChallenge.Application.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using FluentValidation;
using TechChallenge.Validadores;
using TechChallenge.Domain.Entities.Usuario;
using TechChallenge.Infra.Data.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Injeção de dependência do DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Injeção do seu service
builder.Services.AddScoped<UsuarioService>();

// Injeção do seu repositório
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepositorio>();

// Registro do FluentValidation
builder.Services.AddValidatorsFromAssemblyContaining<UsuarioValidador>();

// Configuração do JWT
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings["Issuer"],
            ValidAudience = jwtSettings["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]))
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();

// Configure o pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// >>> Estas duas são essenciais para autenticação
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
