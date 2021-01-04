create procedure [dbo].[STP_UPD_ANOM_ALERTA] 
@ale_id int,
@ale_codigo nvarchar(10), 
@ale_descricao nVARCHAR(255), 
@ale_ativo int,
@usu_id int,
@ip nvarchar(30)

--with encryption
as

begin try
		BEGIN TRAN T2
				declare @actionDate datetime
				set @actionDate = getdate()

-- ********* INSERÇÃO DE LOG - parte 1 ****************************

				-- checa se as tmp tables existem e as exclui
set nocount on;
				if OBJECT_ID('tempdb..#tmpComparar') is not null
				DROP TABLE #tmpComparar;

				-- cria e insere dados OLD na tabela #tmpComparar
				SELECT 1 as nRow, * into #tmpComparar 
				from dbo.tab_anomalia_alertas
				where  ale_id= @ale_id ;

-- #########################################################################
set nocount off;
				update dbo.tab_anomalia_alertas
				    set ale_codigo = @ale_codigo,
						ale_descricao = @ale_descricao,
						ale_ativo = @ale_ativo,
						ale_atualizado_por = @usu_id,
						ale_data_atualizacao = @actionDate 
					where ale_id= @ale_id ;


-- ********* INSERÇÃO DE LOG - continualeo ****************************
set nocount on;
				declare @tabela varchar(300) = 'tab_anomalia_alertas';
				declare @tra_id int = 6; -- tra_id= 6 ==> alteração
				declare @mod_id_log int = 240; -- alerta

				-- cria e insere dados NEW na tabela #tmpComparar
				insert into  #tmpComparar 
				SELECT 2 as nRow, *  
				from dbo.tab_anomalia_alertas
				where  ale_id= @ale_id ;


				-- compara as linhas e retorna em varchar
				declare @log_texto varchar(MAX); 
				exec dbo.STP_COMPARA_COLS_ANTES_DEPOIS  @tabela, @retorno = @log_texto output;

				-- exclui a temporaria
				DROP TABLE #tmpComparar;

				exec dbo.STP_INS_LOGSISTEMA @tra_id, @usu_id, @mod_id_log,	@log_texto,	@ip			

set nocount off;

		COMMIT TRAN T2

		return @ale_id;
end try
begin catch
		ROLLBACK TRAN T2
            PRINT 'The following error has occurred:  ' + ERROR_MESSAGE()

		--Raise an error
		 DECLARE @ErrorMessage varchar(255), @ErrorSeverity int, @ErrorState int 
		 SELECT @ErrorMessage = ERROR_MESSAGE(), @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE() 
		 RAISERROR(@ErrorMessage,@ErrorSeverity,@ErrorState) 
end catch
