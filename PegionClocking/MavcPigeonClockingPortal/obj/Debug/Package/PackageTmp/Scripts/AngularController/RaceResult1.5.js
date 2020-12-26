angular.module('MyApp') // extending from previously created angular module in the First Part
.controller('RaceResultController', function ($scope, $location, $window, $rootScope, $http, $filter, MainService, ngTableParams) {
    $scope.RaceResultFilter = {
        ClubID: '',
        ClubName: [],
        BirdCategory: 'All',
        GroupCategory: 'All',
        SearchName: '',
        RaceReleaseDate: '',
        AccessNumber: '',
        StickerNumber: '',
        MobileNumber: '',
        URL: '',
        RemainingDays: ''
    };

    $scope.RaceDetails = {
        LiberationPoint: '',
        Sunset: '',
        Sunrise: '',
        Coordinates: '',
        TotalBirdEntry: '',
        LapNo: '',
        TotalBirdClock: '',
        Description: '',
        ReleaseTime: '',
        MinSpeed: '',
        StopTime: '',
        BirdEntryCount: '',
        Longtitude: '',
        Latitude: ''
    };

    $scope.formats = ['dd-MM-yyyy', 'yyyy/MM/dd', 'yyyy-MM-dd', 'dd.MM.yyyy', 'shortDate'];
    $scope.format = $scope.formats[0];
    $scope.loading = false;
    $scope.WithError = false;
    $scope.open1 = function () {
        $scope.popup1.opened = true;
    };

    $scope.popup1 = {
        opened: false
    };

    $scope.loading = true;
    MainService.GetClubNameList().then(function (clubNameList) { $scope.ClubNameList = clubNameList }, function ()
    { alert('error while fetching data from server') }).finally(function () { $scope.loading = false });

    $scope.SetInitialValues = function () {

        $scope.RaceResultFilter.RaceReleaseDate = new Date();
        //$scope.RaceResultFilter.ClubName = ClubID.toString();  
    }


    $scope.ClubNameChange = function (value) {
        var urlparameter = "ClubID=" + value + "&ReleaseDate=" + $scope.RaceResultFilter.RaceReleaseDate + "&SearchName=" + $scope.RaceResultFilter.SearchName;
        window.location.href = "/Main/RaceResult?" + urlparameter;
    };

    $scope.GetAccessNumber = function (objects) {
        debugger;
        $scope.RaceResultFilter.AccessNumber = objects.accessNumber;
        $scope.RaceResultFilter.ClubID = objects.clubID;
        $scope.RemainingDays = objects.daysRemaining;
        $scope.RaceResultFilter.URL = "/Images/ClubLogo/" + objects.clubAbbreviation + ".png";
        $scope.loading = true;
        $scope.WithError = false;
        MainService.GetBirdCategory($scope.RaceResultFilter.ClubID).then(function (BirdCategory) {
            $scope.BirdCategory = BirdCategory
        }, function ()
        { alert('error while fetching data from server'); $scope.WithError = true; $scope.loading = false;}).finally(function () { });

        if ($scope.WithError != true) {
            $scope.loading = true;
            MainService.GetGroupCategory($scope.RaceResultFilter.ClubID).then(function (GroupCategory) {
                $scope.GroupCategory = GroupCategory
            }, function ()
            { alert('error while fetching data from server') }).finally(function () { $scope.loading = false; });
        }
    };

    $scope.tableParams = {};
    $scope.data = [];
    $scope.tableParamsEntry = {};
    $scope.dataEntry = [];
    $scope.listPromise = null;

    $scope.list = function () {
        $scope.data = $scope.RaceResult;
        $scope.tableParams.reload();
    };

    $scope.listEntry = function () {
        $scope.dataEntry = $scope.RaceEntry;
        $scope.tableParamsEntry.reload();
    };

    $scope.$watch('RaceResult', function (newVal) {
        $scope.list();
    });

    $scope.$watch('RaceEntry', function (newVal) {
        $scope.listEntry();
    });

    $scope.tableParams = new ngTableParams({
        page: 1,            // show first page
        count: 10,          // count per page
        sorting: {
            name: 'asc'     // initial sorting
        }
    }, {
        total: $scope.data.length, // length of data
        getData: function ($defer, params) {
            var filteredData = $scope.data;
            var orderedData = params.sorting() ?
                                $filter('orderBy')(filteredData, params.orderBy()) :
                                filteredData;
            params.total($scope.data.length);
            $defer.resolve(orderedData.slice((params.page() - 1) * params.count(), params.page() * params.count()));
        }
    });

    $scope.tableParamsEntry = new ngTableParams({
        page: 1,            // show first page
        count: 10,          // count per page
        sorting: {
            name: 'asc'     // initial sorting
        }
    }, {
        total: $scope.dataEntry.length, // length of data
        getData: function ($defer, params) {
            var filteredData = $scope.dataEntry;
            var orderedData = params.sorting() ?
                                $filter('orderBy')(filteredData, params.orderBy()) :
                                filteredData;
            params.total($scope.dataEntry.length);
            $defer.resolve(orderedData.slice((params.page() - 1) * params.count(), params.page() * params.count()));
        }
    });

    $scope.ViewMaps = function (latitude, longtitude, location, coordinates) {
        var a = $location;
        var rooturl = $location.$$protocol + '://' + $location.$$host;

        if ($location.$$port != '') rooturl = rooturl + ':' + $location.$$port;
        $window.open('https://www.google.com/maps/place/' + latitude + ' ' + longtitude, '_blank');
    }

    $scope.Submit = function (searchName) {
        if (searchName == '') $scope.RaceResultFilter.SearchName = '';
        var raceResultFilterData = $scope.RaceResultFilter;
        var month = $filter('date')(raceResultFilterData.RaceReleaseDate, 'MM'); //December-November like
        var day = $filter('date')(raceResultFilterData.RaceReleaseDate, 'dd'); //01-31 like
        var year = $filter('date')(raceResultFilterData.RaceReleaseDate, 'yyyy'); //2014 like

        raceResultFilterData.RaceReleaseDate = year + '-' + month + '-' + day
        $scope.loading = true;
        $scope.WithError = false;
        MainService.GetRaceDetails(raceResultFilterData).then(function (raceDetails) {
            $scope.RaceDetails.LiberationPoint = raceDetails.locationName;
            if (raceDetails.stopTime != '') {
                $scope.RaceDetails.Sunset = raceDetails.stopFrom;
                $scope.RaceDetails.Sunrise = raceDetails.stopTo;
            }
            $scope.RaceDetails.Coordinates = raceDetails.coordinates;
            $scope.RaceDetails.TotalBirdEntry = raceDetails.totalBird;
            $scope.RaceDetails.LapNo = raceDetails.lap;
            $scope.RaceDetails.TotalBirdClock = raceDetails.sMSCount;
            $scope.RaceDetails.Description = raceDetails.description;
            $scope.RaceDetails.ReleaseTime = raceDetails.releaseTime;
            $scope.RaceDetails.MinSpeed = raceDetails.minSpeed;
            $scope.RaceDetails.BirdEntryCount = raceDetails.birdEntryCount;
            $scope.RaceDetails.Longtitude = raceDetails.longtitude;
            $scope.RaceDetails.Latitude = raceDetails.latitude;

        }, function ()
        { alert('error while fetching data from server'); $scope.WithError = true; $scope.loading = false; }).finally(function () {});

       
        if ($scope.WithError != true) {
            $scope.loading = true;
            MainService.GetRaceResult(raceResultFilterData).then(function (raceResult) { $scope.RaceResult = raceResult }, function ()
            { alert('error while fetching data from server'); $scope.WithError = true; $scope.loading = false; }).finally(function () { });;
        }

        if ($scope.WithError != true) {
            $scope.loading = true;
            MainService.GetRaceEntry(raceResultFilterData).then(function (raceEntry) { $scope.RaceEntry = raceEntry }, function ()
            { alert('error while fetching data from server'); $scope.WithError = true; }).finally(function () { $scope.loading = false; });;
        }

        $scope.list();
        $scope.listEntry();
    }

    $scope.Send = function (mobileNumber) {
        if (mobileNumber == '') return;
        var len = mobileNumber.toString().length;
        if (len == 12) $scope.RaceResultFilter.MobileNumber = '+' + mobileNumber;
        if ($scope.RaceResultFilter.ClubID == "") { alert("Please select club"); return; }
        if ($scope.RaceResultFilter.StickerNumber == "") { alert("Enter Sticker Number."); return; }
        MainService.SendSticker($scope.RaceResultFilter.ClubID, $scope.RaceResultFilter.MobileNumber, $scope.RaceResultFilter.StickerNumber).then(function (unregResult) {
            $scope.UnregResult = unregResult;
            alert(unregResult[0].result);
            $scope.RaceResultFilter.StickerNumber = '';
            $scope.Submit('');
        }, function ()
        { alert('error while sending data. please try again') })
    };

    $scope.Forecast = function (mobileNumber) {
        if (mobileNumber == '') return;
        var len = mobileNumber.toString().length;
        if (len == 12) $scope.RaceResultFilter.MobileNumber = '+' + mobileNumber;
        if ($scope.RaceResultFilter.ClubID == "") { alert("Please select club"); return; }
        MainService.Forecast($scope.RaceResultFilter.ClubID, $scope.RaceResultFilter.MobileNumber).then(function (unregResult) {
            $scope.UnregResult = unregResult;
            alert(unregResult[0].result);
        }, function (unregResult)
        { alert('error while sending data. please try again') })
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

    fac.GetClubNameList = function () {
        var deferred = $q.defer();
        // Initiates the AJAX call
        $http({ method: 'POST', url: '/RaceResult/GetClubList' }).success(deferred.resolve).error(deferred.reject);
        // Returns the promise - Contains result once request completes
        return deferred.promise;
    };

    fac.GetBirdCategory = function (d) {
        var deferred = $q.defer();
        // Initiates the AJAX call
        $http({ method: 'POST', url: '/RaceResult/GetBirdCategory', params: { ClubID: d } }).success(deferred.resolve).error(deferred.reject);
        // Returns the promise - Contains result once request completes
        return deferred.promise;
    };

    fac.GetGroupCategory = function (d) {
        var deferred = $q.defer();
        // Initiates the AJAX call
        $http({ method: 'POST', url: '/RaceResult/GetGroupCategory', params: { ClubID: d } }).success(deferred.resolve).error(deferred.reject);
        // Returns the promise - Contains result once request completes
        return deferred.promise;
    };

    fac.GetRaceResult = function (d) {
        var deferred = $q.defer();
        // Initiates the AJAX call
        $http({ method: 'POST', url: '/RaceResult/GetRaceResult', params: { ClubID: d.ClubID, BirdCategory: d.BirdCategory, RaceCategory: d.GroupCategory, ReleaseDate: d.RaceReleaseDate, SearchName: d.SearchName } }).success(deferred.resolve).error(deferred.reject);
        // Returns the promise - Contains result once request completes
        return deferred.promise;
    };

    fac.GetRaceEntry = function (d) {
        var deferred = $q.defer();
        // Initiates the AJAX call
        $http({ method: 'POST', url: '/RaceResult/GetRaceEntry', params: { ClubID: d.ClubID, BirdCategory: d.BirdCategory, RaceCategory: d.GroupCategory, ReleaseDate: d.RaceReleaseDate, SearchName: d.SearchName } }).success(deferred.resolve).error(deferred.reject);
        // Returns the promise - Contains result once request completes
        return deferred.promise;
    };

    fac.GetRaceDetails = function (d) {
        var deferred = $q.defer();
        // Initiates the AJAX call
        $http({ method: 'POST', url: '/RaceResult/GetRaceDetails', params: { ClubID: d.ClubID, BirdCategory: d.BirdCategory, RaceCategory: d.GroupCategory, ReleaseDate: d.RaceReleaseDate, SearchName: d.SearchName } }).success(deferred.resolve).error(deferred.reject);
        // Returns the promise - Contains result once request completes
        return deferred.promise;
    };

    fac.SendSticker = function (ClubID, MobileNumber, StickerNumber) {
        var deferred = $q.defer();
        // Initiates the AJAX call
        $http({ method: 'POST', url: '/RaceResult/SendSticker', params: { ClubID: ClubID, MobileNumber: MobileNumber, StickerNumber: StickerNumber } }).success(deferred.resolve).error(deferred.reject);
        // Returns the promise - Contains result once request completes
        return deferred.promise;
    };

    fac.Forecast = function (ClubID, MobileNumber) {
        var deferred = $q.defer();
        // Initiates the AJAX call
        $http({ method: 'POST', url: '/RaceResult/Forecast', params: { ClubID: ClubID, MobileNumber: MobileNumber } }).success(deferred.resolve).error(deferred.reject);
        // Returns the promise - Contains result once request completes
        return deferred.promise;
    };
    return fac;
});