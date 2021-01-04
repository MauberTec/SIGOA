CREATE PROCEDURE [dbo].[STP_SEL_PRECOS_UNITARIOS] 
@tpu_data_base_der varchar(20) = null,
@tpt_id varchar(1) = null,
@fas_id int = -1

AS 
  BEGIN 

  declare @data date =(select top 1 tpu_data_base_der
							from (
									select distinct  tpu_data_base_der
									from dbo.tab_tpu_precos_unitarios 
						          ) tb1
						order by 1 desc);


  if (@tpu_data_base_der is null )
	set @tpu_data_base_der = convert(varchar(20), @data, 103) ;

		select 
			tpu_id
			, CONVERT(varchar(20),tpu_data_base_der,103)  AS tpu_data_base_der
			,tpu_codigo_der
			,tpu_descricao
			,tpu_preco_unitario
			,tpu_tipo_unidade
			,tpu_preco_calculado
			,tpu.fas_id
			,fas_descricao
			,tpu.tpt_id
			,tpt_descricao
			,tpu.uni_id
			,uni_unidade
			,uni_descricao
			,tpu.moe_id
			,tpu_ativo
			,moe_nome
			,uni.unt_id
			,unt_nome
	from dbo.tab_tpu_precos_unitarios tpu
			inner join dbo.tab_tpu_tipos_preco tpt on tpt.tpt_id = tpu.tpt_id and tpt_deletado is null and tpt_ativo = 1
			inner join dbo.tab_tpu_fases fas on fas.fas_id = tpu.fas_id and fas_deletado is null and fas_ativo = 1
			inner join dbo.tab_moedas moe on moe.moe_id = tpu.moe_id and moe_deletado is null and moe_ativo = 1
			inner join dbo.tab_unidades_medida uni on uni.uni_id = tpu.uni_id and uni_deletado is null and uni_ativo = 1
			inner join dbo.tab_unidades_tipos unt on unt.unt_id = uni.unt_id  and unt_deletado is null and unt_ativo = 1    
	where tpu_deletado is null 
			and tpu_ativo = 1
			and tpu_data_base_der = convert(date, @tpu_data_base_der, 103)
			and tpu.tpt_id = case when @tpt_id = 'x' then tpu.tpt_id else @tpt_id end
			and tpu.fas_id = case when @fas_id < 0 then tpu.fas_id else @fas_id end;

  END ;
