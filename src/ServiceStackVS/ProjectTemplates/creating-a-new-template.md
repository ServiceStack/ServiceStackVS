# Creating a new template
This is a simple walkthrough for adding a new template to ServiceStackVS.

It will depend on the template being created, but generally the templates follow the 4 project template encouraged by ServiceStack of the following structure.

- Host - contains the web UI and AppHost configuration for the template
- Host.ServiceInterface - contains the ServiceStack `Service`s, usually a `MyService` with HelloWorld.
- Host.ServiecModel - contains the DTOs, usually a `Hello` and HelloResponse` requests
- Host.Tests - contains NUnit tests already setup showing a unit test against the `MyService` and `Hello` request.

For creating a non TypeScript Single Page Application template, it is easiest to just start from a copy of `EmptyAspNetHost` template with a new folder name. Once that is done, the following files should be updated to change how the template presents in Visual Studio.

### `Definitions\CSharp.vstemplate`
This is the definition that links the 4 separate projects together. If the host project name changes (eg from `EmptyAspNetHost` to a more generic `Host`, this is where paths will have to be fixed.

The preview name and description can be found at the top, this is what provides data to the Visual Studio New Project Wizard.

``` xml
    <Name>ServiceStack ASP.NET Empty</Name>
    <Description>ServiceStack ASP.NET Empty</Description>
    <DefaultName>WebApplication</DefaultName>
```

`DefaultName` influences what suggested name VisualStudio gives to the project. If a collision is detected, it increments project names with a number by default.

The other *.vstemplate files in each sub project template directory have these fields above, but they are ignored since this is a multi-project template. Each of these however have their own custom Wizards to affect which NuGet packages they install and other behaviour depending on what each template needs. Some of the custom VSIX IWizards ServiceStackVS have their own `README.md` for additional information on how these can be used. 

A common tokenization needed more multi-project templates is to reuse the project name given by the user in the sub-projects. This is the same `$safeprojectname$` token for the `Host` project, but this is overridden for the `Host.ServiceInterface`, `Host.ServiceModel` and `Host.Tests` projects. **To get the original project name of the `Host` project in the others, use `$saferootprojectname$`.**

#### NuGet Notes

The two WizardExtensions should remain, specifically the `NuGetInstallerWizard` which ensure the latest version of ServiceStack libraries are added to the project. This currently depends on NuGet v2 and may need upgrading or a new VSIX `IWizard` may have to be created to handle .NET Core and NuGet v3.

#### Project GUID notes
One of the limitations with multi-project templates is generating unique guids and sharing them across sub projects. Unfortunately this means the project GUIDs for the `Host.*` projects are reused across projects. This hasn't caused any problems but if multiple of these project templates were added to the same solution, this might cause issues.
 
## The Web UI
The Single Page Application templates (SPA) use NPM (and JSPM) for resolving dependencies on create. This action (in VS 2015 and VS 2013 with specific extensions) of `npm install` is automatically kicked off. The same tokenization for the `*.cs` files can be used in any file across the project, so JS/TS or *.json config files can use the `$safeprojectname$` if needed. 

#### Tool dependencies
Early on in ServiceStackVS, NodeJS, NPM and other tooling wasn't apart of VS.NET, so we have a custom Wizard to handle install detection and prompting for the users to install. Once older versions of VS.NET are deprecated, this will no longer be needed, however the same pattern might be needed to ensure those using templates have the correct prerequisites installed locally. 

For example, in the AngularSPA template, the following is configured in the `.vstemplate` file.

``` xml
  <WizardExtension>
    <Assembly>ServiceStackVS.NPMInstallerWizard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=5020d645716c0b0b</Assembly>
    <FullClassName>ServiceStackVS.NPMInstallerWizard.NodeJsRequiredWizard</FullClassName>
  </WizardExtension>
  <WizardExtension>
    <Assembly>ServiceStackVS.NPMInstallerWizard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=5020d645716c0b0b</Assembly>
    <FullClassName>ServiceStackVS.NPMInstallerWizard.GitRequiredWizard</FullClassName>
  </WizardExtension>
```

The `NPMInstallerWizard` would be where these extensions for prerequisite detection should probably live.



