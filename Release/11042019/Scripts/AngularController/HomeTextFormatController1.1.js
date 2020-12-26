angular.module('MyApp') // extending from previously created angular module in the First Part
.controller('HomeTextFormatController', function ($scope, $interval, HomeService, ngDialog) {

    $scope.Home = function () {
        window.location.href = "/Home/Login";

    }

    $scope.PigeonProducts = function () {
        window.location.href = "/Home/PigeonProducts";

    }

    $scope.OurProducts = function () {
        window.location.href = "/Home/OurProducts";

    }

    $scope.ContactUs = function () {
        window.location.href = "/Home/ContactUs";

    }

    $scope.Tutorials = function () {
        window.location.href = "/Home/Tutorials";

    }

    $scope.ApplicationForm = function () {
        window.location.href = "../../Application/ApplicationForm.docx";

    }

})
.factory('HomeService', function ($http, $q) {
    var fac = {};

    return fac;
});
