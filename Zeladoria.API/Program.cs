var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Sucesso! Clean Architecture (Zeladoria.API) compilada e rodando no Docker via GitHub Actions!");

app.Run();