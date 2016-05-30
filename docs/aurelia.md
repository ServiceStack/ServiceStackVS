# Aurelia Template

![](https://raw.githubusercontent.com/ServiceStack/Assets/master/img/servicestackvs/typescript-aurelia-jspm-banner.png)

In the latest version of ServiceStackVS, we've added an [Aurelia](http://aurelia.io/) template to help you get started with Aurelia, [TypeScript](https://www.typescriptlang.org/), [JSPM](http://jspm.io/) and [Gulp](http://gulpjs.com/).

> Aurelia is a JavaScript client framework for mobile, desktop and web leveraging simple conventions and empowering creativity.

## The Aurelia Experience
Aurelia's strong focus on simple un-obtrusive conventions to produce a great developer experience. ServiceStack shares this same focus on developer experience to enable developers to *enjoy* creating their applications and leaves developers to concentrate on what's important to their solution.

#### Known Issues 
Aurelia itself it still in beta and uses a lot of modern web developer tools which are still being worked on and updated by various vendors. Currently there are 2 known issues to a smooth workflow in Visual Studio that are easily worked around and/or a fix will soon be released.

##### Aurelia bundler error when deploying via Gulp

Due to the now 2-year-old version of NodeJS that still ships in Visual Studio External Web Tools, version `0.10.31`, the `aurelia-bunlder` won't work from Task Runner Explorer from Visual Studio 2013/2015. To resolve this issue, install the latest version of node and change the order of the paths used by Visual Studio to resolve External Web Tools. If NodeJS is installed on the `PATH`, it should be listed first like below.

![](https://raw.githubusercontent.com/ServiceStack/Assets/master/img/servicestackvs/aurelia-workaround-1.png)

##### TypeScript decorators showing as errors when used with ReSharper

This issue only effects those using ReSharper with Visual Studio. The current latest version of ReSharper still has an outstanding issue which is to be resolved in 2016.2 which is currently in Early Access Preview (EAP).

To resolve the issue, simply disable JavaScript and TypeScript from ReSharper options menu which can be accessed by `ReSharper -> Options -> Products & Features -> JavaScript and TypeScript`.

Once this is diabled, these highlighted errors will disapear and TypeScript will continue to compile to JavaScript.

> Note, this template requires the TypeScript Visual Studio Extension that uses at least TypeScript 1.8+