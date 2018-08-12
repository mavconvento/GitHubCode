angular.module('MyApp') // extending from previously created angular module in the First Part
.controller('RaceResultController', function ($scope, $rootScope, $http, $filter, MainService, ngTableParams) {


    $scope.ViewLogsFilter = {
        DateFrom: '',
        DateTo: '',
        Keyword: '',
        MobileNumber: ''
    };

    $scope.SetDate = function () {
            $scope.ViewLogsFilter.DateFrom = new Date();
            $scope.ViewLogsFilter.DateTo = new Date();
    }
    

    $scope.open1 = function () {
        $scope.popup1.opened = true;
    };

    $scope.popup1 = {
        opened: false
    };

    $scope.open2 = function () {
        $scope.popup2.opened = true;
    };

    $scope.popup2 = {
        opened: false
    };


    $scope.tableParams = {};
    $scope.data = [];
    $scope.listPromise = null;

    $scope.list = function () {
        $scope.data = $scope.SMSLogs;
        $scope.tableParams.reload();
    };

    $scope.$watch('SMSLogs', function (newVal) {
        $scope.list();
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

    $scope.Submit = function (mobileNumber) {
        if (mobileNumber == '') return;
        if ($scope.ViewLogsFilter.DateFrom == null || $scope.ViewLogsFilter.DateTo == null) {
            alert("Please select Date Range");
            return;
        }
        $scope.ViewLogsFilter.MobileNumber = '+' + mobileNumber;

        var viewLogsFilterData = $scope.ViewLogsFilter;

        var monthFrom = $filter('date')(viewLogsFilterData.DateFrom, 'MM'); //December-November like
        var dayFrom = $filter('date')(viewLogsFilterData.DateFrom, 'dd'); //01-31 like
        var yearFrom = $filter('date')(viewLogsFilterData.DateFrom, 'yyyy'); //2014 like

        viewLogsFilterData.DateFrom = yearFrom + '-' + monthFrom + '-' + dayFrom

        var monthTo = $filter('date')(viewLogsFilterData.DateFrom, 'MM'); //December-November like
        var dayTo = $filter('date')(viewLogsFilterData.DateFrom, 'dd'); //01-31 like
        var yearTo = $filter('date')(viewLogsFilterData.DateFrom, 'yyyy'); //2014 like

        viewLogsFilterData.DateTo = yearTo + '-' + monthTo + '-' + dayTo

        MainService.GetLogs(viewLogsFilterData).then(function (smsLogs) { $scope.SMSLogs = smsLogs }, function ()
        { alert('error while fetching data from server') });

        $scope.list();
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

    fac.GetLogs = function (d) {
        var deferred = $q.defer();
        // Initiates the AJAX call
        $http({ method: 'POST', url: '/ViewLogs/GetViewLogs', params: { MobileNumber: d.MobileNumber, Keyword: d.Keyword, DateFrom: d.DateFrom, DateTo: d.DateTo} }).success(deferred.resolve).error(deferred.reject);
        // Returns the promise - Contains result once request completes
        return deferred.promise;
    };

    return fac;
});