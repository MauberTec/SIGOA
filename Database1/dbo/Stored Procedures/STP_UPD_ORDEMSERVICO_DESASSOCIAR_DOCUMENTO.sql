CREATE procedure [dbo].[STP_UPD_ORDEMSERVICO_DESASSOCIAR_DOCUMENTO]
@doc_id int,
@ord_id int,
@usu_id_logado int,
@ip nvarchar(30)

as

begin try
		BEGIN TRAN T1

				update dbo.tab_documento_ordens_servicos 
				set dos_deletado = getdate(),
					dos_atualizado_por = @usu_id_logado,
					dos_data_atualizacao = getdate()
				where doc_id = @doc_id
						and ord_id= @ord_id
						and [dos_deletado] is null


			-- ********* INSERÇÃO DE LOG **************************************

			    -- busca e concatena os codigos dos documentos
				declare @tabela varchar(300) = 'tab_documento_ordens_servicos';
				declare @tra_id int = 14; -- "DESASSOCIAÇÃO"
				--declare @mod_id_log int = -710;-- -710 = tab_documento_ordens_servicos
				declare @mod_id_log int = -540;--  cadastro de ordens servicos

				declare @doc_codigo varchar(50) = (select top 1 doc_codigo from dbo.tab_documentos where doc_id = @doc_id);
				declare @ord_codigo varchar(150) = (select top 1 ord_codigo from dbo.tab_ordens_servico where ord_id = @ord_id);

				set @doc_codigo = 'doc_id:[' + convert(varchar(5), @doc_id) + ']; doc_codigo:[' + @doc_codigo + ']';
				set @ord_codigo = 'ord_id:[' + convert(varchar(5), @ord_id) + ']; ord_codigo:[' + @ord_codigo + ']';

				declare @log_texto varchar(max) = @ord_codigo + '; ' + @doc_codigo;

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
