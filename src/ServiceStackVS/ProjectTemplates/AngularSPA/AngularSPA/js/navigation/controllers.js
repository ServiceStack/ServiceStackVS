/* global angular */
(function () {
    "use strict";
    var app = angular.module('navigation.controllers', []);

    app.controller('navigationCtrl', ['$scope', '$location',
        function ($scope, $location) {
            $scope.IsRouteActive = function (routePath) {
                return routePath === $location.path();
            };
        }
    ]);
})();
