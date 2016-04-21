### New ssutil-cli - Command line ServiceStack reference tool
As requested, we've created a simple command line, cross platform executable for adding and updating ServiceStack references! Just like the same functionality in our growing list of supported IDEs, ssutil now allows for build servers or other automated tasks to add or update references simple and straight forward.

![Windows Demo](https://github.com/ServiceStack/Assets/raw/master/img/servicestackvs/ssutil-demo.gif)

![OSX Demo](https://github.com/ServiceStack/Assets/raw/master/img/servicestackvs/ssutil-demo-osx.gif)

#### Add new reference
To create a new file of a ServiceStack reference, simple pass the ServiceStack base url as the first argument and then specify the `-file` and `-lang`uage base on your own needs. For example.

`ssutil http://techstacks.io/ -file TechStacks -lang CSharp`

The command above will fetch the `CSharp` DTOs and create a local file called `TechStacks.dtos.cs`.

#### Update existing refrence
If you want to update an existing file as a part of a build process, you just need to pass the file path as the first argument. For example, given the file we just created in the previous example, we would update that file by running:

`ssutil TechStacks.dtos.cs`

#### Download
[ssutil is available is a cross platform binary here](https://github.com/ServiceStack/ServiceStackVS/raw/master/dist/ssutil.exe)

Help can be seen if no arguments are given the to executable.

![Help](https://github.com/ServiceStack/Assets/raw/master/img/servicestackvs/ssutil-help.png)
