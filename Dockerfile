FROM mcr.microsoft.com/dotnet/aspnet:5.0 as base
EXPOSE 5001
FROM mcr.microsoft.com/dotnet/sdk:5.0 as build
WORKDIR /src

COPY TapTrackAPI/TapTrackAPI.csproj ./TapTrackAPI/
COPY TapTrackAPI.Core/TapTrackAPI.Core.csproj ./TapTrackAPI.Core/
COPY TapTrackAPI.Core.Features/TapTrackAPI.Core.Features.csproj ./TapTrackAPI.Core.Features/
COPY TapTrackAPI.Data/TapTrackAPI.Data.csproj ./TapTrackAPI.Data/
COPY TapTrackAPI.TelegramBot/TapTrackAPI.TelegramBot.csproj ./TapTrackAPI.TelegramBot/

WORKDIR /src/TapTrackAPI/
RUN dotnet restore TapTrackAPI.csproj

WORKDIR /src
COPY TapTrackAPI/ ./TapTrackAPI/
COPY TapTrackAPI.Core/ ./TapTrackAPI.Core/
COPY TapTrackAPI.Core.Features/ ./TapTrackAPI.Core.Features/
COPY TapTrackAPI.Data/ ./TapTrackAPI.Data/
COPY TapTrackAPI.TelegramBot ./TapTrackAPI.TelegramBot/

WORKDIR /src/TapTrackAPI/
RUN dotnet build TapTrackAPI.csproj -c Release -o /app

FROM build as publish
RUN dotnet publish TapTrackAPI.csproj /property:PublishWithAspNetCoreTargetManifest=false -c Release -o /app

FROM base as final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "TapTrackAPI.dll"]