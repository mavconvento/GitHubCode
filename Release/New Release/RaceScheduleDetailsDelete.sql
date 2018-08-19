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