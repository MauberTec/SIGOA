CREATE procedure [dbo].[STP_INS_USUARIO] 
@usu_nome nVARCHAR(80), 
@usu_usuario nVARCHAR(20), 
@usu_email nVARCHAR(255)=null, 
@usu_foto VARCHAR(max)=null, 
@usu_ativo int,
@usu_id_logado int,
@ip nvarchar(30)

--with encryption
as

begin try
		BEGIN TRAN T1
		
		
				declare @actionDate datetime
				set @actionDate = getdate()

				declare @usu_id int
				set @usu_id = (select isnull(max(usu_id),0) +1 from dbo.tab_usuarios);
				
				-- insere novo usuario
				INSERT INTO dbo.tab_usuarios(usu_id, usu_usuario, usu_nome, usu_email, usu_ativo, usu_foto, usu_data_criacao , usu_criado_por )
				    values (@usu_id, @usu_usuario, @usu_nome, @usu_email, @usu_ativo, @usu_foto, @actionDate, @usu_id_logado);
			
		

			-- ********* INSERÇÃO DE LOG **************************************

				declare @tabela varchar(300) = 'tab_usuarios';
				declare @tra_id int = 4; -- 4= insercao
				declare @mod_id_log int = 1080; -- 1080 = usuarios

				-- checa se a tmp table existem e a exclui
				set nocount on;
				if OBJECT_ID('tempdb..#tmpTabela') is not null
					DROP TABLE #tmpTabela;

				-- insere dados NEW na tabela #tmpTabela
				SELECT * into #tmpTabela 
				from dbo.tab_usuarios
				where  usu_id= @usu_id ;


				-- concatena os valores e retorna em varchar
				declare @log_texto varchar(MAX); 
				exec dbo.STP_CONCATENA_COLS_VALS  @tabela, @retorno = @log_texto output;

				-- exclui a temporaria
				DROP TABLE #tmpTabela;

				set nocount off;
				exec dbo.STP_INS_LOGSISTEMA @tra_id, @usu_id_logado, @mod_id_log,	@log_texto,	@ip			

			 -- ****************************************************************
		COMMIT TRAN T1

		return @usu_id
end try
begin catch
		ROLLBACK TRAN T1
            PRINT 'The following error has occurred:  ' + ERROR_MESSAGE()

		--Raise an error
		 DECLARE @ErrorMessage varchar(255), @ErrorSeverity int, @ErrorState int 
		 SELECT @ErrorMessage = ERROR_MESSAGE(), @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE() 
		 RAISERROR(@ErrorMessage,@ErrorSeverity,@ErrorState) 
end catch
