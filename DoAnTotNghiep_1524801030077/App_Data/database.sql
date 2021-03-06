USE [master]
GO
/****** Object:  Database [DOANTOTNGHIEP]    Script Date: 05/13/2019 06:29:36 AM ******/
CREATE DATABASE [DOANTOTNGHIEP]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'DOANTOTNGHIEP', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\DOANTOTNGHIEP.mdf' , SIZE = 4096KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'DOANTOTNGHIEP_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\DOANTOTNGHIEP_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [DOANTOTNGHIEP] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [DOANTOTNGHIEP].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [DOANTOTNGHIEP] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [DOANTOTNGHIEP] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [DOANTOTNGHIEP] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [DOANTOTNGHIEP] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [DOANTOTNGHIEP] SET ARITHABORT OFF 
GO
ALTER DATABASE [DOANTOTNGHIEP] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [DOANTOTNGHIEP] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [DOANTOTNGHIEP] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [DOANTOTNGHIEP] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [DOANTOTNGHIEP] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [DOANTOTNGHIEP] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [DOANTOTNGHIEP] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [DOANTOTNGHIEP] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [DOANTOTNGHIEP] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [DOANTOTNGHIEP] SET  DISABLE_BROKER 
GO
ALTER DATABASE [DOANTOTNGHIEP] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [DOANTOTNGHIEP] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [DOANTOTNGHIEP] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [DOANTOTNGHIEP] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [DOANTOTNGHIEP] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [DOANTOTNGHIEP] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [DOANTOTNGHIEP] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [DOANTOTNGHIEP] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [DOANTOTNGHIEP] SET  MULTI_USER 
GO
ALTER DATABASE [DOANTOTNGHIEP] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [DOANTOTNGHIEP] SET DB_CHAINING OFF 
GO
ALTER DATABASE [DOANTOTNGHIEP] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [DOANTOTNGHIEP] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [DOANTOTNGHIEP] SET DELAYED_DURABILITY = DISABLED 
GO
USE [DOANTOTNGHIEP]
GO
/****** Object:  User [sadmin]    Script Date: 05/13/2019 06:29:36 AM ******/
CREATE USER [sadmin] FOR LOGIN [sadmin] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  Table [dbo].[Attendance]    Script Date: 05/13/2019 06:29:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Attendance](
	[Pid] [bigint] IDENTITY(1,1) NOT NULL,
	[TeacherCode] [nvarchar](50) NULL,
	[Status] [int] NULL,
	[ActiveCode] [bigint] NULL,
	[Lock] [int] NULL,
 CONSTRAINT [PK_Attendance] PRIMARY KEY CLUSTERED 
(
	[Pid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ExtratimeActive]    Script Date: 05/13/2019 06:29:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ExtratimeActive](
	[PID] [bigint] IDENTITY(1,1) NOT NULL,
	[Date] [datetime] NULL,
	[Content] [nvarchar](max) NULL,
	[Hour] [nvarchar](12) NULL,
	[Participant] [nvarchar](max) NULL,
	[Location] [nvarchar](258) NULL,
	[KindActive] [bigint] NULL,
 CONSTRAINT [PK_ExtratimeActive] PRIMARY KEY CLUSTERED 
(
	[PID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PersonalExtratimeActive]    Script Date: 05/13/2019 06:29:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PersonalExtratimeActive](
	[Pid] [bigint] IDENTITY(1,1) NOT NULL,
	[Content] [nvarchar](max) NULL,
	[Date] [datetime] NULL,
	[Time] [nvarchar](8) NULL,
	[Location] [nvarchar](512) NULL,
	[Proof] [nvarchar](128) NULL,
	[KindActive] [bigint] NULL,
	[TeacherCode] [nvarchar](50) NULL,
	[Status] [int] NULL,
 CONSTRAINT [PK_PersonalExtratimeActive] PRIMARY KEY CLUSTERED 
(
	[Pid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 05/13/2019 06:29:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[Pid] [bigint] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[Pid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserLogin]    Script Date: 05/13/2019 06:29:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserLogin](
	[PID] [bigint] IDENTITY(1,1) NOT NULL,
	[USER] [varchar](24) NOT NULL,
	[PASSWORD] [varchar](128) NULL,
	[TOKEN] [varchar](64) NULL,
	[IPLOGIN] [varchar](24) NULL,
	[STATUS] [bigint] NULL,
	[SALT] [varchar](128) NULL,
	[Role] [nvarchar](50) NULL,
	[FullName] [nvarchar](258) NULL,
	[DayOfBirth] [datetime] NULL,
	[Country] [nvarchar](258) NULL,
	[CID] [nvarchar](128) NULL,
	[Sex] [int] NULL,
	[Code] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_UserLogin] PRIMARY KEY CLUSTERED 
(
	[PID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WorkingDefine]    Script Date: 05/13/2019 06:29:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WorkingDefine](
	[Pid] [bigint] IDENTITY(1,1) NOT NULL,
	[CatePid] [bigint] NULL,
	[Content] [nvarchar](max) NOT NULL,
	[Value] [int] NOT NULL,
 CONSTRAINT [PK_WorkingDefine] PRIMARY KEY CLUSTERED 
(
	[Pid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WorkingDefineCate]    Script Date: 05/13/2019 06:29:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WorkingDefineCate](
	[Pid] [bigint] IDENTITY(1,1) NOT NULL,
	[NameCate] [nvarchar](256) NULL,
 CONSTRAINT [PK_WorkingDefineCate] PRIMARY KEY CLUSTERED 
(
	[Pid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Attendance] ON 

INSERT [dbo].[Attendance] ([Pid], [TeacherCode], [Status], [ActiveCode], [Lock]) VALUES (1, N'123', 1, 21, NULL)
INSERT [dbo].[Attendance] ([Pid], [TeacherCode], [Status], [ActiveCode], [Lock]) VALUES (2, N'124', 0, 21, NULL)
INSERT [dbo].[Attendance] ([Pid], [TeacherCode], [Status], [ActiveCode], [Lock]) VALUES (20, N'125', 1, 22, NULL)
INSERT [dbo].[Attendance] ([Pid], [TeacherCode], [Status], [ActiveCode], [Lock]) VALUES (21, N'123', NULL, 23, NULL)
INSERT [dbo].[Attendance] ([Pid], [TeacherCode], [Status], [ActiveCode], [Lock]) VALUES (22, N'124', NULL, 23, NULL)
INSERT [dbo].[Attendance] ([Pid], [TeacherCode], [Status], [ActiveCode], [Lock]) VALUES (23, N'125', NULL, 23, NULL)
INSERT [dbo].[Attendance] ([Pid], [TeacherCode], [Status], [ActiveCode], [Lock]) VALUES (24, N'126', NULL, 23, NULL)
INSERT [dbo].[Attendance] ([Pid], [TeacherCode], [Status], [ActiveCode], [Lock]) VALUES (25, N'127', NULL, 23, NULL)
INSERT [dbo].[Attendance] ([Pid], [TeacherCode], [Status], [ActiveCode], [Lock]) VALUES (26, N'128', NULL, 23, NULL)
INSERT [dbo].[Attendance] ([Pid], [TeacherCode], [Status], [ActiveCode], [Lock]) VALUES (27, N'129', NULL, 23, NULL)
INSERT [dbo].[Attendance] ([Pid], [TeacherCode], [Status], [ActiveCode], [Lock]) VALUES (28, N'130', NULL, 23, NULL)
INSERT [dbo].[Attendance] ([Pid], [TeacherCode], [Status], [ActiveCode], [Lock]) VALUES (29, N'131', NULL, 23, NULL)
INSERT [dbo].[Attendance] ([Pid], [TeacherCode], [Status], [ActiveCode], [Lock]) VALUES (30, N'123', NULL, 24, NULL)
INSERT [dbo].[Attendance] ([Pid], [TeacherCode], [Status], [ActiveCode], [Lock]) VALUES (31, N'123', NULL, 25, NULL)
INSERT [dbo].[Attendance] ([Pid], [TeacherCode], [Status], [ActiveCode], [Lock]) VALUES (32, N'124', NULL, 25, NULL)
INSERT [dbo].[Attendance] ([Pid], [TeacherCode], [Status], [ActiveCode], [Lock]) VALUES (33, N'125', 0, 26, NULL)
INSERT [dbo].[Attendance] ([Pid], [TeacherCode], [Status], [ActiveCode], [Lock]) VALUES (34, N'126', 1, 26, NULL)
INSERT [dbo].[Attendance] ([Pid], [TeacherCode], [Status], [ActiveCode], [Lock]) VALUES (35, N'127', 1, 26, NULL)
INSERT [dbo].[Attendance] ([Pid], [TeacherCode], [Status], [ActiveCode], [Lock]) VALUES (36, N'128', 1, 26, NULL)
INSERT [dbo].[Attendance] ([Pid], [TeacherCode], [Status], [ActiveCode], [Lock]) VALUES (37, N'129', 1, 26, NULL)
INSERT [dbo].[Attendance] ([Pid], [TeacherCode], [Status], [ActiveCode], [Lock]) VALUES (38, N'130', 0, 26, NULL)
INSERT [dbo].[Attendance] ([Pid], [TeacherCode], [Status], [ActiveCode], [Lock]) VALUES (39, N'131', 0, 26, NULL)
INSERT [dbo].[Attendance] ([Pid], [TeacherCode], [Status], [ActiveCode], [Lock]) VALUES (47, N'124', NULL, 28, NULL)
INSERT [dbo].[Attendance] ([Pid], [TeacherCode], [Status], [ActiveCode], [Lock]) VALUES (48, N'125', NULL, 28, NULL)
INSERT [dbo].[Attendance] ([Pid], [TeacherCode], [Status], [ActiveCode], [Lock]) VALUES (49, N'126', NULL, 28, NULL)
INSERT [dbo].[Attendance] ([Pid], [TeacherCode], [Status], [ActiveCode], [Lock]) VALUES (50, N'127', NULL, 28, NULL)
INSERT [dbo].[Attendance] ([Pid], [TeacherCode], [Status], [ActiveCode], [Lock]) VALUES (51, N'128', NULL, 28, NULL)
INSERT [dbo].[Attendance] ([Pid], [TeacherCode], [Status], [ActiveCode], [Lock]) VALUES (52, N'129', NULL, 28, NULL)
INSERT [dbo].[Attendance] ([Pid], [TeacherCode], [Status], [ActiveCode], [Lock]) VALUES (53, N'130', NULL, 28, NULL)
INSERT [dbo].[Attendance] ([Pid], [TeacherCode], [Status], [ActiveCode], [Lock]) VALUES (54, N'131', NULL, 28, NULL)
INSERT [dbo].[Attendance] ([Pid], [TeacherCode], [Status], [ActiveCode], [Lock]) VALUES (55, N'125', NULL, 27, NULL)
INSERT [dbo].[Attendance] ([Pid], [TeacherCode], [Status], [ActiveCode], [Lock]) VALUES (56, N'126', NULL, 27, NULL)
INSERT [dbo].[Attendance] ([Pid], [TeacherCode], [Status], [ActiveCode], [Lock]) VALUES (57, N'127', NULL, 27, NULL)
INSERT [dbo].[Attendance] ([Pid], [TeacherCode], [Status], [ActiveCode], [Lock]) VALUES (58, N'128', NULL, 27, NULL)
INSERT [dbo].[Attendance] ([Pid], [TeacherCode], [Status], [ActiveCode], [Lock]) VALUES (59, N'129', NULL, 27, NULL)
INSERT [dbo].[Attendance] ([Pid], [TeacherCode], [Status], [ActiveCode], [Lock]) VALUES (60, N'130', NULL, 27, NULL)
INSERT [dbo].[Attendance] ([Pid], [TeacherCode], [Status], [ActiveCode], [Lock]) VALUES (61, N'131', NULL, 27, NULL)
INSERT [dbo].[Attendance] ([Pid], [TeacherCode], [Status], [ActiveCode], [Lock]) VALUES (62, N'125', NULL, 29, NULL)
INSERT [dbo].[Attendance] ([Pid], [TeacherCode], [Status], [ActiveCode], [Lock]) VALUES (63, N'126', NULL, 29, NULL)
INSERT [dbo].[Attendance] ([Pid], [TeacherCode], [Status], [ActiveCode], [Lock]) VALUES (64, N'127', NULL, 29, NULL)
INSERT [dbo].[Attendance] ([Pid], [TeacherCode], [Status], [ActiveCode], [Lock]) VALUES (65, N'128', NULL, 29, NULL)
INSERT [dbo].[Attendance] ([Pid], [TeacherCode], [Status], [ActiveCode], [Lock]) VALUES (66, N'129', NULL, 29, NULL)
INSERT [dbo].[Attendance] ([Pid], [TeacherCode], [Status], [ActiveCode], [Lock]) VALUES (67, N'130', NULL, 29, NULL)
INSERT [dbo].[Attendance] ([Pid], [TeacherCode], [Status], [ActiveCode], [Lock]) VALUES (68, N'131', NULL, 29, NULL)
INSERT [dbo].[Attendance] ([Pid], [TeacherCode], [Status], [ActiveCode], [Lock]) VALUES (69, N'123', 1, 30, NULL)
INSERT [dbo].[Attendance] ([Pid], [TeacherCode], [Status], [ActiveCode], [Lock]) VALUES (70, N'123', NULL, 31, NULL)
INSERT [dbo].[Attendance] ([Pid], [TeacherCode], [Status], [ActiveCode], [Lock]) VALUES (71, N'124', NULL, 31, NULL)
INSERT [dbo].[Attendance] ([Pid], [TeacherCode], [Status], [ActiveCode], [Lock]) VALUES (72, N'125', NULL, 31, NULL)
INSERT [dbo].[Attendance] ([Pid], [TeacherCode], [Status], [ActiveCode], [Lock]) VALUES (73, N'126', NULL, 31, NULL)
INSERT [dbo].[Attendance] ([Pid], [TeacherCode], [Status], [ActiveCode], [Lock]) VALUES (74, N'127', NULL, 31, NULL)
INSERT [dbo].[Attendance] ([Pid], [TeacherCode], [Status], [ActiveCode], [Lock]) VALUES (75, N'128', NULL, 31, NULL)
INSERT [dbo].[Attendance] ([Pid], [TeacherCode], [Status], [ActiveCode], [Lock]) VALUES (76, N'129', NULL, 31, NULL)
INSERT [dbo].[Attendance] ([Pid], [TeacherCode], [Status], [ActiveCode], [Lock]) VALUES (77, N'130', NULL, 31, NULL)
INSERT [dbo].[Attendance] ([Pid], [TeacherCode], [Status], [ActiveCode], [Lock]) VALUES (78, N'131', NULL, 31, NULL)
INSERT [dbo].[Attendance] ([Pid], [TeacherCode], [Status], [ActiveCode], [Lock]) VALUES (79, N'000', 0, 32, NULL)
INSERT [dbo].[Attendance] ([Pid], [TeacherCode], [Status], [ActiveCode], [Lock]) VALUES (80, N'123', 1, 33, NULL)
INSERT [dbo].[Attendance] ([Pid], [TeacherCode], [Status], [ActiveCode], [Lock]) VALUES (81, N'124', NULL, 34, NULL)
INSERT [dbo].[Attendance] ([Pid], [TeacherCode], [Status], [ActiveCode], [Lock]) VALUES (82, N'123', NULL, 34, NULL)
INSERT [dbo].[Attendance] ([Pid], [TeacherCode], [Status], [ActiveCode], [Lock]) VALUES (83, N'125', NULL, 34, NULL)
INSERT [dbo].[Attendance] ([Pid], [TeacherCode], [Status], [ActiveCode], [Lock]) VALUES (84, N'126', NULL, 34, NULL)
INSERT [dbo].[Attendance] ([Pid], [TeacherCode], [Status], [ActiveCode], [Lock]) VALUES (85, N'127', NULL, 34, NULL)
INSERT [dbo].[Attendance] ([Pid], [TeacherCode], [Status], [ActiveCode], [Lock]) VALUES (86, N'128', NULL, 34, NULL)
INSERT [dbo].[Attendance] ([Pid], [TeacherCode], [Status], [ActiveCode], [Lock]) VALUES (87, N'129', NULL, 34, NULL)
INSERT [dbo].[Attendance] ([Pid], [TeacherCode], [Status], [ActiveCode], [Lock]) VALUES (88, N'130', NULL, 34, NULL)
INSERT [dbo].[Attendance] ([Pid], [TeacherCode], [Status], [ActiveCode], [Lock]) VALUES (89, N'131', NULL, 34, NULL)
SET IDENTITY_INSERT [dbo].[Attendance] OFF
SET IDENTITY_INSERT [dbo].[ExtratimeActive] ON 

INSERT [dbo].[ExtratimeActive] ([PID], [Date], [Content], [Hour], [Participant], [Location], [KindActive]) VALUES (23, CAST(N'2019-01-21T00:00:00.000' AS DateTime), N'Chào cờ đầu tuần
', N'06:45', N'["123","124","125","126","127","128","129","130","131"]', N'Sân cờ', 0)
INSERT [dbo].[ExtratimeActive] ([PID], [Date], [Content], [Hour], [Participant], [Location], [KindActive]) VALUES (24, CAST(N'2019-01-21T00:00:00.000' AS DateTime), N'Phó Hiệu trưởng Lê Tuấn Anh dự họp mặt giữa Lãnh đạo tỉnh với các cơ quan Lãnh sự nước ngoài và một số doanh nghiệp trong, ngoài nước nhân dịp Tết cổ truyền Việt Nam – Xuân Kỷ Hợi 2019
', N'18:00', N'["123"]', N'Khách sạn Becamex- Thành phố mới bình Dương', 0)
INSERT [dbo].[ExtratimeActive] ([PID], [Date], [Content], [Hour], [Participant], [Location], [KindActive]) VALUES (25, CAST(N'2019-01-22T00:00:00.000' AS DateTime), N'Chủ tịch Hội đồng Trường chủ trì phiên họp Hội đồng Trường lần 3
', N'12:00', N'["123","124"]', N'Phòng họp 4', 0)
INSERT [dbo].[ExtratimeActive] ([PID], [Date], [Content], [Hour], [Participant], [Location], [KindActive]) VALUES (26, CAST(N'2019-01-24T00:00:00.000' AS DateTime), N'Phó Hiệu trưởng Ngô Hồng Điệp chủ trì họp Tổ quản lý và vận hành Hệ thống giảng dạy học tập trực tuyến E-Learning
', N'14:00', N'["125","126","127","128","129","130","131"]', N'Phòng họp 2', 0)
INSERT [dbo].[ExtratimeActive] ([PID], [Date], [Content], [Hour], [Participant], [Location], [KindActive]) VALUES (27, CAST(N'2019-01-24T00:00:00.000' AS DateTime), N'Phó Hiệu trưởng Lê Tuấn Anh dự họp mặt cùng với cán bộ, viên chức Sở Thông tin và Truyền thông tỉnh Bình Dương
', N'17:00', N'["125","126","127","128","129","130","131"]', N'Trung tâm công nghệ thông tin', NULL)
INSERT [dbo].[ExtratimeActive] ([PID], [Date], [Content], [Hour], [Participant], [Location], [KindActive]) VALUES (28, CAST(N'2019-01-25T00:00:00.000' AS DateTime), N'Phó Hiệu trưởng Lê Tuấn Anh dự chương trình “Tết vì người nghèo – Xuân Kỷ Hợi” và Lễ hội Xuân hồng năm 2019 (theo thư mời của UBND tỉnh)
', N'07:30', N'["124","125","126","127","128","129","130","131"]', N'Trung tâm Hội nghị triển lãm tỉnh', 0)
INSERT [dbo].[ExtratimeActive] ([PID], [Date], [Content], [Hour], [Participant], [Location], [KindActive]) VALUES (29, CAST(N'2019-01-25T00:00:00.000' AS DateTime), N'Hội nghị cán bộ viên chức và sơ kết học kỳ I, năm học 2018-2019 &quot;Theo kế hoạch HNVC trường 2018-2019, thành phần được cử đi tham dự Hội nghị cán bộ viên chức năm học 2018-2019 là Bí thư chi bộ, Trưởng các đơn vị (nếu trưởng đơn vị là lãnh đạo trường kiêm nhiệm thì cử Phó trưởng đơn vị), Giám đốc chương trình (nếu GĐCT là lãnh đạo khoa kiêm nhiệm thì cử phó giám đốc), Thư ký chương trình đào tạo, Tổ trưởng công đoàn&quot;
', N'14:30', N'["125","126","127","128","129","130","131"]', N'Hội trường 1', 0)
INSERT [dbo].[ExtratimeActive] ([PID], [Date], [Content], [Hour], [Participant], [Location], [KindActive]) VALUES (30, CAST(N'2019-01-25T00:00:00.000' AS DateTime), N'Tiệc tất niên mừng Xuân Kỷ Hợi 2019
', N'12:00', N'["123"]', N'Sân trước Hội trường 1', 0)
INSERT [dbo].[ExtratimeActive] ([PID], [Date], [Content], [Hour], [Participant], [Location], [KindActive]) VALUES (31, CAST(N'2019-01-26T00:00:00.000' AS DateTime), N'Phó Hiệu trưởng Lê Tuấn Anh dự họp mặt cùng cán bộ, viên chức Ngân hàng Vietcombank Bình Dương
', N'17:30', N'["123","124","125","126","127","128","129","130","131"]', N'Sảnh B1, Công viên văn hóa Thành Lễ', 0)
INSERT [dbo].[ExtratimeActive] ([PID], [Date], [Content], [Hour], [Participant], [Location], [KindActive]) VALUES (32, CAST(N'2019-05-10T00:00:00.000' AS DateTime), N'a
', N'12:00', N'["000"]', N'b', 2)
INSERT [dbo].[ExtratimeActive] ([PID], [Date], [Content], [Hour], [Participant], [Location], [KindActive]) VALUES (33, CAST(N'2019-05-10T00:00:00.000' AS DateTime), N'q
', N'04:15', N'["123"]', N'q', 2)
INSERT [dbo].[ExtratimeActive] ([PID], [Date], [Content], [Hour], [Participant], [Location], [KindActive]) VALUES (34, CAST(N'2019-05-11T00:00:00.000' AS DateTime), N'Họp cuối tháng
', N'16:15', N'["124","123","125","126","127","128","129","130","131"]', N'Phòng họp1', 2)
SET IDENTITY_INSERT [dbo].[ExtratimeActive] OFF
SET IDENTITY_INSERT [dbo].[PersonalExtratimeActive] ON 

INSERT [dbo].[PersonalExtratimeActive] ([Pid], [Content], [Date], [Time], [Location], [Proof], [KindActive], [TeacherCode], [Status]) VALUES (1, N'Conog tác
', CAST(N'2019-05-09T00:00:00.000' AS DateTime), N'15:10', N'a', N'/Images/1111/9520192230256_1111.jpg;/Images/1111/9520192237118_1111.jpeg;', 2, N'123', 0)
INSERT [dbo].[PersonalExtratimeActive] ([Pid], [Content], [Date], [Time], [Location], [Proof], [KindActive], [TeacherCode], [Status]) VALUES (2, N'Conog tácaaa
', CAST(N'2019-05-09T00:00:00.000' AS DateTime), N'15:10', N'a', N'', 2, N'123', 0)
SET IDENTITY_INSERT [dbo].[PersonalExtratimeActive] OFF
SET IDENTITY_INSERT [dbo].[Role] ON 

INSERT [dbo].[Role] ([Pid], [Code], [Name]) VALUES (1, N'Admin', N'Admin')
INSERT [dbo].[Role] ([Pid], [Code], [Name]) VALUES (2, N'TK', N'Trưởng Khoa')
INSERT [dbo].[Role] ([Pid], [Code], [Name]) VALUES (3, N'PK', N'Phó khoa')
INSERT [dbo].[Role] ([Pid], [Code], [Name]) VALUES (4, N'GV', N'Giảng viên')
SET IDENTITY_INSERT [dbo].[Role] OFF
SET IDENTITY_INSERT [dbo].[UserLogin] ON 

INSERT [dbo].[UserLogin] ([PID], [USER], [PASSWORD], [TOKEN], [IPLOGIN], [STATUS], [SALT], [Role], [FullName], [DayOfBirth], [Country], [CID], [Sex], [Code]) VALUES (1, N'admin@gmail.com', N'8917903F0B01559FCCC70125A944D2FE', NULL, NULL, 1, N'58c3915e-e162-43d8-9425-e95db6cc47bb', N'Admin', N'Châu Duy Cảnh', CAST(N'1997-08-12T00:00:00.000' AS DateTime), N'Bình Dương', N'1234679', 1, N'000')
INSERT [dbo].[UserLogin] ([PID], [USER], [PASSWORD], [TOKEN], [IPLOGIN], [STATUS], [SALT], [Role], [FullName], [DayOfBirth], [Country], [CID], [Sex], [Code]) VALUES (2, N'test1@gmail.com', N'123456', NULL, NULL, 1, NULL, N'TK', N'Nguyễn Trung Ty', CAST(N'1997-08-12T00:00:00.000' AS DateTime), N'Bình Dương', N'1234678', 1, N'1231')
INSERT [dbo].[UserLogin] ([PID], [USER], [PASSWORD], [TOKEN], [IPLOGIN], [STATUS], [SALT], [Role], [FullName], [DayOfBirth], [Country], [CID], [Sex], [Code]) VALUES (3, N'test2@gmail.com', N'123456', NULL, NULL, 1, NULL, N'PK', N'Nguyễn Đình Phong', CAST(N'1997-08-12T00:00:00.000' AS DateTime), N'Bình Dương', N'2234679', 1, N'124')
INSERT [dbo].[UserLogin] ([PID], [USER], [PASSWORD], [TOKEN], [IPLOGIN], [STATUS], [SALT], [Role], [FullName], [DayOfBirth], [Country], [CID], [Sex], [Code]) VALUES (4, N'test3@gmail.com', N'123456', NULL, NULL, 1, NULL, N'GV', N'Nguyễn Công Thành', CAST(N'1997-08-12T00:00:00.000' AS DateTime), N'Bình Dương', N'3234679', 1, N'125')
INSERT [dbo].[UserLogin] ([PID], [USER], [PASSWORD], [TOKEN], [IPLOGIN], [STATUS], [SALT], [Role], [FullName], [DayOfBirth], [Country], [CID], [Sex], [Code]) VALUES (5, N'test4@gmail.com', N'123456', NULL, NULL, 1, NULL, N'GV', N'Nguyễn Minh Thành', CAST(N'1997-08-12T00:00:00.000' AS DateTime), N'Bình Dương', N'4234679', 1, N'126')
INSERT [dbo].[UserLogin] ([PID], [USER], [PASSWORD], [TOKEN], [IPLOGIN], [STATUS], [SALT], [Role], [FullName], [DayOfBirth], [Country], [CID], [Sex], [Code]) VALUES (6, N'test5@gmail.com', N'123456', NULL, NULL, 1, NULL, N'GV', N'Nguyễn Minh Cảnh', CAST(N'1997-08-12T00:00:00.000' AS DateTime), N'Bình Dương', N'5234679', 1, N'127')
INSERT [dbo].[UserLogin] ([PID], [USER], [PASSWORD], [TOKEN], [IPLOGIN], [STATUS], [SALT], [Role], [FullName], [DayOfBirth], [Country], [CID], [Sex], [Code]) VALUES (7, N'test6@gmail.com', N'123456', NULL, NULL, 1, NULL, N'GV', N'Nguyễn Đức Mạnh', CAST(N'1997-08-12T00:00:00.000' AS DateTime), N'Bình Dương', N'6234679', 1, N'128')
INSERT [dbo].[UserLogin] ([PID], [USER], [PASSWORD], [TOKEN], [IPLOGIN], [STATUS], [SALT], [Role], [FullName], [DayOfBirth], [Country], [CID], [Sex], [Code]) VALUES (8, N'test7@gmail.com', N'123456', NULL, NULL, 1, NULL, N'GV', N'Nguyễn Duy Cảnh', CAST(N'1997-08-12T00:00:00.000' AS DateTime), N'Bình Dương', N'7234679', 1, N'129')
INSERT [dbo].[UserLogin] ([PID], [USER], [PASSWORD], [TOKEN], [IPLOGIN], [STATUS], [SALT], [Role], [FullName], [DayOfBirth], [Country], [CID], [Sex], [Code]) VALUES (9, N'test8@gmail.com', N'123456', NULL, NULL, 1, NULL, N'GV', N'Nguyễn Quốc Hùng', CAST(N'1997-08-12T00:00:00.000' AS DateTime), N'Bình Dương', N'8234679', 1, N'130')
INSERT [dbo].[UserLogin] ([PID], [USER], [PASSWORD], [TOKEN], [IPLOGIN], [STATUS], [SALT], [Role], [FullName], [DayOfBirth], [Country], [CID], [Sex], [Code]) VALUES (10, N'test9@gmail.com', N'123456', NULL, NULL, 1, NULL, N'GV', N'Nguyễn Quốc Cường', CAST(N'1997-08-12T00:00:00.000' AS DateTime), N'Bình Dương', N'9234679', 1, N'131')
INSERT [dbo].[UserLogin] ([PID], [USER], [PASSWORD], [TOKEN], [IPLOGIN], [STATUS], [SALT], [Role], [FullName], [DayOfBirth], [Country], [CID], [Sex], [Code]) VALUES (11, N'c@mail.com', N'E8A8E6C708AAE09828B26C7E6320AADB', NULL, NULL, 1, N'29170fe5-6cd7-4604-9524-821c06c26c83', N'GV', N'Canh', CAST(N'2019-08-05T00:00:00.000' AS DateTime), NULL, NULL, 1, N'123')
SET IDENTITY_INSERT [dbo].[UserLogin] OFF
SET IDENTITY_INSERT [dbo].[WorkingDefine] ON 

INSERT [dbo].[WorkingDefine] ([Pid], [CatePid], [Content], [Value]) VALUES (2, 2, N'Tham gia công tác tư vấn tuyển sinh
', 4)
INSERT [dbo].[WorkingDefine] ([Pid], [CatePid], [Content], [Value]) VALUES (3, 2, N'Thu hồ sơ nhập học đầu khóa hệ chính quy và hệ thường xuyên
', 4)
INSERT [dbo].[WorkingDefine] ([Pid], [CatePid], [Content], [Value]) VALUES (4, 2, N'Tham gia tổ chức lễ trao bằng tốt nghiệp
', 4)
INSERT [dbo].[WorkingDefine] ([Pid], [CatePid], [Content], [Value]) VALUES (5, 2, N'Tham gia kiểm tra hồ sơ đào tạo phục vụ kiểm định chất lượng đánh giá chương trình đào tạo
', 4)
INSERT [dbo].[WorkingDefine] ([Pid], [CatePid], [Content], [Value]) VALUES (6, 2, N'Tham gia kiểm tra hồ sơ đào tạo phục vụ kiểm định chất lượng, đánh giá chương trình đào tạo
', 4)
INSERT [dbo].[WorkingDefine] ([Pid], [CatePid], [Content], [Value]) VALUES (7, 2, N'Tham gia công tác khảo sát các bên liên quan phục vụ việc rà soát và điều chỉnh chương trình đào tạo
', 4)
INSERT [dbo].[WorkingDefine] ([Pid], [CatePid], [Content], [Value]) VALUES (8, 2, N'Hướng dẫn sinh viên đi thực tết, thực tập (ngoài chương trình đào tạo)
', 4)
INSERT [dbo].[WorkingDefine] ([Pid], [CatePid], [Content], [Value]) VALUES (9, 4, N'Tham dự ngày hội khoa học sinh viên;
 Ngày hội khoa học cán bộ; giảng viên trẻ và học viên cao học
', 4)
INSERT [dbo].[WorkingDefine] ([Pid], [CatePid], [Content], [Value]) VALUES (12, 4, N'Có tác phẩm văn học, nghệ thuật,(âm nhạc, mỹ thuật, sân khấu, điện ảnh, kiến trúc), chương trình biểu diễn tham gia các cuộc thi, thi đấu thể dục thể thao nhưng không đạt giải thưởng- Cấp trường		
', 4)
INSERT [dbo].[WorkingDefine] ([Pid], [CatePid], [Content], [Value]) VALUES (15, 4, N'Có tác phẩm văn học, nghệ thuật,(âm nhạc, mỹ thuật, sân khấu, điện ảnh, kiến trúc), chương trình biểu diễn tham gia các cuộc thi, thi đấu thể dục thể thao nhưng không đạt giải thưởng- Cấp Quốc tế		
', 10)
INSERT [dbo].[WorkingDefine] ([Pid], [CatePid], [Content], [Value]) VALUES (16, 4, N'Có tác phẩm văn học, nghệ thuật,(âm nhạc, mỹ thuật, sân khấu, điện ảnh, kiến trúc), chương trình biểu diễn tham gia các cuộc thi, thi đấu thể dục thể thao nhưng không đạt giải thưởng- Cấp Quốc gia
', 8)
INSERT [dbo].[WorkingDefine] ([Pid], [CatePid], [Content], [Value]) VALUES (17, 4, N'Có tác phẩm văn học, nghệ thuật,(âm nhạc, mỹ thuật, sân khấu, điện ảnh, kiến trúc), chương trình biểu diễn tham gia các cuộc thi, thi đấu thể dục thể thao nhưng không đạt giải thưởng- Cấp tỉnh	
', 6)
INSERT [dbo].[WorkingDefine] ([Pid], [CatePid], [Content], [Value]) VALUES (18, 5, N'Thực hiện công tác hành chính, hội họp, sinh hoạt chuyên môn, sinh hoạt chuyên đề và các công việc hành chính do lãnh đạo trường phân công
', 4)
INSERT [dbo].[WorkingDefine] ([Pid], [CatePid], [Content], [Value]) VALUES (23, 2, N'Hoạt động đào tạo
', 4)
SET IDENTITY_INSERT [dbo].[WorkingDefine] OFF
SET IDENTITY_INSERT [dbo].[WorkingDefineCate] ON 

INSERT [dbo].[WorkingDefineCate] ([Pid], [NameCate]) VALUES (2, N'Công tác đào tạo đại học')
INSERT [dbo].[WorkingDefineCate] ([Pid], [NameCate]) VALUES (4, N'Công tác nghiên cứu khoa học')
INSERT [dbo].[WorkingDefineCate] ([Pid], [NameCate]) VALUES (5, N'Công tác hành chính khoa')
SET IDENTITY_INSERT [dbo].[WorkingDefineCate] OFF
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Role]    Script Date: 05/13/2019 06:29:36 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Role] ON [dbo].[Role]
(
	[Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Role_1]    Script Date: 05/13/2019 06:29:36 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Role_1] ON [dbo].[Role]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Code]    Script Date: 05/13/2019 06:29:36 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Code] ON [dbo].[UserLogin]
(
	[Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Email]    Script Date: 05/13/2019 06:29:36 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Email] ON [dbo].[UserLogin]
(
	[PID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
USE [master]
GO
ALTER DATABASE [DOANTOTNGHIEP] SET  READ_WRITE 
GO
