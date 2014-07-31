Create your first project
=========================

This is a walkthrough of getting your first web service up and running whilst having a look at the how some of the different components work. 

## Step 0: Preparing your enviornment ##
To make this process as quick and as simple as we can, we first need to take a few steps to make sure we have all the right tools.

First we want to install ServiceStackVS Visual Studio extension. The easiest way to do this is to look for it from within Visual Studio. 

<img src="https://raw.githubusercontent.com/ServiceStack/ServiceStackVS/master/Images/tools_extensions.png" width="30%" align="left" hspace="10"><img src="https://raw.githubusercontent.com/ServiceStack/ServiceStackVS/master/Images/search_download.png" width="70%" align="right" hspace="10>

## Step 1: Selecting a template ##
Once the ServiceStackVS extension is installed, you will have new project templates available when creating a new project. For this example, let's choose ServiceStack ASP.NET Empty to get started.

![](https://raw.githubusercontent.com/ServiceStack/ServiceStackVS/master/Images/new_project_aspnet_empty.png)

Once you've created your application from the template, you should have 4 projects in your new solution. If you left the default name, you'll end up with a solution with the following structure.

![](https://raw.githubusercontent.com/ServiceStack/ServiceStackVS/master/Images/empty_project_solution.png)

## Step 2: Run your project ##

Press F5 and run your project!

![](https://raw.githubusercontent.com/ServiceStack/ServiceStackVS/master/Images/empty_project_run.png)

## Troubleshooting ##

If continuing to use an older version of the **NuGet Package Manager** you will need to click on **Enable NuGet Package Restore** after creating a new project to ensure its NuGet dependencies are installed. Without this enabled, Visual Studio will not pull down the ServiceStack dependencies and successfully build the project.

## How does it work? ##

Now that you're new project is running, let's have a look at what we have. The template comes with a single web service route which comes from the request DTO (Data Transfer Object).

    [Route("/hello/{Name}")]
    public class Hello : IReturn<HelloResponse>
    {
        public string Name { get; set; }
    }

    public class HelloResponse
    {
        public string Result { get; set; }
    }

The `Route` attribute is specifying what path `/hello/{Name}` where `{Name}` binds it's value to the public string property of 'Name'.

Let's access the HelloWorld service you created in your browser, so write the following URL in your address bar:

`GET http://<root_path>/hello/YourName` 
eg http://mono.servicestack.net/ServiceStack.Hello/servicestack/hello/Max.

As you can see after clicking on this link, ServiceStack also contains a HTML response format, which makes the XML/Json (...) output human-readable. To change the return format to Json, simply add `?format=json` to the end of the URL. You'll learn more about formats, endpoints (URLs, etc) when you continue reading the documentation.

## SOAP Troubleshooting
If you happen to generate requests from the wsdls with a tool like soapUI you may end up with an incorrectly generated request like this:
```xml
<soap:Envelope xmlns:soap="http://www.w3.org/2003/05/soap-envelope" xmlns:typ="http://schemas.servicestack.net/types">
  <soap:Header/>
  <soap:Body>
    <typ:Hello/>
  </soap:Body>
</soap:Envelope>
```

You can resolve this issue by adding the following line to your AssemblyInfo file
```csharp
[assembly: ContractNamespace("http://schemas.servicestack.net/types", 
           ClrNamespace = "<YOUR NAMESPACE>")]
```

Rebuild and regenerate the request from the updated wsdl. You should get a correct request this time.

```xml
<soap:Envelope xmlns:soap="http://www.w3.org/2003/05/soap-envelope" xmlns:typ="http://schemas.servicestack.net/types">
   <soap:Header/>
   <soap:Body>
      <typ:Hello>
         <!--Optional:-->
         <typ:Name>?</typ:Name>
      </typ:Hello>
   </soap:Body>
</soap:Envelope>
```

## Explore more ServiceStack features

The [EmailContacts solution](https://github.com/ServiceStack/EmailContacts/) is a new guidance available that walks through the recommended setup and physical layout structure of typical medium-sized ServiceStack projects, including complete documentation of how to create the solution from scratch, whilst explaining all the ServiceStack features it makes use of along the way.

# Community Resources

  - [How to build web services in MS.Net using ServiceStack](http://kborra.wordpress.com/2014/07/29/how-to-build-web-services-in-ms-net-using-service-stack/) by [@kishoreborra](http://kborra.wordpress.com/about/)
  - [Creating a Web API using ServiceStack](http://blogs.askcts.com/2014/05/15/getting-started-with-servicestack-part-3/) by [Lydon Bergin](http://blogs.askcts.com/)
  - [Getting Started with ServiceStack: Part 1](http://blogs.askcts.com/2014/04/23/getting-started-with-servicestack-part-one/) by [Lydon Bergin](http://blogs.askcts.com/)
  - [Getting started with ServiceStack – Creating a service](http://dilanperera.wordpress.com/2014/02/22/getting-started-with-servicestack-creating-a-service/)
  - [ServiceStack Quick Start](http://mediocresoft.com/things/servicestack-quick-start) by [@aarondandy](https://github.com/aarondandy)
  - [Fantastic Step-by-step walk-thru into ServiceStack with Screenshots!](http://nilsnaegele.com/codeedge/servicestack.html) by [@nilsnagele](https://twitter.com/nilsnagele)
  - [Your first REST service with ServiceStack](http://tech.pro/tutorial/1148/your-first-rest-service-with-servicestack) by [@cyberzeddk](https://twitter.com/cyberzeddk)
  - [New course: Using ServiceStack to Build APIs](http://blog.pluralsight.com/2012/11/29/new-course-using-servicestack-to-build-apis/) by [@pluralsight](http://twitter.com/pluralsight)
  - [ServiceStack the way I like it](http://tonyonsoftware.blogspot.co.uk/2012/09/lessons-learned-whilst-using.html) by [@tonydenyer](https://twitter.com/tonydenyer)
  - [Generating a RESTful Api and UI from a database with LLBLGen](http://www.mattjcowan.com/funcoding/2013/03/10/rest-api-with-llblgen-and-servicestack/) by [@mattjcowan](https://twitter.com/mattjcowan)
  - [ServiceStack: Reusing DTOs](http://korneliuk.blogspot.com/2012/08/servicestack-reusing-dtos.html) by [@korneliuk](https://twitter.com/korneliuk)
  - [Using ServiceStack with CodeFluent Entities](http://blog.codefluententities.com/2013/03/06/using-servicestack-with-codefluent-entities/) by [@SoftFluent](https://twitter.com/SoftFluent)
  - [ServiceStack, Rest Service and EasyHttp](http://blogs.lessthandot.com/index.php/WebDev/ServerProgramming/servicestack-restservice-and-easyhttp) by [@chrissie1](https://twitter.com/chrissie1)
  - [Building a Web API in SharePoint 2010 with ServiceStack](http://www.mattjcowan.com/funcoding/2012/05/04/building-a-web-api-in-sharepoint-2010-with-servicestack)
  - [JQueryMobile and ServiceStack: EventsManager tutorial part #3](http://paymentnetworks.wordpress.com/2012/04/24/jquerymobile-and-service-stack-eventsmanager-tutorial-post-3/) by [+Kyle Hodgson](https://plus.google.com/u/0/113523377752095590770/posts)
  - [REST Raiding. ServiceStack](http://dgondotnet.blogspot.de/2012/04/rest-raiding-servicestack.html) by [Daniel Gonzalez](http://www.blogger.com/profile/13468563783321963413)
  - [JQueryMobile and Service Stack: EventsManager tutorial](http://kylehodgson.com/2012/04/21/jquerymobile-and-service-stack-eventsmanager-tutorial-post-2/) / [Part 3](http://kylehodgson.com/2012/04/23/jquerymobile-and-service-stack-eventsmanager-tutorial-post-3/) by [+Kyle Hodgson](https://plus.google.com/u/0/113523377752095590770/posts)
  - [Like WCF: Only cleaner!](http://kylehodgson.com/2012/04/18/like-wcf-only-cleaner-9/) by [+Kyle Hodgson](https://plus.google.com/u/0/113523377752095590770/posts)
  - [ServiceStack I heart you. My conversion from WCF to SS](http://www.philliphaydon.com/2012/02/service-stack-i-heart-you-my-conversion-from-wcf-to-ss/) by [@philliphaydon](https://twitter.com/philliphaydon)
  - [Service Stack vs WCF Data Services](http://codealoc.wordpress.com/2012/03/24/service-stack-vs-wcf-data-services/)
  - [Creating a basic catalogue endpoint with ServiceStack](http://blogs.7digital.com/dev/2011/10/17/creating-a-basic-catalogue-endpoint-with-servicestack/) by [7digital](http://blogs.7digital.com)
  - [Building a Tridion WebService with jQuery and ServiceStack](http://www.curlette.com/?p=161) by [@robrtc](https://twitter.com/#!/robrtc)
  - [Anonymous type + Dynamic + ServiceStack == Consuming cloud has never been easier](http://www.ienablemuch.com/2012/05/anonymous-type-dynamic-servicestack.html) by [@ienablemuch](https://twitter.com/ienablemuch)
  - [Handful of examples of using ServiceStack based on the ServiceStack.Hello Tutorial](https://github.com/jfoshee/TryServiceStack) by [@82unpluggd](https://twitter.com/82unpluggd)