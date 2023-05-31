#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0-bullseye-slim-arm32v7 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY Directory.Build.props ./
COPY ["Solve.Server/Solve.Server.csproj", "Solve.Server/"]
COPY ["Framework/NLogTools/NLogTools.csproj", "Framework/NLogTools/"]
COPY ["Framework/WebAPI.Host/WebAPI.Host.csproj", "Framework/WebAPI.Host/"]
COPY ["Framework/WinAPI/WinAPI.csproj", "Framework/WinAPI/"]
COPY ["Solve.Abstraction/Solve.Abstraction.csproj", "Solve.Abstraction/"]
COPY ["Solve/Solve.csproj", "Solve/"]
RUN dotnet restore "Solve.Server/Solve.Server.csproj"
COPY . .
WORKDIR "/src/Solve.Server"
# RUN dotnet build "Solve.Server.csproj" -c Release -o /app/build -r linux-arm

FROM build AS publish
RUN dotnet publish "Solve.Server.csproj" -c Release -o /app/publish /p:SelfContained=false -r linux-arm 

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Sudoku.Solve.Server.dll"]