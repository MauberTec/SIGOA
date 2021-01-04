CREATE PROCEDURE [dbo].[STP_SEL_ATRIBUTOS_ITENS] 
@atr_id int,
@ati_id int = null
AS 
  BEGIN 
		  SELECT  *
		  FROM  dbo.tab_atributo_itens 
		  where ati_deletado is null
			and atr_id = @atr_id
			and ati_id = case when @ati_id is null then ati_id else @ati_id end
		  order by ati_id; -- ati_item;

  END ;
