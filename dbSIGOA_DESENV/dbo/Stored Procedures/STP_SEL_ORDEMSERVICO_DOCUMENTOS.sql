CREATE procedure [dbo].[STP_SEL_ORDEMSERVICO_DOCUMENTOS]
@ord_id int,
@obj_id int = null,
@doc_codigo varchar(200)=null,
@somente_referencia int = null

as

 	IF OBJECT_ID('tempdb..#temp2') IS NOT NULL
		DROP TABLE #temp2;

		

create table #temp2(idOrigem int null,
					doc_id bigint not null,
					doc_codigo nvarchar(50) null,
					doc_descricao nvarchar(255) null,
					doc_caminho varchar(max) null,
					doc_ativo bit null,
					tpd_id  nvarchar(3) not null,
					tpd_descricao varchar(255) null,
					doc_Associado int not null,
					dos_referencia int not null);

	if @ord_id = -1
	begin
			set @ord_id = (select top 1 ord_id
							  FROM [dbo].[tab_documento_ordens_servicos] dos
								inner join [dbo].[tab_documento_objeto] dob on dob.doc_id=dos.doc_id and dob.dob_deletado is null
							  where obj_id = @obj_id
								  and dos.dos_deletado is null 
								  and dos.dos_referencia = 1);
	end
 
   	if (@doc_codigo is null) or (@doc_codigo = '')-- retorna os documentos associados
	begin
		if @ord_id < 0
			insert into #temp2 (idOrigem, doc_id, doc_codigo, doc_descricao, doc_caminho, doc_ativo, tpd_id, tpd_descricao, doc_Associado, dos_referencia ) 
			select
			1 as idOrigem, 
			-1 as doc_id, 
			'' as doc_codigo, 
			'' as doc_descricao, 
			'' as doc_caminho, 
			 1 as doc_ativo, 
			 1 as tpd_id, 
			 '' as tpd_descricao, 
			 0 as doc_Associado, 
			 0 as dos_referencia

			--select -1 as ord_id	
			--,'' as ord_codigo
			--, '' as ord_descricao	
			--,-1 as doc_id	
			--, 0 as ord_Associado
			--, 0 as dos_referencia
		else
			begin
					insert into #temp2 (idOrigem, doc_id, doc_codigo, doc_descricao, doc_caminho, doc_ativo, tpd_id, tpd_descricao, doc_Associado, dos_referencia ) 
					select 2 as idOrigem,
						doc.doc_id, doc_codigo, doc_descricao, doc_caminho, doc_ativo, 
						tip.tpd_id, tpd_descricao,
						0 as Associado,
						isnull(dos.dos_referencia,0) as dos_referencia
					from [dbo].[tab_documento_ordens_servicos] dos
						inner join [dbo].[tab_documentos] doc on doc.doc_id = dos.doc_id and doc.doc_deletado is null and doc.doc_ativo =1
						inner join [dbo].[tab_documento_tipos] tip on tip.tpd_id = doc.tpd_id and tip.tpd_deletado is null and tip.tpd_ativo=1
					where dos_deletado is null
						and [ord_id] = @ord_id
						and isnull(dos_referencia,0) = case when @somente_referencia = 1 then 1 else isnull(dos_referencia,0)  end
					order by  idOrigem;
			 end
	end
	else -- retorna as O.S.s NAO associadas
	begin
		  insert into #temp2 (doc_id, idOrigem, doc_codigo, doc_descricao, doc_caminho, doc_ativo, tpd_id, tpd_descricao, doc_Associado, dos_referencia ) 
          select --doc.*,  1 as doc_Associado, tpd_descricao, dob.ord_id
				distinct 
				    doc.doc_id, 
					2 as idOrigem,
					doc.doc_codigo,
					doc.doc_descricao,
					doc.doc_caminho,
					doc.doc_ativo, 
					tip.tpd_id, 
					tpd_descricao,  
					1 as doc_Associado, 
					0 as dos_referencia
            from dbo.tab_documentos doc
				inner join [dbo].[tab_documento_tipos] tip on tip.tpd_id = doc.tpd_id
		  where doc.doc_deletado is  null 
				and doc.doc_ativo=1
				and doc.doc_codigo like  @doc_codigo + '%'	
				and doc.doc_id not in (select distinct doc_id
										from dbo.tab_documento_ordens_servicos
										where 
										dos_deletado is null 
										and ord_id = @ord_id);
	end;

	declare @total_registros int = (select count(*) from #temp2);
	select top 100 @total_registros as total_registros, * 
	from  #temp2 order by doc_codigo;


return
