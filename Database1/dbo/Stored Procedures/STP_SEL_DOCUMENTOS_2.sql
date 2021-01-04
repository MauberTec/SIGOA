CREATE PROCEDURE [dbo].[STP_SEL_DOCUMENTOS_2] 
@doc_id int=null,
@doc_codigo varchar(50) = '',
@doc_descricao varchar(255) = '',
@tpd_id nvarchar(3)= '',
@dcl_codigo nvarchar(3) = '',
@registro_ini int = 0,
@ordenado_por varchar(50)= 'doc_codigo asc',
@qt_por_pagina int = 10

AS 
  BEGIN

--declare @doc_id int=null
--declare @doc_codigo varchar(50) = ''
--declare @doc_descricao varchar(255) = ''
--declare @tpd_id nvarchar(3)= ''
--declare @dcl_codigo nvarchar(3) = ''
--declare @registro_ini int = 10000
--declare @ordenado_por varchar(50)= 'doc_codigo asc'
--declare @qt_por_pagina int = 10


declare @horaIni datetime = getdate()

  if (@registro_ini = 0)
   set @registro_ini = 1;

   if (@ordenado_por = '')
	set @ordenado_por = 'doc_codigo asc'

declare @total_registros int = 0;
declare @total_paginas int = 0;

declare @doc_id_procurado int = @doc_id;
declare @posicao_doc_id_procurado int = 0;
declare @pagina_procurada int = 0;

SET NOCOUNT ON

---- /***************** SELECAO ************************
	IF OBJECT_ID('tempdb..#temp2') IS NOT NULL
		DROP TABLE #temp2;


		--************************ acha o numero total de registros ***********************************
declare @campos_Count varchar(50) = 'select @Result=count(*) ';

declare @sql_from NVarchar(max) = ' from [dbo].[tab_documentos] doc ';

	if @tpd_id <> ''
	  set @sql_from = @sql_from + ' inner join [dbo].[tab_documento_tipos] tip on tip.tpd_id = doc.tpd_id ';

	if @dcl_codigo <> ''
	     set @sql_from = @sql_from + ' left join dbo.tab_documento_classes_projeto dcl on dcl.dcl_codigo = substring(doc_codigo, CHARINDEX(''/'',doc_codigo)-3, 3) ';
		 
	set @sql_from = @sql_from + ' where [doc_deletado] is null  ';

declare @where varchar(max)= '';
		if @doc_codigo <> ''
			set @where = @where  + ' and doc_codigo like ' + char(39) + '%' +  @doc_codigo + '%' + char(39) ;

		if @doc_descricao <> ''
			set @where = @where  + ' and doc_descricao like ' + char(39) + '%' + @doc_descricao  + '%' + char(39) ;

		if @tpd_id <> ''
			set @where = @where  + ' and doc.tpd_id = ' + char(39) + @tpd_id + char(39) ;

		if @dcl_codigo <> ''
		    set @where = @where  + ' and isnull(dcl.dcl_codigo,'''') = ' + char(39) +  @dcl_codigo + char(39);


declare @sql1 NVarchar(max) = @campos_Count + @sql_from + @where;
declare @result int

exec sp_executesql @sql1, N'@Result int Output', @Result output

set @total_registros = @Result;


-- ************* seleciona a pagina correspondente ********************************************


	IF OBJECT_ID('tempDB..##temp_ids','U') IS NOT NULL
		DROP TABLE ##temp_ids;

declare @sql_tudo varchar(max) = 'select * into  ##temp_ids from ( ' ;
declare   @campos varchar(max) = 'select doc_id, ' ; 

	if (@doc_codigo <> '') or (CHARINDEX('doc_codigo', @ordenado_por) > 0 ) 
		set @campos = @campos + ' doc_codigo, ';

	if (@doc_descricao <> '') or (CHARINDEX('doc_descricao', @ordenado_por) > 0 ) 
		set @campos = @campos + ' doc_descricao, ';

	if  (CHARINDEX('doc_caminho', @ordenado_por) > 0 ) 
		set @campos = @campos + ' doc_caminho, ';

	if (@tpd_id <> '') or (CHARINDEX('tpd_descricao', @ordenado_por) > 0 ) 
		set @campos = @campos +  ' doc.tpd_id, tpd_descricao, ' ; --' tpd_subtipo, tpd_descricao, ' ;

	if (@dcl_codigo <> '') or (CHARINDEX('dcl_codigo', @ordenado_por) > 0 ) 
		set @campos = @campos + ' dcl_codigo, ';

    set	@campos	= @campos + ' ROW_NUMBER() OVER (order by ' + @ordenado_por + ') AS numero_linha ';

	set @sql_from  = ' from [dbo].[tab_documentos] doc ';

	if (@tpd_id <> '') or (CHARINDEX('tpd_descricao', @ordenado_por) > 0 ) 
	  set @sql_from = @sql_from + ' inner join [dbo].[tab_documento_tipos] tip on tip.tpd_id = doc.tpd_id ';

	if (@dcl_codigo <> '') or (CHARINDEX('dcl_codigo', @ordenado_por) > 0 ) 
	     set @sql_from = @sql_from + ' left join dbo.tab_documento_classes_projeto dcl on dcl.dcl_codigo = substring(doc_codigo, CHARINDEX(''/'',doc_codigo)-3, 3) ';
		 
	set @sql_from = @sql_from + ' where [doc_deletado] is null  ';

	set @sql1 = @sql_tudo +  @campos + @sql_from + @where + ' ) as tb1 where 1=1 ';


		if (@doc_id_procurado > 0) and (@registro_ini < 0)
			set @where = ' and doc_id >= ' + convert(varchar(20), (@doc_id_procurado - @qt_por_pagina)) + ' and doc_id <= ' + convert(varchar(20),(@doc_id_procurado + @qt_por_pagina));
		else
			set @where = ' and numero_linha >= ' + convert(varchar(20), @registro_ini) + ' and numero_linha <= ' + convert(varchar(20),(@qt_por_pagina + @registro_ini-1));


	set @sql1 = @sql1  + @where;
	set @sql1 = @sql1  + ' order by ' + @ordenado_por + '';
	
	--set @sql1 = @sql1  + ' OPTION (RECOMPILE) ' ;

	execute (@sql1)


	select ids.doc_id, ids.numero_linha, 
			doc.doc_codigo, 
			doc.doc_descricao, doc.dcl_id, doc.tpd_id, doc.doc_caminho, doc_ativo,
			tip.tpd_subtipo, tip.tpd_descricao,
			dcl.dcl_codigo,
			dcl_descricao
	into #temp2
	from ##temp_ids ids
	inner join [dbo].[tab_documentos] doc on doc.doc_id = ids.doc_id
	inner join [dbo].[tab_documento_tipos] tip on tip.tpd_id = doc.tpd_id 
	left join dbo.tab_documento_classes_projeto dcl on dcl.dcl_codigo = substring(doc.doc_codigo, CHARINDEX('/',doc.doc_codigo)-3, 3)  


 -- ****************** ANALISE DOS DADOS ***********************************

   -- acha o numero de registros
   set @posicao_doc_id_procurado = (SELECT numero_linha  FROM #temp2 where doc_id = @doc_id_procurado);

   -- procura a pagina que contem o doc_id_selecionado
   if (@doc_id_procurado > 0) and (@registro_ini < 0) 
 		set @registro_ini = ( @posicao_doc_id_procurado / @qt_por_pagina)*@qt_por_pagina ;

	   -- retorna a pagina solicitada
  select @total_registros as total_registros, 
		  0 as recordsFiltered, 
		  @posicao_doc_id_procurado as posicao_doc_id_procurado, 
		  @registro_ini as registro_ini,
		  * 
   from #temp2
   where numero_linha between (@registro_ini) and (@registro_ini + @qt_por_pagina -1);
		

SET NOCOUNT Off

end
