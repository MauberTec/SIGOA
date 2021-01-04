create procedure [dbo].[STP_INS_INSPECAO_TIPO] 
@ipt_codigo nVARCHAR(10), 
@ipt_descricao nVARCHAR(255), 
@ipt_ativo int,
@usu_id int,
@ip nvarchar(30)

--with encryption
as

begin try
		BEGIN TRAN T1
				declare @actionDate datetime
				set @actionDate = getdate()

				declare @ipt_id int
				set @ipt_id = (select isnull(max(ipt_id),0) +1 from dbo.tab_inspecao_tipos);
				
				-- insere novo 
				INSERT INTO dbo.tab_inspecao_tipos (ipt_id, ipt_codigo, ipt_descricao, ipt_ativo, ipt_criado_por, ipt_data_criacao )
				    values (@ipt_id, @ipt_codigo, @ipt_descricao, @ipt_ativo, @usu_id, @actionDate);

	

			-- ********* INSERÇÃO DE LOG **************************************

				declare @tabela varchar(300) = 'tab_inspecao_tipos';
				declare @tra_transaleo_id int = 4; -- 4= insercao
				declare @mod_modulo_id_log int = 610; -- tipo

				-- checa se a tmp table existem e a exclui
				set nocount on;
				if OBJECT_ID('tempdb..#tmpTabela') is not null
					DROP TABLE #tmpTabela;

				-- insere dados NEW na tabela #tmpTabela
				SELECT * into #tmpTabela 
				from dbo.tab_inspecao_tipos
				where  ipt_id= @ipt_id ;


				-- concatena os valores e retorna em varchar
				declare @log_texto varchar(MAX); 
				exec dbo.STP_CONCATENA_COLS_VALS  @tabela, @retorno = @log_texto output;

				-- exclui a temporaria
				DROP TABLE #tmpTabela;

				set nocount off;
				exec dbo.STP_INS_LOGSISTEMA @tra_transaleo_id, @usu_id, @mod_modulo_id_log,	@log_texto,	@ip			

			 -- ****************************************************************
		COMMIT TRAN T1

		return @ipt_id
end try
begin catch
		ROLLBACK TRAN T1
            PRINT 'The following error has occurred:  ' + ERROR_MESSAGE()

		--Raise an error
		 DECLARE @ErrorMessage varchar(255), @ErrorSeverity int, @ErrorState int 
		 SELECT @ErrorMessage = ERROR_MESSAGE(), @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE() 
		 RAISERROR(@ErrorMessage,@ErrorSeverity,@ErrorState) 
end catch
