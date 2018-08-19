alter proc [dbo].[RaceScheduleCategorySelectAll]
	@ClubID varchar(100),
	@RaceScheduleID BIGINT,
	@RaceScheduleName VARCHAR(100)
as

IF (ISNULL(@RaceScheduleName,'') != '')
BEGIN
	SELECT @RaceScheduleID = racescheduleid FROM RaceSchedule where raceschedulename = @RaceScheduleName and clubid = @clubid
END

SELECT sc.RaceScheduleCategoryID as "Category ID",
	   'DELETE' as " ",
	   LocationName as "Location Name",
	   DateRelease as "Date Release",
	   Lap,
	   Multiplier as "Points"
from RaceScheduleCategory sc
	inner join RaceReleasePoint rr on rr.RaceScheduleCategoryID=sc.RaceScheduleCategoryID and rr.IsActive=1
where sc.Clubid=@Clubid 
	and sc.RaceScheduleID=@RaceScheduleID
	and sc.RaceScheduleCategoryName = 'OverAllResult'
	and sc.IsActive=1
--group by sc.RaceScheduleCategoryID,RaceScheduleCategoryName,Lap
order by sc.RaceScheduleCategoryID


