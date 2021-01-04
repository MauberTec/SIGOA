CREATE function [dbo].[checa_pode_deletar_objeto] (@obj_id_aux int) RETURNS int
AS
BEGIN

-- checa se as tmp table existe e a exclui
  declare @tmpObjs table (obj_id int);


	-- acha os ids que possuem associacao e salva numa tabela temporaria
	insert into  @tmpObjs (obj_id)
	select distinct obj_id from dbo.tab_documento_objeto dob where dob_deletado is null

	insert into  @tmpObjs (obj_id)
	select distinct obj_id from  dbo.tab_ordens_servico ord where ord_deletado is null and ord_ativo=1

	-- pesquisa a hierarquia acima dos objetos existentes, e acrescenta nessa tabela temporaria
		declare @obj_id int = 0;
		declare @nn int =0;
		declare @obj_pai_aux int = 0

		declare cursor_p cursor for  select distinct obj_id from @tmpObjs
		open cursor_p;
			fetch next from  cursor_p into  @obj_id ;
		while @@FETCH_STATUS = 0
		begin

				while (@obj_pai_aux is not null) and (@nn < 20)
				begin

					insert into @tmpObjs (obj_id) values (@obj_id);
					set @obj_pai_aux = (select  obj_pai	from dbo.tab_objetos  where obj_deletado is null and obj_id = @obj_id);

				  set @obj_id = @obj_pai_aux;
				  set @nn = @nn + 1;
				end;

		  fetch next from  cursor_p into  @obj_id ;
		  set @obj_pai_aux  = @obj_id;
		end;

		close cursor_p;
		deallocate cursor_p;
	

	set @nn = (select count(*) from @tmpObjs where obj_id = @obj_id_aux);
	if (@nn > 0)
	 set @nn =0;
	else
	 set @nn = 1;

   return @nn ;

END
