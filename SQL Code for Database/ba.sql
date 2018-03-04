DECLARE @SQLString nvarchar(max)
SET @SQLString = 
   'CREATE TABLE BA (                                              
      baID int IDENTITY(1,1),                        
      adminemail nvarchar(50) NOT NULL,						
	  beaconID nvarchar(100) NOT NULL,		
    
    
    CONSTRAINT PK_BAID PRIMARY KEY (baID),

	CONSTRAINT FK_BEACONID FOREIGN KEY (beaconID) 
	     REFERENCES BEACON(beaconID)
	     ON UPDATE CASCADE 
	     ON DELETE NO ACTION,

	CONSTRAINT FK_ADMINMAIL FOREIGN KEY (adminemail) 
	     REFERENCES ADMIN(email)
	     ON UPDATE CASCADE 
	     ON DELETE NO ACTION
   )'


SET @SQLString = 'USE knowhau' + 
                  ' if not exists (select * from dbo.sysobjects  where id = object_id(N''[dbo].[BA]''))  begin '+ 
				      @SQLString +' end'

EXEC ( @SQLString)    