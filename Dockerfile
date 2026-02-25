FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY ["ApiHelloGit/ApiHelloGit.csproj", "ApiHelloGit/"]
RUN dotnet restore "ApiHelloGit/ApiHelloGit.csproj"
COPY . .
WORKDIR "/src/ApiHelloGit"
RUN dotnet publish "ApiHelloGit.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
EXPOSE 8080
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "ApiHelloGit.dll"]
