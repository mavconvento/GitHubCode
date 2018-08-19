drop table EntryIdentity
go
create table dbo.EntryIdentity
(
EntryIdentity bigint identity(1,1),
BarcodeID varchar(100),
BandID bigint,
MemberID bigint,
ClubID bigint,
ScheduleID bigint
)
go
drop proc dbo.EntryIdentitySave 
go
create proc dbo.EntryIdentitySave
	@BarcodeID varchar(100),
	@BandID bigint,
	@MemberID bigint,
	@ClubID bigint,
	@ScheduleID bigint
as set nocount on;
begin
	if not exists(select 1 from EntryIdentity where BarcodeID = @BarcodeID and ClubID = @ClubID and ScheduleID = @ScheduleID)
	begin
		insert into EntryIdentity values (@BarcodeID,@BandID,@MemberID,@ClubID,@ScheduleID)
	end	
	else
	begin
		update EntryIdentity set 
			BandID = @BandID,
			MemberID = @MemberID
		where ClubID = @ClubID and BarcodeID = @BarcodeID and ScheduleID = @ScheduleID
	end
end	
go
drop proc dbo.GetEntryIdentity 
go
create proc dbo.GetEntryIdentity
	@BarcodeID varchar(100),
	@ClubID bigint,
	@RaceScheduleName varchar(100)
as set nocount on;
begin
	declare @RaceScheduleID bigint

	select @RaceScheduleID = rc.RaceScheduleID from RaceSchedule rc where rc.RaceScheduleName = @RaceScheduleName and ClubID = @ClubID

	select ei.*,b.bandnumber,md.MemberIDNo from EntryIdentity ei
		inner join dbo.BandNumber b on ei.BandID = b.bandid
		inner join memberdetails md on ei.MemberID = md.MemberID
	where BarcodeID = @BarcodeID and ei.ClubID = @ClubID and ScheduleID = @RaceScheduleID
end
go
alter proc [dbo].[RaceScheduleDetailsDelete]
	@ClubId bigint,
	@UserID bigint,
	@ID BIGINT
as
begin

if EXISTS (SELECT TOP 1 1 FROM dbo.RaceScheduleDetails RS 
			INNER JOIN dbo.RaceReleasePoint RR ON RS.RaceScheduleDetailsID = RR.RaceScheduleDetailsID
			INNER JOIN dbo.Entry E ON E.RaceReleasePointID = E.RaceReleasePointID
			INNER JOIN dbo.RaceResult_Archive rsult ON rsult.EntryID = e.EntryID
		WHERE RR.RaceScheduleDetailsID = @ID)
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
--exec RaceScheduleSelectAll 0
alter proc [dbo].[RaceScheduleSelectAll]
	@ClubID varchar(100)
as
SELECT 'EDIT' AS " ",s.RaceScheduleID as "ID",RaceScheduleName as "Schedule Name",RegionName,
"Schedule Details"= case when sum(isnull(sd.racescheduleid,0)) > 0  then 'VIEW'
						 else 'CREATE' end
--"Schedule Category"= case when sum(isnull(sc.racescheduleid,0)) > 0  then 'VIEW'
--						 else 'CREATE' end

from RaceSchedule s (nolock)
	left join Region r (nolock) on s.RegionID=r.RegionID
	left join racescheduledetails sd (nolock) on s.racescheduleid=sd.racescheduleid and s.clubid=sd.clubid
	left join raceschedulecategory sc (nolock) on s.racescheduleid=sc.racescheduleid and s.clubid=sc.clubid and sc.IsActive=1
where s.Clubid=@Clubid and s.IsActive=1
group by s.racescheduleid,s.raceschedulename,RegionName
go



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
	update RaceReleasePoint set RaceReleasePointID=@RaceReleasePointID,ExternalID = @RaceReleasePointID where ID=@RaceReleasePointID
	SET @Action = 'Insert'
END

exec FileNotesSave
	@AccountName = 'RaceReleasePoint',
	@AccountID = @RaceReleasePointID,
	@Action = @Action,
	@CreatedBy = @UserID,
	@ClubID = @clubID





go
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





go
/*
2017.11
11.2017
*/
--exec [MemberDetailsSelectAll] 87,'','','',''
alter proc [dbo].[MemberDetailsSelectAll]
	@ClubID bigint,
	@ID varchar(100)='',
	@MemberIDNo varchar(100)='',
	@MemberName varchar(100)='',
	@MobileNumber varchar(100)=''
as
set nocount on;

SELECT DISTINCT
	m.ID
	,m.MemberID
	,MemberIDNO as "MemberID No."
	,dbo.fnMemberName(lastname,firstname,middlename,extensionname) as "Name"
	,dbo.fnDistance(distancelatdegree,distancelatminutes,distancelatseconds,distancelatsign) + ' - ' + dbo.fnDistance(distancelongdegree,distancelongminutes,distancelongseconds,distancelongsign)as "Coordinates"
	,dbo.RegisteredMobileNumber(m.MemberID) as "Register Mobile No."
INTO #RESULT
FROM memberdetails (nolock) m
WHERE m.ClubID=@clubid and m.IsActive=1
	  and (@ID='' or @ID='0' or m.MemberID=@ID)
	  and (@MemberIDNo='' or MemberIDNo=@MemberIDNo)
	  and (@MemberName='' or ((LastName like '%' + @MemberName + '%') or (FirstName like '%' + @MemberName + '%')))
ORDER BY m.ID

SELECT a.MemberID,
		'SELECT' AS "  ",
		'EDIT' AS " ",
		ROW_NUMBER() OVER (ORDER BY memberid ASC) AS ROWID,
		a.[MemberID No.],
		a.Name,
		a.Coordinates,
		a.[Register Mobile No.] 
FROM #RESULT a
where (@MobileNumber='' or a.[Register Mobile No.] like '%'+ @MobileNumber +'%')


go
/*exec EntrySave 
	@EntryID=0,@ClubID=8,
	@RaceReleasePointID=31,
	@RaceScheduleName=N'Test',
	@RaceScheduleCategoryName=N'Test Pooling',
	@RaceCategory=N'Young Bird(s)',
	@RaceCategoryGroup=N'None',
	@MemberID=0,
	@StickerCode=N'444',
	@RingNumber=N'PHIL 2012-12346',
	@UserID=0,
	@EntryRemarks=N'TEST 1',
	@MemberIDNo=N'0001',
	@IsUpload=1
*/
alter proc [dbo].[EntrySave]
	@ClubID bigint,
	@RaceReleasePointID bigint,
	@RaceScheduleName varchar(100),
	@RaceScheduleCategoryName varchar(100),
	@RaceCategory varchar(100),
	@RaceCategoryGroup varchar(100),
	@MemberID bigint=0,
	@StickerCode varchar(50)='',
	@RingNumber varchar(50)='',
	@MemberIDNo varchar(20)='',
	@UserID bigint,
	@IsUpload bit = 0,
	@BandID bigint = 0,
	@BarCodeEntryID varchar(100)= '',
	@EntryRemarks VARCHAR(100)='',
	@EntryID BIGINT = 0 output,
	@ERROR VARCHAR(1000) = '' OUTPUT
as
declare @RaceCategoryID bigint,
		@RaceCategoryGroupID BIGINT,
		@RaceScheduleID bigint,
		@RaceLapNo bigint,
		@RaceScheduleCategoryID bigint,
		@MEMBERNAME VARCHAR(100),
		@BANDNumber VARCHAR(100),
		@Remarks VARCHAR(100)='',
		@LatDegreeSimplified DECIMAL(18,6),
		@LongDegreeSimplified DECIMAL(18,6),
		@IsAllowDoubleEntry BIT

if (@MemberID=0 and @MemberIDNo<>'') select @MemberID=MemberID from MemberDetails where MemberIDNo=@MemberIDNo and ClubID=@ClubID

SELECT @IsAllowDoubleEntry = AllowDoubleEntry FROM dbo.Club WHERE ClubID = @ClubID

SELECT @LatDegreeSimplified = LatDegreeSimplified,
	   @LongDegreeSimplified = LongDegreeSimplified,
	   @MemberName = case when isnull(LoftName,'') <> '' then LoftName
						  else dbo.fnMemberNameFormal(lastname,firstname,middlename,extensionname) 
					 end
FROM dbo.MemberDetails WHERE MemberID = @MemberID AND ClubID = @ClubID

select @RaceScheduleID=RaceScheduleID from RaceSchedule (Nolock) where raceschedulename=@RaceScheduleName and ClubID=@ClubID
select @RaceCategoryID = RaceCategoryID from RaceCategory (Nolock) where Description=@RaceCategory and ClubID = @ClubID
select @RaceCategoryGroupID=RaceCategoryGroupID from RaceCategoryGroup (Nolock) where RaceCategoryGroupName=@RaceCategoryGroup and ClubID=@ClubID 
select @RaceLapNo=LapNo from RaceReleasePoint (Nolock) where RaceReleasePointID=@RaceReleasePointID and ClubID=@ClubID
select @RaceScheduleCategoryID=RaceScheduleCategoryID from RaceScheduleCategory (Nolock) where RaceScheduleCategoryName=@RaceScheduleCategoryName and ClubID=@ClubID

IF (@IsUpload=1)
BEGIN
	IF NOT EXISTS (SELECT 1 FROM dbo.MemberDetails WHERE MemberIDNo=@MemberIDNo AND ClubID=@ClubID)
	BEGIN
		SET @ERROR='Member ID: ' + @MemberIDNo + ' is not exists.' 
	END
	ELSE IF (ISNULL(@RaceCategoryGroupID,0) = 0 OR ISNULL(@RaceCategoryID,0) = 0)
	BEGIN
		IF (ISNULL(@RaceCategoryGroupID,0) = 0) SET @ERROR='Invalid Race Category Group.'
		ELSE SET @ERROR='Invalid Race Category.'
	END	
	ELSE if ((select COUNT(1) from BandNumber where BandNumber=@ringnumber and MemberID=@MemberID and ClubID=@ClubID) = 0)
	begin
		if (@BandID > 0)
			begin
				update BandNumber set BandNumber=@RingNumber where BandID=@BandID AND ClubID = @ClubID
			end
		else
			begin
				EXEC [MemberRingEnrolledSave]
					@ClubID = @ClubID,
					@UserID = @UserID,
					@MemberRingEnrolledID = 0,
					@MemberID = @MemberID,
					@RaceScheduleName = @RaceScheduleName,
					@RaceScheduleCategoryName = @RaceScheduleCategoryName,
					@RingNumber = @RingNumber,
					@IsUpload=@IsUpload,
					@ErrorRemarks=@ERROR OUTPUT,
					@BandID=@bandID OUTPUT
			end
	end
END
--select @ringnumber,@MemberID,@ClubID,@StickerCode,@RaceReleasePointID
IF (@StickerCode = '')
	BEGIN
		SET @ERROR='Sticker code not set'
	END
else if (@RingNumber='')
	begin
		SET @ERROR='Ring Number not set'
	END
else if ((select COUNT(1) from BandNumber where BandNumber=@ringnumber and MemberID=@MemberID and ClubID=@ClubID) = 0 AND @IsUpload=0)
	begin
		--SET @ERROR='This band number is not registered.'
		
		EXEC [MemberRingEnrolledSave]
				@ClubID = @ClubID,
				@UserID = @UserID,
				@MemberRingEnrolledID = 0,
				@MemberID = @MemberID,
				@RaceScheduleName = @RaceScheduleName,
				@RaceScheduleCategoryName = @RaceScheduleCategoryName,
				@RingNumber = @RingNumber,
				@IsUpload=@IsUpload,
				@ErrorRemarks=@ERROR OUTPUT,
				@BandID=@bandID OUTPUT
	end
ELSE IF EXISTS (SELECT 1 FROM Entry E 
					INNER JOIN BandNumber B ON E.BandID=B.BandID AND B.ClubID=@ClubID
					where RaceReleasePointID=@RaceReleasePointID and @EntryID=0 and E.MemberID=@MemberID and E.BandNumber=@RingNumber AND e.ClubID = @ClubID)
	BEGIN
		SET @ERROR='Duplicate Entry with band number: ' + @RingNumber
	END
ELSE IF EXISTS (SELECT 1 FROM Entry where RaceReleasePointID=@RaceReleasePointID and MemberID=@MemberID and StickerCode=@StickerCode AND ClubID = @ClubID and @EntryID=0)
	BEGIN
		select @BANDNumber=b.BandNumber 
			FROM Entry E (NOLOCK)
				INNER JOIN BandNumber B ON E.BandID=B.BandID AND B.ClubID=@ClubID
			WHERE RaceReleasePointID=@RaceReleasePointID and StickerCode=@StickerCode AND e.ClubID = @ClubID

		SET @ERROR='This sticker is already used.' + CHAR(13) +'Band number ' + @BANDNumber + '.' + CHAR(13) + ' Invalid sticker duplication.'
	END
ELSE IF (LEN(@StickerCode) = 5)
	BEGIN
		IF NOT EXISTS(SELECT 1 FROM pigeon_mavcpigeonclocking.dbo.StickerNumber WHERE Outter = @StickerCode)
		BEGIN
			SET @ERROR='Invalid sticker code. Please check your banding form.'	
		END
	END
ELSE IF (LEN(@StickerCode) < 5 AND @IsAllowDoubleEntry = 0)
	BEGIN
		SET @ERROR='Invalid sticker code. Please check your banding form.'	
	END
	
IF (@ERROR <> '' AND @IsUpload=0)
	BEGIN
		RAISERROR(@ERROR,16,1)	
		RETURN
	END
ELSE IF (@ERROR<>'' AND @IsUpload=1)
	BEGIN
		SELECT @ERROR "Remarks"
		RETURN
	END

--DECLARE @BANDIDs bigint
declare @Action varchar(20)
select @BANDID=bandid from BandNumber where BandNumber=@RingNumber and MemberID=@MemberID AND ClubID = @ClubID

if (isnull(@BarCodeEntryID,'') != '')
begin
	exec dbo.EntryIdentitySave	
		@BarcodeID = @BarCodeEntryID,
		@BandID = @BandID,
		@MemberID = @MemberID,
		@ClubID = @ClubID
end

if (@EntryID > 0)
begin
	Update Entry
	Set	EntryID=@EntryID
		,ClubID=@ClubID
		,RaceReleasePointID=@RaceReleasePointID
		,RaceCategoryID=@RaceCategoryID
		,RaceCategoryGroupID=@RaceCategoryGroupID
		,MemberID=@MemberID
		,StickerCode=@StickerCode
		,BandID=@BANDID
		,UpdatedBy=@UserID
		,DateUpdatedBy=dbo.fnGetDate()
		,Remarks=@EntryRemarks
		,EntryBarcodeID=@BarCodeEntryID
		,LatDegreeSimplified = @LatDegreeSimplified
		,LongDegreeSimplified = @LongDegreeSimplified
		,MemberName = @MemberName
		,BandNumber = @RingNumber
	Where EntryID = @EntryID
	set @action = 'Update'

	if exists(select 1 from dbo.entrycategory where categoryid = @RaceCategoryGroupID)
	begin
	DELETE dbo.entrycategory 
	where categoryid = @RaceCategoryGroupID
		and memberid = @memberid
		and entryid = @EntryID
	end
end
else
begin
	Insert Into Entry
		(
		EntryID
		,ClubID
		,RaceReleasePointID
		,RaceCategoryID
		,RaceCategoryGroupID
		,MemberID
		,StickerCode
		,BandID
		,CreatedBy
		,DateCreated
		,Remarks
		,EntryBarcodeID
		,LatDegreeSimplified
		,LongDegreeSimplified
		,BandNumber
		,MemberName
		)
	values
		(
		@EntryID
		,@ClubID
		,@RaceReleasePointID
		,@RaceCategoryID
		,@RaceCategoryGroupID
		,@MemberID
		,@StickerCode
		,@BANDID
		,@UserID
		,dbo.fnGetDate()
		,@EntryRemarks
		,@BarCodeEntryID
		,@LatDegreeSimplified
		,@LongDegreeSimplified
		,@RingNumber
		,@MemberName
		)
	
	Set @EntryID=@@identity
	update Entry set entryID=@EntryID where id=@EntryID
	
	set @action='Insert'
	SELECT 'Success' AS "REMARKS"
	SELECT @BandID AS "BANDID"
end

exec FileNotesSave
	@AccountName = 'Entry',
	@AccountID = @EntryID,
	@Action = @Action,
	@CreatedBy = @UserID,
	@ClubID = @ClubID




go

/*
exec EntryGetByRaceReleasePoint 
	@ClubID=5,
	@RaceReleasePointID=694,
	@RaceCategory=N'None',
	@RaceCategoryGroup=N'Pooling',
	@MemberID=22846
11.2017
*/
alter PROC [dbo].[EntryGetByRaceReleasePoint]
	@ClubID bigint,
	@RaceReleasePointID bigint,
	@RaceCategory varchar(100),
	@RaceCategoryGroup varchar(100),
	@MemberID BIGINT = 0
AS SET NOCOUNT ON; 
declare @RaceCategoryID bigint,
		@RaceCategoryGroupID bigint

declare @RaceReleasePointTable table (RaceReleasePointID bigint,LocationName varchar(100))
declare @MemberTable table (MemberID bigint,MemberIDNo varchar(100),LastName varchar(100),FirstName varchar(100),MiddleName varchar(100),ExtensionName varchar(100),Loftname VARCHAR(100))

insert into @RaceReleasePointTable
select RaceReleasePointID,r.LocationName 
from RaceReleasePoint R (Nolock)
		--inner join RaceScheduleDetails sd (Nolock) on sd.ID=r.RaceScheduleDetailsID
		--inner join Location L (Nolock) on sd.LocationID=L.LocationID  and l.IsActive=1
where RaceReleasePointID=@RaceReleasePointID and R.ClubID=@ClubID and r.IsActive=1

insert into @MemberTable
	select MemberID,MemberIDNo,LastName,FirstName,MiddleName,ExtensionName,LoftName from MemberDetails (Nolock) where ClubID=@ClubID and IsActive=1

select @RaceCategoryID=RaceCategoryID from RaceCategory (Nolock) where Description=@RaceCategory and ClubID=@ClubID and IsActive=1
select @RaceCategoryGroupID=RaceCategoryGroupID from RaceCategoryGroup (Nolock) where RaceCategoryGroupName=@RaceCategoryGroup and ClubID=@ClubID and IsActive=1

IF (@MemberID = 0)
	BEGIN
		select EntryID 
			,MemberIDNo as "MemberID No."
			,[Member Name]= CASE WHEN ISNULL(loftname,'') != '' THEN loftname ELSE dbo.fnMemberName(LastName,FirstName,MiddleName,ExtensionName) END
			,BandID AS "Band ID"
			,BandNumber as "Ring Number"
			,StickerCode as "Sticker Code"
			,EntryBarcodeID AS "EntryBarcodeID"
			,c.Description + '/' + g.RaceCategoryGroupName AS "Category/Group"
			--,'EDIT' AS " "
		from Entry E (Nolock)
			--INNER JOIN BandNumber B (NOLOCK)ON B.BandID=E.BandID and b.ClubID=@ClubID
			inner join RaceCategory C (Nolock) on E.RaceCategoryID=C.RaceCategoryID and c.IsActive=1
			inner join RaceCategoryGroup G (Nolock) on E.RaceCategoryGroupID=G.RaceCategoryGroupID and g.IsActive=1
			inner join @RaceReleasePointTable R on R.RaceReleasePointID=E.RaceReleasePointID
			inner join @MemberTable M on M.MemberID=E.MemberID
		where E.RaceCategoryID=@RaceCategoryId
			and E.ClubID=@ClubID
			and E.RaceReleasePointID=@RaceReleasePointID
			and E.IsActive=1
			and E.RaceCategoryGroupID=@RaceCategoryGroupID
			--AND (m.MemberID = @MemberID OR @MemberID=0)
		order by EntryID DESC
	END
ELSE
	BEGIN
		select EntryID 
			,'EDIT' AS " "
			,'DELETE' AS "  "
			,MemberIDNo as "MemberID No."
			,[Member Name]= CASE WHEN ISNULL(loftname,'') != '' THEN loftname ELSE dbo.fnMemberName(LastName,FirstName,MiddleName,ExtensionName) END
			,BandID AS "Band ID"
			,e.BandNumber as "Ring Number"
			,StickerCode as "Sticker Code"
			,EntryBarcodeID AS "EntryBarcodeID"
			--,c.Description + '/' + g.RaceCategoryGroupName as "Category/Group"
			,c.Description as "Category"
			,g.RaceCategoryGroupName AS "Group"
			,dbo.fnGetEntryCategory(e.MemberID,EntryID) as "Other Group Category"
			,' + ' AS "Add Group Category"
		from Entry E (Nolock)
			--INNER JOIN BandNumber B (NOLOCK)ON B.BandID=E.BandID and b.ClubID=@ClubID
			inner join RaceCategory C (Nolock) on E.RaceCategoryID=C.RaceCategoryID and c.IsActive=1
			inner join RaceCategoryGroup G (Nolock) on E.RaceCategoryGroupID=G.RaceCategoryGroupID and g.IsActive=1
			inner join @RaceReleasePointTable R on R.RaceReleasePointID=E.RaceReleasePointID
			inner join @MemberTable M on M.MemberID=E.MemberID
		where  E.ClubID=@ClubID
			and E.RaceReleasePointID=@RaceReleasePointID
			and E.IsActive=1
			and m.MemberID = @MemberID
		order by EntryID DESC
	END
	
go

