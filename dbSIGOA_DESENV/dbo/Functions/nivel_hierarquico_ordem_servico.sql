create function [dbo].[nivel_hierarquico_ordem_servico] (@ord_id_aux int) RETURNS int
AS
BEGIN

declare @nivel int =0;
declare @ord_id_aux2 int = 0;
declare @ord_pai_aux int = 0

while (@ord_pai_aux is not null) and (@nivel < 20)
begin

	select @ord_id_aux2 = ord_id,
			@ord_pai_aux = ord_pai
	from dbo.tab_ordens_servico
	where ord_deletado is null
			and ord_id = @ord_id_aux;

	set @nivel = @nivel + 1;
	set @ord_id_aux = @ord_pai_aux;
end;

   return @nivel

END
