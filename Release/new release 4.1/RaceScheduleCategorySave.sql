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

