CREATE procedure [dbo].[STP_DEL_ATRIBUTO]
@atr_id int, 
@usu_id int,
@ip nvarchar(30)


--with encryption
as

begin try
		declare @retorno int = 0;
					
		if (@atr_id < 0) -- atr_id < 0 significa que o usuario clicou em cancelar na tela de cadastro ==> apagar os temporarios 
		begin
			delete from dbo.tab_atributo_itens where atr_id = @atr_id;
			delete from dbo.tab_atributos where atr_id = @atr_id;
			set @retorno = 1;
		end
		else
		begin
			BEGIN TRAN T1
				-- concatena os itens apagados para colocar no Log
				declare @atr_itens_codigo varchar(MAX); 
				declare @atr_itens_descricao varchar(MAX); 
				declare @atr_itens_ids varchar(MAX); 
				declare @atr_itens varchar(MAX); 

				set @atr_itens_codigo = (select dbo.ConcatenarAtributoItens( @atr_id, 0 ));
				set @atr_itens_descricao = (select dbo.ConcatenarAtributoItens( @atr_id, 1 ));
				set @atr_itens_ids = (select dbo.ConcatenarAtributoItens( @atr_id, 2 ));

				set @atr_itens = 'ITENS: ids=['  + @atr_itens_ids + ']; codigos=[' + @atr_itens_codigo + ']; descricao=[' + @atr_itens_descricao + '];'


				-- "APAGA" OS DADOS

					update dbo.tab_atributos
					set atr_deletado = GETDATE(),
						atr_data_atualizacao = GETDATE(),
						atr_atualizado_por = @usu_id
					 where  atr_id= @atr_id ;

					update dbo.tab_atributo_itens
					set ati_deletado = GETDATE(),
						ati_data_atualizacao = GETDATE(),
						ati_atualizado_por = @usu_id
					 where  atr_id= @atr_id ;


					 				
			-- ********* INSERÇÃO DE LOG **************************************

				declare @tabela varchar(300) = 'tab_atributos';
				declare @tra_transacao_id int = 5; -- 5= "exclusão"
				declare @mod_modulo_id_log int = 120; -- 120 = atributos 

				-- checa se a tmp table existem e a exclui
				set nocount on;
				if OBJECT_ID('tempdb..#tmpTabela') is not null
					DROP TABLE #tmpTabela;

				-- insere dados na tabela #tmpTabela
				SELECT * into #tmpTabela 
				from dbo.tab_atributos
					 where  atr_id= @atr_id;


				-- concatena os valores e retorna em varchar
				declare @log_texto varchar(MAX); 
				exec dbo.STP_CONCATENA_COLS_VALS  @tabela, @retorno = @log_texto output, @tudo=0 ;

				if rtrim(@atr_itens) <> ''
					set @log_texto = @log_texto + ';' + @atr_itens;

				-- exclui a temporaria
				DROP TABLE #tmpTabela;

				set nocount off;
				exec dbo.STP_INS_LOGSISTEMA @tra_transacao_id, @usu_id, @mod_modulo_id_log,	@log_texto,	@ip			

			 -- ****************************************************************
			COMMIT TRAN T1

			set @retorno = 1;
		end

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
