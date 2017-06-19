(function () {


    "use strict";
    //dohvacanje postojeceg modula
    angular.module("apppcelinjak")
        .controller("pcelinjakDetailsController", pcelinjakDetailsController);



    function pcelinjakDetailsController($scope, $http, $stateParams) {

        var ctrlDetaljiPcelinjaka = this;

        ctrlDetaljiPcelinjaka.id = $stateParams.id;

        ctrlDetaljiPcelinjaka.updatedPcelinjak = {};
        

        ctrlDetaljiPcelinjaka.stariPcelinjak = [];


        //za pcelinjak

        var url = "/api/pcelinjak/jedan/" + ctrlDetaljiPcelinjaka.id;

        $http.get(url)
            .then(function (response) {
                // success
                angular.copy(response.data, ctrlDetaljiPcelinjaka.stariPcelinjak);
                ctrlDetaljiPcelinjaka.updatedPcelinjak= ctrlDetaljiPcelinjaka.stariPcelinjak[0];

            }, function (err) {
                // failure
                $scope.errorMessage = "Failed to load";
            })
            .finally(function () {
                $scope.final = "true";
            });

        ctrlDetaljiPcelinjaka.updatePcelinjak = function () {
            $http({
                method: 'POST',
                url: '/api/pcelinjak/update',
                data: JSON.stringify(ctrlDetaljiPcelinjaka.updatedPcelinjak),
                headers: { 'Content-Type': 'application/JSON' }
            });
             


        }

        $scope.message = "gluvo i coravo";
    }

})();