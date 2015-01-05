/****** Object:  Table [dbo].[Abstract]    Script Date: 2014-5-9 16:46:53 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Abstract](
	[PGuid] [char](32) NOT NULL,
	[Abstract] [text] NULL,
	[Task] [varchar](50) null
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
print 'Abstract Success'

CREATE TABLE [dbo].[Author](
	[Name] [varchar](100) NOT NULL,
	[AbbreName] [varchar](100) NULL,
	[PGuid] [char](32) NULL,
	[Year] [int] NULL,
	[Order] [int] NULL,
	[Task] [varchar](50) null
) ON [PRIMARY]
go

print 'Author Success'
CREATE TABLE [dbo].[Category](
	[Category] [varchar](100) NOT NULL,
	[Type] [nvarchar](50) NULL,
	[PGuid] [char](32) NULL,
	[Year] [int] NULL,
	[Order] [int] NULL,
	[Task] [varchar](50) null
) ON [PRIMARY]

GO
print 'Category Success'

CREATE TABLE [dbo].[ReprintAuthor](
	[Name] [varchar](50) NULL,
	[NameAbbre] [varchar](200) NULL,
	[Country] [varchar](50) NULL,
	[Institute] [varchar](200) NULL,
	[Province] [varchar](200) NULL,
	[City] [varchar](200) NULL,
	[PostCode] [varchar](50) NULL,
	[Address] [varchar](500) NULL,
	[ins1] [varchar](200) NULL,
	[ins2] [varchar](200) NULL,
	[ins3] [varchar](200) NULL,
	[ins4] [varchar](200) NULL,
	[ins5] [varchar](200) NULL,
	[PGuid] [char](32) NULL,
	[Task] [varchar](50) null
) ON [PRIMARY]

GO

print 'ReprintAuthor Success'
CREATE TABLE [dbo].[Reference](
	[PGuid] [char](32) NULL,
	[Reference] [varchar](1000) NULL,
	[Order] [int] NULL,
	[Author] [varchar](500) NULL,
	[Year] [int] NULL,
	[Journal] [varchar](500) NULL,
	[Volume] [int] NULL,
	[Page] [int] NULL,
	[Doi] [varchar](500) NULL,
	[Task] [varchar](50) null
) ON [PRIMARY]

GO
print 'Reference Success'
CREATE TABLE [dbo].[Paper](
	[Guid] [char](32) NULL,
	[Title] [varchar](1000) NULL,
	[Source] [varchar](500) NULL,
	[Year] [int] NULL,
	[TotalCites] [int] NULL,
	[Language] [varchar](50) NULL,
	[Journal] [varchar](200) NULL,
	[JournalAbbreiation] [varchar](200) NULL,
	[ISSN] [char](11) NULL,
	[Month] [varchar](50) NULL,
	[Issue] [varchar](50) NULL,
	[Volume] [varchar](50) NULL,
	[PageBegin] [varchar](50) NULL,
	[PageEnd] [varchar](50) NULL,
	[PublishType] [varchar](50) NULL,
	[Email] [varchar](1000) NULL,
	[Publisher] [varchar](100) NULL,
	[CityofPublisher] [varchar](50) NULL,
	[AddressofPublisher] [varchar](200) NULL,
	[ArticleNumber] [varchar](50) NULL,
	[Doi] [varchar](1000) NULL,
	[IDSNumber] [varchar](50) NULL,
	[AccessionNumber] [varchar](50) NULL,
	[FundCount] [int] NULL,
	[AuthorCount] [int] NULL,
	[FullAuthorCount] [int] NULL,
	[CategoryCount] [int] NULL,
	[ResearchAreaCount] [int] NULL,
	[KeywordCount] [int] NULL,
	[KeywordPlusCount] [int] NULL,
	[ReferenceCount] [int] NULL,
	[DocumentTypeCount] [int] NULL,
	[Task] [varchar](50) NULL
) ON [PRIMARY]

GO
print 'Paper Success'

CREATE TABLE [dbo].[Keyword](
	[PGuid] [char](32) NULL,
	[Keyword] [varchar](700) NULL,
	[Type] [varchar](50) NULL,
	[Year] [int] NULL,
	[Order] [int] NULL,
	[Task] [varchar](50) null
) ON [PRIMARY]
GO
print 'Keyword Success'

CREATE TABLE [dbo].[Institute](
	[Country] [varchar](50) NULL,
	[Institute] [varchar](200) NULL,
	[Organization] [varchar](200) NULL,
	[Lab] [varchar](200) NULL,
	[Hospital] [varchar](200) NULL,
	[Province] [varchar](200) NULL,
	[City] [varchar](200) NULL,
	[PostCode] [varchar](50) NULL,
	[Address] [varchar](500) NULL,
	[ins1] [varchar](200) NULL,
	[ins2] [varchar](200) NULL,
	[ins3] [varchar](200) NULL,
	[ins4] [varchar](200) NULL,
	[ins5] [varchar](200) NULL,
	[PGuid] [char](32) NULL,
	[Guid] [char](32) NULL,
	[Task] [varchar](50) null
) ON [PRIMARY]

GO
print 'Institute Success'
CREATE TABLE [dbo].[DocumentType](
	[PGuid] [char](32) NULL,
	[DocumentType] [varchar](50) NULL,
	[Year] [int] NULL,
	[Order] [int] NULL,
[Task] [varchar](50) null
) ON [PRIMARY]

GO
print 'DocumentType Success'
CREATE TABLE [dbo].[Citation](
	[PGuid] [char](32) NULL,
	[RGuid] [char](32) NULL,
	[Year] [int] NULL,
	[Task] [varchar](50) null
) ON [PRIMARY]

GO
print 'Citation Success'

CREATE TABLE [dbo].[AuthorInstitute](
	[Author] [varchar](100) NULL,
	[Order] [int] NULL,
	[PGuid] [char](32) NULL,
	[IGuid] [char](32) NULL,
	[Task] [varchar](50) null
) ON [PRIMARY]

GO
print 'AuthorInsitute Success'

SET ANSI_PADDING OFF
GO

CREATE TABLE [dbo].[Fund](
	[PGuid] [varchar](32) NULL,
	[Funding] [nvarchar](500) NULL,
	[Code] [varchar](100) NULL,
	[Order] [int] NULL,
	[Task] [varchar](50) null
) ON [PRIMARY]
print 'Fund Success'


/****** Object:  Index [NonClusteredIndex-20140513-212910]    Script Date: 2014-5-16 22:13:12 ******/
CREATE NONCLUSTERED INDEX [NonClusteredIndex-20140513-212910] ON [dbo].[Abstract]
(
	[Task] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO


/****** Object:  Index [NonClusteredIndex-20140513-213027]    Script Date: 2014-5-16 22:13:32 ******/
CREATE NONCLUSTERED INDEX [NonClusteredIndex-20140513-213027] ON [dbo].[Abstract]
(
	[PGuid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

GO

/****** Object:  Index [NonClusteredIndex-20140513-213714]    Script Date: 2014-5-16 22:13:47 ******/
CREATE NONCLUSTERED INDEX [NonClusteredIndex-20140513-213714] ON [dbo].[Author]
(
	[PGuid] ASC,
	[Name] ASC,
	[AbbreName] ASC,
	[Year] ASC,
	[Order] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO


GO

/****** Object:  Index [NonClusteredIndex-20140514-002338]    Script Date: 2014-5-16 22:13:53 ******/
CREATE NONCLUSTERED INDEX [NonClusteredIndex-20140514-002338] ON [dbo].[Author]
(
	[Task] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

GO

/****** Object:  Index [NonClusteredIndex-20140514-002438]    Script Date: 2014-5-16 22:14:04 ******/
CREATE NONCLUSTERED INDEX [NonClusteredIndex-20140514-002438] ON [dbo].[AuthorInstitute]
(
	[Author] ASC,
	[Order] ASC,
	[PGuid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

GO

/****** Object:  Index [NonClusteredIndex-20140514-002501]    Script Date: 2014-5-16 22:14:12 ******/
CREATE NONCLUSTERED INDEX [NonClusteredIndex-20140514-002501] ON [dbo].[AuthorInstitute]
(
	[IGuid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO


GO

/****** Object:  Index [NonClusteredIndex-20140514-002512]    Script Date: 2014-5-16 22:14:21 ******/
CREATE NONCLUSTERED INDEX [NonClusteredIndex-20140514-002512] ON [dbo].[AuthorInstitute]
(
	[Task] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

GO

/****** Object:  Index [NonClusteredIndex-20140514-002529]    Script Date: 2014-5-16 22:14:40 ******/
CREATE NONCLUSTERED INDEX [NonClusteredIndex-20140514-002529] ON [dbo].[Category]
(
	[Task] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO


GO

/****** Object:  Index [NonClusteredIndex-20140514-002542]    Script Date: 2014-5-16 22:14:49 ******/
CREATE NONCLUSTERED INDEX [NonClusteredIndex-20140514-002542] ON [dbo].[Category]
(
	[Category] ASC,
	[Type] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

GO

/****** Object:  Index [NonClusteredIndex-20140514-002559]    Script Date: 2014-5-16 22:14:54 ******/
CREATE NONCLUSTERED INDEX [NonClusteredIndex-20140514-002559] ON [dbo].[Category]
(
	[Category] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

GO

/****** Object:  Index [NonClusteredIndex-20140514-002615]    Script Date: 2014-5-16 22:15:04 ******/
CREATE NONCLUSTERED INDEX [NonClusteredIndex-20140514-002615] ON [dbo].[Citation]
(
	[PGuid] ASC,
	[RGuid] ASC,
	[Year] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO


GO

/****** Object:  Index [NonClusteredIndex-20140514-002627]    Script Date: 2014-5-16 22:15:11 ******/
CREATE NONCLUSTERED INDEX [NonClusteredIndex-20140514-002627] ON [dbo].[Citation]
(
	[Task] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

GO

/****** Object:  Index [NonClusteredIndex-20140514-002639]    Script Date: 2014-5-16 22:15:19 ******/
CREATE NONCLUSTERED INDEX [NonClusteredIndex-20140514-002639] ON [dbo].[DocumentType]
(
	[PGuid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO


GO

/****** Object:  Index [NonClusteredIndex-20140514-002649]    Script Date: 2014-5-16 22:15:28 ******/
CREATE NONCLUSTERED INDEX [NonClusteredIndex-20140514-002649] ON [dbo].[DocumentType]
(
	[Task] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

GO

/****** Object:  Index [NonClusteredIndex-20140514-002700]    Script Date: 2014-5-16 22:15:34 ******/
CREATE NONCLUSTERED INDEX [NonClusteredIndex-20140514-002700] ON [dbo].[DocumentType]
(
	[DocumentType] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

GO

/****** Object:  Index [NonClusteredIndex-20140514-002716]    Script Date: 2014-5-16 22:15:43 ******/
CREATE NONCLUSTERED INDEX [NonClusteredIndex-20140514-002716] ON [dbo].[Fund]
(
	[PGuid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

GO

/****** Object:  Index [NonClusteredIndex-20140514-002725]    Script Date: 2014-5-16 22:15:49 ******/
CREATE NONCLUSTERED INDEX [NonClusteredIndex-20140514-002725] ON [dbo].[Fund]
(
	[Task] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

GO

/****** Object:  Index [NonClusteredIndex-20140514-002745]    Script Date: 2014-5-16 22:16:01 ******/
CREATE NONCLUSTERED INDEX [NonClusteredIndex-20140514-002745] ON [dbo].[Institute]
(
	[PGuid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

GO

/****** Object:  Index [NonClusteredIndex-20140514-002821]    Script Date: 2014-5-16 22:16:06 ******/
CREATE NONCLUSTERED INDEX [NonClusteredIndex-20140514-002821] ON [dbo].[Institute]
(
	[Task] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO


GO

/****** Object:  Index [NonClusteredIndex-20140514-002837]    Script Date: 2014-5-16 22:16:16 ******/
CREATE NONCLUSTERED INDEX [NonClusteredIndex-20140514-002837] ON [dbo].[Keyword]
(
	[Task] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

GO

/****** Object:  Index [NonClusteredIndex-20140514-002847]    Script Date: 2014-5-16 22:16:20 ******/
CREATE NONCLUSTERED INDEX [NonClusteredIndex-20140514-002847] ON [dbo].[Keyword]
(
	[PGuid] ASC,
	[Type] ASC,
	[Year] ASC,
	[Order] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO


GO

/****** Object:  Index [ClusteredIndex-20140516-221634]    Script Date: 2014-5-16 22:18:31 ******/
CREATE CLUSTERED INDEX [ClusteredIndex-20140516-221634] ON [dbo].[Paper]
(
	[Guid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO


GO

/****** Object:  Index [NonClusteredIndex-20140516-221702]    Script Date: 2014-5-16 22:18:42 ******/
CREATE NONCLUSTERED INDEX [NonClusteredIndex-20140516-221702] ON [dbo].[Paper]
(
	[Task] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO


/****** Object:  Index [NonClusteredIndex-20140516-221714]    Script Date: 2014-5-16 22:18:48 ******/
CREATE NONCLUSTERED INDEX [NonClusteredIndex-20140516-221714] ON [dbo].[Paper]
(
	[ISSN] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

GO

/****** Object:  Index [NonClusteredIndex-20140516-221740]    Script Date: 2014-5-16 22:18:53 ******/
CREATE NONCLUSTERED INDEX [NonClusteredIndex-20140516-221740] ON [dbo].[Paper]
(
	[AccessionNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO




GO

/****** Object:  Index [NonClusteredIndex-20140516-221801]    Script Date: 2014-5-16 22:18:59 ******/
CREATE NONCLUSTERED INDEX [NonClusteredIndex-20140516-221801] ON [dbo].[Paper]
(
	[Year] ASC,
	[Journal] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO



GO

/****** Object:  Index [NonClusteredIndex-20140514-003009]    Script Date: 2014-5-16 22:19:06 ******/
CREATE NONCLUSTERED INDEX [NonClusteredIndex-20140514-003009] ON [dbo].[Reference]
(
	[PGuid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

GO

/****** Object:  Index [NonClusteredIndex-20140514-003019]    Script Date: 2014-5-16 22:19:11 ******/
CREATE NONCLUSTERED INDEX [NonClusteredIndex-20140514-003019] ON [dbo].[Reference]
(
	[Task] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO


GO

/****** Object:  Index [NonClusteredIndex-20140514-003030]    Script Date: 2014-5-16 22:19:18 ******/
CREATE NONCLUSTERED INDEX [NonClusteredIndex-20140514-003030] ON [dbo].[ReprintAuthor]
(
	[Name] ASC,
	[NameAbbre] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO


GO

/****** Object:  Index [NonClusteredIndex-20140514-003115]    Script Date: 2014-5-16 22:19:22 ******/
CREATE NONCLUSTERED INDEX [NonClusteredIndex-20140514-003115] ON [dbo].[ReprintAuthor]
(
	[PGuid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO


GO

/****** Object:  Index [NonClusteredIndex-20140514-003124]    Script Date: 2014-5-16 22:19:27 ******/
CREATE NONCLUSTERED INDEX [NonClusteredIndex-20140514-003124] ON [dbo].[ReprintAuthor]
(
	[Task] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
