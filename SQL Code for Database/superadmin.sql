DECLARE @SQLString nvarchar(max)
SET @SQLString = 
   'CREATE TABLE SUPERADMIN (                                              
      email nvarchar(50) NOT NULL,                        -- email admin
      name nvarchar(50) NOT NULL,						 -- Morada cliente
	  username nvarchar(50) NOT NULL,	
	  password nvarchar(50) NOT NULL,	
    
    CONSTRAINT PK_SUPERID PRIMARY KEY (email) -- Chave primária
   )'


SET @SQLString = 'USE knowhau' + 
                  ' if not exists (select * from dbo.sysobjects  where id = object_id(N''[dbo].[SUPERADMIN]''))  begin '+ 
				      @SQLString +' end'

EXEC ( @SQLString)    
