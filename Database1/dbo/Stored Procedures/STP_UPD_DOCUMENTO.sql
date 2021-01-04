CREATE procedure [dbo].[STP_UPD_DOCUMENTO] 
@doc_id bigint,
@doc_codigo nvarchar(50), 
@doc_descricao nVARCHAR(255), 
@tpd_id nvarchar(3), 
@dcl_id int = null,
@doc_caminho varchar(max), 
@doc_ativo bit,
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
				from dbo.tab_documentos
				where  doc_id= @doc_id ;

-- #########################################################################
set nocount off;

				update dbo.tab_documentos
				    set 
						doc_id = @doc_id, 
						doc_codigo = @doc_codigo, 
						doc_descricao = @doc_descricao, 
						tpd_id = @tpd_id, 
						dcl_id = @dcl_id, 
						doc_caminho = @doc_caminho, 
						doc_ativo = @doc_ativo, 
						doc_data_atualizacao= @actionDate, 
						doc_atualizado_por = @usu_id
					where doc_id= @doc_id ;


-- ********* INSERÇÃO DE LOG - continuacao ****************************
set nocount on;
				declare @tabela varchar(300) = 'tab_documentos';
				declare @tra_id int = 6; -- tra_id= 6 ==> alteração
				declare @mod_id_log int = 710; -- 710 = Cadastro de Documentos

				-- cria e insere dados NEW na tabela #tmpComparar
				insert into  #tmpComparar 
				SELECT 2 as nRow, *  
				from dbo.tab_documentos
				where  doc_id= @doc_id ;


				-- compara as linhas e retorna em varchar
				declare @log_texto varchar(MAX); 
				exec dbo.STP_COMPARA_COLS_ANTES_DEPOIS  @tabela, @retorno = @log_texto output;

				-- exclui a temporaria
				DROP TABLE #tmpComparar;

				exec dbo.STP_INS_LOGSISTEMA @tra_id, @usu_id, @mod_id_log,	@log_texto,	@ip			

set nocount off;

		COMMIT TRAN T1

		return @doc_id;
end try
begin catch
		ROLLBACK TRAN T1
            PRINT 'The following error has occurred:  ' + ERROR_MESSAGE()

		--Raise an error
		 DECLARE @ErrorMessage varchar(255), @ErrorSeverity int, @ErrorState int 
		 SELECT @ErrorMessage = ERROR_MESSAGE(), @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE() 
		 RAISERROR(@ErrorMessage,@ErrorSeverity,@ErrorState) 
end catch
