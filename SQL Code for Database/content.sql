DECLARE @SQLString nvarchar(max)
SET @SQLString = 
   'CREATE TABLE CONTENT (                                              
      contentID int IDENTITY(1,1),                        
      contentmsg nvarchar(1000) NOT NULL,						
	  beaconID nvarchar(100) NOT NULL,		
    
    CONSTRAINT PK_CONTENTID PRIMARY KEY (contentID),

	CONSTRAINT FK_BEACONIDINCONTENT FOREIGN KEY (beaconID) 
	     REFERENCES BEACON(beaconID)
	     ON UPDATE CASCADE 
	     ON DELETE NO ACTION
   )'


SET @SQLString = 'USE knowhau' + 
                  ' if not exists (select * from dbo.sysobjects  where id = object_id(N''[dbo].[CONTENT]''))  begin '+ 
				      @SQLString +' end'

EXEC ( @SQLString)    