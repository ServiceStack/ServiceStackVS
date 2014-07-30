ServiceStackVS
==========

ServiceStackVS is a Visual Studio extension to be used along side the ServiceStack framework.

Installation
------------
ServiceStackVS is packaged as a VSIX and is available the Visual Studio Gallery or from this repository.

ServiceStackVS does support **Visual Studio 2012**, however it does **require the installation of the [following patch](http://www.microsoft.com/en-au/download/details.aspx?id=40764)** as well as IE10 or above. If you are using Visual Studio 2013, this is not required.

This extension has a dependency on NuGet 2.0 or higher.

NuGet 2.0-2.6 Users
-------------------
NuGet users that haven't updated recently will need to "Enable NuGet Package Restore" on the solution context menu. This will enable NuGet dependencies to be included on build.

![](https://raw.githubusercontent.com/ServiceStack/ServiceStackVS/master/enable_package_restore.png)

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