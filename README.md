# jsreport .net docker-compose example

Simply open in VS and run docker with F5.

The example shows how you can run jsreport outside your app what is generally a better practice. It shows how you can use the [jsreport.AspNetCore](https://jsreport.net/learn/dotnet-aspnetcore) and Razor views to render reports through "external" jsreport docker container.

Additionally, it shows how you can define reports inside jsreport studio. Mount the data volume so your templates stay in the version control. And how to render such stored reports from your app.


### how to run

Open in Visual Code for example, and type in the terminal:

    docker-compose up --build

if you have problems have a file (`cleaner-ALL-container.ps1`) that automates the process of cleaning all containers, use with care.

**You can access:**

Webapp (contains examples)
http://localhost:32768/

And check the JsReport server:
http://localhost:5488/