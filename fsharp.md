# ServiceStackVS F# #

## F# Templates

New in [ServiceStackVS](https://github.com/ServiceStack/ServiceStack/wiki/HTML5ReportFormat) are a number of different F# Project Templates:

![F# templates](https://github.com/ServiceStack/Assets/raw/master/img/servicestackvs/fhsarp-templates.png)

These F# templates follow the same recommended multi-project structure used in the C# templates. In addition, there's also a community created [F# ServiceStack](http://visualstudiogallery.msdn.microsoft.com/278caff1-917a-4ac1-a552-e5a2ce0f6e1f) extension for ServiceStack (V3 and V4) projects containing different single project configurations.

### F# Template Freebase Demo

Below is an example of creating a service that serves data from Freebase, showing F# strengths of concise, readable code working with ServiceStack's data formats:

![Freebase Demo](https://github.com/ServiceStack/Assets/raw/master/img/servicestackvs/fsharp-template-with-demo.gif)

In this quick demo above, a new F# ASP.NET project was created to show how to expose data from the [Freebase API](https://www.freebase.com/) behind ServiceStack Web Services. This API is a great example of F#'s great integration with the [FSharp.Data package](https://www.nuget.org/packages/FSharp.Data). In the demo, we're previewing the first 10 records from the **Video Games** list, showing the results in [ServiceStack's Auto HTML page](https://github.com/ServiceStack/ServiceStack/wiki/HTML5ReportFormat) as well as in the built-in [CSV format](https://github.com/ServiceStack/ServiceStack/wiki/ServiceStack-CSV-Format).

The steps to create the demo boil down to:

- Create a new project using an F# template from ServiceStackVS
- Add the [FSharp.Data NuGet package](https://www.nuget.org/packages/FSharp.Data)

    Install-Package FSharp.Data

Create our own [Freebase](https://www.freebase.com/) client using our own API key:

```fsharp
type FreebaseDataWithKey = FreebaseDataProvider<Key="YouKeyGoesHere",LocalCache=true>
```

Create the `Games` Type to expose our Route:

```fsharp
[<Route("/games")>]
type Games() = class end
```

Create a typed request method on the existing `MyServices` service and query **Video Games** from the Freebase API:

```fsharp
member this.Get(request : Games)=
    let data = FreebaseDataWithKey.GetDataContext()
    let result = query { 
        for e in data.``Arts and Entertainment``.``Video Games``.``Video Games`` do
            take 10
            select e } |> Seq.toList
    result
```

This demo shows off the advantages that come from ServiceStack's design and focus on simplicity as well as F#'s concise and readable syntax.