create function [dbo].[fn_GetErroMsg] (@codigo int, @culture nvarchar(20)) returns varchar(500)
as
begin
declare @retorno  varchar(500);

	set @retorno = (SELECT MSG_DESCRICAO
	  FROM dbo.APPMENSAGEM
	  where MSG_CODIGO = @codigo and MSG_CULTURE= @culture)	  ;

return @retorno

end
