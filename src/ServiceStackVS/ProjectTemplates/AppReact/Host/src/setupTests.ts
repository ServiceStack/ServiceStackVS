import { configure } from 'enzyme';
import * as Adapter from 'enzyme-adapter-react-16';

configure({ adapter: new Adapter() });

declare var global: any;

const packageConfig = require("../package.json");
global.BaseUrl = packageConfig["proxy"];
