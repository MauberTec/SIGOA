CREATE procedure [dbo].[STP_UPD_USUARIO] 
@usu_id int, 
@usu_nome nVARCHAR(80), 
@usu_usuario nVARCHAR(20), 
@usu_email nVARCHAR(255), 
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

-- ********* INSERÇÃO DE LOG - parte 1 ****************************

				-- checa se as tmp tables existem e as exclui
set nocount on;
				if OBJECT_ID('tempdb..#tmpComparar') is not null
				DROP TABLE #tmpComparar;

				-- cria e insere dados OLD na tabela #tmpComparar
				SELECT 1 as nRow, * into #tmpComparar 
				from dbo.tab_usuarios
				where  usu_id= @usu_id ;

-- #########################################################################
set nocount off;

				update dbo.tab_usuarios
				    set usu_nome = @usu_nome,
						usu_usuario = @usu_usuario,
						usu_email = @usu_email,
						usu_ativo = @usu_ativo,
						usu_foto = @usu_foto,
						usu_atualizado_por = @usu_id_logado,
						usu_data_atualizacao = @actionDate 
					where usu_id= @usu_id ;


-- ********* INSERÇÃO DE LOG - continuacao ****************************
set nocount on;
				declare @tabela varchar(300) = 'tab_usuarios';
				declare @tra_id int = 6; -- tra_id= 6 ==> alteração
				declare @mod_id_log int = 1080; -- 1080:usuarios

				-- cria e insere dados NEW na tabela #tmpComparar
				insert into  #tmpComparar 
				SELECT 2 as nRow, *  
				from dbo.tab_usuarios
				where  usu_id= @usu_id ;


				-- compara as linhas e retorna em varchar
				declare @log_texto varchar(MAX); 
				exec dbo.STP_COMPARA_COLS_ANTES_DEPOIS  @tabela, @retorno = @log_texto output;

				-- exclui a temporaria
				DROP TABLE #tmpComparar;
				exec dbo.STP_INS_LOGSISTEMA @tra_id, @usu_id_logado, @mod_id_log,	@log_texto,	@ip			

set nocount off;

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
