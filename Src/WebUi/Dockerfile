#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0-bookworm-slim-arm32v7 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY Directory.Build.props ./
COPY ["WebUi/WebUi.csproj", "WebUi/"]
COPY ["Framework/NLogTools/NLogTools.csproj", "Framework/NLogTools/"]
COPY ["Framework/WebAPI.Host/WebAPI.Host.csproj", "Framework/WebAPI.Host/"]
COPY ["Framework/WinAPI/WinAPI.csproj", "Framework/WinAPI/"]
COPY ["Solve.Abstraction/Solve.Abstraction.csproj", "Solve.Abstraction/"]
COPY ["Solve/Solve.csproj", "Solve/"]
RUN dotnet restore "WebUi/WebUi.csproj"
COPY . .
WORKDIR "/src/WebUi"
RUN dotnet build "WebUi.csproj" -c Release -o /app/build -r linux-arm

FROM build AS publish
RUN dotnet publish "WebUi.csproj" -c Release -o /app/publish /p:UseAppHost=true /p:SelfContained=true -r linux-arm 

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Sudoku.WebUi.dll"]