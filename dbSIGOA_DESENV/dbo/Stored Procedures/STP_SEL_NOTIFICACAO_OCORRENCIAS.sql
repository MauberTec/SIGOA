
CREATE PROCEDURE [dbo].[STP_SEL_NOTIFICACAO_OCORRENCIAS] 
@ord_id int = -1
AS 
 BEGIN 
		declare @obj_id_TipoObraDeArte int = (select obj_id from tab_ordens_servico where ord_id = @ord_id);

 	   -- IDENTIFICAÇÃO DA OBRA DE ARTE / CÓDIGO
		declare @IdentificacaoOAE varchar(2000) = (select top 1 obj_codigo from dbo.tab_objetos where obj_id = @obj_id_TipoObraDeArte) ;

	   -- Nome da Obra de Arte
		declare @NomeOAE varchar(200) = (select top 1 obj_descricao from dbo.tab_objetos where obj_id = @obj_id_TipoObraDeArte) ;

	   -- Código da Rodovia
		declare @CodigoRodovia varchar(200) = (select top 1 isnull(obj_codigo,'') from dbo.tab_objetos where clo_id = 1 and obj_deletado is null and obj_codigo = LEFT(@IdentificacaoOAE, LEN(obj_codigo)));

	   -- Nome Rodovia
	    declare @NomeRodovia varchar(2000) = (select top 1 obj_descricao from dbo.tab_objetos where  clo_id = 1 and obj_deletado is null and obj_codigo = LEFT(@IdentificacaoOAE, LEN(obj_codigo))) ;

	   -- Localização - KM
		declare @KM_ObraDeArte varchar(200) = replace(@IdentificacaoOAE, @CodigoRodovia + '-', '') ;
		declare @LocalizacaoKm  varchar(200) = left(@KM_ObraDeArte, 7); -- deixa somente o '000,000'

	   -- municipio
		declare @codmunicipio varchar (3) = (select top 1 isnull(atv_valor,'-1') from  [dbo].[tab_objeto_atributos_valores]  where atr_id = 14 and obj_id = @obj_id_TipoObraDeArte);
		declare @Municipio varchar(200) = (select top 1 isnull([mun_municipio],'') from [dbo].[tab_municipios] where mun_id = CONVERT(int, @codmunicipio));

	   -- tipoOAE
		 declare @Tipo varchar(200) = (SELECT  tip_nome + ' (' + [tip_codigo] + ')'  
												FROM dbo.tab_objeto_tipos  tip
												inner join dbo.tab_objetos obj on obj.tip_id = tip.tip_id and obj.obj_id = @obj_id_TipoObraDeArte
												where tip.clo_id = 3 and tip_deletado is null)


 declare @n int = (select count(*) from [dbo].tab_notificacao_ocorrencias where ord_id = @ord_id );


 if @n > 0 
 begin


		select  
			isnull(@IdentificacaoOAE,'') as IdentificacaoOAE,
			isnull(@NomeOAE,'') as NomeOAE, 
			isnull(@CodigoRodovia,'') as CodigoRodovia,
			isnull(@NomeRodovia,'') as NomeRodovia,
			isnull(@KM_ObraDeArte,'') as KM_ObraDeArte,
			isnull(@LocalizacaoKm,'') as LocalizacaoKm,
			isnull(@Municipio,'') as Municipio,
			isnull(@Tipo,'') as Tipo,
			noc_id, 
			ord_id, 
			noc_data_notificacao, 
			noc_responsavel_notificacao, 
			noc_descricao_ocorrencia, 
			noc_solicitante, 
			noc_solicitante_data, 
			noc_responsavel_recebimento, 
			noc_responsavel_recebimento_data
		from [dbo].tab_notificacao_ocorrencias
		where ord_id = @ord_id;
 end
else
select 		
			isnull(@IdentificacaoOAE,'') as IdentificacaoOAE,
			isnull(@NomeOAE,'') as NomeOAE, 
			isnull(@CodigoRodovia,'') as CodigoRodovia,
			isnull(@NomeRodovia,'') as NomeRodovia,
			isnull(@KM_ObraDeArte,'') as KM_ObraDeArte,
			isnull(@LocalizacaoKm,'') as LocalizacaoKm,
			isnull(@Municipio,'') as Municipio,
			isnull(@Tipo,'') as Tipo,
		-1 as noc_id, 
		-1 as ord_id, 
		'' as noc_data_notificacao, 
		'' as noc_responsavel_notificacao, 
		'' as noc_descricao_ocorrencia, 
		'' as noc_solicitante, 
		'' as noc_solicitante_data, 
		'' as noc_responsavel_recebimento, 
		'' as noc_responsavel_recebimento_data


  END ;


