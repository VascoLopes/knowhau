DECLARE @SQLString nvarchar(max)
SET @SQLString = 
   'CREATE TABLE LOGWEBAPP (                                              
      eventtype nvarchar(2) NOT NULL,                        -- email admin
      date datetime NOT NULL DEFAULT GetDate(),						 -- Morada cliente
	  username nvarchar(50) NOT NULL,	
	  logwaID int IDENTITY(1,1),	
    
    CONSTRAINT PK_LOGWEB PRIMARY KEY (logwaID) -- Chave primária
   )'


SET @SQLString = 'USE knowhau' + 
                  ' if not exists (select * from dbo.sysobjects  where id = object_id(N''[dbo].[LOGWEBAPP]''))  begin '+ 
				      @SQLString +' end'

EXEC ( @SQLString)    