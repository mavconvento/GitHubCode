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