namespace $safeprojectname$

open ServiceStack

[<CLIMutable>]
type HelloResponse = 
    { Result : string }

[<Route("/hello")>]
[<Route("/hello/{name}")>]
type Hello() = 
    interface IReturn<HelloResponse>
    member val Name : string = null with get, set