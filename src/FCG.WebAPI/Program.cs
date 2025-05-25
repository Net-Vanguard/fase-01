using FCG.Application.Interfaces;
using FCG.Application.Mappings;
using FCG.Application.Services;
using FCG.Domain.Interfaces;
using FCG.Infrastructure.Persistence;
using FCG.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using FCG.WebAPI.Middleware;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;


// Adiciona Application Services e Repositories
builder.Services.AddScoped<IUserAppService, UserAppService>();
builder.Services.AddScoped<IGameAppService, GameAppService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IGameRepository, GameRepository>();

// Adiciona AutoMapper
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<DomainToDtoMappingProfile>();
});

// Adiciona Controllers
builder.Services.AddControllers();

// Configuração do Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// lê a connection string
var conn = builder.Configuration.GetConnectionString("DefaultConnection");

// registra o DbContext com MySQL (Pomelo)
//builder.Services.AddDbContext<FcgDbContext>(options =>
//    options.UseMySql(conn, ServerVersion.AutoDetect(conn))
//);
builder.Services.AddDbContext<FcgDbContext>(options => options.UseMySQL(conn));

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
    });
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();