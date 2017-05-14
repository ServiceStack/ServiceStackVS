import {} from "aurelia-framework";

export class Home {
    name: string;
    activate(params, routeConfig, $navigationInstruction) {
        this.name = routeConfig.settings.name;
    }
}