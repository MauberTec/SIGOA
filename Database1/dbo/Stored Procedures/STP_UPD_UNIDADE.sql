CREATE procedure [dbo].[STP_UPD_UNIDADE] 
@uni_id int, 
@uni_unidade nVARCHAR(10), 
@uni_descricao nVARCHAR(255), 
@unt_id int,
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
				from dbo.tab_unidades_medida
				where  uni_id= @uni_id ;

-- #########################################################################
set nocount off;
				update dbo.tab_unidades_medida
				    set 
						uni_unidade = @uni_unidade,
						uni_descricao = @uni_descricao,
						unt_id = @unt_id,
						uni_atualizado_por = @usu_id,
						uni_data_atualizacao = @actionDate 
					where uni_id= @uni_id ;


-- ********* INSERÇÃO DE LOG - continuacao ****************************
set nocount on;
				declare @tabela varchar(300) = 'tab_unidades_medida';
				declare @tra_id int = 6; -- tra_id= 6 ==> alteração
				declare @mod_id_log int = 410; -- unidade

				-- cria e insere dados NEW na tabela #tmpComparar
				insert into  #tmpComparar 
				SELECT 2 as nRow, *  
				from dbo.tab_unidades_medida
				where  uni_id= @uni_id ;


				-- compara as linhas e retorna em varchar
				declare @log_texto varchar(MAX); 
				exec dbo.STP_COMPARA_COLS_ANTES_DEPOIS  @tabela, @retorno = @log_texto output;

				-- exclui a temporaria
				DROP TABLE #tmpComparar;

				exec dbo.STP_INS_LOGSISTEMA @tra_id, @usu_id, @mod_id_log,	@log_texto,	@ip			

set nocount off;

		COMMIT TRAN T1

		return @uni_id
end try
begin catch
		ROLLBACK TRAN T1
            PRINT 'The following error has occurred:  ' + ERROR_MESSAGE()

		--Raise an error
		 DECLARE @ErrorMessage varchar(255), @ErrorSeverity int, @ErrorState int 
		 SELECT @ErrorMessage = ERROR_MESSAGE(), @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE() 
		 RAISERROR(@ErrorMessage,@ErrorSeverity,@ErrorState) 
end catch
