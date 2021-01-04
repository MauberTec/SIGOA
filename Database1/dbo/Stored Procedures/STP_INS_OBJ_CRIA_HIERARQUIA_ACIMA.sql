CREATE procedure [dbo].[STP_INS_OBJ_CRIA_HIERARQUIA_ACIMA]
@obj_codigo nvarchar(200),
@usu_id int,
@ip nvarchar(30)


--with encryption
as

begin try
set nocount on;
	BEGIN TRAN T1
				declare @actionDate datetime
				set @actionDate = getdate()

				declare @obj_id_ORIG nvarchar(200) = @obj_codigo;

				declare @n int =0;
				declare @pedaco nvarchar(100);

				declare @tip_id int =1;
				declare @clo_id int =1;
				declare @rodovia_codigo varchar(100)= ''; declare @rodovia_id int = -1;
				declare @obra_arte_codigo varchar(100)= ''; declare @obra_arte_id int = -1;
				declare @tipo_obra_arte_codigo varchar(100)= ''; declare @tipo_obra_arte_id int = -1;
				declare @subdivisão1_codigo varchar(100)= ''; declare @subdivisão1_id int = -1;
				declare @subdivisão2_codigo varchar(100)= ''; declare @subdivisão2_id int = -1;
				declare @subdivisão3_codigo varchar(100)= ''; declare @subdivisão3_id int = -1;
				declare @grupo_objetos_codigo varchar(100)= ''; declare @grupo_objetos_id int = -1;
				declare @elemento_codigo varchar(100)= ''; declare @elemento_id int = -1;

				declare @pos int = 0;
				declare @len int = 0;
				declare @delimiter char(1) = '-';

				set @obj_codigo = @obj_codigo + @delimiter;
				while CHARINDEX(@delimiter, @obj_codigo, @pos + 1) > 0
				begin
					set @len = CHARINDEX(@delimiter, @obj_codigo, @pos + 1) - @pos;
					set @pedaco = ltrim(rtrim(substring(@obj_codigo, @pos, @len)));

					set @n = @n + 1;

					--select @n, @pedaco

					if @n=1  -- rodovia
					  set @rodovia_codigo = @pedaco; 

					if (@n=2) -- rodovia ou obra de arte
					begin
						if (@pedaco = 'D' or @pedaco = 'E')
							set @rodovia_codigo = @rodovia_codigo + '-' + @pedaco;

						set @clo_id = 1;
						set @tip_id = dbo.fn_tip_id_por_codigo (substring(rtrim(@rodovia_codigo),1,3) , @clo_id);

						set @rodovia_id = (select obj_id from dbo.tab_objetos where obj_codigo = @rodovia_codigo);
						if (@rodovia_id is null)  -- NAO TEM RODOVIA entao insere
						begin
							set @rodovia_id = (select isnull(max(obj_id),0) +1 from dbo.tab_objetos);

							insert into dbo.tab_objetos (obj_id, clo_id, tip_id, obj_codigo, obj_descricao, obj_pai, obj_ativo, obj_data_criacao, obj_criado_por)
							values (@rodovia_id, @clo_id, @tip_id, @rodovia_codigo, @rodovia_codigo, NULL, 1, getdate(), @usu_id);

							if (@n=2 and (@pedaco = 'D' or @pedaco = 'E'))
								set @n = 1; -- ajuste no contador
						end


						if (@n=2)  -- obra de arte (quilometragem)
						begin
							set @obra_arte_codigo = @rodovia_codigo + '-' + @pedaco;
							set @obra_arte_id = (select obj_id from dbo.tab_objetos where obj_codigo = @obra_arte_codigo);
							if @obra_arte_id is null -- SE NAO TEM OBRA DE ARTE, INSERE
							begin
								set @obra_arte_id = (select isnull(max(obj_id),0) +1 from dbo.tab_objetos);
								set @clo_id = 2;
								set @tip_id = 28;

								insert into dbo.tab_objetos (obj_id, clo_id, tip_id, obj_codigo, obj_descricao, obj_pai, obj_ativo, obj_data_criacao, obj_criado_por)
								values (@obra_arte_id, @clo_id, @tip_id, @obra_arte_codigo, @obra_arte_codigo, @rodovia_id, 1, getdate(), @usu_id);
							end
						end

					end

					if @n=3 -- tipo de obra de arte
					begin
						set @clo_id = 3;
						set @tip_id = dbo.fn_tip_id_por_codigo (substring(rtrim(@pedaco),1,3) , @clo_id);
						set @tipo_obra_arte_codigo = @obra_arte_codigo + '-' + @pedaco;
						set @tipo_obra_arte_id = (select obj_id from dbo.tab_objetos where obj_codigo = @tipo_obra_arte_codigo);
						if @tipo_obra_arte_id is null -- SE NAO TEM TIPO DE OBRA DE ARTE, INSERE
						begin
							set @tipo_obra_arte_id = (select isnull(max(obj_id),0) +1 from dbo.tab_objetos);
							insert into dbo.tab_objetos (obj_id, clo_id, tip_id, obj_codigo, obj_descricao, obj_pai, obj_ativo, obj_data_criacao, obj_criado_por)
							values (@tipo_obra_arte_id, @clo_id, @tip_id, @tipo_obra_arte_codigo, @tipo_obra_arte_codigo, @obra_arte_id, 1, getdate(), @usu_id);
						end
					end

					if @n=4 -- subdivisao1
					begin
						set @clo_id = 6;
						set @tip_id = dbo.fn_tip_id_por_codigo (rtrim(@pedaco), @clo_id);

						set @subdivisão1_codigo = @tipo_obra_arte_codigo + '-' + @pedaco;
						set @subdivisão1_id = (select obj_id from dbo.tab_objetos where obj_codigo = @subdivisão1_codigo);
						if @subdivisão1_id is null -- SE NAO TEM subdivisão1, INSERE
						begin
							set @subdivisão1_id = (select isnull(max(obj_id),0) +1 from dbo.tab_objetos);
							insert into dbo.tab_objetos (obj_id, clo_id, tip_id, obj_codigo, obj_descricao, obj_pai, obj_ativo, obj_data_criacao, obj_criado_por)
							values (@subdivisão1_id, @clo_id, @tip_id, @subdivisão1_codigo, @subdivisão1_codigo, @tipo_obra_arte_id, 1, getdate(), @usu_id);
						end	

						-- AJUSTE: se for mesoestrutura ou infraestrutura pula direto para classe Grupo de Objetos
						if (@clo_id = 6 and (@tip_id = 12 or @tip_id = 3))
						begin
							set @subdivisão3_id = @subdivisão1_id;
							set @subdivisão3_codigo = @subdivisão1_codigo;
							set @n = 6;
							set @pos = CHARINDEX(@delimiter, @obj_codigo, @pos + @len) + 1;
							continue;
						end
					end

					if @n=5 -- subdivisao2
					begin
						set @clo_id = 7;
						set @tip_id = dbo.fn_tip_id_por_codigo (@pedaco, @clo_id);

						set @subdivisão2_codigo = @subdivisão1_codigo + '-' + @pedaco;
						set @subdivisão2_id = (select obj_id from dbo.tab_objetos where obj_codigo = @subdivisão2_codigo);
						if @subdivisão2_id is null -- SE NAO tem subdivisão2, INSERE
						begin
							set @subdivisão2_id = (select isnull(max(obj_id),0) +1 from dbo.tab_objetos);
							insert into dbo.tab_objetos (obj_id, clo_id, tip_id, obj_codigo, obj_descricao, obj_pai, obj_ativo, obj_data_criacao, obj_criado_por)
							values (@subdivisão2_id, @clo_id, @tip_id, @subdivisão2_codigo, @subdivisão2_codigo, @subdivisão1_id, 1, getdate(), @usu_id);
						end	

						-- AJUSTE: se for superestrutura/Tabuleiro Face Superior ou Inferior ou encontros/acessos pula direto para classe Grupo de Objetos
						if (@tip_id = 15 or @tip_id = 16 or @tip_id = 24)
						begin
							set @subdivisão3_id = @subdivisão2_id;
							set @subdivisão3_codigo = @subdivisão2_codigo;
							set @n = 6;
							set @pos = CHARINDEX(@delimiter, @obj_codigo, @pos + @len) + 1;
							continue;
						end

					end

					if @n=6 -- subdivisao3
					begin
						set @clo_id = 8;
						set @tip_id = dbo.fn_tip_id_por_codigo (@pedaco, @clo_id);

						set @subdivisão3_codigo = @subdivisão2_codigo + '-' + @pedaco;
						set @subdivisão3_id = (select obj_id from dbo.tab_objetos where obj_codigo = @subdivisão3_codigo);
						if @subdivisão3_id is null -- SE NAO tem subdivisão3, INSERE
						begin
							set @subdivisão3_id = (select isnull(max(obj_id),0) +1 from dbo.tab_objetos);
							insert into dbo.tab_objetos (obj_id, clo_id, tip_id, obj_codigo, obj_descricao, obj_pai, obj_ativo, obj_data_criacao, obj_criado_por)
							values (@subdivisão3_id, @clo_id, @tip_id, @subdivisão3_codigo, @subdivisão3_codigo, @subdivisão2_id, 1, getdate(), @usu_id);
						end	
					end

					if @n=7 -- grupo de objetos
					begin
						set @clo_id = 9;
						set @tip_id = dbo.fn_tip_id_por_codigo (@pedaco, @clo_id);

						set @grupo_objetos_codigo = @subdivisão3_codigo + '-' + @pedaco;
						set @grupo_objetos_id = (select obj_id from dbo.tab_objetos where obj_codigo = @grupo_objetos_codigo);
						if @grupo_objetos_id is null -- SE NAO tem INSERE
						begin
							set @grupo_objetos_id = (select isnull(max(obj_id),0) +1 from dbo.tab_objetos);
							insert into dbo.tab_objetos (obj_id, clo_id, tip_id, obj_codigo, obj_descricao, obj_pai, obj_ativo, obj_data_criacao, obj_criado_por)
							values (@grupo_objetos_id, @clo_id, @tip_id, @grupo_objetos_codigo, @grupo_objetos_codigo, @subdivisão3_id, 1, getdate(), @usu_id);
						end	
					end

					set @pos = CHARINDEX(@delimiter, @obj_codigo, @pos + @len) + 1;
				end

			-- insere o elemento
				set @elemento_id = (select isnull(max(obj_id),0) +1 from dbo.tab_objetos);
				set @clo_id = 10; -- classe localizacao
				set @tip_id = 25; -- tipo elemento

				set @obj_codigo = LEFT(@obj_codigo, LEN(@obj_codigo) - 1) ; -- remove o ultimo caracter
				insert into dbo.tab_objetos (obj_id, clo_id, tip_id, obj_codigo, obj_descricao, obj_pai, obj_ativo, obj_data_criacao, obj_criado_por)
				values (@elemento_id, @clo_id, @tip_id, @obj_codigo, @obj_codigo, @grupo_objetos_id, 1, getdate(), @usu_id);


			-- ********* INSERÇÃO DE LOG= SOMENTE DO ULTIMO (ELEMENTO)**************************************

				declare @tabela varchar(300) = 'tab_objetos';
				declare @tra_id int = 4; -- 4= insercao
				declare @mod_id_log int = 140; -- cadastro de OBJETOS

				-- checa se a tmp table existem e a exclui
				set nocount on;
				if OBJECT_ID('tempdb..#tmpTabela') is not null
					DROP TABLE #tmpTabela;

				-- insere dados NEW na tabela #tmpTabela
				SELECT * into #tmpTabela 
				from dbo.tab_objetos
				where  obj_id= @elemento_id ;

				-- concatena os valores e retorna em varchar
				declare @log_texto varchar(MAX); 
				exec dbo.STP_CONCATENA_COLS_VALS  @tabela, @retorno = @log_texto output;

				-- exclui a temporaria
				DROP TABLE #tmpTabela;

				set nocount off;
				exec dbo.STP_INS_LOGSISTEMA @tra_id, @usu_id, @mod_id_log,	@log_texto,	@ip			


	COMMIT TRAN T1
set nocount off;
return ''
end try
begin catch
	ROLLBACK TRAN T1
            PRINT 'The following error has occurred:  ' + ERROR_MESSAGE()

		--Raise an error
		 DECLARE @ErrorMessage varchar(255), @ErrorSeverity int, @ErrorState int 
		 SELECT @ErrorMessage = ERROR_MESSAGE(), @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE() 
		 RAISERROR(@ErrorMessage,@ErrorSeverity,@ErrorState) 
end catch
