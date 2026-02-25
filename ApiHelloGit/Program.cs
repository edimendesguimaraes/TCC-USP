var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// A nossa rota raiz que vai provar que o GitHub Actions trabalhou por nós
app.MapGet("/", () => "Deploy automatizado via GitHub Actions! TCC USP - Município de Indaiatuba rodando 100% na nuvem.");

// Mantemos o weatherforecast só de enfeite por enquanto
app.MapGet("/weatherforecast", () => "O clima em Indaiatuba hoje é de muito código e automação!");

app.Run();