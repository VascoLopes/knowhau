DECLARE @SQLString nvarchar(max)
SET @SQLString = 
   'CREATE TABLE USERM (                                              
      name nvarchar(50) NOT NULL,                      
      genre nvarchar(1) NOT NULL  DEFAULT ''O'',			
	  username nvarchar(50) NOT NULL UNIQUE,	
	  birthdate datetime NOT NULL,	
	  email nvarchar(50) NOT NULL,
	  password nvarchar(50) NOT NULL,

    
    CONSTRAINT PK_USERMAIL PRIMARY KEY (email)
   )'


SET @SQLString = 'USE knowhau' + 
                  ' if not exists (select * from dbo.sysobjects  where id = object_id(N''[dbo].[USERM]''))  begin '+ 
				      @SQLString +' end'

EXEC ( @SQLString)    


