CREATE procedure [dbo].[STP_INS_ATRIBUTO_ITEM] 
@atr_id int, 
@ati_item nvarchar(1000), 
@ati_ativo bit,
@atr_atributo_funcional bit = 0,
@usu_id int,
@ip nvarchar(30)

--with encryption
as
 
begin try
		BEGIN TRAN T1
				declare @actionDate datetime
				set @actionDate = getdate()

				if (@atr_id < 0)
				begin
				    declare @tem int = (select COUNT(*) from dbo.tab_atributos where atr_id = @atr_id);
					if (@tem =  0)
					begin
						set @atr_id = -(select isnull(max(atr_id),0) +1 from dbo.tab_atributos);
				
						-- insere novo com atr_id negativo
							insert into dbo.tab_atributos (atr_id, tip_id, clo_id, atr_atributo_nome, atr_descricao,  atr_herdavel, atr_ativo, atr_atributo_funcional, atr_criado_por, atr_data_criacao )
							values (@atr_id, -1, 3, '', '', 1, 1, @atr_atributo_funcional, @usu_id, @actionDate) ;
					end 
				end

				declare @ati_id int
				set @ati_id = (select isnull(max(ati_id),0) +1 from dbo.tab_atributo_itens);
				
				-- insere novo 
				insert into dbo.tab_atributo_itens ( ati_id, atr_id, ati_item, ati_ativo, ati_data_criacao, ati_criado_por )
				values (@ati_id, @atr_id, @ati_item, @ati_ativo, @actionDate,  @usu_id) ;
	

			-- ********* INSERÇÃO DE LOG **************************************

				declare @tabela varchar(300) = 'tab_atributo_itens';
				declare @tra_transacao_id int = 4; -- 4= insercao
				declare @mod_modulo_id_log int = -121; -- -121 = atributos item

				-- checa se a tmp table existem e a exclui
				set nocount on;
				if OBJECT_ID('tempdb..#tmpTabela') is not null
					DROP TABLE #tmpTabela;

				-- insere dados NEW na tabela #tmpTabela
				SELECT * into #tmpTabela 
				from dbo.tab_atributo_itens
				where  ati_id= @ati_id ;


				-- concatena os valores e retorna em varchar
				declare @log_texto varchar(MAX); 
				exec dbo.STP_CONCATENA_COLS_VALS  @tabela, @retorno = @log_texto output, @tudo=0 ;

				-- exclui a temporaria
				DROP TABLE #tmpTabela;

				set nocount off;
				if (@atr_id >= 0)
				  exec dbo.STP_INS_LOGSISTEMA @tra_transacao_id, @usu_id, @mod_modulo_id_log,	@log_texto,	@ip			

			 -- ****************************************************************
		COMMIT TRAN T1

		declare @retorno int = @ati_id;
		if (@atr_id < 0)
			set @retorno = @atr_id;

		return @retorno;
end try
begin catch
		ROLLBACK TRAN T1
            PRINT 'The following error has occurred:  ' + ERROR_MESSAGE()

		--Raise an error
		 DECLARE @ErrorMessage varchar(255), @ErrorSeverity int, @ErrorState int 
		 SELECT @ErrorMessage = ERROR_MESSAGE(), @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE() 
		 RAISERROR(@ErrorMessage,@ErrorSeverity,@ErrorState) 
end catch
