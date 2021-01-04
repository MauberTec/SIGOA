CREATE PROCEDURE [dbo].[STP_SEL_UNIDADE_TIPO] 
@unt_id int = null,
@unt_nome varchar(30) = null
AS 
  BEGIN
  
  		  select  * 
		  from dbo.tab_unidades_tipos 
		  where unt_deletado is null
				and unt_id = case when @unt_id = -31415 
								  then (select top 1 convert(int, par_valor) from dbo.tab_parametros where par_id = 'Id_TipoUnidade_TPU')
								  else case when @unt_id is not null 
											then @unt_id
											else unt_id 
											end
								  end
				and unt_nome like (case when @unt_nome is not null 
												then @unt_nome + '%'
												else unt_nome
												end)
		  order by unt_nome;

  END ;
