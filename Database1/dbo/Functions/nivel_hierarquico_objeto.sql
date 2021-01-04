CREATE function [dbo].[nivel_hierarquico_objeto] (@obj_id_aux int) RETURNS int
AS
BEGIN

declare @nivel int =0;
declare @obj_id_aux2 int = 0;
declare @obj_pai_aux int = 0

while (@obj_pai_aux is not null) and (@nivel < 20)
begin

	select @obj_id_aux2 = obj_id,
			@obj_pai_aux = obj_pai
	from dbo.tab_objetos
	where obj_deletado is null
			--and obj_ativo = 1
			and obj_id = @obj_id_aux;

	set @nivel = @nivel + 1;

--	select @nivel, @obj_id_aux, @obj_pai_aux

	set @obj_id_aux = @obj_pai_aux;

end;
   return @nivel

END
