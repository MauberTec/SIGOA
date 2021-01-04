CREATE procedure [dbo].[STP_UPD_ATIVARDESATIVAR_GRUPOUSUARIO] 
@gru_id int,
@usu_id int,
@usu_id_logado int,
@ip nvarchar(30)


--with encryption
as

begin try
		BEGIN TRAN T1

				declare @jatem int = ( select COUNT(*)
                          				from dbo.tab_grupos_usuarios
										where gru_id = @gru_id
												and usu_id = @usu_id );

												 				
				-- checa se a tmp table existem e a exclui
				set nocount on;
				if OBJECT_ID('tempdb..#tmpTabela') is not null
					DROP TABLE #tmpTabela;

					-- cria a #tmpTabela e limpa
					SELECT top 1 gru_id, usu_id, gpu_deletado, gpu_data_criacao, gpu_criado_por, gpu_data_atualizacao, gpu_atualizado_por 
					into #tmpTabela 
					from dbo.tab_grupos_usuarios;

					delete from #tmpTabela ;

					-- continua		   
					if 	@jatem =0
					begin
						insert into dbo.tab_grupos_usuarios (gru_id, usu_id, gpu_data_criacao, gpu_criado_por )
						values (@gru_id, @usu_id, getdate(), @usu_id_logado);

						-- insere dados novos na tabela #tmpTabela 
						insert into #tmpTabela 
						SELECT gru_id, usu_id, gpu_deletado, gpu_data_criacao, gpu_criado_por, gpu_data_atualizacao, gpu_atualizado_por  
						from dbo.tab_grupos_usuarios
						where gru_id = @gru_id
							   and usu_id = @usu_id;
					end
					else
					begin
						-- insere dados antigos na tabela #tmpTabela 
						insert into #tmpTabela 
						SELECT gru_id, usu_id, gpu_deletado, gpu_data_criacao, gpu_criado_por, gpu_data_atualizacao, gpu_atualizado_por  
						from dbo.tab_grupos_usuarios
						where gru_id = @gru_id
							   and usu_id = @usu_id;

						-- desativa o registro
						delete from dbo.tab_grupos_usuarios
						where gru_id = @gru_id
							 and usu_id = @usu_id ;						
					end;

			-- ********* INSERÇÃO DE LOG **************************************

				declare @tabela varchar(300) = 'tab_grupos_usuarios';
				declare @tra_id int = 8; --8= "ativacão"
				declare @mod_id_log int = -1075; -- 1075 = Grupos_Usuarios

				if @jatem =1 
				   set @tra_id = 9; -- 9= desativacao / registro desativado

				-- checa se a tmp table existem e a exclui
				set nocount on;

				-- compara as linhas e retorna em varchar
				declare @log_texto varchar(MAX); 
				exec dbo.STP_CONCATENA_COLS_VALS  @tabela, @retorno = @log_texto output, @tudo=1;

				-- exclui a temporaria
				DROP TABLE #tmpTabela;

				set nocount off;
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
