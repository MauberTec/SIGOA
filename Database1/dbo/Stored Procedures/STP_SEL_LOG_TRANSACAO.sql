--Select Employees 

CREATE PROCEDURE [dbo].[STP_SEL_LOG_TRANSACAO] 
AS 
  BEGIN 

declare @dbName varchar(100) = (SELECT DB_NAME());
if CHARINDEX('_DESENV', @dbName )> 0
      SELECT  * 
      FROM  [SIGOA_SECURITY_DESENV].[dbo].[tab_transacao]
	  ORDER BY TRA_NOME; 
else
      SELECT  * 
      FROM  [SIGOA_SECURITY].[dbo].[tab_transacao]
	  ORDER BY TRA_NOME; 


  END ;
