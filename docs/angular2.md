## Angular2 Single Page App template

![](https://raw.githubusercontent.com/ServiceStack/docs/master/docs/images/ssvs/typescript-angular2-jspm-banner.png)

The new Angular2 template closely follows the existing npm-based TypeScript / JSPM / Gulp technology stack 
solidified in our other SPA templates with the main difference being that it's based on the 
[Material Design Lite](https://getmdl.io/) theme:

![](https://raw.githubusercontent.com/ServiceStack/docs/master/docs/images/ssvs/angular2/screenshot-surface-96res.png)

Whereas all other Single Page App templates are based on Bootstrap. **Material Design Lite** was used for Angular2 
as it was the more natural and popular fit given they're both actively developed and maintained by Google. 

### Modular Layout

The Angular2 template takes advantage of Angular2's modular architecture with a physical structure optimal for 
small-to-medium sized projects where its modular layout is compartmentalized into multiple independent sub modules. 
Each sub module can then be further divided into "feature folders" encapsulating the components required to render 
each individual page so they're able to be maintained separately:

    src/
        main.module.ts          - Register all Modules used in entire App
        app.ts                  - Container component for entire App
        dtos.ts                 - TypeScript Server DTOs
        shared/                 - Shared Components available to entire App
            header.ts
            footer.ts
        modules/                - All Sub Modules used to construct App
            app/                - Main App Module
                home/           - Feature Folders
                    home.ts     - The Home Page Tab Contents
                    hello.html  - Hello Sub Component HTML Layout
                    hello.ts    - Hello Sub Component TypeScript
                products/
                technology/
                app.module.ts   - All Dependencies + Routes used in Module

### TypeScript Client Integration

Like all SPA Templates, Angular2 includes deep integration with ServiceStack with a TypeScript Service client 
that's preconfigured to make end-to-end typed API calls out-of-the-box:

```ts
var client = new JsonServiceClient('/');

var req = new Hello();
req.name = newValue;
client.get(req).then(r => {
    this.result = r.result
});
```

The TypeScript DTOs can then be easily updated by right-clicking on `dtos.ts` to 
[Update the TypeScript reference](http://docs.servicestack.net/virtual-file-system#updating-html-and-metadata-page-templates)
and generate the TypeScript DTOs for any new Services.
