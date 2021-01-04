CREATE procedure [dbo].[STP_UPD_ATIVARDESATIVAR_DOCUMENTOOBJETO] 
@doc_id int,
@obj_id int,
@usu_id_logado int,
@ip nvarchar(30)


--with encryption
as

begin try
		BEGIN TRAN T1

				declare @jatem int = ( select COUNT(*)
                          				from dbo.tab_documento_objeto
										where obj_id = @obj_id
												and doc_id = @doc_id );

												 				
				-- checa se a tmp table existem e a exclui
				if OBJECT_ID('tempdb..#tmpTabela') is not null
					DROP TABLE #tmpTabela;

					-- cria a #tmpTabela e limpa
					SELECT top 1 doc_id, obj_id, dob_deletado, dob_data_criacao, dob_criado_por, dob_data_atualizacao, dob_atualizado_por 
					into #tmpTabela 
					from dbo.tab_documento_objeto;

					delete from #tmpTabela ;

					-- continua		   
					if 	@jatem =0
					begin
						insert into dbo.tab_documento_objeto (obj_id, doc_id, dob_data_criacao, dob_criado_por )
						values (@obj_id, @doc_id, getdate(), @usu_id_logado);

						-- insere dados novos na tabela #tmpTabela 
						insert into #tmpTabela 
						SELECT doc_id, obj_id, dob_deletado, dob_data_criacao, dob_criado_por, dob_data_atualizacao, dob_atualizado_por  
						from dbo.tab_documento_objeto
						where obj_id = @obj_id
							   and doc_id = @doc_id;
					end
					else
					begin
						-- insere dados antigos na tabela #tmpTabela 
						insert into #tmpTabela 
						SELECT doc_id, obj_id, dob_deletado, dob_data_criacao, dob_criado_por, dob_data_atualizacao, dob_atualizado_por  
						from dbo.tab_documento_objeto
						where obj_id = @obj_id
							   and doc_id = @doc_id;

						-- desativa o registro
						delete from dbo.tab_documento_objeto
						where obj_id = @obj_id
							 and doc_id = @doc_id ;						
					end;

			-- ********* INSERÇÃO DE LOG **************************************

				declare @tabela varchar(300) = 'tab_documento_objeto';
				declare @tra_id int = 8; --8= "ativacão"
				declare @mod_id_log int = -710;-- -710 = documento_objeto

				if @jatem =1 
				   set @tra_id = 9; -- 9= desativacao / registro desativado

				-- compara as linhas e retorna em varchar
				declare @log_texto varchar(MAX); 
				exec dbo.STP_CONCATENA_COLS_VALS  @tabela, @retorno = @log_texto output, @tudo=1;

				-- exclui a temporaria
				DROP TABLE #tmpTabela;

				exec dbo.STP_INS_LOGSISTEMA @tra_id, @usu_id_logado, @mod_id_log,	@log_texto,	@ip			

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
