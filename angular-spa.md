## AngularJS Single Page App Template

**AngularJS App** is a new template in [ServiceStackVS](https://github.com/ServiceStack/ServiceStack/wiki/Creating-your-first-project) that provides a modern opinionated web technology stack for developing rich Single Page Apps with [AngularJS](https://angularjs.org/) and ServiceStack. 

### The trend towards a JavaScript-based Client Build system 

The future of modern client web development has been moving towards a pure JavaScript environment for client HTML/JS/CSS development, we've noticed the benefits of this approach years ago when we developed our [node.js powered Bundler](https://github.com/ServiceStack/Bundler) taking advantage of [node.js](http://nodejs.org/) rich ecosystem for all our bundling, minification and Web DSL needs. As this is also the trend we see with web development in VS.NET going visible by the recent preview of [Grunt and Gulp.js integration in Visual Studio](http://www.hanselman.com/blog/IntroducingGulpGruntBowerAndNpmSupportForVisualStudio.aspx), we're confident in promoting this approach for the development of large, JavaScript-heavy Web Apps. 

This template marks our first iteration of this effort that we intend to continually improve and closely follow VS.NET's Grunt/Gulp.js integration so our automation tasks can be run directly from VS.NET UI Task Runners. In addition, this template also provides `.bat` files for running high-level grunt tasks so it also enables a good experience even without VS.NET's [Task Runner Explorer extension](http://visualstudiogallery.msdn.microsoft.com/8e1b4368-4afb-467a-bc13-9650572db708) (a preview of what will be available in VS 2014) which can be quickly run with a  [keyboard shortcut for running external commands](https://github.com/ServiceStack/Bundler#create-an-external-tool-inside-vsnet) in VS.NET.

![Template Runner Explorer](https://github.com/ServiceStack/Assets/raw/master/img/servicestackvs/trx-tasks.png)

### Technologies used

This templates leverages a number of best-in-class libraries to managing client web app development:

 - [Karma](http://karma-runner.github.io/0.12/index.html) for UI and JavaScript Unit testing
 - [Gulp](http://gulpjs.com/) used by Grunt to do the heavy-lifting bundling and minification
 - [NPM](https://www.npmjs.org/) to manage node.js dependencies (grunt, gulp, etc)

A lot this functionality is pre-configured and working out-of-the-box encapsulated within the high-level Grunt Tasks below:

 - **[01-run-tests](https://github.com/ServiceStack/ServiceStackVS/blob/master/angular-spa.md#01-run-tests)** - Runs Karma JavaScript Unit Tests
 - **[02-package-server](https://github.com/ServiceStack/ServiceStackVS/blob/master/angular-spa.md#02-package-server)** - Uses msbuild to build the application and copies server artefacts to `/wwwroot`
 - **[03-package-client](https://github.com/ServiceStack/ServiceStackVS/blob/master/angular-spa.md#03-package-client)** - Optimizes and packages the client artefacts for deployment in `/wwwroot`
 - **[04-deploy-app](https://github.com/ServiceStack/ServiceStackVS/blob/master/angular-spa.md#04-deploy-app)** - Uses MS WebDeploy and `/wwwroot_buld/publish/config.json` to deploy app to specified server

### Requirements

This template relies on having [Node.js installed](http://nodejs.org/). If you try to use this template without node.js installed (ie, node.exe not found on the local machines PATH), you will be prompted to install it.

Once downloaded and installed, click `Continue` to create your project. If any of the NPM dependencies are not installed globally, the template will install the required NPM dependencies for the template. 

![](https://github.com/ServiceStack/Assets/raw/master/img/servicestackvs/git-setup.png)

As soon as your project is open, all the required front-end dependencies will be ready to go. Local NPM dependencies to run Karma and Gulp will download asynchronously and you'll be able to see the progress inside the ServiceStackVS output window in Visual Studio.
> ServiceStackVS introduces auto install dependencies for 2012 but falls back to default Visual Studio behavior for 2013/2015.

![](https://github.com/ServiceStack/Assets/raw/master/img/servicestackvs/npm-install-on-create.png)

**For Visual Studio 2012 developers**, we have included shortcuts to the 4 main grunt tasks using batch files in the `wwwroot_build` folder. This is due to the [Task Runner Explorer extension](http://visualstudiogallery.msdn.microsoft.com/8e1b4368-4afb-467a-bc13-9650572db708) only supporting Visual Studio 2013 Update 3 and above.

### Managing front-end dependencies

To help add and install NPM dependencies, ServiceStackVS watches `package.json` for changes and will run the appropriate install whenever these files are updated.

![NPM install performed on save](https://github.com/ServiceStack/Assets/raw/master/img/servicestackvs/angular-spa-package-demo.gif)

A great extension to help find names and versions of these dependencies is the [Package Intellisense extension](http://visualstudiogallery.msdn.microsoft.com/65748cdb-4087-497e-a394-2e3449c8e61e).

### Debugging and Intellisense

Development iterations follow along with Visual Studio’s normal pattern when used with the built in IIS Express as it hosts the local development folder, so vendor provided JS/CSS are directly referenced from the `/node_modules` folder.

Included in the template is the `_references.js` that enables CSS/JS intellisense for all the included NPM components. It is included in the default location of `/Scripts/_references.js` so that it should work by default when creating a new application.

### Building with Gulp

As front-ends are getting more complicated, tools like Gulp help to fill the gaps left out of the MSBuild cycle. Included in the template is a series of 4 tasks to test, package and deploy your application from Gulp. 

#### The `wwwroot` folder

This folder is where your application is **packaged to ready to be deployed**. It contains the result of the 'package' Gulp tasks which take care of things like minification and updating the associated references. It also contains all the required server components like the 'bin', Global.asax and web.config. 

![wwwroot folder output](https://github.com/ServiceStack/Assets/raw/master/img/servicestackvs/angular-spa-wwwroot-output.png)

#### Karma tests

This task runs your tests configured in the `karma.conf.js` file. The template by default runs the tests once using **PhantomJS** as the browser. If you want to have Karma running and watching your files as you code, this can be changed by switching the `singleRun` flag in the Grunt configuration:

    karma: {
            unit: {
                configFile: 'tests/karma.conf.js',
                singleRun: false,
                browsers: ['Chrome'],
                logLevel: 'ERROR'
            }
        }


#### 01-package-server

To keep the packaging of your server self-contained within Grunt, this task performs the following tasks to get all the required server components staged in the `wwwroot` folder:

 - Restore NuGet packages
 - Release build
 - Clean server files in wwwroot folder
 - Copy server required files

One of these server files included, is the `appsettings.txt`. This can be used in deployment for overriding any development App Settings in the `Web.config` and read them in using ServiceStacks `AppSettings` property in the `AppHost`:

```csharp
SetConfig(new HostConfig {
    DebugMode = AppSettings.Get("DebugMode", false)
});
```

If `appsettings.txt` exists in the root directory it will be used instead of the default Web.Config's `AppSettings`: 

```csharp
var customSettings = new FileInfo(@"~/appsettings.txt".MapHostAbsolutePath());
AppSettings = customSettings.Exists
    ? (IAppSettings)new TextFileSettings(customSettings.FullName, " ")
    : new AppSettings();
```

As an example the `appsettings.txt` contains a single `DebugMode` setting:

# Release App Settings

    DebugMode false

The `appsettings.txt` file is located in the `wwwroot_build/deploy` folder by default which is copied as a part of the `02-package-server` task.

#### 02-package-client

This task is also separated out to allow updating, and if required deployment, to just the client side resources. It cleans the client side related files in the wwwroot folder, bundles and copies the new resources. The bundling searches for assets in the index.html file and follows build comments to minify and replace references. This enables simple use of debug JS files whilst still having control how our resources minify.

```html
<!-- build:js lib/js/angular.min.js -->
<script src="node_modules/angular/angular.js"></script>
<script src="node_modules/angular-route/angular-route.js"></script>
<!-- endbuild -->
<!-- build:js js/app.min.js -->
<script src="js/hello/controllers.js"></script>
<script src="js/navigation/controllers.js"></script>
<script src="js/app.js"></script>
<!-- endbuild -->
```

When creating new JS files for your application, they should be added in the `build:js js/app.min.js` comments shown above.

Vendor resources from NPM are also minified here to keep deployment simple and straight forward. If it makes more sense to use CDN resources, these build comments can easily be replaced. The above example results in the following two script includes.

```html
<script src="lib/js/angular.min.js"></script>
<script src="js/app.min.js"></script>
```

If you want to use a CDN resource when your application is deployed but use node module components locally, `build` can be changed to `htmlbuild` specifying your own key after `:`. For example, if you want to include jQuery via a CDN when your application is deployed, the orignal

```html
<!-- build:js lib/js/jquery.min.js -->
<script src="node_modules/jquery/dist/jquery.js"></script>
<!-- endbuild -->
```

Would be changed to 
```html
<!-- htmlbuild:jqueryCdn -->
<script src="node_modules/jquery/dist/jquery.js"></script>
<!-- endbuild -->
```

To specify the URL or what should be added in the `htmlbuild` block at deploy time, just update the htmlBuild task assosiated with the `jqueryCdn` key. Eg,
``` JavaScript
.pipe(htmlBuild({
    jqueryCdn: function (block) {
        pipeTemplate(block, '<script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/1.11.3/jquery.min.js"></script>');
    })
}))
```

#### 03-deploy-app

To give developers a starting point for deployment, we have included the use of a **gulp-msdeploy** task that can deploy to an IIS server with Web deploy. Config for the deployment, eg the IIS Server address, application name, username and password is located in the `/wwwroot_build/publish/config.js`. 

    {
        "iisApp": "YourAppName",
        "serverAddress": "deploy-server.example.com",
        "userName": "{WebDeployUserName}",
        "password" : "{WebDeployPassword}"
    }

If you are using **Github's default Visual Studio ignore, this file will not be included in source control** due to the default rule of `publish/` to be ignored. You should check your Git Repository `.gitignore` rules before committing any potentially sensitive information into public source control.

This task shows a quick way of updating your development server quickly after making changes to your application. For more information on use web-deploy using either Grunt or just Visual Studio publish, see **[Simple Deployments to AWS with WebDeploy](https://github.com/ServiceStack/ServiceStack/wiki/Simple-Deployments-to-AWS-with-WebDeploy#deploy-using-grunt)**.

#### Main project structure

    /css
        Application specific CSS files
    /img
        Image resources
    /js
        Application JS
    /partials
        AngularJS templates
    /Scripts
        Only here to enable intellisense for CSS and JS libraries by default
    /tests
        Karma config
        /unit
            Karma spec files
    /wwwroot
        Output directory
    /wwwroot_build
        Gulp shortcuts, build and deploy files
    AppHost.cs
    Global.asax
    gulpfile.js
    index.html
    package.json
    packages.config
    web.config

This project structure includes examples of a lot of the different tasks that will have to be done while building a single page application to guide developers as their application grows. The AngularJS side is largely influenced by various incarnations of the angular-seed project whilst still be contained within a VS project.

### Feedback Welcomed!

The AngularJS App template is trying to give a good starting point for developing a single page application within Visual Studio. Please raise any issues in the [Issues List](https://github.com/ServiceStack/Issues) or submit new [feature requests in our UserVoice](http://servicestack.uservoice.com/forums/176786-feature-requests).
