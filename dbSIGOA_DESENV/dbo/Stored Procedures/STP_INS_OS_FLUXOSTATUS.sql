create procedure [dbo].[STP_INS_OS_FLUXOSTATUS]
@sos_id_de int, 
@sos_id_para int, 
@fos_descricao nVARCHAR(255), 
@fos_ativo int,
@usu_id int,
@ip nvarchar(30)

--with encryption
as

begin try
		BEGIN TRAN T1
				declare @actionDate datetime
				set @actionDate = getdate()

				declare @fos_id int
				set @fos_id = (select isnull(max(fos_id),0) +1 from dbo.tab_ordem_servico_fluxos_status);

				-- insere novo status
				INSERT INTO dbo.tab_ordem_servico_fluxos_status(fos_id, sos_id_de, sos_id_para, fos_descricao, fos_ativo, fos_data_criacao, fos_criado_por )
				    values (@fos_id, @sos_id_de, @sos_id_para, @fos_descricao, @fos_ativo, @actionDate, @usu_id);
			

			-- ********* INSERÇÃO DE LOG **************************************

				declare @tabela varchar(300) = 'tab_ordem_servico_fluxos_status';
				declare @tra_id int = 4; -- 4= insercao
				declare @mod_id_log int = 530; -- FLUXO DE status de OS

				-- checa se a tmp table existem e a exclui
				set nocount on;
				if OBJECT_ID('tempdb..#tmpTabela') is not null
					DROP TABLE #tmpTabela;

				-- insere dados NEW na tabela #tmpTabela
				SELECT * into #tmpTabela 
				from dbo.tab_ordem_servico_fluxos_status
				where  fos_id= @fos_id ;


				-- concatena os valores e retorna em varchar
				declare @log_texto varchar(MAX); 
				exec dbo.STP_CONCATENA_COLS_VALS  @tabela, @retorno = @log_texto output;

				-- exclui a temporaria
				DROP TABLE #tmpTabela;

				set nocount off;
				exec dbo.STP_INS_LOGSISTEMA @tra_id, @usu_id, @mod_id_log,	@log_texto,	@ip			

			 -- ****************************************************************
		COMMIT TRAN T1

		return @fos_id;
end try
begin catch
		ROLLBACK TRAN T1
            PRINT 'The following error has occurred:  ' + ERROR_MESSAGE()

		--Raise an error
		 DECLARE @ErrorMessage varchar(255), @ErrorSeverity int, @ErrorState int 
		 SELECT @ErrorMessage = ERROR_MESSAGE(), @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE() 
		 RAISERROR(@ErrorMessage,@ErrorSeverity,@ErrorState) 
end catch
