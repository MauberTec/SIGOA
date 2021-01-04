CREATE procedure [dbo].[STP_INS_LOGSISTEMA](
	@tra_id [smallint],
	@usu_id nvarchar(20),
	@mod_id [int],
	@log_texto [varchar](max) = null,
	@log_ip [nvarchar](30),
	@XXXXX nvarchar(30) = null,
	@YYYYY nvarchar(30) = null
)

as
begin try
	BEGIN TRAN T1
	set nocount on;

		if (@log_texto is null) 
		 set @log_texto = '';

		declare @tra_mensagem nvarchar (MAX);
		
		declare @dbName varchar(100) = (SELECT DB_NAME());
		if CHARINDEX('_DESENV', @dbName )> 0
			SET @tra_mensagem = (select isnull(tra_mensagem,'') as tra_mensagem
												  from [SIGOA_SECURITY_DESENV].dbo.tab_transacao
													where tra_id = @tra_id);
		else
			SET @tra_mensagem = (select isnull(tra_mensagem,'') as tra_mensagem
												  from [SIGOA_SECURITY].dbo.tab_transacao
													where tra_id = @tra_id);

		--declare @mod_nome_modulo nvarchar(100) = (select isnull(mod_nome_modulo,'') as mod_nome_modulo
		--										   from [dbo].[tab_modulos]
		--											where mod_id = @mod_id);

		
		declare @mod_nome_modulo nvarchar(100) ;
		declare @mod_nome_modulo_pai nvarchar(100) ;
		declare @mod_pai_id int;
		
		select @mod_nome_modulo=isnull(mod_nome_modulo,''),
				@mod_pai_id = mod_pai_id
				 from [dbo].[tab_modulos]
					where mod_id = @mod_id;

		-- se o modulo for filho, acha o modulo Pai
		if @mod_pai_id is not null 
		begin
			set @mod_nome_modulo_pai = (select isnull(mod_nome_modulo,'')
										 from [dbo].[tab_modulos]
											where mod_id = @mod_pai_id);
			--concatena o pai+filho
			set @mod_nome_modulo = @mod_nome_modulo_pai + '->' + @mod_nome_modulo;
		end
		else
			set @mod_nome_modulo_pai = ''



		declare @usu_usuario nvarchar(20);
		if ISNUMERIC(@usu_id) = 1 -- "1" significa retorno true
			set @usu_usuario = (select usu_usuario
											  from [dbo].[tab_usuarios]
												where usu_id = @usu_id);
		else
			begin
				set @usu_usuario = @usu_id;
				set @usu_id = -1;
			end

		set @tra_mensagem = replace(@tra_mensagem,'MMMMM',@mod_nome_modulo); 
		set @tra_mensagem = replace(@tra_mensagem,'UUUUU',@usu_usuario); 

		if (@XXXXX is not null)
			set @tra_mensagem = replace(@tra_mensagem,'XXXXX',@XXXXX); 

		if (@YYYYY is not null)
			set @tra_mensagem = replace(@tra_mensagem,'YYYYY',@YYYYY); 

		set @log_texto = @tra_mensagem  + ' ' + @log_texto ;	

		if CHARINDEX('_DESENV', @dbName )> 0
			insert into  SIGOA_SECURITY_DESENV.dbo.tab_log (tra_id, usu_id, log_data_criacao, mod_id, log_texto, log_ip )
			VALUES (@tra_id,@usu_id, GETDATE(),@mod_id, @log_texto, @log_ip );
		ELSE
			insert into  SIGOA_SECURITY.dbo.tab_log (tra_id, usu_id, log_data_criacao, mod_id, log_texto, log_ip )
			VALUES (@tra_id,@usu_id, GETDATE(),@mod_id, @log_texto, @log_ip );
		
		set nocount off;			   
	COMMIT TRAN T1
end try
begin catch
		ROLLBACK TRAN T1
            --PRINT 'The following error has occurred:  ' + ERROR_MESSAGE()

		--Raise an error
		 DECLARE @ErrorMessage varchar(255), @ErrorSeverity int, @ErrorState int 
		 SELECT @ErrorMessage = ERROR_MESSAGE(), @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE() 
		 RAISERROR(@ErrorMessage,@ErrorSeverity,@ErrorState) 
end catch
