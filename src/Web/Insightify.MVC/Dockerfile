#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 5030

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Insightify.MVC/Insightify.MVC.csproj", "Insightify.MVC/"]
RUN dotnet restore "Insightify.MVC/Insightify.MVC.csproj"
COPY . .
WORKDIR "/src/Insightify.MVC"
RUN dotnet build "Insightify.MVC.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Insightify.MVC.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Insightify.MVC.dll"]