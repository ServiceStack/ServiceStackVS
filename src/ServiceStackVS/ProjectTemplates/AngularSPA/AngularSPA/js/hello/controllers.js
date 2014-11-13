var app = angular.module('helloApp.controllers', []);

app.controller('helloCtrl', ['$scope', '$http',
        function ($scope, $http) {
            $scope.$watch('name', function () {
                if ($scope.name) {
                    $http.get('/hello/' + $scope.name)
                        .success(function (response) {
                            $scope.helloResult = response.Result;
                        });
                }
            });

            $scope.testFunction = function() {
                return true;
            }
        }
]);