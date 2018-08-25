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

