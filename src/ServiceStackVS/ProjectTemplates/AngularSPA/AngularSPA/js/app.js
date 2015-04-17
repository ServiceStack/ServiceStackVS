/* global angular */
(function () {
    "use strict";
    // Declare app level module which depends on filters, and services
    var module = angular.module('helloApp', [
        'ngRoute',
        'helloApp.controllers',
        'navigation.controllers'
    ]);

    module.config(['$routeProvider', '$locationProvider', function ($routeProvider, $locationProvider) {
        $routeProvider.when('/', { templateUrl: '/partials/hello/hello.html', controller: 'helloCtrl' });
        $routeProvider.when('/view1', { templateUrl: '/partials/partial1.html' });
        $routeProvider.when('/view2', { templateUrl: '/partials/partial2.html' });
        $routeProvider.when('/404', { templateUrl: '/partials/404.html' });
        $routeProvider.otherwise({ redirectTo: '/404' });

        $locationProvider.html5Mode(true);
    }]);

})();