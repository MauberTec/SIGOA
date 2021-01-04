CREATE procedure [dbo].[STP_INS_REPARO_TIPO] 
@rpt_codigo nVARCHAR(10), 
@rpt_descricao nVARCHAR(255), 
@rpt_ativo int,
@usu_id int,
@ip nvarchar(30)

--with encryption
as

begin try
		BEGIN TRAN T1
				declare @actionDate datetime
				set @actionDate = getdate()

				declare @rpt_id int
				set @rpt_id = (select isnull(max(rpt_id),0) +1 from dbo.tab_reparo_tipos);
				
				-- insere novo 
				INSERT INTO dbo.tab_reparo_tipos (rpt_id, rpt_codigo, rpt_descricao, rpt_ativo, rpt_criado_por, rpt_data_criacao )
				    values (@rpt_id, @rpt_codigo, @rpt_descricao, @rpt_ativo, @usu_id, @actionDate);

	

			-- ********* INSERÇÃO DE LOG **************************************

				declare @tabela varchar(300) = 'tab_reparo_tipos';
				declare @tra_transaleo_id int = 4; -- 4= insercao
				declare @mod_modulo_id_log int = 310; -- tipo

				-- checa se a tmp table existem e a exclui
				set nocount on;
				if OBJECT_ID('tempdb..#tmpTabela') is not null
					DROP TABLE #tmpTabela;

				-- insere dados NEW na tabela #tmpTabela
				SELECT * into #tmpTabela 
				from dbo.tab_reparo_tipos
				where  rpt_id= @rpt_id ;


				-- concatena os valores e retorna em varchar
				declare @log_texto varchar(MAX); 
				exec dbo.STP_CONCATENA_COLS_VALS  @tabela, @retorno = @log_texto output;

				-- exclui a temporaria
				DROP TABLE #tmpTabela;

				set nocount off;
				exec dbo.STP_INS_LOGSISTEMA @tra_transaleo_id, @usu_id, @mod_modulo_id_log,	@log_texto,	@ip			

			 -- ****************************************************************
		COMMIT TRAN T1

		return @rpt_id
end try
begin catch
		ROLLBACK TRAN T1
            PRINT 'The following error has occurred:  ' + ERROR_MESSAGE()

		--Raise an error
		 DECLARE @ErrorMessage varchar(255), @ErrorSeverity int, @ErrorState int 
		 SELECT @ErrorMessage = ERROR_MESSAGE(), @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE() 
		 RAISERROR(@ErrorMessage,@ErrorSeverity,@ErrorState) 
end catch
