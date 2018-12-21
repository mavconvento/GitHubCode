angular.module('MyApp') // extending from previously created angular module in the First Part
.controller('HomeTutorialsController', function ($scope, $interval, HomeService, ngDialog) {
    //debugger;
    $scope.Home = function () {
        window.location.href = "/Home/Login";

    }

    $scope.PigeonProducts = function () {
        window.location.href = "/Home/PigeonProducts";

    }

    $scope.ContactUs = function () {
        window.location.href = "/Home/ContactUs";

    }

    $scope.TextFormat = function () {
        window.location.href = "../../Application/TextFormat.docx";
    }

    $scope.OurProducts = function () {
        window.location.href = "/Home/OurProducts";

    }

    $scope.ApplicationForm = function () {
        window.location.href = "../../Application/ApplicationForm.docx";

    }

    $scope.Service = HomeService;

    $scope.ElectronicPigeonClockingDemo = function () {

        var _url = 'https://www.youtube.com/watch?v=4mvX36jvVl0';
        var tabWindowId = window.open('about:blank', '_blank');
        tabWindowId.location.href = _url;
    }

    var myVideo = document.getElementById("video1");

    $scope.playPause = function () {
                if (myVideo.paused)
                    myVideo.play();
                else
                    myVideo.pause();
    }

})
.factory('HomeService', function ($http, $q) {
    var fac = {};

    return fac;
});
