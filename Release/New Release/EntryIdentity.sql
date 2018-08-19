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
SELECT * FROM EntryIdentity