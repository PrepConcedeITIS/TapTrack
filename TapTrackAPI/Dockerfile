FROM mcr.microsoft.com/dotnet/aspnet:5.0 as base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 as build
WORKDIR /src

COPY TapTrackAPI.csproj ./TapTrackAPI/
COPY ./TapTrackAPI.Core/TaptrackAPI.Core.csproj ./TaptrackAPI.Core/
COPY ../TapTrackAPI.Core.Features/*.csproj ./TapTrackAPI.Core/Features
COPY ../TapTrackAPI.Data/*.csproj ./TapTrackAPI.Data/

WORKDIR /src/TapTrackApi/
RUN dotnet restore
COPY . . 
WORKDIR /src/TapTrackApi/
RUN dotnet build TapTrackApi.csproj -c Release -o /app

FROM build as publish
RUN dotnet publish  TapTrackApi.csproj -c Release -o /app

FROM base as final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "TapTrackApi.dll"]