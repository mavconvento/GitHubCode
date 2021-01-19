create table FileUpload
(
FileUploadID uniqueidentifier primary key,
Data VARBINARY(max),
[FileName] varchar(500),
[FileType] varchar(500)
)

select * from mavcpigeon_user

alter table mavcpigeon_user add LoftName varchar(100), FileUploadID uniqueidentifier


select * from prepaidcardnumber where id >= 183601
select top 1 * from prepaidcardnumber where isprinter != 1

update prepaidcardnumber set isprinter = 0, externalid = '',lastdateprint = null where id >= 183601

select top 1 * from [dbo].[PasaloadTransaction] where Receiver = '+639178565944'
select top 1 * from [dbo].[LoadMonitoring] where mobilenumber = '+639676584625'
select * from [dbo].[WebPasaloadTransaction] where mobilenumberfrom = '+639178565944' order by transactiondate desc

update [dbo].[LoadMonitoring] set pasaload = pasaload - 2292 where  mobilenumber = '+639676584625'
update [dbo].[LoadMonitoring] set pasaload = pasaload + 2292 where  mobilenumber = '+639178565944'

delete from [dbo].[WebPasaloadTransaction] where transactiondate = '2021-01-12 20:44:29.233'
delete from [dbo].[WebPasaloadTransaction] where transactiondate = '2021-01-12 20:44:29.230'



