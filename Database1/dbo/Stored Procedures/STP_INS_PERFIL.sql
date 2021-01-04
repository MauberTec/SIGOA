CREATE procedure [dbo].[STP_INS_PERFIL] 
@per_descricao nVARCHAR(255), 
@per_ativo int,
@usu_id int,
@ip nvarchar(30)

--with encryption
as

begin try
		BEGIN TRAN T1
				declare @actionDate datetime
				set @actionDate = getdate()

				declare @per_id int
				set @per_id = (select isnull(max(per_id),0) +1 from dbo.tab_perfis);
				
				-- insere novo perfil
				INSERT INTO dbo.tab_perfis(per_id, per_descricao, per_ativo, per_data_criacao, per_criado_por )
				    values (@per_id, @per_descricao, @per_ativo, @actionDate, @usu_id);

				-- associa modulos a esse perfil
				insert into dbo.tab_modulos_perfis (per_id, mod_id, mfl_leitura, mfl_escrita, mfl_excluir, mfl_inserir, mfl_data_criacao, mfl_criado_por)
				select @per_id, mod_id, 0, 0, 0, 0, @actionDate, @usu_id
				from dbo.tab_modulos;
				
		

			-- ********* INSERÇÃO DE LOG **************************************

				declare @tabela varchar(300) = 'tab_perfis';
				declare @tra_id int = 4; -- 4= insercao
				declare @mod_id_log int = 1060; -- 1060 = perfis

				-- checa se a tmp table existem e a exclui
				set nocount on;
				if OBJECT_ID('tempdb..#tmpTabela') is not null
					DROP TABLE #tmpTabela;

				-- insere dados NEW na tabela #tmpTabela
				SELECT * into #tmpTabela 
				from dbo.tab_perfis
				where  per_id= @per_id ;


				-- concatena os valores e retorna em varchar
				declare @log_texto varchar(MAX); 
				exec dbo.STP_CONCATENA_COLS_VALS  @tabela, @retorno = @log_texto output;

				-- exclui a temporaria
				DROP TABLE #tmpTabela;

				set nocount off;
				exec dbo.STP_INS_LOGSISTEMA @tra_id, @usu_id, @mod_id_log,	@log_texto,	@ip			

			 -- ****************************************************************

		COMMIT TRAN T1

		return @per_id
end try
begin catch
		ROLLBACK TRAN T1
            PRINT 'The following error has occurred:  ' + ERROR_MESSAGE()

		--Raise an error
		 DECLARE @ErrorMessage varchar(255), @ErrorSeverity int, @ErrorState int 
		 SELECT @ErrorMessage = ERROR_MESSAGE(), @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE() 
		 RAISERROR(@ErrorMessage,@ErrorSeverity,@ErrorState) 
end catch
