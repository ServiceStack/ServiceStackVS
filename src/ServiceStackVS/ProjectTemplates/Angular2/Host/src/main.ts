import 'zone.js';
import 'reflect-metadata';

import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';

import { MainModule } from './main.module';

//Uncomment when deploying to production
//import { enableProdMode } from '@angular/core';
//enableProdMode();

platformBrowserDynamic().bootstrapModule(MainModule);
