alter proc [dbo].[RaceScheduleDetailsDelete]
	@ClubId bigint,
	@UserID bigint,
	@ID BIGINT
as
begin
declare @WithResult bit = 0

if EXISTS (SELECT TOP 1 1 FROM dbo.RaceScheduleDetails RS 
			INNER JOIN dbo.RaceReleasePoint RR ON RS.RaceScheduleDetailsID = RR.RaceScheduleDetailsID
			INNER JOIN dbo.Entry E ON E.RaceReleasePointID = RR.RaceReleasePointID
			INNER JOIN dbo.RaceResult rsult ON rsult.EntryID = e.EntryID
		WHERE RR.RaceScheduleDetailsID = @ID and rs.ClubID = @ClubId)
begin
	set @WithResult = 1
end

if EXISTS (SELECT TOP 1 1 FROM dbo.RaceScheduleDetails RS 
			INNER JOIN dbo.RaceReleasePoint RR ON RS.RaceScheduleDetailsID = RR.RaceScheduleDetailsID
			INNER JOIN dbo.Entry E ON E.RaceReleasePointID = RR.RaceReleasePointID
			INNER JOIN dbo.RaceResult_Archive rsult ON rsult.EntryID = e.EntryID
		WHERE RR.RaceScheduleDetailsID = @ID and rs.ClubID = @ClubId)
begin
	set @WithResult = 1
end

if (@WithResult = 1)
Begin
	raiserror('Race Details has result. Operation not allowed',16,1)
	return
END

Delete from RaceScheduleDetails 
WHERE ID=@ID and clubid=@ClubId

DECLARE @RaceScheduleCategoryID bigint

SELECT @RaceScheduleCategoryID = RR.RaceScheduleCategoryID FROM dbo.RaceReleasePoint RR WHERE RaceScheduleDetailsID = @ID and clubid=@ClubId

DELETE FROM dbo.RaceReleasePoint WHERE RaceScheduleDetailsID = @ID and clubid=@ClubId

DELETE FROM RaceScheduleCategory 
where RaceScheduleCategoryID = @RaceScheduleCategoryID 
	and clubid=@ClubId
	and DATEDIFF(day,'2018-08-17',DateCreated) >= 0

exec FileNotesSave
	@AccountName = 'RaceScheduleDetails',
	@AccountID = @ID,
	@Action = 'Delete',
	@CreatedBy = @UserID,
	@ClubID = @Clubid

end
go
--exec [dbo].[RaceScheduleCategorySelectAll] 3,0,'north race 2018'
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
	   --'DELETE' as " ",
	   LocationName as "Location Name",
	   DateRelease as "Date Release",
	   LapNo,
	   Multiplier as "Points"
from RaceScheduleCategory sc
	inner join RaceReleasePoint rr on rr.RaceScheduleCategoryID=sc.RaceScheduleCategoryID and rr.IsActive=1
where sc.Clubid=@Clubid 
	and sc.RaceScheduleID=@RaceScheduleID
	and sc.RaceScheduleCategoryName = 'OverAllResult'
	and sc.IsActive=1
--group by sc.RaceScheduleCategoryID,RaceScheduleCategoryName,Lap
order by LapNo
go
alter proc [dbo].[RaceScheduleCategorySave]
	@ClubID bigint,
	@UserID bigint,
	@RaceScheduleID BIGINT,
	@RaceScheduleCategoryID BIGINT,
	@RaceScheduleCategoryName varchar(500),
	@Lap bigint,

	--this is add thru overall result
	@IsMain bit = 0,
	@RaceScheduleName varchar(100) = '',
	@RaceReleasePointID bigint = 0
as
if @RaceScheduleCategoryName=''
begin
	raiserror('Schedule Category Name is empty',16,1)
	return
END

if (@IsMain = 1)
begin
	select @RaceScheduleID = rs.RaceScheduleID from RaceSchedule rs where rs.RaceScheduleName = @RaceScheduleName and ClubID = @ClubID
	select @Lap = LapNo from RaceReleasePoint rr where rr.RaceReleasePointID = @RaceReleasepointID and ClubID = @ClubID

	if not exists(select 1 from RaceScheduleCategory rsc where rsc.RaceScheduleCategoryName = 'OverAllResult' and ClubID = @ClubID)
	begin
			INSERT INTO RaceScheduleCategory
			(
				ClubID
				,RaceScheduleID
				,RaceScheduleCategoryID
				,RaceScheduleCategoryName
				,Lap
				,CreatedBy
				,DateCreated
			)
			Values
			(
				@ClubID
				,@RaceScheduleID
				,@RaceScheduleCategoryID
				,'OverAllResult'
				,isnull(@Lap,1)
				,@UserID
				,getdate()
			)
	
		set @RaceScheduleCategoryID=IDENT_CURRENT('RaceScheduleCategory')
		update RaceScheduleCategory set RaceScheduleCategoryID=@RaceScheduleCategoryID where ID=@RaceScheduleCategoryID
	end

	select @RaceScheduleCategoryID = rsc.RaceScheduleCategoryID from RaceScheduleCategory rsc where rsc.RaceScheduleCategoryName = 'OverAllResult' and ClubID = @ClubID
	
	--GET COUNT NUMBER BASE ON RECORD
	select @Lap = COUNT(1) FROM RaceReleasePoint WHERE RaceScheduleCategoryID = @RaceScheduleCategoryID AND ClubID = @ClubID

	update RaceReleasePoint set RaceScheduleCategoryID = @RaceScheduleCategoryID, LapNo = ISNULL(@Lap,0) + 1
	WHERE RaceReleasePointID = @RaceReleasepointID AND ClubID = @ClubID

	update RaceScheduleCategory set Lap = ISNULL(@Lap,0) + 1
	from RaceScheduleCategory rsc where rsc.RaceScheduleCategoryName = 'OverAllResult' and ClubID = @ClubID
	return
end


declare @Action varchar(20)
IF (@RaceScheduleCategoryID > 0)
BEGIN
	UPDATE	RaceScheduleCategory
	SET RaceScheduleID=@RaceScheduleID,
		--RaceScheduleCategoryID=@RaceScheduleCategoryID,
		RaceScheduleCategoryName=@RaceScheduleCategoryName,
		Lap=@Lap,
		UpdatedBy=@UserID,
		DateUpdated=getdate()
	WHERE RaceScheduleCategoryID=@RaceScheduleCategoryID and ClubID=@ClubID
	SET @Action = 'Update'
END
ELSE
BEGIN
	INSERT INTO RaceScheduleCategory
		(
		ClubID
		,RaceScheduleID
		,RaceScheduleCategoryID
		,RaceScheduleCategoryName
		,Lap
		,CreatedBy
		,DateCreated
		)

	Values
		(
		@ClubID
		,@RaceScheduleID
		,@RaceScheduleCategoryID
		,@RaceScheduleCategoryName
		,@Lap
		,@UserID
		,getdate()
		)
	
	set @RaceScheduleCategoryID=IDENT_CURRENT('RaceScheduleCategory')
	update RaceScheduleCategory set RaceScheduleCategoryID=@RaceScheduleCategoryID where ID=@RaceScheduleCategoryID
	SET @Action = 'Insert'
END

exec FileNotesSave
	@AccountName = 'RaceScheduleCategory',
	@AccountID = @RaceScheduleCategoryID,
	@Action = @Action,
	@CreatedBy = @UserID,
	@ClubID = @ClubID

go
alter proc [dbo].[RaceScheduleCategoryDelete]
	@ClubId bigint,
	@UserID bigint,
	@RaceScheduleCategoryID bigint
as

select @RaceScheduleCategoryID = RaceScheduleCategoryID  from RaceScheduleCategory where RaceScheduleCategoryName = 'OverAllResult' and ClubID = @ClubId
if (@RaceScheduleCategoryID > 0)
begin
--DELETE FROM RaceScheduleCategory
--WHERE RaceScheduleCategoryID=@RaceScheduleCategoryID and clubid=@Clubid

update dbo.RaceReleasePoint set RaceScheduleCategoryID = ExternalID  WHERE RaceScheduleCategoryID = @RaceScheduleCategoryID and clubid=@Clubid

exec FileNotesSave
	@AccountName = 'RaceScheduleCategory',
	@AccountID = @RaceScheduleCategoryID,
	@Action = 'Delete',
	@CreatedBy = @UserID,
	@ClubID = @ClubID
end
go
update dbo.RaceReleasePoint set ExternalID = RaceScheduleCategoryID
go
alter proc [dbo].[RaceReleasePointSave]
	--@ID BIGINT,
	@ClubID bigint,
	@RaceScheduleCategoryID BIGINT,
	@RaceScheduleDetailsID BIGINT,
	@RaceReleasePointID BIGINT,
	@TimeReleased varchar(100),
	@Multiplier decimal(18,2),
	@LapNo bigint,
	@MinSpeed varchar(100),
	@IsStop bit,
	@StopFromDate datetime,
	@StopFromTime varchar(100),
	@StopToDate datetime,
	@StopToTime varchar(100),
	@UserID BIGINT,
	@Description VARCHAR(1000)
	
as
declare @StopTime bigint
declare @Action varchar(20)

set @StopTime=dbo.fnTimetoMinutes(dbo.fnFlight(
convert(datetime,(convert(varchar(10),@StopFromDate,101) + ' ' + @StopFromTime)),
convert(datetime,(convert(varchar(10),@StopToDate,101) + ' ' + @StopToTime))
))

IF (@TimeReleased = ':') SET @TimeReleased = ''

--if exists (select 1 from RaceReleasePoint rr
--			INNER JOIN dbo.RaceScheduleCategory rsc ON rr.RaceScheduleCategoryID=rsc.RaceScheduleCategoryID
--			INNER JOIN dbo.RaceScheduleDetails rd ON rr.RaceScheduleDetailsID = rd.RaceScheduleDetailsID
--			where LapNo=@LapNo and rsc.RaceScheduleCategoryID=@RaceScheduleCategoryID and rd.RaceScheduleDetailsID<>@RaceScheduleDetailsID and rr.ClubID=@ClubID)
--	begin
--		raiserror('Invalid duplication of lap no.',16,1)
--		return
--	end

--IF (charindex('a',@TimeReleased,1) > 0 or charindex('p',@TimeReleased,1) > 0 or charindex('m',@TimeReleased,1) > 0)
--	begin
--		raiserror('Please remove "AM" or "PM" on release time. Invalid time value.',16,1)
--		return
--	END

--IF (charindex(':',@TimeReleased,1) = 0 AND @TimeReleased != '')
--	begin
--		raiserror('Invalid Time format. Invalid time value.',16,1)
--		return
--	END

--IF (LEN(@TimeReleased) != 5 AND @TimeReleased != '')
--BEGIN
--	raiserror('Invalid Time format. Invalid time value.',16,1)
--	return
--END

--IF EXISTS (SELECT 1 FROM dbo.RaceReleasePoint WHERE RaceScheduleDetailsID = @RaceScheduleDetailsID AND @RaceReleasePointID = 0)
--	BEGIN
--		raiserror('Location and Release Date already exists. Operation not Allowed',16,1)
--		return
--	END	

--IF (@MinSpeed = '')
--BEGIN
--	raiserror('Please Enter minimum speed. Operation not Allowed',16,1)
--	return
--END

declare @LocationName VARCHAR(200),
		@ScheduleName VARCHAR(200),
		@LatDegree INT,
		@LatMinutes INT,
		@LatSecond DECIMAL(18,4),
		@LatSign varchar(10),
		@LatDegreeSimplified DECIMAL(18,6),
		@LongDegree INT,
		@LongMinutes INT,
		@LongSecond DECIMAL(18,4),
		@LongSign varchar(10),
		@LongDegreeSimplified DECIMAL(18,6),
		@LocationID BIGINT,
		@DateRelease DATETIME,
		@RaceScheduleID BIGINT

--get locationid
SELECT @LocationID = LocationID,
	   @RaceScheduleID = RaceScheduleID,
	   @DateRelease = DateRelease,
	   @ScheduleName = ScheduleName
from dbo.RaceScheduleDetails WHERE RaceScheduleDetailsID = @RaceScheduleDetailsID and ClubID = @ClubID

--get location details
SELECT @LocationName = LocationName,  
	    @LatDegree = DistanceLatDegree ,
		@LatMinutes = DistanceLatMinutes ,
		@LatSecond = DistanceLatSecond,
		@LatSign = DistanceLatSign,
		@LatDegreeSimplified = dbo.fnCalculateDegree(DistanceLatDegree,DistanceLatMinutes,DistanceLatSecond,DistanceLatSign),
		@LongDegree = DistanceLongDegree ,
		@LongMinutes = DistanceLongMinutes,
		@LongSecond = DistanceLongSecond ,
		@LongSign = DistanceLongSign,
		@LongDegreeSimplified = dbo.fnCalculateDegree(DistanceLongDegree,DistanceLongMinutes,DistanceLongSecond,DistanceLongSign)
FROM dbo.Location
WHERE LocationID = @LocationID and ClubID = @ClubID

IF (@RaceReleasePointID > 0)
BEGIN
	UPDATE	RaceReleasePoint
	SET RaceScheduleCategoryID=@RaceScheduleCategoryID,
		RaceScheduleDetailsID=@RaceScheduleDetailsID,
		ReleaseTime=@TimeReleased,
		Multiplier=@Multiplier,
		LapNo=@Lapno,
		MinSpeed=@MinSpeed,
		IsStop=@IsStop,
		StopFromDate=@StopFromDate,
		StopFromTime=@StopFromTime,
		StopToDate=@StopToDate,
		StopToTime=@StopToTime,
		StopTime=@StopTime,
		UpdatedBy=@UserID,
		DateUpdated=dbo.fnGetDate(),
		Description = @Description,
		DateRelease = @DateRelease,
		LocationID = @LocationID,
		LocationName = @LocationName,
		RaceScheduleID = @RaceScheduleID,
		RaceScheduleName = @ScheduleName,
		LatDegree = @LatDegree ,
		LatMinutes = @LatMinutes ,
		LatSecond = @LatSecond,
		LatSign = @LatSign,
		LatDegreeSimplified = @LatDegreeSimplified,
		LongDegree = @LongDegree ,
		LongMinutes = @LongMinutes,
		LongSecond = @LongSecond ,
		LongSign = @LongSign,
		LongDegreeSimplified = @LongDegreeSimplified
	WHERE RaceReleasePointID=@RaceReleasePointID and ClubID=@ClubID

	SET @Action = 'Update'

	END
ELSE
BEGIN
	INSERT INTO RaceReleasePoint
		(
		ClubID
		,RaceScheduleCategoryID
		,RaceScheduleDetailsID
		,RaceReleasePointID
		,ReleaseTime
		,Multiplier
		,LapNo
		,MinSpeed
		,IsStop
		,StopFromDate
		,StopFromTime
		,StopToDate
		,StopToTime
		,StopTime
		,CreatedBy
		,DateCreated
		,Description
		,DateRelease,
		LocationID,
		LocationName,
		RaceScheduleID,
		RaceScheduleName,
		LatDegree ,
		LatMinutes ,
		LatSecond ,
		LatSign,
		LatDegreeSimplified,
		LongDegree ,
		LongMinutes,
		LongSecond ,
		LongSign,
		LongDegreeSimplified
		)
	Values
		(
		@ClubID
		,@RaceScheduleCategoryID
		,@RaceScheduleDetailsID
		,@RaceReleasePointID
		,@TimeReleased
		,@Multiplier
		,@LapNo
		,@MinSpeed
		,@IsStop
		,@StopFromDate
		,@StopFromTime
		,@StopToDate
		,@StopToTime
		,@StopTime
		,@UserID
		,dbo.fnGetDate()
		,@Description
		,@DateRelease
		,@LocationID
		,@LocationName
		,@RaceScheduleID
		,@ScheduleName
		,@LatDegree
		,@LatMinutes
		,@LatSecond
		,@LatSign
		,@LatDegreeSimplified
		,@LongDegree
		,@LongMinutes
		,@LongSecond
		,@LongSign
		,@LongDegreeSimplified
		)
	
	set @RaceReleasePointID=IDENT_CURRENT('RaceReleasePoint')
	update RaceReleasePoint set RaceReleasePointID=@RaceReleasePointID,ExternalID = @RaceScheduleCategoryID where ID=@RaceReleasePointID
	SET @Action = 'Insert'
END

exec FileNotesSave
	@AccountName = 'RaceReleasePoint',
	@AccountID = @RaceReleasePointID,
	@Action = @Action,
	@CreatedBy = @UserID,
	@ClubID = @clubID

go