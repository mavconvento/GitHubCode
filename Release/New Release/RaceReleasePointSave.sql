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
	update RaceReleasePoint set RaceReleasePointID=@RaceReleasePointID,ExternalID = @RaceReleasePointID where ID=@RaceReleasePointID
	SET @Action = 'Insert'
END

exec FileNotesSave
	@AccountName = 'RaceReleasePoint',
	@AccountID = @RaceReleasePointID,
	@Action = @Action,
	@CreatedBy = @UserID,
	@ClubID = @clubID




