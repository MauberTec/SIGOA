CREATE procedure [dbo].[STP_UPD_OBJ_TIPO] 
@tip_id int,
@clo_id int,
@tip_codigo nVARCHAR(15), 
@tip_nome nVARCHAR(50), 
@tip_descricao nVARCHAR(255), 
@tip_ativo int,
@usu_id int,
@ip nvarchar(30)

--with encryption
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
				from dbo.tab_objeto_tipos
				where  tip_id= @tip_id 
						and clo_id = @clo_id;

-- #########################################################################
set nocount off;
				update dbo.tab_objeto_tipos
				    set 
						tip_codigo = @tip_codigo,
						tip_nome = @tip_nome,
						tip_descricao = @tip_descricao,
						tip_ativo = @tip_ativo,
						tip_atualizado_por = @usu_id,
						tip_data_atualizacao = @actionDate 
					where  tip_id= @tip_id 
					and clo_id = @clo_id;


-- ********* INSERÇÃO DE LOG - continuacao ****************************
set nocount on;
				declare @tabela varchar(300) = 'tab_objeto_tipos';
				declare @tra_transacao_id int = 6; -- tra_transacao_id= 6 ==> alteração
				declare @mod_modulo_id_log int = 110; -- 110: tipos/classes de objeto

				-- cria e insere dados NEW na tabela #tmpComparar
				insert into  #tmpComparar 
				SELECT 2 as nRow, *  
				from dbo.tab_objeto_tipos
				where  tip_id= @tip_id 
				and clo_id = @clo_id;


				-- compara as linhas e retorna em varchar
				declare @log_texto varchar(MAX); 
				exec dbo.STP_COMPARA_COLS_ANTES_DEPOIS  @tabela, @retorno = @log_texto output;

				-- exclui a temporaria
				DROP TABLE #tmpComparar;

				exec dbo.STP_INS_LOGSISTEMA @tra_transacao_id, @usu_id, @mod_modulo_id_log,	@log_texto,	@ip			

set nocount off;

		COMMIT TRAN T1

		return @tip_id
end try
begin catch
		ROLLBACK TRAN T1
            PRINT 'The following error has occurred:  ' + ERROR_MESSAGE()

		--Raise an error
		 DECLARE @ErrorMessage varchar(255), @ErrorSeverity int, @ErrorState int 
		 SELECT @ErrorMessage = ERROR_MESSAGE(), @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE() 
		 RAISERROR(@ErrorMessage,@ErrorSeverity,@ErrorState) 
end catch
