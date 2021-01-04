CREATE procedure [dbo].[STP_INS_OBJ_ATRIBUTO_VALORES] 
@obj_id int,
@atr_id int,
@ati_id int = null,
@atv_valor varchar(max) = null,
@uni_id int = null,
@usu_id int,
@ip nvarchar(30)

--with encryption
as
 
begin try
		BEGIN TRAN T1
				declare @actionDate datetime
				set @actionDate = getdate()

				declare @atv_id int
				set @atv_id = (select isnull(max(atv_id),0) +1 from dbo.tab_objeto_atributos_valores);
			
				-- insere novo 
				insert into dbo.tab_objeto_atributos_valores (atv_id, obj_id, atr_id, ati_id, atv_valor, uni_id, atv_ativo, atv_data_criacao, atv_criado_por )
				values (@atv_id, @obj_id, @atr_id, @ati_id, @atv_valor, @uni_id, 1, getdate(), @usu_id);
	

			-- ********* INSERÇÃO DE LOG **************************************

				declare @tabela varchar(300) = 'tab_objeto_atributos_valores';
				declare @tra_transacao_id int = 4; -- 4= insercao
				declare @mod_modulo_id_log int = -102; -- -102 = atributos valores

				-- checa se a tmp table existem e a exclui
				set nocount on;
				if OBJECT_ID('tempdb..#tmpTabela') is not null
					DROP TABLE #tmpTabela;

				-- insere dados NEW na tabela #tmpTabela
				SELECT  atv_id, obj_id, atr_id, ati_id, atv_valor, uni_id, atv_ativo, atv_deletado, atv_data_criacao, atv_criado_por, atv_data_atualizacao, atv_atualizado_por
				into #tmpTabela 
				from dbo.tab_objeto_atributos_valores
				where atv_id = @atv_id;


				-- concatena os valores e retorna em varchar
				declare @log_texto varchar(MAX); 
				exec dbo.STP_CONCATENA_COLS_VALS  @tabela, @retorno = @log_texto output, @tudo=0 ;

				-- exclui a temporaria
				DROP TABLE #tmpTabela;

				set nocount off;
				exec dbo.STP_INS_LOGSISTEMA @tra_transacao_id, @usu_id, @mod_modulo_id_log,	@log_texto,	@ip			

			 -- ****************************************************************
		COMMIT TRAN T1

		return @ati_id
end try
begin catch
		ROLLBACK TRAN T1
            PRINT 'The following error has occurred:  ' + ERROR_MESSAGE()

		--Raise an error
		 DECLARE @ErrorMessage varchar(255), @ErrorSeverity int, @ErrorState int 
		 SELECT @ErrorMessage = ERROR_MESSAGE(), @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE() 
		 RAISERROR(@ErrorMessage,@ErrorSeverity,@ErrorState) 
end catch
