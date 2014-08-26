namespace $safeprojectname$

open ServiceStack.Text
open System.Diagnostics

module Main =
    open System
        [<EntryPoint>]
        let main argv = 
            let appHost = new AppHost()
            appHost.Init() |> ignore
            appHost.Start("http://*:8088/") |> ignore
            "ServiceStack SelfHost listening at http://localhost:8088 ".Print() |> ignore
            Process.Start("http://localhost:8088/") |> ignore
            Console.ReadLine() |> ignore
            0
