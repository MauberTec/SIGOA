﻿CREATE procedure [dbo].[STP_INS_OS_CLASSE] 
@ocl_codigo nVARCHAR(10), 
@ocl_descricao nVARCHAR(255), 
@ocl_ativo int,
@usu_id int,
@ip nvarchar(30)

--with encryption
as

begin try
		BEGIN TRAN T1
				declare @actionDate datetime
				set @actionDate = getdate()

				declare @ocl_id int
				set @ocl_id = (select isnull(max(ocl_id),0) +1 from dbo.tab_ordem_servico_classes);
				
				-- insere novo grupo
				INSERT INTO dbo.tab_ordem_servico_classes(ocl_id, ocl_codigo, ocl_descricao, ocl_ativo, ocl_data_criacao, ocl_criado_por )
				    values (@ocl_id, @ocl_codigo, @ocl_descricao, @ocl_ativo, @actionDate, @usu_id);
			

			-- ********* INSERÇÃO DE LOG **************************************

				declare @tabela varchar(300) = 'tab_ordem_servico_classes';
				declare @tra_id int = 4; -- 4= insercao
				declare @mod_id_log int = 510; -- tab_ordem_servico_classes

				-- checa se a tmp table existem e a exclui
				set nocount on;
				if OBJECT_ID('tempdb..#tmpTabela') is not null
					DROP TABLE #tmpTabela;

				-- insere dados NEW na tabela #tmpTabela
				SELECT * into #tmpTabela 
				from dbo.tab_ordem_servico_classes
				where  ocl_id= @ocl_id ;


				-- concatena os valores e retorna em varchar
				declare @log_texto varchar(MAX); 
				exec dbo.STP_CONCATENA_COLS_VALS  @tabela, @retorno = @log_texto output;

				-- exclui a temporaria
				DROP TABLE #tmpTabela;

				set nocount off;
				exec dbo.STP_INS_LOGSISTEMA @tra_id, @usu_id, @mod_id_log,	@log_texto,	@ip			

			 -- ****************************************************************
		COMMIT TRAN T1

		return @ocl_id
end try
begin catch
		ROLLBACK TRAN T1
            PRINT 'The following error has occurred:  ' + ERROR_MESSAGE()

		--Raise an error
		 DECLARE @ErrorMessage varchar(255), @ErrorSeverity int, @ErrorState int 
		 SELECT @ErrorMessage = ERROR_MESSAGE(), @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE() 
		 RAISERROR(@ErrorMessage,@ErrorSeverity,@ErrorState) 
end catch
