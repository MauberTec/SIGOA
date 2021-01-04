create procedure [dbo].[STP_UPD_OS_FLUXOSTATUS]
@fos_id int,
@sos_id_de int,
@sos_id_para int,
@fos_descricao nVARCHAR(255), 
@fos_ativo int,
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
				from dbo.tab_ordem_servico_fluxos_status
				where  fos_id= @fos_id ;

-- #########################################################################
set nocount off;
				update dbo.tab_ordem_servico_fluxos_status
				    set sos_id_de = @sos_id_de,
						sos_id_para = @sos_id_para,
						fos_descricao = @fos_descricao,
						fos_ativo = @fos_ativo,
						fos_atualizado_por = @usu_id,
						fos_data_atualizacao = @actionDate 
					where fos_id= @fos_id ;


-- ********* INSERÇÃO DE LOG - continuacao ****************************
set nocount on;
				declare @tabela varchar(300) = 'tab_ordem_servico_fluxos_status';
				declare @tra_id int = 6; -- tra_id= 6 ==> alteração
				declare @mod_id_log int = 530; -- fluxo de Status OS

				-- cria e insere dados NEW na tabela #tmpComparar
				insert into  #tmpComparar 
				SELECT 2 as nRow, *  
				from dbo.tab_ordem_servico_fluxos_status
				where  fos_id= @fos_id ;


				-- compara as linhas e retorna em varchar
				declare @log_texto varchar(MAX); 
				exec dbo.STP_COMPARA_COLS_ANTES_DEPOIS  @tabela, @retorno = @log_texto output;

				-- exclui a temporaria
				DROP TABLE #tmpComparar;

				exec dbo.STP_INS_LOGSISTEMA @tra_id, @usu_id, @mod_id_log,	@log_texto,	@ip			

set nocount off;

		COMMIT TRAN T2

		return @fos_id;
end try
begin catch
		ROLLBACK TRAN T2
            PRINT 'The following error has occurred:  ' + ERROR_MESSAGE()

		--Raise an error
		 DECLARE @ErrorMessage varchar(255), @ErrorSeverity int, @ErrorState int 
		 SELECT @ErrorMessage = ERROR_MESSAGE(), @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE() 
		 RAISERROR(@ErrorMessage,@ErrorSeverity,@ErrorState) 
end catch
