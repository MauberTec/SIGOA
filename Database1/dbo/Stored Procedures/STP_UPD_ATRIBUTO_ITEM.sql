CREATE procedure [dbo].[STP_UPD_ATRIBUTO_ITEM] 
@ati_id int,
@atr_id int, 
@ati_item varchar(1000), 
@ati_ativo bit,
@atr_atributo_funcional bit = 0,
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
				from dbo.tab_atributo_itens
					where  ati_id = @ati_id ;

-- #########################################################################
set nocount off;
				update dbo.tab_atributo_itens
				    set 
					--	atr_id = @atr_id,
						ati_item = @ati_item, 
						ati_ativo = @ati_ativo,
						ati_atualizado_por = @usu_id,
						ati_data_atualizacao = @actionDate 
					 where  ati_id = @ati_id ;


-- ********* INSERÇÃO DE LOG - continuacao ****************************
set nocount on;
				declare @tabela varchar(300) = 'tab_atributo_itens';
				declare @tra_transacao_id int = 6; -- tra_transacao_id= 6 ==> alteração
				declare @mod_modulo_id_log int = -121; -- 121: atributos item

				-- cria e insere dados NEW na tabela #tmpComparar
				insert into  #tmpComparar 
				SELECT 2 as nRow, *  
				from dbo.tab_atributo_itens
					where  ati_id = @ati_id;


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
