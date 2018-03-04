DECLARE @SQLString nvarchar(max)
SET @SQLString = 
   'CREATE TABLE ADMIN (                                              
      email nvarchar(50) NOT NULL,                        
      name nvarchar(50) NOT NULL,						
	  username nvarchar(50) NOT NULL,	
	  password nvarchar(50) NOT NULL,	
    
    
    CONSTRAINT PK_ADMINMAIL PRIMARY KEY (email)
   )'


SET @SQLString = 'USE knowhau' + 
                  ' if not exists (select * from dbo.sysobjects  where id = object_id(N''[dbo].[ADMIN]''))  begin '+ 
				      @SQLString +' end'

EXEC ( @SQLString)    

