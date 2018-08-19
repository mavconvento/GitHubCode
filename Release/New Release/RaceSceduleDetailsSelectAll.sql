alter proc [dbo].[RaceSceduleDetailsSelectAll]
	@ClubID bigint,
	@ScheduleID bigint = 0,
	@ScheduleName varchar(100) = null
as

if (isnull(@ScheduleName,'') != '')
begin
	select @ScheduleID = rs.RaceScheduleID  from RaceSchedule rs where rs.RaceScheduleName = @ScheduleName and ClubID = @ClubID
end

select
	d.RaceScheduleDetailsID AS "ID",
	l.locationid,
	'EDIT' as ' ',
	l.LocationName,
	Distance = dbo.ComputeDistance
		(
		dbo.fnCalculateDegree
			(
			l.distancelatdegree,
			l.distancelatminutes,
			l.distancelatsecond,
			l.distancelatsign
			),
		dbo.fnCalculateDegree
			(
			l.distancelongdegree,
			l.distancelongminutes,
			l.distancelongsecond,
			l.distancelongsign
			),
		dbo.fnCalculateDegree
			(
			c.distancelatdegree,
			c.distancelatminutes,
			c.distancelatsecond,
			c.distancelatsign
			),
		dbo.fnCalculateDegree
			(
			c.distancelongdegree,
			c.distancelongminutes,
			c.distancelongsecond,
			c.distancelongsign
			)
		),
	Loading,
	LoadingTimeFrom + ' - ' + LoadingTimeTo as "Loading Time",
	CONVERT(varchar(10),d.DateRelease,101) as "Date Release",
	rrp.ReleaseTime,
	rrp.RaceReleasePointID,
	rrp.LapNo,
	rrp.Multiplier,
	rrp.MinSpeed,
	rrp.Description,
	rrp.IsStop,
	rrp.StopFromDate,
	rrp.StopFromTime,
	rrp.StopToDate,
	rrp.StopToTime
from raceschedule s (nolock)
	inner join racescheduledetails d (nolock) on d.racescheduleid=s.racescheduleid
	inner join location l (nolock) on l.locationid=d.locationid
	inner join Club c (nolock) on c.ClubID=s.ClubID
	inner join RaceReleasePoint rrp on rrp.RaceScheduleDetailsID = d.RaceScheduleDetailsID and rrp.ClubID = d.ClubID
	where d.RaceScheduleID=@scheduleid and d.clubid=@clubid
	order by d.daterelease


