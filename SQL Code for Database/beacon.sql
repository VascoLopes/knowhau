DECLARE @SQLString nvarchar(max)
SET @SQLString = 
   'CREATE TABLE BEACON (                                              
      beaconID nvarchar(100),                      			
	  majorvalue int,	
	  minorvalue int,	
	  name nvarchar(50) NOT NULL,
	  model nvarchar(50),

    
    CONSTRAINT PK_BEACONID PRIMARY KEY (BeaconID)
   )'


SET @SQLString = 'USE knowhau' + 
                  ' if not exists (select * from dbo.sysobjects  where id = object_id(N''[dbo].[BEACON]''))  begin '+ 
				      @SQLString +' end'

EXEC ( @SQLString)    

