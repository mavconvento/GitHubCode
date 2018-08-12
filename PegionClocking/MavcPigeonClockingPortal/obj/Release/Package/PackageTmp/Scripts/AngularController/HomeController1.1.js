angular.module('MyApp') // extending from previously created angular module in the First Part
.controller('HomeController', function ($scope, $interval, HomeService, ngDialog) {
    //debugger;

    $scope.Service = HomeService;
    var intervals = 0;
    var index = 0;
    $scope.Message = "Testing Flash Message";
    $scope.IsMenu = false;
    $scope.IsLogedIn = false;
    $scope.Submitted = false;
    $scope.IsFormValid = false;
    $scope.LoginData = {
        Username: '',
        Password: ''
    };

    $scope.ViewWebsite = function (action) {
        if (action == "mobile") {
            window.location.href = "http://www.portal.mavcpigeonclocking.com";
        }
        else if (action == "pc") {
            window.location.href = "/Home/Login";
        }
    }

    $scope.SetAuthenTokens = function (vUserName, vPassword) {
        $scope.LoginData.Username = vUserName;
        $scope.LoginData.Password = vPassword;
    }

    $scope.ForgotPasswordData = { MobileNumber: '', Password: '', ActionType: '' }

    HomeService.GetAdvertisement().then(function (advertisement) { $scope.advertisement = advertisement }, function ()
    { alert('Connection Error') })

    $scope.Message = "Welcome to MAVC Pigeon Clocking System";
    $scope.IsShowProducts = false;
    
    //Check is Form Valid or Not // Here f1 is our form Name
    $scope.$watch('frmLogin.$valid', function (newVal) {
        $scope.IsFormValid = newVal;
    });

    $scope.Login = function () {
        $scope.Submitted = true;
        if ($scope.IsFormValid) {
            HomeService.ValidateUser($scope.LoginData).then(function (d) {
                var a = d.data.data;
                //alert(a.Message);
                if (a.Message != null) {
                    $scope.IsLogedIn = true;
                    window.location.href = "/Main/MyProfile";
                }
                else {
                    alert(a.Remarks);
                }
            });
        }
    };

    $scope.SendPassword = function () {
        $scope.Submitted = true;
        $scope.ForgotPasswordData.ActionType = 'ForgotPassword'

        $scope.Service.ForgotPassword($scope.ForgotPasswordData).then(function (d) {
            var a = d.data.data;
            if (a.Message != null) {
                alert(a.Message);
                window.location.href = "/Home/Login";
            }
        });
    };

    $scope.DownloadApplication = function () {
        window.location.href = "../../Application/MAVCPigeonClocking.msi";

    }

    $scope.SetPassword = function () {
        debugger;
        $scope.Submitted = true;
        $scope.ForgotPasswordData.ActionType = 'SetPassword'
        $scope.Service.ForgotPassword($scope.ForgotPasswordData).then(function (d) {
            var a = d.data.data;
            if (a.Message != null) {
                alert(a.Message);
                window.location.href = "/Home/Login";
            }
        });
    };

    $scope.ForgotPassword = function () {
        ngDialog.open({ template: 'forgotPassword',
            className: 'ngdialog-theme-default',
            controller: 'HomeController',
            scope: $scope
        });
    };

    $scope.RegisterPassword = function () {
        ngDialog.open({ template: 'setpassword',
            className: 'ngdialog-theme-default',
            controller: 'HomeController',
            scope: $scope
        });
    };

    $scope.NewtoMavcPigeonClocking = function (d) {
        window.location.href = "/Home/Registration";
    };
})
.factory('HomeService', function ($http, $q) {
    var fac = {};

    fac.ValidateUser = function (d) {
        return $http({
            url: '/Home/UserLogin',
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

    fac.GetNexusPartners = function (d) {
        return $http({
            url: '/Home/GetNexusPartners',
            method: 'POST',
            data: JSON.stringify(d),
            headers: { 'content-type': 'application/json' }
        });
    };

    fac.ForgotPassword = function (d) {
        return $http({
            url: '/Home/GetForgotPassword',
            method: 'POST',
            data: JSON.stringify(d),
            headers: { 'content-type': 'application/json' }
        });
    };

    return fac;
});
