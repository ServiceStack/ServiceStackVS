## ssutil.exe - Command line ServiceStack Reference tool

Add ServiceStack Reference is also moving beyond our growing list of supported IDEs and is now available in a single cross-platform .NET command-line **.exe** making it easy for build servers and automated tasks or command-line runners of your favorite text editors to easily Add and Update ServiceStack References!

To Get Started download **ssutil.exe** and open a command prompt to the containing directory:

#### Download [ssutil.exe](https://github.com/ServiceStack/ServiceStackVS/raw/master/dist/ssutil.exe)

#### ssutil.exe Usage:

![](https://raw.githubusercontent.com/ServiceStack/Assets/master/img/servicestackvs/ssutil-help.png)

**Adding a new ServiceStack Reference**

To create a new ServiceStack Reference, pass the remote ServiceStack **BaseUrl** then specify both which `-file` and `-lang` you want, e.g:

    ssutil http://techstacks.io -file TechStacks -lang CSharp

Executing the above command fetches the C# DTOs and saves them in a local file named `TechStacks.dtos.cs`.

**Available Languages**

 - CSharp
 - FSharp
 - VbNet
 - Java
 - Kotlin
 - Swift
 - TypeScript.d (interfaces definitions only)
 - TypeScript (Concrete classes)
 
**Update existing ServiceStack Reference**

Updating a ServiceStack Reference is even easier we just specify the path to the existing generated DTO's. E.g. Update the `TechStacks.dtos.cs` we just created with:

    ssutil TechStacks.dtos.cs
