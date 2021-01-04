create procedure dbo.STP_INS_NOTIFICACAO_OCORRENCIA 
@ord_id int,
@data_notificacao nVARCHAR(25),
@responsavel_notificacao nVARCHAR(100),
@descricao_ocorrencia nVARCHAR(1000),
@solicitante nVARCHAR(100),
@solicitante_data nVARCHAR(25),
@responsavel_recebimento nVARCHAR(100),
@responsavel_recebimento_data nVARCHAR(25),
@usu_id int,
@ip nvarchar(30)

--with encryption
as

begin try
		BEGIN TRAN T1
				declare @actionDate datetime
				set @actionDate = getdate()

				declare @noc_id int
				set @noc_id = (select isnull(max(noc_id),0) +1 from dbo.tab_notificacao_ocorrencias);
				
				-- insere novo grupo
				INSERT INTO dbo.tab_notificacao_ocorrencias(noc_id, ord_id, noc_data_notificacao, noc_responsavel_notificacao, noc_descricao_ocorrencia, noc_solicitante, noc_solicitante_data, noc_responsavel_recebimento, noc_responsavel_recebimento_data, noc_ativo, noc_data_criacao, noc_criado_por )
				    values (@noc_id, @ord_id, @data_notificacao, @responsavel_notificacao, @descricao_ocorrencia, @solicitante, @solicitante_data, @responsavel_recebimento, @responsavel_recebimento_data, 1, @actionDate, @usu_id);
			

			-- ********* INSERÇÃO DE LOG **************************************

				declare @tabela varchar(300) = 'tab_notificacao_ocorrencias';
				declare @tra_id int = 4; -- 4= insercao
				declare @mod_id_log int = -541; 

				-- checa se a tmp table existem e a exclui
				set nocount on;
				if OBJECT_ID('tempdb..#tmpTabela') is not null
					DROP TABLE #tmpTabela;

				-- insere dados NEW na tabela #tmpTabela
				SELECT * into #tmpTabela 
				from dbo.tab_notificacao_ocorrencias
				where  noc_id= @noc_id ;


				-- concatena os valores e retorna em varchar
				declare @log_texto varchar(MAX); 
				exec dbo.STP_CONCATENA_COLS_VALS  @tabela, @retorno = @log_texto output;

				-- exclui a temporaria
				DROP TABLE #tmpTabela;

				set nocount off;
				exec dbo.STP_INS_LOGSISTEMA @tra_id, @usu_id, @mod_id_log,	@log_texto,	@ip			

			 -- ****************************************************************
		COMMIT TRAN T1

		return @noc_id
end try
begin catch
		ROLLBACK TRAN T1
            PRINT 'The following error has occurred:  ' + ERROR_MESSAGE()

		--Raise an error
		 DECLARE @ErrorMessage varchar(255), @ErrorSeverity int, @ErrorState int 
		 SELECT @ErrorMessage = ERROR_MESSAGE(), @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE() 
		 RAISERROR(@ErrorMessage,@ErrorSeverity,@ErrorState) 
end catch
