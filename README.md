# [ServiceStack VS.NET Templates](https://visualstudiogallery.msdn.microsoft.com/5bd40817-0986-444d-a77d-482e43a48da7)

ServiceStackVS supports Visual Studio 2015, 2013 and 2012* and can be [downloaded from the Visual Studio Gallery](http://visualstudiogallery.msdn.microsoft.com/5bd40817-0986-444d-a77d-482e43a48da7)

[![VS.NET Gallery Download](https://raw.githubusercontent.com/ServiceStack/Assets/master/img/servicestackvs/vsgallery-download.png)](http://visualstudiogallery.msdn.microsoft.com/5bd40817-0986-444d-a77d-482e43a48da7)

### Recommended Plugins and Tools
To take advantage of all the templates and to improve the development workflow, it's best to get the following Visual Studio plugins/extensions for all available versions of Visual Studio.

- [Node Tools for Visual Stuido](https://github.com/Microsoft/nodejstools/releases/tag/v1.1.1)
- [TypeScript Extension](https://github.com/Microsoft/TypeScript/releases)

### VS.NET 2012 Prerequisites

  - VS.NET 2012 Users must install the [Microsoft Visual Studio Shell Redistributable](http://www.microsoft.com/en-au/download/details.aspx?id=40764)
  - It's also highly recommended to [Update to the latest NuGet](http://docs.nuget.org/docs/start-here/installing-nuget). 

> Alternatively if continuing to use an older version of the **NuGet Package Manager** you will need to click on **Enable NuGet Package Restore** after creating a new project to ensure its NuGet dependencies are installed.

This project structure includes examples of a lot of the different tasks that will have to be done while building a single page application to guide developers as their application grows.

- [AngularJS App](#servicestack-with-angularjs)
- [Aurelia App](#typescript-aurelia-app)
- [React App](#typescript-react-app)
- [React Desktop Apps](#react-desktop-apps-template)
- [ASP.NET Empty](https://github.com/ServiceStack/ServiceStackVS/blob/master/docs/empty-aspnet.md)
- [ServiceStack ASP.NET Empty](https://github.com/ServiceStack/ServiceStack/wiki/Creating-your-first-project)
- [ServiceStack ASP.NET MVC4](https://github.com/ServiceStackApps/SocialBootstrapApi)
- ServiceStack ASP.NET MVC5 Empty
- [ServiceStack ASP.NET with AngularJS](#angularjs-app-template)
- [ServiceStack ASP.NET with Bootstrap](https://github.com/ServiceStackApps/EmailContacts)
- [ServiceStack ASP.NET with Razor](http://razor.servicestack.net)
- [Self Host Empty](https://github.com/ServiceStack/ServiceStack/wiki/Self-hosting)
- [Self Host with Razor](http://razor.servicestack.net/#runs-everywhere)
- [Windows Service Empty](#windows-service-template)

These project templates are structured to encourage patterns to help kickstart your new ServiceStack application.

## [TypeScript Aurelia App](https://github.com/ServiceStack/ServiceStackVS/blob/master/docs/aurelia.md)

[![](https://raw.githubusercontent.com/ServiceStack/Assets/master/img/servicestackvs/typescript-aurelia-jspm-banner.png)](https://github.com/ServiceStack/ServiceStackVS/blob/master/docs/aurelia.md)

ServiceStack's new [Aurelia TypeScript Template](https://github.com/ServiceStack/ServiceStackVS/blob/master/docs/aurelia.md)
offers a great starting point for developing modern web front-end that provides a great development experience in VS.NET:
 
  - [TypeScript 1.8+](http://www.typescriptlang.org/) - Superset of JavaScript with optional typing, advanced language features and down-level ES5 support
  - [JSPM](http://jspm.io/) - JavaScript Package Manager supporting SystemJS modules and multiple npm and GitHub repositories 
  - [typings](https://github.com/typings/typings) - Package manager to search and install TypeScript definition files
  - [Aurelia](http://aurelia.io/) - Aurelia is a JavaScript client framework for mobile, desktop and web leveraging simple conventions and empowering creativity.

Providing a great base for the development of large-scale, JavaScript Apps that's further enhanced by Aurelia's simple approach to front-end development.

### Pre-Requisites

Download and install the latest **TypeScript 1.8+** for your IDE:

 - [VS.NET 2015](https://www.microsoft.com/en-us/download/details.aspx?id=48593)

This template has been updated to [support Typings 1.0+ which had some breaking changes](https://github.com/typings/typings#updating-from-0x-to-10). If you are having trouble with any of the TypeScript templates, please ensure you have Typings 1.0+ installed globally by running: 

    npm install -g typings

> Due to an [outstanding ReSharper bug](https://youtrack.jetbrains.com/issue/RSRP-458759) and VS.NET's default old version of NodeJS v0.10.31, there are [currently two known issues with simple work arounds when developing with this template](https://github.com/ServiceStack/ServiceStackVS/blob/master/docs/aurelia.md#known-issues).
> 
> **Only Visual Studio 2015 is supported** due to [lack of `tsconfig.json` support in earlier versions of Visual Studio](https://github.com/Microsoft/TypeScript/issues/8784).

## TypeScript React App

[![](https://raw.githubusercontent.com/ServiceStack/Assets/master/img/release-notes/typescript-react-jspm-banner.png)](https://github.com/ServiceStackApps/typescript-react-template/)

ServiceStack's new [TypeScript React Single Page App VS.NET template](https://github.com/ServiceStackApps/typescript-react-template/)
offers the current **Gold Standard** for developing modern JavaScript Apps in VS.NET with the just released:
 
  - [TypeScript 1.8](http://www.typescriptlang.org/) - Superset of JavaScript with optional typing, advanced language features and down-level ES5 support
  - [JSPM](http://jspm.io/) - JavaScript Package Manager supporting SystemJS modules and multiple npm and GitHub repositories 
  - [typings](https://github.com/typings/typings) - Package manager to search and install TypeScript definition files
  - [React](https://facebook.github.io/react/) - Simple, high-performance JavaScript UI Framework utilizing a Virtual DOM and Reactive Data flows

Providing a great base for the development of large-scale, JavaScript Apps that's further enhanced by a great 
development experience within Visual Studio. 

### Pre-Requisites

Download and install the latest **TypeScript 1.8+** for your IDE:

 - [VS.NET 2015](https://www.microsoft.com/en-us/download/details.aspx?id=48593)
 - [VS.NET 2013](https://www.microsoft.com/en-us/download/details.aspx?id=48739)

This template has been updated to [support Typings 1.0+ which had some breaking changes](https://github.com/typings/typings#updating-from-0x-to-10). If you are having trouble with any of the TypeScript templates, please ensure you have Typings 1.0+ installed globally by running `npm install -g typings`.

> The TypeScript Single Page App templates are optimized for VS.NET 2015's support for [tsconfig.json](https://github.com/Microsoft/TypeScript/wiki/tsconfig.json)

![](https://raw.githubusercontent.com/ServiceStack/Assets/master/img/release-notes/typescript-react-jspm-template.png)

### [typescript-react-template](https://github.com/ServiceStackApps/typescript-react-template/)

To learn more about this template and explore its different features. please see the 
[in-depth typescript-react-template guide](https://github.com/ServiceStackApps/typescript-react-template/).

Happy TypeScripting!

## [React Desktop Apps Template](https://github.com/ServiceStackApps/ReactDesktopApps)

The **React Desktop Apps** template provides everything you need to package your ASP.NET ServiceStack Web App into a native Windows Winforms App, OSX Cocoa Desktop App or cross-platform (Windows/OSX/Linux) "headless" Console App which instead of being 
embedded inside a Native UI, launches the Users preferred Web Browser for its Web UI.

![React Desktop Apps](https://raw.githubusercontent.com/ServiceStack/Assets/master/img/gap/react-desktop-splash.png)

This Hybrid model of developing Desktop Apps with modern WebKit technologies offers a more productive and reusable alternative to developing bespoke WPF Apps in XAML or Cocoa OSX Apps with Xcode. It enables full code reuse of the Web App whilst still allowing for platform specific .js, .css and C# specialization when needed. These advantages are also why GitHub also adopted a similar approach for their new cross-platform UI in their flagship [Windows and OSX Desktop Apps](http://githubengineering.com/cross-platform-ui-in-github-desktop/).

The template is pre-configured with the necessary tools to package your Web Application 
into multiple platforms using the provided Gulp build tasks. The Desktop Apps are also debuggable allowing for a simplified and iterative dev workflow by running the preferred Host Project:

- **Web** - ASP.NET Web Application
- **Windows** - Native Windows application embedded in a CefSharp Chromium browser
- **OSX** - Native OS X Cocoa App embedded in a WebView control (requires Xamarin.Mac)
- **Console** - Single portable, cross platform executable that launches the user's preferred browser

To highlight this new template, we've ported the Chat-React example application in the [ReactChatApps project](https://github.com/ServiceStackApps/ReactChatApps).

This template has been updated to [support Typings 1.0+ which had some breaking changes](https://github.com/typings/typings#updating-from-0x-to-10). If you are having trouble with any of the TypeScript templates, please ensure you have Typings 1.0+ installed globally by running `npm install -g typings`.

## [AngularJS App Template](https://github.com/ServiceStack/ServiceStackVS/blob/master/docs/angular-spa.md)

**AngularJS App** is a new template in [ServiceStackVS](https://github.com/ServiceStack/ServiceStack/wiki/Creating-your-first-project) that provides a modern opinionated web technology stack for developing rich Single Page Apps with [AngularJS](https://angularjs.org/) and ServiceStack. 

#### Technologies used

This templates leverages a number of best-in-class libraries to managing client web app development:

 - [Karma](http://karma-runner.github.io/0.12/index.html) for UI and JavaScript Unit testing.
 - [Gulp](http://gulpjs.com/) build tool to automate build and deployments.
 - [NPM](https://www.npmjs.org/) to manage node.js dependencies.

A lot this functionality is pre-configured and working out-of-the-box encapsulated within the high-level Gulp Tasks below:

 - **[01-package-server](https://github.com/ServiceStack/ServiceStackVS/blob/master/docs/angular-spa.md#01-package-server)** - Uses msbuild to build the application and copies server artifacts to `/wwwroot`
 - **[02-package-client](https://github.com/ServiceStack/ServiceStackVS/blob/master/docs/angular-spa.md#02-package-client)** - Optimizes and packages the client artifacts for deployment in `/wwwroot`
 - **[03-deploy-app](https://github.com/ServiceStack/ServiceStackVS/blob/master/docs/angular-spa.md#03-deploy-app)** - Uses MS WebDeploy and `/wwwroot_buld/publish/config.json` to deploy app to specified server
 - **package-and-deploy** - One task to rebuild and deploy via MS WebDeploy.

#### Tests
A simple Karma-Jasmine configuration is also provided with this template using a preconfigured `karma.conf.js` file. Any runner can be used to run these tests, for example the [Karma Test Adapter](https://github.com/MortenHoustonLudvigsen/KarmaTestAdapter) extension.

### Requirements

This template relies on having [Node.js installed](http://nodejs.org/). If you try to use this template without node.js installed (ie, node.exe not found on the local machines PATH), you will be prompted to install it.

![](https://github.com/ServiceStack/Assets/raw/master/img/servicestackvs/npm-install-on-create.png)

**For Visual Studio 2012 developers**, we have included shortcuts to the 3 main Gulp tasks using batch files in the `wwwroot_build` folder. This is due to the [Task Runner Explorer extension](http://visualstudiogallery.msdn.microsoft.com/8e1b4368-4afb-467a-bc13-9650572db708) only supporting Visual Studio 2013 latest Update 


## Windows Service Template

The Windows Service Template makes it super easy to create and install self-hosted ServiceStack solutions running within vanilla Windows Services without needing to rely on any additional 3rd Party packages or dependencies.

#### Optimized for Developer Productivity

To improve the development experience, the Windows Service Template includes a "Debug Mode" where **DEBUG** builds are run as a Console Application - improving developer iteration times and debugging experience.

#### Install, Start and Stop Windows Service Scripts

Also included are `install.bat`, `uninstall.bat`, `start.bat` and `stop.bat` Batch Scripts which lets you easily install and run your project as a local Windows Service. 

To Install, just build your project in **RELEASE** mode then run the `install.bat` script that's located in your projects home directory. After it's installed you can run `start.bat` to start your Windows Service which will launch your ServiceStack Project's Home Page in your default browser:
> The batch files will automatically prompt for admin access if required

## ServiceStack with AngularJS

The simple HelloWorld AngularJS application that is provided in the template calls the `/hello/{Name}` route and displays the result in the `<p>` below. 

![](https://github.com/ServiceStack/ServiceStackVS/raw/master/Images/angularjs_hello_app.png)

This template uses NuGet to manage JavaScript dependencies like Angular, unlike the AngularJS App template which uses NPM and Gulp.

## Common Template Project Structure

Starting a new application using a ServiceStackVS template will give you 4 new projects.

- Host project
- Service Interface project
- Service Model project
- Unit Testing project

![](https://raw.githubusercontent.com/ServiceStack/ServiceStackVS/master/Images/angularjs_solution.png)

The Host project contains an `AppHost` which has been configured with the `RazorFormat` plugin as well as hosting all the required JavaScript packages like AngularJS, Bootstrap and jQuery. It is setup initially with a single `_Layout.cshtml` using the default Bootstrap template and a `default.cshtml` which contains the HelloWorld demo. The Single Page Application (SPA) templates use a plain `default.html` in which these Razor views aren't used.

![](https://raw.githubusercontent.com/ServiceStack/ServiceStackVS/master/Images/angularjs_main_project.png)

The Host project has dependencies on the Service Model and Service Interface projects. These are the projects that contain your request/response DTOs, validators and filters. This structure is trying to encourage have your data structures and services in separate projects make testing and reuse easier.

![](https://raw.githubusercontent.com/ServiceStack/ServiceStackVS/master/Images/angularjs_other_projects.png)

The Unit Testing project, also as a dependency on these projects as it tests them in isolation of the main Host project. In the template, we are using the `BasicAppHost` to mock the AppHost we are using in the Host project. The example unit test is using NUit to setup and run the tests.

## F# Templates

The F# Project templates included in ServiceStackVS extension:

![F# templates](https://github.com/ServiceStack/Assets/raw/master/img/servicestackvs/fhsarp-templates.png)

These F# templates follow the same recommended multi-project structure used in the C# templates. There's also a community created [F# ServiceStack](http://visualstudiogallery.msdn.microsoft.com/278caff1-917a-4ac1-a552-e5a2ce0f6e1f) extension for ServiceStack (V3 and V4) projects in different single project configurations.

### F# ASP.NET with Freebase API Demo

Below is an example of creating a service that serves data from Freebase, showing F# strengths of concise, readable code taking advantage of ServiceStack's built-in data formats:

![](https://github.com/ServiceStack/Assets/raw/master/img/servicestackvs/fsharp-template-with-demo.gif)

[About the F# Freebase Demo](https://github.com/ServiceStack/ServiceStackVS/blob/master/fsharp.md#f-aspnet-with-freebase-api-demo).

# [Add ServiceStack Reference](https://github.com/ServiceStack/ServiceStack/wiki/Add-ServiceStack-Reference)

ServiceStack's **Add ServiceStack Reference** feature shows our initial support for adding generated Native Types to client VS.NET projects using [ServiceStackVS](https://github.com/ServiceStack/ServiceStack/wiki/Creating-your-first-project#step-1-download-and-install-servicestackvs) - providing a simpler, cleaner and more versatile alternative to WCF's **Add Service Reference** feature that's built into VS.NET. 

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

#### Disable Update On Save Settings
The default behavior of ServiceStackVS is to update ServiceStack references on save so that you can easily get the latest changes and not work with incorrect or out of date references. Something this behavior might not be desired, so this behavior can be disabled with a `servicestack.vsconfig` file at the base of the project with these refereces. The following snippet can be pasted into a new file called `servicestack.vsconfig` at the ase of a project to control this behaviour on a project by project basis. 

###### Example of `servicestack.vsconfig`
``` servicestack.vsconfig
DisableNpmInstallOnSave true
DisableBowerInstallOnSave true
DisableUpdateReferenceOnSave true
```

To apply this configuration, right click on the appropriate project, select `File`->`Add`->`New Item`, search for `Text` and add a new file called `servicestack.vsconfig`. This file is just a key/value pair separated by space with 3 options.

- `DisableNpmInstallOnSave` - This disables ServiceStackVS default to update NPM references on the save of a `packages.json` file*. *=This feature auto disables based on version of VS as to not intefer with other operations performing the NPM install.
- `DisableBowerInstallOnSave` - This disables ServiceStackVS default to update Bower references on the save of a `bower.json` file*. * =This feature auto disables based on version of VS as to not intefer with other operations performing the Bower install.
- `DisableUpdateReferenceOnSave` - This disables ServiceStackVS default to update ServiceStack reference files automatically on save.

### Feedback

We hope **ServiceStackVS** helps make ServiceStack developers more productive than ever and we'll look at continue improving it with new features in future. [Suggestions and feedback are welcome](http://servicestack.uservoice.com/forums/176786-feature-requests) or raise any issues in the [Issues List](https://github.com/ServiceStack/Issues) or submit new [feature requests in our UserVoice](http://servicestack.uservoice.com/forums/176786-feature-requests).
