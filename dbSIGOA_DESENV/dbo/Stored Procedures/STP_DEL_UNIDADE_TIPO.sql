CREATE procedure [dbo].[STP_DEL_UNIDADE_TIPO] 
@unt_id int,
@usu_id int,
@ip nvarchar(30)


--with encryption
as

begin try
		BEGIN TRAN T1

				-- concatena os itens apagados para colocar no Log
				declare @uni_itens_ids varchar(MAX); 
				declare @uni_itens_unidade varchar(MAX); 
				declare @uni_itens_descricao varchar(MAX); 
				declare @uni_itens varchar(MAX); 

				set @uni_itens_unidade = (select dbo.ConcatenarUnidadesMedidaPorTipo( @unt_id, 0 ));
				set @uni_itens_descricao = (select dbo.ConcatenarUnidadesMedidaPorTipo( @unt_id, 1 ));
				set @uni_itens_ids = (select dbo.ConcatenarUnidadesMedidaPorTipo( @unt_id, 2 ));

				set @uni_itens = 'ITENS: ids=['  + @uni_itens_ids + ']; unidades=[' + @uni_itens_unidade + ']; descricao=[' + @uni_itens_descricao + '];'


				-- "APAGA" OS DADOS					
					update dbo.tab_unidades_tipos
					set unt_deletado = GETDATE(),
						unt_data_atualizacao = GETDATE(),
						unt_atualizado_por = @usu_id
					 where unt_id = @unt_id;

				 
					update dbo.tab_unidades_medida
					set uni_deletado = GETDATE(),
						uni_data_atualizacao = GETDATE(),
						uni_atualizado_por = @usu_id
					 where  unt_id= @unt_id ;

					 				
			-- ********* INSERÇÃO DE LOG **************************************

				declare @tabela varchar(300) = 'tab_unidades_tipos';
				declare @tra_id int = 5; -- 5= "exclusão"
				declare @mod_id_log int = -411; -- tipo de unidade

				-- checa se a tmp table existem e a exclui
				set nocount on;
				if OBJECT_ID('tempdb..#tmpTabela') is not null
					DROP TABLE #tmpTabela;

				-- insere dados na tabela #tmpTabela
				SELECT * into #tmpTabela 
				from dbo.tab_unidades_tipos
				where  unt_id= @unt_id ;


				-- concatena os valores e retorna em varchar
				declare @log_texto varchar(MAX); 
				exec dbo.STP_CONCATENA_COLS_VALS  @tabela, @retorno = @log_texto output;

				if rtrim(@uni_itens) <> ''
					set @log_texto = @log_texto + ';' + @uni_itens;

				-- exclui a temporaria
				DROP TABLE #tmpTabela;

				set nocount off;
				exec dbo.STP_INS_LOGSISTEMA @tra_id, @usu_id, @mod_id_log,	@log_texto,	@ip			

			 -- ****************************************************************

				
		COMMIT TRAN T1

end try
begin catch
		ROLLBACK TRAN T1
            PRINT 'The following error has occurred:  ' + ERROR_MESSAGE()

		--Raise an error
		 DECLARE @ErrorMessage varchar(255), @ErrorSeverity int, @ErrorState int 
		 SELECT @ErrorMessage = ERROR_MESSAGE(), @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE() 
		 RAISERROR(@ErrorMessage,@ErrorSeverity,@ErrorState) 
end catch
