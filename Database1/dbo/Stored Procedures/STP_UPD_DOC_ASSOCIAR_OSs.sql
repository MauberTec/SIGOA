CREATE procedure [dbo].[STP_UPD_DOC_ASSOCIAR_OSs]
@doc_id int,
@ord_ids varchar(max),
@usu_id_logado int,
@ip nvarchar(30)

as

begin try
		BEGIN TRAN T1
				declare @sql varchar(max) = '';
				set @ord_ids = replace(@ord_ids, ';', ','); 

					-- insere dados novos na tabela tab_documento_ordens_servicos
						set @sql = '  insert into dbo.tab_documento_ordens_servicos (doc_id, ord_id, dos_data_criacao, dos_criado_por )
									  select ' + convert(varchar, @doc_id) + ', ord_id, getdate(), ' +  convert(varchar, @usu_id_logado) +
									' from dbo.tab_ordens_servico where ord_id in ('+ LEFT(@ord_ids, LEN(@ord_ids) - 1)  + ')';
						exec ( @sql );


			-- ********* INSERÇÃO DE LOG **************************************

			    -- busca e concatena os codigos dos documentos
				declare @tabela varchar(300) = 'tab_documento_ordens_servicos';
				declare @tra_id int = 13; -- "ASSOCIAÇÃO"
				declare @mod_id_log int = -715;-- -715 = documento_os

				declare @ord_id varchar(5);
				declare @ord_codigo varchar(50);
				declare @ord_idsA varchar(max) = '';
				declare @ord_codigosA varchar(max) = '';
				declare @logA varchar(max) = '';

				set nocount on;


				declare @delimiter varchar(1) = ',';
				declare @idx int  = 1   ;  
				declare @i int = 0;

				while (@idx != 0)
				begin     
					set @i = @i + 1;
					set @idx = charindex(@delimiter, @ord_ids);
	
					if @idx!= 0 
					begin

						select @ord_id = left(@ord_ids,@idx - 1) ; 
						select @ord_codigo = (select top 1 ord_codigo from dbo.tab_ordens_servico where ord_id = @ord_id);

						if @i =1 
						begin
							set @ord_idsA = '[' +  @ord_id + ']';
							set @ord_codigosA =  '[' + @ord_codigo + ']';
						end
						else
						begin
							set @ord_idsA = @ord_idsA + ',[' + @ord_id + ']';
							set @ord_codigosA = @ord_codigosA + ',[' + @ord_codigo + ']';
						end
				    end

					set @ord_ids = right(@ord_ids,len(@ord_ids) - @idx)     
					if len(@ord_ids) = 0 break;
				end 


				set nocount off;

				declare @doc_codigo varchar(150) = (select top 1 doc_codigo from dbo.tab_documentos where doc_id = @doc_id);
				set @doc_codigo = 'doc_id:[' + convert(varchar(5), @doc_id) + ']; doc_codigo:[' + @doc_codigo + ']';

				declare @log_texto varchar(max) = @doc_codigo + '; ord_id:'+ @ord_idsA + '; ord_codigo:'+ @ord_codigosA;


				exec dbo.STP_INS_LOGSISTEMA @tra_id, @usu_id_logado, @mod_id_log,	@log_texto,	@ip, 'Documento', 'Ordem Serviço'

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
