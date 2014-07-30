Getting Started with ServiceStackVS Templates
=============================================

Once you have [installed ServiceStackVS](https://github.com/ServiceStack/ServiceStackVS#installation), you'll have a new set of templates appear on the left menu of 'Add New Project'.

![](https://raw.githubusercontent.com/ServiceStack/ServiceStackVS/master/Images/angularjs_add_new_project.png)

Once the projects have been created, just hit F5 to run the application. 

**Note: If you are running NuGet 2.6 or older, please checkout [these instructions](https://github.com/ServiceStack/ServiceStackVS/blob/master/GettingStarted.md).**

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







