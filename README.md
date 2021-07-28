## **General information**

This application is task tracker like YouTrack or Jira. As out of the box solution it has integration with Telegram
messenger. You can checkout deployed version [here](https://www.taptrack.tech)

## **Preconditions**

Your PC should have this installed:

* .NET 5 [download](https://dotnet.microsoft.com/download/dotnet/5.0)
* PostgreSQL [download](https://www.postgresql.org/download/)
* Node.js [download](https://nodejs.org/en/download/)
* Angular (`npm install -g @angular/cli` command after node.js installation)

## **How to start project?**

1. Run `UpdateDatabase.bat` file located at `TapTrackAPI.Data` folder
2. Open TapTrackAPI.sln file with IDE
3. There are two project configurations for local startup. You have two ways:
    * Select `Angular+API` configuration and run project. App will start at https://localhost:5001/
    * Select `Debug` configuration for separated start of API. In that case you also may start Angular client app.  
      To start Angular:
        1. Go to `TapTrackAPI/taptrack` folder
        2. Run `npm install` command
        3. Run `npm start` command
        4. API runs on https://localhost:5001/  
           Angular App runs on https://localhost:4200/

## **Tech stack**

* **Backend** - ASP.NET (.NET 5)
* **Client app** - Angular 11
* **Database** - PostgreSQL  
  **External API usage:** Telegram, Mailgun, image4io  
  **Additional technologies:** Docker, Heroku, Cloudflare
  
## P.S.
Feel free to create github issues for suggestions or errors