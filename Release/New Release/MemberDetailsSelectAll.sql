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

