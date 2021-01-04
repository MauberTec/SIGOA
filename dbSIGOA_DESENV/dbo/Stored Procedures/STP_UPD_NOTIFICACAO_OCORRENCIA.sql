create procedure dbo.STP_UPD_NOTIFICACAO_OCORRENCIA 
@noc_id int,
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

as

begin try
		BEGIN TRAN T1
				declare @actionDate datetime
				set @actionDate = getdate()

-- ********* INSERÇÃO DE LOG - parte 1 ****************************

				-- checa se as tmp tables existem e as exclui
set nocount on;
				if OBJECT_ID('tempdb..#tmpComparar') is not null
				DROP TABLE #tmpComparar;

				-- cria e insere dados OLD na tabela #tmpComparar
				SELECT 1 as nRow, * into #tmpComparar 
				from dbo.tab_notificacao_ocorrencias
				where  noc_id= @noc_id ;

-- #########################################################################
set nocount off;
				update dbo.tab_notificacao_ocorrencias
				    set 
						noc_data_notificacao = @data_notificacao, 
						noc_responsavel_notificacao = @responsavel_notificacao, 
						noc_descricao_ocorrencia = @descricao_ocorrencia, 
						noc_solicitante = @solicitante, 
						noc_solicitante_data = @solicitante_data, 
						noc_responsavel_recebimento = @responsavel_recebimento, 
						noc_responsavel_recebimento_data = @responsavel_recebimento_data, 
						noc_atualizado_por = @usu_id,
						noc_data_atualizacao = @actionDate 
					where noc_id= @noc_id;


-- ********* INSERÇÃO DE LOG - continuacao ****************************
set nocount on;
				declare @tabela varchar(300) = 'tab_notificacao_ocorrencias';
				declare @tra_id int = 6; -- tra_id= 6 ==> alteração
				declare @mod_id_log int = -541; 

				-- cria e insere dados NEW na tabela #tmpComparar
				insert into  #tmpComparar 
				SELECT 2 as nRow, *  
				from dbo.tab_notificacao_ocorrencias
				where  noc_id= @noc_id ;


				-- compara as linhas e retorna em varchar
				declare @log_texto varchar(MAX); 
				exec dbo.STP_COMPARA_COLS_ANTES_DEPOIS  @tabela, @retorno = @log_texto output;

				-- exclui a temporaria
				DROP TABLE #tmpComparar;

				exec dbo.STP_INS_LOGSISTEMA @tra_id, @usu_id, @mod_id_log,	@log_texto,	@ip			

set nocount off;

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
