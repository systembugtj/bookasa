/*
 * Drop the table and any foreign keys
 */
IF  EXISTS (SELECT * 
			FROM sys.objects 
			WHERE object_id = OBJECT_ID(N'[DublinCore].[MARCRelator]') AND type in (N'U'))
DROP TABLE [DublinCore].[MARCRelator]
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
 *
 * MARC Code List: Relator Codes -- Term Sequence
 * See http://www.loc.gov/marc/relators/relaterm.html
 */ 
CREATE TABLE [DublinCore].[MARCRelator](
	[MARCRealtorId] [NVARCHAR](3) NOT NULL,
	[Title] [NVARCHAR](50),
	[DESCRIPTION] [NVARCHAR](512)
 CONSTRAINT [PK_MARCRelator] PRIMARY KEY CLUSTERED 
(
	[MARCRealtorId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
EXEC sys.sp_addextendedproperty 
	@name=N'MS_Description', 
	@value=N'The Library of Congress Marc relators for a resource.  See http://www.loc.gov/marc/relators/relaterm.html', 
	@level0type=N'SCHEMA',
	@level0name=N'DublinCore', 
	@level1type=N'TABLE',
	@level1name=N'MARCRelator'
GO
EXEC sys.sp_addextendedproperty 
	@name=N'MS_Description', 
	@value=N'The MARC Relator Code.' , 
	@level0type=N'SCHEMA',
	@level0name=N'DublinCore', 
	@level1type=N'TABLE',
	@level1name=N'MARCRelator',
	@level2type=N'COLUMN',
	@level2name=N'MARCRealtorId'
GO
EXEC sys.sp_addextendedproperty 
	@name=N'MS_Description', 
	@value=N'The Title of the Relator.' , 
	@level0type=N'SCHEMA',
	@level0name=N'DublinCore', 
	@level1type=N'TABLE',
	@level1name=N'MARCRelator',
	@level2type=N'COLUMN',
	@level2name=N'Title'
GO
EXEC sys.sp_addextendedproperty 
	@name=N'MS_Description', 
	@value=N'A description of the MARC relator code.' , 
	@level0type=N'SCHEMA',
	@level0name=N'DublinCore', 
	@level1type=N'TABLE',
	@level1name=N'MARCRelator',
	@level2type=N'COLUMN',
	@level2name=N'Description'
GO
/*
 * Grant the table permissions 
 */
GRANT SELECT, INSERT, UPDATE, DELETE 
	ON [DublinCore].[MARCRelator]
	TO CRIMSONREADER_EDITOR, CRIMSONREADER_ADMINISTRATOR
GO	
GRANT SELECT
	ON [DublinCore].[MARCRelator]
	TO CRIMSONREADER_READER
/*
 * Populate the table with the MARC relators
 */
Insert Into [DublinCore].[MARCRelator] (MARCRealtorId, Title, Description)
VALUES
	( 'acp', 'Art copyist', 'Use for a person (e.g., a painter or sculptor) who makes copies of works of visual art.' ),
	( 'act', 'Actor', 'Use for a person or organization who principally exhibits acting skills in a musical or dramatic presentation or entertainment.' ),
	( 'adp', 'Adaptor', 'Use for a person or organization who 1) reworks a musical composition, usually for a different medium, or 2) rewrites novels or stories for motion pictures or other audiovisual medium.' ),
	( 'aft', 'Author of afterword, colophon, etc.', 'Use for a person or organization responsible for an afterword, postface, colophon, etc. but who is not the chief author of a work.'  ),
	( 'anl', 'Analyst', 'Use for a person or organization that reviews, examines and interprets data or information in a specific area.' ),
	( 'anm', 'Animator', 'Use for a person or organization who draws the two-dimensional figures, manipulates the three dimensional objects and/or also programs the computer to move objects and images for the purpose of animated film processing. Animation cameras, stands, celluloid screens, transparencies and inks are some of the tools of the animator.' ),
	( 'ann', 'Annotator', 'Use for a person who writes manuscript annotations on a printed item.' ),
	( 'ant', 'Bibliographic antecedent', 'Use for a person or organization responsible for a work upon which the work represented by the catalog record is based. This may be appropriate for adaptations, sequels, continuations, indexes, etc.' ),
	( 'app', 'Applicant', 'Use for a person or organization responsible for the submission of an application or who is named as eligible for the results of the processing of the application (e.g., bestowing of rights, reward, title, position).' ),
	( 'aqt', 'Author in quotations or text abstracts', 'Use for a person or organization whose work is largely quoted or extracted in works to which he or she did not contribute directly. Such quotations are found particularly in exhibition catalogs, collections of photographs, etc.' ),
	( 'arc', 'Architect ', 'Use for a person or organization who designs structures or oversees their construction.' ),
	( 'ard', 'Artistic director', 'Use for a person responsible for controlling the development of the artistic style of an entire production, including the choice of works to be presented and selection of senior production staff.' ),
	( 'arr', 'Arranger', 'Use for a person or organization who transcribes a musical composition, usually for a different medium from that of the original; in an arrangement the musical substance remains essentially unchanged.' ),
	( 'art', 'Artist', 'Use for a person (e.g., a painter) or organization who conceives, and perhaps also implements, an original graphic design or work of art, if specific codes (e.g., [egr], [etr]) are not desired. For book illustrators, prefer Illustrator [ill].' ), 
	( 'asg', 'Assignee', 'Use for a person or organization to whom a license for printing or publishing has been transferred.' ), 
	( 'asn', 'Associated name', 'Use for a person or organization associated with or found in an item or collection, which cannot be determined to be that of a Former owner [fmo] or other designated relator indicative of provenance.' ), 
	( 'att', 'Attributed name', 'Use for an author, artist, etc., relating him/her to a work for which there is or once was substantial authority for designating that person as author, creator, etc. of the work.' ), 
	( 'auc', 'Auctioneer', 'Use for a person or organization in charge of the estimation and public auctioning of goods, particularly books, artistic works, etc.' ), 
	( 'aud', 'Author of dialog', 'Use for a person or organization responsible for the dialog or spoken commentary for a screenplay or sound recording.' ), 
	( 'aui', 'Author of introduction, etc.', 'Use for a person or organization responsible for an introduction, preface, foreword, or other critical introductory matter, but who is not the chief author' ), 
	( 'aus', 'Author of screenplay, etc.', 'Use for a person or organization responsible for a motion picture screenplay, dialog, spoken commentary, etc.' ), 
	( 'aut', 'Author', 'Use for a person or organization chiefly responsible for the intellectual or artistic content of a work, usually printed text. This term may also be used when more than one person or body bears such responsibility.' ), 
	( 'bdd', 'Binding designer', 'Use for a person or organization responsible for the binding design of a book, including the type of binding, the type of materials used, and any decorative aspects of the binding.' ), 
	( 'bjd', 'Bookjacket designer', 'Use for a person or organization responsible for the design of flexible covers designed for or published with a book, including the type of materials used, and any decorative aspects of the bookjacket.' ), 
	( 'bkd', 'Book designer', 'Use for a person or organization responsible for the entire graphic design of a book, including arrangement of type and illustration, choice of materials, and process used.' ),
	( 'bkp', 'Book producer', 'Use for a person or organization responsible for the production of books and other print media, if specific codes (e.g., [bkd], [egr], [tyd], [prt]) are not desired.' ),
	( 'bnd', 'Binder', 'Use for a person or organization responsible for the binding of printed or manuscript materials.' ),
	( 'bpd', 'Bookplate designer', 'Use for a person or organization responsible for the design of a book owner''s identification label that is most commonly pasted to the inside front cover of a book.' ),
	( 'bsl', 'Bookseller', 'Use for a person or organization who makes books and other bibliographic materials available for purchase. Interest in the materials is primarily lucrative.' ),
	( 'ccp', 'Conceptor', 'Use for a person or organization responsible for the original idea on which a work is based, this includes the scientific author of an audio-visual item and the conceptor of an advertisement.' ),
	( 'chr', 'Choreographer', 'Use for a person or organization who composes or arranges dances or other movements (e.g., "master of swords") for a musical or dramatic presentation or entertainment.' ),
	( 'clb', 'Collaborator', 'Use for a person or organization that takes a limited part in the elaboration of a work of another person or organization that brings complements (e.g., appendices, notes) to the work.' ),
	( 'cli', 'Client', 'Use for a person or organization for whom another person or organization is acting.' ),
	( 'cll', 'Calligrapher', 'Use for a person or organization who writes in an artistic hand, usually as a copyist and or engrosser.' ),
	( 'clt', 'Collotyper', 'Use for a person or organization responsible for the production of photographic prints from film or other colloid that has ink-receptive and ink-repellent surfaces.' ),
	( 'cmm', 'Commentator', 'Use for a person or organization who provides interpretation, analysis, or a discussion of the subject matter on a recording, motion picture, or other audiovisual medium.' ),
	( 'cmp', 'Composer', 'Use for a person or organization who creates a musical work, usually a piece of music in manuscript or printed form.' ),
	( 'cmt', 'Compositor', 'Use for a person or organization responsible for the creation of metal slug, or molds made of other materials, used to produce the text and images in printed matter.' ),
	( 'cng', 'Cinematographer', 'Use for a person or organization who is in charge of the images captured for a motion picture film. The cinematographer works under the supervision of a director, and may also be referred to as director of photography. Do not confuse with videographer.' ),
	( 'cnd', 'Conductor', 'Use for a person who directs a performing group (orchestra, chorus, opera, etc.) in a musical or dramatic presentation or entertainment.' ),
	( 'cns', 'Censor', 'Use for a censor, bowdlerizer, expurgator, etc., official or private.' ),
	( 'coe', 'Contestant-appellee', 'Use for a contestant against whom an appeal is taken from one court of law or jurisdiction to another to reverse the judgment.' ),
	( 'col', 'Collector', 'Use for a person or organization who has brought together material from various sources that has been arranged, described, and cataloged as a collection. A collector is neither the creator of the material nor a person to whom manuscripts in the collection may have been addressed.' ),
	( 'com', 'Compiler ', 'Use for a person or organization who produces a work or publication by selecting and putting together material from the works of various persons or bodies.' ),
	( 'cos', 'Contestant', 'Use for the party who opposes, resists, or disputes, in a court of law, a claim, decision, result, etc.' ), 
	( 'cot', 'Contestant-appellant', 'Use for a contestant who takes an appeal from one court of law or jurisdiction to another to reverse the judgment.' ),
	( 'cov', 'Cover designer', 'Use for a person or organization responsible for the graphic design of a book cover, album cover, slipcase, box, container, etc. For a person or organization responsible for the graphic design of an entire book, use Book designer; for book jackets, use Bookjacket designer.' ),
	( 'cpc', 'Copyright claimant', 'Use for a person or organization listed as a copyright owner at the time of registration. Copyright can be granted or later transferred to another person or organization, at which time the claimant becomes the copyright holder' ),
	( 'cpe', 'Complainant-appellee', 'Use for a complainant against whom an appeal is taken from one court or jurisdiction to another to reverse the judgment, usually in an equity proceeding.' ),
	( 'cph', 'Copyright holder', 'Use for a person or organization to whom copy and legal rights have been granted or transferred for the intellectual content of a work. The copyright holder, although not necessarily the creator of the work, usually has the exclusive right to benefit financially from the sale and use of the work to which the associated copyright protection applies.' ),
	( 'cpl', 'Complainant', 'Use for the party who applies to the courts for redress, usually in an equity proceeding.' ),
	( 'cpt', 'Complainant-appellant', 'Use for a complainant who takes an appeal from one court or jurisdiction to another to reverse the judgment, usually in an equity proceeding.' ),
	( 'cre', 'Creator', 'Use for a person or organization responsible for the intellectual or artistic content of a work.' ),
	( 'crp', 'Correspondent', 'Use for a person or organization who was either the writer or recipient of a letter or other communication.' ),
	( 'crr', 'Corrector', 'Use for a person or organization who is a corrector of manuscripts, such as the scriptorium official who corrected the work of a scribe. For printed matter, use Proofreader.' ),
	( 'csl', 'Consultant', 'Use for a person or organization relevant to a resource, who is called upon for professional advice or services in a specialized field of knowledge or training.' ),
	( 'csp', 'Consultant to a project', 'Use for a person or organization relevant to a resource, who is engaged specifically to provide an intellectual overview of a strategic or operational task and by analysis, specification, or instruction, to create or propose a cost-effective course of action or solution.' ),
	( 'cst', 'Costume designer', 'Use for a person or organization who designs or makes costumes, fixes hair, etc., for a musical or dramatic presentation or entertainment.' ),
	( 'ctb', 'Contributor', 'Use for a person or organization one whose work has been contributed to a larger work, such as an anthology, serial publication, or other compilation of individual works. Do not use if the sole function in relation to a work is as author, editor, compiler or translator.' ),
	( 'cte', 'Contestee-appellee', 'Use for a contestee against whom an appeal is taken from one court or jurisdiction to another to reverse the judgment.' ),
	( 'ctg', 'Cartographer', 'Use for a person or organization responsible for the creation of maps and other cartographic materials.' ),
	( 'ctr', 'Contractor', 'Use for a person or organization relevant to a resource, who enters into a contract with another person or organization to perform a specific task.' ),
	( 'cts', 'Contestee', 'Use for the party defending a claim, decision, result, etc. being opposed, resisted, or disputed in a court of law.' ),
	( 'ctt', 'Contestee-appellant', 'Use for a contestee who takes an appeal from one court or jurisdiction to another to reverse the judgment.' ),
	( 'cur', 'Curator of an exhibition', 'Use for a person or organization responsible for conceiving and organizing an exhibition.' ),
	( 'cwt', 'Commentator for written text', 'Use for a person or organization responsible for the commentary or explanatory notes about a text. For the writer of manuscript annotations in a printed book, use Annotator [ann].' ),
	( 'dfd', 'Defendant', 'Use for the party defending or denying allegations made in a suit and against whom relief or recovery is sought in the courts, usually in a legal action.' ),
	( 'dfe', 'Defendant-appellee', 'Use for a defendant against whom an appeal is taken from one court or jurisdiction to another to reverse the judgment, usually in a legal action.' ),
	( 'dft', 'Defendant-appellant', 'Use for a defendant who takes an appeal from one court or jurisdiction to another to reverse the judgment, usually in a legal action.' ),
	( 'dgg', 'Degree grantor', 'Use for the organization granting a degree for which the thesis or dissertation described was presented.' ),
	( 'dis', 'Dissertant', 'Use for a person who presents a thesis for a university or higher-level educational degree.' ),
	( 'dln', 'Delineator', 'Use for a person or organization executing technical drawings from others'' designs' ),
	( 'dnc', 'Dancer', 'Use for a person or organization who principally exhibits dancing skills in a musical or dramatic presentation or entertainment.' ),
	( 'dnr', 'Donor', 'Use for a person or organization who is the donor of a book, manuscript, etc., to its present owner. Donors to previous owners are designated as Former owner [fmo] or Inscriber [ins].' ),
	( 'dpc', 'Depicted', 'Use for an entity depicted or portrayed in a work, particularly in a work of art.' ),
	( 'dpt', 'Depositor', 'Use for a person or organization placing material in the physical custody of a library or repository without transferring the legal title.' ),
	( 'drm', 'Draftsman', 'Use for a person or organization who prepares artistic or technical drawings.' ),
	( 'drt', 'Director', 'Use for a person or organization who is responsible for the general management of a work or who supervises the production of a performance for stage, screen, or sound recording.' ),
	( 'dsr', 'Designer', 'Use for a person or organization responsible for the design if more specific codes (e.g., [bkd], [tyd]) are not desired.' ),
	( 'dst', 'Distributor', 'Use for a person or organization that has exclusive or shared marketing rights for an item.' ),
	( 'dtc', 'Data contributor', 'Use for a person or organization that submits data for inclusion in a database or other collection of data.' ),
	( 'dte', 'Dedicatee', 'Use for a person or organization to whom a book, manuscript, etc., is dedicated (not the recipient of a gift).' ),
	( 'dtm', 'Data manager', 'Use for a person or organization responsible for managing databases or other data sources.' ),
	( 'dto', 'Dedicator', 'Use for the author of a dedication, which may be a formal statement or in epistolary or verse form' ),
	( 'dub', 'Dubious author', 'Use for a person or organization to which authorship has been dubiously or incorrectly ascribed.' ),
	( 'edt', 'Editor', 'Use for a person or organization who prepares for publication a work not primarily his/her own, such as by elucidating text, adding introductory or other critical matter, or technically directing an editorial staff.' ),
	( 'egr', 'Engraver', 'Use for a person or organization who cuts letters, figures, etc. on a surface, such as a wooden or metal plate, for printing.' ),
	( 'elg', 'Electrician', 'Use for a person responsible for setting up a lighting rig and focusing the lights for a production, and running the lighting at a performance.' ),
	( 'elt', 'Electrotyper', 'Use for a person or organization who creates a duplicate printing surface by pressure molding and electrodepositing of metal that is then backed up with lead for printing.' ),
	( 'eng', 'Engineer', 'Use for a person or organization that is responsible for technical planning and design, particularly with construction.' ),
	( 'etr', 'Etcher', 'Use for a person or organization who produces text or images for printing by subjecting metal, glass, or some other surface to acid or the corrosive action of some other substance.' ),
	( 'exp', 'Expert', 'Use for a person or organization in charge of the description and appraisal of the value of goods, particularly rare items, works of art, etc.' ),
	( 'fac', 'Facsimilist', 'Use for a person or organization that executed the facsimile.' ),
	( 'fld', 'Field director', 'Use for a person or organization that manages or supervises the work done to collect raw data or do research in an actual setting or environment (typically applies to the natural and social sciences).' ),
	( 'flm', 'Film editor', 'Use for a person or organization who is an editor of a motion picture film. This term is used regardless of the medium upon which the motion picture is produced or manufactured (e.g., acetate film, video tape).' ),
	( 'fmo', 'Former owner', 'Use for a person or organization who owned an item at any time in the past. Includes those to whom the material was once presented. A person or organization giving the item to the present owner is designated as Donor' ),
	( 'fpy', 'First party', 'Use for a person or organization who is identified as the only party or the party of the first part. In the case of transfer of right, this is the assignor, transferor, licensor, grantor, etc. Multiple parties can be named jointly as the first party' ),
	( 'fnd', 'Funder', 'Use for a person or organization that furnished financial support for the production of the work.' ),
	( 'frg', 'Forger', 'Use for a person or organization who makes or imitates something of value or importance, especially with the intent to defraud.' ),
	( 'gis', 'Geographic information specialist', 'Use for a person responsible for geographic information system (GIS) development and integration with global positioning system data.' ),
	( 'hnr', 'Honoree', 'Use for a person or organization in memory or honor of whom a book, manuscript, etc. is donated.' ),
	( 'hst', 'Host', 'Use for a person who is invited or regularly leads a program (often broadcast) that includes other guests, performers, etc. (e.g., talk show host).' ),
	( 'ill', 'Illustrator', 'Use for a person or organization who conceives, and perhaps also implements, a design or illustration, usually to accompany a written text.' ),
	( 'ilu', 'Illuminator', 'Use for a person or organization responsible for the decoration of a work (especially manuscript material) with precious metals or color, usually with elaborate designs and motifs.' ),
	( 'ins', 'Inscriber', 'Use for a person who signs a presentation statement' ),
	( 'inv', 'Inventor', 'Use for a person or organization who first produces a particular useful item, or develops a new process for obtaining a known item or result.' ),
	( 'itr', 'Instrumentalist', 'Use for a person or organization who principally plays an instrument in a musical or dramatic presentation or entertainment.' ),
	( 'ive', 'Interviewee', 'Use for a person or organization who is interviewed at a consultation or meeting, usually by a reporter, pollster, or some other information gathering agent.' ),
	( 'ivr', 'Interviewer', 'Use for a person or organization who acts as a reporter, pollster, or other information gathering agent in a consultation or meeting involving one or more individuals.' ),
	( 'lbr', 'Laboratory', 'Use for an institution that provides scientific analyses of material samples.' ),
	( 'lbt', 'Librettist', 'Use for a person or organization who is a writer of the text of an opera, oratorio, etc.' ),
	( 'ldr', 'Laboratory director', 'Use for a person or organization that manages or supervises work done in a controlled setting or environment.' ),
	( 'led', 'Lead', 'Use to indicate that a person or organization takes primary responsibility for a particular activity or endeavor. Use with another relator term or code to show the greater importance this person or organization has regarding that particular role. If more than one relator is assigned to a heading, use the Lead relator only if it applies to all the relators.' ),
	( 'lee', 'Libelee-appellee', 'Use for a libelee against whom an appeal is taken from one ecclesiastical court or admiralty to another to reverse the judgment.' ),
	( 'lel', 'Libelee', 'Use for a party against whom a libel has been filed in an ecclesiastical court or admiralty.' ),
	( 'len', 'Lender', 'Use for a person or organization permitting the temporary use of a book, manuscript, etc., such as for photocopying or microfilming.' ),
	( 'let', 'Libelee-appellant', 'Use for a libelee who takes an appeal from one ecclesiastical court or admiralty to another to reverse the judgment.' ),
	( 'lgd', 'Lighting designer', 'Use for a person or organization who designs the lighting scheme for a theatrical presentation, entertainment, motion picture, etc. ' ),
	( 'lie', 'Libelant-appellee', 'Use for a libelant against whom an appeal is taken from one ecclesiastical court or admiralty to another to reverse the judgment.' ),
	( 'lil', 'Libelant', 'Use for the party who files a libel in an ecclesiastical or admiralty case.' ),
	( 'lit', 'Libelant-appellant', 'Use for a libelant who takes an appeal from one ecclesiastical court or admiralty to another to reverse the judgment.' ),
	( 'lsa', 'Landscape architect', 'Use for a person or organization whose work involves coordinating the arrangement of existing and proposed land features and structures.' ),
	( 'lse', 'Licensee', 'Use for a person or organization who is an original recipient of the right to print or publish.' ),
	( 'lso', 'Licensor', 'Use for person or organization who is a signer of the license, imprimatur, etc.' ),
	( 'ltg', 'Lithographer', 'Use for a person or organization who prepares the stone or plate for lithographic printing, including a graphic artist creating a design directly on the surface from which printing will be done.' ),
	( 'lyr', 'Lyricist', 'Use for a person or organization who is the a writer of the text of a song.' ),
	( 'mcp', 'Music copyist', 'Use for a person who transcribes or copies musical notation' ),
	( 'mfr', 'Manufacturer', 'Use for a person or organization that makes an artifactual work (an object made or modified by one or more persons). Examples of artifactual works include vases, cannons or pieces of furniture.' ),
	( 'mdc', 'Metadata contact', 'Use for a person or organization primarily responsible for compiling and maintaining the original description of a metadata set (e.g., geospatial metadata set). ' ),
	( 'mod', 'Moderator', 'Use for a person who leads a program (often broadcast) where topics are discussed, usually with participation of experts in fields related to the discussion.Use for a person who leads a program (often broadcast) where topics are discussed, usually with participation of experts in fields related to the discussion.' ),
	( 'mon', 'Monitor', 'Use for a person or organization that supervises compliance with the contract and is responsible for the report and controls its distribution. Sometimes referred to as the grantee, or controlling agency.' ),
	( 'mrk', 'Markup editor', 'Use for a person or organization performing the coding of SGML, HTML, or XML markup of metadata, text, etc.' ),
	( 'msd', 'Musical director', 'Use for a person responsible for basic music decisions about a production, including coordinating the work of the composer, the sound editor, and sound mixers, selecting musicians, and organizing and/or conducting sound for rehearsals and performances.' ),
	( 'mte', 'Metal-engraver', 'Use for a person or organization responsible for decorations, illustrations, letters, etc. cut on a metal surface for printing or decoration.' ),
	( 'mus', 'Musician', 'Use for a person or organization who performs music or contributes to the musical content of a work when it is not possible or desirable to identify the function more precisely.' ),
	( 'nrt', 'Narrator', 'Use for a person who is a speaker relating the particulars of an act, occurrence, or course of events.' ),
	( 'opn', 'Opponent', 'Use for a person or organization responsible for opposing a thesis or dissertation.' ),
	( 'org',  'Originator', 'Use for a person or organization performing the work, i.e., the name of a person or organization associated with the intellectual content of the work. This category does not include the publisher or personal affiliation, or sponsor except where it is also the corporate author.'),
	( 'orm', 'Organizer of meeting', 'Use for a person or organization responsible for organizing a meeting for which an item is the report or proceedings.' ),
	( 'oth', 'Other', 'Use for relator codes from other lists which have no equivalent in the MARC list or for terms which have not been assigned a code.' ),
	( 'own', 'Owner', 'Use for a person or organization that currently owns an item or collection.' ),
	( 'pat', 'Patron', 'Use for a person or organization responsible for commissioning a work. Usually a patron uses his or her means or influence to support the work of artists, writers, etc. This includes those who commission and pay for individual works.' ),
	( 'pbd', 'Publishing director', 'Use for a person or organization who presides over the elaboration of a collective work to ensure its coherence or continuity. This includes editors-in-chief, literary editors, editors of series, etc.' ),
	( 'pbl', 'Publisher', 'Use for a person or organization that makes printed matter, often text, but also printed music, artwork, etc. available to the public.' ),
	( 'pdr', 'Project director', 'Use for a person or organization with primary responsibility for all essential aspects of a project, or that manages a very large project that demands senior level responsibility, or that has overall responsibility for managing projects, or provides overall direction to a project manager.' ),
	( 'pfr', 'Proofreader', 'Use for a person who corrects printed matter. For manuscripts, use Corrector' ),
	( 'pht', 'Photographer', 'Use for a person or organization responsible for taking photographs, whether they are used in their original form or as reproductions.' ),
	( 'plt', 'Platemaker', 'Use for a person or organization responsible for the production of plates, usually for the production of printed images and/or text.' ),
	( 'pma', 'Permitting agency', 'Use for an authority (usually a government agency) that issues permits under which work is accomplished.' ),
	( 'pmn', 'Production manager', 'Use for a person responsible for all technical and business matters in a production.' ),
	( 'pop', 'Printer of plates', 'Use for a person or organization who prints illustrations from plates.' ),
	( 'ppm', 'Papermaker', 'Use for a person or organization responsible for the production of paper, usually from wood, cloth, or other fibrous material.' ),
	( 'ppt', 'Puppeteer', 'Use for a person or organization who manipulates, controls, or directs puppets or marionettes in a musical or dramatic presentation or entertainment.' ),
	( 'prc', 'Process contact', 'Use for a person or organization primarily responsible for performing or initiating a process, such as is done with the collection of metadata sets.' ),
	( 'prd', 'Production personnel', 'Use for a person or organization associated with the production (props, lighting, special effects, etc.) of a musical or dramatic presentation or entertainment.' ),
	( 'prf', 'Performer', 'Use for a person or organization who exhibits musical or acting skills in a musical or dramatic presentation or entertainment, if specific codes for those functions ([act], [dnc], [itr], [voc], etc.) are not used. If specific codes are used, [prf] is used for a person whose principal skill is not known or specified.' ),
	( 'prg', 'Programmer', 'Use for a person or organization responsible for the creation and/or maintenance of computer program design documents, source code, and machine-executable digital files and supporting documentation.' ),
	( 'prm', 'Printmaker', 'Use for a person or organization who makes a relief, intaglio, or planographic printing surface.' ),
	( 'pro', 'Producer', 'Use for a person or organization responsible for the making of a motion picture, including business aspects, management of the productions, and the commercial success of the work.' ),
	( 'prt', 'Printer', 'Use for a person or organization who prints texts, whether from type or plates.' ),
	( 'pta', 'Patent applicant', 'Use for a person or organization that applied for a patent.' ),
	( 'pte', 'Plaintiff-appellee', 'Use for a plaintiff against whom an appeal is taken from one court or jurisdiction to another to reverse the judgment, usually in a legal proceeding.' ),
	( 'ptf', 'Plaintiff', 'Use for the party who complains or sues in court in a personal action, usually in a legal proceeding.' ),
	( 'pth', 'Patent holder', 'Use for a person or organization that was granted the patent referred to by the item.' ),
	( 'ptt', 'Plaintiff-appellant', 'Use for a plaintiff who takes an appeal from one court or jurisdiction to another to reverse the judgment, usually in a legal proceeding.' ),
	( 'rbr', 'Rubricator', 'Use for a person or organization responsible for parts of a work, often headings or opening parts of a manuscript, that appear in a distinctive color, usually red.' ),
	( 'rce', 'Recording engineer', 'Use for a person or organization who supervises the technical aspects of a sound or video recording session.' ),
	( 'rcp', 'Recipient', 'Use for a person or organization to whom correspondence is addressed.' ),
	( 'red', 'Redactor', 'Use for a person or organization who writes or develops the framework for an item without being intellectually responsible for its content.' ),
	( 'ren', 'Renderer', 'Use for a person or organization who prepares drawings of architectural designs (i.e., renderings) in accurate, representational perspective to show what the project will look like when completed.' ),
	( 'res', 'Researcher', 'Use for a person or organization responsible for performing research.' ),
	( 'rev', 'Reviewer', 'Use for a person or organization responsible for the review of a book, motion picture, performance, etc.' ),
	( 'rps', 'Repository', 'Use for an agency that hosts data or material culture objects and provides services to promote long term, consistent and shared use of those data or objects.' ),
	( 'rpt', 'Reporter', 'Use for a person or organization who writes or presents reports of news or current events on air or in print' ),
	( 'rpy', 'Responsible party', 'Use for a person or organization legally responsible for the content of the published material.' ),
	( 'rse', 'Respondent-appellee', 'Use for a respondent against whom an appeal is taken from one court or jurisdiction to another to reverse the judgment, usually in an equity proceeding. ' ),
	( 'rsg', 'Restager', 'Use for a person or organization, other than the original choreographer or director, responsible for restaging a choreographic or dramatic work and who contributes minimal new content.' ),
	( 'rsp', 'Respondent', 'Use for the party who makes an answer to the courts pursuant to an application for redress, usually in an equity proceeding.' ),
	( 'rst', 'Respondent-appellant', 'Use for a respondent who takes an appeal from one court or jurisdiction to another to reverse the judgment, usually in an equity proceeding. Use for a respondent who takes an appeal from one court or jurisdiction to another to reverse the judgment, usually in an equity proceeding.' ),
	( 'rth', 'Research team head', 'Use for a person who directed or managed a research project.' ),
	( 'rtm', 'Research team member', 'Use for a person who participated in a research project but whose role did not involve direction or management of it.' ),
	( 'sad', 'Scientific advisor', 'Use for a person or organization who brings scientific, pedagogical, or historical competence to the conception and realization on a work, particularly in the case of audio-visual items.' ),
	( 'sce', 'Scenarist', 'Use for a person or organization who is the author of a motion picture screenplay.' ),
	( 'scl', 'Sculptor', 'Use for a person or organization who models or carves figures that are three-dimensional representations.' ),
	( 'scr', 'Scribe', 'Use for a person who is an amanuensis and for a writer of manuscripts proper. For a person who makes pen-facsimiles, use Facsimilist [fac].' ),
	( 'sds', 'Sound designer', 'Use for a person who produces and reproduces the sound score (both live and recorded), the installation of microphones, the setting of sound levels, and the coordination of sources of sound for a production.' ),
	( 'sec', 'Secretary', 'Use for a person or organization who is a recorder, redactor, or other person responsible for expressing the views of a organization.' ),
	( 'sgn', 'Signer', 'Use for a person whose signature appears without a presentation or other statement indicative of provenance. When there is a presentation statement, use Inscriber [ins].' ),
	( 'sht', 'Supporting host', 'Use for a person or organization that supports (by allocating facilities, staff, or other resources) a project, program, meeting, event, data objects, material culture objects, or other entities capable of support.' ),
	( 'sng', 'Singer', 'Use for a person or organization who uses his/her/their voice with or without instrumental accompaniment to produce music. A performance may or may not include actual words.' ),
	( 'spk', 'Speaker', 'Use for a person who participates in a program (often broadcast) and makes a formalized contribution or presentation generally prepared in advance.' ),
	( 'spn', 'Sponsor', 'Use for a person or organization that issued a contract or under the auspices of which a work has been written, printed, published, etc.' ),
	( 'spy', 'Second party', 'Use for a person or organization who is identified as the party of the second part. In the case of transfer of right, this is the assignee, transferee, licensee, grantee, etc. Multiple parties can be named jointly as the second party.' ),
	( 'srv', 'Surveyor', 'Use for a person or organization who does measurements of tracts of land, etc. to determine location, forms, and boundaries.' ),
	( 'std', 'Set designer', 'Use for a person or organization who translates the rough sketches of the art director into actual architectural structures for a theatrical presentation, entertainment, motion picture, etc. Set designers draw the detailed guides and specifications for building the set.' ),
	( 'stl', 'Storyteller', 'Use for a person relaying a story with creative and/or theatrical interpretation.' ),
	( 'stm', 'Stage manager', 'Use for a person who is in charge of everything that occurs on a performance stage, and who acts as chief of all crews and assistant to a director during rehearsals.' ),
	( 'stn', 'Standards body', 'Use for an organization responsible for the development or enforcement of a standard.' ),
	( 'str', 'Stereotyper', 'Use for a person or organization who creates a new plate for printing by molding or copying another printing surface.' ),
	( 'tcd', 'Technical director', 'Use for a person who is ultimately in charge of scenery, props, lights and sound for a production.' ),
	( 'tch', 'Teacher', 'Use for a person who, in the context of a resource, gives instruction in an intellectual subject or demonstrates while teaching physical skills.' ),
	( 'ths', 'Thesis advisor', 'Use for a person under whose supervision a degree candidate develops and presents a thesis, mémoire, or text of a dissertation.' ),
	( 'trc', 'Transcriber', 'Use for a person who prepares a handwritten or typewritten copy from original material, including from dictated or orally recorded material. For makers of pen-facsimiles, use Facsimilist [fac].' ),
	( 'trl', 'Translator', 'Use for a person or organization who renders a text from one language into another, or from an older form of a language into the modern form.' ),
	( 'tyd', 'Type designer', 'Use for a person or organization who designed the type face used in a particular item.' ),
	( 'tyg', 'Typographer', 'Use for a person or organization primarily responsible for choice and arrangement of type used in an item. If the typographer is also responsible for other aspects of the graphic design of a book (e.g., Book designer [bkd]), codes for both functions may be needed.' ),
	( 'vdg', 'Videographer', 'Use for a person or organization in charge of a video production, e.g. the video recording of a stage production as opposed to a commercial motion picture. The videographer may be the camera operator or may supervise one or more camera operators. Do not confuse with cinematographer.' ),
	( 'voc', 'Vocalist', 'Use for a person or organization who principally exhibits singing skills in a musical or dramatic presentation or entertainment.' ),
	( 'wam', 'Writer of accompanying material', 'Use for a person or organization who writes significant material which accompanies a sound recording or other audiovisual material' ),
	( 'wdc', 'Woodcutter', 'Use for a person or organization who makes prints by cutting the image in relief on the plank side of a wood block.' ),
	( 'wde', 'Wood-engraver', 'Use for a person or organization who makes prints by cutting the image in relief on the end-grain of a wood block.' ),
	( 'wit', 'Witness', 'Use for a person who verifies the truthfulness of an event or action.' )
GO
