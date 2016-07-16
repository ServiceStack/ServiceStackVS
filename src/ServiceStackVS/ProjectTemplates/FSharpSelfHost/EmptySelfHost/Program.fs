namespace $safeprojectname$

open ServiceStack.Text
open System.Diagnostics

module Main =
    open System
        [<EntryPoint>]
        let main argv = 
            let appHost = new AppHost()
            appHost.Init().Start("http://*:8088/") |> ignore
            "ServiceStack SelfHost listening at http://127.0.0.1:8088".Print()
            Process.Start("http://127.0.0.1:8088/") |> ignore
            Console.ReadLine() |> ignore
            0
