CREATE procedure [dbo].[STP_INS_ANOM_CAUSA] 
@leg_id int, 
@leg_codigo nVARCHAR(255), 
@aca_descricao nVARCHAR(255), 
@aca_ativo int,
@usu_id int,
@ip nvarchar(30)

--with encryption
as

begin try
		BEGIN TRAN T1
				declare @actionDate datetime
				set @actionDate = getdate()

				declare @aca_id int
				set @aca_id = (select isnull(max(aca_id),0) +1 from dbo.tab_anomalia_causas);
				

				declare @aca_codigo  nVARCHAR(10)= (select max(convert(int,aca_codigo)) +1 from dbo.tab_anomalia_causas where leg_id = @leg_id);



				-- insere novo 
				INSERT INTO dbo.tab_anomalia_causas (aca_id, aca_codigo, aca_descricao, aca_ativo, aca_criado_por, aca_data_criacao, leg_id, leg_codigo )
				    values (@aca_id, @aca_codigo, @aca_descricao, @aca_ativo, @usu_id, @actionDate, @leg_id, @leg_codigo);

	

			-- ********* INSERÇÃO DE LOG **************************************

				declare @tabela varchar(300) = 'tab_anomalia_causas';
				declare @tra_transacao_id int = 4; -- 4= insercao
				declare @mod_modulo_id_log int = 230; -- causa

				-- checa se a tmp table existem e a exclui
				set nocount on;
				if OBJECT_ID('tempdb..#tmpTabela') is not null
					DROP TABLE #tmpTabela;

				-- insere dados NEW na tabela #tmpTabela
				SELECT * into #tmpTabela 
				from dbo.tab_anomalia_causas
				where  aca_id= @aca_id ;


				-- concatena os valores e retorna em varchar
				declare @log_texto varchar(MAX); 
				exec dbo.STP_CONCATENA_COLS_VALS  @tabela, @retorno = @log_texto output;

				-- exclui a temporaria
				DROP TABLE #tmpTabela;

				set nocount off;
				exec dbo.STP_INS_LOGSISTEMA @tra_transacao_id, @usu_id, @mod_modulo_id_log,	@log_texto,	@ip			

			 -- ****************************************************************
		COMMIT TRAN T1

		return @aca_id
end try
begin catch
		ROLLBACK TRAN T1
            PRINT 'The following error has occurred:  ' + ERROR_MESSAGE()

		--Raise an error
		 DECLARE @ErrorMessage varchar(255), @ErrorSeverity int, @ErrorState int 
		 SELECT @ErrorMessage = ERROR_MESSAGE(), @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE() 
		 RAISERROR(@ErrorMessage,@ErrorSeverity,@ErrorState) 
end catch
