#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Insightify.Posts.Startup/Insightify.Posts.Startup.csproj", "Insightify.Posts.Startup/"]
COPY ["Insightify.Posts.Application/Insightify.Posts.Application.csproj", "Insightify.Posts.Application/"]
COPY ["Insightify.Posts.Domain/Insightify.Posts.Domain.csproj", "Insightify.Posts.Domain/"]
COPY ["Insightify.Posts.Infrastructure/Insightify.Posts.Infrastructure.csproj", "Insightify.Posts.Infrastructure/"]
COPY ["Insightify.Posts.Web/Insightify.Posts.Web.csproj", "Insightify.Posts.Web/"]
RUN dotnet restore "Insightify.Posts.Startup/Insightify.Posts.Startup.csproj"
COPY . .
WORKDIR "/src/Insightify.Posts.Startup"
RUN dotnet build "Insightify.Posts.Startup.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Insightify.Posts.Startup.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Insightify.Posts.Startup.dll"]