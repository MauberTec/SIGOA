CREATE procedure [dbo].[STP_UPD_ATIVARDESATIVAR_ANOM_ALERTA] 
@ale_id int,
@usu_id int,
@ip nvarchar(30)


--with encryption
as

begin try
		BEGIN TRAN T1
					
					update dbo.tab_anomalia_alertas
					set ale_ativo = ~ ale_ativo,
						ale_data_atualizacao = GETDATE(),
						ale_atualizado_por = @usu_id
					 where ale_id = @ale_id;


					 				
			-- ********* INSERÇÃO DE LOG **************************************

				declare @tabela varchar(300) = 'tab_anomalia_alertas';
				declare @tra_id int = 8; --8= "ativacão"
				declare @mod_id_log int = 240; -- alerta
				declare @ale_ativo int = 0; 

				set @ale_ativo = (select ale_ativo 
				                  from dbo.tab_anomalia_alertas
				                   where  ale_id= @ale_id );

				if @ale_ativo = 0
					set @tra_id = 9; -- 9= desativado / registro desativado


				-- checa se a tmp table existem e a exclui
				set nocount on;
				if OBJECT_ID('tempdb..#tmpTabela') is not null
					DROP TABLE #tmpTabela;

				-- insere dados NEW na tabela #tmpTabela
				SELECT * into #tmpTabela 
				from dbo.tab_anomalia_alertas
				where  ale_id= @ale_id ;


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
