"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
Object.defineProperty(exports, "__esModule", { value: true });
exports.MemberDistanceComponent = void 0;
var core_1 = require("@angular/core");
var router_1 = require("@angular/router");
var forms_1 = require("@angular/forms");
var paginator_1 = require("@angular/material/paginator");
var sort_1 = require("@angular/material/sort");
var table_1 = require("@angular/material/table");
var helpers_1 = require("../../helpers/helpers");
var user_service_1 = require("../../services/user.service");
var MemberDistanceComponent = /** @class */ (function () {
    function MemberDistanceComponent(fb, helper, activatedRoute, userService) {
        this.fb = fb;
        this.helper = helper;
        this.activatedRoute = activatedRoute;
        this.userService = userService;
        this.displayedColumns = ["ReleasePoint", "Coordinates", "Distance"];
        this.length = 0;
        this.pageIndex = 0;
        this.pageSize = 20;
        this.pageSizeOptions = [20, 30, 50, 100];
        this.length2 = 0;
        this.pageIndex2 = 0;
        this.pageSize2 = 20;
        this.pageSizeOptions2 = [20, 30, 50, 100];
        this.dataSource = new table_1.MatTableDataSource(this.distanceCollectionNorth);
        this.dataSource2 = new table_1.MatTableDataSource(this.distanceCollectionSouth);
    }
    Object.defineProperty(MemberDistanceComponent.prototype, "sort", {
        set: function (sort) {
            this.dataSource.sort = sort;
            this.dataSource2.sort = sort;
        },
        enumerable: false,
        configurable: true
    });
    ;
    MemberDistanceComponent.prototype.changeView = function (option) {
        if (option.value == "North") {
            this.isNorth = true;
            this.isSouth = false;
        }
        else if (option.value == "South") {
            this.isNorth = false;
            this.isSouth = true;
        }
    };
    MemberDistanceComponent.prototype.ngOnInit = function () {
        this.form = this.fb.group({
            distance: ['North', forms_1.Validators.required]
        });
        this.clubname = this.activatedRoute.snapshot.params.clubname;
        this.dbname = this.activatedRoute.snapshot.params.dbname;
        this.memberid = this.activatedRoute.snapshot.params.memberidno;
        this.GetDistance(this.clubname, this.memberid, this.dbname);
        this.isNorth = true;
        this.isSouth = false;
    };
    MemberDistanceComponent.prototype.ngAfterViewInit = function () {
        this.dataSource.paginator = this.paginator;
        this.dataSource2.paginator = this.paginator2;
        this.dataSource.sort = this.sort;
    };
    MemberDistanceComponent.prototype.getNext = function (event) {
        this.length = event.length;
        this.pageIndex = event.pageIndex;
        this.pageSize = event.pageSize;
    };
    MemberDistanceComponent.prototype.getNext2 = function (event) {
        this.length2 = event.length;
        this.pageIndex2 = event.pageIndex;
        this.pageSize2 = event.pageSize;
    };
    MemberDistanceComponent.prototype.GetDistance = function (clubname, memberid, dbname) {
        var _this = this;
        this.userService.getMemberDistance(clubname, memberid, dbname).subscribe(function (data) {
            var result = JSON.parse(data.content);
            console.log(result.Table);
            if (result.Table.length > 0) {
                _this.name = result.Table[0].Name;
                _this.coordinates = result.Table[0].Coordinates;
            }
            _this.distanceCollectionNorth = result.Table1;
            _this.distanceCollectionSouth = result.Table2;
            _this.dataSource.data = _this.distanceCollectionNorth;
            _this.dataSource2.data = _this.distanceCollectionSouth;
            _this.length = _this.distanceCollectionNorth.length;
            _this.length2 = _this.distanceCollectionSouth.length;
        });
    };
    MemberDistanceComponent.prototype.viewDistanceMap = function () {
        console.log(this.memberid);
        console.log(this.clubname);
        console.log(this.dbname);
        this.userService.getMemberCoordinates(this.memberid, this.clubname, this.dbname).subscribe(function (data) {
            var result = JSON.parse(data.content);
            if (result.Table.length > 0) {
                window.open('https://www.google.com/maps/place/' + result.Table[0].Lat + ' ' + result.Table[0].Long, '_blank');
            }
            console.log((JSON.parse(data.content)).Table);
        });
    };
    Object.defineProperty(MemberDistanceComponent.prototype, "f", {
        get: function () { return this.form.controls; },
        enumerable: false,
        configurable: true
    });
    __decorate([
        core_1.ViewChild('paginator', { static: false }),
        __metadata("design:type", paginator_1.MatPaginator)
    ], MemberDistanceComponent.prototype, "paginator", void 0);
    __decorate([
        core_1.ViewChild('paginator2', { static: false }),
        __metadata("design:type", paginator_1.MatPaginator)
    ], MemberDistanceComponent.prototype, "paginator2", void 0);
    __decorate([
        core_1.ViewChild(sort_1.MatSort, { static: false }),
        __metadata("design:type", sort_1.MatSort),
        __metadata("design:paramtypes", [sort_1.MatSort])
    ], MemberDistanceComponent.prototype, "sort", null);
    MemberDistanceComponent = __decorate([
        core_1.Component({
            selector: 'app-member-distance',
            templateUrl: './member-distance.component.html',
            styleUrls: ['./member-distance.component.css']
        }),
        __metadata("design:paramtypes", [forms_1.FormBuilder,
            helpers_1.Helpers,
            router_1.ActivatedRoute,
            user_service_1.UserService])
    ], MemberDistanceComponent);
    return MemberDistanceComponent;
}());
exports.MemberDistanceComponent = MemberDistanceComponent;
//# sourceMappingURL=member-distance.component.js.map