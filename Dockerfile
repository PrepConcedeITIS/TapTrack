FROM mcr.microsoft.com/dotnet/sdk:5.0 as build
WORKDIR /app

COPY TapTrackAPI/TapTrackAPI.csproj ./TapTrackAPI/
COPY TapTrackAPI.Core/TapTrackAPI.Core.csproj ./TapTrackAPI.Core/
COPY TapTrackAPI.Core.Features/TapTrackAPI.Core.Features.csproj ./TapTrackAPI.Core.Features/
COPY TapTrackAPI.Data/TapTrackAPI.Data.csproj ./TapTrackAPI.Data/
COPY TapTrackAPI.TelegramBot/TapTrackAPI.TelegramBot.csproj ./TapTrackAPI.TelegramBot/

RUN dotnet restore ./TapTrackAPI/TapTrackAPI.csproj

COPY TapTrackAPI/ ./TapTrackAPI/
COPY TapTrackAPI.Core/ ./TapTrackAPI.Core/
COPY TapTrackAPI.Core.Features/ ./TapTrackAPI.Core.Features/
COPY TapTrackAPI.Data/ ./TapTrackAPI.Data/
COPY TapTrackAPI.TelegramBot ./TapTrackAPI.TelegramBot/

RUN dotnet publish ./TapTrackAPI/TapTrackAPI.csproj -c Release -o ./out

FROM mcr.microsoft.com/dotnet/aspnet:5.0 
WORKDIR /app
EXPOSE 80
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "TapTrackAPI.dll"]