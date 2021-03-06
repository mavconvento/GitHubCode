USE [master]
GO
/****** Object:  Database [PigeonIDSystem]    Script Date: 1/4/2019 11:30:22 AM ******/
CREATE DATABASE [PigeonIDSystem]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'PigeonIDSystem', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\PigeonIDSystem.mdf' , SIZE = 4096KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'PigeonIDSystem_log', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\PigeonIDSystem_log.ldf' , SIZE = 1280KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [PigeonIDSystem] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [PigeonIDSystem].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [PigeonIDSystem] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [PigeonIDSystem] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [PigeonIDSystem] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [PigeonIDSystem] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [PigeonIDSystem] SET ARITHABORT OFF 
GO
ALTER DATABASE [PigeonIDSystem] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [PigeonIDSystem] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [PigeonIDSystem] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [PigeonIDSystem] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [PigeonIDSystem] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [PigeonIDSystem] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [PigeonIDSystem] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [PigeonIDSystem] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [PigeonIDSystem] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [PigeonIDSystem] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [PigeonIDSystem] SET  DISABLE_BROKER 
GO
ALTER DATABASE [PigeonIDSystem] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [PigeonIDSystem] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [PigeonIDSystem] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [PigeonIDSystem] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [PigeonIDSystem] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [PigeonIDSystem] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [PigeonIDSystem] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [PigeonIDSystem] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [PigeonIDSystem] SET  MULTI_USER 
GO
ALTER DATABASE [PigeonIDSystem] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [PigeonIDSystem] SET DB_CHAINING OFF 
GO
ALTER DATABASE [PigeonIDSystem] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [PigeonIDSystem] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
USE [PigeonIDSystem]
GO
/****** Object:  StoredProcedure [dbo].[DeletePigeon]    Script Date: 1/4/2019 11:30:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[DeletePigeon]
	@PigeonID bigint
as set nocount on;
begin
	Delete PigeonDetails where ID = @PigeonID

	--select pd.ID,pd.BandNumber,pd.Sex,pd.Color from Member m 
	--inner join PigeonDetails pd on m.ID = pd.MemberID
	--where m.ID = @MemberID
end
GO
/****** Object:  StoredProcedure [dbo].[GetAllPigeonDetails]    Script Date: 1/4/2019 11:30:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[GetAllPigeonDetails]
	@MemberIDNo varchar(100)
as set nocount on;
begin
	select 'EDIT' AS ' ','DELETE' AS '  ',pd.ID,pd.BandNumber,pd.Sex,pd.Color from Member m 
	inner join PigeonDetails pd on m.ID = pd.MemberID
	where m.MemberIDNo = @MemberIDNo
end
GO
/****** Object:  StoredProcedure [dbo].[GetMemberDetails]    Script Date: 1/4/2019 11:30:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[GetMemberDetails]
	@MemberID varchar(100)
as set nocount on;
begin
	select * from Member where MemberIDNo = @MemberID
end
GO
/****** Object:  StoredProcedure [dbo].[GetPigeonDetails]    Script Date: 1/4/2019 11:30:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[GetPigeonDetails]
	@PigeonID bigint
as set nocount on;
begin
		select pd.ID,pd.BandNumber,pd.Sex,pd.Color,Photo from Member m 
		inner join PigeonDetails pd on m.ID = pd.MemberID
		where pd.ID = @PigeonID
end
GO
/****** Object:  StoredProcedure [dbo].[MemberSave]    Script Date: 1/4/2019 11:30:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[MemberSave]
	@MemberIDNo varchar(200),
	@MemberName varchar(100),
	@PigeonID bigint,
	@BandNumber varchar(100),
	@Sex varchar(100),
	@Color varchar(100),
	@Photo Image
as set nocount on;
begin

	if not exists(select 1 from  dbo.Member where MemberIDNo = @MemberIDNo)
	begin
		insert into Member values (@MemberIDNo,@MemberName,GETDATE())
	end

	Declare @MemberID bigint

	select @MemberID = ID from Member where MemberIDNo = @MemberIDNo

	if (@PigeonID > 0)
	begin
		update dbo.PigeonDetails set BandNumber = @BandNumber,
									 Sex = @Sex,
									 Color = @Color,
									 Photo = @Photo
		where id = @PigeonID
	end
	else
	begin
		if not exists(select 1 from dbo.PigeonDetails where BandNumber = @BandNumber and MemberID = @MemberID)
		begin
			insert into dbo.PigeonDetails values (@MemberID,@BandNumber,@Sex,@Color,@Photo,GETDATE(),null)
		end
		else
		begin
			update dbo.PigeonDetails set  BandNumber = @BandNumber,
									 Sex = @Sex,
									 Color = @Color,
									 Photo = @Photo
			where BandNumber = @BandNumber and MemberID = @MemberID
		end
	end

	select pd.ID,pd.BandNumber,pd.Sex,pd.Color from Member m 
		inner join PigeonDetails pd on m.ID = pd.MemberID
	where m.ID = @MemberID
end

GO
/****** Object:  Table [dbo].[Member]    Script Date: 1/4/2019 11:30:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Member](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[MemberIDNo] [varchar](200) NULL,
	[MemberName] [varchar](100) NULL,
	[CreatedDate] [datetime] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PigeonDetails]    Script Date: 1/4/2019 11:30:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PigeonDetails](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[MemberID] [bigint] NULL,
	[BandNumber] [varchar](100) NULL,
	[Sex] [varchar](100) NULL,
	[Color] [varchar](100) NULL,
	[Photo] [image] NULL,
	[CreatedDate] [datetime] NULL,
	[UpdateDate] [datetime] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
USE [master]
GO
ALTER DATABASE [PigeonIDSystem] SET  READ_WRITE 
GO
