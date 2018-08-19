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
	