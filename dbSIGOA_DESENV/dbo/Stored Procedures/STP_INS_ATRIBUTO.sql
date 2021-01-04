CREATE procedure [dbo].[STP_INS_ATRIBUTO] 
@atr_id int,
@tip_id int,
@clo_id int, 
@atr_atributo_nome nvarchar(50),
@atr_descricao nVARCHAR(255), 
@atr_mascara_texto nvarchar(50) = null,
@atr_herdavel bit ,
@atr_ativo bit, 
@atr_atributo_funcional bit=0,
@atr_apresentacao_itens nvarchar(20) = null,
@usu_id int,
@ip nvarchar(30)

--with encryption
as
 
begin try
		BEGIN TRAN T1
				declare @actionDate datetime = getdate()

				declare @atr_id_new int = (select isnull(max(atr_id),0) +1 from dbo.tab_atributos);


				   -- cria novo registro
				   insert into  dbo.tab_atributos (atr_id, tip_id, clo_id, atr_atributo_nome, atr_descricao, atr_mascara_texto, atr_herdavel, atr_ativo, atr_apresentacao_itens, atr_criado_por, atr_data_criacao, atr_atributo_funcional )
				   values ( @atr_id_new, @tip_id, @clo_id, @atr_atributo_nome, @atr_descricao, @atr_mascara_texto, @atr_herdavel, @atr_ativo, @atr_apresentacao_itens, @usu_id, @actionDate, @atr_atributo_funcional );

				   -- expande para os itens
				   update dbo.tab_atributo_itens
				   set atr_id = @atr_id_new
				   where  atr_id = @atr_id;

				   -- apaga o original
				   delete from dbo.tab_atributos
				   where  atr_id = @atr_id;


			-- ********* INSERÇÃO DE LOG **************************************

				declare @tabela varchar(300) = 'tab_atributos';
				declare @tra_transacao_id int = 4; -- 4= insercao
				declare @mod_modulo_id_log int = 120; -- 120 = atributos fixos

				-- checa se a tmp table existem e a exclui
				set nocount on;
				if OBJECT_ID('tempdb..#tmpTabela') is not null
					DROP TABLE #tmpTabela;

				-- insere dados NEW na tabela #tmpTabela
				SELECT * into #tmpTabela 
				from dbo.tab_atributos
				where  atr_id= @atr_id_new ;


				-- concatena os valores e retorna em varchar
				declare @log_texto varchar(MAX); 
				exec dbo.STP_CONCATENA_COLS_VALS  @tabela, @retorno = @log_texto output, @tudo=0 ;

				-- exclui a temporaria
				DROP TABLE #tmpTabela;

				set nocount off;
				exec dbo.STP_INS_LOGSISTEMA @tra_transacao_id, @usu_id, @mod_modulo_id_log,	@log_texto,	@ip			

			 -- ****************************************************************
		COMMIT TRAN T1

		return @atr_id_new
end try
begin catch
		ROLLBACK TRAN T1
            PRINT 'The following error has occurred:  ' + ERROR_MESSAGE()

		--Raise an error
		 DECLARE @ErrorMessage varchar(255), @ErrorSeverity int, @ErrorState int 
		 SELECT @ErrorMessage = ERROR_MESSAGE(), @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE() 
		 RAISERROR(@ErrorMessage,@ErrorSeverity,@ErrorState) 
end catch
