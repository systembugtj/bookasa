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
			WHERE object_id = OBJECT_ID(N'[DublinCore].[MethodOfAccrual]') AND type in (N'U'))
DROP TABLE [DublinCore].[MethodOfAccrual]
GO
/*
 * A method by which resources are added to a collection.
 * See http://purl.org/dc/terms/MethodOfAccrual 
 */ 
CREATE TABLE [DublinCore].[MethodOfAccrual](
	[MethodOfAccrualId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_MethodOfAccrual] PRIMARY KEY CLUSTERED 
(
	[MethodOfAccrualId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
EXEC sys.sp_addextendedproperty 
	@name=N'MS_Description', 
	@value=N'A method by which resources are added to a collection.', 
	@level0type=N'SCHEMA',
	@level0name=N'DublinCore', 
	@level1type=N'TABLE',
	@level1name=N'MethodOfAccrual'
GO
EXEC sys.sp_addextendedproperty 
	@name=N'MS_Description', 
	@value=N'The unique Identifer for the MethodOfAccrual.' , 
	@level0type=N'SCHEMA',
	@level0name=N'DublinCore', 
	@level1type=N'TABLE',
	@level1name=N'MethodOfAccrual',
	@level2type=N'COLUMN',
	@level2name=N'MethodOfAccrualId'
GO
/*
 * Grant the table permissions 
 */
GRANT SELECT, INSERT, UPDATE, DELETE 
	ON [DublinCore].[MethodOfAccrual]
	TO CRIMSONREADER_EDITOR, CRIMSONREADER_ADMINISTRATOR
GO	
GRANT SELECT
	ON [DublinCore].[MethodOfAccrual]
	TO CRIMSONREADER_READER