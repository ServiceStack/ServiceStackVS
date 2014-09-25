## AngularJS SPA template ##

ServiceStackVS has a new template that pulls together a number of modern techniques used for developing single page applications. This template provides a starting point for a single page application with use of NPM, Karma, Bower, Grunt and Gulp already setup with some useful tasks. These tasks can be used from the [Task Runner Explorer extension](http://visualstudiogallery.msdn.microsoft.com/8e1b4368-4afb-467a-bc13-9650572db708) which is a preview of functionality that has said will be included in Visual Studio ‘14’. 

![Template Runner Explorer](https://github.com/ServiceStack/Assets/raw/master/img/servicestackvs/template-TRX.png)

This template is the first iteration of a new effort that follows VS.NET's development of their new Grunt/Gulp.js support. Although it's still early, we believe this represents the future direction of more advanced Single Page web application development. We will continue to improve ServiceStackVS and follow VS.NET's progress.

### Requirements ###
This template relies on having [Node.js installed](http://nodejs.org/). If you try to use this template without Node.js installed (ie, node.exe not found on the local machines PATH), you will be prompted to install it.

![Node.js required](https://github.com/ServiceStack/Assets/raw/master/img/servicestackvs/nodejs-required.png)

Once downloaded and installed, click continue to create your project. If any of the NPM depedencies are not installed globally, the template will install the required NPM packages as well as download the Bower depdendencies for the template. 

As soon as your project is open, all the required front-end dependencies will be ready to go. Local NPM depdendecies to run Karma, Grunt and Gulp will download asyncronously and you'll be able to see the progress inside the ServiceStackVS output window in Visual Studio.

![](https://github.com/ServiceStack/Assets/raw/master/img/servicestackvs/npm-install-on-create.png)

### Managing front-end dependencies ###
To help add and install Bower and NPM dependencies, ServiceStackVS watches bower.json and package.json for changes and will run the appropriate install whenever these files are updated.

![NPM install performed on save](https://github.com/ServiceStack/Assets/raw/master/img/servicestackvs/angular-spa-package-demo.gif)

A great extension to help find names and versions of these dependencies is the [Package Intellisense extension](http://visualstudiogallery.msdn.microsoft.com/65748cdb-4087-497e-a394-2e3449c8e61e).

### Debugging ###
Development iterations follow along with Visual Studio’s normal pattern when used with the built in IIS Express as it hosts the local development folder, so vendor provided JS/CSS are directly referenced from the bower_components folder.

### Building the front-end with Gulp and Grunt ###
As front-ends are getting more complicated, tools like Grunt and Gulp help to manage some of this complexity. Included in the template is a ‘build_Release’ Gulp task. This task does a number of things to get a release version of your application's front end built and copied to your ‘wwwroot’ folder. Once completed, the ‘wwwroot’ folder will contain a Release version of your application ready for deployment.
>If you have updated any **C# code, you will need to use MSBUILD/Visual Studio to update your DLLs** before running the gulp 'build_Release' task as it **also copies your bin folder to the wwwroot folder**.

![Build app using Gulp](https://github.com/ServiceStack/Assets/raw/master/img/servicestackvs/angular-spa-build-app-demo.gif)

The resultant wwwroot folder has all the required files to run your application.

![](https://github.com/ServiceStack/Assets/raw/master/img/servicestackvs/angular-spa-wwwroot-output.png)

Also included is an example task with the use of grunt-msdeploy that can be used to configure an msdeploy task to deploy your application from the Task Runner Explorer.


### Minification ###
As a part of the ‘build_Release’ Gulp task, minification of JS and CSS is performed and references are replaced. This is due to HTML comments around included JS and CSS source. 

    <!-- build:js js/app.min.js -->
    <script src="js/hello/controllers.js"></script>
    <script src="js/navigation/controllers.js"></script>
    <script src="js/app.js"></script>
    <!-- endbuild -->

When you add a new JS file as a part of your application, you will need to add it in between the build comment shown above. This will minify and include the new file within the resultant app.min.js.

Vendor JS/CSS is minified in the process along with your applications JS/CSS to keep things simple. This allows the use of debug JS when locally debugging as well as easily controlly versions and minification from the package.json and build_Release Gulp task.

### Karma testing ###
Also included is a Grunt task to run the example Karma tests.  These tests can also be run independently via a batch file that will watch your files for changes and re-run tests. As your AngularJS application becomes larger, more test ‘spec’ files can be added to the tests/unit folder which will be included via the provided karma.conf.js configuration.

![](https://github.com/ServiceStack/Assets/raw/master/img/servicestackvs/karma-TRX.png)

### Visual Studio 2012 ###
If you are not running on Visual Studio 2013, we have included two batch files to launch a test watcher and run 'gulp build_Release'. All these Gulp/Grunt tasks can be run from the command-line as Task Runner Explorer does not support Visual Studio 2012.



