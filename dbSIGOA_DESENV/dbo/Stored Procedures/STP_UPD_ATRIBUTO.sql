CREATE procedure [dbo].[STP_UPD_ATRIBUTO] 
@atr_id int, 
@tip_id int,
@clo_id int, 
@atr_atributo_nome nvarchar(50),
@atr_descricao nVARCHAR(255), 
@atr_mascara_texto nvarchar(50) = null,
@atr_herdavel bit ,
@atr_ativo bit, 
@atr_atributo_funcional bit=0,
@atr_apresentacao_itens nvarchar(20) = null,
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
				from dbo.tab_atributos
					 where  atr_id= @atr_id ;

-- #########################################################################
set nocount off;
				update dbo.tab_atributos
				    set 
						atr_atributo_nome = @atr_atributo_nome,
						atr_descricao = @atr_descricao,
						atr_mascara_texto = @atr_mascara_texto,
						tip_id = @tip_id,
						clo_id = @clo_id, 
						atr_herdavel = @atr_herdavel,
						atr_ativo = @atr_ativo,
						atr_atributo_funcional = @atr_atributo_funcional,
						atr_apresentacao_itens = @atr_apresentacao_itens,
						atr_atualizado_por = @usu_id,
						atr_data_atualizacao = @actionDate 
					 where  atr_id= @atr_id ;


-- ********* INSERÇÃO DE LOG - continuacao ****************************
set nocount on;
				declare @tabela varchar(300) = 'tab_atributos';
				declare @tra_transacao_id int = 6; -- tra_transacao_id= 6 ==> alteração
				declare @mod_modulo_id_log int = 120; -- 120: atributos fixos

				-- cria e insere dados NEW na tabela #tmpComparar
				insert into  #tmpComparar 
				SELECT 2 as nRow, *  
				from dbo.tab_atributos
					 where  atr_id= @atr_id;


				-- compara as linhas e retorna em varchar
				declare @log_texto varchar(MAX); 
				exec dbo.STP_COMPARA_COLS_ANTES_DEPOIS  @tabela, @retorno = @log_texto output;

				-- exclui a temporaria
				DROP TABLE #tmpComparar;

				exec dbo.STP_INS_LOGSISTEMA @tra_transacao_id, @usu_id, @mod_modulo_id_log,	@log_texto,	@ip			

set nocount off;

		COMMIT TRAN T1

		return @atr_id
end try
begin catch
		ROLLBACK TRAN T1
            PRINT 'The following error has occurred:  ' + ERROR_MESSAGE()

		--Raise an error
		 DECLARE @ErrorMessage varchar(255), @ErrorSeverity int, @ErrorState int 
		 SELECT @ErrorMessage = ERROR_MESSAGE(), @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE() 
		 RAISERROR(@ErrorMessage,@ErrorSeverity,@ErrorState) 
end catch
