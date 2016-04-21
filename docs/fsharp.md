# ServiceStackVS F# #

## F# VS.NET ServiceStack Templates

New in [ServiceStackVS](http://visualstudiogallery.msdn.microsoft.com/5bd40817-0986-444d-a77d-482e43a48da7) are a number of different F# Project Templates:

![F# templates](https://github.com/ServiceStack/Assets/raw/master/img/servicestackvs/fhsarp-templates.png)

These F# templates follow the same **recommended multi-project layout** used in the C# templates. In addition, there's also a community created [F# ServiceStack](http://visualstudiogallery.msdn.microsoft.com/278caff1-917a-4ac1-a552-e5a2ce0f6e1f) extension for ServiceStack (V3 and V4) projects containing different single project configurations.

### F# ASP.NET with Freebase API Demo

The example below shows how we can use one of the templates to easily create a Service that serves data from Freebase API, showing the elegance of F#'s' concise, readable code working with ServiceStack's data formats:

![Freebase Demo](https://github.com/ServiceStack/Assets/raw/master/img/servicestackvs/fsharp-template-with-demo.gif)

For this quick demo, a new F# ASP.NET project was created to show how to expose data from the [Freebase API](https://www.freebase.com/) behind ServiceStack Web Services. This API also shows F#'s great integration with the [FSharp.Data package](https://www.nuget.org/packages/FSharp.Data). In the demo `Games` Service, we're returning a preview of the first 10 records from the **Video Games** list, showing the results in [ServiceStack's Auto HTML page](https://github.com/ServiceStack/ServiceStack/wiki/HTML5ReportFormat) as well as in the built-in [CSV format](https://github.com/ServiceStack/ServiceStack/wiki/ServiceStack-CSV-Format).

The steps used to create the demo boil down to:

- Create a new project using an F# template from ServiceStackVS
- Add the [FSharp.Data NuGet package](https://www.nuget.org/packages/FSharp.Data)

    Install-Package FSharp.Data

Create our own [Freebase](https://www.freebase.com/) client using our own API key:

```fsharp
type FreebaseDataWithKey = FreebaseDataProvider<Key="YouKeyGoesHere",LocalCache=true>
```

Create the `Games` Request DTO and make it available from the `/games` Route:

```fsharp
[<Route("/games")>]
type Games() = class end
```

Implement the Games Request method inside the existing `MyServices` Service and query **Video Games** from the Freebase API:

```fsharp
member this.Get(request : Games)=
    let data = FreebaseDataWithKey.GetDataContext()
    let result = query { 
        for e in data.``Arts and Entertainment``.``Video Games``.``Video Games`` do
            take 10
            select e } |> Seq.toList
    result
```

### Summary

This demo shows off the advantages that come from ServiceStack's design and focus on simplicity as well as F#'s concise and readable syntax.

------

## Download ServiceStackVS

ServiceStackVS supports both VS.NET 2013 and 2012 and can be [downloaded from the Visual Studio Gallery](http://visualstudiogallery.msdn.microsoft.com/5bd40817-0986-444d-a77d-482e43a48da7)

[![VS.NET Gallery Download](https://raw.githubusercontent.com/ServiceStack/Assets/master/img/servicestackvs/vsgallery-download.png)](http://visualstudiogallery.msdn.microsoft.com/5bd40817-0986-444d-a77d-482e43a48da7)

[VS.NET 2012 Prerequisites](https://github.com/ServiceStack/ServiceStackVS#vsnet-2012-prerequisites).