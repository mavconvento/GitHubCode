ALTER PROC [dbo].[RaceScheduleDetailsSave]
	@ClubID BIGINT,
	@UserID BIGINT,	
	@ID BIGINT,
	@RaceScheduleID BIGINT,
	@LocationID BIGINT,
	@DateRelease DATETIME,
	@Loading datetime,
	@LoadingTimeFrom varchar(100),
	@LoadingTimeTo varchar(100),
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
	@Description VARCHAR(1000)

as

declare @StopTime bigint
declare @Action varchar(20)

set @StopTime=dbo.fnTimetoMinutes(dbo.fnFlight(
convert(datetime,(convert(varchar(10),@StopFromDate,101) + ' ' + @StopFromTime)),
convert(datetime,(convert(varchar(10),@StopToDate,101) + ' ' + @StopToTime))
))

IF (@TimeReleased = ':') SET @TimeReleased = ''

if @LocationID=0 
begin
	raiserror('Invalid Location Selection',16,1)
	return
end

if exists (select 1 from RaceScheduleDetails rd
			inner join location loc on rd.locationid=loc.locationid
			where rd.id<>@ID and DateRelease=@DateRelease and RaceScheduleID=@RaceScheduleID and rd.ClubID=@ClubID)
Begin
	raiserror('Release Date already assigned.',16,1)
	return
End

--from race release point save sp
IF (charindex('a',@TimeReleased,1) > 0 or charindex('p',@TimeReleased,1) > 0 or charindex('m',@TimeReleased,1) > 0)
	begin
		raiserror('Please remove "AM" or "PM" on release time. Invalid time value.',16,1)
		return
	END
IF (charindex(':',@TimeReleased,1) = 0 AND @TimeReleased != '')
	begin
		raiserror('Invalid Time format. Invalid time value.',16,1)
		return
	END
IF (LEN(@TimeReleased) != 5 AND @TimeReleased != '')
BEGIN
	raiserror('Invalid Time format. Invalid time value.',16,1)
	return
END

IF (@MinSpeed = '')
BEGIN
	raiserror('Please Enter minimum speed. Operation not Allowed',16,1)
	return
END

--delete unbind racescheduledetails
delete from RaceScheduleDetails where 
	DateRelease = @DateRelease and 
	clubid=@clubid and 
	RaceScheduleDetailsID<>@ID
	
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
		@ResultCount BIGINT,
		@RaceScheduleDetailsID Bigint,
		@RaceScheduleCategoryID bigint,
		@RaceScheduleCategoryName varchar(100)

--get location name
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
WHERE LocationID = @LocationID

--get ScheduleName
SELECT @ScheduleName = RaceScheduleName from dbo.RaceSchedule WHERE RaceScheduleID = @RaceScheduleID 

--set RaceScheduleCategoryName
SET @RaceScheduleCategoryName = @ScheduleName + '-' + @LocationName + '-' + CONVERT(VARCHAR(20),@DateRelease,101) 

if (@ID > 0)
	begin
	
    if EXISTS (SELECT TOP 1 1 FROM dbo.RaceScheduleDetails RS 
					INNER JOIN dbo.RaceReleasePoint RR ON RS.RaceScheduleDetailsID = RR.RaceScheduleDetailsID
					INNER JOIN dbo.Entry E ON E.RaceReleasePointID = rr.RaceReleasePointID
					INNER JOIN dbo.RaceResult_Archive rsult ON rsult.EntryID = e.EntryID
				WHERE RR.RaceScheduleDetailsID = @ID)
	Begin
		raiserror('Race Details has result. Operation not allowed',16,1)
		return
	END
    

	UPDATE RaceScheduleDetails
	set DateRelease=@DateRelease,
		Loading=@Loading,
		LoadingTimeFrom=@LoadingTimeFrom,
		LoadingTimeTo=@LoadingTimeTo,
		UpdatedBy=@UserID,
		LocationiD=@LocationID,
		LocationName = @LocationName,
		DateUpdated=getdate(),
		RaceScheduleID=@RaceScheduleID,
		ScheduleName = @ScheduleName
	where RaceScheduleDetailsID=@ID 
		  and ClubID=@ClubID
	
	SET @RaceScheduleDetailsID = @ID

	SELECT @RaceScheduleCategoryID = rr.RaceScheduleCategoryID from RaceReleasePoint rr 
	where RaceReleasePointID = @RaceReleasePointID and ClubID = @ClubID

	--update record on racereleasepoint if record exists
	IF EXISTS(SELECT 1 FROM dbo.RaceReleasePoint WHERE RaceScheduleDetailsID = @ID)
	BEGIN
		exec [dbo].[RaceReleasePointSave]
			@ClubID = @ClubID,
			@RaceScheduleCategoryID = @RaceScheduleCategoryID,
			@RaceScheduleDetailsID = @RaceScheduleDetailsID,
			@RaceReleasePointID = @RaceReleasePointID,
			@TimeReleased = @TimeReleased,
			@Multiplier = @Multiplier,
			@LapNo = @LapNo,
			@MinSpeed = @MinSpeed,
			@IsStop = @IsStop,
			@StopFromDate = @StopFromDate,
			@StopFromTime = @StopFromTime,
			@StopToDate = @StopToDate,
			@StopToTime = @StopToTime,
			@UserID = @UserID,
			@Description = @Description
	END

	--UPDATE RECORD ON RaceScheduleCategory
	exec [dbo].[RaceScheduleCategorySave]
		@ClubID = @ClubID,
		@UserID = @UserID,
		@RaceScheduleID = @RaceScheduleID,
		@RaceScheduleCategoryID = @RaceScheduleCategoryID,
		@RaceScheduleCategoryName = @RaceScheduleCategoryName,
		@Lap = @LapNo
	 
	SET @action = 'Update'	
	END
else
	begin
	if exists (select 1 from RaceScheduleDetails where DateRelease=@DateRelease and RaceScheduleID=@RaceScheduleID and ClubID=@ClubID)
	Begin
		RAISERROR('Date Release is already exists.',16,1)
		RETURN
	End

	insert into RaceScheduleDetails
		(
		RaceScheduleDetailsID,
		ClubID,
		RaceScheduleID,
		ScheduleName,
		LocationId,
		LocationName,
		DateRelease,
		Loading,
		LoadingTimeFrom,
		LoadingTimeTo,
		Createby,
		Datecreated
		)
	values
		(
		IDENT_CURRENT('RaceScheduleDetails'),
		@ClubID,
		@RaceScheduleID,
		@ScheduleName,
		@LocationID,
		@LocationName,
		@DateRelease,
		@Loading,
		@LoadingTimeFrom,
		@LoadingTimeTo,
		@UserID,
		getdate()
		)
	
	select @RaceScheduleDetailsID = rs.RaceScheduleDetailsID  from RaceScheduleDetails rs where DateRelease=@DateRelease and RaceScheduleID=@RaceScheduleID and ClubID=@ClubID
	
	--save record in RaceScheduleCategory
	exec [dbo].[RaceScheduleCategorySave]
		@ClubID = @ClubID,
		@UserID = @UserID,
		@RaceScheduleID = @RaceScheduleID,
		@RaceScheduleCategoryID = 0,
		@RaceScheduleCategoryName = @RaceScheduleCategoryName,
		@Lap = @LapNo
	
	SELECT @RaceScheduleCategoryID = RS.RaceScheduleCategoryID FROM RaceScheduleCategory RS WHERE RS.RaceScheduleCategoryName = @RaceScheduleCategoryName

	--save record in RaceReleasePoint
	exec [dbo].[RaceReleasePointSave]
		@ClubID = @ClubID,
		@RaceScheduleCategoryID = @RaceScheduleCategoryID,
		@RaceScheduleDetailsID = @RaceScheduleDetailsID,
		@RaceReleasePointID = 0,
		@TimeReleased = @TimeReleased,
		@Multiplier = @Multiplier,
		@LapNo = @LapNo,
		@MinSpeed = @MinSpeed,
		@IsStop = @IsStop,
		@StopFromDate = @StopFromDate,
		@StopFromTime = @StopFromTime,
		@StopToDate = @StopToDate,
		@StopToTime = @StopToTime,
		@UserID = @UserID,
		@Description = @Description
		
	set @ID = @@identity
	set @Action = 'Insert'
	end

exec FileNotesSave
	@AccountName = 'RaceScheduleDetails',
	@AccountID = @ID,
	@Action = @Action,
	@CreatedBy = @UserID,
	@ClubID = @ClubID







