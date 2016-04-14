## React Desktop Apps (Beta) Update

[![](https://raw.githubusercontent.com/ServiceStack/Assets/master/img/servicestackvs/Squirrel-Logo.png)](https://github.com/Squirrel/Squirrel.Windows)

To improve the experience of deploying and installing of the standalone Windows application for the [React Desktop Apps template](https://github.com/ServiceStackApps/ReactChatApps), [Squirrel.Windows](https://github.com/Squirrel/Squirrel.Windows) has been incorporated so that you're application packages into a self executing installer that is setup to for quick automatic updates.

[![](https://raw.githubusercontent.com/ServiceStack/Assets/master/img/release-notes/typescript-react-jspm-banner.png)](https://github.com/ServiceStackApps/typescript-react-template/)

This template has also been updated to use TypeScript, React, JSPM and Gulp to match what we think is the best way to produce web applications. The use of TypeScript with JSPM creates a smooth developer workflow with a great language from Microsoft that is improving all the time. TypeScript with Visual Studio 2015 gives you compile on save, intellisense and build errors all within Visual Studio itself.

#### Enabling Auto Updates using GitHub
To use GitHub for your releases of your Windows application updates, we need to have source committed to an accessible GitHub project. 

A few things should be added to the **default Visual Studiom .gitignore** before we commit a new project. Ensure the following is in your .gitignore file.

```
node_modules/
jspm_packages/
wwwroot/
webdeploy.zip
```

Once you've created a project from the React Desktop Apps template, we need to change two pieces of config within the `App.config` in the **Host.AppWinForms** project, specifically `EnableAutoUpdate` to **true** and `UpdateManagerUrl` to your **GitHub project URL** (exclude the trailing slash).

``` xml
<configuration>
  ...
  <appSettings>
    <add key="EnableAutoUpdate" value="true" />
    <add key="UpdateManagerUrl" value="https://github.com/{Name}/{AppName}"/>
  </appSettings>
</configuration>
```

To package the Windows application we can use a preconfigured Gulp task called **02-package-winforms**. This will build all the required resources for your application and package them into a `Setup.exe` Windows installer. These files are located in the main project under **wwwroot_build\apps\winforms-installer**. The **Releases** folder contains all the distributables of your Windows application. 

```
MyReactApp
\_ wwwroot_build
  \_ apps
    \_ winforms-installer
      \_ Releases
        \_ MyReactApp-1.0.0.0-full.nupkg
        \_ RELEASES
        \_ Setup.exe 
```

To publish your initial version to GitHub, create a [Release in GitHub](https://help.github.com/articles/creating-releases/) and upload these 3 files in your releases folder.

![](https://raw.githubusercontent.com/ServiceStack/Assets/master/img/servicestackvs/react-desktop-apps-release1.png)

Steps to update your application, eg to 1.1, would be the following.

1. Update the version of the AppWinForms project, either directly in `Properties/AssemblyInfo.cs` or through Project properties GUI.
2. Save changes and run the `02-package-winforms` Gulp task.

![](https://raw.githubusercontent.com/ServiceStack/Assets/master/img/servicestackvs/react-desktop-gulp-squirrel-package.png)
3. Commit your changes and push them to GitHub.
4. Create a new GitHub release and include the same 3 files, plus the **delta** NuGet package. Clients running `1.0.0.0` will detect the new version and updates can be easily managed with Squirrel.Windows.

>During step 2 your new version is picked up by the Gulp task and Squirrel creates a delta NuGet package, eg `MyReactApp-1.1.0.0-delta.nupkg` which will be used for quick updates to clients on the previous version (1.0). 

![](https://raw.githubusercontent.com/ServiceStack/Assets/master/img/servicestackvs/react-desktop-apps-release2.png)

Users that have installed version `1.0.0.0` will see a prompt already setup in the template that asks to update the application. By clicking update, the `delta` of `1.1.0.0` is downloaded and applied, then the application is restarted running the newer version of the application. 

![](https://raw.githubusercontent.com/ServiceStack/Assets/master/img/servicestackvs/auto-update-preview.gif)

#### Switching to Amazon S3 for releases
If you find the GitHub approach doesn't suit your needs, Squirrel.Windows also has support for Amazon S3 or any statically hosted URL. Though the template is setup to be used with GitHub, it can be easily changed to use Amazon S3. 

The `AppGitHubUpdater` class in the AppWinForms project wraps Squirrel's `UpdateManager` to simplify correctly handling and disposing of the `UpdateManager`. To use Amazon S3, the static property `GitHubUpdater` can be changed from:

``` csharp
public static UpdateManager AppUpdateManager
{
    get
    {
        if (_updateManagerInstance != null)
        {
            return _updateManagerInstance;
        }

        var appSettings = new AppSettings();
        var updateManagerTask =
            UpdateManager.GitHubUpdateManager(appSettings.GetString("UpdateManagerUrl"));
        updateManagerTask.Wait(TimeSpan.FromMinutes(1));
        _updateManagerInstance = updateManagerTask.Result;
        return _updateManagerInstance;
    }
}
```

To:

``` csharp
public static UpdateManager AppUpdateManager
{
    get
    {
        if (_updateManagerInstance != null)
        {
            return _updateManagerInstance;
        }

        var appSettings = new AppSettings();
        _updateManagerInstance = new UpdateManager(appSettings.GetString("UpdateManagerUrl"));
        return _updateManagerInstance;
    }
}
```

The rest of the update process can stay the same. For more information on using Amazon S3 with Squirrel.Windows, [see their documentation](https://github.com/Squirrel/Squirrel.Windows/blob/master/docs/using/amazon-s3.md).

Squirrel.Windows also has a number of options for updating icons, installer gifs and other customizations. For more info, [checkout Squirrel.Windows documentation](https://github.com/Squirrel/Squirrel.Windows/tree/master/docs).