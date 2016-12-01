System.register([], function(exports_1, context_1) {
    "use strict";
    var __moduleName = context_1 && context_1.id;
    var HelloResponse, Hello;
    return {
        setters:[],
        execute: function() {
            HelloResponse = (function () {
                function HelloResponse() {
                }
                return HelloResponse;
            }());
            exports_1("HelloResponse", HelloResponse);
            Hello = (function () {
                function Hello() {
                }
                Hello.prototype.createResponse = function () { return new HelloResponse(); };
                Hello.prototype.getTypeName = function () { return "Hello"; };
                return Hello;
            }());
            exports_1("Hello", Hello);
        }
    }
});
//# sourceMappingURL=dtos.js.map