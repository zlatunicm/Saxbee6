//pzajedniceController

(function () {

    "use strict";

    angular.module("apppcelinjak")
        .controller("zajedniceController", zajedniceController);

    function zajedniceController($http, $stateParams) {
        var vm = this;
        vm.id = $stateParams.id;
        vm.pzajednice = [];
        vm.isBusy = true;
        vm.novazajednica = {};

        var url = "/api/pcelinjak/" + vm.id;

        $http.get(url)
            .then(function (response) {
                // success
                angular.copy(response.data, vm.pzajednice);
                
            }, function (err) {
                // failure
                vm.errorMessage = "Failed to load";
            })
            .finally(function () {
                vm.isBusy = false;
            });


        //$http({
        //    url: "/api/pcelinjak",
        //    method: "get",
        //    params: { id: $stateParams.id }
        //}).then(function (response) {
        //    vm.pzajednice = response.data;
        //})
     };
})();