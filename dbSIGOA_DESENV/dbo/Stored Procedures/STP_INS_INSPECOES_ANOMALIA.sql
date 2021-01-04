CREATE procedure [dbo].[STP_INS_INSPECOES_ANOMALIA] 
	@obj_id bigint ,
	@ins_id bigint ,
	@usu_id int,
	@ip nvarchar(30)


--with encryption
as

begin try
		BEGIN TRAN T1
				declare @actionDate datetime
				set @actionDate = getdate()

				declare @ian_id int
				set @ian_id = (select isnull(max(ian_id),0) +1 from dbo.tab_inspecoes_anomalias);
				
				INSERT into dbo.tab_inspecoes_anomalias
						   (ian_id
						   ,obj_id
						   ,ins_id
						   ,ian_numero
						   ,atp_id
						   ,ian_sigla
						   ,ale_id
						   ,ian_quantidade
						   ,ian_espacamento
						   ,ian_largura
						   ,ian_comprimento
						   ,ian_abertura_minima
						   ,ian_abertura_maxima
						   ,aca_id
						   ,ian_fotografia
						   ,ian_desenho
						   ,leg_id
						   ,ian_ativo
						   ,ian_data_criacao
						   ,ian_criado_por)
					 VALUES
						   (@ian_id
						   , @obj_id
						   , @ins_id
						   , -1
						   , 1
						   , ''
						   , -1
						   , -1
						   , -1
						   , -1
						   , -1
						   , -1
						   , -1
						   , -1
						   , -1
						   , ''
						   , -1
						   , 1
						   , @actionDate
						   , @usu_id );

			

			-- ********* INSERÇÃO DE LOG **************************************

				declare @tabela varchar(300) = 'tab_inspecoes_anomalias';
				declare @tra_id int = 4; -- 4= insercao
				declare @mod_id_log int = -201; -- tab_inspecoes_anomalias

				-- checa se a tmp table existem e a exclui
				set nocount on;
				if OBJECT_ID('tempdb..#tmpTabela') is not null
					DROP TABLE #tmpTabela;

				-- insere dados NEW na tabela #tmpTabela
				SELECT * into #tmpTabela 
				from dbo.tab_inspecoes_anomalias
				where  ian_id= @ian_id ;


				-- concatena os valores e retorna em varchar
				declare @log_texto varchar(MAX); 
				exec dbo.STP_CONCATENA_COLS_VALS  @tabela, @retorno = @log_texto output;

				-- exclui a temporaria
				DROP TABLE #tmpTabela;

				set nocount off;
				exec dbo.STP_INS_LOGSISTEMA @tra_id, @usu_id, @mod_id_log,	@log_texto,	@ip			

			 -- ****************************************************************
		COMMIT TRAN T1

		return @ian_id
end try
begin catch
		ROLLBACK TRAN T1
            PRINT 'The following error has occurred:  ' + ERROR_MESSAGE()

		--Raise an error
		 DECLARE @ErrorMessage varchar(255), @ErrorSeverity int, @ErrorState int 
		 SELECT @ErrorMessage = ERROR_MESSAGE(), @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE() 
		 RAISERROR(@ErrorMessage,@ErrorSeverity,@ErrorState) 
end catch
