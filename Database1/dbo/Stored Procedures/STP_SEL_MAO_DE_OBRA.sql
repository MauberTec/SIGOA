--Select Employees 

CREATE PROCEDURE [dbo].[STP_SEL_MAO_DE_OBRA] 
@mao_id int = null
AS 
  BEGIN 

  if (@mao_id is null )
      SELECT  * 
      FROM  dbo.tab_mao_de_obra mao
	  inner join dbo.tab_unidades_medida uni on uni.uni_id = mao.uni_id
      where mao_deletado is  null;
	else
      SELECT  * 
      FROM  dbo.tab_mao_de_obra mao
	  inner join dbo.tab_unidades_medida uni on uni.uni_id = mao.uni_id
      where mao_deletado is  null
		and mao_id = @mao_id;

  END ;
