import { configure } from 'enzyme';
import * as Adapter from 'enzyme-adapter-react-16';

configure({ adapter: new Adapter() });

declare var global: any;

const packageConfig = require("../package.json");
global.BaseUrl = packageConfig["proxy"];

function allowLocalSelfSignedCerts(url) {
    if (url.startsWith("https://localhost") || url.startsWith("https://127.0.0.1")) {
        process.env["NODE_TLS_REJECT_UNAUTHORIZED"] = "0"; // ignore self-signed SSL errors for localhost
    }
}
allowLocalSelfSignedCerts(global.BaseUrl);

global.fetch = require('node-fetch');
console.error = function(){}; //TODO: remove when fixed https://github.com/facebook/react/issues/14769
