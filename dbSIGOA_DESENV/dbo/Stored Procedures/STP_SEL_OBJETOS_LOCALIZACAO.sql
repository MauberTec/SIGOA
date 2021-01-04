
create PROCEDURE [dbo].[STP_SEL_OBJETOS_LOCALIZACAO] 
@obj_id_TipoOAE int = -1,
@tip_id_Subdivisao1 int = null

AS 
BEGIN 

		declare @clo_id int = 6
		declare @obj_codigo_TipoOAE varchar(100) = (select obj_codigo from tab_objetos where obj_id = @obj_id_TipoOAE);
		declare @obj_codigo_Subdivisao1 varchar(100) = (select obj_codigo from tab_objetos where obj_deletado is null and obj_ativo = 1 and obj_codigo like @obj_codigo_TipoOAE + '%' and clo_id = @clo_id and tip_id = @tip_id_Subdivisao1);

		select * 
		from  tab_objetos 
		where obj_deletado is null and obj_ativo = 1 
		and clo_id > 9 
		and obj_codigo like @obj_codigo_Subdivisao1 + '%' 
		order by obj_codigo;


end
