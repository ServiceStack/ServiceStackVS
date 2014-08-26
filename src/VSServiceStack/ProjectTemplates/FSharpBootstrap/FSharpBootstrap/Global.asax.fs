namespace $safeprojectname$

open System

type Global() = 
    inherit System.Web.HttpApplication()
    member x.Application_Start (sender:Object, e:EventArgs) = 
          AppHost.Start()