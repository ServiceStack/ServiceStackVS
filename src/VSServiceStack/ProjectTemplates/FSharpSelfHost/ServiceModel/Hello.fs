namespace $safeprojectname$

open ServiceStack

[<CLIMutable>]
type HelloResponse = 
    { Result : string }

[<Route("/hello/{name}")>]
type Hello() = 
    interface IReturn<HelloResponse>
    member val Name = "" with get, set