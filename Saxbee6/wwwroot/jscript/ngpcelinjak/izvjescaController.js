(function () {

    "use strict";
    //dohvacanje postojeceg modula
    angular.module("apppcelinjak")
        .controller("izvjescaController", izvjescaController);


    function izvjescaController($scope) {

        $scope.message = "Test za izvjesca"
    };


})();