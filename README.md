# PostManager Project

![Alt text](./assets/post-manager-screen.png?raw=true "Title")

This project is a client/server application for searching post base on filters (tags) and sorting properties

**_To run the all project (FrontEnd and Backend), you must have the server and the client running. You can proceed with next section in order to launch both._**

## Server

The server is a .NET 6 Web API that serve an endpoint (`https://localhost:7401/api/posts`) to fetch posts based on query params (**_tags, sortBy, direction_**).

**Launch Server**

To run this web API, please check the following requirements

1. .NET 6 SDK
2. dotnet CLI (^7.0.302)
3. Visual Studio IDE (^2019)

- **Run With dotnet CLI**

  You can run the server with the following steps:

  1. Navigate to web API root: `cd server/PostManager`
  2. Build and launch server : `dotnet run PostManager.Api`

  You can then navigate to `https://localhost:7074/swagger/index.html` and perform some tests through swagger.

  You can also directly navigate to `https://localhost:7074/api/posts?tags=tech&sortBy=likes&direction=desc`

- **Run With Visual Studio**

  To run the project with Visual Studio you just have to make sure the project **PostManager.Api** is set as default starter project. Once done, you can launch the server by clicking on Visual Studio play button.

  You can then navigate to `https://localhost:7074/swagger/index.html` and perform some tests through swagger.

  You can also directly navigate to `https://localhost:7074/api/posts?tags=tech&sortBy=likes&direction=desc`

## Client

The client is an Angular which acts as frontend to fetch and display posts data. It's build with Angular 14.2.0

### Installation

In order to run the client,

- **[Angular CLI 14.2.0](https://github.com/angular/angular-cli)**
- **NodeJS 14.18.1**
- **NPM 6.14.15**

Once all requirements are meet, proceed with following steps :

- Navigate to client root : `cd client`
- Install dependencies: `npm install`
- Run : `npm start`

The dev server should be running at `http://localhost:4200/`
