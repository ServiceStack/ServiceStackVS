## AngularJS SPA template ##

ServiceStackVS has a new template that pulls together a number of modern techniques used for developing rich single page applications. This template provides a starting point for a single page application with use of NPM, Karma, Bower, Grunt and Gulp already setup with useful tasks. These tasks can be used from the Task Runner Explorer extension which is a preview of functionality that has said will be included in Visual Studio ‘14’. 

This template is the first iteration of a new effort that follows VS.NET's development of their new Grunt/Gulp.js support. Although it's still early, we believe this represents the future direction of more advanced Single Page web application development. We will continue to improve ServiceStackVS and follow VS.NET's progress.

### Building the front-end with Gulp and Grunt ###
As front-ends are getting more complicated, tools like Grunt and Gulp help to manage some of this complexity. Included in the template is a ‘build_Release’ Gulp task. This task does a number of things to get a release version of your application's front end built and copied to your ‘wwwroot’ folder. Once completed, the ‘wwwroot’ folder will contain a Release version of your application ready for deployment. Also included is an example task with the use of grunt-msdeploy that can be used to configure an msdeploy task to deploy your application. 

The 'build_Release' Gulp task also performs the web.config Release transformations and copies the result to the wwwroot staging folder
>You will still **need to rebuild your application** before running the gulp 'build_Release' task as it also copies your bin folder to the wwwroot folder.

### Minification ###
As a part of the ‘build_Release’ Gulp task, minification of JS and CSS files is performed and references are replaced. This is due to HTML comments around included JS and CSS source. 

    <!-- build:js js/app.min.js -->
    <script src="js/hello/controllers.js"></script>
    <script src="js/navigation/controllers.js"></script>
    <script src="js/app.js"></script>
    <!-- endbuild -->

When you add a new JS file as a part of your application, you will need to add it in between the build comment shown above. This will minify and include the new file within the resultant app.min.js.

Vendor JS/CSS is minified in the process along with your applications JS/CSS to keep things simple. This allows the use of debug JS when locally debugging as well as easily controlly versions and minification from the package.json and build_Release Gulp task.

### Karma testing ###
Also included is a Grunt task to run the example Karma tests.  These tests can also be run independently via a batch file that will watch your files for changes and re-run tests. As your AngularJS application becomes larger, more test ‘spec’ files can be added to the tests/unit folder which will be included via the provided karma.conf.js configuration.



### Debugging ###
Development iterations follow along with Visual Studio’s normal pattern when used with the built in IIS Express as it hosts the local development folder, so vendor provided JS/CSS are directly referenced from the bower_components folder.
