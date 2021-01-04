CREATE procedure [dbo].[STP_INS_ORDEM_SERVICO_NOVA] 
	@ord_codigo nvarchar(200),
	@ord_descricao nvarchar(255),
	@tos_id int  = null,
	@obj_id bigint  = null,
	@ord_ativo bit,
	@ord_data_inicio_programada date  = null,
	@ord_aberta_por int  = null,
	@ord_data_abertura datetime  = null,
	@usu_id int,
	@ip nvarchar(30)


--with encryption
as

begin try
		BEGIN TRAN T1
				declare @actionDate datetime
				set @actionDate = getdate()

				declare @ord_id int
				set @ord_id = (select isnull(max(ord_id),0) +1 from dbo.tab_ordens_servico);
				
				declare @sos_id int = 1 -- status Solicitada

				declare @ord_sequencial_tipo bigint = isnull( (select max(ord_sequencial_tipo) from  dbo.tab_ordens_servico where tos_id = @tos_id ),0)+1;
				set @ord_codigo = (select tos_codigo from dbo.tab_ordem_servico_tipos where tos_id = @tos_id) + '-' + right('000000' + convert(varchar(6), @ord_sequencial_tipo),6);

				-- insere a O.S.
				INSERT INTO dbo.tab_ordens_servico( ord_id, ord_codigo, ord_sequencial_tipo, ord_descricao, tos_id, sos_id, obj_id, ord_ativo, ord_data_criacao, ord_criado_por, ord_data_inicio_programada, ord_aberta_por, ord_data_abertura )
				    values (@ord_id, @ord_codigo, @ord_sequencial_tipo, @ord_descricao, @tos_id, @sos_id, @obj_id, @ord_ativo,@actionDate, @usu_id, @ord_data_inicio_programada, @ord_aberta_por, @ord_data_abertura);
			
				-- se for O.S. de Inspecao, cria um registro na tabela de Inspecoes
				if (@tos_id <= 9) -- tos_id sincronizado com ipt_id
				begin
					declare @ins_id bigint = (select isnull(max(ins_id),0) +1 from dbo.tab_inspecoes);
					insert into dbo.tab_inspecoes (ins_id, ipt_id, obj_id, ord_id, ins_ativo, ins_data_criacao, ins_criado_por)
					values (@ins_id, @tos_id, @obj_id, @ord_id, 1, @actionDate, @usu_id );
				end


			-- ********* INSERÇÃO DE LOG **************************************

				declare @tabela varchar(300) = 'tab_ordens_servico';
				declare @tra_id int = 4; -- 4= insercao
				declare @mod_id_log int = 540; -- tab_ordens_servico

				-- checa se a tmp table existem e a exclui
				set nocount on;
				if OBJECT_ID('tempdb..#tmpTabela') is not null
					DROP TABLE #tmpTabela;

				-- insere dados NEW na tabela #tmpTabela
				SELECT * into #tmpTabela 
				from dbo.tab_ordens_servico
				where  ord_id= @ord_id ;


				-- concatena os valores e retorna em varchar
				declare @log_texto varchar(MAX); 
				exec dbo.STP_CONCATENA_COLS_VALS  @tabela, @retorno = @log_texto output;

				-- exclui a temporaria
				DROP TABLE #tmpTabela;

				set nocount off;
				exec dbo.STP_INS_LOGSISTEMA @tra_id, @usu_id, @mod_id_log,	@log_texto,	@ip			

			 -- ****************************************************************
		COMMIT TRAN T1

		return @ord_id
end try
begin catch
		ROLLBACK TRAN T1
            PRINT 'The following error has occurred:  ' + ERROR_MESSAGE()

		--Raise an error
		 DECLARE @ErrorMessage varchar(255), @ErrorSeverity int, @ErrorState int 
		 SELECT @ErrorMessage = ERROR_MESSAGE(), @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE() 
		 RAISERROR(@ErrorMessage,@ErrorSeverity,@ErrorState) 
end catch
