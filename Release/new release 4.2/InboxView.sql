--exec [InboxView] '+639178135727','7/31/2017','8/2/2017','',9000
alter proc [dbo].[InboxView]
	@sender varchar(100)='',
	@dateCoveredFrom DATE,
	@dateCoveredTO DATE,
	@keyword varchar(100),
	@ClubID bigint = '',
	@IsPilipinasKalapati bit = 0
as set nocount on;

declare @ClubAbbreviation varchar(100)=''

select @ClubAbbreviation = clubabbreviation from club where clubid = @ClubID

if @ClubID = 9000 SET @ClubAbbreviation = 'Admin'

create table #inbox
(
ModemID varchar(100),
Reply varchar(100),
IsReply bit,
SMSContent varchar(200),
Sender varchar(100),
SMSDate varchar(100),
SMSTime varchar(100),
DateReceived datetime,
Keyword varchar(100),
ReplyMessage varchar(500)
)

IF (@IsPilipinasKalapati = 1)
BEGIN
	insert into #inbox
	select ModemID AS "Reciever",ActivationCode AS "Reply",IsReply,SMSContent,Sender,SMSDate,SMSTime,convert(datetime,DateReceived,101) as "DateReceived",Keyword,ReplyMessage 
	from pigeon_mavcpigeonclocking.dbo.Inbox_Archive (nolock)
	where 
	(Sender=@sender OR @sender='') 
	and (SMSContent like ('%' + @keyword + '%')) 
	AND CONVERT(DATE,CONVERT(VARCHAR(10),DateReceived,101),101) BETWEEN @dateCoveredFrom AND @dateCoveredTO
	and LEN(smscontent) < 100
	and (@ClubAbbreviation = 'Admin' or (SMSContent like ('CLOCK ' + @ClubAbbreviation + ' %')))

	insert into #inbox select modemid AS "Reciever",ActivationCode AS "Reply",IsReply,SMSContent,Sender,SMSDate,SMSTime,convert(datetime,DateReceived,101),Keyword,ReplyMessage 
	from pigeon_mavcpigeonclocking.dbo.Inbox (nolock)
	where (Sender=@sender OR @sender='') 
	and (SMSContent like ('%' + @keyword + '%')) 
	AND CONVERT(DATE,CONVERT(VARCHAR(10),DateReceived,101),101) BETWEEN @dateCoveredFrom AND @dateCoveredTO
	and LEN(smscontent) < 100
	and (@ClubAbbreviation = 'Admin' or (SMSContent like ('CLOCK ' + @ClubAbbreviation + ' %')))
END

insert into #inbox
select ModemID AS "Reciever",ActivationCode AS "Reply",IsReply,SMSContent,Sender,SMSDate,SMSTime,convert(datetime,DateReceived,101) as "DateReceived",Keyword,ReplyMessage 
from Inbox_Archive (nolock)
where 
(Sender=@sender OR @sender='') 
and (SMSContent like ('%' + @keyword + '%')) 
AND CONVERT(DATE,CONVERT(VARCHAR(10),DateReceived,101),101) BETWEEN @dateCoveredFrom AND @dateCoveredTO
and LEN(smscontent) < 100
and (@ClubAbbreviation = 'Admin' or (SMSContent like ('CLOCK ' + @ClubAbbreviation + ' %')))

insert into #inbox select modemid AS "Reciever",ActivationCode AS "Reply",IsReply,SMSContent,Sender,SMSDate,SMSTime,convert(datetime,DateReceived,101),Keyword,ReplyMessage 
from Inbox (nolock)
where (Sender=@sender OR @sender='') 
and (SMSContent like ('%' + @keyword + '%')) 
AND CONVERT(DATE,CONVERT(VARCHAR(10),DateReceived,101),101) BETWEEN @dateCoveredFrom AND @dateCoveredTO
and LEN(smscontent) < 100
and (@ClubAbbreviation = 'Admin' or (SMSContent like ('CLOCK ' + @ClubAbbreviation + ' %')))

select " " = CASE WHEN CHARINDEX('*',keyword) > 0 and CHARINDEX('*',replymessage) = 0 THEN 'ADD TO RESULT' ELSE '' END,
	   modemid AS "Reciever",
	   --ActivationCode AS "Reply",
	   --IsReply,
	   SMSContent,
	   Keyword,
	   Sender,
	   '20' + replace(SMSDate,'/','-') as SMSDate,
	   SMSTime,
	   --convert(datetime,DateReceived,101),
	   ReplyMessage 
from #inbox
order by DateReceived desc