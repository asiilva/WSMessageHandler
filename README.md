# WebSocket Message Handler
This is an open source project implementing WebSocket (RFC6455) using .NET Core. As we commonly see websockets being used on chats, this project aims to apply websocket communication not only on chats but also on different applicable scenarious. 

**Content** 

  - [Install](#install)
  - [Test](#test)
  - [Basic Usage](#basic-usage)
  - [Paths](#paths)
  - [Examples](#examples)

## Install

Prerequisities:

```bash
https://www.microsoft.com/net/learn/get-started/windows (.NET Core SDK - Windows)
https://www.microsoft.com/net/learn/get-started/linux (.NET Core SDK - Linux)
https://www.microsoft.com/net/learn/get-started/macos (.NET Core SDK - macOS)
http://activemq.apache.org/getting-started.html (ActiveMQ)
```

Run Visual Studio Code Integrated Terminal (bash/cmd):

```bash
dotnet restore
dotnet build
dotnet run
```

Run ActiveMQ on Docker

```bash
docker pull webcenter/activemq 
docker run --name='activemq' -d -p 8161:8161 -p 61616:61616 -p 61613:61613 webcenter/activemq:latest
```

## Test

This WebSocket uses xUnit as a test framework. It can be easily tested by running the command below.

```bash
dotnet test
```

## Basic Usage

This application works through the url's shown below:

```bash
http://localhost:<port>/swagger (API documentation)
http://localhost:<port>/<version>/api/version/<resource> (API)
http://localhost:<port>/<version>/ws (WebSocket)
```
    
## Paths

Available API resources:

Resource | Entity | HTTP Verbs | Query Strings
--------- | ----------- | --------------- | ---------------------
/notification | Notification | POST |


## Examples:

```
http://localhost:<port>/api/v1/notification
ws://localhost:<port>/v1/ws
```