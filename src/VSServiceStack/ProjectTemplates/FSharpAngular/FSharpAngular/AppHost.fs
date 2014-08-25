namespace $safeprojectname$
    open ServiceStack
    open ServiceStack.Common
    open ServiceStack.Razor
    open $safeprojectname$
    open $safeprojectname$.ServiceInterface
    open System

    type AppHost() = 
        inherit AppHostBase("$safeprojectname$", typeof<MyServices>.Assembly)
        override this.Configure container =
            //Add plugins
            //this.Plugins.Add(new PostmanFeature())
            //this.Plugins.Add(new CorsFeature())
            this.Plugins.Add(new RazorFormat())
            ignore()
        static member Start() = 
            new AppHost() |> (fun x -> x.Init()) |> ignore