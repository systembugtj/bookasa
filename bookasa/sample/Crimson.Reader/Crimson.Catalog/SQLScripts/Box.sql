/*  Microsoft Public License (Ms-PL)
 * 
 * This license governs use of the accompanying software. If you use the 
 * software, you accept this license. If you do not accept the license, do not 
 * use the software.
 * 
 * 1. Definitions
 * 
 * The terms "reproduce," "reproduction," "derivative works," and "distribution" 
 * have the same meaning here as under U.S. copyright law.
 * 
 * A "contribution" is the original software, or any additions or changes to 
 * the software.
 * 
 * A "contributor" is any person that distributes its contribution under this 
 * license.
 * 
 * "Licensed patents" are a contributor's patent claims that read directly on 
 * its contribution.
 * 
 * 2. Grant of Rights
 * 
 * (A) Copyright Grant- Subject to the terms of this license, including the 
 * license conditions and limitations in section 3, each contributor grants you 
 * a non-exclusive, worldwide, royalty-free copyright license to reproduce its 
 * contribution, prepare derivative works of its contribution, and distribute its 
 * contribution or any derivative works that you create.
 * 
 * (B) Patent Grant- Subject to the terms of this license, including the 
 * license conditions and limitations in section 3, each contributor grants 
 * you a non-exclusive, worldwide, royalty-free license under its licensed 
 * patents to make, have made, use, sell, offer for sale, import, and/or 
 * otherwise dispose of its contribution in the software or derivative works of 
 * the contribution in the software.
 * 
 * 3. Conditions and Limitations
 * 
 * (A) No Trademark License- This license does not grant you rights to use 
 * any contributors' name, logo, or trademarks.
 * 
 * (B) If you bring a patent claim against any contributor over patents that 
 * you claim are infringed by the software, your patent license from such 
 * contributor to the software ends automatically.
 * 
 * (C) If you distribute any portion of the software, you must retain all 
 * copyright, patent, trademark, and attribution notices that are present in 
 * the software.
 * 
 * (D) If you distribute any portion of the software in source code form, 
 * you may do so only under this license by including a complete copy of 
 * this license with your distribution. If you distribute any portion of the 
 * software in compiled or object code form, you may only do so under a license
 * that complies with this license.
 * 
 * (E) The software is licensed "as-is." You bear the risk of using it. The 
 * contributors give no express warranties, guarantees or conditions. You may 
 * have additional consumer rights under your local laws which this license 
 * cannot change. To the extent permitted under your local laws, the 
 * contributors exclude the implied warranties of merchantability, fitness 
 * for a particular purpose and non-infringement.  
 * 
 * This Software is Copyright (c)2009 by LigoSoftware.com
 */

/*
 * Drop the table and any foreign keys
 */
IF  EXISTS (SELECT * 
			FROM sys.objects 
			WHERE object_id = OBJECT_ID(N'[DublinCore].[Box]') AND type in (N'U'))
DROP TABLE [DublinCore].[Box]
GO
/*
 * The DCMI Box encoding scheme is a method for identifying a region of space using 
 * its geographic limits and representing that information as a value string.
 * See http://dublincore.org/documents/dcmi-box/index.shtml#dcmes
 */ 
CREATE TABLE [DublinCore].[Box](
	[BoxId] [uniqueidentifier] NOT NULL,
	[Name] [NVARCHAR](50) NOT NULL,
	[NorthLimit] [NVARCHAR](20),
	[EastLimit] [NVARCHAR](20),
	[SouthLimit] [NVARCHAR](20),
	[WestLimit] [NVARCHAR](20),
	[UpLimit] [NVARCHAR](20),
	[DownLimit] [NVARCHAR](20),
	[Units] [NVARCHAR](20),
	[ZUnits] [NVARCHAR](20),
	[Projection] [NVARCHAR](20),
 CONSTRAINT [PK_Box] PRIMARY KEY CLUSTERED 
(
	[BoxId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
EXEC sys.sp_addextendedproperty 
	@name=N'MS_Description', 
	@value=N'The DCMI Box encoding scheme is a method for identifying a region of space using its geographic limits and representing that information as a value string. Components of the value string correspond to the bounding coordinates in north, south, east and west directions, plus optionally up and down, and also allow the coordinate system and units to be specified, and a name if desired. A method for encoding DCMI Box in a text string using the DCSV syntax is described. This notation is intended for representing a value of the DCMES element Coverage, particularly when using HTML meta elements.' , 
	@level0type=N'SCHEMA',
	@level0name=N'DublinCore', 
	@level1type=N'TABLE',
	@level1name=N'Box'
GO
EXEC sys.sp_addextendedproperty 
	@name=N'MS_Description', 
	@value=N'The unique Identifer for the Box.' , 
	@level0type=N'SCHEMA',
	@level0name=N'DublinCore', 
	@level1type=N'TABLE',
	@level1name=N'Box',
	@level2type=N'COLUMN',
	@level2name=N'BoxId'
GO
EXEC sys.sp_addextendedproperty 
	@name=N'MS_Description', 
	@value=N'The constant coordinate for the northernmost face or edge.', 
	@level0type=N'SCHEMA',
	@level0name=N'DublinCore', 
	@level1type=N'TABLE',
	@level1name=N'Box',
	@level2type=N'COLUMN',
	@level2name=N'NorthLimit'
GO
EXEC sys.sp_addextendedproperty 
	@name=N'MS_Description', 
	@value=N'The constant coordinate for the easternmost face or edge.', 
	@level0type=N'SCHEMA',
	@level0name=N'DublinCore', 
	@level1type=N'TABLE',
	@level1name=N'Box',
	@level2type=N'COLUMN',
	@level2name=N'EastLimit'
GO
EXEC sys.sp_addextendedproperty 
	@name=N'MS_Description', 
	@value=N'The constant coordinate for the southernmost face or edge.', 
	@level0type=N'SCHEMA',
	@level0name=N'DublinCore', 
	@level1type=N'TABLE',
	@level1name=N'Box',
	@level2type=N'COLUMN',
	@level2name=N'SouthLimit'
GO
EXEC sys.sp_addextendedproperty 
	@name=N'MS_Description', 
	@value=N'The constant coordinate for the westernmost face or edge.', 
	@level0type=N'SCHEMA',
	@level0name=N'DublinCore', 
	@level1type=N'TABLE',
	@level1name=N'Box',
	@level2type=N'COLUMN',
	@level2name=N'WestLimit'
GO
EXEC sys.sp_addextendedproperty 
	@name=N'MS_Description', 
	@value=N'The constant coordinate for the uppermost face or edge.', 
	@level0type=N'SCHEMA',
	@level0name=N'DublinCore', 
	@level1type=N'TABLE',
	@level1name=N'Box',
	@level2type=N'COLUMN',
	@level2name=N'UpLimit'
GO
EXEC sys.sp_addextendedproperty 
	@name=N'MS_Description', 
	@value=N'The constant coordinate for the lowermost face or edge.', 
	@level0type=N'SCHEMA',
	@level0name=N'DublinCore', 
	@level1type=N'TABLE',
	@level1name=N'Box',
	@level2type=N'COLUMN',
	@level2name=N'DownLimit'
GO
EXEC sys.sp_addextendedproperty 
	@name=N'MS_Description', 
	@value=N'The units applying to unlabelled numeric values of northlimit, eastlimit, southlimit, westlimit', 
	@level0type=N'SCHEMA',
	@level0name=N'DublinCore', 
	@level1type=N'TABLE',
	@level1name=N'Box',
	@level2type=N'COLUMN',
	@level2name=N'Units'
GO
EXEC sys.sp_addextendedproperty 
	@name=N'MS_Description', 
	@value=N'The units applying to unlabelled numeric values of uplimit, downlimit.', 
	@level0type=N'SCHEMA',
	@level0name=N'DublinCore', 
	@level1type=N'TABLE',
	@level1name=N'Box',
	@level2type=N'COLUMN',
	@level2name=N'ZUnits'
GO
EXEC sys.sp_addextendedproperty 
	@name=N'MS_Description', 
	@value=N'The name of the projection used with any parameters required, such as ellipsoid parameters, datum, standard parallels and meridians, zone, etc.', 
	@level0type=N'SCHEMA',
	@level0name=N'DublinCore', 
	@level1type=N'TABLE',
	@level1name=N'Box',
	@level2type=N'COLUMN',
	@level2name=N'Projection'
GO
EXEC sys.sp_addextendedproperty 
	@name=N'MS_Description', 
	@value=N'A name for the place.', 
	@level0type=N'SCHEMA',
	@level0name=N'DublinCore', 
	@level1type=N'TABLE',
	@level1name=N'Box',
	@level2type=N'COLUMN',
	@level2name=N'Name'
GO
/*
 * Grant the table permissions 
 */
GRANT SELECT, INSERT, UPDATE, DELETE 
	ON [DublinCore].[Box]
	TO CRIMSONREADER_EDITOR, CRIMSONREADER_ADMINISTRATOR
GO	
GRANT SELECT
	ON [DublinCore].[Box]
	TO CRIMSONREADER_READER