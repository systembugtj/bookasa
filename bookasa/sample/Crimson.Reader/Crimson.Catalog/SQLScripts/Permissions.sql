-- Set permissions for standard users
grant select on CrimsonReader.Binding to CrimsonReader_Reader
grant select on CrimsonReader.Book to CrimsonReader_Reader
grant select on CrimsonReader.BookCreator to CrimsonReader_Reader
grant select on CrimsonReader.BookList to CrimsonReader_Reader
grant select on CrimsonReader.BookListMember to CrimsonReader_Reader
grant select on CrimsonReader.BookReview to CrimsonReader_Reader
grant select on CrimsonReader.BookSeries to CrimsonReader_Reader
grant select on CrimsonReader.BookSubject to CrimsonReader_Reader
grant select on CrimsonReader.Catalog to CrimsonReader_Reader
grant select on CrimsonReader.Creator to CrimsonReader_Reader
grant select on CrimsonReader.Edition to CrimsonReader_Reader
grant select on CrimsonReader.EditionBinding to CrimsonReader_Reader
grant select on CrimsonReader.LibrarySubject to CrimsonReader_Reader
grant select on CrimsonReader.Review to CrimsonReader_Reader
grant select on CrimsonReader.Series to CrimsonReader_Reader

-- Set Permissions for the Editor
grant select, insert, update, delete on CrimsonReader.Binding to CrimsonReader_Editor;
grant select, insert, update, delete on CrimsonReader.Book to CrimsonReader_Editor;
grant select, insert, update, delete on CrimsonReader.BookCreator to CrimsonReader_Editor;
grant select, insert, update, delete on CrimsonReader.BookList to CrimsonReader_Editor;
grant select, insert, update, delete on CrimsonReader.BookListMember to CrimsonReader_Editor;
grant select, insert, update, delete on CrimsonReader.BookReview to CrimsonReader_Editor;
grant select, insert, update, delete on CrimsonReader.BookSeries to CrimsonReader_Editor;
grant select, insert, update, delete on CrimsonReader.BookSubject to CrimsonReader_Editor;
grant select, insert, update, delete on CrimsonReader.Catalog to CrimsonReader_Editor;
grant select, insert, update, delete on CrimsonReader.Creator to CrimsonReader_Editor;
grant select, insert, update, delete on CrimsonReader.Edition to CrimsonReader_Editor;
grant select, insert, update, delete on CrimsonReader.EditionBinding to CrimsonReader_Editor;
grant select, insert, update, delete on CrimsonReader.LibrarySubject to CrimsonReader_Editor;
grant select, insert, update, delete on CrimsonReader.Review to CrimsonReader_Editor;
grant select, insert, update, delete on CrimsonReader.Series to CrimsonReader_Editor;

-- Set permissions for the administrator
grant select, insert, update, delete on CrimsonReader.APIKey to CrimsonReader_Administrator
grant select, insert, update, delete on CrimsonReader.Binding to CrimsonReader_Administrator
grant select, insert, update, delete on CrimsonReader.Book to CrimsonReader_Administrator
grant select, insert, update, delete on CrimsonReader.BookCreator to CrimsonReader_Administrator
grant select, insert, update, delete on CrimsonReader.BookList to CrimsonReader_Administrator
grant select, insert, update, delete on CrimsonReader.BookListMember to CrimsonReader_Administrator
grant select, insert, update, delete on CrimsonReader.BookReview to CrimsonReader_Administrator
grant select, insert, update, delete on CrimsonReader.BookSeries to CrimsonReader_Administrator
grant select, insert, update, delete on CrimsonReader.BookSubject to CrimsonReader_Administrator
grant select, insert, update, delete on CrimsonReader.Catalog to CrimsonReader_Administrator
grant select, insert, update, delete on CrimsonReader.Creator to CrimsonReader_Administrator
grant select, insert, update, delete on CrimsonReader.Edition to CrimsonReader_Administrator
grant select, insert, update, delete on CrimsonReader.EditionBinding to CrimsonReader_Administrator
grant select, insert, update, delete on CrimsonReader.LibrarySubject to CrimsonReader_Administrator
grant select, insert, update, delete on CrimsonReader.Review to CrimsonReader_Administrator
grant select, insert, update, delete on CrimsonReader.Series to CrimsonReader_Administrator
