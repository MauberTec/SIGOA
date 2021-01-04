CREATE procedure [dbo].[STP_INS_DOCUMENTO] 
@doc_codigo nvarchar(50) , 
@doc_descricao nvarchar(255), 
@tpd_id nvarchar(3), 
@dcl_id int = null, 
@doc_caminho varchar(max), 
@doc_ativo bit,
@usu_id int,
@ip nvarchar(30),
@doc_codigo_filtro varchar(50) = '',
@doc_descricao_filtro varchar(255) = '',
@tpd_id_filtro nvarchar(3)= '',
@dcl_codigo_filtro nvarchar(3) = '',
@ordenado_por varchar(50)= 'doc_codigo asc',
@qt_por_pagina int = 10



--with encryption
as

begin try
		BEGIN TRAN T1
				declare @actionDate datetime
				set @actionDate = getdate()

				declare @doc_id bigint;
				set @doc_id = (select isnull(max(doc_id),0) +1 from dbo.tab_documentos);
				
				-- insere novo grupo
				set @doc_ativo = 1; -- ativo
				INSERT INTO dbo.tab_documentos(doc_id, doc_codigo, doc_descricao, tpd_id, dcl_id, doc_caminho, doc_ativo, doc_data_criacao, doc_criado_por )
				    values (@doc_id, @doc_codigo, @doc_descricao, @tpd_id, @dcl_id, @doc_caminho, @doc_ativo, @actionDate, @usu_id);
			

			-- ********* INSERÇÃO DE LOG **************************************

				declare @tabela varchar(300) = 'tab_documentos';
				declare @tra_id int = 4; -- 4= insercao
				declare @mod_id_log int = 710; -- 710 = Cadastro de Documentos

				-- checa se a tmp table existem e a exclui
				set nocount on;
				if OBJECT_ID('tempdb..#tmpTabela') is not null
					DROP TABLE #tmpTabela;

				-- insere dados NEW na tabela #tmpTabela
				SELECT * into #tmpTabela 
				from dbo.tab_documentos
				where  doc_id= @doc_id ;


				-- concatena os valores e retorna em varchar
				declare @log_texto varchar(MAX); 
				exec dbo.STP_CONCATENA_COLS_VALS  @tabela, @retorno = @log_texto output;

				-- exclui a temporaria
				DROP TABLE #tmpTabela;

				set nocount off;
				exec dbo.STP_INS_LOGSISTEMA @tra_id, @usu_id, @mod_id_log,	@log_texto,	@ip			

			 -- ****************************************************************
		COMMIT TRAN T1

	--return @doc_id

set @doc_codigo = ''
set @doc_descricao  = ''
set @tpd_id = ''
declare @dcl_codigo nvarchar(3) = ''
declare @registro_ini int = 0

  if (@registro_ini = 0)
   set @registro_ini = 1;

   if (@ordenado_por = '')
	set @ordenado_por = 'doc_codigo asc'


SET NOCOUNT ON

-- ************* localiza a pagina correspondente para posicionar o novo documento no grid ********************************************

declare @sql1 NVarchar(max) ='';
declare @sql_from NVarchar(max) = ' from [dbo].[tab_documentos] doc ';
declare @where varchar(max)= '';


	IF OBJECT_ID('tempDB..##temp_ids','U') IS NOT NULL
		DROP TABLE ##temp_ids;

	declare @sql_tudo varchar(max) = 'select * into  ##temp_ids from ( ' ;
	declare   @campos varchar(max) = 'select doc_id, ' ; 

	if (@doc_codigo_filtro <> '') 
		or (CHARINDEX('doc_codigo', @ordenado_por) > 0 ) 
		set @campos = @campos + ' doc_codigo, ';

    set	@campos	= @campos + ' ROW_NUMBER() OVER (order by ' + @ordenado_por + ') AS numero_linha ';

	set @sql_from  = ' from [dbo].[tab_documentos] doc ';

	if (@tpd_id_filtro <> '') or (CHARINDEX('tpd_descricao', @ordenado_por) > 0 ) 
	  set @sql_from = @sql_from + ' inner join [dbo].[tab_documento_tipos] tip on tip.tpd_id = doc.tpd_id ';

	if (@dcl_codigo_filtro <> '') or (CHARINDEX('dcl_codigo', @ordenado_por) > 0 ) 
	     set @sql_from = @sql_from + ' left join dbo.tab_documento_classes_projeto dcl on dcl.dcl_codigo = substring(doc_codigo, CHARINDEX(''/'',doc_codigo)-3, 3) ';
		 
	set @sql_from = @sql_from + ' where [doc_deletado] is null  ';

			
	if (@doc_codigo_filtro <> '')
		and (CHARINDEX(@doc_codigo_filtro, @doc_codigo) > 0 ) 
			set @where = @where  + ' and doc_codigo like ' + char(39) + '%' +  @doc_codigo_filtro + '%' + char(39) ;

	if (@doc_descricao_filtro <> '')
		and (CHARINDEX(@doc_descricao_filtro, @doc_descricao) > 0 ) 
			set @where = @where  + ' and doc_descricao like ' + char(39) + '%' + @doc_descricao_filtro  + '%' + char(39) ;

	if (@tpd_id_filtro <> '')
		and (CHARINDEX(@tpd_id_filtro, @tpd_id) > 0 ) 
			set @where = @where  + ' and doc.tpd_id = ' + char(39) + @tpd_id_filtro + char(39) ;

	if (@dcl_codigo_filtro <> '')
		and (CHARINDEX(@dcl_codigo_filtro, @dcl_codigo) > 0 ) 
		    set @where = @where  + ' and isnull(dcl.dcl_codigo,'''') = ' + char(39) +  @dcl_codigo_filtro + char(39);

	set @sql1 = @sql_tudo +  @campos + @sql_from + @where + ' ) as tb1 where 1=1 ';


	--	if (@doc_id_procurado > 0)
			set @where = ' and doc_id >= ' + convert(varchar(20), (@doc_id - @qt_por_pagina)) + ' and doc_id <= ' + convert(varchar(20),(@doc_id + @qt_por_pagina));
		--else
		--	set @where = ' and numero_linha >= ' + convert(varchar(20), @registro_ini) + ' and numero_linha <= ' + convert(varchar(20),(@qt_por_pagina + @registro_ini-1));


	set @sql1 = @sql1  + @where;
	set @sql1 = @sql1  + ' order by ' + @ordenado_por + '';
	
	execute (@sql1)

   -- procura a pagina que contem o doc_id_selecionado
   declare @posicao_doc_id int = (SELECT numero_linha  FROM ##temp_ids where doc_id = @doc_id);

   declare @pagina_procurada int =0;

   if (@doc_id > 0)
		set @pagina_procurada = ( @posicao_doc_id / @qt_por_pagina);


		select convert(varchar(100), @doc_id) + ':' + convert(varchar(100), @pagina_procurada) as saida
 	--	set @registro_ini = ( @posicao_doc_id_procurado / @qt_por_pagina)*@qt_por_pagina ;

	-- retorna a pagina solicitada
  -- select  @registro_ini as registro_ini

  SET NOCOUNT Off
		





end try
begin catch
		ROLLBACK TRAN T1
            PRINT 'The following error has occurred:  ' + ERROR_MESSAGE()

		--Raise an error
		 DECLARE @ErrorMessage varchar(255), @ErrorSeverity int, @ErrorState int 
		 SELECT @ErrorMessage = ERROR_MESSAGE(), @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE() 
		 RAISERROR(@ErrorMessage,@ErrorSeverity,@ErrorState) 
end catch
