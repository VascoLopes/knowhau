DECLARE @SQLString nvarchar(max)
SET @SQLString = 
   'CREATE TABLE HISTORIC (                                              
      historicID int IDENTITY(1,1),                        
      contentID int NOT NULL,						
	  userMAIL nvarchar(50) NOT NULL,
	  date datetime NOT NULL DEFAULT GetDate(),
    
    CONSTRAINT PK_HISTORICID PRIMARY KEY (historicID),

	CONSTRAINT FK_USERMAILHIST FOREIGN KEY (userMAIL) 
	     REFERENCES USERM(email)
	     ON UPDATE CASCADE 
	     ON DELETE NO ACTION,

	CONSTRAINT FK_CONTENTIDHIST FOREIGN KEY (contentID) 
	     REFERENCES CONTENT(contentID)
	     ON UPDATE CASCADE 
	     ON DELETE NO ACTION
   )'


SET @SQLString = 'USE knowhau' + 
                  ' if not exists (select * from dbo.sysobjects  where id = object_id(N''[dbo].[HISTORIC]''))  begin '+ 
				      @SQLString +' end'

EXEC ( @SQLString)    