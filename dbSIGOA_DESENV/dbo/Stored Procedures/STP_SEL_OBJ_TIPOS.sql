CREATE PROCEDURE [dbo].[STP_SEL_OBJ_TIPOS] 
@clo_id int = 0,
@tip_id int = 0,
@tip_pai int = 0,
@excluir_existentes int = 0,
@obj_id int = 0
AS 
  BEGIN 

  if @tip_pai is null 
    set @tip_pai = -1;


  if (@excluir_existentes = 1) 
  begin
		declare @codRodovia varchar(10) = '';
		if (@clo_id = 1)
		  set @codRodovia = (select top 1 rtrim(obj_codigo) from [dbo].[tab_objetos] where obj_id = @obj_id);
		else
		  set @codRodovia = (select top 1 substring(obj_codigo,1, charindex('-', obj_codigo)-1)  from [dbo].[tab_objetos] where obj_id = @obj_id);

		 SELECT  tip.*
				,(select COUNT(*) from [dbo].[tab_objeto_grupo_objeto_variaveis] where ogv_deletado is null and tip_id = tip.tip_id) as tem_var_inspecao
		  FROM  dbo.tab_objeto_tipos tip
		  where tip_deletado is null
			  and clo_id = case when @clo_id is null then clo_id else @clo_id end
			  and tip_pai = case when @tip_pai = 0 then tip_pai else @tip_pai end
		      and (tip_id not in (select tip_id from [dbo].[tab_objetos] where clo_id = @clo_id and obj_deletado is null and obj_codigo like (@codRodovia + '%'))
					or tip_id = @tip_id)
		 order by tip_codigo, clo_id, tip_id;
  end
  else
		  SELECT  tip.*
				,(select COUNT(*) from [dbo].[tab_objeto_grupo_objeto_variaveis] where ogv_deletado is null and tip_id = tip.tip_id) as tem_var_inspecao
		  FROM  dbo.tab_objeto_tipos tip
		  where tip_deletado is null
			  and clo_id = case when @clo_id =0 or @clo_id is null then clo_id else @clo_id end
			  and tip_id = case when @tip_id =0 or @tip_id is null  then tip_id else @tip_id end
			  and tip_pai = case when @tip_pai = 0 then tip_pai else @tip_pai end
			order by tip_codigo, clo_id, tip_id; 

end
