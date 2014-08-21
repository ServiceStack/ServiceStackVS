namespace $safeprojectname$

open ServiceStack.Text
open System.Diagnostics

module Main =
    open System
        [<EntryPoint>]
        let main argv = 
            new AppHost() |> (fun x -> x.Init()) |> (fun x -> x.Start("http://*:8088/")) |> ignore
            "ServiceStack SelfHost listening at http://localhost:8088 ".Print() |> ignore
            Process.Start("http://localhost:8088/") |> ignore
            Console.ReadLine() |> ignore
            0
