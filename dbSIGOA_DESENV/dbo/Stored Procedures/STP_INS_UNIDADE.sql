CREATE procedure [dbo].[STP_INS_UNIDADE] 
@uni_unidade nVARCHAR(10), 
@uni_descricao nVARCHAR(255), 
@usu_id int,
@unt_id int,
@ip nvarchar(30)

--with encryption
as

begin try
		BEGIN TRAN T1
				declare @actionDate datetime
				set @actionDate = getdate()

				declare @uni_id int
				set @uni_id = (select isnull(max(uni_id),0) +1 from dbo.tab_unidades_medida);
				
				-- insere novo grupo
				INSERT INTO dbo.tab_unidades_medida(uni_id, uni_unidade, uni_descricao, unt_id, uni_ativo, uni_data_criacao, uni_criado_por )
				    values (@uni_id, @uni_unidade, @uni_descricao, @unt_id, 1, @actionDate, @usu_id);
			

			-- ********* INSERÇÃO DE LOG **************************************

				declare @tabela varchar(300) = 'tab_unidades_medida';
				declare @tra_id int = 4; -- 4= insercao
				declare @mod_id_log int = 410; -- unidades

				-- checa se a tmp table existem e a exclui
				set nocount on;
				if OBJECT_ID('tempdb..#tmpTabela') is not null
					DROP TABLE #tmpTabela;

				-- insere dados NEW na tabela #tmpTabela
				SELECT * into #tmpTabela 
				from dbo.tab_unidades_medida
				where  uni_id= @uni_id ;


				-- concatena os valores e retorna em varchar
				declare @log_texto varchar(MAX); 
				exec dbo.STP_CONCATENA_COLS_VALS  @tabela, @retorno = @log_texto output;

				-- exclui a temporaria
				DROP TABLE #tmpTabela;

				set nocount off;
				exec dbo.STP_INS_LOGSISTEMA @tra_id, @usu_id, @mod_id_log,	@log_texto,	@ip			

			 -- ****************************************************************
		COMMIT TRAN T1

		return @uni_id
end try
begin catch
		ROLLBACK TRAN T1
            PRINT 'The following error has occurred:  ' + ERROR_MESSAGE()

		--Raise an error
		 DECLARE @ErrorMessage varchar(255), @ErrorSeverity int, @ErrorState int 
		 SELECT @ErrorMessage = ERROR_MESSAGE(), @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE() 
		 RAISERROR(@ErrorMessage,@ErrorSeverity,@ErrorState) 
end catch
