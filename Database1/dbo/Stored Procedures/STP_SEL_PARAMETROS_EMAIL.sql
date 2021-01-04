--Select Employees 

 CREATE PROCEDURE [dbo].[STP_SEL_PARAMETROS_EMAIL] 
AS 
  BEGIN 
      --SELECT  par_id, par_valor, par_descricao
      --FROM  dbo.tab_parametros 
      --where par_id like '%email_%'; 
      
DECLARE @cols AS NVARCHAR(MAX),
    @query  AS NVARCHAR(MAX)

select @cols = STUFF((SELECT ',' + QUOTENAME(par_id) 
                    from dbo.tab_parametros
                    where par_id like '%email_%'
                    group by par_id
                    order by par_id
            FOR XML PATH(''), TYPE
            ).value('.', 'NVARCHAR(MAX)') 
        ,1,1,'')

set @query = N'SELECT ' + @cols + N' from 
             (
                select par_valor, par_id
                from dbo.tab_parametros
            ) x
            pivot 
            (
                max(par_valor)
                for par_id in (' + @cols + N')
            ) p '

exec sp_executesql @query;      
      
      
      
      
  END ;
