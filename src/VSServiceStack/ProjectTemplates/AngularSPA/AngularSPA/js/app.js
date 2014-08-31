'use strict';


// Declare app level module which depends on filters, and services
angular.module('helloApp', [
  'ngRoute',
  'helloApp.controllers'
]).
config(['$routeProvider', function ($routeProvider) {
    $routeProvider.when('/', { templateUrl: 'partials/hello/hello.html', controller: 'helloCtrl' });
    $routeProvider.when('/view1', { templateUrl: 'partials/partial1.html' });
    $routeProvider.when('/view2', { templateUrl: 'partials/partial2.html' });
    $routeProvider.otherwise({ redirectTo: '/' });
}]);