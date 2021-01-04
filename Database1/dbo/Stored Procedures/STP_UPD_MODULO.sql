CREATE procedure [dbo].[STP_UPD_MODULO] 
@mod_id int, 
@mod_nome_modulo nVARCHAR(100), 
@mod_descricao nVARCHAR(255), 
@mod_ativo int,
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
				from dbo.tab_modulos
				where  mod_id= @mod_id ;

-- #########################################################################

				update dbo.tab_modulos
				    set 
						mod_nome_modulo=@mod_nome_modulo,
						mod_descricao=@mod_descricao,
						mod_ativo = @mod_ativo,
						mod_atualizado_por = @usu_id,
						mod_data_atualizacao = @actionDate 
					where mod_id= @mod_id ;


-- ********* INSERÇÃO DE LOG - continuacao ****************************

				declare @tabela varchar(300) = 'tab_modulos';
				declare @tra_id int = 6;
				declare @mod_id_log int = 1050; 

				-- cria e insere dados NEW na tabela #tmpComparar
				insert into  #tmpComparar 
				SELECT 2 as nRow, * 
				from dbo.tab_modulos
				where  mod_id= @mod_id ;

				-- compara as linhas e retorna em varchar
				declare @log_texto varchar(MAX); 
				exec dbo.STP_COMPARA_COLS_ANTES_DEPOIS  @tabela, @retorno = @log_texto output;

				-- exclui a temporaria
				DROP TABLE #tmpComparar;

				set nocount off;
				exec dbo.STP_INS_LOGSISTEMA @tra_id, @usu_id, @mod_id_log,	@log_texto,	@ip			

		COMMIT TRAN T1

		return @mod_id
end try
begin catch
		ROLLBACK TRAN T1
            PRINT 'The following error has occurred:  ' + ERROR_MESSAGE()

		--Raise an error
		 DECLARE @ErrorMessage varchar(255), @ErrorSeverity int, @ErrorState int 
		 SELECT @ErrorMessage = ERROR_MESSAGE(), @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE() 
		 RAISERROR(@ErrorMessage,@ErrorSeverity,@ErrorState) 
end catch
