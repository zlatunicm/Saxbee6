(function () {


    "use strict";
    //dohvacanje postojeceg modula
    angular.module("apppcelinjak")
        .controller("zajedniceDetailsController", zajedniceDetailsController);



    function zajedniceDetailsController($scope, $http, $stateParams) {

        //var ctrlDetaljiZajednice = this;
        //ctrlDetaljiZajednice.id = $stateParams.id;
        //ctrlDetaljiZajednice.oznaka = $stateParams.oznaka;

        $scope.id = $stateParams.id;

       
    };


})();