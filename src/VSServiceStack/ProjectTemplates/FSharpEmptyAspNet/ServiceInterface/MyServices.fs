namespace $safeprojectname$

open ServiceStack
open $saferootprojectname$.ServiceModel

type MyServices() = 
    inherit Service()
    member this.Any(request : Hello) = { Result = "Hello, {0}!".Fmt(request.Name) }