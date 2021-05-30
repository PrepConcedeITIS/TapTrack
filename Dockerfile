FROM mcr.microsoft.com/dotnet/aspnet:5.0 as base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 as build

# Install Node.js
RUN curl -fsSL https://deb.nodesource.com/setup_14.x | bash - \
    && apt-get install -y \
        nodejs \
    && rm -rf /var/lib/apt/lists/*

WORKDIR /src
COPY TapTrackAPI/TapTrackAPI.csproj TapTrackAPI/
COPY TapTrackAPI.Core/TapTrackAPI.Core.csproj TapTrackAPI.Core/
COPY TapTrackAPI.Core.Features/TapTrackAPI.Core.Features.csproj TapTrackAPI.Core.Features/
COPY TapTrackAPI.Data/TapTrackAPI.Data.csproj TapTrackAPI.Data/
COPY TapTrackAPI.TelegramBot/TapTrackAPI.TelegramBot.csproj TapTrackAPI.TelegramBot/

RUN dotnet restore TapTrackAPI/TapTrackAPI.csproj
COPY . . 
WORKDIR /src/TapTrackAPI/
RUN dotnet build TapTrackAPI.csproj -c Release -o /app/build

FROM build as publish
RUN dotnet publish TapTrackAPI.csproj -c Release -o /app/publish

FROM base as final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TapTrackAPI.dll"]