CREATE PROCEDURE [dbo].[STP_SEL_FICHA_INSPECAO_CADASTRAL] 
@obj_id int,
@ord_id int = 0
as 
begin 

SET FMTONLY OFF

declare @atr_id int = null

set nocount on
		-- checa se as tmp table existe e a exclui
		IF OBJECT_ID('tempdb..#temp1') IS NOT NULL
			DROP TABLE #temp1;

		-- checa se as tmp table existe e a exclui
		IF OBJECT_ID('tempdb..#temp2') IS NOT NULL
			DROP TABLE #temp2;

		declare @obj_id_orig int = @obj_id;


		-- cria lista de obj_ids subindo a hierarquia
		declare @obj_id_Rodovia int = -1
		declare @obj_id_ObraDeArte int = -1
		declare @obj_id_TipoObraDeArte int = -1

		declare @clo_id_aux int = -1

		 -- se a classe do @obj_id for 2 (obra de arte)  ou 1 (rodovia) entao procura-os pois estao hierarquia abaixo de obj_id
		 		select  @clo_id_aux = clo_id
				from dbo.tab_objetos
				where obj_id = @obj_id_orig;

				if (@clo_id_aux = 1) -- rodovia
				begin
					set @obj_id_Rodovia = @obj_id;

		 			set @obj_id_ObraDeArte = (select top 1 obj_id from dbo.tab_objetos 
					                                where obj_pai = @obj_id_Rodovia and clo_id = 2);
					set @obj_id = @obj_id_ObraDeArte;
				end

				if (@clo_id_aux = 2) or (@obj_id_ObraDeArte > 0)  -- oae = quilometragem
				begin
					set @obj_id_ObraDeArte = @obj_id;

		 			set @obj_id_TipoObraDeArte = (select top 1 obj_id from dbo.tab_objetos 
					                                where obj_pai = @obj_id_ObraDeArte and clo_id = 3);

				    set @obj_id = @obj_id_TipoObraDeArte;
				end
				

		-- cria lista de obj_ids subindo a hierarquia
		declare @obj_pai int = -1
		declare @lista_obj_ids varchar(max) = convert(varchar(3), @obj_id);

		while @obj_pai <> ''
		begin
				select  @obj_pai = isnull(obj_pai, ''),
						@clo_id_aux = clo_id
				from dbo.tab_objetos
				where obj_id = @obj_id;

				if (@clo_id_aux = 1) -- 1= RODOVIA
				  set @obj_id_Rodovia = @obj_id;

				if (@clo_id_aux = 2) -- 2= OBRA DE ARTE
				  set @obj_id_ObraDeArte = @obj_id;

				if (@clo_id_aux = 3) -- 3= TIPO DE OBRA DE ARTE
				  set @obj_id_TipoObraDeArte = @obj_id;

				if (@obj_pai >0)
					set @lista_obj_ids = @lista_obj_ids + ',' +  convert(varchar(3), @obj_pai);

			set @obj_id = @obj_pai;
		 end


		-- desce a hierarquia até o nivel de superestrutura/mesoestrutura/infraestrutura/encontros
		declare @saida varchar(max) = '';
		select  @saida = COALESCE(@saida + ',', '') + CAST(obj_id AS VARCHAR(3))
		from dbo.tab_objetos
		where clo_id=6 and tip_id in (11,12,13,14)
			and obj_pai = @obj_id_TipoObraDeArte

		-- remove a 1a virgula se houver
		if left(@saida,1) = ','
			set @saida = SUBSTRING(@saida, 2, 10000);

		if @lista_obj_ids <> ''
			set @lista_obj_ids = @lista_obj_ids + ',' + @saida;
		else
			set @lista_obj_ids = @saida;


		-- acha a classe e o tipo do objeto
		declare @clo_id int; declare @tip_id int;
		select @clo_id = clo_id,
				@tip_id = tip_id
		from dbo.tab_objetos
		where obj_id = @obj_id;


		-- seleciona
		select 
				distinct atv.obj_id,
				atf.atr_id, 
				case when atf.atr_apresentacao_itens is null or  atf.atr_apresentacao_itens = 'combobox'
					 then 'lblatr_id_' + convert(varchar(3), atf.atr_id)
					 else 
						   'lblatr_id_' + convert(varchar(3), atf.atr_id) +'_' + convert(varchar(3), ati.ati_id)
					 end  as atv_LBL,

				case when atf.atr_apresentacao_itens = 'checkbox' then
					ati.ati_item
				else
					isnull(replace(atf.atr_atributo_nome, char(39),'´'),'')
				end as atv_LBL_texto,

				case when atf.atr_apresentacao_itens is null
					 then 'txt_atr_id_' + convert(varchar(3), atf.atr_id)
					 else 
						case when atf.atr_apresentacao_itens = 'combobox' then
						   'txt_atr_id_' + convert(varchar(3), atf.atr_id) 
						else
						   'chk_atr_id_' + convert(varchar(3), atf.atr_id) +'_' + convert(varchar(3), ati.ati_id)
						end
				end  as atv_TXT,

				case when atf.atr_apresentacao_itens = 'combobox' then
						case when atf.atr_id = 14 then
						    (select top 1 mun_municipio from dbo.tab_municipios where mun_id = convert(int, atv.atv_valor) ) 
						else
							isnull( (select top 1 isnull(replace(ati_item, char(39),'´'),'') as ati_item from dbo.tab_atributo_itens where ati_id = convert(int, atv.atv_valor)),'') 
						end
				else
				   case when atf.atr_apresentacao_itens = 'checkbox' then
				     case when len(atv.atv_valor)>0 then
					    case when (left(atv.atv_valor,1) = '0') then  '0' 
						else
						  case when len(atv.atv_valor)<=2 then
							'1'
						  else
						    SUBSTRING(atv.atv_valor,3,100)
						  end
						end
					 else
						isnull(atv.atv_valor, '0')
					 end
				   else 
					  isnull(atv.atv_valor ,'')
				   end
				end  as atv_TXT_texto

		into #temp1
		from dbo.tab_atributos atf
			left join dbo.tab_objeto_atributos_valores atv on atv.atr_id = atf.atr_id
			left join dbo.tab_atributo_itens ati on ati.atr_id = atf.atr_id   and ati.ati_id = case when atv.ati_id is not null then atv.ati_id else ati.ati_id end
		where
			 atf.atr_deletado is null and atf.atr_ativo = 1
		order by atv_LBL ;	

--select *, @obj_id_ObraDeArte,@obj_id_Rodovia  from #temp1

	 -- filtra os dados brutos
	 update #temp1
	 set 
		obj_id = NULL,
		atv_TXT_texto = case when left(atv_TXT,3) = 'chk' then '0' else '' end
	 where obj_id not in (select * from  dbo.ConvertToTableInt(@lista_obj_ids)); 

	  -- **********************	COLOCA OS DADOS GERAIS ******************
	  	declare @CodigoRodovia varchar(20) = (select top 1 isnull(obj_codigo,'') from dbo.tab_objetos   where obj_id = @obj_id_Rodovia);
		declare  @KM_ObraDeArte varchar(20) = (select top 1 isnull(obj_codigo,'') from dbo.tab_objetos   where obj_id = @obj_id_ObraDeArte);
		set @KM_ObraDeArte = substring(@KM_ObraDeArte, (CHARINDEX('-', @KM_ObraDeArte)+1),10); -- deixa somente o '000,000'


	   -- IDENTIFICAÇÃO DA OBRA DE ARTE / CÓDIGO
		update #temp1 set atv_TXT_texto = (select top 1 obj_codigo from dbo.tab_objetos where obj_id = @obj_id_TipoObraDeArte) where atr_id = 102;

	   -- Nome da Obra de Arte
		update #temp1 set atv_TXT_texto = (select top 1 obj_descricao from dbo.tab_objetos where obj_id = @obj_id_TipoObraDeArte) where atr_id = 105;

	   -- Nome Rodovia
	    update #temp1 set atv_TXT_texto = (select top 1 obj_descricao from dbo.tab_objetos where obj_id = @obj_id_Rodovia) where atr_id = 107;

	   -- Código da Rodovia
		update #temp1 set atv_TXT_texto = @CodigoRodovia where atr_id = 106;

	   -- Localização - KM
		update #temp1 set atv_TXT_texto = @KM_ObraDeArte where atr_id = 13;

		-- tipo de OAE
		declare @tip_idOAE int = (select tip_id from dbo.tab_objetos where obj_id = @obj_id_TipoObraDeArte );
		
		update #temp1 set
				atv_TXT_texto = (select top 1 tip_nome from dbo.tab_objeto_tipos where tip_id = @tip_idOAE)
		 where atr_id = 98;


	  -- *********************************************************************
	  	alter table #temp1 alter column atv_LBL varchar(8000) ;
	  	alter table #temp1 alter column atv_LBL_texto varchar(8000) ;
	  	alter table #temp1 alter column atv_TXT varchar(8000) ;
	  	alter table #temp1 alter column atv_TXT_texto varchar(8000) ;

	 --remove coluna obj_id
	 alter table #temp1 drop column obj_id

--select * from #temp1

	  create table #temp2 (atv_LABEL varchar(max) not null, atv_TEXTO varchar(max) null);

	  insert into #temp2 (atv_LABEL, atv_TEXTO)
	  SELECT distinct atv_LBL, 
		case when  charindex('|', ATV_LBL_texto) >0 
				then ltrim(rtrim(SUBSTRING( ATV_LBL_texto , LEN(ATV_LBL_texto)- CHARINDEX('|',REVERSE(ATV_LBL_texto))+2,LEN(ATV_LBL_texto))))
				else ATV_LBL_texto
				end
	  from #temp1
	  where atv_LBL is not null and atv_LBL <> ''
	 union all
	  select distinct atv_TXT, max(atv_TXT_texto)
	  from #temp1
	   where atv_TXT is not null and atv_TXT <> ''	  
	  group by (atv_TXT)
	  order by atv_LBL ;

		---- retorna
declare @cols1 VARCHAR(MAX)='';
declare @cols2 VARCHAR(MAX)='';
declare @vals VARCHAR(MAX)='';

declare @query NVARCHAR(MAX);

declare @atv_LABEL varchar(500);
declare @atv_TEXTO varchar(max);

DECLARE cursor1 CURSOR FOR SELECT distinct atv_LABEL, atv_TEXTO  FROM  #temp2 ;
OPEN cursor1;
FETCH NEXT FROM cursor1 INTO  @atv_LABEL, @atv_TEXTO;

SET @cols1 =  @atv_LABEL;
SET @cols2 =  @atv_LABEL + ' nvarchar(max) null';
SET @vals  =  char(39) + @atv_TEXTO + char(39);

FETCH NEXT FROM cursor1 INTO  @atv_LABEL, @atv_TEXTO;

WHILE @@FETCH_STATUS = 0
BEGIN
	set @cols1 = @cols1 + ',' + @atv_LABEL;
	set @cols2 = @cols2 + ',' + @atv_LABEL + ' nvarchar(max) null';
	set @vals = @vals + ',' + char(39) + isnull(@atv_TEXTO,'') + char(39);

    FETCH NEXT FROM cursor1 INTO  @atv_LABEL,  @atv_TEXTO;
END;


CLOSE cursor1;
DEALLOCATE cursor1;

declare @sql varchar(max);

IF OBJECT_ID('tempdb..##temp3') IS NOT NULL
	DROP TABLE ##temp3;

--set @sql = 'drop table #temp3'
--exec (@sql)

set @sql = 'create table ##temp3 (' + @cols2 + ');';
--exec (@sql)

set @sql = @sql  + ' insert into ##temp3  (' + @cols1 + ')'
  				+  ' values (' + @vals + '); ';
--exec (@sql)
--select @sql
--select * from ##temp3;



	  -- **********************	COLOCA OS DADOS ABA HISTORICO ******************

		declare @tos_descricao varchar(255) ='';
		declare @ins_documento varchar(255) ='';
		declare @ins_data varchar(15) = '';
		declare @ins_executantes varchar(500) = '';
		declare @ins_pontuacaoOAE varchar(5) ='';	
		declare @tos_id int = (select tos_id from [dbo].[tab_ordens_servico] where ord_id = @ord_id ) ;

		IF OBJECT_ID('tempdb..#temp_historico') IS NOT NULL
			DROP TABLE #temp_historico;

		declare @ins_data_atual varchar(20)= (select ins_data from dbo.tab_inspecoes where ord_id = @ord_id);
		declare @dt_atual date = CONVERT(date,@ins_data_atual,103);

		create table #temp_historico
		(tos_id  int null,
		tos_descricao nvarchar(255) null, 
		ins_documento nvarchar(255) null, 
		ins_data nvarchar(15) null, 
		ins_executantes nvarchar(500) null, 
		ins_pontuacaoOAE nvarchar(5) null
		);

		insert into  #temp_historico (tos_id, tos_descricao, ins_documento, ins_data, ins_executantes, ins_pontuacaoOAE )
		SELECT tos.tos_id, tos.tos_descricao,  ins_documento, ins_data, ins_executantes, ins_pontuacaoOAE
		FROM dbo.tab_ordens_servico ord
		inner join dbo.tab_inspecoes ins on ins.ord_id = ord.ord_id and ins.ins_deletado is null and ins.ins_ativo = 1 --or ins.ins_data is null
		inner join dbo.tab_ordem_servico_tipos tos on tos.tos_id = ord.tos_id and tos.tos_deletado is null and tos.tos_ativo = 1
		where ord.ord_id = @ord_id					
		
		insert into  #temp_historico (tos_id, tos_descricao, ins_documento, ins_data, ins_executantes, ins_pontuacaoOAE )
		SELECT  2, 'Insp ',   ins_documento_2, ins_data_2, ins_executantes_2, ins_pontuacaoOAE_2
		FROM  dbo.tab_inspecoes ins 
		where ins.ins_deletado is null and ins.ins_ativo = 1 
			and  ord_id = @ord_id;

		insert into  #temp_historico (tos_id, tos_descricao, ins_documento, ins_data, ins_executantes, ins_pontuacaoOAE )
		SELECT  3, 'Insp ',  ins_documento_3, ins_data_3, ins_executantes_3, ins_pontuacaoOAE_3
		FROM  dbo.tab_inspecoes ins 
		where ins.ins_deletado is null and ins.ins_ativo = 1 
			and  ord_id = @ord_id;

/*
	  	SELECT top 3 tos.tos_descricao, ins.ins_documento, ins.ins_data, ins.ins_executantes, ins.ins_pontuacaoOAE
		into #temp_historico
		FROM dbo.tab_ordens_servico ord
		inner join dbo.tab_inspecoes ins on ins.ord_id = ord.ord_id and ins.ins_deletado is null and ins.ins_ativo = 1
		inner join dbo.tab_ordem_servico_tipos tos on tos.tos_id = ord.tos_id and tos.tos_deletado is null and tos.tos_ativo = 1
		where ord.ord_deletado is null and ord.ord_ativo = 1					
		--	and ord.sos_id = 11 -- executada						
			and ord.obj_id = @obj_id_TipoObraDeArte
			and ord.tos_id = case when @tos_id = 7 then 7 else ord.tos_id end
			and CONVERT(date,ins_data,103) <= @dt_atual
		order by  CONVERT(date,ins_data,103) desc, ord.tos_id desc,
					ord_data_inicio_programada desc;

*/

		alter table #temp_historico drop column tos_id;

		declare @linha int = 1;
		declare @atual_anterior varchar(20) = ' Atual';
		
		DECLARE cursor_aux2 CURSOR for select * from #temp_historico;
		OPEN cursor_aux2;
		FETCH NEXT FROM cursor_aux2 INTO @tos_descricao, @ins_documento, @ins_data, @ins_executantes, @ins_pontuacaoOAE;

		DECLARE @sql_historico varchar(max) = ''

		while @linha <=3
		begin	
			if @linha > 1 
			 set @atual_anterior = ' Anterior';

			if @ins_data = '' or CHARINDEX('Cadastral',@tos_descricao) >0 
			 set @atual_anterior = '';

			--select @tos_descricao, @ins_documento, @ins_data, @ins_executantes, @ins_pontuacaoOAE;

			set @sql_historico = @sql_historico  + 'ALTER TABLE ##temp3 ADD ' +  'lbl_historico_Inspecao_' + convert(varchar(1),@linha) + ' varchar(1000); ' ;
			set @sql_historico = @sql_historico  + ' update ##temp3 set ' +  'lbl_historico_Inspecao_' + convert(varchar(1),@linha) + ' = ' + char(39) + @tos_descricao + @atual_anterior + char(39) + ';';

			set @sql_historico = @sql_historico  + 'ALTER TABLE ##temp3 ADD ' +  'txt_historico_documento_' + convert(varchar(1),@linha) + ' varchar(8000); ' ;
			set @sql_historico = @sql_historico  + ' update ##temp3 set ' +  'txt_historico_documento_' + convert(varchar(1),@linha) + ' = ' + char(39) +  isnull(@ins_documento,' ') + char(39) + ';';

			set @sql_historico = @sql_historico  + 'ALTER TABLE ##temp3 ADD ' +  'txt_historico_data_' + convert(varchar(1),@linha) + ' varchar(100); ' ;
			set @sql_historico = @sql_historico  + ' update ##temp3 set ' +  'txt_historico_data_' + convert(varchar(1),@linha) + ' = ' + char(39) +  isnull(@ins_data,' ') + char(39) + ';';

			set @sql_historico = @sql_historico  + 'ALTER TABLE ##temp3 ADD ' +  'txt_historico_executantes_' + convert(varchar(1),@linha) + ' varchar(8000); ' ;
			set @sql_historico = @sql_historico  + ' update ##temp3 set ' +  'txt_historico_executantes_' + convert(varchar(1),@linha) + ' = ' + char(39) +  isnull(@ins_executantes,' ') + char(39) + ';';
		
			set @sql_historico = @sql_historico  + 'ALTER TABLE ##temp3 ADD ' +  'txt_historico_Pontuacao_Geral_OAE_' + convert(varchar(1),@linha) + ' varchar(100); ' ;
			set @sql_historico = @sql_historico  + ' update ##temp3 set ' +  'txt_historico_Pontuacao_Geral_OAE_' + convert(varchar(1),@linha) + ' = ' + char(39) +  isnull(@ins_pontuacaoOAE,' ') + char(39) + ';';

			--reseta as variaveis porque pode ser que o select nao retorne linhas
			set @tos_descricao = ' ';
			set @ins_documento = ' ';
			set @ins_data = ' ';
			set @ins_executantes  = ' ';
			set @ins_pontuacaoOAE = ' ';

			FETCH NEXT FROM cursor_aux2 INTO @tos_descricao, @ins_documento, @ins_data, @ins_executantes, @ins_pontuacaoOAE;
			set @linha = @linha + 1;
		end;

		CLOSE cursor_aux2;
		DEALLOCATE cursor_aux2;


	set @sql = @sql  + @sql_historico;







-- ****************	 documentos de referencia **************************************************


--declare @ord_id int = 0;
			set @ord_id = (select top 1 ord_id
							  FROM [dbo].[tab_documento_ordens_servicos] dos
								inner join [dbo].[tab_documento_objeto] dob on dob.doc_id=dos.doc_id and dob.dob_deletado is null
							  where obj_id = @obj_id_TipoObraDeArte
								  and dos.dos_deletado is null 
								  and dos.dos_referencia = 1);
declare @docs varchar(max) = '';

					select 
						 @docs = COALESCE(@docs + '', '') + (doc_codigo + ' - ' +  doc_descricao + ';<br />')
					from [dbo].[tab_documento_ordens_servicos] dos
						inner join [dbo].[tab_documentos] doc on doc.doc_id = dos.doc_id and doc.doc_deletado is null and doc.doc_ativo =1
					where dos_deletado is null
						and [ord_id] = @ord_id
						and dos_referencia = 1
					order by  doc_codigo;

set @sql = @sql  + 'ALTER TABLE ##temp3 ADD lbl_docs_referencia varchar(max); ' ;
set @sql = @sql  + ' update ##temp3 set lbl_docs_referencia = ' + char(39) + @docs + char(39) + ';';

-- SAIDA DOS DADOS, SELECT FINAL
--	set @sql = @sql  +  ' select * into dbo.tab_Ficha2 from ##temp3; select * from ##temp3'
	set @sql = @sql  +  ' select * from ##temp3;'
exec (@sql)

		-- checa se as tmp table existe e a exclui
		IF OBJECT_ID('tempdb..#temp1') IS NOT NULL
			DROP TABLE #temp1;

set nocount off

SET FMTONLY ON

end ;
