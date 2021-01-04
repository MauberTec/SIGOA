CREATE procedure [dbo].[STP_UPD_DOC_ASSOCIAR_OBJETOS]
@doc_id int,
@obj_ids varchar(max),
@usu_id_logado int,
@ip nvarchar(30)

as

begin try
		BEGIN TRAN T1
				declare @sql varchar(max) = '';
				set @obj_ids = replace(@obj_ids, ';', ','); 

					-- insere dados novos na tabela tab_documento_objeto
						set @sql = '  insert into dbo.tab_documento_objeto (doc_id, obj_id, dob_data_criacao, dob_criado_por )
									  select ' + convert(varchar, @doc_id) + ', obj_id, getdate(), ' +  convert(varchar, @usu_id_logado) +
									' from dbo.tab_objetos where obj_id in ('+ LEFT(@obj_ids, LEN(@obj_ids) - 1)  + ')';
						exec ( @sql );


			-- ********* INSERÇÃO DE LOG **************************************

			    -- busca e concatena os codigos dos documentos
				declare @tabela varchar(300) = 'tab_documento_objeto';
				declare @tra_id int = 13; -- "ASSOCIAÇÃO"
				declare @mod_id_log int = -710;-- -710 = documento_objeto

				declare @obj_id varchar(5);
				declare @obj_codigo varchar(50);
				declare @obj_idsA varchar(max) = '';
				declare @obj_codigosA varchar(max) = '';
				declare @logA varchar(max) = '';

				set nocount on;


				declare @delimiter varchar(1) = ',';
				declare @idx int  = 1   ;  
				declare @i int = 0;

				while (@idx != 0)
				begin     
					set @i = @i + 1;
					set @idx = charindex(@delimiter, @obj_ids);
	
					if @idx!= 0 
					begin

						select @obj_id = left(@obj_ids,@idx - 1) ; 
						select @obj_codigo = (select top 1 obj_codigo from dbo.tab_objetos where obj_id = @obj_id);

						if @i =1 
						begin
							set @obj_idsA = '[' +  @obj_id + ']';
							set @obj_codigosA =  '[' + @obj_codigo + ']';
						end
						else
						begin
							set @obj_idsA = @obj_idsA + ',[' + @obj_id + ']';
							set @obj_codigosA = @obj_codigosA + ',[' + @obj_codigo + ']';
						end
				    end

					set @obj_ids = right(@obj_ids,len(@obj_ids) - @idx)     
					if len(@obj_ids) = 0 break;
				end 


				set nocount off;

				declare @doc_codigo varchar(150) = (select top 1 doc_codigo from dbo.tab_documentos where doc_id = @doc_id);
				set @doc_codigo = 'doc_id:[' + convert(varchar(5), @doc_id) + ']; doc_codigo:[' + @doc_codigo + ']';

				declare @log_texto varchar(max) = @doc_codigo + '; obj_id:'+ @obj_idsA + '; obj_codigo:'+ @obj_codigosA;


				exec dbo.STP_INS_LOGSISTEMA @tra_id, @usu_id_logado, @mod_id_log,	@log_texto,	@ip, 'Documento', 'Objetos'

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
