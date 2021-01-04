CREATE procedure [dbo].[STP_SEL_OBJETO_DOCUMENTOS]
@obj_id int,
@doc_codigo varchar(200)=null

as
 
 	IF OBJECT_ID('tempdb..#temp2') IS NOT NULL
		DROP TABLE #temp2;


create table #temp2 (idOrigem int null,					
				    doc_id bigint not null,
					doc_codigo nvarchar(50) null,
					doc_descricao nvarchar(255) null,
					doc_caminho varchar(max) null,
					doc_ativo bit null,
					tpd_id  nvarchar(3) not null,
					tpd_descricao varchar(255) null,
					doc_Associado int not null,
					dos_referencia int not null);


   	if (@doc_codigo is null) -- retorna os Objetos associados
	begin
		if @obj_id < 0
		insert into #temp2 (idOrigem, doc_id, doc_codigo, doc_descricao, doc_caminho, doc_ativo, tpd_id, tpd_descricao, doc_Associado, dos_referencia ) 
			select 0 as idOrigem,
					-1 as doc_id,
					'' as doc_codigo, 
					'' as doc_descricao, 
					'' as doc_caminho, 
					1 as doc_ativo,
					0 as tpd_id, 
					'' as tpd_descricao,
					0 as doc_Associado,
					0 as dos_referencia
			--select 0 as idOrigem,
			---1 as obj_id	
			--,'' as obj_codigo
			--, '' as obj_descricao	
			--,-1 as doc_id	
			--, 0 as obj_Associado,
			--'' as doc_codigo, 
			--'' as doc_descricao, 
			--'' as doc_caminho, 
			--1 as doc_ativo,
			--0 as tpd_id, 
			--'' as tpd_descricao,
			--0 as dos_referencia		
		else
		begin
			-- procura @obj_id_TipoObraDeArte subindo a hierarquia
				declare @obj_pai int =0
				declare @clo_id_aux int = -1;
				declare @obj_id_TipoObraDeArte int = -1;
				declare @obj_id_orig int = @obj_id;

				declare @fim int = 0;
				while (@obj_pai >=0  and @fim < 15)
				begin
						select  @obj_pai = isnull(obj_pai, -1),
								@clo_id_aux = clo_id
						from dbo.tab_objetos
						where obj_id = @obj_id;

						if (@clo_id_aux = 3) -- 3= TIPO DE OBRA DE ARTE
						begin
							set @obj_id_TipoObraDeArte = @obj_id;
							set @fim = 20;
						end

					set @obj_id = @obj_pai;
					set @fim = @fim + 1;
				 end

			-- seleciona os documentos associados ao @obj_id_TipoObraDeArte + documentos associados ao proprio objeto
			insert into #temp2 (idOrigem, doc_id, doc_codigo, doc_descricao, doc_caminho, doc_ativo, tpd_id, tpd_descricao,doc_Associado,  dos_referencia ) 
		       select distinct idOrigem, doc_id, doc_codigo, doc_descricao, doc_caminho, doc_ativo, tpd_id, tpd_descricao, 1 as doc_Associado,  max( convert(int, dos_referencia)) as dos_referencia
				from (
					select 1 as idOrigem,
						doc.doc_id, '*'+ doc_codigo as doc_codigo, doc_descricao, doc_caminho, doc_ativo, 
						tip.tpd_id, tpd_descricao,
						isnull(dos.dos_referencia,0) as dos_referencia
					from [dbo].[tab_documento_objeto] dob
						left join dbo.tab_documento_ordens_servicos dos on dos.doc_id = dob.doc_id and [dos_deletado] is null
						inner join [dbo].[tab_documentos] doc on doc.doc_id = dob.doc_id and doc.doc_deletado is null and doc.doc_ativo =1
						inner join [dbo].[tab_documento_tipos] tip on tip.tpd_id = doc.tpd_id and tip.tpd_deletado is null and tip.tpd_ativo=1
					where dob_deletado is null
					        and [obj_id] = @obj_id_TipoObraDeArte
					union		 
					select 2 as idOrigem,
						doc.doc_id, doc_codigo as doc_codigo, doc_descricao, doc_caminho, doc_ativo, 
						tip.tpd_id, tpd_descricao,
						isnull(dos.dos_referencia,0) as dos_referencia
					from [dbo].[tab_documento_objeto] dob
						left join dbo.tab_documento_ordens_servicos dos on dos.doc_id = dob.doc_id and [dos_deletado] is null 
						inner join [dbo].[tab_documentos] doc on doc.doc_id = dob.doc_id and doc.doc_deletado is null and doc.doc_ativo =1
						inner join [dbo].[tab_documento_tipos] tip on tip.tpd_id = doc.tpd_id and tip.tpd_deletado is null and tip.tpd_ativo=1
					where dob_deletado is null
						and [obj_id] = @obj_id_orig
						and @obj_id_TipoObraDeArte <> @obj_id_orig
				) as tb1
			group by idOrigem, doc_id, doc_codigo, doc_descricao, doc_caminho, doc_ativo, tpd_id, tpd_descricao
				order by  idOrigem;

		
    --      select doc.*,  1 as doc_Associado, tpd_descricao, dob.obj_id
    --        from dbo.tab_documentos doc
				--inner join [dbo].[tab_documento_tipos] tip on tip.tpd_id = doc.tpd_id
	   --         inner join dbo.tab_documento_objeto dob on dob.doc_id= doc.doc_id
	   --         where obj_id = @obj_id 
				--	and dob_deletado is  null
				--	and doc_deletado is null
				--	and doc_ativo = 1
			 --order by doc_codigo asc;
			 end
	end
	else -- retorna os Objetos NAO associados
	begin
	insert into #temp2 (idOrigem, doc_id, doc_codigo, doc_descricao, doc_caminho, doc_ativo, tpd_id, tpd_descricao, doc_Associado, dos_referencia ) 
          select --doc.*,  1 as doc_Associado, tpd_descricao, dob.obj_id
					distinct 
					0 as idOrigem,
				    doc.doc_id, 
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
										from dbo.tab_documento_objeto
										where 
										dob_deletado is null 
										and obj_id = @obj_id);
	end;


	--update #temp2
--	set total_registros = (select count(*) from #temp2);

	declare @total_registros int = (select count(*) from #temp2);
	select top 100 @total_registros as total_registros, * 
	from  #temp2 order by doc_codigo;


return
