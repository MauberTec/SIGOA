CREATE procedure [dbo].[STP_UPD_ATIVARDESATIVAR_OBJETO] 
@obj_id int,
@usu_id int,
@ip nvarchar(30)


--with encryption
as

begin try
		BEGIN TRAN T1
				declare @actionDate datetime
				set @actionDate = getdate()

				declare @obj_codigo nvarchar(200) = (select top 1 obj_codigo from dbo.tab_objetos where  obj_deletado is null and obj_id= @obj_id );					
				declare @obj_ativo_old bit = (select top 1 obj_ativo from dbo.tab_objetos where  obj_deletado is null and obj_id= @obj_id );
				declare @obj_ativo_new bit =  ~ @obj_ativo_old;

					update dbo.tab_objetos
					set obj_ativo = @obj_ativo_new,
						obj_data_atualizacao = @actionDate,
						obj_atualizado_por = @usu_id
					 where obj_id = @obj_id;

				--se houve alteracao em cascata nos filhos
				update dbo.tab_objetos
					set obj_ativo = @obj_ativo_new,
						obj_data_atualizacao = @actionDate,
						obj_atualizado_por = @usu_id
					 where  obj_deletado is null 
							and obj_codigo like (@obj_codigo + '%'); 

					 				
			-- ********* INSERÇÃO DE LOG **************************************

				declare @tabela varchar(300) = 'tab_objetos';
				declare @tra_id int = 8; --8= "ativacão"
				declare @mod_id_log int = 140; -- cadastro de OBJETOS
				declare @obj_ativo int = 0; 

				set @obj_ativo = (select obj_ativo 
				                  from dbo.tab_objetos
				                   where  obj_id= @obj_id );

				if @obj_ativo = 0
					set @tra_id = 9; -- 9= desativacao / registro desativado


				-- checa se a tmp table existem e a exclui
				set nocount on;
				if OBJECT_ID('tempdb..#tmpTabela') is not null
					DROP TABLE #tmpTabela;

				-- insere dados NEW na tabela #tmpTabela
				SELECT * into #tmpTabela 
				from dbo.tab_objetos
				where  obj_id= @obj_id ;


				-- compara as linhas e retorna em varchar
				declare @log_texto varchar(MAX); 
				exec dbo.STP_CONCATENA_COLS_VALS  @tabela, @retorno = @log_texto output;

				-- exclui a temporaria
				DROP TABLE #tmpTabela;

				set nocount off;
				exec dbo.STP_INS_LOGSISTEMA @tra_id, @usu_id, @mod_id_log,	@log_texto,	@ip			

			 -- ****************************************************************

		COMMIT TRAN T1

end try
begin catch
		ROLLBACK TRAN T1
            PRINT 'The following error has occurred:  ' + ERROR_MESSAGE()

		--Raise an error
		 DECLARE @ErrorMessage varchar(255), @ErrorSeverity int, @ErrorState int 
		 SELECT @ErrorMessage = ERROR_MESSAGE(), @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE() 
		 RAISERROR(@ErrorMessage,@ErrorSeverity,@ErrorState) 
end catch
