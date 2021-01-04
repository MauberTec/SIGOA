CREATE procedure [dbo].[STP_DEL_GRUPO] 
@gru_id int,
@usu_id int,
@ip nvarchar(30)


--with encryption
as

begin try
		BEGIN TRAN T1
					
					update dbo.tab_grupos
					set gru_deletado = GETDATE(),
						gru_data_atualizacao = GETDATE(),
						gru_atualizado_por = @usu_id
					 where gru_id = @gru_id;


					--exclui os perfis associados 
					update dbo.tab_perfis_grupos
					set pfg_deletado = GETDATE(),
						pfg_data_atualizacao = GETDATE(),
						pfg_atualizado_por = @usu_id
					 where gru_id = @gru_id;	
					 
					 
					--exclui os usuarios associados 
					update dbo.tab_grupos_usuarios
					set gpu_deletado = GETDATE(),
						gpu_data_atualizacao = GETDATE(),
						gpu_atualizado_por = @usu_id
					 where gru_id = @gru_id;						 
					 
					 				
			-- ********* INSERÇÃO DE LOG **************************************

				declare @tabela varchar(300) = 'tab_grupos';
				declare @tra_id int = 5; -- 5= "exclusão"
				declare @mod_id_log int = 1070; -- 1070 = grupo

				-- checa se a tmp table existem e a exclui
				set nocount on;
				if OBJECT_ID('tempdb..#tmpTabela') is not null
					DROP TABLE #tmpTabela;

				-- insere dados na tabela #tmpTabela
				SELECT * into #tmpTabela 
				from dbo.tab_grupos
				where  gru_id= @gru_id ;


				-- concatena os valores e retorna em varchar
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
