DECLARE @SQLString nvarchar(max)
SET @SQLString = 
   'CREATE TABLE LOGMOBILEAPP (                                              
      eventtype nvarchar(2) NOT NULL,                        -- email admin
      date datetime NOT NULL DEFAULT GetDate(),						 -- Morada cliente
	  username nvarchar(50) NOT NULL,	
	  logmaID int IDENTITY(1,1),	
    
    CONSTRAINT PK_LOGMOBILEID PRIMARY KEY (logmaID) -- Chave primária
   )'


SET @SQLString = 'USE knowhau' + 
                  ' if not exists (select * from dbo.sysobjects  where id = object_id(N''[dbo].[LOGMOBILEAPP]''))  begin '+ 
				      @SQLString +' end'

EXEC ( @SQLString)    