CREATE procedure [dbo].[STP_INS_OBJ_TIPO] 
@clo_id int,
@tip_codigo nVARCHAR(15), 
@tip_nome nVARCHAR(50), 
@tip_descricao nVARCHAR(255), 
@tip_ativo int,
@usu_id int,
@ip nvarchar(30)

--with encryption
as

begin try
		BEGIN TRAN T1
				declare @actionDate datetime
				set @actionDate = getdate()

				declare @tip_id int
				set @tip_id = (select isnull(max(tip_id),0) +1 from dbo.tab_objeto_tipos);
				
				-- insere novo 
				INSERT INTO dbo.tab_objeto_tipos (tip_id, clo_id, tip_codigo, tip_nome, tip_descricao, tip_ativo, tip_criado_por, tip_data_criacao )
				    values (@tip_id, @clo_id, @tip_codigo, @tip_nome, @tip_descricao, @tip_ativo, @usu_id, @actionDate);

	

			-- ********* INSERÇÃO DE LOG **************************************

				declare @tabela varchar(300) = 'tab_objeto_tipos';
				declare @tra_transacao_id int = 4; -- 4= insercao
				declare @mod_modulo_id_log int = 110; -- 110 = tipos/classes de objeto

				-- checa se a tmp table existem e a exclui
				set nocount on;
				if OBJECT_ID('tempdb..#tmpTabela') is not null
					DROP TABLE #tmpTabela;

				-- insere dados NEW na tabela #tmpTabela
				SELECT * into #tmpTabela 
				from dbo.tab_objeto_tipos
				where  tip_id= @tip_id ;


				-- concatena os valores e retorna em varchar
				declare @log_texto varchar(MAX); 
				exec dbo.STP_CONCATENA_COLS_VALS  @tabela, @retorno = @log_texto output;

				-- exclui a temporaria
				DROP TABLE #tmpTabela;

				set nocount off;
				exec dbo.STP_INS_LOGSISTEMA @tra_transacao_id, @usu_id, @mod_modulo_id_log,	@log_texto,	@ip			

			 -- ****************************************************************
		COMMIT TRAN T1

		return @tip_id
end try
begin catch
		ROLLBACK TRAN T1
            PRINT 'The following error has occurred:  ' + ERROR_MESSAGE()

		--Raise an error
		 DECLARE @ErrorMessage varchar(255), @ErrorSeverity int, @ErrorState int 
		 SELECT @ErrorMessage = ERROR_MESSAGE(), @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE() 
		 RAISERROR(@ErrorMessage,@ErrorSeverity,@ErrorState) 
end catch
