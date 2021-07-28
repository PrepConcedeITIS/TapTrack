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
