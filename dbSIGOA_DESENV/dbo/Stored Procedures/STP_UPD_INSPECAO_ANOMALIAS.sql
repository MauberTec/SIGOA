create procedure [dbo].[STP_UPD_INSPECAO_ANOMALIAS] 
@ian_id int,
@obj_id bigint,
@ins_id bigint,
@ian_numero int,
@atp_id int,
@ian_sigla nvarchar(2),
@ale_id int,
@ian_quantidade int,
@ian_espacamento int,
@ian_largura int,
@ian_comprimento int,
@ian_abertura_minima int,
@ian_abertura_maxima int,
@aca_id int,
@ian_fotografia varchar(255),
@ian_croqui varchar(255),
@ian_desenho nvarchar(255),
@ian_observacoes nvarchar(255),
@leg_id int,
@ian_ativo bit,
@rpt_id_sugerido int,
@rpt_id_adotado int,
@ian_quantidade_adotada real,
@ian_quantidade_sugerida real,
@usu_id int,
@ip nvarchar(30)

--with encryption
as

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
				SELECT 1 as nRow, * into #tmpComparar 
				from dbo.tab_inspecoes_anomalias
				where  ian_id= @ian_id ;

-- #########################################################################
set nocount off;

				update dbo.tab_inspecoes_anomalias 
				set
					obj_id = @obj_id
				   ,ins_id = @ins_id
				   ,ian_numero = @ian_numero
				   ,atp_id = @atp_id
				   ,ian_sigla = @ian_sigla
				   ,ale_id = @ale_id
				   ,ian_quantidade = @ian_quantidade
				   ,ian_espacamento = @ian_espacamento
				   ,ian_largura = @ian_largura
				   ,ian_comprimento = @ian_comprimento
				   ,ian_abertura_minima = @ian_abertura_minima
				   ,ian_abertura_maxima = @ian_abertura_maxima
				   ,aca_id = @aca_id
				   ,ian_fotografia = @ian_fotografia
				   ,ian_croqui = @ian_croqui
				   ,ian_desenho = @ian_desenho
				   ,ian_observacoes = @ian_observacoes
				   ,leg_id = @leg_id 
				   ,ian_ativo = @ian_ativo
				   ,ian_data_atualizacao = @actionDate
				   ,ian_atualizado_por = @usu_id
				   ,rpt_id_sugerido = @rpt_id_sugerido
				   ,rpt_id_adotado = @rpt_id_adotado
				   ,ian_quantidade_adotada = @ian_quantidade_adotada
				   ,ian_quantidade_sugerida = @ian_quantidade_sugerida
			 where ian_id = @ian_id;


-- ********* INSERÇÃO DE LOG - continuacao ****************************
set nocount on;
				declare @tabela varchar(300) = 'tab_inspecoes_anomalias';
				declare @tra_id int = 6; -- tra_id= 6 ==> alteração
				declare @mod_id_log int = 540; -- grupos

				-- cria e insere dados NEW na tabela #tmpComparar
				insert into  #tmpComparar 
				SELECT 2 as nRow, *  
				from dbo.tab_inspecoes_anomalias
				where  ian_id= @ian_id ;


				-- compara as linhas e retorna em varchar
				declare @log_texto varchar(MAX); 
				exec dbo.STP_COMPARA_COLS_ANTES_DEPOIS  @tabela, @retorno = @log_texto output;

				-- exclui a temporaria
				DROP TABLE #tmpComparar;

				exec dbo.STP_INS_LOGSISTEMA @tra_id, @usu_id, @mod_id_log,	@log_texto,	@ip			

set nocount off;

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
