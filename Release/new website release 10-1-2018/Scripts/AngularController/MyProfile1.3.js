angular.module('MyApp') // extending from previously created angular module in the First Part
.controller('MyProfileController', function ($scope, $rootScope, $http, $filter, MainService, ngDialog, ngTableParams) {

    $scope.MyProfileFilter = {
        MobileNumber: '',
        PinNumber: '',
        MobileNumberSelected: '',
        ToMobileNumber: '',
        PasaloadAmount: '',
        ClubID: ''
    };

    $scope.GotoMyDistance = function (value) {
        //debugger;
        //alert(value.MemberIdNo);
        var membeid = value.MemberIdNo.replace('#','|');
        window.location.href = "/Main/MemberDistance?ClubID=" + value.ClubID + "&MemberIDNo=" + membeid;
    }

    $scope.UnregMobileNumber = function (mobile, clubID) {
        $scope.MyProfileFilter.MobileNumberSelected = mobile;
        $scope.MyProfileFilter.ClubID = clubID;
        ngDialog.open({ template: 'unreg',
            className: 'ngdialog-theme-default',
            controller: 'MyProfileController',
            closeByDocument: true,
            //preCloseCallback: function () { $scope.GetProfileData($scope.MyProfileFilter.MobileNumber); },
            scope: $scope
        });
    };

    $scope.LoanMavcCard = function (mobile, clubID) {
        $scope.MyProfileFilter.MobileNumberSelected = mobile;
        $scope.MyProfileFilter.ClubID = clubID;
        ngDialog.open({ template: 'loadMavcCard',
            className: 'ngdialog-theme-default',
            controller: 'MyProfileController',
            closeByDocument: true,
            //preCloseCallback: function () { $scope.GetProfileData($scope.MyProfileFilter.MobileNumber); },
            scope: $scope
        });
    };

    $scope.Pasaload = function (mobile) {
        $scope.MyProfileFilter.MobileNumberSelected = mobile;
        ngDialog.open({ template: 'pasaload',
            className: 'ngdialog-theme-default',
            controller: 'MyProfileController',
            closeByDocument: true,
            //preCloseCallback: function () { $scope.GetProfileData($scope.MyProfileFilter.MobileNumber); },
            scope: $scope
        });
    };

    $scope.LoadNow = function () {
        if ($scope.MyProfileFilter.PinNumber != '') {
            MainService.LoadMavcCard($scope.$parent.MyProfileFilter.ClubID, $scope.$parent.MyProfileFilter.MobileNumberSelected, $scope.MyProfileFilter.PinNumber).then(function (unregResult) {
                $scope.UnregResult = unregResult;
                alert(unregResult[0].result);
                window.location.href = "/Main/MyProfile";
            }, function ()
            { alert('Connection Error') })
        };
        ngDialog.close();
    }

    $scope.Unreg = function (value) {
        if (value == true) {
            MainService.UnRegMobileNumber($scope.$parent.MyProfileFilter.ClubID, $scope.$parent.MyProfileFilter.MobileNumberSelected).then(function (unregResult) {
                $scope.UnregResult = unregResult;
                alert(unregResult[0].result);
                window.location.href = "/Main/MyProfile";
            }, function ()
            { alert('Connection Error') })
        };
        ngDialog.close();
    }

    $scope.PasaloadNow = function () {
        if ($scope.MyProfileFilter.PasaloadAmount != '' && $scope.MyProfileFilter.ToMobileNumber != '') {
            MainService.PasaloadNow($scope.$parent.MyProfileFilter.MobileNumberSelected, $scope.MyProfileFilter.ToMobileNumber, $scope.MyProfileFilter.PasaloadAmount).then(function (pasaload) {
                $scope.Pasaload = pasaload;
                alert(pasaload[0].result);
                window.location.href = "/Main/MyProfile";
            }, function ()
            { alert('Connection Error') })
        };
        ngDialog.close();
    };

    $scope.GetProfileData = function (mobileNumber) {
        if (mobileNumber == '') return;
        var len = mobileNumber.toString().length;
        if (len == 12) $scope.MyProfileFilter.MobileNumber = '+' + mobileNumber;
        MainService.GetMobileList($scope.MyProfileFilter.MobileNumber).then(function (myProfile) {
            $scope.MyProfile = myProfile;
            $scope.ClubCollection = [];
            var clubName = '';
            for (i = 0; i <= myProfile.length - 1; i++) {
                if (myProfile[i].clubName != clubName) {
                    clubName = myProfile[i].clubName;
                    var item = {
                        ClubID: myProfile[i].clubAbbreviation,
                        ClubName: myProfile[i].clubName,
                        MemberIdNo: myProfile[i].memberIdNo,
                        LastName: myProfile[i].lastName,
                        FirstName: myProfile[i].firstName,
                        MiddleName: myProfile[i].middleName,
                        ExtensionName: myProfile[i].extensionName,
                        LoftName: myProfile[i].loftName,
                        Coordinates: myProfile[i].coordinates,
                        MembershipStatus: myProfile[i].membershipStatus
                    };
                    $scope.ClubCollection.push(item);
                }
            }

        }, function ()
        { alert('Connection Error') });

        //$scope.list();
    };
})
.factory('MainService', function ($http, $q) {
    var fac = {};

    fac.GetInitialValues = function () {
        return $http({
            url: '/RaceResult/GetInitialValues',
            method: 'POST',
            headers: { 'content-type': 'application/json' }
        });
    };

    fac.GetMobileList = function (d) {
        var deferred = $q.defer();
        // Initiates the AJAX call
        $http({ method: 'POST', url: '/MyProfile/GetMobileList', params: { MobileNumber: d} }).success(deferred.resolve).error(deferred.reject);
        // Returns the promise - Contains result once request completes
        return deferred.promise;
    };

    fac.LoadMavcCard = function (ClubID, MobileNumber, PinNumber) {
        var deferred = $q.defer();
        // Initiates the AJAX call
        $http({ method: 'POST', url: '/MyProfile/LoadMavcCard', params: { MobileNumber: MobileNumber, ClubID: ClubID, PinNumber: PinNumber} }).success(deferred.resolve).error(deferred.reject);
        // Returns the promise - Contains result once request completes
        return deferred.promise;
    };

    fac.PasaloadNow = function (MobileNumberFrom, MobileNumberTo, Amount) {
        var deferred = $q.defer();
        // Initiates the AJAX call
        $http({ method: 'POST', url: '/MyProfile/Pasaload', params: { MobileNumberFrom: MobileNumberFrom, MobileNumberTo: MobileNumberTo, Amount: Amount} }).success(deferred.resolve).error(deferred.reject);
        // Returns the promise - Contains result once request completes
        return deferred.promise;
    };

    fac.UnRegMobileNumber = function (ClubID, MobileNumber) {
        var deferred = $q.defer();
        // Initiates the AJAX call
        $http({ method: 'POST', url: '/MyProfile/UnregMobileNumber', params: { MobileNumber: MobileNumber, ClubID: ClubID} }).success(deferred.resolve).error(deferred.reject);
        // Returns the promise - Contains result once request completes
        return deferred.promise;
    };
    return fac;
});