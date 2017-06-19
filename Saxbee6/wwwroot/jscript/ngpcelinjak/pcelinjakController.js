//pcelinjakController.js

(function () {

    "use strict";
    //dohvacanje postojeceg modula
    angular.module("apppcelinjak")
        .controller("pcelinjakController", pcelinjakController);

    function pcelinjakController($http, $scope) {

        var ctrlPcelinjak = this;
      
        ctrlPcelinjak.itemsByPage = 5;
        ctrlPcelinjak.pcelinjaci = [];
        ctrlPcelinjak.errorMessage = "";

        ctrlPcelinjak.newPcelinjak = {};

        $http.get("/api/pcelinjak")
            .then(function (response) {
                //uspjeh
                angular.copy(response.data, ctrlPcelinjak.pcelinjaci);
                
            }, function (err) {
                //neuspjeh
            });


        ctrlPcelinjak.addPcelinjak = function () {

            $http.post("/api/pcelinjak", ctrlPcelinjak.newPcelinjak)
                .then(function (response) {
                    ctrlPcelinjak.pcelinjaci.push(response.data);
                    //ctrlPcelinjak.newPcelinjak = {};
                    //success
                }, function () {

                    //faillure
                });

        }





      
         
    };









      
    

})();