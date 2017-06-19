////app-pcelinjak.js
//(function () {
"use strict";
var apppcelinjak = angular.module("apppcelinjak", ["ui.router", "smart-table"]);

        apppcelinjak.config(function ($stateProvider) {

            $stateProvider
                .state("home", {
                    url: "/home",
                    templateUrl: "templates/homeView.html",
                    controller: "homeController",
                    controllerAs: "ctrlHome"
                })
                .state("pcelinjak", {
                    url: "/pcelinjaci",
                    templateUrl: "templates/pcelinjakView.html",
                    controller: "pcelinjakController",
                    controllerAs: "ctrlPcelinjak"
                })
                .state("izvjesca", {
                    url: "/izvjesca",
                    templateUrl: "templates/izvjescaView.html",
                    controller: "izvjescaController",
                    controllerAs: "ctrlIzvjesca"
                })
                .state("zajednice", {
                    url: "/pcelinjaci/zajednice:id",
                    templateUrl: "templates/zajedniceView.html",
                    controller: "zajedniceController",
                    controllerAs:"vm"

                })
                .state("zajedniceDetails", {
                    url: "/pcelinjaci/zajednice/zajednica/:id",
                    templateUrl: "templates/zajedniceDetails/zajedniceDetailsView.html",
                    controller: "zajedniceDetailsController",
                    controllerAs: "ctrlDetaljiZajednice",
                    //params: { id: null }
                })
                .state("pcelinjakDetails", {
                    url: "/pcelinjaci/pcelinjak/:id",
                    templateUrl: "templates/pcelinjakDetails/pcelinjakDetailsView.html",
                    controller: "pcelinjakDetailsController",
                    controllerAs:"ctrlDetaljiPcelinjaka"
                });
          

            

        });

//})();