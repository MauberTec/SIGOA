CREATE procedure [dbo].[STP_UPD_GRUPOS_VARIAVEIS]
@obj_id int,
@grupos_codigos varchar(max),
@usu_id int,
@ip nvarchar(30)

as

begin try
		BEGIN TRAN T1

		declare @obj_codigo_tipoOAE varchar(30) = (select distinct obj_codigo from tab_objetos where obj_deletado is null and obj_ativo = 1 and obj_id = @obj_id);

			-- se quando for ENCONTROS, insere todos os grupos
				if right(rtrim(@grupos_codigos),3) = 'ENC'
				begin

						-- cria uma tabela temporaria
						if OBJECT_ID('tempdb..##temp2') IS NOT NULL
							DROP TABLE ##temp2;

						create table ##temp2(	tip_id int, 
												tip_pai int, 
												tip_codigo varchar(1000), 
												tip_nome varchar(1000), 
												nivel int, 
												codigo varchar(1000));

						declare @tip_id_raiz int = 14;
						declare @saida varchar(max) = '';

						-- cria lista dos objetos a serem criados
						WITH cte (tip_id, tip_pai, tip_codigo, tip_nome, nivel, codigo) AS (

							select tip_id, tip_pai, tip_codigo, tip_nome,
									0 AS nivel, CAST(tip_codigo AS VARCHAR(1000)) AS codigo
							from tab_objeto_tipos 
								where tip_id = @tip_id_raiz 
							UNION ALL 
							select c.tip_id, c.tip_pai, c.tip_codigo, c.tip_nome, 
									cte.nivel + 1 AS nivel, 
									CAST((cte.codigo + '-' + c.tip_codigo) AS VARCHAR(1000)) AS codigo
							from tab_objeto_tipos c 
								INNER JOIN cte ON cte.tip_id = c.tip_pai 
						) 

						insert into ##temp2 (tip_id, tip_pai, tip_codigo, tip_nome, nivel, codigo )
						select tip_id, tip_pai, tip_codigo, tip_nome, nivel, (@obj_codigo_tipoOAE + '-' + codigo)
						from cte
						order by codigo ASC

						select  @saida = COALESCE(@saida + ';', '') + codigo
							from ##temp2

						-- ajusta os ponto e virgulas
						set @saida = substring(@saida,2, len(@saida)) + ';';

						-- continua
						set @grupos_codigos = @saida;
				end


				declare @sql varchar(max) = '';

				declare @grupos_codigos_orig varchar(max) = @grupos_codigos;
				declare @grupo_codigo varchar(50);

				set nocount on;


				declare @delimiter varchar(1) = ';';
				declare @idx int  = 1   ;  
				declare @retorno varchar(max) = '';

				while (@idx != 0)
				begin     
					set @idx = charindex(@delimiter, @grupos_codigos);
	
					if @idx!= 0 
					begin

						set @grupo_codigo = left(@grupos_codigos,@idx - 1) ; 
						
						-- insere o grupo
						EXECUTE @retorno =  dbo.STP_INS_OBJETO @grupo_codigo, '', null, null,	@usu_id, @ip
				    end

					set @grupos_codigos = right(@grupos_codigos,len(@grupos_codigos) - @idx)     
					if len(@grupos_codigos) = 0 break;
				end 


			-- ********* INSERÇÃO DE LOG **************************************

			    -- busca e concatena os codigos dos documentos
				declare @tabela varchar(300) = 'tab_objetos';
				declare @tra_id int = 4; -- 4= insercao
				declare @mod_id_log int = 540; -- Cadastro de O.S.s

				declare @log_texto varchar(max) = 'Inserção dos grupos:['+ @grupos_codigos_orig + ']';

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
