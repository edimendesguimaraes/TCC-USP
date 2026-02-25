using Microsoft.EntityFrameworkCore;
using Zeladoria.Domain.Interfaces;
using Zeladoria.Infrastructure.Data;
using Zeladoria.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// 1. Configura os Controllers e o Swagger (A interface de testes)
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 2. Configura o Banco de Dados (PostgreSQL)
builder.Services.AddDbContext<ZeladoriaDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// 3. Injeção de Dependência (Ensina o .NET a usar o seu repositório)
builder.Services.AddScoped<IOcorrenciaRepository, OcorrenciaRepository>();

var app = builder.Build();

// 4. Habilita o Swagger para visualizarmos a API
app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();
app.MapControllers();

// Endpoint de saúde da API (Aquele que configuramos para o GitHub Actions)
app.MapGet("/", () => "API de Zeladoria ativa e operante com Clean Architecture!");

app.Run();