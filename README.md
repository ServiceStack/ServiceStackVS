# [ServiceStack VS.NET Templates](https://visualstudiogallery.msdn.microsoft.com/5bd40817-0986-444d-a77d-482e43a48da7)

[![VS.NET Gallery Download](https://raw.githubusercontent.com/ServiceStack/Assets/master/img/servicestackvs/vsgallery-download.png)](http://visualstudiogallery.msdn.microsoft.com/5bd40817-0986-444d-a77d-482e43a48da7)

ServiceStackVS is a Visual Studio extension that enhances the development experience when working the [ServiceStack framework](https://servicestack.net).

# [TypeScript React App (beta)](https://github.com/ServiceStackApps/typescript-react-template/)

[![](https://raw.githubusercontent.com/ServiceStack/Assets/master/img/release-notes/typescript-react-jspm-banner.png)](https://github.com/ServiceStackApps/typescript-react-template/)

ServiceStack's new TypeScript React Single Page App VS.NET template offers the current **Gold Standard** for 
developing modern JavaScript Apps in VS.NET with the just released:
 
  - [TypeScript 1.8](http://www.typescriptlang.org/) - Superset of JavaScript with optional typing, advanced language features and down-level ES5 support
  - [JSPM](http://jspm.io/) - JavaScript Package Manager supporting SystemJS modules and multiple npm and GitHub repositories 
  - [typings](https://github.com/typings/typings) - Package manager to search and install TypeScript definition files
  - [React](https://facebook.github.io/react/) - Simple, high-performance JavaScript UI Framework utilizing a Virtual DOM and Reactive Data flows
  - [Redux](https://github.com/rackt/redux) - Predictable state manager for JavaScript Apps
  
Providing a great base for the development of large-scale, JavaScript Apps that's further enhanced by a great 
development experience within Visual Studio. 

### Pre-Requisites

Download and install the latest **TypeScript 1.8+** for your IDE:

 - [VS.NET 2015](https://www.microsoft.com/en-us/download/details.aspx?id=48593)
 - [VS.NET 2013](https://www.microsoft.com/en-us/download/details.aspx?id=48739)

> The TypeScript Single Page App templates are optimized for VS.NET 2015's support for [tsconfig.json](https://github.com/Microsoft/TypeScript/wiki/tsconfig.json)

![](https://raw.githubusercontent.com/ServiceStack/Assets/master/img/release-notes/typescript-react-jspm-template.png)

## [typescript-react-template](https://github.com/ServiceStackApps/typescript-react-template/)

To learn more about this template and explore its different features. please see the 
[in-depth typescript-react-template guide](https://github.com/ServiceStackApps/typescript-react-template/).

Happy TypeScripting!

### [React Desktop Apps (Beta) Template](https://github.com/ServiceStackApps/ReactDesktopApps)

The **React Desktop Apps** template provides everything you need to package your ASP.NET ServiceStack Web App into a native Windows Winforms App, OSX Cocoa Desktop App or cross-platform (Windows/OSX/Linux) "headless" Console App which instead of being 
embedded inside a Native UI, launches the Users prefered Web Browser for its Web UI.

![React Desktop Apps](https://raw.githubusercontent.com/ServiceStack/Assets/master/img/gap/react-desktop-splash.png)

This Hybrid model of developing Desktop Apps with modern WebKit technologies offers a more productive and reusable alternative to developing bespoke WPF Apps in XAML or Cocoa OSX Apps with Xcode. It enables full code reuse of the Web App whilst still allowing for platform specific .js, .css and C# specialization when needed. These advantages are also why GitHub also adopted a similar approach for their new cross-platform UI in their flagship [Windows and OSX Desktop Apps](http://githubengineering.com/cross-platform-ui-in-github-desktop/).

The template is pre-configured with the necessary tools to package your Web Application 
into multiple platforms using the provided Grunt build tasks. The Desktop Apps are also debuggable allowing for a simplified and iterative dev workflow by running the preferred Host Project:

- **Web** - ASP.NET Web Application
- **Windows** - Native Windows application embedded in a CefSharp Chromium browser
- **OSX** - Native OS X Cocoa App embedded in a WebView control (requires Xamarin.Mac)
- **Console** - Single portable, cross platform executable that launches the user's prefered browser

To highlight this new template, we've ported the Chat-React example application in the [ReactChatApps project](https://github.com/ServiceStackApps/ReactChatApps).

![WinForms application with loading splash screen](https://github.com/ServiceStack/Assets/raw/master/img/livedemos/react-desktop-apps/redis-chat-app.gif)

# Project Structure
Just like other templates in ServiceStackVS, the **React Desktop Apps** template provides the same recommended structure as well as 3 additional other projects for producing the Console and WinForms applications in the VS generated solution as well as a **Xamarin.Mac** project and solution to build the native OSX application.

![Project structure of ReactChatApps](https://github.com/ServiceStack/Assets/raw/master/img/servicestackvs/react-desktop-apps-proj-structure.png)

### Visual Studio Solution
- **ReactChat** - Web applicaton which contains all our resources and files used while developing.
- **ReactChat.AppConsole*** - Console application, launches default browser on users application
- **ReactChat.AppWinForms*** - WinForms application using CefSharp and Chromium Embedded Framework to output our web application in a native application.
- **ReactChat.Resources*** - Embedded resources that are used by our AppWinForms and AppConsole application and target of `01-bundle-all` Grunt task. This project has references to all minified client resources (CSS, JavaScript, images, etc) and includes each of them as an *Embedded Resource*.
- **ReactChat.ServiceInterface** - Contains ServiceStack services.
- **ReactChat.ServiceModel** - Contains request/response classes.
- **ReactChat.Tests** - Contains NUnit tests. 

### Xamarin Studio Solution
The additional **ReactChat.AppMac** project which is referenced by the **ReactChatMac.sln** file is setup ready to run from Xamarin Studio, reusing resources as well as web services.

- **ReactChat.AppMac** - Xamarin.Mac host application controling native UI features and platform specific files for OSX
- **ReactChat.ServiceInterface** - Project refernce to Visual Studio generated project
- **ReactChat.ServiceModel** - Project reference to the Visual Studio generated project
- **ReactChat.Resources*** - Assembly reference, by running the `01-bundle-all` grunt task, the output assembly is placed in a `lib` folder which is referenced by the Xamarin.Mac project.

### [Windows Service Template](https://visualstudiogallery.msdn.microsoft.com/5bd40817-0986-444d-a77d-482e43a48da7)

The Windows Service Template makes it super easy to create and install self-hosted ServiceStack solutions running within vanilla Windows Services without needing to rely on any additional 3rd Party packages or dependencies.

#### Optimized for Developer Productivity

To improve the development experience, the Windows Service Template includes a "Debug Mode" where **DEBUG** builds are run as a Console Application - improving developer iteration times and debugging experience.

#### Install, Start and Stop Windows Service Scripts

Also included are `install.bat`, `uninstall.bat`, `start.bat` and `stop.bat` Batch Scripts which lets you easily install and run your project as a local Windows Service. 

To Install, just build your project in **RELEASE** mode then run the `install.bat` script that's located in your projects home directory. After it's installed you can run `start.bat` to start your Windows Service which will launch your ServiceStack Project's Home Page in your default browser:

![](https://github.com/ServiceStack/Assets/raw/69c62bfb26cfddcc99ceae70ba8b10ca62d4ce99/img/servicestackvs/windows-service-template.gif)

> The batch files will automatically prompt for admin access if required

#### Install ServiceStackVS VS.NET Plugin

To use the new Windows Service Template [download the Install the latest ServiceStackVS](https://visualstudiogallery.msdn.microsoft.com/5bd40817-0986-444d-a77d-482e43a48da7). If you have a previous version of **ServiceStackVS** installed, it will need to be uninstalled before installing the latest version.

### [ReactJS App Template](https://github.com/ServiceStackApps/Chat-React)

The ServiceStackVS **ReactJS App** template shares the same approach for developing modern Single Page Apps in VS.NET as the existing [AngularJS App](https://github.com/ServiceStack/ServiceStackVS/blob/master/angular-spa.md) template by leveraging the **node.js** ecosystem for managing all aspects of Client App development utilizing the best-in-class libraries:

 - [npm](https://www.npmjs.org/) to manage node.js dependencies (bower, grunt, gulp)
 - [Bower](http://bower.io/) for managing client dependencies (angular, jquery, bootstrap, etc)
 - [Grunt](http://gruntjs.com/) as the primary task runner for server, client packaging and deployments
 - [Gulp](http://gulpjs.com/) used by Grunt to do the heavy-lifting bundling and minification

The templates conveniently pre-configures the above libraries into a working out-of-the-box solution, including high-level grunt tasks to take care of the full-dev-cycle of **building**, **packaging** and **deploying** your app:

 - **[01-run-tests](#01-run-tests)** - Runs Karma JavaScript Unit Tests
 - **[02-package-server](#02-package-server)** - Uses msbuild to build the application and copies server artefacts to `/wwwroot`
 - **[03-package-client](#03-package-client)** - Optimizes and packages the client artefacts for deployment in `/wwwroot`
 - **[04-deploy-app](#04-deploy-app)** - Uses MS WebDeploy and `/wwwroot_buld/publish/config.json` to deploy app to specified server

For in-depth details on how this template can be used for a great development workflow, see the [Chat-React demo](https://github.com/ServiceStackApps/Chat-React). 

### [AngularJS App Template](https://github.com/ServiceStack/ServiceStackVS/blob/master/angular-spa.md)

**AngularJS App** is a new template in [ServiceStackVS](https://github.com/ServiceStack/ServiceStack/wiki/Creating-your-first-project) that provides a modern opinionated web technology stack for developing rich Single Page Apps with [AngularJS](https://angularjs.org/) and ServiceStack. 

### The trend towards a JavaScript-based Client Build system 

The future of modern client web development has been moving towards a pure JavaScript environment for client HTML/JS/CSS development, we've noticed the benefits of this approach years ago when we developed our [node.js powered Bundler](https://github.com/ServiceStack/Bundler) taking advantage of [node.js](http://nodejs.org/) rich ecosystem for all our bundling, minification and Web DSL needs. As this is also the trend we see with web development in VS.NET going visible by the recent preview of [Grunt and Gulp.js integration in Visual Studio](http://www.hanselman.com/blog/IntroducingGulpGruntBowerAndNpmSupportForVisualStudio.aspx), we're confident in promoting this approach for the development of large, JavaScript-heavy Web Apps. 

This template marks our first iteration of this effort that we intend to continually improve and closely follow VS.NET's Grunt/Gulp.js integration so our automation tasks can be run directly from VS.NET UI Task Runners. In addition, this template also provides `.bat` files for running high-level grunt tasks so it alsa enables a good experience even without VS.NET's [Task Runner Explorer extension](http://visualstudiogallery.msdn.microsoft.com/8e1b4368-4afb-467a-bc13-9650572db708) (extension for those using 2013, built into VS 2015) which can be quickly run with a  [keyboard shortcut for running external commands](https://github.com/ServiceStack/Bundler#create-an-external-tool-inside-vsnet) in VS.NET.

![Template Runner Explorer](https://github.com/ServiceStack/Assets/raw/master/img/servicestackvs/trx-tasks.png)

### Technologies used

This templates leverages a number of best-in-class libraries to managing client web app development:

 - [Karma](http://karma-runner.github.io/0.12/index.html) for UI and JavaScript Unit testing
 - [Bower](http://bower.io/) for managing client dependencies (angular, jquery, bootstrap, etc)
 - [Grunt](http://gruntjs.com/) as the primary task runner for server, client packaging and deployments
 - [Gulp](http://gulpjs.com/) used by Grunt to do the heavy-lifting bundling and minification
 - [NPM](https://www.npmjs.org/) to manage node.js dependencies (grunt, gulp, etc)

A lot this functionality is pre-configured and working out-of-the-box encapsulated within the high-level Grunt Tasks below:

 - **[01-run-tests](https://github.com/ServiceStack/ServiceStackVS/blob/angular-spa-template/angular-spa.md#01-run-tests)** - Runs Karma JavaScript Unit Tests
 - **[02-package-server](https://github.com/ServiceStack/ServiceStackVS/blob/angular-spa-template/angular-spa.md#02-package-server)** - Uses msbuild to build the application and copies server artefacts to `/wwwroot`
 - **[03-package-client](https://github.com/ServiceStack/ServiceStackVS/blob/angular-spa-template/angular-spa.md#03-package-client)** - Optimizes and packages the client artefacts for deployment in `/wwwroot`
 - **[04-deploy-app](https://github.com/ServiceStack/ServiceStackVS/blob/angular-spa-template/angular-spa.md#04-deploy-app)** - Uses MS WebDeploy and `/wwwroot_buld/publish/config.json` to deploy app to specified server

### Requirements

This template relies on having [Node.js installed](http://nodejs.org/). If you try to use this template without node.js installed (ie, node.exe not found on the local machines PATH), you will be prompted to install it.

Once downloaded and installed, click `Continue` to create your project. If any of the NPM dependencies are not installed globally, the template will install the required NPM packages as well as download the Bower dependencies for the template. 

Due to the dependency on Bower, [Git also needs to be installed](https://www.npmjs.com/package/bower#windows-users) and select the second option to **Use Git from the Windows Command Prompt**. This is required due to commands run by Bower to install dependencies from Git repositories.

![](https://github.com/ServiceStack/Assets/raw/master/img/servicestackvs/git-setup.png)

As soon as your project is open, all the required front-end dependencies will be ready to go. Local NPM dependencies to run Karma, Grunt and Gulp will download asynchronously and you'll be able to see the progress inside the ServiceStackVS output window in Visual Studio.

![](https://github.com/ServiceStack/Assets/raw/master/img/servicestackvs/npm-install-on-create.png)

**For Visual Studio 2012 developers**, we have included shortcuts to the 4 main grunt tasks using batch files in the `wwwroot_build` folder. This is due to the [Task Runner Explorer extension](http://visualstudiogallery.msdn.microsoft.com/8e1b4368-4afb-467a-bc13-9650572db708) only supporting Visual Studio 2013 Update 3 and above.

### Managing front-end dependencies

To help add and install Bower and NPM dependencies, ServiceStackVS watches `bower.json` and `package.json` for changes and will run the appropriate install whenever these files are updated.

![NPM install performed on save](https://github.com/ServiceStack/Assets/raw/master/img/servicestackvs/angular-spa-package-demo.gif)

A great extension to help find names and versions of these dependencies is the [Package Intellisense extension](http://visualstudiogallery.msdn.microsoft.com/65748cdb-4087-497e-a394-2e3449c8e61e).

### Debugging and Intellisense

Development iterations follow along with Visual Studio’s normal pattern when used with the built in IIS Express as it hosts the local development folder, so vendor provided JS/CSS are directly referenced from the `/bower_components` folder.

Included in the template is the `_references.js` that enables CSS/JS intellisense for all the included Bower components and NPM components. It is included in the default location of `/Scripts/_references.js` so that it should work by default when creating a new application.

### Building with Grunt/Gulp

As front-ends are getting more complicated, tools like Grunt and Gulp help to fill the gaps left out of the MSBuild cycle. Included in the template is a series of 4 tasks to test, package and deploy your application from Grunt. 

![Building with TRX](https://github.com/ServiceStack/Assets/raw/master/img/servicestackvs/angular-spa-build-app-demo_2.gif)

Although the template doesn't include a Gulpfile.js, it leverages Gulp via a grunt package called **grunt-gulp**. This done to take advantage of Gulp packages whilst keeping all tasks within one file.

#### The `wwwroot` folder

This folder is where your application is **packaged to ready to be deployed**. It contains the result of the 'package' Grunt tasks which take care of things like minification and updating the associated references. It also contains all the required server components like the 'bin', Global.asax and web.config. 

![wwwroot folder output](https://github.com/ServiceStack/Assets/raw/master/img/servicestackvs/angular-spa-wwwroot-output.png)

#### 01-run-tests

This task runs your tests configured in the `karma.conf.js` file. The template by default runs the tests once using **PhantomJS** as the browser. If you want to have Karma running and watching your files as you code, this can be changed by switching the `singleRun` flag in the Grunt configuration:

    karma: {
            unit: {
                configFile: 'tests/karma.conf.js',
                singleRun: false,
                browsers: ['PhantomJS'],
                logLevel: 'ERROR'
            }
        }

![Running Karma tests via TRX](https://github.com/ServiceStack/Assets/raw/master/img/servicestackvs/01-run-tests.png)

#### 02-package-server

To keep the packaging of your server self-contained within Grunt, this task performs the following tasks to get all the required server components staged in the `wwwroot` folder:

 - Restore NuGet packages
 - Release build
 - Clean server files in wwwroot folder
 - Copy server required files

One of these server files included, is the `appsettings.txt`. This can be used in deployment for overriding any development App Settings in the `Web.config` and read them in using ServiceStacks `AppSettings` property in the `AppHost`:

```csharp
SetConfig(new HostConfig {
    DebugMode = AppSettings.Get("DebugMode", false)
});
```

If `appsettings.txt` exists in the root directory it will be used instead of the default Web.Config's `AppSettings`: 

```csharp
var customSettings = new FileInfo(@"~/appsettings.txt".MapHostAbsolutePath());
AppSettings = customSettings.Exists
    ? (IAppSettings)new TextFileSettings(customSettings.FullName, " ")
    : new AppSettings();
```

As an example the `appsettings.txt` contains a single `DebugMode` setting:

    # Release App Settings

    DebugMode false

The `appsettings.txt` file is located in the `wwwroot_build/deploy` folder by default which is copied as a part of the `02-package-server` task.

#### 03-package-client

This task is also separated out to allow updating, and if required deployment, to just the client side resources. It cleans the client side related files in the wwwroot folder, bundles and copies the new resources. The bundling searches for assets in the index.html file and follows build comments to minify and replace references. This enables simple use of debug JS files whilst still having control how our resources minify.

```html
<!-- build:js lib/js/angular.min.js -->
<script src="bower_components/angular/angular.js"></script>
<script src="bower_components/angular-route/angular-route.js"></script>
<!-- endbuild -->
<!-- build:js js/app.min.js -->
<script src="js/hello/controllers.js"></script>
<script src="js/navigation/controllers.js"></script>
<script src="js/app.js"></script>
<!-- endbuild -->
```

When creating new JS files for your application, they should be added in the `build:js js/app.min.js` comments shown above.

Vendor resources from bower are also minified here to keep deployment simple and straight forward. If it makes more sense to use CDN resources, these build comments can easily be replaced. The above example results in the following two script includes.

```html
<script src="lib/js/angular.min.js"></script>
<script src="js/app.min.js"></script>
```

If you want to use a **CDN resource when your application is deployed** but use bower components locally, `build` can be changed to `htmlbuild` specifying your own key after `:`. For example, if you want to include jQuery via a CDN when your application is deployed, the orignal

```html
<!-- build:js lib/js/jquery.min.js -->
<script src="bower_components/jquery/dist/jquery.js"></script>
<!-- endbuild -->
```

Would be changed to 
```html
<!-- htmlbuild:jqueryCdn -->
<script src="bower_components/jquery/dist/jquery.js"></script>
<!-- endbuild -->
```

To specify the URL or what should be added in the `htmlbuild` block at deploy time, just update the htmlBuild task assosiated with the `jqueryCdn` key. Eg,
``` JavaScript
.pipe(htmlBuild({
    jqueryCdn: function (block) {
        pipeTemplate(block, '<script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/1.11.3/jquery.min.js"></script>');
    })
}))
```

#### 04-deploy-app

To give developers a starting point for deployment, we have included the use of a **grunt-msdeploy** task that can deploy to an IIS server with Web deploy. Config for the deployment, eg the IIS Server address, application name, username and password is located in the `/wwwroot_build/publish/config.js`. 

    {
        "iisApp": "YourAppName",
        "serverAddress": "deploy-server.example.com",
        "userName": "{WebDeployUserName}",
        "password" : "{WebDeployPassword}"
    }

If you are using **Github's default Visual Studio ignore, this file will not be included in source control** due to the default rule of `publish/` to be ignored. You should check your Git Repository `.gitignore` rules before committing any potentially sensitive information into public source control.

This task shows a quick way of updating your development server quickly after making changes to your application. For more information on use web-deploy using either Grunt or just Visual Studio publish, see '[WebDeploy with AWS](https://github.com/ServiceStack/ServiceStack/wiki/WebDeploy-with-AWS#deploy-using-grunt)'.

#### Main project structure

    /css
        Application specific CSS files
    /img
        Image resources
    /js
        Application JS
    /partials
        AngularJS templates
    /Scripts
        Only here to enable intellisense for CSS and JS libraries by default
    /tests
        Karma config
        /unit
            Karma spec files
    /wwwroot
        Output directory
    /wwwroot_build
        Grunt shortcuts, build and deploy files
    AppHost.cs
    bower.json
    Global.asax
    gruntfile.js
    index.html
    package.json
    packages.config
    web.config

![](https://raw.githubusercontent.com/ServiceStack/ServiceStackVS/master/servicestackvs-templates.gif)

### ServiceStack with Angular JS Demo

The simple HelloWorld AngularJS application that is provided in the template calls the `/hello/{Name}` route and displays the result in the `<p>` below. 

![](https://github.com/ServiceStack/ServiceStackVS/raw/master/Images/angularjs_hello_app.png)

# Getting Started

### Download ServiceStackVS

ServiceStackVS supports Visual Studio 2015 RC1, 2013 and 2012 and can be [downloaded from the Visual Studio Gallery](http://visualstudiogallery.msdn.microsoft.com/5bd40817-0986-444d-a77d-482e43a48da7)

[![VS.NET Gallery Download](https://raw.githubusercontent.com/ServiceStack/Assets/master/img/servicestackvs/vsgallery-download.png)](http://visualstudiogallery.msdn.microsoft.com/5bd40817-0986-444d-a77d-482e43a48da7)

### VS.NET 2012 Prerequisites

  - VS.NET 2012 Users must install the [Microsoft Visual Studio Shell Redistributable](http://www.microsoft.com/en-au/download/details.aspx?id=40764)
  - It's also highly recommended to [Update to the latest NuGet](http://docs.nuget.org/docs/start-here/installing-nuget). 

> Alternatively if continuing to use an older version of the **NuGet Package Manager** you will need to click on **Enable NuGet Package Restore** after creating a new project to ensure its NuGet dependencies are installed.

This project structure includes examples of a lot of the different tasks that will have to be done while building a single page application to guide developers as their application grows. The AngularJS side is largely influenced by various incarnations of the angular-seed project whilst still be contained within a VS project.

## ServiceStackVS Project Templates 

The C# Project templates included in ServiceStackVS extension:

- AngularJS App 
- ReactJS App
- ServiceStack ASP.NET Empty
- ServiceStack ASP.NET MVC4 & MVC5
- ServiceStack ASP.NET with AngularJS
- ServiceStack ASP.NET with Bootstrap
- ServiceStack ASP.NET with Razor
- ServiceStack Self Host Empty
- ServiceStack Self Host with Razor
- ServiceStack Windows Service

These project templates are structured to encourage patterns to help kickstart your new ServiceStack application.

## Projects Structure

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

#### Disable Update On Save Settings
The default beahviour of ServiceStackVS is to update ServiceStack references on save so that you can easily get the latest changes and not work with incorrect or out of date references. Something this behaviour might not be desired, so this behaviour can be disabled with a `servicestack.vsconfig` file at the base of the project with these refereces. 
``` servicestack.vsconfig
DisableNpmInstallOnSave true
DisableBowerInstallOnSave true
DisableUpdateReferenceOnSave true
```

## F# Templates

The F# Project templates included in ServiceStackVS extension:

![F# templates](https://github.com/ServiceStack/Assets/raw/master/img/servicestackvs/fhsarp-templates.png)

These F# templates follow the same recommended multi-project structure used in the C# templates. There's also a community created [F# ServiceStack](http://visualstudiogallery.msdn.microsoft.com/278caff1-917a-4ac1-a552-e5a2ce0f6e1f) extension for ServiceStack (V3 and V4) projects in different single project configurations.

### F# ASP.NET with Freebase API Demo

Below is an example of creating a service that serves data from Freebase, showing F# strengths of concise, readable code taking advantage of ServiceStack's built-in data formats:

![](https://github.com/ServiceStack/Assets/raw/master/img/servicestackvs/fsharp-template-with-demo.gif)

[About the F# Freebase Demo](https://github.com/ServiceStack/ServiceStackVS/blob/master/fsharp.md#f-aspnet-with-freebase-api-demo).

## Build

This project requires the Visual Studio 2013 SDK to build the extension.

------

# [Add ServiceStack Reference](https://github.com/ServiceStack/ServiceStack/wiki/Add-ServiceStack-Reference)

ServiceStack's new **Add ServiceStack Reference** feature shows our initial support for adding generated Native Types to client VS.NET projects using [ServiceStackVS](https://github.com/ServiceStack/ServiceStack/wiki/Creating-your-first-project#step-1-download-and-install-servicestackvs) - providing a simpler, cleaner and more versatile alternative to WCF's **Add Service Reference** feature that's built into VS.NET. 

The first languages supported are C#, F#, VB.Net and TypeScript, effectively providing a flexible alternative than sharing your DTO assembly with clients, now clients can easily add a reference to a remote ServiceStack instance and update DTO's directly from within VS.NET. This also lays the groundwork and signals our approach on adding support for typed API's in other languages in future. Add a [feature request for your favorite language](http://servicestack.uservoice.com/forums/176786-feature-requests) to prioritize support for it sooner!

Our goal with Native Types is to provide an alternative for sharing DTO dlls, that can enable a better dev workflow for external clients who are now able to generate (and update) Typed APIs for your Services from a remote url - reducing the burden and effort required to consume ServiceStack Services whilst benefiting from clients native language strong-typing feedback.

ServiceStackVS offers the generation and updating of these clients through the same context for C#, F# and VB.Net. This gives developers a consistent way of creating and updating your DTOs regardless of your language of choice.

### Supported Languages

* [C# Add ServiceStack Reference](https://github.com/ServiceStack/ServiceStack/wiki/CSharp-Add-ServiceStack-Reference)
* [F# Add ServiceStack Reference](https://github.com/ServiceStack/ServiceStack/wiki/FSharp-Add-ServiceStack-Reference)
* [VB.NET Add ServiceStack Reference](https://github.com/ServiceStack/ServiceStack/wiki/VB.Net-Add-ServiceStack-Reference)
* [TypeScript Add ServiceStack Reference](https://github.com/ServiceStack/ServiceStack/wiki/TypeScript-Add-ServiceStack-Reference)

## Example Usage

> C# Android PCL Client example

![C# Android PCL Client example](https://raw.githubusercontent.com/ServiceStack/ServiceStackVS/master/Images/android-add-ref-demo.gif)

> VB.NET client talking with C# Server example

![CSharp server with VB.Net client example](https://github.com/ServiceStack/Assets/raw/master/img/servicestackvs/servicestack%20reference/csharp-server-vb-client.gif)

## ServiceStackXS - Xamarin Studio Addin
ServiceStack's **Add ServiceStack Reference** is now available for Xamarin Studio via the official ServiceStack addin, ServiceStackXS! Just like ServiceStackVS, ServiceStackXS includes Add/Update ServiceStack Reference for use with ServiceStack servers enabling a better workflow for external clients. ServiceStackXS is initially adding support for the following languages.

* [C# Add ServiceStack Reference](https://github.com/ServiceStack/ServiceStack/wiki/CSharp-Add-ServiceStack-Reference)
* [F# Add ServiceStack Reference](https://github.com/ServiceStack/ServiceStack/wiki/FSharp-Add-ServiceStack-Reference)
* [VB.NET Add ServiceStack Reference](https://github.com/ServiceStack/ServiceStack/wiki/VB.Net-Add-ServiceStack-Reference)


### Troubleshooting templates with 4.0.48
If you're having issues with the **AngularJS App**, **React App** or **React Desktop Apps** templates add the following work around for 4.0.48 in your `AppHost.cs` below the `SetConfig` statement in the `Configure` method.

```
for (int i = 0; i < Config.ScanSkipPaths.Count; i++)
{
    Config.ScanSkipPaths[i] = Config.ScanSkipPaths[i].TrimStart('/');
}
```
[More information about this work around in the 4.0.48 release notes](https://github.com/ServiceStack/ServiceStack/blob/1304873a9ee27e57942e3d97f0bd6edf4e8bae72/docs/2015/release-notes.md#configscanskippaths-not-ignoring-folders).

### Feedback

We hope **ServiceStackVS** helps make ServiceStack developers more productive than ever and we'll look at continue improving it with new features in future. [Suggestions and feedback are welcome](http://servicestack.uservoice.com/forums/176786-feature-requests) or raise any issues in the [Issues List](https://github.com/ServiceStack/Issues) or submit new [feature requests in our UserVoice](http://servicestack.uservoice.com/forums/176786-feature-requests).
