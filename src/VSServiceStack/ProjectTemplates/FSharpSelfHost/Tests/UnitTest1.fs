namespace $safeprojectname$

open NUnit.Framework
open FsUnit
open Funq
open ServiceStack
open ServiceStack.Testing
open $saferootprojectname$.ServiceInterface
open $saferootprojectname$.ServiceModel

[<TestFixture>] 
type UnitTests ()=
    let mutable appHost = new BasicAppHost(typeof<MyServices>.Assembly)
    //Register dependencies
    let configContainer (c: Container)=
        ignore()

    //Action<Container> from unit
    let configureContainer = new System.Action<Container>(configContainer)
    
    [<TestFixtureSetUp>]
    member x.Init ()=
        appHost <- new BasicAppHost(typeof<MyServices>.Assembly)
        appHost.ConfigureContainer <- configureContainer
        appHost.Init() |> ignore
        ignore()

    [<TestFixtureTearDown>]
    member x.CleanUp ()=
        appHost.Dispose()

    [<Test>] 
    member x.HelloWorld ()=
        let service = appHost.Resolve<MyServices>()
        let req = new Hello(Name = "World")
        let response = service.Any req
        response.Result |> should equal "Hello, World!"