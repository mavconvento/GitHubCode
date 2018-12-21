angular.module('MyApp') // extending from previously created angular module in the First Part

// Controller
.controller('MapsController', function ($scope, $interval, $location, CommonService, uiGmapIsReady) {

    //    $scope.GetQueryString = function () {
    //    var latitute = $location.$$search.latitude;
    //    var longtitude = $location.$$search.longtitude;
    //    var liberationpoint = $location.$$search.liberationpoint;
    //    var coordinates = $location.$$search.coordinates;
    //    };

    //Populate all location
    var latitute = $location.$$search.latitude;
    var longtitude = $location.$$search.longtitude;
    var liberationpoint = $location.$$search.liberationpoint;
    var coordinates = $location.$$search.coordinates;


    //this is for default map focus when load first time
    $scope.map = { center: { latitude: latitute, longitude: longtitude }, zoom: 15 }

    $scope.markers = [];
    $scope.locations = [];

    //get marker info
    $scope.markers = [];
    $scope.markers.push({
        id: 0,
        coords: { latitude: latitute, longitude: longtitude },
        title: liberationpoint,
        address: coordinates
        //image: data.data.ImagePath
    });

    //set map focus to center
    $scope.map.center.latitude = latitute;
    $scope.map.center.longitude = longtitude;


    //set map focus to center
    //    $scope.map.center.latitude = 22.590406;
    //    $scope.map.center.longitude = 88.366034;

    //Show / Hide marker on map
    $scope.windowOptions = {
        show: true
    };

})

// factory
.factory('MapsService', function ($http, $q) {
    //    var fac = {};
    //    fac.GetLocation = function (d) {
    //        return $http({
    //            url: '/Home/GetMemberSessionID',
    //            method: 'POST',
    //            data: JSON.stringify(d),
    //            headers: { 'content-type': 'application/json' }
    //        });
    //    };

});