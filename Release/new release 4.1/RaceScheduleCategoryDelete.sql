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