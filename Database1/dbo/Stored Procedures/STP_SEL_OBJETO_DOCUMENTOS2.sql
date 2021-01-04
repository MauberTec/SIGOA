CREATE procedure [dbo].[STP_SEL_OBJETO_DOCUMENTOS2]
@ord_id int = -1
as

		select 
					doc.doc_id, doc_codigo as doc_codigo, doc_descricao, doc_caminho, doc_ativo, 
					tip.tpd_id, tpd_descricao,
					isnull(dos.dos_referencia,0) as dos_referencia
		from [dbo].[tab_ordens_servico] ord 
						inner join [dbo].[tab_documento_objeto] dob on ord.obj_id = dob.obj_id and dob_deletado is null
						left join [dbo].[tab_documento_ordens_servicos] dos on (dos.doc_id = dob.doc_id  or dos.doc_id is null) and dos.ord_id = @ord_id and dos.dos_deletado is null
						inner join [dbo].[tab_documentos] doc on doc.doc_id = dob.doc_id and (dos.doc_id = doc.doc_id or dos.doc_id is null) and doc.doc_deletado is null and doc.doc_ativo =1
						inner join [dbo].[tab_documento_tipos] tip on tip.tpd_id = doc.tpd_id and tip.tpd_deletado is null and tip.tpd_ativo=1
		where  ord.ord_id = @ord_id

return
