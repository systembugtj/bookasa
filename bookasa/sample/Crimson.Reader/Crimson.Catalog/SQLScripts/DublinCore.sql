IF  EXISTS (SELECT * 
			FROM sys.objects 
			WHERE object_id = OBJECT_ID(N'[DublinCore].[Agent]') 
				AND type in (N'U'))
DROP TABLE [DublinCore].[Agent]
IF  EXISTS (SELECT * 
			FROM sys.foreign_keys 
			WHERE object_id = OBJECT_ID(N'[DublinCore].[FK_PeriodOfTime]') 
				AND parent_object_id = OBJECT_ID(N'[DublinCore].[ResourcePeriodOfTime]'))
ALTER TABLE [DublinCore].[ResourcePeriodOfTime] 
	DROP CONSTRAINT [FK_PeriodOfTime]
GO
IF  EXISTS (SELECT * 
			FROM sys.foreign_keys 
			WHERE object_id = OBJECT_ID(N'[DublinCore].[FK_Resource]') 
				AND parent_object_id = OBJECT_ID(N'[DublinCore].[ResourcePeriodOfTime]'))
ALTER TABLE [DublinCore].[ResourcePeriodOfTime] 
	DROP CONSTRAINT [FK_Resource]
GO
IF  EXISTS (SELECT * 
			FROM sys.objects 
			WHERE object_id = OBJECT_ID(N'[DublinCore].[ResourcePeriodOfTime]') 
				AND type in (N'U'))
DROP TABLE [DublinCore].[ResourcePeriodOfTime]
GO
IF  EXISTS (SELECT * 
			FROM sys.objects 
			WHERE object_id = OBJECT_ID(N'[DublinCore].[PeriodOfTime]') 
				AND type in (N'U'))
DROP TABLE [DublinCore].[PeriodOfTime]
GO
IF  EXISTS (SELECT * 
			FROM sys.objects 
			WHERE object_id = OBJECT_ID(N'[DublinCore].[Resource]') 
				AND type in (N'U'))
DROP TABLE [DublinCore].[Resource]
/*
 * Drop and create the DublinCore Schema
 */
IF  EXISTS (SELECT * FROM sys.schemas WHERE name = N'DublinCore')
DROP SCHEMA [DublinCore]
GO
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
 * Drop the tables in the correct order
 */
CREATE SCHEMA [DublinCore] AUTHORIZATION [CrimsonReader_Administrator]
GO
EXEC sys.sp_addextendedproperty 
	@name=N'MS_Description', 
	@value=N'A Database schema that supoorts the Dublin Core 2.0 definition (See http://www.dublincore.org/documents/dcmi-terms/).' , 
	@level0type=N'SCHEMA',
	@level0name=N'DublinCore' 
