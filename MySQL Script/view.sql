CREATE OR REPLACE VIEW BookAuthorReport AS
     SELECT		
		author.Name as AuthorName,
        BOOK.Title as BookTitle,
        BOOK.Company as BookCompany,
        BOOK.Edition as BookEdition,
        BOOK.PublishYear as BookPublishYear,
        BOOK.Value as BookValue,
        subject.Description as BookSubject        
    FROM book  
	JOIN booksubject 
	    ON book.BookId = booksubject.BookId
    JOIN subject
        ON booksubject.SubjectId = subject.SubjectId
    JOIN bookauthor
        ON book.BookId = bookauthor.BookId
    JOIN author
        ON bookauthor.AuthorId = author.AuthorId
    AND author.AuthorId is not NULL
        GROUP BY author.Name;