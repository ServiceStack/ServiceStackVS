import { JsonServiceClient } from "servicestack-client";

declare var global; //populated from package.json/jest

export var client = new JsonServiceClient(global.BaseUrl || '/');
