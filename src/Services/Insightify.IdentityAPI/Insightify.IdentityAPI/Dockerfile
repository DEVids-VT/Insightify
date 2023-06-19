FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# Copy solution file
COPY "NuGet.config" "NuGet.config"

# Copy all project files
COPY "Services/Insightify.IdentityAPI/Insightify.IdentityAPI/Insightify.IdentityAPI.csproj" "Services/Insightify.IdentityAPI/Insightify.IdentityAPI/Insightify.IdentityAPI.csproj"
COPY . .

# Restore packages
RUN dotnet restore "Services/Insightify.IdentityAPI/Insightify.IdentityAPI/Insightify.IdentityAPI.csproj"


# Publish Identity.API project
WORKDIR /src/Services/Insightify.IdentityAPI/Insightify.IdentityAPI/
RUN dotnet build "Insightify.IdentityAPI.csproj" -c Release -o /app/build
RUN dotnet publish --no-restore --no-dependencies -c Release -o /app

FROM build AS publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Insightify.IdentityAPI.dll"]