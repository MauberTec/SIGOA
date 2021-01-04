CREATE PROCEDURE [dbo].[STP_SEL_INSPECOES] 
@ins_id int=0,
@filtroOrdemServico_codigo varchar(100)= null,
@filtroObj_codigo varchar(100)=null,
@filtroTiposOS int = null,
@filtroStatusOS int = null,
@filtroData varchar(50)= 'Inicio_Programada',
@filtroord_data_De varchar(15)= '01/01/2000',
@filtroord_data_Ate varchar(15)= '31/12/2100'

AS 
  BEGIN 

--SELECT ins_id,
--		ins.obj_id, obj_codigo, obj_descricao,
--		ins.ipt_id, ipt_codigo, ipt_descricao,
--		ins.ord_id, ord_codigo, ord_descricao,
--		sos.sos_id,sos_codigo, sos_descricao
SELECT *
FROM [dbo].[tab_inspecoes] ins
 -- inner join [dbo].[tab_inspecao_tipos] ipt on ipt.ipt_id = ins.ipt_id and ipt.ipt_ativo = 1 and ipt.ipt_deletado is null
  inner join [dbo].[tab_ordens_servico] ord on ord.ord_id = ins.ord_id and ord.ord_ativo = 1 and ord.ord_deletado is null
  inner join [dbo].[tab_objetos] obj on obj.obj_id = ins.obj_id and obj.obj_ativo = 1 and obj.obj_deletado is null
  inner join [dbo].[tab_ordem_servico_status] sos on sos.sos_id = ord.sos_id and sos.sos_ativo = 1 and sos.sos_deletado is null
  inner join [dbo].[tab_ordem_servico_tipos] tos on tos.tos_id = ord.tos_id and tos.tos_ativo = 1 and tos.tos_deletado is null
where ins.ins_deletado is null
		and ins_id = case when @ins_id = 0 then ins_id else @ins_id end
	and	ord_codigo like '%' + isnull(@filtroOrdemServico_codigo, '') + '%'
	and	isnull(obj.obj_codigo,'%') like '%' + isnull(@filtroObj_codigo, '') + '%'
	and isnull(sos.sos_id,-1) = case when @filtroStatusOS is null then isnull(sos.sos_id,-1) else @filtroStatusOS end
	and isnull(tos.tos_id,-1) = case when @filtroTiposOS is null then isnull(tos.tos_id,-1) else @filtroTiposOS end



  END ;
