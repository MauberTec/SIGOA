CREATE procedure [dbo].[STP_DEL_OBJETO_SUBDIVISAO2]
@tip_id int, 
@obj_id_tipoOAE int, 
@usu_id int,
@ip nvarchar(30)


--with encryption
as

DECLARE @ErrorMessage varchar(255), @ErrorSeverity int, @ErrorState int 

begin try
		BEGIN TRAN T1
			declare @actionDate datetime = getdate();

			declare @obj_codigo_tipoOAE  varchar(400) = (select obj_codigo from dbo.tab_objetos where obj_id = @obj_id_tipoOAE);

			declare @obj_codigo  varchar(400) = (select top 1 obj_codigo from dbo.tab_objetos where obj_codigo like  @obj_codigo_tipoOAE + '%' and tip_id = @tip_id);
			declare @obj_id int = (select top 1 obj_id from dbo.tab_objetos where obj_codigo like  @obj_codigo_tipoOAE + '%' and tip_id = @tip_id);

			declare @temDados int = (
										select COUNT(*) 
										from [dbo].[tab_objeto_grupo_objeto_variaveis_valores]
										where ovv_deletado is null
										and obj_id in ( 
														select obj_id 
														from dbo.tab_objetos
														where obj_deletado is null and obj_ativo = 1
														and obj_codigo like @obj_codigo + '%')
									);


			if (@temDados > 0)
			begin
			
				--Raise an error
				 SELECT @ErrorMessage = 'Não é possível excluir o Objeto ou seus filhos porque já possuem dados associados', @ErrorSeverity = 1, @ErrorState = 1
				 RAISERROR(@ErrorMessage,@ErrorSeverity,@ErrorState) 

			end
			else
			begin
				-- "APAGA" OS DADOS
					update dbo.tab_objetos
					set obj_deletado = @actionDate,
						obj_data_atualizacao = @actionDate,
						obj_atualizado_por = @usu_id
					 where  obj_id= @obj_id ;


				--apaga em cascata: filhos
				update dbo.tab_objetos
				    set obj_deletado = @actionDate,
						obj_data_atualizacao = @actionDate,
						obj_atualizado_por = @usu_id
				     where  obj_deletado is null 
							and obj_codigo like (@obj_codigo + '%'); 
/*
			   -- apaga as associacoes com documentos
					update dbo.tab_documento_objeto
					set dob_deletado = @actionDate,
						dob_data_atualizacao = @actionDate,
						dob_atualizado_por = @usu_id
					 where  obj_id= @obj_id ;

					update dbo.tab_ordens_servico
					set ord_deletado = @actionDate,
						ord_data_atualizacao = @actionDate,
						ord_atualizado_por = @usu_id
					 where  obj_id= @obj_id ;
*/


					 				
			-- ********* INSERÇÃO DE LOG **************************************

				declare @tabela varchar(300) = 'tab_objetos';
				declare @tra_transacao_id int = 5; -- 5= "exclusão"
				declare @mod_modulo_id_log int = 140; -- cadastro de OBJETOS

				-- checa se a tmp table existem e a exclui
				set nocount on;
				if OBJECT_ID('tempdb..#tmpTabela') is not null
					DROP TABLE #tmpTabela;

				-- insere dados na tabela #tmpTabela
				SELECT * into #tmpTabela 
				from dbo.tab_objetos
					 where  obj_id= @obj_id;

				-- concatena os valores e retorna em varchar
				declare @log_texto varchar(MAX); 
				exec dbo.STP_CONCATENA_COLS_VALS  @tabela, @retorno = @log_texto output, @tudo=0 ;

				-- exclui a temporaria
				DROP TABLE #tmpTabela;

				set nocount off;
				exec dbo.STP_INS_LOGSISTEMA @tra_transacao_id, @usu_id, @mod_modulo_id_log,	@log_texto,	@ip			

			 -- ****************************************************************
			 end;
				
		COMMIT TRAN T1
		return 1
end try
begin catch
		ROLLBACK TRAN T1
            PRINT 'The following error has occurred:  ' + ERROR_MESSAGE()

		--Raise an error
		 SELECT @ErrorMessage = ERROR_MESSAGE(), @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE() 
		 RAISERROR(@ErrorMessage,@ErrorSeverity,@ErrorState) 
end catch
