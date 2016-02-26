### TypeScript React Template
To follow up Microsoft's recent [release of TypeScript 1.8 and related extensions for Visual Studio (2013 & 2015)](https://github.com/Microsoft/TypeScript/releases), ServiceStackVS now includes the simplest way to get your new TypeScript React project going with a new template that includes everything you'll need to start, iterate and deploy. 

This template has a lot of similarities to the other ServiceStackVS templates but with all the configuration to help [kick start your Typescript React](https://github.com/ServiceStackApps/typescript-redux#start-typescripting) application. NPM's package.json is setup to include all the parts you'll need including:

- [Typings](https://github.com/typings/typings) - For ambient TypeScript definitions
- [JSPM](http://jspm.io/) - To manage your application dependencies and give you a smooth workflow
- [Gulp](http://gulpjs.com/) - Grunt has been removed in favor of a Gulp only approach to simplify build setup.

We've removed the use of Grunt and Bower in this new template to help reduce the number of concepts and points of configuration, however the tasks to help you build, stage and deploy your application are very similar using just Gulp. 

- `00-update-deps-js` - This task is to regenerate deps.lib.js from deps.tsx which is used to [reduce how much HTTP requests are done each time you make a change](https://github.com/ServiceStackApps/typescript-redux#preloading-dependencies).
- `01-package-server` - Just like other templates, this is for building and staging the server components of your application.
- `02-package-client` - Again like other templates, this compiles and stages your client side resources ready for deployment.
- `03-deploy-app` - This deploys your application using `msdeploy` and `config.json` found in `wwwroot_build/publish`. 
- `default` - This is the default task that builds and stages your application.
- `package-and-deploy` - This task is the whole process of building, staging and deploying your application.

The template provides your client side application entry point in `app.tsx` and splits out HelloWorld example into the `components` directory. This is just a guide line to show separating out your application from the various components to help keep your application more maintainable.

> This new template is only supported by VS 2013 and 2015, VS 2012 can't install the required Microsoft extension to work productively in VS with TypeScript. Also although `tsconfig.json` is included, VS2013 ignores this file and only uses settings found in the projects Properties menu.