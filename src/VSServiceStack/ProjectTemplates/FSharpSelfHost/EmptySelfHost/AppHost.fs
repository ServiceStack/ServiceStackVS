namespace $safeprojectname$
    open ServiceStack
    open ServiceStack.Common
    open System
    open $safeprojectname$.ServiceInterface

    type AppHost() = 
        inherit AppSelfHostBase("$safeprojectname$", typeof<MyServices>.Assembly)
        override this.Configure container =
            //Add plugins
            //this.Plugins.Add(new PostmanFeature())
            //this.Plugins.Add(new CorsFeature())
            ignore()