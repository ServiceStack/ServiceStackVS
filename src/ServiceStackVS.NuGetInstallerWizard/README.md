# NuGet Installer VSIX Wizard
This wizard installs specific NuGet packages either by version or `latest`. Since ServiceStack packages are in lock-step with `ServiceStack.Interfaces`, we can also install multiple packages which the same version as a `RootNuGetPackage`.

This wizard is used in all ServiceStack templates since it allows the use of `latest` instead of constantly updating NuGet version numbers in the extension every ServiceStack release.

## WizardData
The NuGet Installer wizard can be used in multi or single project templates, however `rootPackage` can only be used in the multi project `vstemplate` as it dictates which version of a package for other projects to install.

For example, a multi project `vstemplate` file with a root package would declare.

``` xml
  <WizardExtension>
    <Assembly>ServiceStackVS.NuGetInstallerWizard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=5020d645716c0b0b</Assembly>
    <FullClassName>ServiceStackVS.NuGetInstallerWizard.NuGetPackageInstallerMultiProjectWizard</FullClassName>
  </WizardExtension>
  <WizardData>
    <packages>
      <package id="ServiceStack.Interfaces" version="latest" rootPackage="true" />
    </packages>
  </WizardData>
```

Child projects that specify `latest` for version or leave it out will pickup the rootPackage version. This is also to minimize the number of requests for `latest` version to the NuGet v2 servers (which are slow) and speeds up installs for creation of new projects from the template.

This wizard can also be used in individual project `vstemplate`s. The `WizardData` mimics that of the `package.config` found in projects. Simply provide the `name` and the desired `version` and it will run the NuGet install. For example.

``` xml
<WizardData>
  <NuGetPackages>
      <package id="ServiceStack" version="latest" />
      <package id="ServiceStack.Client" version="latest" />
      <package id="ServiceStack.Common" version="latest" />
      <package id="ServiceStack.Interfaces" version="latest" />
      <package id="ServiceStack.OrmLite" version="latest" />
      <package id="ServiceStack.Text" version="latest" />
      <package id="ServiceStack.Server" version="latest" />
      <package id="CefSharp.Common" version="51.0.0" />
      <package id="CefSharp.WinForms" version="51.0.0" />
      <package id="squirrel.windows" version="1.3.0" />
    </NuGetPackages>
  </WizardData>
```

> This points at NuGet v2 API and uses NuGet V2 library. This will have to be updated to support NuGet v3 if NuGet v3 is required.