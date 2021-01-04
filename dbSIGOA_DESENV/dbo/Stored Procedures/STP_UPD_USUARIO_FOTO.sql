CREATE procedure [dbo].[STP_UPD_USUARIO_FOTO]
@usu_foto VARCHAR(max)=null, 
@usu_id_logado int

--with encryption
as

begin try
		BEGIN TRAN T1
				declare @actionDate datetime
				set @actionDate = getdate()

				update dbo.tab_usuarios
				    set
						usu_foto = @usu_foto,
						usu_atualizado_por = @usu_id_logado,
						usu_data_atualizacao = @actionDate 
					where usu_id= @usu_id_logado
					and usu_ativo= 1
					and usu_deletado is null;

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
