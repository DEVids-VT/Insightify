FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Insightify.Notifications/Insightify.NotificationsAPI.csproj", "Insightify.Notifications/"]
RUN dotnet restore "Insightify.Notifications/Insightify.NotificationsAPI.csproj"
COPY . .
WORKDIR "/src/Insightify.Notifications"
RUN dotnet build "Insightify.NotificationsAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Insightify.NotificationsAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Insightify.NotificationsAPI.dll"]