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
			WHERE object_id = OBJECT_ID(N'[DublinCore].[AgentAddress]') AND type in (N'U'))
DROP TABLE [DublinCore].[AgentAddress]
GO
/*
 * A resource that acts or has the power to act.
 * See http://purl.org/dc/terms/AgentAddress 
 */ 
CREATE TABLE [DublinCore].[AgentAddress]
(
	[AgentAddressId] [uniqueidentifier] NOT NULL,
	[POBox] [NVARCHAR](100),
	[Street] [NVARCHAR](100),
	[Street2] [NVARCHAR](100),
	[Street3] [NVARCHAR](100),
	[City] [NVARCHAR](50),
	[State] [NVARCHAR](50),
	[PostalCode] [NVARCHAR](20),
	[Country] [NVARCHAR](50),
	[Phone] [NVARCHAR](20),
	[Phone2] [NVARCHAR](20),
	[Fax] [NVARCHAR](20),
	[BoxId] [uniqueidentifier],
	[EMail] [NVARCHAR] (50),
	[WebAddress] [NVARCHAR] (80),
	[Note] [NVARCHAR] (256)
 CONSTRAINT [PK_AgentAddress] PRIMARY KEY CLUSTERED 
(
	[AgentAddressId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
EXEC sys.sp_addextendedproperty 
	@name=N'MS_Description', 
	@value=N'The Address of a agent (company or person).' , 
	@level0type=N'SCHEMA',
	@level0name=N'DublinCore', 
	@level1type=N'TABLE',
	@level1name=N'AgentAddress'
GO
EXEC sys.sp_addextendedproperty 
	@name=N'MS_Description', 
	@value=N'The unique Identifer for the AgentAddress.' , 
	@level0type=N'SCHEMA',
	@level0name=N'DublinCore', 
	@level1type=N'TABLE',
	@level1name=N'AgentAddress',
	@level2type=N'COLUMN',
	@level2name=N'AgentAddressId'
GO
EXEC sys.sp_addextendedproperty 
	@name=N'MS_Description', 
	@value=N'The Address for the PO BOX for the Agent.' , 
	@level0type=N'SCHEMA',
	@level0name=N'DublinCore', 
	@level1type=N'TABLE',
	@level1name=N'AgentAddress',
	@level2type=N'COLUMN',
	@level2name=N'POBox'
GO
EXEC sys.sp_addextendedproperty 
	@name=N'MS_Description', 
	@value=N'The First Address line for the Agent.' , 
	@level0type=N'SCHEMA',
	@level0name=N'DublinCore', 
	@level1type=N'TABLE',
	@level1name=N'AgentAddress',
	@level2type=N'COLUMN',
	@level2name=N'Street'
GO
EXEC sys.sp_addextendedproperty 
	@name=N'MS_Description', 
	@value=N'The Second Address line for the Agent.' , 
	@level0type=N'SCHEMA',
	@level0name=N'DublinCore', 
	@level1type=N'TABLE',
	@level1name=N'AgentAddress',
	@level2type=N'COLUMN',
	@level2name=N'Street2'
GO
EXEC sys.sp_addextendedproperty 
	@name=N'MS_Description', 
	@value=N'The Third Address line for the Agent.' , 
	@level0type=N'SCHEMA',
	@level0name=N'DublinCore', 
	@level1type=N'TABLE',
	@level1name=N'AgentAddress',
	@level2type=N'COLUMN',
	@level2name=N'Street3'
GO
EXEC sys.sp_addextendedproperty 
	@name=N'MS_Description', 
	@value=N'The City for the Agent.' , 
	@level0type=N'SCHEMA',
	@level0name=N'DublinCore', 
	@level1type=N'TABLE',
	@level1name=N'AgentAddress',
	@level2type=N'COLUMN',
	@level2name=N'City'
GO
EXEC sys.sp_addextendedproperty 
	@name=N'MS_Description', 
	@value=N'The state for the Agent.' , 
	@level0type=N'SCHEMA',
	@level0name=N'DublinCore', 
	@level1type=N'TABLE',
	@level1name=N'AgentAddress',
	@level2type=N'COLUMN',
	@level2name=N'State'
GO
EXEC sys.sp_addextendedproperty 
	@name=N'MS_Description', 
	@value=N'The postal code for the Agent.' , 
	@level0type=N'SCHEMA',
	@level0name=N'DublinCore', 
	@level1type=N'TABLE',
	@level1name=N'AgentAddress',
	@level2type=N'COLUMN',
	@level2name=N'PostalCode'
GO
EXEC sys.sp_addextendedproperty 
	@name=N'MS_Description', 
	@value=N'The country for the Agent.' , 
	@level0type=N'SCHEMA',
	@level0name=N'DublinCore', 
	@level1type=N'TABLE',
	@level1name=N'AgentAddress',
	@level2type=N'COLUMN',
	@level2name=N'Country'
GO
EXEC sys.sp_addextendedproperty 
	@name=N'MS_Description', 
	@value=N'The phone number for the Agent.' , 
	@level0type=N'SCHEMA',
	@level0name=N'DublinCore', 
	@level1type=N'TABLE',
	@level1name=N'AgentAddress',
	@level2type=N'COLUMN',
	@level2name=N'Phone'
GO
EXEC sys.sp_addextendedproperty 
	@name=N'MS_Description', 
	@value=N'The alternate phone number for the Agent.' , 
	@level0type=N'SCHEMA',
	@level0name=N'DublinCore', 
	@level1type=N'TABLE',
	@level1name=N'AgentAddress',
	@level2type=N'COLUMN',
	@level2name=N'Phone2'
GO
EXEC sys.sp_addextendedproperty 
	@name=N'MS_Description', 
	@value=N'The fax number for the Agent.' , 
	@level0type=N'SCHEMA',
	@level0name=N'DublinCore', 
	@level1type=N'TABLE',
	@level1name=N'AgentAddress',
	@level2type=N'COLUMN',
	@level2name=N'Fax'
GO
EXEC sys.sp_addextendedproperty 
	@name=N'MS_Description', 
	@value=N'The Dublin Core BoxId for the Agent.' , 
	@level0type=N'SCHEMA',
	@level0name=N'DublinCore', 
	@level1type=N'TABLE',
	@level1name=N'AgentAddress',
	@level2type=N'COLUMN',
	@level2name=N'BoxId'
GO
EXEC sys.sp_addextendedproperty 
	@name=N'MS_Description', 
	@value=N'The Email Address for the Agent.' , 
	@level0type=N'SCHEMA',
	@level0name=N'DublinCore', 
	@level1type=N'TABLE',
	@level1name=N'AgentAddress',
	@level2type=N'COLUMN',
	@level2name=N'Email'
GO
EXEC sys.sp_addextendedproperty 
	@name=N'MS_Description', 
	@value=N'The Web Address for the Agent.' , 
	@level0type=N'SCHEMA',
	@level0name=N'DublinCore', 
	@level1type=N'TABLE',
	@level1name=N'AgentAddress',
	@level2type=N'COLUMN',
	@level2name=N'WebAddress'
GO
EXEC sys.sp_addextendedproperty 
	@name=N'MS_Description', 
	@value=N'Any notes for the Agent Adress.' , 
	@level0type=N'SCHEMA',
	@level0name=N'DublinCore', 
	@level1type=N'TABLE',
	@level1name=N'AgentAddress',
	@level2type=N'COLUMN',
	@level2name=N'Note'
GO
/*
 * Grant the table permissions 
 */
GRANT SELECT, INSERT, UPDATE, DELETE 
	ON [DublinCore].[AgentAddress]
	TO CRIMSONREADER_EDITOR, CRIMSONREADER_ADMINISTRATOR
GO	
GRANT SELECT
	ON [DublinCore].[AgentAddress]
	TO CRIMSONREADER_READER
go
Insert into [DublinCore].[AgentAddress]
(
	[AgentAddressId],
	[POBox],
	[Street],
	[Street2],
	[Street3],
	[City],
	[State],
	[PostalCode],
	[Country],
	[Phone],
	[Phone2],
	[Fax],
	[BoxId],
	[EMail],
	[WebAddress],
	[Note]
)
VALUES
(NEWID(), NULL, '809 North 1500 West', NULL, NULL, 'Salt Lake City', 'UT', '84116', 'United States', NULL, NULL, NULL, NULL, 'help@pglaf.org', 'http://www.gutenberg.org', NULL ),
(NEWID(), NULL, '620 Eighth Avenue', NULL, NULL, 'New York', 'NY', '10018', 'United States', '(212) 556-1831', '(212)556-3622', NULL, NULL, 'executive-editor@nytimes.com', 'http://www.nytimes.com/', NULL ),
(NEWID(), NULL, '101 Independence Ave, SE', NULL, NULL, 'Washington', 'DC', '20540', 'United States', '(202) 707-5000', NULL, NULL, NULL, NULL, 'http://www.loc.gov', NULL )