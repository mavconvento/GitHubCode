angular.module('MyApp') // extending from previously created angular module in the First Part
.controller('MemberDistanceController', function ($scope, $interval, MainService, ngDialog) {
    //debugger;

    $scope.GetMemberDistance = function (clubID, memberIDNo) {
        debugger;
        MainService.GetMemberDistanceData(clubID, memberIDNo).then(function (memberDistance) { $scope.MemberDistance = memberDistance }, function ()
        { alert('error while fetching data from server') })
    }



})
.factory('MainService', function ($http, $q) {
    var fac = {};

    fac.GetMemberDistanceData = function (ClubID, MemberID) {
        var deferred = $q.defer();
        // Initiates the AJAX call
        $http({ method: 'GET', url: '/MyProfile/GetMemberDistance', params: { ClubID: ClubID, MemberID: MemberID} }).success(deferred.resolve).error(deferred.reject);
        // Returns the promise - Contains result once request completes
        return deferred.promise;
    };

    return fac;
});
