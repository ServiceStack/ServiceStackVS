Follow [@ServiceStack](https://twitter.com/servicestack) or join the [Google+ Community](https://plus.google.com/communities/112445368900682590445)
for updates, or [StackOverflow](http://stackoverflow.com/questions/ask) or the [Customer Forums](https://forums.servicestack.net/) for support.

# [ServiceStack VS.NET Templates](https://visualstudiogallery.msdn.microsoft.com/5bd40817-0986-444d-a77d-482e43a48da7)

ServiceStackVS supports Visual Studio 2017, 2015 and 2013 and can be installed from within VS.NET:

## Install ServiceStackVS 

Install the ServiceStackVS VS.NET Extension by going to `Tools > Extensions and Updates...`

[![](https://raw.githubusercontent.com/ServiceStack/ServiceStackVS/master/Images/tools_extensions.png)](https://raw.githubusercontent.com/ServiceStack/ServiceStackVS/master/Images/tools_extensions.png)

Then searching the Visual Studio Gallery for **ServiceStack**

[![](https://raw.githubusercontent.com/ServiceStack/ServiceStackVS/master/Images/search_download.png)](https://raw.githubusercontent.com/ServiceStack/ServiceStackVS/master/Images/search_download.png)

Optionally it can be downloaded and installed from the [VS.NET Gallery](http://visualstudiogallery.msdn.microsoft.com/5bd40817-0986-444d-a77d-482e43a48da7)

[![VS.NET Gallery Download](https://raw.githubusercontent.com/ServiceStack/Assets/master/img/servicestackvs/vsgallery-download.png)](http://visualstudiogallery.msdn.microsoft.com/5bd40817-0986-444d-a77d-482e43a48da7)

Once installed you can create a new ServiceStack Project from the new VS.NET Projects dialog:

![ServiceStack VS.NET Templates Dialog](https://raw.githubusercontent.com/ServiceStack/docs/master/docs/images/ssvs/new-projects-dialog.png)

Most of ServiceStack Templates follow our [multi-project recommended projects structure](http://docs.servicestack.net/physical-project-structure).

### Visual Studio 2015 or 2013

If you're still using VS.NET 2015 or 2013 you can install the previous VS.NET ServiceStackVS extension:

 - [ServiceStackVS.vsix](https://github.com/ServiceStack/ServiceStackVS/raw/master/dist/2018/ServiceStackVS.vsix)

### Recommended Plugins and Tools

To take advantage of all the templates and to improve the development workflow, it's best to get the following Visual Studio 
plugins/extensions for all available versions of Visual Studio.

- [Node Tools for Visual Stuido](https://github.com/Microsoft/nodejstools/releases/tag/v1.1.1)
- [TypeScript Extension](https://github.com/Microsoft/TypeScript/releases)

## ServiceStack VS.NET Templates

This project structure includes examples of a lot of the different tasks that will have to be done while building a 
single page application to guide developers as their application grows.

 - [Angular4 App](http://docs.servicestack.net/templates-single-page-apps)
 - [Aurelia App](http://docs.servicestack.net/templates-single-page-apps)
 - [React App](http://docs.servicestack.net/templates-single-page-apps)
 - [React Desktop Apps](http://docs.servicestack.net/templates-single-page-apps)
 - [Vue App](http://docs.servicestack.net/templates-single-page-apps)
 - [AngularJS 1.5 App](http://docs.servicestack.net/templates-angularjs-v15)
 - [ASP.NET Empty](http://docs.servicestack.net/templates-aspnet-empty)
 - [ServiceStack ASP.NET Empty](http://docs.servicestack.net/create-your-first-webservice)
 - [ServiceStack ASP.NET MVC4](https://github.com/ServiceStackApps/SocialBootstrapApi)
 - [ServiceStack ASP.NET with Bootstrap](https://github.com/ServiceStackApps/EmailContacts)
 - [ServiceStack ASP.NET with Razor](http://razor.servicestack.net)
 - [Self Host Empty](http://docs.servicestack.net/self-hosting)
 - [Self Host with Razor](http://razor.servicestack.net/#runs-everywhere)
 - [Windows Service Empty](http://docs.servicestack.net/templates-windows-service)

These project templates are structured to encourage patterns to help kickstart your new ServiceStack application.

## Single Page App Templates

Our goal with our [Webpack-powered Single Page App VS.NET templates](http://docs.servicestack.net/templates-single-page-apps) 
is to provide access to the best tooling and development experience whilst keeping complexity and required knowledge 
to an absolute minimum, as such we're constantly researching how we can simplify our SPA templates, using the least 
moving parts and pick technologies that work seamlessly together whilst offering maximum value for minimal complexity. 
As the best SPA tooling mandates an npm-based workflow this is a delicate balance of trade-offs as the number of 3rd party 
JS components, tools, transpilers, scripts and plugins required for building on a modern SPA JS framework can quickly 
explode - something we actively fight hard against that influences each aspect of our default templates. 

### Benefits of TypeScript

Centering around TypeScript enables a lot of benefits in both a modern Type Safe language as well as access to 
the latest JS features whilst being able to target downstream ES5 browsers for broad compatibility. TypeScript
is expertly designed and actively developed with frequent releases where new features added are backwards-compatible, 
intuitive and work well with existing features. Being encapsulated within a single tool means new features 
doesn't introduce new complexity, i.e. there's no additional config files to learn and little possibility
that new TypeScript releases will break existing Apps. 

## F# Templates

The F# Project templates included in ServiceStackVS extension:

![F# templates](https://raw.githubusercontent.com/ServiceStack/docs/master/docs/images/ssvs/new-projects-fsharp-dialog.png)

These F# templates follow the same recommended multi-project structure used in the C# templates. 
There's also a community created [F# ServiceStack](http://visualstudiogallery.msdn.microsoft.com/278caff1-917a-4ac1-a552-e5a2ce0f6e1f) 
extension for ServiceStack (V3 and V4) projects in different single project configurations.

### F# ASP.NET with Freebase API Demo

Below is an example of creating a service that serves data from Freebase, showing F# strengths of concise, readable code taking advantage of ServiceStack's built-in data formats:

![](https://github.com/ServiceStack/Assets/raw/master/img/servicestackvs/fsharp-template-with-demo.gif)

[About the F# Freebase Demo](https://github.com/ServiceStack/ServiceStackVS/blob/master/fsharp.md#f-aspnet-with-freebase-api-demo).

# [Add ServiceStack Reference](http://docs.servicestack.net/add-servicestack-reference)

ServiceStack's **Add ServiceStack Reference** feature shows our initial support for adding generated Native Types to 
client VS.NET projects using 
[ServiceStackVS](http://docs.servicestack.net/create-your-first-webservice#step-1-download-and-install-servicestackvs) - providing a 
simpler, cleaner and more versatile alternative to WCF's **Add Service Reference** feature that's built into VS.NET. 

The first languages supported are C#, F#, VB.Net and TypeScript, effectively providing a flexible alternative than sharing your 
DTO assembly with clients, now clients can easily add a reference to a remote ServiceStack instance and update DTO's directly 
from within VS.NET. This also lays the groundwork and signals our approach on adding support for typed API's in other languages in future. 
Add a [feature request for your favorite language](http://servicestack.uservoice.com/forums/176786-feature-requests) to prioritize support for it sooner!

Our goal with Native Types is to provide an alternative for sharing DTO dlls, that can enable a better dev workflow for 
external clients who are now able to generate (and update) Typed APIs for your Services from a remote url - reducing the 
burden and effort required to consume ServiceStack Services whilst benefiting from clients native language strong-typing feedback.

ServiceStackVS offers the generation and updating of these clients through the same context for C#, F# and VB.Net. 
This gives developers a consistent way of creating and updating your DTOs regardless of your language of choice.

### Supported Languages

* [C# Add ServiceStack Reference](/csharp-add-servicestack-reference)
* [TypeScript Add ServiceStack Reference](/typescript-add-servicestack-reference)
* [Swift Add ServiceStack Reference](/swift-add-servicestack-reference)
* [Java Add ServiceStack Reference](/java-add-servicestack-reference)
* [Kotlin Add ServiceStack Reference](/kotlin-add-servicestack-reference)
* [F# Add ServiceStack Reference](/fsharp-add-servicestack-reference)
* [VB.NET Add ServiceStack Reference](/vbnet-add-servicestack-reference)

## Example Usage

> C# Android PCL Client example

![C# Android PCL Client example](https://raw.githubusercontent.com/ServiceStack/ServiceStackVS/master/Images/android-add-ref-demo.gif)

> VB.NET client talking with C# Server example

![CSharp server with VB.Net client example](https://github.com/ServiceStack/Assets/raw/master/img/servicestackvs/servicestack%20reference/csharp-server-vb-client.gif)

## ServiceStackXS - Xamarin Studio Addin

ServiceStack's **Add ServiceStack Reference** is now available for Xamarin Studio via the official ServiceStack addin, ServiceStackXS! Just like ServiceStackVS, ServiceStackXS includes Add/Update ServiceStack Reference for use with ServiceStack servers enabling a better workflow for external clients. ServiceStackXS is initially adding support for the following languages.

* [C# Add ServiceStack Reference](/csharp-add-servicestack-reference)
* [F# Add ServiceStack Reference](/fsharp-add-servicestack-reference)
* [VB.NET Add ServiceStack Reference](/vbnet-add-servicestack-reference)

#### Disable Update On Save Settings

The default behavior of ServiceStackVS is to update ServiceStack references on save so that you can easily get the latest changes 
and not work with incorrect or out of date references. Something this behavior might not be desired, so this behavior can be 
disabled with a `servicestack.vsconfig` file at the base of the project with these refereces. The following snippet can be pasted 
into a new file called `servicestack.vsconfig` at the ase of a project to control this behaviour on a project by project basis. 

###### Example of `servicestack.vsconfig`

``` servicestack.vsconfig
DisableNpmInstallOnSave true
DisableBowerInstallOnSave true
DisableUpdateReferenceOnSave true
```

To apply this configuration, right click on the appropriate project, select `File`->`Add`->`New Item`, search for `Text` and add a 
new file called `servicestack.vsconfig`. This file is just a key/value pair separated by space with 3 options.

- `DisableNpmInstallOnSave` - This disables ServiceStackVS default to update NPM references on the save of a `packages.json` file*. *=This feature auto disables based on version of VS as to not intefer with other operations performing the NPM install.
- `DisableBowerInstallOnSave` - This disables ServiceStackVS default to update Bower references on the save of a `bower.json` file*. * =This feature auto disables based on version of VS as to not intefer with other operations performing the Bower install.
- `DisableUpdateReferenceOnSave` - This disables ServiceStackVS default to update ServiceStack reference files automatically on save.

### Feedback

We hope **ServiceStackVS** helps make ServiceStack developers more productive than ever and we'll look at continue improving it with new features in future. [Suggestions and feedback are welcome](http://servicestack.uservoice.com/forums/176786-feature-requests) or raise any issues in the [Issues List](https://github.com/ServiceStack/Issues) or submit new [feature requests in our UserVoice](http://servicestack.uservoice.com/forums/176786-feature-requests).
