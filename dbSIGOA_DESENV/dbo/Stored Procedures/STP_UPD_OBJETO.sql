CREATE procedure [dbo].[STP_UPD_OBJETO] 
@obj_id int, 
@obj_codigo nvarchar(200),
@obj_descricao nvarchar(255),
@tip_id int,
@usu_id int,
@ip nvarchar(30)

--with encryption
as

begin try
		BEGIN TRAN T1
				declare @actionDate datetime
				set @actionDate = getdate()

set nocount on;



				declare @atv_valores_log varchar (max)='';

				declare @obj_codigo_old nvarchar(200) = ''; 
				declare @obj_descricao_old nvarchar(200) = ''; 
				declare @tip_id_old int = -1; 

				-- pega os valores antigos
				select  @obj_codigo_old= obj_codigo,
						@obj_descricao_old = obj_descricao,
						@tip_id_old = @tip_id
				from dbo.tab_objetos 
				where  obj_deletado is null 
						and obj_id= @obj_id ;

				-- atualiza os novos
				update dbo.tab_objetos
				    set 
					    obj_codigo = @obj_codigo, 
						obj_descricao = @obj_descricao,
						tip_id = @tip_id,
						obj_atualizado_por = @usu_id,
						obj_data_atualizacao = @actionDate 
					 where  obj_deletado is null
							and obj_id= @obj_id ;

				-- LOG ---
				set @atv_valores_log = @atv_valores_log + 'Chave[obj_id]=[' + convert(varchar(3), @obj_id)  + '];';

				if (@tip_id_old <> @tip_id)
					set @atv_valores_log = @atv_valores_log + 'Coluna [tab_objetos].[tip_id] de ['  +  convert(varchar(3), @tip_id_old)  + '] para [' + convert(varchar(3),@tip_id)  + '];';

				set @atv_valores_log = @atv_valores_log + 'Coluna [tab_objetos].[obj_codigo] de ['  + @obj_codigo_old  + '] para [' + @obj_codigo  + '];';
				set @atv_valores_log = @atv_valores_log + 'Coluna [tab_objetos].[obj_descricao] de ['  + @obj_descricao_old  + '] para [' + @obj_descricao  + '];';

				--se houve alteracao no código, entao altera em cascata nos filhos
				if (@obj_codigo_old <> @obj_codigo)
				begin
					-- expande para os filhos e cria log
					declare @obj_id_tmp bigint=0;
					declare @obj_codigo_tmp nvarchar(200) = '';
					declare @obj_descricao_tmp nvarchar(200) = '';


					declare cursor_objs CURSOR FOR	select obj_id, obj_codigo, obj_descricao
														from dbo.tab_objetos 
														where  
														obj_deletado is null 
														and obj_codigo like @obj_codigo_old + '%'
														and obj_id <> @obj_id;

					open cursor_objs;
					fetch next from cursor_objs into @obj_id_tmp, @obj_codigo_tmp, @obj_descricao_tmp;

					while @@FETCH_STATUS = 0
						begin
							-- atualiza os dados
							update dbo.tab_objetos
							set 
								obj_codigo = replace(obj_codigo,  @obj_codigo_old, @obj_codigo),
								obj_descricao = replace(obj_descricao,  @obj_codigo_old, @obj_codigo),
								obj_atualizado_por = @usu_id,
								obj_data_atualizacao = getdate() 
								where  obj_id= @obj_id_tmp ;
											
							-- cria log
							set @atv_valores_log = @atv_valores_log + 'Chave[obj_id]=[' + convert(varchar(3), @obj_id_tmp)  + '];';
							set @atv_valores_log = @atv_valores_log + 'Coluna [tab_objetos].[obj_codigo] de ['  + @obj_codigo_tmp  + '] para [' + replace(@obj_codigo_tmp,  @obj_codigo_old, @obj_codigo)  + '];';
							set @atv_valores_log = @atv_valores_log + 'Coluna [tab_objetos].[obj_descricao] de ['  + @obj_descricao_tmp  + '] para [' + replace(@obj_descricao_tmp, @obj_codigo_old, @obj_codigo)  + '];';

							fetch next from cursor_objs into @obj_id_tmp, @obj_codigo_tmp, @obj_descricao_tmp;
						end;
					close cursor_objs;
					DEALLOCATE cursor_objs;
				end


-- ********* INSERÇÃO DE LOG  ****************************
				declare @tabela varchar(300) = 'tab_objetos';
				declare @tra_transacao_id int = 6; -- tra_transacao_id= 6 ==> alteração
				declare @mod_modulo_id_log int =140; -- cadastro de OBJETOS

				exec dbo.STP_INS_LOGSISTEMA @tra_transacao_id, @usu_id, @mod_modulo_id_log,	@atv_valores_log,	@ip	

				set nocount off;


		COMMIT TRAN T1

		return @obj_id
end try
begin catch
		ROLLBACK TRAN T1
            PRINT 'The following error has occurred:  ' + ERROR_MESSAGE()

		--Raise an error
		 DECLARE @ErrorMessage varchar(255), @ErrorSeverity int, @ErrorState int 
		 SELECT @ErrorMessage = ERROR_MESSAGE(), @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE() 
		 RAISERROR(@ErrorMessage,@ErrorSeverity,@ErrorState) 
end catch
