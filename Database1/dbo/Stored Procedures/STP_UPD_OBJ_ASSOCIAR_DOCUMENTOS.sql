CREATE procedure [dbo].[STP_UPD_OBJ_ASSOCIAR_DOCUMENTOS]
@doc_ids varchar(max),
@obj_id int,
@usu_id_logado int,
@ip nvarchar(30)

as

begin try
		BEGIN TRAN T1
				declare @sql varchar(max) = '';
				set @doc_ids = replace(@doc_ids, ';', ','); 

					-- insere dados novos na tabela tab_documento_objeto
						set @sql = '  insert into dbo.tab_documento_objeto (obj_id, doc_id, dob_data_criacao, dob_criado_por )
									  select ' + convert(varchar, @obj_id) + ', doc_id, getdate(), ' +  convert(varchar, @usu_id_logado) +
									' from dbo.tab_documentos where doc_id in ('+ LEFT(@doc_ids, LEN(@doc_ids) - 1)  + ')';
						exec ( @sql );


			-- ********* INSERÇÃO DE LOG **************************************

			    -- busca e concatena os codigos dos documentos
				declare @tabela varchar(300) = 'tab_documento_objeto';
				declare @tra_id int = 13; -- "ASSOCIAÇÃO"
				declare @mod_id_log int = -710;-- -710 = documento_objeto

				declare @doc_id varchar(5);
				declare @doc_codigo varchar(50);
				declare @doc_idsA varchar(max) = '';
				declare @doc_codigosA varchar(max) = '';
				declare @logA varchar(max) = '';

				set nocount on;


				declare @delimiter varchar(1) = ',';
				declare @idx int  = 1   ;  
				declare @i int = 0;

				while (@idx != 0)
				begin     
					set @i = @i + 1;
					set @idx = charindex(@delimiter, @doc_ids);
	
					if @idx!= 0 
					begin

						select @doc_id = left(@doc_ids,@idx - 1) ; 
						select @doc_codigo = (select top 1 doc_codigo from dbo.tab_documentos where doc_id = @doc_id);

						if @i =1 
						begin
							set @doc_idsA = '[' +  @doc_id + ']';
							set @doc_codigosA =  '[' + @doc_codigo + ']';
						end
						else
						begin
							set @doc_idsA = @doc_idsA + ',[' + @doc_id + ']';
							set @doc_codigosA = @doc_codigosA + ',[' + @doc_codigo + ']';
						end
				    end

					set @doc_ids = right(@doc_ids,len(@doc_ids) - @idx)     
					if len(@doc_ids) = 0 break;
				end 


				set nocount off;

				declare @obj_codigo varchar(150) = (select top 1 obj_codigo from dbo.tab_objetos where obj_id = @obj_id);
				set @obj_codigo = 'obj_id:[' + convert(varchar(5), @obj_id) + ']; obj_codigo:[' + @obj_codigo + ']';

				declare @log_texto varchar(max) = @obj_codigo + '; doc_id:'+ @doc_idsA + '; doc_codigo:'+ @doc_codigosA;


				exec dbo.STP_INS_LOGSISTEMA @tra_id, @usu_id_logado, @mod_id_log,	@log_texto,	@ip, 'Documentos', 'Objeto'			

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
