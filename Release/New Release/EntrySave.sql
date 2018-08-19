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



