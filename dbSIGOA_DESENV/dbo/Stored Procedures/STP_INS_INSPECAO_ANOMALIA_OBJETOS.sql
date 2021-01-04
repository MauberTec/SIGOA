

CREATE procedure [dbo].[STP_INS_INSPECAO_ANOMALIA_OBJETOS]
@ord_id int,
@obj_ids varchar(max),
@usu_id int,
@ip nvarchar(30)

as

begin try
		BEGIN TRAN T1

				declare @actionDate datetime = getdate();

				declare @ins_id int = (select ins_id from dbo.tab_inspecoes where ord_id = @ord_id );


				declare @sql varchar(max) = '';

				declare @obj_ids_orig varchar(max) = @obj_ids;
				declare @obj_id int = 0;
				declare @jatem int = 0;

				set nocount on;


				declare @delimiter varchar(1) = ';';
				declare @idx int  = 1   ;  
				declare @retorno varchar(max) = '';
				declare @ian_id int = 0;

				while (@idx != 0)
				begin     
					set @idx = charindex(@delimiter, @obj_ids);
	
					if @idx!= 0 
					begin
						set @obj_id = left(@obj_ids, @idx - 1) ; 

						-- CHECA SE JA TEM
						set @jatem = (select count(*) from dbo.tab_inspecoes_anomalias where ins_id= @ins_id and obj_id = CONVERT(int, @obj_id));

						if (@jatem = 0) -- insere porque nao tem
						begin
							set @ian_id = (select isnull(max(ian_id),0) +1 from dbo.tab_inspecoes_anomalias);
				
							INSERT into dbo.tab_inspecoes_anomalias	(ian_id,obj_id,ins_id,ian_numero,atp_id,ian_sigla,ale_id,ian_quantidade,ian_espacamento,ian_largura,ian_comprimento,ian_abertura_minima,ian_abertura_maxima,aca_id,ian_fotografia,ian_desenho,leg_id,ian_ativo,ian_data_criacao,ian_criado_por)
							VALUES (@ian_id, @obj_id, @ins_id, -1, 1, 'A', 1, -1, -1, -1, -1, -1, -1, -1, -1, '', -1, 1, @actionDate, @usu_id );
						end
				    end

					set @obj_ids = right(@obj_ids,len(@obj_ids) - @idx)     
					if len(@obj_ids) = 0 break;
				end 


			-- ********* INSERÇÃO DE LOG **************************************

			    -- busca e concatena os codigos dos documentos
				declare @tabela varchar(300) = 'tab_inspecoes_anomalias';
				declare @tra_id int = 4; -- 4= insercao
				declare @mod_id_log int = -201;-- tab_inspecoes_anomalias

				declare @log_texto varchar(max) = 'Inserção dos objetos:['+ @obj_ids_orig + ']';

				exec dbo.STP_INS_LOGSISTEMA @tra_id, @usu_id, @mod_id_log,	@log_texto,	@ip

			 -- ****************************************************************


				set nocount off;

		COMMIT TRAN T1
		
		return 1;

end try
begin catch
		ROLLBACK TRAN T1
            PRINT 'The following error has occurred:  ' + ERROR_MESSAGE()

		--Raise an error
		 DECLARE @ErrorMessage varchar(255), @ErrorSeverity int, @ErrorState int 
		 SELECT @ErrorMessage = ERROR_MESSAGE(), @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE() 
		 RAISERROR(@ErrorMessage,@ErrorSeverity,@ErrorState) 
end catch
