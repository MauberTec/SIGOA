CREATE procedure [dbo].[STP_UPD_ATIVARDESATIVAR_OS_FLUXOSTATUS] 
@fos_id int,
@usu_id int,
@ip nvarchar(30)


--with encryption
as

begin try
		BEGIN TRAN T1
					
					update dbo.tab_ordem_servico_fluxos_status
					set fos_ativo = ~ fos_ativo,
						fos_data_atualizacao = GETDATE(),
						fos_atualizado_por = @usu_id
					 where fos_id = @fos_id;


					 				
			-- ********* INSERÇÃO DE LOG **************************************

				declare @tabela varchar(300) = 'tab_ordem_servico_fluxos_status';
				declare @tra_id int = 8; --8= "ativacão"
				declare @mod_id_log int = 530; -- fluxo de status de OS
				declare @fos_ativo int = 0; 

				set @fos_ativo = (select fos_ativo 
				                  from dbo.tab_ordem_servico_fluxos_status
				                   where  fos_id= @fos_id );

				if @fos_ativo = 0
					set @tra_id = 9; -- 9= desativacao / registro desativado


				-- checa se a tmp table existem e a exclui
				set nocount on;
				if OBJECT_ID('tempdb..#tmpTabela') is not null
					DROP TABLE #tmpTabela;

				-- insere dados NEW na tabela #tmpTabela
				SELECT * into #tmpTabela 
				from dbo.tab_ordem_servico_fluxos_status
				where  fos_id= @fos_id ;


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
