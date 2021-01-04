CREATE procedure [dbo].[STP_INS_LOG_EXCECAO_SISTEMA] 
@tls_tipo nVARCHAR(10),
@tls_processo nVARCHAR(max),
@tls_excecao nVARCHAR(max),
@tls_id int output
--with encryption
as

begin try
		BEGIN TRAN T1
		declare @dbName varchar(100) = (SELECT DB_NAME());
		if CHARINDEX('_DESENV', @dbName )> 0
				INSERT INTO [SIGOA_SECURITY_DESENV].[dbo].[tab_logErros] (tls_tipo, tls_processo, tls_excecao )
				    values (@tls_tipo, @tls_processo, @tls_excecao);
		else
				INSERT INTO [SIGOA_SECURITY].[dbo].[tab_logErros] (tls_tipo, tls_processo, tls_excecao )
				    values (@tls_tipo, @tls_processo, @tls_excecao);

		COMMIT TRAN T1

		select @tls_id = 1
end try
begin catch
		ROLLBACK TRAN T1
            PRINT 'The following error has occurred:  ' + ERROR_MESSAGE()

		--Raise an error
		 DECLARE @ErrorMessage varchar(255), @ErrorSeverity int, @ErrorState int 
		 SELECT @ErrorMessage = ERROR_MESSAGE(), @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE() 
		 RAISERROR(@ErrorMessage,@ErrorSeverity,@ErrorState) 
end catch
