CREATE PROCEDURE [dbo].[STP_SEL_ATRIBUTOS] 
@atr_id int = null,
@filtro_codigo varchar(100)= null,
@filtro_descricao varchar(100)=null,
@filtro_clo_id int = null,
@filtro_tip_id int = null,
@atr_atributo_funcional int =0

AS 
  BEGIN 

		  SELECT  atf.* ,
				clo.clo_nome, clo.clo_descricao,
				obt.tip_nome, obt.tip_descricao,
				dbo.ConcatenarAtributoItens( atf.atr_id, 0 ) as atr_itens_codigo,
				dbo.ConcatenarAtributoItens( atf.atr_id, 1 ) as atr_itens_descricao,
				dbo.ConcatenarAtributoItens( atf.atr_id, 2 ) as atr_itens_ids
		  FROM  dbo.tab_atributos atf
				  inner join dbo.tab_objeto_classes clo on clo.clo_id = atf.clo_id and clo_deletado is null and clo_ativo=1
				  left join dbo.tab_objeto_tipos obt on obt.tip_id = atf.tip_id and  tip_deletado is null and tip_ativo=1
		  where atr_deletado is null
				and atr_id = case when @atr_id is null or @atr_id = -1 then atr_id else @atr_id end
				and	atr_atributo_nome like '%' + @filtro_codigo + '%'
				and	atr_descricao like '%' + isnull(@filtro_descricao, '') + '%'
				and atf.clo_id = case when @filtro_clo_id = -1 then atf.clo_id else @filtro_clo_id end
				and atf.tip_id = case when @filtro_tip_id = -1 then atf.tip_id else @filtro_tip_id end
				and atf.atr_atributo_funcional =  @atr_atributo_funcional
				and atf.atr_id >=0
		  order by atr_atributo_nome; 

  END ;
