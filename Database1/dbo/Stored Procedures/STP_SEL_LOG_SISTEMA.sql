--Select Employees 

CREATE PROCEDURE [dbo].[STP_SEL_LOG_SISTEMA](
	@usu_id [int] = null,
	@mod_id [int] = null,
	@data_inicio varchar(30) = null,
	@data_fim  varchar(30) = null,
	@texto_procurado [varchar](1000) = null,
	@tra_id int = null
	)

AS 
  BEGIN 
declare @dbName varchar(100) = (SELECT DB_NAME());
declare @dbNameSecurity varchar(100) = (SELECT DB_NAME());

if CHARINDEX('_DESENV', @dbName )> 0
	set @dbNameSecurity = 'SIGOA_SECURITY_DESENV';
else
	set @dbNameSecurity = 'SIGOA_SECURITY';

  DECLARE @sql NVARCHAR(MAX);
					--	CONVERT(varchar(10),lg.log_data_criacao,3) + ' + char(39)+ ' '  + char(39) + ' + CONVERT(varchar(10),lg.log_data_criacao,8) AS log_data_criacao,

  declare @maxRows varchar(10) = (select par_valor from dbo.tab_parametros where par_id = 'MaxLinhasLog');
  if (@maxRows is null or convert(int,@maxRows) > 4500) 
	set @maxRows = '1000';

  set @sql = ' SELECT top ' + @maxRows + '  lg.log_id,
						lg.tra_id,
						lg.usu_id,
						CONVERT(varchar(20),lg.log_data_criacao,103) + ' + char(39)+ ' '  + char(39) + ' + CONVERT(varchar(10),lg.log_data_criacao,8) AS log_data_criacao,
					    lg.mod_id,
						lg.log_texto,
						lg.log_ip,
						isnull (tr.tra_nome, ' + char(39)  + char(39) +  ') as tra_nome,
						isnull (md.mod_nome_modulo, ' + char(39)  + char(39) + ') as mod_nome_modulo,
						isnull (md.mod_descricao, ' + char(39)  + char(39) + ') as mod_descricao,
						isnull (usu.usu_usuario, ' + char(39)  + char(39) + ') as usu_usuario,
						usu.usu_nome 
				FROM  ' + @dbNameSecurity + '.dbo.tab_log lg
				inner join ' + @dbNameSecurity + '.dbo.tab_transacao tr on tr.tra_id = lg.tra_id
				left join dbo.tab_usuarios usu on usu.usu_id = lg.usu_id
				left join dbo.tab_modulos md on md.mod_id = lg.mod_id
			where 1=1 ' ;
			
		if (@usu_id  >=0  )
		  set @sql = @sql + ' and lg.usu_id = ' + CONVERT(varchar(5), @usu_id) ;
		  
		if (@mod_id >=0 )
		begin
			declare @mod_pai_id int = (select mod_pai_id from [dbo].[tab_modulos] where mod_id = @mod_id);
			if (@mod_pai_id is null or @mod_pai_id <0)
				set @sql = @sql + ' and lg.mod_id in (select  mod_id 
																from dbo.tab_modulos 
																   where mod_pai_id = ' + CONVERT(varchar(5), @mod_id) +
																   ' or mod_id = ' + CONVERT(varchar(5), @mod_id) +
																   ')';
			else
				set @sql = @sql + ' and lg.mod_id = ' + CONVERT(varchar(5), @mod_id) ;
		end

		if (@tra_id  >=0  )
		  set @sql = @sql + ' and lg.tra_id = ' + CONVERT(varchar(5), @tra_id) ;
		  
		if (@data_inicio is not null )
		  set @sql = @sql + ' and lg.log_data_criacao >= convert( datetime, ' + char(39)  + @data_inicio  + ' 00:00:00' + char(39) + ', 103) ';
		  	
		if (@data_fim is not null )
		  set @sql = @sql + ' and lg.log_data_criacao <= convert( datetime, ' + char(39)  + @data_fim  + ' 23:59:59' + char(39) + ', 103) ';
      
  		if (@texto_procurado   is not null )
		  set @sql = @sql + ' and lg.log_texto like ' + CHAR(39) + '%' + @texto_procurado + '%' + CHAR(39);
    
   -- select @sql 
      set @sql = @sql + ' order by  lg.log_data_criacao desc ' 

      EXEC sp_executesql @sql
      
      
  END ;
