(function () {

    "use strict";
    //dohvacanje postojeceg modula
    angular.module("apppcelinjak")
        .controller("homeController", homeController);


    function homeController($scope) {

        $scope.message = "welcome to Cela Party"
    };


})();