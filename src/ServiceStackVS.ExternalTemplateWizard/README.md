# External Template VSIX Wizard
This IWizard enables your custom template to have files outside of the *proj files and still add them to the file system during the creation of the template with tokenization.

This was initially used to support the React Desktop Apps template which required an additional solution to be created when opening/building the project for Xamarin.Mac as at the time there wasn't another way to support building Xamarin.Mac.

## WizardData
The `ExternalTemplateWizard` should be used in a multi-project `*.vstemplate` definition and is designed to create an additional solution and project. For example,

``` xml
<WizardExtension>
    <Assembly>ServiceStackVS.ExternalTemplateWizard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b5de165d076e49f5</Assembly>
    <FullClassName>ServiceStackVS.ExternalTemplateWizard.ExternalTemplateWizard</FullClassName>
  </WizardExtension>
  <WizardData>
    ...
    <ExternalTemplate name="ReactJSMac" projFile="ReactJSMac.csproj" solutionFile="ReactJSMac.sln"
                      safeProjectNameReplace="$safeprojectname$.AppMac"
                      outputSolutionName="$saferootprojectname$Mac.sln" 
                      outputProjectName="$saferootprojectname$.AppMac.csproj"
                      >
      <Files>
        <File name="AppDelegate.cs" />
        <File name="AppDelegate.designer.cs" />
        <File name="AppHost.cs" />
        <File name="Info.plist" />
        <File name="Program.cs" />
        <File name="MainMenu.xib" />
        <File name="MainWindow.cs" />
        <File name="MainWindow.designer.cs" />
        <File name="MainWindow.xib" />
        <File name="MainWindowController.cs" />
        <File name="platform.css" />
        <File name="platform.js" />
        <File name="packages.config" />
        <File name="logo.icns" dest="Resources"/>
      </Files>
    </ExternalTemplate>
  </WizardData>
```

The `Files` xml is to indicate which files should be processed for token replacement.

