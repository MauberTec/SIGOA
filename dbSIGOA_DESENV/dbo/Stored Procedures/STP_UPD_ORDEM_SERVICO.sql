CREATE procedure [dbo].[STP_UPD_ORDEM_SERVICO] 
	@ord_id bigint,
	@ord_codigo nvarchar(200),
	@ord_descricao nvarchar(255),
	@ord_pai bigint  = null,
	@ocl_id int  = null,
	@tos_id int  = null,
	@sos_id int  = null,
	@obj_id bigint  = null,
	@ord_ativo bit,
	@ord_deletado datetime  = null,
	@ord_criticidade int  = null,
	@con_id bigint  = null,
	@ord_data_inicio_programada date  = null,
	@ord_data_termino_programada date  = null,
	@ord_data_inicio_execucao date  = null,
	@ord_data_termino_execucao date  = null,
	@ord_quantidade_estimada float  = null,
	@uni_id_qt_estimada int  = null,
	@ord_quantidade_executada float  = null,
	@uni_id_qt_executada int  = null,
	@ord_custo_estimado float  = null,
	@ord_custo_final float  = null,
	@ord_aberta_por int  = null,
	@ord_data_abertura datetime  = null,
	@ord_responsavel_der nchar(100)  = null,
	@ord_responsavel_fiscalizacao nchar(100)  = null,
	@con_id_fiscalizacao int  = null,
	@ord_responsavel_execucao nchar(100)  = null,
	@con_id_execucao int  = null,
	@ord_responsavel_suspensao nchar(100)  = null,
	@ord_data_suspensao date  = null,
	@ord_responsavel_cancelamento nchar(100)  = null,
	@ord_data_cancelamento date  = null,
	@ord_data_reinicio date  = null,
	@con_id_orcamento bigint  = null,
	@tpt_id char  = null,
	@tpu_data_base_der date  = null,
	@tpu_id int  = null,
	@tpu_preco_unitario real  = null,
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
				from dbo.tab_ordens_servico
				where  ord_id= @ord_id ;

-- #########################################################################
set nocount off;
		if @ocl_id = 0
		 set @ocl_id = null;

		declare @sos_id_atual int = (select sos_id from dbo.tab_ordens_servico where ord_id= @ord_id);
		--if @sos_id <> @sos_id_atual
		--begin
			
		--end


				update dbo.tab_ordens_servico
				    set 
						  ord_codigo = @ord_codigo, 
						  ord_descricao = @ord_descricao,
						  ord_pai = @ord_pai, 
						  ocl_id = @ocl_id, 
						  tos_id = @tos_id, 
						  sos_id = @sos_id, 
						  obj_id = @obj_id, 
					--      ord_ativo = @ord_ativo, 
						  ord_data_atualizacao = @actionDate, 
						  ord_atualizado_por = @usu_id, 
						  ord_criticidade = @ord_criticidade, 
						  con_id = @con_id, 
						  ord_data_inicio_programada = ord_data_inicio_programada, 
						  ord_data_termino_programada = @ord_data_termino_programada, 

						  ord_data_inicio_execucao = @ord_data_inicio_execucao, 
						  ord_data_termino_execucao = @ord_data_termino_execucao, 

						  ord_quantidade_estimada = @ord_quantidade_estimada, 
						  uni_id_qt_estimada = @uni_id_qt_estimada, 
						  ord_quantidade_executada = @ord_quantidade_executada, 
						  uni_id_qt_executada = @uni_id_qt_executada, 
						  ord_custo_estimado = @ord_custo_estimado, 
						  ord_custo_final = @ord_custo_final, 
						  --ord_aberta_por = @ord_aberta_por, 
						  --ord_data_abertura = @ord_data_abertura, 
						  ord_responsavel_der = @ord_responsavel_der, 
						  ord_responsavel_fiscalizacao = @ord_responsavel_fiscalizacao, 
						  con_id_fiscalizacao = @con_id_fiscalizacao, 
						  ord_responsavel_execucao = @ord_responsavel_execucao, 
						  con_id_execucao = @con_id_execucao, 

						  ord_responsavel_suspensao = @ord_responsavel_suspensao, 
						  ord_data_suspensao = @ord_data_suspensao, 

						  ord_responsavel_cancelamento = @ord_responsavel_cancelamento, 
						  ord_data_cancelamento = @ord_data_cancelamento, 

						  ord_data_reinicio = @ord_data_reinicio, 

						  con_id_orcamento = @con_id_orcamento, 
						  tpt_id = @tpt_id, 
						  tpu_data_base_der = @tpu_data_base_der, 
						  tpu_id = @tpu_id, 
						  tpu_preco_unitario = @tpu_preco_unitario
					where ord_id= @ord_id ;


-- ********* INSERÇÃO DE LOG - continuacao ****************************
set nocount on;
				declare @tabela varchar(300) = 'tab_ordens_servico';
				declare @tra_id int = 6; -- tra_id= 6 ==> alteração
				declare @mod_id_log int = 540; -- tab_ordens_servico

				-- cria e insere dados NEW na tabela #tmpComparar
				insert into  #tmpComparar 
				SELECT 2 as nRow, *  
				from dbo.tab_ordens_servico
				where  ord_id= @ord_id ;


				-- compara as linhas e retorna em varchar
				declare @log_texto varchar(MAX); 
				exec dbo.STP_COMPARA_COLS_ANTES_DEPOIS  @tabela, @retorno = @log_texto output;

				-- exclui a temporaria
				DROP TABLE #tmpComparar;

				exec dbo.STP_INS_LOGSISTEMA @tra_id, @usu_id, @mod_id_log,	@log_texto,	@ip			

set nocount off;

		COMMIT TRAN T2

		return @ord_id;
end try
begin catch
		ROLLBACK TRAN T2
            PRINT 'The following error has occurred:  ' + ERROR_MESSAGE()

		--Raise an error
		 DECLARE @ErrorMessage varchar(255), @ErrorSeverity int, @ErrorState int 
		 SELECT @ErrorMessage = ERROR_MESSAGE(), @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE() 
		 RAISERROR(@ErrorMessage,@ErrorSeverity,@ErrorState) 
end catch
