CREATE procedure [dbo].[STP_UPD_ATRIBUTO_VALOR] 
@obj_id int,
@atr_id int,
@ati_id int = null,
@atv_valor varchar(max) = null,
@uni_id int = null,
@usu_id int,
@ip nvarchar(30)

--with encryption
as

declare @tem int = (select count(*) 
					from dbo.tab_objeto_atributos_valores
					where atv_deletado is null
					and obj_id = @obj_id
					and atr_id = @atr_id
					);

if ( @tem = 0 )
begin
	exec dbo.STP_INS_ATRIBUTO_VALORES 
											@obj_id,
											@atr_id,
											@ati_id,
											@atv_valor,
											@uni_id,
											@usu_id,
											@ip
end
else
begin try

		BEGIN TRAN T1
				declare @actionDate datetime
				set @actionDate = getdate()

-- ********* INSERÇÃO DE LOG - parte 1 ****************************

				-- checa se as tmp tables existem e as exclui
set nocount on;
				if OBJECT_ID('tempdb..#tmpComparar') is not null
				DROP TABLE #tmpComparar;

				-- cria e insere dados OLD na tabela #tmpComparar
				SELECT 1 as nRow, obj_id, atr_id, ati_id, atv_valor, uni_id, atv_ativo, atv_deletado, atv_data_criacao, atv_criado_por, atv_data_atualizacao, atv_atualizado_por 
				into #tmpComparar 
				from dbo.tab_objeto_atributos_valores
				where atv_deletado is null
					and obj_id = @obj_id
					and atr_id = @atr_id;
-- #########################################################################
set nocount off;

		update dbo.tab_objeto_atributos_valores
		set 
			ati_id = @ati_id
			,atv_valor = @atv_valor
			,uni_id = @uni_id
			,atv_data_atualizacao = getdate()
			,atv_atualizado_por = @usu_id
		where atv_deletado is null
			and obj_id = @obj_id
			and atr_id = @atr_id;


-- ********* INSERÇÃO DE LOG - continuacao ****************************
set nocount on;
				declare @tabela varchar(300) = 'tab_objeto_atributos_valores';
				declare @tra_transacao_id int = 6; -- tra_transacao_id= 6 ==> alteração
				declare @mod_modulo_id_log int = -102; -- 102: atributo  valor

				-- cria e insere dados NEW na tabela #tmpComparar
				insert into  #tmpComparar 
				SELECT 2 as nRow, obj_id, atr_id, ati_id, atv_valor, uni_id, atv_ativo, atv_deletado, atv_data_criacao, atv_criado_por, atv_data_atualizacao, atv_atualizado_por  
				from dbo.tab_objeto_atributos_valores
				where atv_deletado is null
					and obj_id = @obj_id
					and atr_id = @atr_id;


				-- compara as linhas e retorna em varchar
				declare @log_texto varchar(MAX); 
				exec dbo.STP_COMPARA_COLS_ANTES_DEPOIS  @tabela, @retorno = @log_texto output;

				-- exclui a temporaria
				DROP TABLE #tmpComparar;

				exec dbo.STP_INS_LOGSISTEMA @tra_transacao_id, @usu_id, @mod_modulo_id_log,	@log_texto,	@ip			

set nocount off;

		COMMIT TRAN T1

		return @atr_id
end try
begin catch
		ROLLBACK TRAN T1
            PRINT 'The following error has occurred:  ' + ERROR_MESSAGE()

		--Raise an error
		 DECLARE @ErrorMessage varchar(255), @ErrorSeverity int, @ErrorState int 
		 SELECT @ErrorMessage = ERROR_MESSAGE(), @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE() 
		 RAISERROR(@ErrorMessage,@ErrorSeverity,@ErrorState) 
end catch
