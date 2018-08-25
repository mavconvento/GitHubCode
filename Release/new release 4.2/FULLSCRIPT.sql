/*
exec [RaceResultSave] 
	@Content = '',
	@Sender = '+639985754822',
	@StickerNumber = '29132*084493240',
	@Arrival = '11/22/2017 18:42:28',
	@RaceReleaseDate = '11/22/2017 12:00:00 AM',
	@Source = 'Back-up',
	@ClubID = 96
	
version3.0
11.2017
*/
alter PROC [dbo].[RaceResultSave]
	@Content varchar(100),
	@Sender varchar(100),
	@StickerNumber varchar(100),
	@Arrival varchar(25),
	@Source varchar(100) = '',
	@ClubID BIGINT = 0,
	@RaceReleaseDate DATETIME = NULL,
	@RaceResultID BIGINT = 0 OUTPUT ,
	@Isvalid bit = 0 output ,
	@Remarks varchar(1000) = '' OUTPUT,
	@Name varchar(500) = '' OUTPUT,
	@Distance varchar(100) = '' OUTPUT,
	@RingNumber varchar(100) = '' OUTPUT,
	@Code varchar(100) = '' OUTPUT,
	@Flight varchar(100) = '' OUTPUT,
	@Speed varchar(100) = '' OUTPUT,
	@MemberID BIGINT = '' OUTPUT
AS SET NOCOUNT ON;
BEGIN
SET NOCOUNT ON;
DECLARE @StickerCode varchar(50),
		@ClubName varchar(10),
		@EntryID BIGINT = 0,
		@ReleasePointID bigint,
		@Delimiter varchar(1),
		@ArrivalDate datetime,
		@IsMAVCStickerUsed BIT,
		@StickerInner VARCHAR(100)='',
		@StickerOutter VARCHAR(100),
		@PrevRaceResultID int = 0,
		@TimeZoneDiff int = 0,
		@AllowDoubleEntry BIT

SET @RaceResultID = 0

if (LEN(@Sender) = 11)
BEGIN
	SET @Sender = '+63' + SUBSTRING(@Sender,2,10)
END

IF (@Source = 'Back-up')
BEGIN
	SELECT @IsMAVCStickerUsed=IsMAVCStickerUsed,@TimeZoneDiff = isnull(TimeZoneDiff,0),@AllowDoubleEntry = AllowDoubleEntry from Club where ClubID = @ClubID
END
ELSE	
BEGIN	
	SELECT @ClubName=[value] from dbo.fnSplitString(@Content,' ') WHERE ID=2
	SELECT @ClubID=ClubID,@IsMAVCStickerUsed=IsMAVCStickerUsed,@TimeZoneDiff = isnull(TimeZoneDiff,0) from Club where ClubAbbreviation=@ClubName 
END

SET @ArrivalDate=CONVERT(datetime,@Arrival,120)
SET @ArrivalDate=DATEADD(HOUR,@TimeZoneDiff,@ArrivalDate)

IF EXISTS(SELECT 1 FROM SMSRegisteredNumber where MobileNumber = @Sender and IsActive=1 and ClubID=@ClubID)
	BEGIN
		IF (CHARINDEX('*',@StickerNumber,1)> 0)
			BEGIN
			SET @Delimiter='*'
			SET @StickerNumber = REPLACE(@StickerNumber,@Delimiter,'*')
			END
		ELSE IF (CHARINDEX(' ',@StickerNumber,1)> 0)
			BEGIN
			SET @Delimiter=' '
			SET @StickerNumber = REPLACE(@StickerNumber,@Delimiter,'*')
			END
		
		ELSE IF (CHARINDEX('#',@StickerNumber,1)> 0)
			BEGIN
			SET @Delimiter='#'
			SET @StickerNumber = REPLACE(@StickerNumber,@Delimiter,'*')
			END
		
		SELECT @MemberID=MemberID FROM SMSRegisteredNumber WHERE MobileNumber=@Sender and ClubID=@ClubID AND IsActive = 1
		SELECT @StickerCode=[value] from dbo.fnSplitString(@StickerNumber,'*') WHERE ID=1
		SELECT @StickerInner=[value] from dbo.fnSplitString(@StickerNumber,'*') WHERE ID=2	

		IF (@Source = 'Back-up')
			BEGIN
			IF (@AllowDoubleEntry = 1)
				BEGIN
					IF NOT EXISTS (SELECT 1 FROM pigeon_mavcpigeonclocking.dbo.StickerNumber WHERE [Inner] = @StickerInner) AND (LEN(@StickerCode) = 5) 
					BEGIN
						SET @Remarks='Invalid Sticker code.'
						GOTO ERROR;
					END
				END
			ELSE	
				BEGIN
				IF NOT EXISTS (SELECT 1 FROM pigeon_mavcpigeonclocking.dbo.StickerNumber WHERE [Inner] = @StickerInner)
					BEGIN
						SET @Remarks='Invalid Sticker code.'
						GOTO ERROR;
					END
				END
			END


		IF (@RaceReleaseDate IS NOT NULL)
		BEGIN
			SELECT top 1 @ReleasePointID=RaceReleasePointID from RaceReleasePoint rs (nolock)
			where rs.ClubID=@ClubID 
				and CONVERT(DATE,DateRelease,101) = CONVERT(DATE,@RaceReleaseDate,101)
			order by DateRelease DESC,rs.RaceReleasePointID DESC	
		END
		ELSE
		BEGIN
			SET @ReleasePointID=dbo.fnGetRaceReleasePointID(@ClubID) 
		END

		--PRINT @ReleasePointID

		SELECT @EntryID = EntryID from dbo.Entry (NOLOCK)
		WHERE MemberID = @MemberID 
			AND StickerCode = @StickerCode
			AND RaceReleasePointID=@ReleasePointid
		 
		SET @StickerOutter = @StickerCode

		--print 'memberid:' + cast(@MemberID as varchar(10))
		--print 'racereleasepoint:' + cast(@ReleasePointID as varchar(10))
		--print 'stickercode:' + cast(@StickerCode as varchar(10))
		--print 'entryid:' + cast(@EntryID as varchar(10))
		
		if (CHARINDEX('-',@StickerNumber,1) > 0)
			begin
			set @Remarks = 'Invalid Text Format'
			end
		else if (ISNUMERIC(@StickerCode) = 0)
			begin
			SET @Remarks = 'Invalid Text Format'
			END
		ELSE IF (@StickerInner = '')
			BEGIN
			SET @Remarks = 'Invalid Sticker Number'	
			END
		else IF (ISNULL(@EntryID,0) > 0)
			begin
				if not exists (Select top 1 1 from RaceResult rr (nolock) 
							   WHERE rr.EntryID = @EntryID 
									AND StickerNumber = @StickerNumber  
									AND rr.IsActive = 1)
					BEGIN
						
						--insert record into raceresult table
						EXEC dbo.RaceResult_Insert
							@ReleasePointID = @ReleasePointID,
							@EntryID = @EntryID,
							@MemberID = @MemberID,
							@IsMAVCStickerUsed = @IsMAVCStickerUsed,
							@StickerNumber = @StickerNumber,
							@ClubID = @ClubID,
							@RaceReleaseDate = @RaceReleaseDate,
							@ArrivalDate = @ArrivalDate,
							@Source = @Source,
							@RaceResultID = @RaceResultID OUTPUT

						--PRINT 'RaceResultID:' + cast(@RaceResultID as varchar(10))
						SELECT	@Name = [Name],
								@Distance = [Distance],
								@RingNumber = [RingNumber],
								@Code = [Code],
								@Arrival = [Arrival],
								@Flight = [Flight],
								@Speed = [Speed]
						FROM dbo.RaceResultDetails_v3 where RaceResultID = @RaceResultID

						SET @IsValid=1
					end
				else
				BEGIN
					DECLARE @arr DATETIME2

					select @RaceResultID=RaceResultiD,@arr = Arrival, @RingNumber = BandNumber, @StickerNumber = StickerNumber
					from RaceResult rr
						INNER JOIN entry e on rr.EntryID = e.EntryID AND e.EntryID = @EntryID
					WHERE rr.EntryID = @EntryID AND rr.IsActive = 1

					IF (DATEDIFF(SECOND,@arr,@ArrivalDate) < 0)
					BEGIN
						--insert record into raceresult table
						EXEC dbo.RaceResult_Insert
							@ReleasePointID = @ReleasePointID,
							@EntryID = @EntryID,
							@MemberID = @MemberID,
							@IsMAVCStickerUsed = @IsMAVCStickerUsed,
							@StickerNumber = @StickerNumber,
							@ClubID = @ClubID,
							@RaceReleaseDate = @RaceReleaseDate,
							@ArrivalDate = @ArrivalDate,
							@Source = @Source,
							@RaceResultID = @RaceResultID OUTPUT

						--PRINT 'RaceResultID:' + cast(@RaceResultID as varchar(10))
						SELECT	@Name = [Name],
								@Distance = [Distance],
								@RingNumber = [RingNumber],
								@Code = [Code],
								@Arrival = [Arrival],
								@Flight = [Flight],
								@Speed = [Speed]
						FROM dbo.RaceResultDetails_v3 where RaceResultID = @RaceResultID

						SET @IsValid=1	
					END
					ELSE	
					BEGIN	

						SET @Remarks = 'Bird Already Clock' + char(13) +
									   'Arrival: ' + CONVERT(varchar(10),@arr,101) + ' ' + CONVERT(varchar(10),@arr,108) + char(13) +
									   'Band No.: ' + @RingNumber + char(13) +
									   'Sticker No.: ' + @StickerNumber
					END
				end
			end
		Else
			BEGIN
				--SELECT @StickerCode,@sender,@ClubID
				if not exists (select 1 from smsregisterednumber rn (nolock)
							inner join entry e (nolock) on rn.memberID=e.MemberID and e.RaceReleasePointID=@ReleasePointID
							where mobilenumber=@sender and rn.IsActive=1 and rn.ClubID=@ClubID)	
					begin
					SET @Remarks = 'No Bird Entry'	
					end	
				else if not exists (select 1 from smsregisterednumber rn (nolock)
						inner join entry e (nolock) on rn.memberID=e.MemberID and e.Stickercode=@StickerCode and e.IsActive=1
						where mobilenumber=@sender and rn.IsActive=1 and rn.ClubID=@ClubID and e.RaceReleasePointID=@ReleasePointID)
					BEGIN
					SET @Remarks = 'Invalid Sticker Code'	
					end
				else
					begin
					SET @Remarks = 'Invalid Text Format'
					end
			end
	END
Else
	begin
		SET @Remarks = 'This number is not registered.'
	END

--insert into raceresult details if not exist
--this will assure that all record on race result has insert into raceresultdetails_v3
IF (@RaceResultID > 0)
BEGIN
	IF NOT EXISTS (SELECT 1 FROM dbo.RaceResultDetails_v3 WHERE RaceResultID = @RaceResultID)
	BEGIN
		--Insert into detail table to faster result viewing
		INSERT INTO dbo.RaceResultDetails_v3
		EXEC dbo.LoadBalanceRaceResult @RaceResultID = @RaceResultID,@ClubID = @ClubID,@RaceReleaseDate = @RaceReleaseDate
	END
END

--return to UI to return error.
ERROR:
IF @Source = 'Back-up' AND @Remarks != ''
BEGIN
	RAISERROR(@Remarks,16,1)
END

END

go
/*
--Return Table for Race Result Details
declare @ClubID bigint
select @ClubID = clubID from club where clubabbreviation = 'TCPC'
exec fred 'fnGetRaceResultDetailsV3'
	@RaceResultID = 0,
	@ClubID = 79,
	@RaceReleaseDate = '2017-02-19'
*/
create PROC [dbo].[LoadBalanceRaceResult]
	@RaceResultID BIGINT,
	@ClubID BIGINT,
	@RaceReleaseDate DATETIME = NULL
AS SET NOCOUNT ON;
BEGIN
	
	DECLARE @ReleasePointID BIGINT
	DECLARE @RaceResultTable TABLE
			(
			[ID] [bigint] NOT NULL,
			[RaceResultID] [bigint] NOT NULL,
			[MemberID] [bigint] NULL,
			[EntryID] [bigint] NULL,
			[StickerCode] [varchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
			[StickerNumber] [varchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
			[Arrival] [datetime] NULL,
			[DateCreated] [datetime] NULL,
			[IsActive] [int] NOT NULL,
			[ExternalID] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
			[Remarks] [varchar] (500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
			[LiberationPointID] [bigint] NULL,
			[DistanceLatDegree] [bigint] NULL,
			[DistanceLatMinutes] [bigint] NULL,
			[DistanceLatSeconds] [float] NULL,
			[DistanceLatSign] [varchar] (1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
			[DistanceLongDegree] [bigint] NULL,
			[DistanceLongMinutes] [bigint] NULL,
			[DistanceLongSeconds] [float] NULL,
			[DistanceLongSign] [varchar] (1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
			[LatDegreeSimplified] [decimal] (18, 6) NULL,
			[LongDegreeSimplified] [decimal] (18, 6) NULL
			)
	
	IF ( @RaceResultID > 0)
	BEGIN
		declare @WithResult table (raceresultID bigint)

		insert into @WithResult
		Select rr.raceresultid from raceresult (nolock)  rr
			inner join  RaceResultDetails_v3 rrd (nolock) on rr.raceresultid = rrd.raceresultid
		WHERE rrd.ClubID = @ClubID

		--get record in current raceresult table
		INSERT INTO @RaceResultTable
		select * from raceresult where raceresultid not in (select raceresultid from @WithResult)

		--get release point id
		SET @ReleasePointID = dbo.fnGetRaceReleasePointID(@ClubID)
	END

	--PRINT @ReleasePointID
	SELECT 
	   e.MemberName
	   ,Distance = convert(decimal(18,4),(
					dbo.ComputeDistance --member distance
						(
						rp.LatDegreeSimplified,rp.LongDegreeSimplified,
						rr.LatDegreeSimplified,rr.LongDegreeSimplified
						)
					))
		,e.BandNumber
		,rr.StickerNumber
		,rr.Arrival
		,Flight=dbo.fnFlight(convert(datetime,(convert(varchar(10),rp.DateRelease,101) + ' ' + rp.ReleaseTime)),
							case when rp.isstop > 0 
								then case when (DATEDIFF(SECOND,convert(datetime,(convert(varchar(10),rp.StopToDate,101) + ' ' + rp.StopToTime)),rr.arrival)) >= 0
											   THEN DATEADD(SECOND,DATEDIFF(SECOND,convert(datetime,(convert(varchar(10),rp.StopFromDate,101) + ' ' + rp.StopFromTime)),
													convert(datetime,(convert(varchar(10),rp.StopToDate,101) + ' ' + rp.StopToTime)))*(-1),rr.arrival)
										  WHEN (DATEDIFF(SECOND,convert(datetime,(convert(varchar(10),rp.StopFromDate,101) + ' ' + rp.StopFromTime)),rr.arrival)) >= 0
											   THEN DATEADD(SECOND,DATEDIFF(SECOND,convert(datetime,(convert(varchar(10),rp.StopFromDate,101) + ' ' + rp.StopFromTime)),
													convert(datetime,(convert(varchar(10),rp.StopToDate,101) + ' ' + rp.StopToTime)))*(-1),
													convert(datetime,(convert(varchar(10),rp.StopToDate,101) + ' ' + rp.StopToTime)))
										  else rr.arrival end
							else rr.arrival  end							
							)
		,Speed=dbo.fnSpeed
				 --Diffirential
				(
				(
				dbo.ComputeDistance --member distance
					(
					rp.LatDegreeSimplified,rp.LongDegreeSimplified,
					rr.LatDegreeSimplified,rr.LongDegreeSimplified
					)
				) * 1000
			,
			dbo.fnFlight(convert(datetime,(convert(varchar(10),rp.DateRelease,101) + ' ' + rp.ReleaseTime)),
						case when rp.isstop > 0 
							then case when (DATEDIFF(SECOND,convert(datetime,(convert(varchar(10),rp.StopToDate,101) + ' ' + rp.StopToTime)),rr.arrival)) >= 0
										   THEN DATEADD(SECOND,DATEDIFF(SECOND,convert(datetime,(convert(varchar(10),rp.StopFromDate,101) + ' ' + rp.StopFromTime)),
													convert(datetime,(convert(varchar(10),rp.StopToDate,101) + ' ' + rp.StopToTime)))*(-1),rr.arrival)
									  WHEN (DATEDIFF(SECOND,convert(datetime,(convert(varchar(10),rp.StopFromDate,101) + ' ' + rp.StopFromTime)),rr.arrival)) >= 0
									       THEN DATEADD(SECOND,DATEDIFF(SECOND,convert(datetime,(convert(varchar(10),rp.StopFromDate,101) + ' ' + rp.StopFromTime)),
													convert(datetime,(convert(varchar(10),rp.StopToDate,101) + ' ' + rp.StopToTime)))*(-1),convert(datetime,(convert(varchar(10),
													rp.StopToDate,101) + ' ' + rp.StopToTime)))
									  else rr.arrival end
						else rr.arrival  end							
						)
			)
			,e.EntryID
			,rp.DateRelease
			,RaceResultID
			,rr.IsActive
			,e.ClubID
			,rr.Remarks
			,ISNULL(c.Description,dbo.GetOriginalCategory(e.RaceCategoryID)) + '||' + isnull(RaceCategoryGroupName,dbo.GetOriginalCategoryGroup(e.RaceCategoryGroupID)) + ISNULL('/' + dbo.fnGetEntryCategory(e.MemberID,e.EntryID),'') as "Category/Group"
			,rr.LatDegreeSimplified AS 'Latitude'
			,rr.LongDegreeSimplified AS 'Longtitude'
			,Coordinates=dbo.fnDistance(rr.distancelatdegree,rr.distancelatminutes,rr.distancelatseconds,rr.distancelatsign)+ ' - ' +
				            dbo.fnDistance(rr.distancelongdegree,rr.distancelongminutes,rr.distancelongseconds,rr.distancelongsign)
	FROM @RaceResultTable rr
		inner join Entry e on e.entryID=rr.entryID and e.IsActive=1 --AND e.RaceReleasePointID = @ReleasePointID
		inner JOIN RaceReleasePoint rp on rp.RaceReleasePointID=e.RaceReleasePointID and rp.IsActive=1 --AND rp.RaceReleasePointID = @ReleasePointID
		--INNER join dbo.MemberDetails md (NOLOCK) ON md.MemberID = e.MemberID
		LEFT join RaceCategory c on e.RaceCategoryID=c.RaceCategoryID and c.IsActive=1 AND c.ClubID = @Clubid
		LEFT join RaceCategoryGroup g on e.RaceCategoryGroupID=g.RaceCategoryGroupID and g.IsActive=1 AND g.ClubID = @Clubid
END	

go
--exec [InboxView] '+639178135727','7/31/2017','8/2/2017','',9000
alter proc [dbo].[InboxView]
	@sender varchar(100)='',
	@dateCoveredFrom DATE,
	@dateCoveredTO DATE,
	@keyword varchar(100),
	@ClubID bigint = '',
	@IsPilipinasKalapati bit = 0
as set nocount on;

declare @ClubAbbreviation varchar(100)=''

select @ClubAbbreviation = clubabbreviation from club where clubid = @ClubID

if @ClubID = 9000 SET @ClubAbbreviation = 'Admin'

create table #inbox
(
ModemID varchar(100),
Reply varchar(100),
IsReply bit,
SMSContent varchar(200),
Sender varchar(100),
SMSDate varchar(100),
SMSTime varchar(100),
DateReceived datetime,
Keyword varchar(100),
ReplyMessage varchar(500)
)

IF (@IsPilipinasKalapati = 1)
BEGIN
	insert into #inbox
	select ModemID AS "Reciever",ActivationCode AS "Reply",IsReply,SMSContent,Sender,SMSDate,SMSTime,convert(datetime,DateReceived,101) as "DateReceived",Keyword,ReplyMessage 
	from pigeon_mavcpigeonclocking.dbo.Inbox_Archive (nolock)
	where 
	(Sender=@sender OR @sender='') 
	and (SMSContent like ('%' + @keyword + '%')) 
	AND CONVERT(DATE,CONVERT(VARCHAR(10),DateReceived,101),101) BETWEEN @dateCoveredFrom AND @dateCoveredTO
	and LEN(smscontent) < 100
	and (@ClubAbbreviation = 'Admin' or (SMSContent like ('CLOCK ' + @ClubAbbreviation + ' %')))

	insert into #inbox select modemid AS "Reciever",ActivationCode AS "Reply",IsReply,SMSContent,Sender,SMSDate,SMSTime,convert(datetime,DateReceived,101),Keyword,ReplyMessage 
	from pigeon_mavcpigeonclocking.dbo.Inbox (nolock)
	where (Sender=@sender OR @sender='') 
	and (SMSContent like ('%' + @keyword + '%')) 
	AND CONVERT(DATE,CONVERT(VARCHAR(10),DateReceived,101),101) BETWEEN @dateCoveredFrom AND @dateCoveredTO
	and LEN(smscontent) < 100
	and (@ClubAbbreviation = 'Admin' or (SMSContent like ('CLOCK ' + @ClubAbbreviation + ' %')))
END

insert into #inbox
select ModemID AS "Reciever",ActivationCode AS "Reply",IsReply,SMSContent,Sender,SMSDate,SMSTime,convert(datetime,DateReceived,101) as "DateReceived",Keyword,ReplyMessage 
from Inbox_Archive (nolock)
where 
(Sender=@sender OR @sender='') 
and (SMSContent like ('%' + @keyword + '%')) 
AND CONVERT(DATE,CONVERT(VARCHAR(10),DateReceived,101),101) BETWEEN @dateCoveredFrom AND @dateCoveredTO
and LEN(smscontent) < 100
and (@ClubAbbreviation = 'Admin' or (SMSContent like ('CLOCK ' + @ClubAbbreviation + ' %')))

insert into #inbox select modemid AS "Reciever",ActivationCode AS "Reply",IsReply,SMSContent,Sender,SMSDate,SMSTime,convert(datetime,DateReceived,101),Keyword,ReplyMessage 
from Inbox (nolock)
where (Sender=@sender OR @sender='') 
and (SMSContent like ('%' + @keyword + '%')) 
AND CONVERT(DATE,CONVERT(VARCHAR(10),DateReceived,101),101) BETWEEN @dateCoveredFrom AND @dateCoveredTO
and LEN(smscontent) < 100
and (@ClubAbbreviation = 'Admin' or (SMSContent like ('CLOCK ' + @ClubAbbreviation + ' %')))

select " " = CASE WHEN CHARINDEX('*',keyword) > 0 and CHARINDEX('*',replymessage) = 0 THEN 'ADD TO RESULT' ELSE '' END,
	   modemid AS "Reciever",
	   --ActivationCode AS "Reply",
	   --IsReply,
	   SMSContent,
	   Keyword,
	   Sender,
	   '20' + replace(SMSDate,'/','-') as SMSDate,
	   SMSTime,
	   --convert(datetime,DateReceived,101),
	   ReplyMessage 
from #inbox
order by DateReceived desc
go
create proc dbo.GetSticker
	@StickerCode varchar(100)
as
select top 1 FullStickerNo from pigeon_mavcpigeonclocking.dbo.StickerNumber where [inner] = @StickerCode and IsActive = 1
GO
/*
exec [RaceResultSave] 
	@Content = '',
	@Sender = '+639985754822',
	@StickerNumber = '29132*084493240',
	@Arrival = '11/22/2017 18:42:28',
	@RaceReleaseDate = '11/22/2017 12:00:00 AM',
	@Source = 'Back-up',
	@ClubID = 96
	
version3.0
11.2017
*/
ALTER PROC [dbo].[RaceResultSave]
	@Content varchar(100),
	@Sender varchar(100),
	@StickerNumber varchar(100),
	@Arrival varchar(25),
	@Source varchar(100) = '',
	@ClubID BIGINT = 0,
	@RaceReleaseDate DATETIME = NULL,
	@RaceResultID BIGINT = 0 OUTPUT ,
	@Isvalid bit = 0 output ,
	@Remarks varchar(1000) = '' OUTPUT,
	@Name varchar(500) = '' OUTPUT,
	@Distance varchar(100) = '' OUTPUT,
	@RingNumber varchar(100) = '' OUTPUT,
	@Code varchar(100) = '' OUTPUT,
	@Flight varchar(100) = '' OUTPUT,
	@Speed varchar(100) = '' OUTPUT,
	@MemberID BIGINT = '' OUTPUT
AS SET NOCOUNT ON;
BEGIN
SET NOCOUNT ON;
DECLARE @StickerCode varchar(50),
		@ClubName varchar(10),
		@EntryID BIGINT = 0,
		@ReleasePointID bigint,
		@Delimiter varchar(1),
		@ArrivalDate datetime,
		@IsMAVCStickerUsed BIT,
		@StickerInner VARCHAR(100)='',
		@StickerOutter VARCHAR(100),
		@PrevRaceResultID int = 0,
		@TimeZoneDiff int = 0,
		@AllowDoubleEntry BIT

SET @RaceResultID = 0
if (LEN(@Sender) = 11)
BEGIN
	SET @Sender = '+63' + SUBSTRING(@Sender,2,10)
END

IF (@Source = 'Back-up')
BEGIN
	SELECT @IsMAVCStickerUsed=IsMAVCStickerUsed,@TimeZoneDiff = isnull(TimeZoneDiff,0),@AllowDoubleEntry = AllowDoubleEntry from Club where ClubID = @ClubID
END
ELSE	
BEGIN	
	SELECT @ClubName=[value] from dbo.fnSplitString(@Content,' ') WHERE ID=2
	SELECT @ClubID=ClubID,@IsMAVCStickerUsed=IsMAVCStickerUsed,@TimeZoneDiff = isnull(TimeZoneDiff,0) from Club where ClubAbbreviation=@ClubName 
END

SET @ArrivalDate=CONVERT(datetime,@Arrival,120)
SET @ArrivalDate=DATEADD(HOUR,@TimeZoneDiff,@ArrivalDate)

IF EXISTS(SELECT 1 FROM SMSRegisteredNumber where MobileNumber = @Sender and IsActive=1 and ClubID=@ClubID)
	BEGIN
		IF (CHARINDEX('*',@StickerNumber,1)> 0)
			BEGIN
			SET @Delimiter='*'
			SET @StickerNumber = REPLACE(@StickerNumber,@Delimiter,'*')
			END
		ELSE IF (CHARINDEX(' ',@StickerNumber,1)> 0)
			BEGIN
			SET @Delimiter=' '
			SET @StickerNumber = REPLACE(@StickerNumber,@Delimiter,'*')
			END
		
		ELSE IF (CHARINDEX('#',@StickerNumber,1)> 0)
			BEGIN
			SET @Delimiter='#'
			SET @StickerNumber = REPLACE(@StickerNumber,@Delimiter,'*')
			END
		
		SELECT @MemberID=MemberID FROM SMSRegisteredNumber WHERE MobileNumber=@Sender and ClubID=@ClubID AND IsActive = 1
		SELECT @StickerCode=[value] from dbo.fnSplitString(@StickerNumber,'*') WHERE ID=1
		SELECT @StickerInner=[value] from dbo.fnSplitString(@StickerNumber,'*') WHERE ID=2	

		IF (@Source = 'Back-up')
			BEGIN
			IF (@AllowDoubleEntry = 1)
				BEGIN
					IF NOT EXISTS (SELECT 1 FROM pigeon_mavcpigeonclocking.dbo.StickerNumber WHERE [Inner] = @StickerInner) AND (LEN(@StickerCode) = 5) 
					BEGIN
						SET @Remarks='Invalid Sticker code.'
						GOTO ERROR;
					END
				END
			ELSE	
				BEGIN
				IF NOT EXISTS (SELECT 1 FROM pigeon_mavcpigeonclocking.dbo.StickerNumber WHERE [Inner] = @StickerInner)
					BEGIN
						SET @Remarks='Invalid Sticker code.'
						GOTO ERROR;
					END
				END
			END

		IF (@RaceReleaseDate IS NOT NULL)
		BEGIN
			SELECT top 1 @ReleasePointID=RaceReleasePointID from RaceReleasePoint rs (nolock)
			where rs.ClubID=@ClubID 
				and CONVERT(DATE,DateRelease,101) = CONVERT(DATE,@RaceReleaseDate,101)
			order by DateRelease DESC,rs.RaceReleasePointID DESC	
		END
		ELSE
		BEGIN
			SET @ReleasePointID=dbo.fnGetRaceReleasePointID(@ClubID) 
		END

		--PRINT @ReleasePointID

		SELECT @EntryID = EntryID from dbo.Entry (NOLOCK)
		WHERE MemberID = @MemberID 
			AND StickerCode = @StickerCode
			AND RaceReleasePointID=@ReleasePointid
		 
		SET @StickerOutter = @StickerCode

		--print 'memberid:' + cast(@MemberID as varchar(10))
		--print 'racereleasepoint:' + cast(@ReleasePointID as varchar(10))
		--print 'stickercode:' + cast(@StickerCode as varchar(10))
		--print 'entryid:' + cast(@EntryID as varchar(10))
		
		if (CHARINDEX('-',@StickerNumber,1) > 0)
			begin
			set @Remarks = 'Invalid Text Format'
			end
		else if (ISNUMERIC(@StickerCode) = 0)
			begin
			SET @Remarks = 'Invalid Text Format'
			END
		ELSE IF (@StickerInner = '')
			BEGIN
			SET @Remarks = 'Invalid Sticker Number'	
			END
		else IF (ISNULL(@EntryID,0) > 0)
			begin
				if not exists (Select top 1 1 from RaceResult rr (nolock) 
							   WHERE rr.EntryID = @EntryID 
									AND StickerNumber = @StickerNumber  
									AND rr.IsActive = 1)
					BEGIN
						
						--insert record into raceresult table
						EXEC dbo.RaceResult_Insert
							@ReleasePointID = @ReleasePointID,
							@EntryID = @EntryID,
							@MemberID = @MemberID,
							@IsMAVCStickerUsed = @IsMAVCStickerUsed,
							@StickerNumber = @StickerNumber,
							@ClubID = @ClubID,
							@RaceReleaseDate = @RaceReleaseDate,
							@ArrivalDate = @ArrivalDate,
							@Source = @Source,
							@RaceResultID = @RaceResultID OUTPUT

						--PRINT 'RaceResultID:' + cast(@RaceResultID as varchar(10))
						SELECT	@Name = [Name],
								@Distance = [Distance],
								@RingNumber = [RingNumber],
								@Code = [Code],
								@Arrival = [Arrival],
								@Flight = [Flight],
								@Speed = [Speed]
						FROM dbo.RaceResultDetails_v3 where RaceResultID = @RaceResultID

						SET @IsValid=1
					end
				else
				BEGIN
					DECLARE @arr DATETIME2

					select @RaceResultID=RaceResultiD,@arr = Arrival, @RingNumber = BandNumber, @StickerNumber = StickerNumber
					from RaceResult rr
						INNER JOIN entry e on rr.EntryID = e.EntryID AND e.EntryID = @EntryID
					WHERE rr.EntryID = @EntryID AND rr.IsActive = 1

					IF (DATEDIFF(SECOND,@arr,@ArrivalDate) < 0)
					BEGIN
						--insert record into raceresult table
						EXEC dbo.RaceResult_Insert
							@ReleasePointID = @ReleasePointID,
							@EntryID = @EntryID,
							@MemberID = @MemberID,
							@IsMAVCStickerUsed = @IsMAVCStickerUsed,
							@StickerNumber = @StickerNumber,
							@ClubID = @ClubID,
							@RaceReleaseDate = @RaceReleaseDate,
							@ArrivalDate = @ArrivalDate,
							@Source = @Source,
							@RaceResultID = @RaceResultID OUTPUT

						--PRINT 'RaceResultID:' + cast(@RaceResultID as varchar(10))
						SELECT	@Name = [Name],
								@Distance = [Distance],
								@RingNumber = [RingNumber],
								@Code = [Code],
								@Arrival = [Arrival],
								@Flight = [Flight],
								@Speed = [Speed]
						FROM dbo.RaceResultDetails_v3 where RaceResultID = @RaceResultID

						SET @IsValid=1	
					END
					ELSE	
					BEGIN	

						SET @Remarks = 'Bird Already Clock' + char(13) +
									   'Arrival: ' + CONVERT(varchar(10),@arr,101) + ' ' + CONVERT(varchar(10),@arr,108) + char(13) +
									   'Band No.: ' + @RingNumber + char(13) +
									   'Sticker No.: ' + @StickerNumber
					END
				end
			end
		Else
			BEGIN
				--SELECT @StickerCode,@sender,@ClubID
				if not exists (select 1 from smsregisterednumber rn (nolock)
							inner join entry e (nolock) on rn.memberID=e.MemberID and e.RaceReleasePointID=@ReleasePointID
							where mobilenumber=@sender and rn.IsActive=1 and rn.ClubID=@ClubID)	
					begin
					SET @Remarks = 'No Bird Entry'	
					end	
				else if not exists (select 1 from smsregisterednumber rn (nolock)
						inner join entry e (nolock) on rn.memberID=e.MemberID and e.Stickercode=@StickerCode and e.IsActive=1
						where mobilenumber=@sender and rn.IsActive=1 and rn.ClubID=@ClubID and e.RaceReleasePointID=@ReleasePointID)
					BEGIN
					SET @Remarks = 'Invalid Sticker Code'	
					end
				else
					begin
					SET @Remarks = 'Invalid Text Format'
					end
			end
	END
Else
	begin
		SET @Remarks = 'This number is not registered.'
	END

--insert into raceresult details if not exist
--this will assure that all record on race result has insert into raceresultdetails_v3
IF (@RaceResultID > 0)
BEGIN
	IF NOT EXISTS (SELECT 1 FROM dbo.RaceResultDetails_v3 WHERE RaceResultID = @RaceResultID)
	BEGIN
		--Insert into detail table to faster result viewing
		INSERT INTO dbo.RaceResultDetails_v3
		EXEC GetRaceResultDetailsV3 @RaceResultID = @RaceResultID,@ClubID = @ClubID,@RaceReleaseDate = @RaceReleaseDate
	END
END

--return to UI to return error.
ERROR:
IF @Source = 'Back-up' AND @Remarks != ''
BEGIN
	RAISERROR(@Remarks,16,1)
END

END
GO
--exec [InboxView] '+639178135727','7/31/2017','8/2/2017','',9000
alter proc [dbo].[InboxView]
	@sender varchar(100)='',
	@dateCoveredFrom DATE,
	@dateCoveredTO DATE,
	@keyword varchar(100),
	@ClubID bigint = '',
	@IsPilipinasKalapati bit = 0
as set nocount on;

declare @ClubAbbreviation varchar(100)=''

select @ClubAbbreviation = clubabbreviation from club where clubid = @ClubID

if @ClubID = 9000 SET @ClubAbbreviation = 'Admin'

create table #inbox
(
ModemID varchar(100),
Reply varchar(100),
IsReply bit,
SMSContent varchar(200),
Sender varchar(100),
SMSDate varchar(100),
SMSTime varchar(100),
DateReceived datetime,
Keyword varchar(100),
ReplyMessage varchar(500)
)

IF (@IsPilipinasKalapati = 1)
BEGIN
	insert into #inbox
	select ModemID AS "Reciever",ActivationCode AS "Reply",IsReply,SMSContent,Sender,SMSDate,SMSTime,convert(datetime,DateReceived,101) as "DateReceived",Keyword,ReplyMessage 
	from pigeon_mavcpigeonclocking.dbo.Inbox_Archive (nolock)
	where 
	(Sender=@sender OR @sender='') 
	and (SMSContent like ('%' + @keyword + '%')) 
	AND CONVERT(DATE,CONVERT(VARCHAR(10),DateReceived,101),101) BETWEEN @dateCoveredFrom AND @dateCoveredTO
	and LEN(smscontent) < 100
	and (@ClubAbbreviation = 'Admin' or (SMSContent like ('CLOCK ' + @ClubAbbreviation + ' %')))

	insert into #inbox select modemid AS "Reciever",ActivationCode AS "Reply",IsReply,SMSContent,Sender,SMSDate,SMSTime,convert(datetime,DateReceived,101),Keyword,ReplyMessage 
	from pigeon_mavcpigeonclocking.dbo.Inbox (nolock)
	where (Sender=@sender OR @sender='') 
	and (SMSContent like ('%' + @keyword + '%')) 
	AND CONVERT(DATE,CONVERT(VARCHAR(10),DateReceived,101),101) BETWEEN @dateCoveredFrom AND @dateCoveredTO
	and LEN(smscontent) < 100
	and (@ClubAbbreviation = 'Admin' or (SMSContent like ('CLOCK ' + @ClubAbbreviation + ' %')))
END

insert into #inbox
select ModemID AS "Reciever",ActivationCode AS "Reply",IsReply,SMSContent,Sender,SMSDate,SMSTime,convert(datetime,DateReceived,101) as "DateReceived",Keyword,ReplyMessage 
from Inbox_Archive (nolock)
where 
(Sender=@sender OR @sender='') 
and (SMSContent like ('%' + @keyword + '%')) 
AND CONVERT(DATE,CONVERT(VARCHAR(10),DateReceived,101),101) BETWEEN @dateCoveredFrom AND @dateCoveredTO
and LEN(smscontent) < 100
and (@ClubAbbreviation = 'Admin' or (SMSContent like ('CLOCK ' + @ClubAbbreviation + ' %')))

insert into #inbox select modemid AS "Reciever",ActivationCode AS "Reply",IsReply,SMSContent,Sender,SMSDate,SMSTime,convert(datetime,DateReceived,101),Keyword,ReplyMessage 
from Inbox (nolock)
where (Sender=@sender OR @sender='') 
and (SMSContent like ('%' + @keyword + '%')) 
AND CONVERT(DATE,CONVERT(VARCHAR(10),DateReceived,101),101) BETWEEN @dateCoveredFrom AND @dateCoveredTO
and LEN(smscontent) < 100
and (@ClubAbbreviation = 'Admin' or (SMSContent like ('CLOCK ' + @ClubAbbreviation + ' %')))

select " " = CASE WHEN CHARINDEX('*',keyword) > 0 and CHARINDEX('*',replymessage) = 0 THEN 'ADD TO RESULT' ELSE '' END,
	   modemid AS "Reciever",
	   --ActivationCode AS "Reply",
	   --IsReply,
	   SMSContent,
	   Keyword,
	   Sender,
	   '20' + replace(SMSDate,'/','-') as SMSDate,
	   SMSTime,
	   --convert(datetime,DateReceived,101),
	   ReplyMessage 
from #inbox
order by DateReceived desc