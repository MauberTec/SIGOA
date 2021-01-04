create procedure [dbo].[STP_DEL_OS_TIPO] 
@tos_id int,
@usu_id int,
@ip nvarchar(30)


--with encryption
as

begin try
		BEGIN TRAN T1
					
					update dbo.tab_ordem_servico_tipos
					set tos_deletado = GETDATE(),
						tos_data_atualizacao = GETDATE(),
						tos_atualizado_por = @usu_id
					 where tos_id = @tos_id;
					 				
			-- ********* INSERÇÃO DE LOG **************************************

				declare @tabela varchar(300) = 'tab_ordem_servico_tipos';
				declare @tra_id int = 5; -- 5= "exclusão"
				declare @mod_id_log int = 510; -- TIPOS DE OS

				-- checa se a tmp table existem e a exclui
				set nocount on;
				if OBJECT_ID('tempdb..#tmpTabela') is not null
					DROP TABLE #tmpTabela;

				-- insere dados na tabela #tmpTabela
				SELECT * into #tmpTabela 
				from dbo.tab_ordem_servico_tipos
				where  tos_id= @tos_id ;


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
