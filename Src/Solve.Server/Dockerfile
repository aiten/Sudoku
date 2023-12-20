#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /Src
COPY ["Directory.Build.props", "."]
COPY ["Solve.Server/Solve.Server.csproj", "Solve.Server/"]
COPY ["Solve.Abstraction/Solve.Abstraction.csproj", "Solve.Abstraction/"]
COPY ["Solve/Solve.csproj", "Solve/"]
RUN dotnet restore "Solve.Server/Solve.Server.csproj"
COPY . .
WORKDIR "/Src/Solve.Server"
#RUN dotnet build "Solve.Server.csproj" -c Release -o /app/build -r linux-arm

FROM build AS publish
RUN dotnet publish "Solve.Server.csproj" -c Release -o /app/publish /p:UseAppHost=false /p:SelfContained=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Sudoku.Solve.Server.dll"]