CREATE procedure [dbo].[STP_INS_OBJETO] 
@obj_codigo nvarchar(200),
@obj_descricao nvarchar(255),
@obj_NumeroObjetoAte nvarchar(2) = '-1',
@obj_localizacaoAte nvarchar(2) = '-1',
@usu_id int,
@ip nvarchar(30)

--with encryption
as

begin try
		BEGIN TRAN T1
				set nocount on; 

				declare @tipos_masculinos nvarchar(MAX) = '14,15,16,24,25, 32, 33, 34, 36, 37, 38, 39, 40, 44, 45, 46, 47, 50, 52, 57, 72, 73, 76, 77, 78, 80, 81, 82, 84, 87, 90, 93, 104, 105, 106, 108, 110';
				declare @tipos_plural nvarchar(MAX) = '33, 39, 40, 43, 47, 56, 95, 98, 101, 106, 114' ;
				
				declare @n_pular int = 0;  
				declare @tipos_excecao varchar(max)= '45, 46, 53, 104, 105, 111, 126';


				declare @obj_NumeroObjetoAte_orig nvarchar(2) = @obj_NumeroObjetoAte;
				declare @atv_valores_log varchar(max)='';

				declare @actionDate datetime = getdate();

				declare @id_aux int = -1; declare @codigo_aux varchar(200) ;
				declare @id_aux2 int = -1; declare @codigo_aux2 varchar(200) ;
				declare @id_aux3 int = -1; declare @codigo_aux3 varchar(200) ;

				declare @n int =0;
				declare @pedaco nvarchar(100);
				declare @descricao nvarchar(255);

				declare @tip_id int =1; 
				declare @tip_nome nvarchar(150);
				declare @tip_pai int = -1; 

				declare @clo_id int =1;
				declare @rodovia_codigo varchar(100)= ''; declare @rodovia_id int = -1;
				declare @obra_arte_codigo varchar(100)= ''; declare @obra_arte_id int = -1; declare @obra_arte_descricao varchar(200)= '';
				declare @tipo_obra_arte_codigo varchar(100)= ''; declare @tipo_obra_arte_id int = -1;
				declare @subdivisão1_codigo varchar(100)= ''; declare @subdivisão1_id int = -1;
				declare @subdivisão2_codigo varchar(100)= ''; declare @subdivisão2_id int = -1;
				declare @subdivisão3_codigo varchar(100)= ''; declare @subdivisão3_id int = -1;
				declare @grupo_objetos_codigo varchar(100)= ''; declare @grupo_objetos_id int = -1; declare @grupo_objetos_nome varchar(255)= ''; declare @grupo_objetos_tipo varchar(25)= '';
				declare @num_objeto_codigo varchar(100)= ''; declare @num_objeto_id int = -1; declare @num_objeto_nome varchar(255)= ''; declare @num_objeto_numero varchar(2)= '';
				declare @elemento_codigo varchar(100)= ''; declare @elemento_id int = -1;

				declare @pos int = 0;
				declare @len int = 0;
				declare @delimiter char(1) = '-';

				declare @insercao_lote_existentes varchar(max) = '';

				set @obj_codigo = @obj_codigo + @delimiter;
				while (CHARINDEX(@delimiter, @obj_codigo, @pos + 1) > 0)
				begin
									set @len = CHARINDEX(@delimiter, @obj_codigo, @pos + 1) - @pos;
									set @pedaco = ltrim(rtrim(substring(@obj_codigo, @pos, @len)));
									set @n = @n + 1;

									--select @n, @pedaco

									if @n=1  -- rodovia
									  set @rodovia_codigo = @pedaco; 

									if @n=2 -- rodovia e obra de arte
									begin
										if (@pedaco = 'D' or @pedaco = 'E')
											set @rodovia_codigo = @rodovia_codigo + '-' + @pedaco;

										set @clo_id = 1;
										set @tip_id = dbo.fn_objeto_tipo_por_codigo (substring(rtrim(@rodovia_codigo),1,3) , @clo_id, 'tip_id', null);
										set @tip_nome = dbo.fn_objeto_tipo_por_codigo (substring(rtrim(@rodovia_codigo),1,3) , @clo_id, 'tip_nome', null);

										set @rodovia_id = ( select obj_id from dbo.tab_objetos where obj_deletado is null and obj_codigo = @rodovia_codigo);
										if (@rodovia_id is null)  -- NAO TEM RODOVIA entao insere
										begin
											set @rodovia_id = (select isnull(max(obj_id),0) +1 from dbo.tab_objetos);

												if (@rodovia_codigo + @delimiter = @obj_codigo)
												  set @descricao = @obj_descricao;
												else
												  set @descricao = (@tip_nome + ' ' + @rodovia_codigo);

											insert into dbo.tab_objetos (obj_id, clo_id, tip_id, obj_codigo, obj_descricao, obj_pai, obj_ativo, obj_data_criacao, obj_criado_por)
											values (@rodovia_id, @clo_id, @tip_id, @rodovia_codigo, @descricao, NULL, 1, @actionDate, @usu_id);

											-- insere log
											set @atv_valores_log = @atv_valores_log + 'Chave[obj_id]=[' + convert(varchar(3), @rodovia_id)  + '];';
											set @atv_valores_log = @atv_valores_log + 'Coluna[clo_id]:[' + convert(varchar(3), @clo_id)   + '];';
											set @atv_valores_log = @atv_valores_log + 'Coluna[tip_id]:[' + convert(varchar(3), @tip_id)   + '];';
											set @atv_valores_log = @atv_valores_log + 'Coluna[obj_codigo]:[' + @rodovia_codigo  + '];';
											set @atv_valores_log = @atv_valores_log + 'Coluna[obj_descricao]:[' + @descricao  + '];';
											set @atv_valores_log = @atv_valores_log + 'Coluna[obj_pai]:[nulo];';

											if (@n=2 and (@pedaco = 'D' or @pedaco = 'E'))
												set @n = 1; -- ajuste no contador
										end


										if (@n=2)  -- obra de arte (quilometragem)
										begin
											set @obra_arte_codigo = @rodovia_codigo + '-' + @pedaco;
											set @obra_arte_id = ( select obj_id from dbo.tab_objetos where obj_deletado is null and obj_codigo = @obra_arte_codigo);
											set @obra_arte_descricao = (select obj_descricao from dbo.tab_objetos where obj_codigo = @obra_arte_codigo);
											if @obra_arte_id is null -- SE NAO TEM OBRA DE ARTE, INSERE
											begin
												set @obra_arte_id = (select isnull(max(obj_id),0) +1 from dbo.tab_objetos);
												set @obra_arte_descricao = 'OAE Km ' + @pedaco + ' da Rodovia ' + @rodovia_codigo;
												set @clo_id = 2;
												set @tip_id = 28;

												if (@obra_arte_codigo + @delimiter  = @obj_codigo)
												  set @descricao = @obj_descricao;
												else
												  set @descricao = @obra_arte_descricao;

												insert into dbo.tab_objetos (obj_id, clo_id, tip_id, obj_codigo, obj_descricao, obj_pai, obj_ativo, obj_data_criacao, obj_criado_por)
												values (@obra_arte_id, @clo_id, @tip_id, @obra_arte_codigo, @obra_arte_descricao, @rodovia_id, 1, @actionDate, @usu_id);

												-- insere log
												set @atv_valores_log = @atv_valores_log + 'Chave[obj_id]=[' + convert(varchar(3), @obra_arte_id)  + '];';
												set @atv_valores_log = @atv_valores_log + 'Coluna[clo_id]:[' + convert(varchar(3), @clo_id)   + '];';
												set @atv_valores_log = @atv_valores_log + 'Coluna[tip_id]:[' + convert(varchar(3), @tip_id)   + '];';
												set @atv_valores_log = @atv_valores_log + 'Coluna[obj_codigo]:[' + @obra_arte_codigo  + '];';
												set @atv_valores_log = @atv_valores_log + 'Coluna[obj_descricao]:[' + @descricao  + '];';
												set @atv_valores_log = @atv_valores_log + 'Coluna[obj_pai]:[' + convert(varchar(3), @rodovia_id)   + '];';
											end
										end

									end

									if @n=3 -- tipo de obra de arte
									begin
										set @clo_id = 3;
										set @tip_id = dbo.fn_objeto_tipo_por_codigo (@pedaco, @clo_id, 'tip_id', null);
										set @tip_nome = dbo.fn_objeto_tipo_por_codigo (@pedaco, @clo_id, 'tip_nome', null);

										set @tipo_obra_arte_codigo = @obra_arte_codigo + '-' + @pedaco;
										set @tipo_obra_arte_id = ( select obj_id from dbo.tab_objetos where obj_deletado is null and obj_codigo = @tipo_obra_arte_codigo);
										if @tipo_obra_arte_id is null -- SE NAO TEM TIPO DE OBRA DE ARTE, INSERE
										begin

											set @tipo_obra_arte_id = (select isnull(max(obj_id),0) +1 from dbo.tab_objetos);

												if (@tipo_obra_arte_codigo + @delimiter = @obj_codigo) and (isnull(@obj_descricao,'') <> '')
												  set @descricao = @obj_descricao;
												else
												   set @descricao = (@tip_nome + ' ' + @tipo_obra_arte_codigo)

											insert into dbo.tab_objetos (obj_id, clo_id, tip_id, obj_codigo, obj_descricao, obj_pai, obj_ativo, obj_data_criacao, obj_criado_por)
											values (@tipo_obra_arte_id, @clo_id, @tip_id, @tipo_obra_arte_codigo, @descricao, @obra_arte_id, 1, @actionDate, @usu_id);

											-- insere log
											set @atv_valores_log = @atv_valores_log + 'Chave[obj_id]=[' + convert(varchar(3), @tipo_obra_arte_id)  + '];';
											set @atv_valores_log = @atv_valores_log + 'Coluna[clo_id]:[' + convert(varchar(3), @clo_id)   + '];';
											set @atv_valores_log = @atv_valores_log + 'Coluna[tip_id]:[' + convert(varchar(3), @tip_id)   + '];';
											set @atv_valores_log = @atv_valores_log + 'Coluna[obj_codigo]:[' + @tipo_obra_arte_codigo  + '];';
											set @atv_valores_log = @atv_valores_log + 'Coluna[obj_descricao]:[' + @descricao  + '];';
											set @atv_valores_log = @atv_valores_log + 'Coluna[obj_pai]:[' + convert(varchar(3), @obra_arte_id)   + '];';
										end



										-- *****************************  CHECA E INSERE OS IRMAOS ***************************************
											set @clo_id = 6;

											set @codigo_aux = @tipo_obra_arte_codigo + '-SE'; -- SUPERESTRUTURA
											set @id_aux = ( select obj_id from dbo.tab_objetos where obj_deletado is null and obj_codigo = @codigo_aux);
											if @id_aux is null -- SE NAO tem INSERE
											begin
												set @tip_nome = dbo.fn_objeto_tipo_por_codigo ('SE', @clo_id, 'tip_nome', null);
												set @id_aux = (select isnull(max(obj_id),0) +1 from dbo.tab_objetos);
												insert into dbo.tab_objetos (obj_id, clo_id, tip_id, obj_codigo, obj_descricao, obj_pai, obj_ativo, obj_data_criacao, obj_criado_por)
												values (@id_aux, @clo_id, 11, @codigo_aux, (@tip_nome + ' ' + @codigo_aux), @tipo_obra_arte_id, 1, @actionDate, @usu_id);

												-- insere log
												set @atv_valores_log = @atv_valores_log + 'Chave[obj_id]=[' + convert(varchar(3), @id_aux)  + '];';
												set @atv_valores_log = @atv_valores_log + 'Coluna[clo_id]:[' + convert(varchar(3), @clo_id)   + '];';
												set @atv_valores_log = @atv_valores_log + 'Coluna[tip_id]:[11];';
												set @atv_valores_log = @atv_valores_log + 'Coluna[obj_codigo]:[' + @codigo_aux  + '];';
												set @atv_valores_log = @atv_valores_log + 'Coluna[obj_descricao]:[' + @tip_nome + ' ' + @codigo_aux  + '];';
												set @atv_valores_log = @atv_valores_log + 'Coluna[obj_pai]:[' + convert(varchar(3), @tipo_obra_arte_id)   + '];';
											end
							
													--  ======CHECA FILHOS DE SUPERESTRUTURA ===============
														set @codigo_aux2 = @codigo_aux + '-FS';  -- FACE SUPERIOR
														set @id_aux2 = ( select obj_id from dbo.tab_objetos where obj_deletado is null and obj_codigo = @codigo_aux2);
														if @id_aux2 is null -- SE NAO tem INSERE
														begin
															set @tip_nome = dbo.fn_objeto_tipo_por_codigo ('FS', 7, 'tip_nome', 11);

															set @id_aux2 = (select isnull(max(obj_id),0) +1 from dbo.tab_objetos);
															insert into dbo.tab_objetos (obj_id, clo_id, tip_id, obj_codigo, obj_descricao, obj_pai, obj_ativo, obj_data_criacao, obj_criado_por)
															values (@id_aux2, 7, 15, @codigo_aux2, (@tip_nome + ' ' + @codigo_aux2), @id_aux, 1, @actionDate, @usu_id);

															-- insere log
															set @atv_valores_log = @atv_valores_log + 'Chave[obj_id]=[' + convert(varchar(3), @id_aux2)  + '];';
															set @atv_valores_log = @atv_valores_log + 'Coluna[clo_id]:[7];';
															set @atv_valores_log = @atv_valores_log + 'Coluna[tip_id]:[15];';
															set @atv_valores_log = @atv_valores_log + 'Coluna[obj_codigo]:[' + @codigo_aux2  + '];';
															set @atv_valores_log = @atv_valores_log + 'Coluna[obj_descricao]:[' + @tip_nome + ' ' + @codigo_aux2  + '];';
															set @atv_valores_log = @atv_valores_log + 'Coluna[obj_pai]:[' + convert(varchar(3), @id_aux)   + '];';

														end	
						
														set @codigo_aux2 = @codigo_aux + '-FI'; -- FACE INFERIOR
														set @id_aux2 = ( select obj_id from dbo.tab_objetos where obj_deletado is null and obj_codigo = @codigo_aux2);
														if @id_aux2 is null -- SE NAO tem INSERE
														begin
															set @tip_nome = dbo.fn_objeto_tipo_por_codigo ('FI', 7, 'tip_nome', 11);

															set @id_aux2 = (select isnull(max(obj_id),0) +1 from dbo.tab_objetos);
															insert into dbo.tab_objetos (obj_id, clo_id, tip_id, obj_codigo, obj_descricao, obj_pai, obj_ativo, obj_data_criacao, obj_criado_por)
															values (@id_aux2, 7, 16, @codigo_aux2, (@tip_nome + ' ' + @codigo_aux2), @id_aux, 1, @actionDate, @usu_id);

															-- insere log
															set @atv_valores_log = @atv_valores_log + 'Chave[obj_id]=[' + convert(varchar(3), @id_aux2)  + '];';
															set @atv_valores_log = @atv_valores_log + 'Coluna[clo_id]:[7];';
															set @atv_valores_log = @atv_valores_log + 'Coluna[tip_id]:[16];';
															set @atv_valores_log = @atv_valores_log + 'Coluna[obj_codigo]:[' + @codigo_aux2  + '];';
															set @atv_valores_log = @atv_valores_log + 'Coluna[obj_descricao]:[' + @tip_nome + ' ' + @codigo_aux2  + '];';
															set @atv_valores_log = @atv_valores_log + 'Coluna[obj_pai]:[' + convert(varchar(3), @id_aux)   + '];';
														end
							
						
											set @codigo_aux = @tipo_obra_arte_codigo + '-ME'; -- MESOESTRUTURA
											set @id_aux = ( select obj_id from dbo.tab_objetos where obj_deletado is null and obj_codigo = @codigo_aux);
											if @id_aux is null -- SE NAO tem INSERE
											begin
												set @tip_nome = dbo.fn_objeto_tipo_por_codigo ('ME', @clo_id, 'tip_nome', null);

												set @id_aux = (select isnull(max(obj_id),0) +1 from dbo.tab_objetos);
												insert into dbo.tab_objetos (obj_id, clo_id, tip_id, obj_codigo, obj_descricao, obj_pai, obj_ativo, obj_data_criacao, obj_criado_por)
												values (@id_aux, @clo_id, 12, @codigo_aux, (@tip_nome + ' ' + @codigo_aux), @tipo_obra_arte_id, 1, @actionDate, @usu_id);

												-- insere log
												set @atv_valores_log = @atv_valores_log + 'Chave[obj_id]=[' + convert(varchar(3), @id_aux)  + '];';
												set @atv_valores_log = @atv_valores_log + 'Coluna[clo_id]:[' + convert(varchar(3), @clo_id)   + '];';
												set @atv_valores_log = @atv_valores_log + 'Coluna[tip_id]:[12];';
												set @atv_valores_log = @atv_valores_log + 'Coluna[obj_codigo]:[' + @codigo_aux  + '];';
												set @atv_valores_log = @atv_valores_log + 'Coluna[obj_descricao]:[' + @tip_nome + ' ' + @codigo_aux  + '];';
												set @atv_valores_log = @atv_valores_log + 'Coluna[obj_pai]:[' + convert(varchar(3), @tipo_obra_arte_id)   + '];';
											end	

											set @codigo_aux = @tipo_obra_arte_codigo + '-IE'; -- INFRAESTRUTURA
											set @id_aux = ( select obj_id from dbo.tab_objetos where obj_deletado is null and obj_codigo = @codigo_aux);
											if @id_aux is null -- SE NAO tem INSERE
											begin
												set @tip_nome = dbo.fn_objeto_tipo_por_codigo ('IE', @clo_id, 'tip_nome', null);
												set @id_aux = (select isnull(max(obj_id),0) +1 from dbo.tab_objetos);
												insert into dbo.tab_objetos (obj_id, clo_id, tip_id, obj_codigo, obj_descricao, obj_pai, obj_ativo, obj_data_criacao, obj_criado_por)
												values (@id_aux, @clo_id, 13, @codigo_aux, (@tip_nome + ' ' + @codigo_aux), @tipo_obra_arte_id, 1, @actionDate, @usu_id);

												-- insere log
												set @atv_valores_log = @atv_valores_log + 'Chave[obj_id]=[' + convert(varchar(3), @id_aux)  + '];';
												set @atv_valores_log = @atv_valores_log + 'Coluna[clo_id]:[' + convert(varchar(3), @clo_id)   + '];';
												set @atv_valores_log = @atv_valores_log + 'Coluna[tip_id]:[13];';
												set @atv_valores_log = @atv_valores_log + 'Coluna[obj_codigo]:[' + @codigo_aux  + '];';
												set @atv_valores_log = @atv_valores_log + 'Coluna[obj_descricao]:[' + @tip_nome + ' ' + @codigo_aux  + '];';
												set @atv_valores_log = @atv_valores_log + 'Coluna[obj_pai]:[' + convert(varchar(3), @tipo_obra_arte_id)   + '];';
											end	

											-- ENCONTROS 
											set @codigo_aux = @tipo_obra_arte_codigo + '-ENC'; -- ENCONTRO
											set @id_aux = ( select obj_id from dbo.tab_objetos where obj_deletado is null and obj_codigo = @codigo_aux);
											if @id_aux is null -- SE NAO tem INSERE
											begin
												set @tip_nome = dbo.fn_objeto_tipo_por_codigo ('ENC', @clo_id, 'tip_nome', null);

												set @id_aux = (select isnull(max(obj_id),0) +1 from dbo.tab_objetos);
												insert into dbo.tab_objetos (obj_id, clo_id, tip_id, obj_codigo, obj_descricao, obj_pai, obj_ativo, obj_data_criacao, obj_criado_por)
												values (@id_aux, @clo_id, 14, @codigo_aux, (@tip_nome + ' ' + @codigo_aux), @tipo_obra_arte_id, 1, @actionDate, @usu_id);

												-- insere log
												set @atv_valores_log = @atv_valores_log + 'Chave[obj_id]=[' + convert(varchar(3), @id_aux)  + '];';
												set @atv_valores_log = @atv_valores_log + 'Coluna[clo_id]:[' + convert(varchar(3), @clo_id)   + '];';
												set @atv_valores_log = @atv_valores_log + 'Coluna[tip_id]:[14];';
												set @atv_valores_log = @atv_valores_log + 'Coluna[obj_codigo]:[' + @codigo_aux  + '];';
												set @atv_valores_log = @atv_valores_log + 'Coluna[obj_descricao]:[' + @tip_nome + ' ' + @codigo_aux  + '];';
												set @atv_valores_log = @atv_valores_log + 'Coluna[obj_pai]:[' + convert(varchar(3), @tipo_obra_arte_id)   + '];';
											end

											--  ========== CHECA FILHOS DE ENCONTRO ===========================
												set @codigo_aux2 = @codigo_aux + '-ET'; -- ESTRUTURA DE TERRA
												set @id_aux2 = ( select obj_id from dbo.tab_objetos where obj_deletado is null and obj_codigo = @codigo_aux2);
												if @id_aux2 is null -- SE NAO tem INSERE
												begin
													set @tip_nome = dbo.fn_objeto_tipo_por_codigo ('ET', 7, 'tip_nome', 14);

													set @id_aux2 = (select isnull(max(obj_id),0) +1 from dbo.tab_objetos);
													insert into dbo.tab_objetos (obj_id, clo_id, tip_id, obj_codigo, obj_descricao, obj_pai, obj_ativo, obj_data_criacao, obj_criado_por)
													values (@id_aux2, 7, 22, @codigo_aux2, (@tip_nome + ' ' + @codigo_aux2), @id_aux, 1, @actionDate, @usu_id);

													-- insere log
													set @atv_valores_log = @atv_valores_log + 'Chave[obj_id]=[' + convert(varchar(3), @id_aux2)  + '];';
													set @atv_valores_log = @atv_valores_log + 'Coluna[clo_id]:[7];';
													set @atv_valores_log = @atv_valores_log + 'Coluna[tip_id]:[22];';
													set @atv_valores_log = @atv_valores_log + 'Coluna[obj_codigo]:[' + @codigo_aux2  + '];';
													set @atv_valores_log = @atv_valores_log + 'Coluna[obj_descricao]:[' + @tip_nome + ' ' + @codigo_aux2  + '];';
													set @atv_valores_log = @atv_valores_log + 'Coluna[obj_pai]:[' + convert(varchar(3), @id_aux)   + '];';

												end	
						

/*														--  ========== CHECA FILHOS DE ESTRUTURA DE TERRA ===========================
														set @codigo_aux3 = @codigo_aux2 + '-TA'; -- TALUDE
														set @id_aux3 = ( select obj_id from dbo.tab_objetos where obj_deletado is null and obj_codigo = @codigo_aux3);
														if @id_aux3 is null -- SE NAO tem INSERE
														begin
															set @tip_nome = dbo.fn_objeto_tipo_por_codigo ('TA', 8, 'tip_nome', 22);
															set @id_aux3 = (select isnull(max(obj_id),0) +1 from dbo.tab_objetos);
															insert into dbo.tab_objetos (obj_id, clo_id, tip_id, obj_codigo, obj_descricao, obj_pai, obj_ativo, obj_data_criacao, obj_criado_por)
															values (@id_aux3, 8, 20, @codigo_aux3, (@tip_nome + ' ' + @codigo_aux3), @id_aux2, 1, @actionDate, @usu_id);

															-- insere log
															set @atv_valores_log = @atv_valores_log + 'Chave[obj_id]=[' + convert(varchar(3), @id_aux3)  + '];';
															set @atv_valores_log = @atv_valores_log + 'Coluna[clo_id]:[8];';
															set @atv_valores_log = @atv_valores_log + 'Coluna[tip_id]:[20];';
															set @atv_valores_log = @atv_valores_log + 'Coluna[obj_codigo]:[' + @codigo_aux3  + '];';
															set @atv_valores_log = @atv_valores_log + 'Coluna[obj_descricao]:[' + @tip_nome + ' ' + @codigo_aux3  + '];';
															set @atv_valores_log = @atv_valores_log + 'Coluna[obj_pai]:[' + convert(varchar(3), @id_aux2)   + '];';
														end	

														set @codigo_aux3 = @codigo_aux2 + '-ATA'; -- Aterro de Aproximação
														set @id_aux3 = ( select obj_id from dbo.tab_objetos where obj_deletado is null and obj_codigo = @codigo_aux3);
														if @id_aux3 is null -- SE NAO tem INSERE
														begin
															set @tip_nome = dbo.fn_objeto_tipo_por_codigo ('ATA', 8, 'tip_nome', 22);
															set @id_aux3 = (select isnull(max(obj_id),0) +1 from dbo.tab_objetos);
															insert into dbo.tab_objetos (obj_id, clo_id, tip_id, obj_codigo, obj_descricao, obj_pai, obj_ativo, obj_data_criacao, obj_criado_por)
															values (@id_aux3, 8, 21, @codigo_aux3, (@tip_nome + ' ' + @codigo_aux3), @id_aux2, 1, @actionDate, @usu_id);

															-- insere log
															set @atv_valores_log = @atv_valores_log + 'Chave[obj_id]=[' + convert(varchar(3), @id_aux3)  + '];';
															set @atv_valores_log = @atv_valores_log + 'Coluna[clo_id]:[8];';
															set @atv_valores_log = @atv_valores_log + 'Coluna[tip_id]:[21];';
															set @atv_valores_log = @atv_valores_log + 'Coluna[obj_codigo]:[' + @codigo_aux3  + '];';
															set @atv_valores_log = @atv_valores_log + 'Coluna[obj_descricao]:[' + @tip_nome + ' ' + @codigo_aux3  + '];';
															set @atv_valores_log = @atv_valores_log + 'Coluna[obj_pai]:[' + convert(varchar(3), @id_aux2)   + '];';

														end	

														set @codigo_aux3 = @codigo_aux2 + '-CONT'; -- CONTENÇÃO
														set @id_aux3 = ( select obj_id from dbo.tab_objetos where obj_deletado is null and obj_codigo = @codigo_aux3);
														if @id_aux3 is null -- SE NAO tem INSERE
														begin
															set @tip_nome = dbo.fn_objeto_tipo_por_codigo ('CONT', 8, 'tip_nome', 22);
															set @id_aux3 = (select isnull(max(obj_id),0) +1 from dbo.tab_objetos);
															insert into dbo.tab_objetos (obj_id, clo_id, tip_id, obj_codigo, obj_descricao, obj_pai, obj_ativo, obj_data_criacao, obj_criado_por)
															values (@id_aux3, 8, 26, @codigo_aux3, (@tip_nome + ' ' + @codigo_aux3), @id_aux2, 1, @actionDate, @usu_id);

															-- insere log
															set @atv_valores_log = @atv_valores_log + 'Chave[obj_id]=[' + convert(varchar(3), @id_aux3)  + '];';
															set @atv_valores_log = @atv_valores_log + 'Coluna[clo_id]:[8];';
															set @atv_valores_log = @atv_valores_log + 'Coluna[tip_id]:[26];';
															set @atv_valores_log = @atv_valores_log + 'Coluna[obj_codigo]:[' + @codigo_aux3  + '];';
															set @atv_valores_log = @atv_valores_log + 'Coluna[obj_descricao]:[' + @tip_nome + ' ' + @codigo_aux3  + '];';
															set @atv_valores_log = @atv_valores_log + 'Coluna[obj_pai]:[' + convert(varchar(3), @id_aux2)   + '];';
														end	
														-- ===================================================================================
*/
												set @codigo_aux2 = @codigo_aux + '-EC'; -- ESTRUTURAS DE CONCRETO
												set @id_aux2 = ( select obj_id from dbo.tab_objetos where obj_deletado is null and obj_codigo = @codigo_aux2);
												if @id_aux2 is null -- SE NAO tem INSERE
												begin
													set @tip_nome = dbo.fn_objeto_tipo_por_codigo ('EC', 7, 'tip_nome', 14);

													set @id_aux2 = (select isnull(max(obj_id),0) +1 from dbo.tab_objetos);
													insert into dbo.tab_objetos (obj_id, clo_id, tip_id, obj_codigo, obj_descricao, obj_pai, obj_ativo, obj_data_criacao, obj_criado_por)
													values (@id_aux2, 7, 23, @codigo_aux2, (@tip_nome + ' ' + @codigo_aux2), @id_aux, 1, @actionDate, @usu_id);

													-- insere log
													set @atv_valores_log = @atv_valores_log + 'Chave[obj_id]=[' + convert(varchar(3), @id_aux2)  + '];';
													set @atv_valores_log = @atv_valores_log + 'Coluna[clo_id]:7];';
													set @atv_valores_log = @atv_valores_log + 'Coluna[tip_id]:[23];';
													set @atv_valores_log = @atv_valores_log + 'Coluna[obj_codigo]:[' + @codigo_aux2  + '];';
													set @atv_valores_log = @atv_valores_log + 'Coluna[obj_descricao]:[' + @tip_nome + ' ' + @codigo_aux2  + '];';
													set @atv_valores_log = @atv_valores_log + 'Coluna[obj_pai]:[' + convert(varchar(3), @id_aux)   + '];';
												end	

/*								
														--  ========== CHECA FILHOS DE ESTRUTURA DE CONCRETO ===========================
														set @codigo_aux3 = @codigo_aux2 + '-PA'; -- PAREDE
														set @id_aux3 = ( select obj_id from dbo.tab_objetos where obj_deletado is null and obj_codigo = @codigo_aux3);
														if @id_aux3 is null -- SE NAO tem INSERE
														begin
															set @tip_nome = dbo.fn_objeto_tipo_por_codigo ('PA', 8, 'tip_nome', 23);

															set @id_aux3 = (select isnull(max(obj_id),0) +1 from dbo.tab_objetos);
															insert into dbo.tab_objetos (obj_id, clo_id, tip_id, obj_codigo, obj_descricao, obj_pai, obj_ativo, obj_data_criacao, obj_criado_por)
															values (@id_aux3, 8, 29, @codigo_aux3, (@tip_nome + ' ' + @codigo_aux3), @id_aux2, 1, @actionDate, @usu_id);

															-- insere log
															set @atv_valores_log = @atv_valores_log + 'Chave[obj_id]=[' + convert(varchar(3), @id_aux3)  + '];';
															set @atv_valores_log = @atv_valores_log + 'Coluna[clo_id]:[8];';
															set @atv_valores_log = @atv_valores_log + 'Coluna[tip_id]:[29];';
															set @atv_valores_log = @atv_valores_log + 'Coluna[obj_codigo]:[' + @codigo_aux3  + '];';
															set @atv_valores_log = @atv_valores_log + 'Coluna[obj_descricao]:[' + @tip_nome + ' ' + @codigo_aux3  + '];';
															set @atv_valores_log = @atv_valores_log + 'Coluna[obj_pai]:[' + convert(varchar(3), @id_aux2)   + '];';
														end	

														set @codigo_aux3 = @codigo_aux2 + '-MA'; -- Muro de Ala
														set @id_aux3 = ( select obj_id from dbo.tab_objetos where obj_deletado is null and obj_codigo = @codigo_aux3);
														if @id_aux3 is null -- SE NAO tem INSERE
														begin
															set @tip_nome = dbo.fn_objeto_tipo_por_codigo ('MA', 8, 'tip_nome', 23);
															set @id_aux3 = (select isnull(max(obj_id),0) +1 from dbo.tab_objetos);
															insert into dbo.tab_objetos (obj_id, clo_id, tip_id, obj_codigo, obj_descricao, obj_pai, obj_ativo, obj_data_criacao, obj_criado_por)
															values (@id_aux3, 8, 30, @codigo_aux3, (@tip_nome + ' ' + @codigo_aux3), @id_aux2, 1, @actionDate, @usu_id);

															-- insere log
															set @atv_valores_log = @atv_valores_log + 'Chave[obj_id]=[' + convert(varchar(3), @id_aux3)  + '];';
															set @atv_valores_log = @atv_valores_log + 'Coluna[clo_id]:[8];';
															set @atv_valores_log = @atv_valores_log + 'Coluna[tip_id]:[30];';
															set @atv_valores_log = @atv_valores_log + 'Coluna[obj_codigo]:[' + @codigo_aux3  + '];';
															set @atv_valores_log = @atv_valores_log + 'Coluna[obj_descricao]:[' + @tip_nome + ' ' + @codigo_aux3  + '];';
															set @atv_valores_log = @atv_valores_log + 'Coluna[obj_pai]:[' + convert(varchar(3), @id_aux2)   + '];';
														end	

														set @codigo_aux3 = @codigo_aux2 + '-CO'; -- Cortina
														set @id_aux3 = ( select obj_id from dbo.tab_objetos where obj_deletado is null and obj_codigo = @codigo_aux3);
														if @id_aux3 is null -- SE NAO tem INSERE
														begin
															set @tip_nome = dbo.fn_objeto_tipo_por_codigo ('CO', 8, 'tip_nome', 23);
															set @id_aux3 = (select isnull(max(obj_id),0) +1 from dbo.tab_objetos);
															insert into dbo.tab_objetos (obj_id, clo_id, tip_id, obj_codigo, obj_descricao, obj_pai, obj_ativo, obj_data_criacao, obj_criado_por)
															values (@id_aux3, 8, 31, @codigo_aux3, (@tip_nome + ' ' + @codigo_aux3), @id_aux2, 1, @actionDate, @usu_id);

															-- insere log
															set @atv_valores_log = @atv_valores_log + 'Chave[obj_id]=[' + convert(varchar(3), @id_aux3)  + '];';
															set @atv_valores_log = @atv_valores_log + 'Coluna[clo_id]:[8];';
															set @atv_valores_log = @atv_valores_log + 'Coluna[tip_id]:[31];';
															set @atv_valores_log = @atv_valores_log + 'Coluna[obj_codigo]:[' + @obj_codigo  + '];';
															set @atv_valores_log = @atv_valores_log + 'Coluna[obj_descricao]:[' + @obj_descricao  + '];';
															set @atv_valores_log = @atv_valores_log + 'Coluna[obj_pai]:[' + convert(varchar(3), @id_aux2)   + '];';
														end	
														-- ===================================================================================
*/

												set @codigo_aux2 = @codigo_aux + '-AC'; --acesso
												set @id_aux2 = ( select obj_id from dbo.tab_objetos where obj_deletado is null and obj_codigo = @codigo_aux2);
												if @id_aux2 is null -- SE NAO tem INSERE
												begin
													set @tip_nome = dbo.fn_objeto_tipo_por_codigo ('AC', 7, 'tip_nome', 14);
													set @id_aux2 = (select isnull(max(obj_id),0) +1 from dbo.tab_objetos);
													insert into dbo.tab_objetos (obj_id, clo_id, tip_id, obj_codigo, obj_descricao, obj_pai, obj_ativo, obj_data_criacao, obj_criado_por)
													values (@id_aux2, 7, 24, @codigo_aux2, (@tip_nome + ' ' + @codigo_aux2), @id_aux, 1, @actionDate, @usu_id);

													-- insere log
													set @atv_valores_log = @atv_valores_log + 'Chave[obj_id]=[' + convert(varchar(3), @id_aux2)  + '];';
													set @atv_valores_log = @atv_valores_log + 'Coluna[clo_id]:[7];';
													set @atv_valores_log = @atv_valores_log + 'Coluna[tip_id]:[26];';
													set @atv_valores_log = @atv_valores_log + 'Coluna[obj_codigo]:[' + @codigo_aux2  + '];';
													set @atv_valores_log = @atv_valores_log + 'Coluna[obj_descricao]:[' + @tip_nome + ' ' + @codigo_aux2  + '];';
													set @atv_valores_log = @atv_valores_log + 'Coluna[obj_pai]:[' + convert(varchar(3), @id_aux)   + '];';
												end	

									end

									if @n=4  -- subdivisao1
									begin
										set @clo_id = 6;
										set @tip_id = dbo.fn_objeto_tipo_por_codigo (@pedaco, @clo_id, 'tip_id', null);
										set @tip_nome = dbo.fn_objeto_tipo_por_codigo (@pedaco, @clo_id, 'tip_nome', null);

										set @subdivisão1_codigo = @tipo_obra_arte_codigo + '-' + @pedaco;
										set @subdivisão1_id = ( select obj_id from dbo.tab_objetos where obj_deletado is null and obj_codigo = @subdivisão1_codigo);
										if @subdivisão1_id is null -- SE NAO TEM subdivisão1, INSERE
										begin
											set @subdivisão1_id = (select isnull(max(obj_id),0) +1 from dbo.tab_objetos);

												if (@subdivisão1_codigo + @delimiter  = @obj_codigo)
												  set @descricao = @obj_descricao;
												else
												  set @descricao = (@tip_nome + ' ' + @subdivisão1_codigo);

											insert into dbo.tab_objetos (obj_id, clo_id, tip_id, obj_codigo, obj_descricao, obj_pai, obj_ativo, obj_data_criacao, obj_criado_por)
											values (@subdivisão1_id, @clo_id, @tip_id, @subdivisão1_codigo, @descricao , @tipo_obra_arte_id, 1, @actionDate, @usu_id);

											-- insere log
											set @atv_valores_log = @atv_valores_log + 'Chave[obj_id]=[' + convert(varchar(3), @subdivisão1_id)  + '];';
											set @atv_valores_log = @atv_valores_log + 'Coluna[clo_id]:[' + convert(varchar(3), @clo_id)   + '];';
											set @atv_valores_log = @atv_valores_log + 'Coluna[tip_id]:[' + convert(varchar(3), @tip_id)   + '];';
											set @atv_valores_log = @atv_valores_log + 'Coluna[obj_codigo]:[' + @subdivisão1_codigo  + '];';
											set @atv_valores_log = @atv_valores_log + 'Coluna[obj_descricao]:[' + @descricao  + '];';
											set @atv_valores_log = @atv_valores_log + 'Coluna[obj_pai]:[' + convert(varchar(3), @tipo_obra_arte_id)   + '];';
										end	

										set @tip_pai = @tip_id;
								
										-- AJUSTE: se for mesoestrutura ou infraestrutura pula direto para classe Grupo de Objetos
										if (@clo_id = 6 and (@tip_id = 12 or @tip_id = 13))
										begin
											set @subdivisão3_id = @subdivisão1_id;
											set @subdivisão3_codigo = @subdivisão1_codigo;
											set @n = 6;
											set @pos = CHARINDEX(@delimiter, @obj_codigo, @pos + @len) + 1;

											set @tip_pai = @tip_id;
											continue;
										end
									end

									if @n=5 and @tipo_obra_arte_id > 0 -- subdivisao2
									begin
										set @clo_id = 7;
										set @tip_id = dbo.fn_objeto_tipo_por_codigo (@pedaco, @clo_id, 'tip_id', @tip_pai);
										set @tip_nome = dbo.fn_objeto_tipo_por_codigo (@pedaco, @clo_id, 'tip_nome', @tip_pai);

										set @subdivisão2_codigo = @subdivisão1_codigo + '-' + @pedaco;
										set @subdivisão2_id = ( select obj_id from dbo.tab_objetos where obj_deletado is null and obj_codigo = @subdivisão2_codigo);
										if @subdivisão2_id is null -- SE NAO tem subdivisão2, INSERE
										begin
											set @subdivisão2_id = (select isnull(max(obj_id),0) +1 from dbo.tab_objetos);

												if (@subdivisão2_codigo + @delimiter  = @obj_codigo)
												  set @descricao = @obj_descricao;
												else
												  set @descricao = (@tip_nome + ' ' + @subdivisão2_codigo);

											insert into dbo.tab_objetos (obj_id, clo_id, tip_id, obj_codigo, obj_descricao, obj_pai, obj_ativo, obj_data_criacao, obj_criado_por)
											values (@subdivisão2_id, @clo_id, @tip_id, @subdivisão2_codigo, @descricao, @subdivisão1_id, 1, @actionDate, @usu_id);

											-- insere log
											set @atv_valores_log = @atv_valores_log + 'Chave[obj_id]=[' + convert(varchar(3), @subdivisão2_id)  + '];';
											set @atv_valores_log = @atv_valores_log + 'Coluna[clo_id]:[' + convert(varchar(3), @clo_id)   + '];';
											set @atv_valores_log = @atv_valores_log + 'Coluna[tip_id]:[' + convert(varchar(3), @tip_id)   + '];';
											set @atv_valores_log = @atv_valores_log + 'Coluna[obj_codigo]:[' + @subdivisão2_codigo  + '];';
											set @atv_valores_log = @atv_valores_log + 'Coluna[obj_descricao]:[' + @descricao  + '];';
											set @atv_valores_log = @atv_valores_log + 'Coluna[obj_pai]:[' + convert(varchar(3), @subdivisão1_id)   + '];';
										end	
										set @tip_pai = @tip_id;
						
										-- AJUSTE: se for superestrutura/Tabuleiro Face Superior ou Inferior ou encontros/acessos pula direto para classe Grupo de Objetos
										if (@tip_id = 15 or @tip_id = 16 or @tip_id = 24)
										begin
											set @subdivisão3_id = @subdivisão2_id;
											set @subdivisão3_codigo = @subdivisão2_codigo;
											set @n = 6;
											set @pos = CHARINDEX(@delimiter, @obj_codigo, @pos + @len) + 1;

											set @tip_pai = @tip_id;

											continue;
										end

									end

									if @n=6 -- subdivisao3
									begin
										set @clo_id = 8;
										set @tip_id = dbo.fn_objeto_tipo_por_codigo (@pedaco, @clo_id, 'tip_id', @tip_pai);
										set @tip_nome = dbo.fn_objeto_tipo_por_codigo (@pedaco, @clo_id, 'tip_nome', @tip_pai);

										set @subdivisão3_codigo = @subdivisão2_codigo + '-' + @pedaco;
										set @subdivisão3_id = ( select obj_id from dbo.tab_objetos where obj_deletado is null and obj_codigo = @subdivisão3_codigo);
										if @subdivisão3_id is null -- SE NAO tem subdivisão3, INSERE
										begin
											set @subdivisão3_id = (select isnull(max(obj_id),0) +1 from dbo.tab_objetos);

												if (@subdivisão3_codigo + @delimiter  = @obj_codigo)
												  set @descricao = @obj_descricao;
												else
												  set @descricao = (@tip_nome + ' ' + @subdivisão3_codigo);

											insert into dbo.tab_objetos (obj_id, clo_id, tip_id, obj_codigo, obj_descricao, obj_pai, obj_ativo, obj_data_criacao, obj_criado_por)
											values (@subdivisão3_id, @clo_id, @tip_id, @subdivisão3_codigo, @descricao, @subdivisão2_id, 1, @actionDate, @usu_id);

											-- insere log
											set @atv_valores_log = @atv_valores_log + 'Chave[obj_id]=[' + convert(varchar(3), @subdivisão3_id)  + '];';
											set @atv_valores_log = @atv_valores_log + 'Coluna[clo_id]:[' + convert(varchar(3), @clo_id)   + '];';
											set @atv_valores_log = @atv_valores_log + 'Coluna[tip_id]:[' + convert(varchar(3), @tip_id)   + '];';
											set @atv_valores_log = @atv_valores_log + 'Coluna[obj_codigo]:[' + @subdivisão3_codigo  + '];';
											set @atv_valores_log = @atv_valores_log + 'Coluna[obj_descricao]:[' + @descricao  + '];';
											set @atv_valores_log = @atv_valores_log + 'Coluna[obj_pai]:[' + convert(varchar(3), @subdivisão2_id)   + '];';
										end	

										set @tip_pai = @tip_id;
									end

									if @n=7 -- grupo de objetos
									begin
										set @clo_id = 9;
										set @tip_id = dbo.fn_objeto_tipo_por_codigo (@pedaco, @clo_id, 'tip_id', @tip_pai); 
										set @tip_nome = dbo.fn_objeto_tipo_por_codigo (@pedaco, @clo_id, 'tip_nome', @tip_pai); 
										set @grupo_objetos_tipo = @pedaco;
										set @grupo_objetos_nome = dbo.fn_objeto_tipo_por_codigo (@pedaco, @clo_id, 'tip_nome', @tip_pai); 
										
										declare @s varchar(1) = '';
										if dbo.checaValueInList (@tip_id, @tipos_plural) = 1
											set @s = 's';

										declare @do varchar(2) = 'da';
										if dbo.checaValueInList (@tip_id, @tipos_masculinos) = 1
											set @do = 'do';


										set @grupo_objetos_codigo = @subdivisão3_codigo + '-' + @pedaco;
										set @grupo_objetos_id = ( select obj_id from dbo.tab_objetos where obj_deletado is null and obj_codigo = @grupo_objetos_codigo);
										
										if @grupo_objetos_id is null -- SE NAO tem INSERE
										begin
											set @grupo_objetos_id = (select isnull(max(obj_id),0) +1 from dbo.tab_objetos);

												if (@grupo_objetos_codigo + @delimiter  = @obj_codigo) and (@obj_descricao <> '')
												  set @descricao = @obj_descricao;
												else
												  set @descricao = (@tip_nome + ' ' + @grupo_objetos_codigo);

											insert into dbo.tab_objetos (obj_id, clo_id, tip_id, obj_codigo, obj_descricao, obj_pai, obj_ativo, obj_data_criacao, obj_criado_por)
											values (@grupo_objetos_id, @clo_id, @tip_id, @grupo_objetos_codigo, @descricao, @subdivisão3_id, 1, @actionDate, @usu_id);

											-- insere log
											set @atv_valores_log = @atv_valores_log + 'Chave[obj_id]=[' + convert(varchar(3), @grupo_objetos_id)  + '];';
											set @atv_valores_log = @atv_valores_log + 'Coluna[clo_id]:[' + convert(varchar(3), @clo_id)   + '];';
											set @atv_valores_log = @atv_valores_log + 'Coluna[tip_id]:[' + convert(varchar(3), @tip_id)   + '];';
											set @atv_valores_log = @atv_valores_log + 'Coluna[obj_codigo]:[' + @grupo_objetos_codigo  + '];';
											set @atv_valores_log = @atv_valores_log + 'Coluna[obj_descricao]:[' + @descricao  + '];';
											set @atv_valores_log = @atv_valores_log + 'Coluna[obj_pai]:[' + convert(varchar(3), @subdivisão3_id)   + '];';

										end	

										if (dbo.checaValueInList(@tip_id, @tipos_excecao) = 1)
										begin
										  set @num_objeto_id = @grupo_objetos_id;
										  set @num_objeto_codigo = @grupo_objetos_codigo;
										  set @n_pular = 1;
										  set @n = @n +1;
									    end
									end
									
									if @n=8 and @n_pular=0 -- numero do objeto
									begin
										set @clo_id = 10;
										set @tip_id = 25; --dbo.fn_objeto_tipo_por_codigo (@pedaco, @clo_id, 'tip_id', @tip_pai); 
										set @tip_nome = 'Número de Objeto';

										set @num_objeto_numero = @pedaco;
										set @num_objeto_codigo = @grupo_objetos_codigo + '-' + @pedaco;

										declare @s_objnum_codigoDe varchar(2) = right(@obj_codigo,2);
										declare @objnum_codigoDe int = 0;
										declare @prefixonum varchar(50) = @grupo_objetos_codigo; --left(@obj_codigo, len(@obj_codigo)-2);
										declare @objnum_codigoX varchar(200) = '';
										declare @objnum_descricaoX varchar(255) = '';
										declare @inum int = 0;

										-- checa se é inserção em lote
										if ISNUMERIC(@num_objeto_numero)=1 and ISNUMERIC(@obj_NumeroObjetoAte)=1 
											if convert(int,@num_objeto_numero) >  convert(int,@obj_NumeroObjetoAte)
											  set @obj_NumeroObjetoAte = @num_objeto_numero;
			    
										if ISNUMERIC(@num_objeto_numero)=1
											set @inum = convert(int,@num_objeto_numero);

										while  @inum <= convert(int,@obj_NumeroObjetoAte)
										begin
												-- monta o codigo do objeto
												set @objnum_codigoX = @prefixonum + '-' + right('00' + convert(varchar(2), @inum),2);

												set @num_objeto_id = ( select obj_id from dbo.tab_objetos where obj_deletado is null and obj_codigo = @objnum_codigoX);
												if @num_objeto_id is null -- SE NAO tem INSERE
												begin
													--if (@num_objeto_codigo + @delimiter = @obj_codigo) and (@obj_descricao <> '')
													--  set @descricao = @obj_descricao;
													--else
													set @descricao = (@grupo_objetos_nome + ' #' + right('00' + convert(varchar(2), @inum),2) + ' (' + @objnum_codigoX + ')');
													set @num_objeto_nome = @descricao;

													set @num_objeto_id = (select isnull(max(obj_id),0) +1 from dbo.tab_objetos);
													insert into dbo.tab_objetos (obj_id, clo_id, tip_id, obj_codigo, obj_descricao, obj_pai, obj_ativo, obj_data_criacao, obj_criado_por)
													values (@num_objeto_id, @clo_id, @tip_id, @objnum_codigoX, @descricao, @grupo_objetos_id, 1, @actionDate, @usu_id);

													-- insere log
													set @atv_valores_log = @atv_valores_log + 'Chave[obj_id]=[' + convert(varchar(3), @num_objeto_id)  + '];';
													set @atv_valores_log = @atv_valores_log + 'Coluna[clo_id]:[' + convert(varchar(3), @clo_id)   + '];';
													set @atv_valores_log = @atv_valores_log + 'Coluna[tip_id]:[' + convert(varchar(3), @tip_id)   + '];';
													set @atv_valores_log = @atv_valores_log + 'Coluna[obj_codigo]:[' + @num_objeto_codigo  + '];';
													set @atv_valores_log = @atv_valores_log + 'Coluna[obj_descricao]:[' + @descricao  + '];';
													set @atv_valores_log = @atv_valores_log + 'Coluna[obj_pai]:[' + convert(varchar(3), @grupo_objetos_id)   + '];';
											    end
												else
												begin
												  if @obj_NumeroObjetoAte_orig <> '-1'
													begin
														set @insercao_lote_existentes = @insercao_lote_existentes + ' Objeto [' + @objnum_codigoX + '] já está cadastrado.';
													end
												end


											set @inum = @inum + 1;
										end	
									end


									if @n=9 -- localizacao
									begin							
										-- insere o elemento
										declare @mudarDescricao int = 0;
										set @elemento_codigo = @num_objeto_codigo + '-' + @pedaco;
										if (@elemento_codigo + @delimiter = @obj_codigo) and (@obj_descricao <> '')
											set @mudarDescricao = 1;
										else
											set @mudarDescricao = 0;

											set @clo_id = 11; -- classe localizacao
											set @tip_id = 115; -- tipo elemento
											--set @tip_nome = 'Localização';

											set @obj_codigo = LEFT(@obj_codigo, LEN(@obj_codigo) - 1) ; -- remove o ultimo caracter
											declare @s_obj_codigoDe varchar(2) = right(@obj_codigo,2);
											declare @obj_codigoDe int = 0;

											declare @prefixo_semNumObj varchar(50) = left(@obj_codigo, charindex(@grupo_objetos_tipo, @obj_codigo)-1) + @grupo_objetos_tipo ; -- left(@obj_codigo, len(@obj_codigo)-7);

											declare @obj_codigoX varchar(200) = '';
											declare @obj_descricaoX varchar(255) = '';
											declare @i int = 0;
											declare @vcvgea varchar(2) = substring(@pedaco,1,2);

											if (substring(@pedaco,1,2) = 'VG')
												set @tip_nome = 'Vão em Grelha'; 
											  else
												if (substring(@pedaco,1,2) = 'VC')
													set @tip_nome = 'Vão Caixão Perdido'; 
												  else
													if (substring(@pedaco,1,1) = 'V')
														set @tip_nome = 'Vão'; 
													  else
														if (substring(@pedaco,1,1) = 'E')
															set @tip_nome = 'Encontro'; 
														  else
															if (substring(@pedaco,1,1) = 'A')
																set @tip_nome = 'Apoio'; 										
											if (@tip_nome = 'Encontro' or @tip_nome = 'Apoio' or @tip_nome='Vão')
												set @vcvgea = substring(@pedaco,1,1);

											-- checa se é inserção em lote
											 if ISNUMERIC(@s_obj_codigoDe)=1 and ISNUMERIC(@obj_localizacaoAte)=1 
												 if convert(int,@s_obj_codigoDe) >=  convert(int,@obj_localizacaoAte)
												   set @obj_localizacaoAte = @s_obj_codigoDe;			    
					
											 -- contorno para excecao
											 if @n_pular=1
												 begin
													set @inum = 0;
													set @num_objeto_numero = 0;
													set @obj_NumeroObjetoAte = 0;
												 end
											 else											      
												if ISNUMERIC(@num_objeto_numero)=1
													set @inum = convert(int,@num_objeto_numero);
												else
													set @inum = 0;

											 declare @snum varchar(2) = '';
										     while  @inum <= convert(int,@obj_NumeroObjetoAte)
										     begin

												-- monta o codigo do objeto
												 if @n_pular=1
													set @snum = '';
												 else 
											        set @snum =  right('00' + convert(varchar(2), @inum),2);

												set @objnum_codigoX = @prefixo_semNumObj + '-' + @snum;
												
												if @n_pular <> 1
													set @objnum_codigoX = @objnum_codigoX + '-';

												--set @num_objeto_id = ( select obj_id from dbo.tab_objetos where obj_deletado is null and obj_codigo = @objnum_codigoX);
												--set @prefixo = left(@obj_codigo, len(@obj_codigo)-7) +  @snum + '-' + @vcvgea ;

												if ISNUMERIC(@s_obj_codigoDe) = 1
													set @i = convert(int,@s_obj_codigoDe);
											
												  while  @i <= convert(int,@obj_localizacaoAte)
													begin
														-- monta o codigo do objeto														
														set @obj_codigoX = @objnum_codigoX + @vcvgea + right('00' + convert(varchar(2), @i),2);
														set @elemento_id = ( select obj_id from dbo.tab_objetos where obj_deletado is null and obj_codigo = @obj_codigoX);
														if @elemento_id is null -- SE NAO TEM, INSERE
														begin
															--if (@mudarDescricao = 0)
														--		set @obj_descricaoX = @tip_nome + ' #' + right('00' + convert(varchar(2), @i),2) + ' ' + @do + @s + ' ' + @grupo_objetos_nome + ' #' + @num_objeto_numero + ' (' + @obj_codigoX + ')' ;
																if @n_pular=1
														 			set @obj_descricaoX = @tip_nome + ' #' + right('00' + convert(varchar(2), @i),2) + ' ' + @do + @s + ' ' + @grupo_objetos_nome; -- + ' (' + @grupo_objetos_codigo + ')' ;
																else
																	set @obj_descricaoX = @tip_nome + ' #' + right('00' + convert(varchar(2), @i),2) + ' ' + @do + @s + ' ' + @grupo_objetos_nome + ' #' + right('00' + convert(varchar(2), @inum),2); 
															--else
															--	set @obj_descricaoX = replace(@obj_descricao, @obj_codigo, @obj_codigoX);

															set @elemento_id = (select isnull(max(obj_id),0) +1 from dbo.tab_objetos);
															insert into dbo.tab_objetos (obj_id, clo_id, tip_id, obj_codigo, obj_descricao, obj_pai, obj_ativo, obj_data_criacao, obj_criado_por)
															values (@elemento_id, @clo_id, @tip_id, @obj_codigoX, @obj_descricaoX, @num_objeto_id, 1, @actionDate, @usu_id);

															-- insere log
															set @atv_valores_log = @atv_valores_log + 'Chave[obj_id]=[' + convert(varchar(3), @elemento_id)  + '];';
															set @atv_valores_log = @atv_valores_log + 'Coluna[clo_id]:[' + convert(varchar(3), @clo_id)   + '];';
															set @atv_valores_log = @atv_valores_log + 'Coluna[tip_id]:[' + convert(varchar(3), @tip_id)   + '];';
															set @atv_valores_log = @atv_valores_log + 'Coluna[obj_codigo]:[' + @obj_codigoX  + '];';
															set @atv_valores_log = @atv_valores_log + 'Coluna[obj_descricao]:[' + @obj_descricaoX  + '];';
															set @atv_valores_log = @atv_valores_log + 'Coluna[obj_pai]:[' + convert(varchar(3), @num_objeto_id)   + '];';
														end
														else
															begin
																set @insercao_lote_existentes = @insercao_lote_existentes + ' Objeto [' + @obj_codigoX + '] já está cadastrado.';
															end

														set @i = @i + 1;
													 end;

												set @inum = @inum+1;
											 end
									end

									-- continuacao do while
									set @pos = CHARINDEX(@delimiter, @obj_codigo, @pos + @len) + 1;
				end

				-- insere log
				declare @tabela varchar(300) = 'tab_objetos';
				declare @tra_transacao_id int = 4; -- 4= insercao
				declare @mod_modulo_id_log int = 140; -- cadastro de objetos

				--set @atv_valores_log = 'Módulo Novo Objeto. ' + @atv_valores_log ; 

				exec dbo.STP_INS_LOGSISTEMA @tra_transacao_id, @usu_id, @mod_modulo_id_log,	@atv_valores_log,	@ip	

				-- retorno
				declare @saida int = -1;
				if (@obra_arte_id < 0)
					set @saida = @rodovia_id;
				else
					if (@tipo_obra_arte_id < 0)
						set @saida = @obra_arte_id;
					else
						if (@subdivisão1_id < 0)
							set @saida = @tipo_obra_arte_id;
						else
							if (@subdivisão2_id < 0)
								set @saida = @subdivisão1_id;
							else
								if (@subdivisão3_id < 0)
									set @saida = @subdivisão2_id;
								else
									if (@grupo_objetos_id < 0)
										set @saida = @subdivisão3_id;
									else
										if (@num_objeto_id < 0)
											set @saida = @grupo_objetos_id;
										else
											if (@elemento_id < 0)
												set @saida = @num_objeto_id;
											else
												set @saida = @elemento_id;

				set nocount off; 
		COMMIT TRAN T1

		select convert(varchar(100),@saida) + ';' + @insercao_lote_existentes as saida;
end try
begin catch
		ROLLBACK TRAN T1
            PRINT 'The following error has occurred:  ' + ERROR_MESSAGE()

		--Raise an error
		 DECLARE @ErrorMessage varchar(255), @ErrorSeverity int, @ErrorState int 
		 SELECT @ErrorMessage = ERROR_MESSAGE(), @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE() 
		 RAISERROR(@ErrorMessage,@ErrorSeverity,@ErrorState) 
end catch
