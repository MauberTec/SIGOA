CREATE PROCEDURE [dbo].[STP_SEL_DOCUMENTOS] 
@doc_id int=null,
@doc_codigo varchar(50) = '',
@doc_descricao varchar(255) = '',
@tpd_id nvarchar(3)= '',
@dcl_codigo nvarchar(3) = ''

AS 
  BEGIN 

   SET NOCOUNT ON

      SELECT top 200 *,
	  --doc_id, doc_codigo, doc_descricao, doc.dcl_id, doc.tpd_id, doc_caminho, 
	  --tpd_subtipo, tpd_descricao,
		  dcl_codigo,
		  dcl_descricao
      FROM [dbo].[tab_documentos] doc
		  inner join [dbo].[tab_documento_tipos] tip on tip.tpd_id = doc.tpd_id 
		  left join dbo.tab_documento_classes_projeto dcl on dcl.dcl_codigo = substring(doc_codigo, CHARINDEX('/',doc_codigo)-3, 3)  --doc.tpd_id 
	 -- left join dbo.tab_documento_classes_projeto dcl on dcl.dcl_id = doc.dcl_id and dcl.dcl_deletado is null
	  where [doc_deletado] is null 
		and doc_id = case when @doc_id is null then doc_id else @doc_id end
		and doc_codigo like case when @doc_codigo = '' then doc_codigo else '%' + @doc_codigo + '%' end
		and doc_descricao like case when @doc_descricao = '' then doc_descricao else  '%' + @doc_descricao + '%' end
		and doc.tpd_id = case when @tpd_id = '' then doc.tpd_id else @tpd_id end
		and isnull(dcl.dcl_codigo,'') = case when @dcl_codigo = '' then isnull(dcl.dcl_codigo,'') else @dcl_codigo end

  END ;
