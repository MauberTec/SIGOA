CREATE procedure [dbo].[STP_UPD_ATIVARDESATIVAR_OBJ_TIPO] 
@tip_id int,
@usu_id int,
@ip nvarchar(30)


--with encryption
as

begin try
		BEGIN TRAN T1
					
					update dbo.tab_objeto_tipos
					set tip_ativo = ~ tip_ativo,
						tip_data_atualizacao = GETDATE(),
						tip_atualizado_por = @usu_id
					 where  tip_id= @tip_id ;


					 				
			-- ********* INSERÇÃO DE LOG **************************************

				declare @tabela varchar(300) = 'tab_objeto_tipos';
				declare @tra_transacao_id int = 8; --8= "ativacão"
				declare @mod_modulo_id_log int = 110; -- 110 = tipo/classe de objeto
				declare @tip_ativo int = 0; 

				set @tip_ativo = (select tip_ativo 
				                  from dbo.tab_objeto_tipos
				                   where  tip_id= @tip_id );

				if @tip_ativo = 0
					set @tra_transacao_id = 9; -- 9= desativacao / registro desativado


				-- checa se a tmp table existem e a exclui
				set nocount on;
				if OBJECT_ID('tempdb..#tmpTabela') is not null
					DROP TABLE #tmpTabela;

				-- insere dados NEW na tabela #tmpTabela
				SELECT * into #tmpTabela 
				from dbo.tab_objeto_tipos
				where  tip_id= @tip_id ;


				-- compara as linhas e retorna em varchar
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
