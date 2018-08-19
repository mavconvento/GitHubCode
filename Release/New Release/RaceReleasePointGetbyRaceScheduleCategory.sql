alter proc [dbo].[RaceReleasePointGetbyRaceScheduleCategory]
	@ClubID bigint,
	@RaceScheduleCategoryName varchar(100) --pass value as schedule name
as
Declare @RaceScheduleID bigint

SET @RaceScheduleID = 0

select @RaceScheduleID=RaceScheduleID 
from RaceSchedule 
where Clubid=@ClubID 
	and RaceScheduleName = @RaceScheduleCategoryName 
	and IsActive=1


select 
	RaceReleasePointID as "Release Point ID",
	L.LocationID as "Location ID",
	'SELECT' AS " ",
	l.LocationName as "Location Name",
	Coordinates =
		dbo.fnDistance
		(
		l.distancelatdegree,
		l.distancelatminutes,
		l.distancelatsecond,
		l.distancelatsign
		) + ' - ' + 
		dbo.fnDistance
		(
		l.distancelongdegree,
		l.distancelongminutes,
		l.distancelongsecond,
		l.distancelongsign
		),
	sd.DateRelease,
	rr.ReleaseTime
from RaceReleasePoint RR (nolock)
inner join RaceScheduleDetails sd (nolock) on sd.ID=rr.RaceScheduleDetailsID
inner join Location L (nolock) on sd.LocationID=L.LocationID and L.IsActive=1
where sd.RacescheduleID=@RaceScheduleID and RR.IsActive=1 and rr.ClubID=@ClubID




