ServiceStackVS
==========

ServiceStackVS is a Visual Studio extension to be used along side the ServiceStack framework.

![](https://raw.githubusercontent.com/ServiceStack/Assets/master/img/servicestackvs/vs-templates.png)

Getting Started with ServiceStackVS Templates
---------------------------------------------

### Download ServiceStackVS

ServiceStackVS supports both VS.NET 2013 and 2012 and can be [downloaded from the Visual Studio Gallery](http://visualstudiogallery.msdn.microsoft.com/5bd40817-0986-444d-a77d-482e43a48da7)

[![VS.NET Gallery Download](https://raw.githubusercontent.com/ServiceStack/Assets/master/img/servicestackvs/vsgallery-download.png)](http://visualstudiogallery.msdn.microsoft.com/5bd40817-0986-444d-a77d-482e43a48da7)

### VS.NET 2012 Prerequisites

  - VS.NET 2012 Users must install the [Microsoft Visual Studio Shell Redistributable](http://www.microsoft.com/en-au/download/details.aspx?id=40764)
  - It's also highly recommended to [Update to the latest NuGet](http://docs.nuget.org/docs/start-here/installing-nuget). 

> Alternatively if continuing to use an older version of the **NuGet Package Manager** you will need to click on **Enable NuGet Package Restore** after creating a new project to ensure its NuGet dependencies are installed.

### Feedback

We hope **ServiceStackVS** helps make ServiceStack developers more productive than ever and we'll look at continue improving it with new features in future. [Suggestions and feedback are welcome](http://servicestack.uservoice.com/forums/176786-feature-requests).  

Templates
---------

Project templates included in the extension:

- ServiceStack ASP.NET Empty
- ServiceStack ASP.NET MVC5
- ServiceStack ASP.NET with AngularJS
- ServiceStack ASP.NET with Bootstrap
- ServiceStack ASp.NET with Razor
- ServiceStack Self Host Empty

These project templates are structured to encourage patterns to help kickstart your new ServiceStack application.

![](https://raw.githubusercontent.com/ServiceStack/ServiceStackVS/master/servicestackvs-templates.gif)

Projects
--------

Starting a new ServiceStack ASP.NET with AngularJS application will give you 4 new projects.

- Host project
- Service Interface project
- Service Model project
- Unit Testing project

![](https://raw.githubusercontent.com/ServiceStack/ServiceStackVS/master/Images/angularjs_solution.png)

The Host project contains an AppHost which has been configured with the RazorFormat plugin as well as hosting all the required JavaScript packages like AngularJS, Bootstrap and jQuery. It is setup initially with a signle `_Layout.cshtml` using the default Bootstrap template and a `default.cshtml` which contains the HelloWorld demo.

![](https://raw.githubusercontent.com/ServiceStack/ServiceStackVS/master/Images/angularjs_main_project.png)

The Host project has dependencies on the Service Model and Service Inteface projects. These are the projects that contain your request/response DTOs, validators and filters. This structure is trying to encourage have your data structures and services in separate projects make testing and reuse easier.

![](https://raw.githubusercontent.com/ServiceStack/ServiceStackVS/master/Images/angularjs_other_projects.png)

The Unit Testing project, also as a dependency on these projects as it tests them in isolation of the main Host project. In the template, we are using the BasicAppHost to mock the AppHost we are using in the Host project. The example unit test is using NUit to setup and run the tests.

The Demo
--------

The simple HelloWorld angular application that is provided in the template calls the `/hello/{Name}` route and displays the result in the `<p>` below. 

![](https://github.com/ServiceStack/ServiceStackVS/raw/master/Images/angularjs_hello_app.png)

Build
-----
This project requires the Visual Studio 2013 SDK to build the extension.
