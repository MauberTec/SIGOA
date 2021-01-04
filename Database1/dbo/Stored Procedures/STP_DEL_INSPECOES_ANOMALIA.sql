create procedure [dbo].[STP_DEL_INSPECOES_ANOMALIA] 
@ian_id int, 
@usu_id int,
@ip nvarchar(30)


--with encryption
as

begin try
		BEGIN TRAN T1
					
					update dbo.tab_inspecoes_anomalias
					set ian_deletado = GETDATE(),
						ian_data_atualizacao = GETDATE(),
						ian_atualizado_por = @usu_id
					 where  ian_id = @ian_id;

					 
					 				
			-- ********* INSERÇÃO DE LOG **************************************

				declare @tabela varchar(300) = 'tab_inspecoes_anomalias';
				declare @tra_transacao_id int = 5; -- 5= "exclusão"
				declare @mod_modulo_id_log int = -201; -- tab_inspecoes_anomalias

				-- checa se a tmp table existem e a exclui
				set nocount on;
				if OBJECT_ID('tempdb..#tmpTabela') is not null
					DROP TABLE #tmpTabela;

				-- insere dados na tabela #tmpTabela
				SELECT * into #tmpTabela 
				from dbo.tab_inspecoes_anomalias
				 where  ian_id= @ian_id ;


				-- concatena os valores e retorna em varchar
				declare @log_texto varchar(MAX); 
				exec dbo.STP_CONCATENA_COLS_VALS  @tabela, @retorno = @log_texto output;

				-- exclui a temporaria
				DROP TABLE #tmpTabela;

				set nocount off;
				exec dbo.STP_INS_LOGSISTEMA @tra_transacao_id, @usu_id, @mod_modulo_id_log,	@log_texto,	@ip			

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
