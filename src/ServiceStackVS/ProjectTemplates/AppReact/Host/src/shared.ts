import { JsonServiceClient } from 'servicestack-client';

declare var global;

export var client = new JsonServiceClient(global.BaseUrl || '/');