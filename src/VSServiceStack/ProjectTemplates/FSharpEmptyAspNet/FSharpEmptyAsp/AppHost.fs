namespace $safeprojectname$
    open ServiceStack
    open ServiceStack.Common
    open $safeprojectname$
    open $safeprojectname$.ServiceInterface
    open System

    type AppHost() = 
        inherit AppHostBase("$safeprojectname$", typeof<MyServices>.Assembly)
        override this.Configure container =
            //Add plugins
            //this.Plugins.Add(new PostmanFeature())
            //this.Plugins.Add(new CorsFeature())
            ignore()
        static member Start() = 
            let appHost = new AppHost()
            appHost.Init() |> ignore