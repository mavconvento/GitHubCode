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



