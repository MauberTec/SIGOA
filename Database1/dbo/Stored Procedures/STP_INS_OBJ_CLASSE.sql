﻿CREATE procedure [dbo].[STP_INS_OBJ_CLASSE] 
@clo_nome nVARCHAR(50), 
@clo_descricao nVARCHAR(255), 
@clo_ativo int,
@usu_id int,
@ip nvarchar(30)

--with encryption
as

begin try
		BEGIN TRAN T1
				declare @actionDate datetime
				set @actionDate = getdate()

				declare @clo_id int
				set @clo_id = (select isnull(max(clo_id),0) +1 from dbo.tab_objeto_classes);
				
				-- insere novo 
				INSERT INTO dbo.tab_objeto_classes (clo_id, clo_nome, clo_descricao, clo_ativo, clo_criado_por, clo_data_criacao )
				    values (@clo_id, @clo_nome, @clo_descricao, @clo_ativo, @usu_id, @actionDate);

	

			-- ********* INSERÇÃO DE LOG **************************************

				declare @tabela varchar(300) = 'tab_objeto_classes';
				declare @tra_transacao_id int = 4; -- 4= insercao
				declare @mod_modulo_id_log int = 110; -- 110 = tipos/classes de objeto

				-- checa se a tmp table existem e a exclui
				set nocount on;
				if OBJECT_ID('tempdb..#tmpTabela') is not null
					DROP TABLE #tmpTabela;

				-- insere dados NEW na tabela #tmpTabela
				SELECT * into #tmpTabela 
				from dbo.tab_objeto_classes
				where  clo_id= @clo_id ;


				-- concatena os valores e retorna em varchar
				declare @log_texto varchar(MAX); 
				exec dbo.STP_CONCATENA_COLS_VALS  @tabela, @retorno = @log_texto output;

				-- exclui a temporaria
				DROP TABLE #tmpTabela;

				set nocount off;
				exec dbo.STP_INS_LOGSISTEMA @tra_transacao_id, @usu_id, @mod_modulo_id_log,	@log_texto,	@ip			

			 -- ****************************************************************
		COMMIT TRAN T1

		return @clo_id
end try
begin catch
		ROLLBACK TRAN T1
            PRINT 'The following error has occurred:  ' + ERROR_MESSAGE()

		--Raise an error
		 DECLARE @ErrorMessage varchar(255), @ErrorSeverity int, @ErrorState int 
		 SELECT @ErrorMessage = ERROR_MESSAGE(), @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE() 
		 RAISERROR(@ErrorMessage,@ErrorSeverity,@ErrorState) 
end catch
