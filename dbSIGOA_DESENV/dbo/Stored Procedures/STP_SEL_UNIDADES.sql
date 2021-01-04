CREATE PROCEDURE [dbo].[STP_SEL_UNIDADES] 
@uni_id int = null,
@unt_id int = null
AS 
  BEGIN 
		select * 
		from dbo.tab_unidades_medida uni
		inner join dbo.tab_unidades_tipos unt on unt.unt_id = uni.unt_id 
		where 
			uni_deletado is null
			and uni_id = case when @uni_id is not null then @uni_id else uni_id end
			and uni.unt_id = case when @unt_id is not null then @unt_id else uni.unt_id end
		order by uni_unidade;
  END ;
