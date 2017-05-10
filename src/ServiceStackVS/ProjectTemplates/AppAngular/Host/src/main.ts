import "material-design-lite/dist/material.deep_purple-pink.min.css";
import "./app.css";
import "material-design-lite/material.min.js";

import 'zone.js';
import 'reflect-metadata';

import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';

import { MainModule } from './main.module';

import { enableProdMode } from '@angular/core';
if (process.env.NODE_ENV === 'production') {
  enableProdMode();
}

platformBrowserDynamic().bootstrapModule(MainModule);
