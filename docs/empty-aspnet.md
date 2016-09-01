## Truly Empty ASP.NET Template

![](http://i.imgur.com/ZCHoJFA.png)

Over the years it's becoming harder and harder to create an Empty ASP.NET VS.NET Template as it 
continues to accumulate more cruft, unused dlls, hidden behavior, hooks into external services and 
other unnecessary bloat. Most of the bloat added since ASP.NET 2.0 for the most part has been unnecessary 
yet most .NET developers end up living with it because it's in the default template and they're 
unsure what each unknown dlls and default configuration does or what unintended behavior it will 
cause down the line if they remove it.

For ServiceStack and other lightweight Web Frameworks this added weight is completely unnecessary
and can be safely removed. 
E.g. [most ServiceStack Apps just needs a few ServiceStack .dlls](https://github.com/ServiceStackApps/Chat#super-lean-front-and-back) 
and a [single Web.config mapping](https://github.com/ServiceStack/ServiceStack/wiki/Create-your-first-webservice#register-servicestack-handler)
to tell ASP.NET to route all calls to ServiceStack. Any other ASP.NET config you would add in 
ServiceStack projects is just to get ASP.NET to disable any conflicting default behavior, e.g:

```xml
<appSettings>
    <add key="webPages:Enabled" value="false" />
</appSettings>
```

Tells ASP.NET to stop hijacking Razor Views, required even when no ASP.NET Web Pages or MVC
dlls are referenced. If using 
[Server Events](https://github.com/ServiceStack/ServiceStack/wiki/Server-Events) 
you'll also need to disable dynamic compression:

```xml
<system.webServer>
   <urlCompression doStaticCompression="true" doDynamicCompression="false" />
</system.webServer>
```

To prevent ASP.NET from buffering responses, required even when `HttpResponseBase.BufferOutput=false`.

Or to reduce unnecessary requests and speed up iteration times, you can disable Browser Link with:

```xml
<appSettings>
    <add key="vs:EnableBrowserLink" value="false"/>
</appSettings>
```

### The Minimal ASP.NET Template we wanted

We've decided to reverse this trend and instead of focusing on what can be added, we're
focusing on what can be removed whilst still remaining useful for most modern ASP.NET Web Apps. 

With this goal we've reduced the ASP.NET Empty Template down to a single project with
the only external dependency being Roslyn:

![](http://i.imgur.com/jKFga3J.png)

Most dlls have been removed and the `Web.config` just contains registration for Roslyn and config for disabling
ASP.NET's unwanted default behavior:

```xml
<configuration>
    <appSettings>
        <add key="vs:EnableBrowserLink" value="false"/>
        <add key="webPages:Enabled" value="false" />
    </appSettings>
    
    <system.web>
        <httpRuntime targetFramework="4.5"/>
        <compilation debug="true"/>
    </system.web>

    <system.webServer>
    </system.webServer>
    
    <system.codedom>
        <compilers>
            <compiler language="c#;cs;csharp" extension=".cs" type="Microsoft.CodeDom.Providers.DotNetCompilerPlatform.CSharpCodeProvider, Microsoft.CodeDom.Providers.DotNetCompilerPlatform, Version=1.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" warningLevel="4" compilerOptions="/langversion:6 /nowarn:1659;1699;1701"/>
            <compiler language="vb;vbs;visualbasic;vbscript" extension=".vb" type="Microsoft.CodeDom.Providers.DotNetCompilerPlatform.VBCodeProvider, Microsoft.CodeDom.Providers.DotNetCompilerPlatform, Version=1.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" warningLevel="4" compilerOptions="/langversion:14 /nowarn:41008 /define:_MYTYPE=\&quot;Web\&quot; /optionInfer+"/>
        </compilers>
    </system.codedom>
</configuration>
```

The only `.cs` file is an Empty `Global.asax.cs` with an empty placeholder for running custom code on Startup:

```csharp
using System;

namespace WebApplication
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            
        }
    }
}
```

And that's it! `ASP.NET Empty` is a single project empty ASP.NET Web Application with no additional references 
which we wont be adding to in future other than any configuration necessary to disable default ASP.NET behavior 
or enable C#'s latest language features so you can safely use this template for creating small stand-alone 
ASP.NET Web Apps using lightweight Web Frameworks like ServiceStack or [Nancy](http://nancyfx.org/).

### Minimal but still Useful

You can then easily [Convert this empty template into a functional ServiceStack Web App](https://github.com/ServiceStack/ServiceStack/wiki/Create-your-first-webservice) by: 

1) Installing [ServiceStack and any other dependency](https://github.com/ServiceStackApps/Todos/blob/master/src/Todos/packages.config) you want to use, e.g:

	PM> Install-Package ServiceStack
	PM> Install-Package ServiceStack.Redis
   
2) Adding the [ASP.NET HTTP Handler mapping](https://github.com/ServiceStackApps/Todos/blob/fdcffd37d4ad49daa82b01b5876a9f308442db8c/src/Todos/Web.config#L34-L39) to route all requests to ServiceStack:

```xml
<system.webServer>
    <validation validateIntegratedModeConfiguration="false"/>
    <handlers>
	    <add path="*" name="ServiceStack.Factory" type="ServiceStack.HttpHandlerFactory, ServiceStack" verb="*" preCondition="integratedMode" resourceType="Unspecified" allowPathInfo="true"/>
    </handlers>
</system.webServer>
```

3) Adding your [ServiceStack AppHost and Services in Global.asax.cs](https://github.com/ServiceStackApps/Todos/blob/master/src/Todos/Global.asax.cs).

That's all that's needed to create a functional Web App, which in this case creates a
[Backbone TODO compatible REST API with a Redis back-end](https://github.com/ServiceStackApps/Todos/) 
which can power all [todomvc.com](http://todomvc.com) Single Page Apps.
