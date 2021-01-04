CREATE procedure [dbo].[STP_UPD_ATIVARDESATIVAR_ATRIBUTO] 
@atr_id int,
@usu_id int,
@ip nvarchar(30)


--with encryption
as

begin try
		BEGIN TRAN T1
					
					update dbo.tab_atributos
					set atr_ativo = ~ atr_ativo,
						atr_data_atualizacao = GETDATE(),
						atr_atualizado_por = @usu_id
					 where  atr_id= @atr_id ;


					 				
			-- ********* INSERÇÃO DE LOG **************************************

				declare @tabela varchar(300) = 'tab_atributos';
				declare @tra_transacao_id int = 8; --8= "ativacão"
				declare @mod_modulo_id_log int = 120; -- 120 = atributos de objeto
				declare @atr_ativo int = 0; 

				set @atr_ativo = (select atr_ativo 
				                  from dbo.tab_atributos
									 where  atr_id= @atr_id  );

				if @atr_ativo = 0
					set @tra_transacao_id = 9; -- 9= desativacao / registro desativado


				-- checa se a tmp table existem e a exclui
				set nocount on;
				if OBJECT_ID('tempdb..#tmpTabela') is not null
					DROP TABLE #tmpTabela;

				-- insere dados NEW na tabela #tmpTabela
				SELECT * into #tmpTabela 
				from dbo.tab_atributos
					 where  atr_id= @atr_id ;


				-- compara as linhas e retorna em varchar
				declare @log_texto varchar(MAX); 
				exec dbo.STP_CONCATENA_COLS_VALS  @tabela, @retorno = @log_texto output, @tudo=0 ;

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
