CREATE procedure [dbo].[STP_UPD_OBJ_DESASSOCIAR_DOCUMENTO]
@doc_id int,
@obj_id int,
@usu_id_logado int,
@ip nvarchar(30)

as

begin try
		BEGIN TRAN T1

				update dbo.tab_documento_objeto 
				set dob_deletado = getdate(),
					dob_atualizado_por = @usu_id_logado,
					dob_data_atualizacao = getdate()
				where doc_id = @doc_id
						and obj_id= @obj_id


			-- ********* INSERÇÃO DE LOG **************************************

			    -- busca e concatena os codigos dos documentos
				declare @tabela varchar(300) = 'tab_documento_objeto';
				declare @tra_id int = 14; -- "DESASSOCIAÇÃO"
				declare @mod_id_log int = -710;-- -710 = documento_objeto

				declare @doc_codigo varchar(50) = (select top 1 doc_codigo from dbo.tab_documentos where doc_id = @doc_id);
				declare @obj_codigo varchar(150) = (select top 1 obj_codigo from dbo.tab_objetos where obj_id = @obj_id);

				set @doc_codigo = 'doc_id:[' + convert(varchar(5), @doc_id) + ']; doc_codigo:[' + @doc_codigo + ']';
				set @obj_codigo = 'obj_id:[' + convert(varchar(5), @obj_id) + ']; obj_codigo:[' + @obj_codigo + ']';

				declare @log_texto varchar(max) = @obj_codigo + '; ' + @doc_codigo;

				exec dbo.STP_INS_LOGSISTEMA @tra_id, @usu_id_logado, @mod_id_log,	@log_texto,	@ip, 'Documento', 'Objeto'			

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
