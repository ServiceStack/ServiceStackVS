/// <reference path="~/node_modules/karma-jasmine/lib/jasmine.js" />
"use strict";

describe('hello controller unit tests', function () {
    var $scope;

    beforeEach(function () {
        module('helloApp.controllers');
        inject(function ($rootScope) {
            $scope = $rootScope.$new();
        });
    });

    it('should have a test function that returns true', inject(function ($controller) {
        //spec body
        var myCtrl1 = $controller('helloCtrl', { $scope: $scope });
        expect(myCtrl1).toBeDefined();
        expect($scope.testFunction).toBeDefined();
        expect($scope.testFunction()).toBe(true);
    }));
});