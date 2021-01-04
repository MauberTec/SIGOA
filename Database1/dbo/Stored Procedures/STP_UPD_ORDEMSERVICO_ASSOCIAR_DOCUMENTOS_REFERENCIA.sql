CREATE procedure [dbo].[STP_UPD_ORDEMSERVICO_ASSOCIAR_DOCUMENTOS_REFERENCIA]
@doc_id int,
@ord_id int,
@dos_referencia int = 0,
@usu_id_logado int,
@ip nvarchar(30)

as

begin try
		BEGIN TRAN T1

				declare @jatem int = (select count(*)
										from dbo.tab_documento_ordens_servicos
											where dos_deletado is null
												and doc_id = @doc_id
												and ord_id = @ord_id);
				if (isnull(@jatem, 0) = 0)
					insert into  dbo.tab_documento_ordens_servicos (doc_id, ord_id, dos_referencia, dos_data_criacao, dos_criado_por )
					values (@doc_id, @ord_id, @dos_referencia, getdate(), @usu_id_logado);
				else
					update dbo.tab_documento_ordens_servicos
						set dos_referencia = @dos_referencia,
							dos_data_atualizacao = getdate(),
							dos_atualizado_por = @usu_id_logado
					where dos_deletado is null
						and doc_id = @doc_id
						and ord_id = @ord_id;


			-- ********* INSERÇÃO DE LOG **************************************

			    -- busca e concatena os codigos dos documentos
				declare @tabela varchar(300) = 'tab_documento_ordens_servicos';
				declare @tra_id int = 13; -- "ASSOCIAÇÃO"
			--	declare @mod_id_log int = -715;-- tab_documento_ordens_servicos
				declare @mod_id_log int = 540;-- ordens servico

				declare @dos_id int = (select top 1 dos_id
										from dbo.tab_documento_ordens_servicos
											where dos_deletado is null
												and doc_id = @doc_id
												and ord_id = @ord_id);

				declare @doc_codigo varchar(150) = (select top 1 doc_codigo from dbo.tab_documentos where doc_id = @doc_id);
				declare @ord_codigo varchar(150) = (select top 1 ord_codigo from dbo.tab_ordens_servico where ord_id = @ord_id);
				declare @usu_usuario varchar(150) = (select top 1 usu_usuario from dbo.tab_usuarios where usu_id = @usu_id_logado);


				declare @log_texto varchar(max) = '';
				if @dos_referencia = 1
				begin
					set @tra_id = 13; -- "ASSOCIAÇÃO"
					set @log_texto = 'Associação de Documento de Referência ' + @doc_codigo + ' à ' + @ord_codigo + ' pelo Usuário ' + @usu_usuario + '.';
				end
				else
				begin
					set @tra_id = 14; -- "DESASSOCIAÇÃO"
					set @log_texto = 'Desassociação de Documento de Referência ' + @doc_codigo + ' à ' + @ord_codigo + ' pelo Usuário ' + @usu_usuario + '.';
				end

				exec dbo.STP_INS_LOGSISTEMA @tra_id, @usu_id_logado, @mod_id_log,	@log_texto,	@ip, 'Documento de Referência', 'Ordem de Serviço'			
				set nocount off;

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
