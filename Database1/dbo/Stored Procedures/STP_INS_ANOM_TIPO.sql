CREATE procedure [dbo].[STP_INS_ANOM_TIPO] 
@atp_codigo nVARCHAR(10), 
@atp_descricao nVARCHAR(255), 
@atp_ativo int,
@usu_id int,
@ip nvarchar(30)

--with encryption
as

begin try
		BEGIN TRAN T1
				declare @actionDate datetime
				set @actionDate = getdate()

				declare @atp_id int
				set @atp_id = (select isnull(max(atp_id),0) +1 from dbo.tab_anomalia_tipos);
				
				-- insere novo 
				INSERT INTO dbo.tab_anomalia_tipos (atp_id, atp_codigo, atp_descricao, atp_ativo, atp_criado_por, atp_data_criacao )
				    values (@atp_id, @atp_codigo, @atp_descricao, @atp_ativo, @usu_id, @actionDate);

	

			-- ********* INSERÇÃO DE LOG **************************************

				declare @tabela varchar(300) = 'tab_anomalia_tipos';
				declare @tra_transacao_id int = 4; -- 4= insercao
				declare @mod_modulo_id_log int = 220; -- tipo

				-- checa se a tmp table existem e a exclui
				set nocount on;
				if OBJECT_ID('tempdb..#tmpTabela') is not null
					DROP TABLE #tmpTabela;

				-- insere dados NEW na tabela #tmpTabela
				SELECT * into #tmpTabela 
				from dbo.tab_anomalia_tipos
				where  atp_id= @atp_id ;


				-- concatena os valores e retorna em varchar
				declare @log_texto varchar(MAX); 
				exec dbo.STP_CONCATENA_COLS_VALS  @tabela, @retorno = @log_texto output;

				-- exclui a temporaria
				DROP TABLE #tmpTabela;

				set nocount off;
				exec dbo.STP_INS_LOGSISTEMA @tra_transacao_id, @usu_id, @mod_modulo_id_log,	@log_texto,	@ip			

			 -- ****************************************************************
		COMMIT TRAN T1

		return @atp_id
end try
begin catch
		ROLLBACK TRAN T1
            PRINT 'The following error has occurred:  ' + ERROR_MESSAGE()

		--Raise an error
		 DECLARE @ErrorMessage varchar(255), @ErrorSeverity int, @ErrorState int 
		 SELECT @ErrorMessage = ERROR_MESSAGE(), @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE() 
		 RAISERROR(@ErrorMessage,@ErrorSeverity,@ErrorState) 
end catch
