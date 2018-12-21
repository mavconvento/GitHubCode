angular.module('MyApp') // extending from previously created angular module in the First Part
.controller('myCtrl', function ($scope, $rootScope, $http, $filter, MainService, ngTableParams) {
    debugger;

    $scope.County = [{ CountyName: 'C1', countyNumber: '01' }, { CountyName: 'C2', countyNumber: '02' }, { CountyName: 'C3', countyNumber: '03' }, { CountyName: 'C4', countyNumber: '04'}];
    $scope.Municipality = [{ MunicipalityName: 'M1', MunicipalityNumber: '01' }, { MunicipalityName: 'M2', MunicipalityNumber: '02' }, { MunicipalityName: 'M3', MunicipalityNumber: '03'}];

    $scope.Districts = [{ DistrictsName: 'D1', DistrictsNumber: '01' }, { DistrictsName: 'D2', DistrictsNumber: '02' }, { DistrictsName: 'D3', DistrictsNumber: '03'}];

    $scope.featuretype = [
            { type: 'County', data: $scope.County, displayName: 'CountyName' },
             { type: 'Municipality', data: $scope.Municipality, displayName: 'MunicipalityName' },
             { type: 'District', data: $scope.Districts, displayName: 'DistrictsName' }
    ];

})
.factory('MainService', function ($http, $q) {
    var fac = {};

    

    return fac;
});
