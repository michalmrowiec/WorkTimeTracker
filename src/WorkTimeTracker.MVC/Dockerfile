#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/WorkTimeTracker.MVC/WorkTimeTracker.MVC.csproj", "src/WorkTimeTracker.MVC/"]
COPY ["src/WorkTimeTracker.Application/WorkTimeTracker.Application.csproj", "src/WorkTimeTracker.Application/"]
COPY ["src/WorkTimeTracker.Domain/WorkTimeTracker.Domain.csproj", "src/WorkTimeTracker.Domain/"]
COPY ["src/WorkTimeTracker.Infrastructure/WorkTimeTracker.Infrastructure.csproj", "src/WorkTimeTracker.Infrastructure/"]
RUN dotnet restore "./src/WorkTimeTracker.MVC/WorkTimeTracker.MVC.csproj"
COPY . .
WORKDIR "/src/src/WorkTimeTracker.MVC"
RUN dotnet build "./WorkTimeTracker.MVC.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./WorkTimeTracker.MVC.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WorkTimeTracker.MVC.dll"]