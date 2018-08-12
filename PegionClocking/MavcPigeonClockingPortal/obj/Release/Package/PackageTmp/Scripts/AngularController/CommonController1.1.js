angular.module('MyApp') // extending from previously created angular module in the First Part

// Controller
.controller('CommonController', function ($scope, $interval, $location, CommonService) {
    var location = $location.$$absUrl;
    $scope.IsShowMenu = false;
    $scope.IsReady = false;
    var intervals = 0;
    var index = 0;

    CommonService.GetAdvertisement().then(function (advertisement) { $scope.advertisement = advertisement }, function ()
    { alert('Connection Error') })

    $scope.Message = "";
    $scope.IsShowProducts = false;
    $interval(function () {
        //debugger;
        var data = $scope.advertisement;
        var advertisementCount = data.length;

        if (advertisementCount > 0) {

        }


        intervals++;
        if (intervals === 5) {
            $scope.IsShowProducts = false;
            $scope.Message = data[index].description;
            $scope.URL = "";
            if (data[index].isProduct == true) {
                $scope.URL = "/Images/Products/" + data[index].imageName;
                $scope.IsShowProducts = true;
            }
            index++;

            if (index == advertisementCount) index = 0;
            intervals = 0;
        }
    }, 1000);

    CommonService.GetCurrentLogin().then(function (d) {
        $scope.UserName = d.UserName;
        $scope.IsShowSubMemberSection = d.IsShowSubMemberSection;
        $scope.SubMemberName = d.SubMemberName;
    }, function ()
    { alert('Connection Error') })


    CommonService.GetMemberSessionID().then(function (d) {

        $scope.MemberSessionID = d.data.MemberSessionID;
        if (location.indexOf('Login') > -1) {
            return;
        }
        if (d.data.MemberSessionID != "") {
            $scope.IsShowMenu = true;
            if (location.indexOf('Maps') > -1) $scope.IsShowMenu = false;
            CommonService.GetMainMenuItems().then(function (MainMenuItems) { $scope.MainMenuItems = MainMenuItems }, function ()
            { alert('error while fetching Menu Items news from server') })
        }

    });

    $scope.Logout = function () {
        window.location.href = "/Home/Login";
    };

    $scope.getClass = function (path) {
        if (location.indexOf(path) > -1) {
            if (path != "") return 'current_menu_item';
        } else {
            return 'inactive_menu_item';
        }
    };

})

// factory
.factory('CommonService', function ($http, $q) {
    var fac = {};
    fac.GetMemberSessionID = function (d) {
        return $http({
            url: '/Home/GetMemberSessionID',
            method: 'POST',
            data: JSON.stringify(d),
            headers: { 'content-type': 'application/json' }
        });
    };

    fac.GetAdvertisement = function () {
        var deferred = $q.defer();
        // Initiates the AJAX call
        $http({ method: 'GET', url: '/Home/GetAdvertisement' }).success(deferred.resolve).error(deferred.reject);
        // Returns the promise - Contains result once request completes
        return deferred.promise;
    };

    fac.GetMainMenuItems = function () {
        var deferred = $q.defer();
        // Initiates the AJAX call
        $http({ method: 'GET', url: '/Home/GetMenus' }).success(deferred.resolve).error(deferred.reject);
        // Returns the promise - Contains result once request completes
        return deferred.promise;
    };

    fac.GetCurrentLogin = function () {
        var deferred = $q.defer();
        // Initiates the AJAX call
        $http({ method: 'POST', url: '/Home/GetCurrentLoginName' }).success(deferred.resolve).error(deferred.reject);
        // Returns the promise - Contains result once request completes
        return deferred.promise;
    };
    return fac;
});