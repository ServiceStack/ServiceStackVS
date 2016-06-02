# Aurelia Template

![](https://raw.githubusercontent.com/ServiceStack/Assets/master/img/servicestackvs/typescript-aurelia-jspm-banner.png)

In the latest version of ServiceStackVS, we've added an [Aurelia](http://aurelia.io/) template to help you get started with Aurelia, [TypeScript](https://www.typescriptlang.org/), [JSPM](http://jspm.io/) and [Gulp](http://gulpjs.com/).

> Aurelia is a JavaScript client framework for mobile, desktop and web leveraging simple conventions and empowering creativity.

## The Aurelia Experience
Aurelia's strong focus on simple un-obtrusive conventions to produce a great developer experience. ServiceStack shares this same focus on developer experience to enable developers to concentrate on what's important to their solution. 

#### Technologies used
This template has a lot of similarities to the other ServiceStackVS templates but with all the configuration to help [kick start your Typescript React](https://github.com/ServiceStackApps/typescript-redux#start-typescripting) application. NPM's package.json is setup to include all the parts you'll need including:

- [Typings](https://github.com/typings/typings) - For ambient TypeScript definitions
- [JSPM](http://jspm.io/) - To manage your application dependencies and give you a smooth workflow
- [Gulp](http://gulpjs.com/) - Grunt has been removed in favor of a Gulp only approach to simplify build setup.

Just like our other TypeScript/JSPM/Gulp templates, we use Gulp for staging and deploying (via MSDeploy) to allow for simple deployments to an existing IIS server. The following tasks are setup when using the template.

- `default` - This is the default task that builds and stages your application.
- `00-update-deps-js` - This task is to regenerate `deps.lib.js` from `deps.tsx` which is used to [reduce how much HTTP requests are done each time you make a change](https://github.com/ServiceStackApps/typescript-redux#preloading-dependencies).
- `01-package-server` - build and stage the **server** components of your application.
- `02-package-client` - build and stage the **client** side resources of your application.
- `03-deploy-app` - This deploys your application using `msdeploy` and `config.json` found in `wwwroot_build/publish`. 
- `package-and-deploy` - This task is the whole process of **building, staging and deploying** your application.

#### Template structure
To try get the most of using Aurelia as your client application, this template is structured in a way that should grow well with your application as well as following some of the known patterns Aurelia encourages. Within the `src` directory, the application is broken up into 3 major parts.

- [Entry point and application configuration](#entry-point-and-application-configuration)
- Models and Views
- Reusable common `resources`


## [Entry point and application configuration](http://aurelia.io/docs.html#/aurelia/framework/1.0.0-beta.1.2.4/doc/article/app-configuration-and-startup)
To start your Aurelia application, we are using the `aurelia-bootstrapper` module. The usage of the bootstrapper is declared in the `index.html` file.

``` html
<body aurelia-app="src/main">
...
<script>
    System.import('aurelia-bootstrapper');
</script>
</body>
``` 

This will kick off our application by looking into `src/main.ts` and specifically for the `configure(aurelia: Aurelia)` function for out application initialization. The template is using the [`standardConfiguration`](http://aurelia.io/docs.html#/aurelia/framework/1.0.0-beta.1.2.4/doc/article/app-configuration-and-startup), the `developmentLogging` and our own internal "Feature" for reusable common resources in the `resources` directory.

``` typescript
import {Aurelia} from 'aurelia-framework';

export function configure(aurelia: Aurelia) {
    aurelia.use
        .standardConfiguration()
        .feature('src/resources')
        .developmentLogging();

    aurelia.start().then(x => x.setRoot('src/app'));
}
```

Once we've declared our application configuration, we need to `start()` the application at a specific root. In the case of the template, our `app.ts` is where we want our entry point of our application. For those unfamiliar with Aurelia, this is where we start to see Aurelia's conventions of using plain TypeScript classes and matching `app.ts` and `app.html` file names which is also used for views. 

The `app.ts` is just a class with a `configureRouter` function for client side routing, it doesn't `extend` or `implement` and special Aurelia base class or interface. 

``` app.ts
import {Router} from 'aurelia-router';

export class App {
    router: Router;
    configureRouter(config, router: Router) {
        config.title = 'Aurelia';
        config.map([
            { route: ['', 'home'], name: 'home', moduleId: './views/home', nav: true, title: 'Home' },
            { route: ['/view1', 'view1'], name: 'view1', moduleId: './views/view1', nav: true, title: 'View 1' },
            { route: ['/view2', 'view2'], name: 'view2', moduleId: './views/view2', nav: true, title: 'View 2' }
        ]);
        this.router = router;
    }
}
```

The `config` being passed as the first argument is being setup with a `title` property. which sets the page `<title>`, and a `map` which the router can use to direct different client paths to different views with a sub title. For example, when the client navigates to `/#/view1` the `<title>` of the page is `Aurelia | View 1`.

The `app.html` provides the related UI for `app.ts` which the template is using as a landing page. The router uses the known navigation paths to generate the different links using Aurelia's templating syntax.

``` html
	<ul class="nav navbar-nav">
	    <li repeat.for="row of router.navigation" class="${row.isActive ? 'active' : ''}">
	        <a href.bind="row.href">
	            ${row.title}
	        </a>
	    </li>
	</ul>
```

The replace the body with the different views associated with each route, `app.html` has also declared a `<router-view>` element.

``` html
    <div class="container">
        <router-view></router-view>
    </div>
```

## Models and Views
Like all MV* UI frameworks, we need a way to show data (our model) on a page (our view). Aurelia takes a simple default convention approach to match our models and views which is the the name of the file. In the Aurelia template, we have 3 example views, `home`, `view1` and `view2`.

> By default they are separated into a `Views` folder, however this is *not* required or part of any convention, just a simple way to group them. 



#### Known Issues 
Aurelia itself it still in beta and uses a lot of modern web developer tools which are still being worked on and updated by various vendors. Currently there are 2 known issues to a smooth workflow in Visual Studio that are easily worked around and/or a fix will soon be released.

##### Aurelia bundler error when deploying via Gulp

Due to the now 2-year-old version of NodeJS that still ships in Visual Studio External Web Tools, version `0.10.31`, the `aurelia-bunlder` won't work from Task Runner Explorer from Visual Studio. To resolve this issue, install the latest version of node and change the order of the paths used by Visual Studio to resolve External Web Tools. If NodeJS is installed on the `PATH`, it should be listed first like below.

![](https://raw.githubusercontent.com/ServiceStack/Assets/master/img/servicestackvs/aurelia-workaround-1.png)

##### TypeScript decorators showing as errors when used with ReSharper

This issue only effects those using ReSharper with Visual Studio. The current latest version of ReSharper still has an outstanding issue which is to be resolved in 2016.2 and the fix is set to release in ReSharper 2016.2 EAP 3.

To resolve the issue, simply disable JavaScript and TypeScript from ReSharper options menu which can be accessed by `ReSharper -> Options -> Products & Features -> JavaScript and TypeScript`.

Once this is diabled, these highlighted errors will disapear and TypeScript will continue to compile to JavaScript.

> Note, this template requires the TypeScript Visual Studio Extension that uses at least TypeScript 1.8+