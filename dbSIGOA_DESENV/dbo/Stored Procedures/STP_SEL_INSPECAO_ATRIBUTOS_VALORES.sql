﻿CREATE PROCEDURE [dbo].[STP_SEL_INSPECAO_ATRIBUTOS_VALORES] 
@ord_id int

as 
begin 
	declare @ins_id bigint = (select ins_id from tab_inspecoes where ord_id = @ord_id );

		-- cria lista de obj_ids subindo a hierarquia
		declare @obj_id_Rodovia int = -1
		declare @CodigoRodovia varchar(10) = ''

		declare @obj_id_ObraDeArte int = -1
		declare @KM_ObraDeArte varchar(20) = ''
		declare @obj_id_TipoObraDeArte int = -1

		declare @clo_id_aux int = 3

		-- checa se as tmp table existe e a exclui
		IF OBJECT_ID('tempdb..#temp1') IS NOT NULL
			DROP TABLE #temp1;


		declare @obj_id int = 0;
		declare @sos_id int = 0;
		select @sos_id = sos_id, @obj_id = obj_id
		                         from dbo.tab_ordens_servico ord
		                         where ord_deletado is null and ord_ativo=1
									and ord_id = @ord_id;



		set @obj_id_TipoObraDeArte = (select obj_id from dbo.tab_inspecoes where ord_id = @ord_id);

		--if (@sos_id <> 7) and (@sos_id <> 14) -- 7=cancelada 14 = encerrada
		--	execute STP_SEL_OBJETO_ATRIBUTOS_VALORES @obj_id;

		declare @obj_id_orig int = @obj_id_TipoObraDeArte;


		---- cria lista de obj_ids subindo a hierarquia
		--declare @obj_id_Rodovia int = -1
		--declare @CodigoRodovia varchar(10) = ''

		--declare @obj_id_ObraDeArte int = -1
		--declare @KM_ObraDeArte varchar(20) = ''
		--declare @obj_id_TipoObraDeArte int = -1

		--declare @clo_id_aux int = -1

		-- -- se a classe do @obj_id for 2 (obra de arte)  ou 1 (rodovia) entao procura-os pois estao hierarquia abaixo de obj_id
		-- 		set @clo_id_aux = (select top 1 clo_id	from dbo.tab_objetos where obj_id = @obj_id_orig);

		--		if (@clo_id_aux = 1) -- rodovia
		--		begin
		--			set @obj_id_Rodovia = @obj_id;

		-- 			set @obj_id_ObraDeArte = (select top 1 obj_id from dbo.tab_objetos 
		--			                                where obj_pai = @obj_id_Rodovia and clo_id = 2);

		--			set @obj_id = @obj_id_ObraDeArte;
		--		end

		--		if (@clo_id_aux = 2) or (@obj_id_ObraDeArte > 0)  -- oae = quilometragem
		--		begin

		--			set @obj_id_ObraDeArte = @obj_id;
		--			set @obj_id_Rodovia = (select top 1 obj_pai from dbo.tab_objetos  where obj_id = @obj_id_ObraDeArte);

		--		end

		--		if (@clo_id_aux = 3)  -- tipo de oae
		--		begin
		-- 			set @obj_id_TipoObraDeArte = @obj_id_orig;
		--		    set @obj_id = @obj_id_orig;
		--		end
				

		---- cria lista de obj_ids subindo a hierarquia
		declare @obj_pai int =0
		declare @lista_obj_ids varchar(max) = convert(varchar(3), @obj_id);
		declare @fim int = 0;
		while (@obj_pai >=0  and @fim < 15)
		begin

				select  @obj_pai = isnull(obj_pai, -1),
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
			set @fim = @fim + 1;
		 end

		 ----select @obj_id_Rodovia,@obj_id_ObraDeArte, @obj_id_TipoObraDeArte
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



		---- acha a classe e o tipo do objeto
		--declare @clo_id int; declare @tip_id int;
		--select @clo_id = clo_id,
		--		@tip_id = tip_id
		--from dbo.tab_objetos
		--where obj_id = @obj_id;

select * into #temp1 from (
			-- atributos cujo valor é item
			SELECT 	distinct ins.obj_id,
							atf.clo_id, atf.tip_id,
							atf.atr_id, atf.atr_atributo_nome, atf.atr_descricao, atf.atr_mascara_texto,
							--atv.ati_id, ati.ati_item,
							case when atf.atr_apresentacao_itens is null
								 then 'lblatr_id_' + convert(varchar(3), atf.atr_id)
								 else 
									case when atf.atr_apresentacao_itens = 'combobox' then
									   'cmb_atr_id_' + convert(varchar(3), atf.atr_id) 
									else
									   'chk_atr_id_' + convert(varchar(3), atf.atr_id)  
									end
						
								 end  as atv_controle,

							case when iav_valor is not null
								 then iav_valor 
								 else '0:'
							end	as atv_valor,

							case when atv.ati_id is not null
								 then dbo.ConcatenarAtributoItens_Valores(@obj_id, atf.atr_id,@ord_id, 0, 1) 
								 else  atv.iav_valor 
								 end  as atv_valores,
					 
							case when ati.ati_id is not null
								 then ati.ati_id -- dbo.ConcatenarAtributoItens_Valores(@obj_id, atf.atr_id, 2) 
								 else '-1'
								 end  as ati_ids,

							isnull (atf.atr_apresentacao_itens,'') as atr_apresentacao_itens,

							case when atf.atr_id = 14
								then (select count(*) from dbo.tab_municipios)
								else (select count(*) from dbo.tab_atributo_itens where atr_id = atf.atr_id and ati_deletado is null) 
							end as nItens,
				
							case when atf.atr_id = 14
								then dbo.ConcatenarMunicipios()
								else dbo.ConcatenarAtributoItemIdCodigo ( atf.atr_id ) 
							end as atr_itens_todos
				
			from dbo.tab_atributos atf
				inner join [dbo].[tab_atributo_itens] ati on ati.atr_id = atf.atr_id
				left join dbo.tab_inspecao_atributos_valores atv on atv.atr_id = atf.atr_id  and (ati.ati_id = atv.ati_id or atv.ati_id is null) and (atv.ins_id = @ins_id or atv.ins_id is null)
				left join dbo.tab_inspecoes ins on ins.ins_id = atv.ins_id
			where 
				--ati.ati_id = case when atv.ati_id is not null then atv.ati_id else null end
			  ins.[obj_id]   in (select * from dbo.ConvertToTableInt(@lista_obj_ids))
			  and ins.obj_id is not null
			--	and atv.ins_id= @ins_id
			--	and ins.ord_id = @ord_id
			union
			-- atributos cujo valor nao é item
			SELECT 	distinct ins.obj_id,
							atf.clo_id, atf.tip_id,
							atf.atr_id, atf.atr_atributo_nome, atf.atr_descricao, atf.atr_mascara_texto,
							--atv.ati_id, ati.ati_item,
							case when atf.atr_apresentacao_itens is null
								 then 'lblatr_id_' + convert(varchar(3), atf.atr_id)
								 else 
									case when atf.atr_apresentacao_itens = 'combobox' then
									   'cmb_atr_id_' + convert(varchar(3), atf.atr_id) 
									else
									   'chk_atr_id_' + convert(varchar(3), atf.atr_id)  
									end
						
								 end  as atv_controle,

							atv.iav_valor as atv_valor,
							case when atv.ati_id is not null
								 then dbo.ConcatenarAtributoItens_Valores(@obj_id, atf.atr_id, @ord_id,0, 1) 
								 else  atv.iav_valor 
								 end  as atv_valores,
					 
							case when atv.ati_id is not null
								 then atv.ati_id -- dbo.ConcatenarAtributoItens_Valores(@obj_id, atf.atr_id, 2) 
								 else '-1'
								 end  as ati_ids,

							isnull (atf.atr_apresentacao_itens,'') as atr_apresentacao_itens,

							case when atf.atr_id = 14
								then (select count(*) from dbo.tab_municipios)
								else (select count(*) from dbo.tab_atributo_itens where atr_id = atf.atr_id and ati_deletado is null) 
							end as nItens,
				
							case when atf.atr_id = 14
								then dbo.ConcatenarMunicipios()
								else dbo.ConcatenarAtributoItemIdCodigo ( atf.atr_id ) 
							end as atr_itens_todos 
			--from dbo.tab_atributos atf
			--		inner join dbo.tab_inspecao_atributos_valores atv on atv.atr_id = atf.atr_id
			--		inner join dbo.tab_inspecoes ins on ins.ins_id = atv.ins_id

			from dbo.tab_atributos atf
				left join dbo.tab_inspecao_atributos_valores atv on atv.atr_id = atf.atr_id  and (atv.ins_id = @ins_id or atv.ins_id is null)
				left join dbo.tab_inspecoes ins on ins.ins_id = atv.ins_id 

				--where  ati_id is null
				--		and  ins.[obj_id] = @obj_id 
				--		and atv.ins_id= @ins_id
				--and ins.ord_id = @ord_id

			union 
			 SELECT 	distinct ins.obj_id,
							atf.clo_id, atf.tip_id,
							atf.atr_id, atf.atr_atributo_nome, atf.atr_descricao, atf.atr_mascara_texto,
							--atv.ati_id, ati.ati_item,
							case when atf.atr_apresentacao_itens is null
								 then 'lblatr_id_' + convert(varchar(3), atf.atr_id)
								 else 
									case when atf.atr_apresentacao_itens = 'combobox' then
									   'cmb_atr_id_' + convert(varchar(3), atf.atr_id) 
									else
									   'chk_atr_id_' + convert(varchar(3), atf.atr_id)  
									end
						
								 end  as atv_controle,

							atv.iav_valor as atv_valor,
							case when atv.ati_id is not null
								 then dbo.ConcatenarAtributoItens_Valores(@obj_id, atf.atr_id, @ord_id,0, 1) 
								 else  atv.iav_valor 
								 end  as atv_valores,
					 
							case when atv.ati_id is not null
								 then atv.ati_id -- dbo.ConcatenarAtributoItens_Valores(@obj_id, atf.atr_id, 2) 
								 else '-1'
								 end  as ati_ids,

							isnull (atf.atr_apresentacao_itens,'') as atr_apresentacao_itens,

							case when atf.atr_id = 14
								then (select count(*) from dbo.tab_municipios)
								else (select count(*) from dbo.tab_atributo_itens where atr_id = atf.atr_id and ati_deletado is null) 
							end as nItens,
				
							case when atf.atr_id = 14
								then dbo.ConcatenarMunicipios()
								else dbo.ConcatenarAtributoItemIdCodigo ( atf.atr_id ) 
							end as atr_itens_todos
			 from [dbo].[tab_atributos] atf
 				left join dbo.tab_inspecao_atributos_valores atv on atv.atr_id = atf.atr_id and atv.ins_id = @ins_id
				left join dbo.tab_inspecoes ins on ins.ins_id = atv.ins_id and ins.obj_id is null

			 where atf.atr_id  not in ( 
						  				select distinct atv.atr_id -- menos os que tem itens
										 from dbo.tab_inspecao_atributos_valores atv 
											inner join dbo.tab_inspecoes ins on ins.ins_id = atv.ins_id 
											inner join [dbo].[tab_atributo_itens] ati on ati.ati_id = atv.ati_id
										 where  ins.[obj_id] = @obj_id_TipoObraDeArte 
										 union
										 select distinct atv.atr_id -- menos os que ja tem e nao sao itens
										 from dbo.tab_inspecao_atributos_valores atv 
											inner join dbo.tab_inspecoes ins on ins.ins_id = atv.ins_id 
										 where  ati_id is null
											and ins.[obj_id] = @obj_id_TipoObraDeArte
									  )						  
 ) as tb1



	 ---- filtra os dados brutos
	 --update #temp1
	 --set 
		--obj_id = NULL,
		--iav_valor = NULL,
		--iav_valores = NULL
	 --where obj_id not in (select * from  dbo.ConvertToTableInt(@lista_obj_ids)); 




	  -- **********************	COLOCA OS DADOS GERAIS ******************
	 
		set @CodigoRodovia = (select top 1 isnull(obj_codigo,'') from dbo.tab_objetos   where obj_id = @obj_id_Rodovia);
		set @KM_ObraDeArte = (select top 1 isnull(obj_codigo,'') from dbo.tab_objetos   where obj_id = @obj_id_ObraDeArte);
		set @KM_ObraDeArte = substring(@KM_ObraDeArte, (CHARINDEX('-', @KM_ObraDeArte)+1),10); -- deixa somente o '000,000'
		
		declare @municipio varchar (100) = (select top 1 isnull(atv_valor,'') from  [dbo].[tab_objeto_atributos_valores]  where atr_id = 14 and obj_id = @obj_id_TipoObraDeArte);

		
		if ((select count(*) from #temp1 where atr_id = 13) = 0)
			insert into #temp1 (obj_id,clo_id,tip_id,atr_id,atr_atributo_nome,atr_descricao,atr_mascara_texto,atv_controle,atv_valor,atv_valores,ati_ids,atr_apresentacao_itens,nItens,atr_itens_todos)
			values (@obj_id, 3, -1, 13, 'Localização - KM', 'Localização - KM', NULL, 'lblatr_id_13', @KM_ObraDeArte, @KM_ObraDeArte, -1, '',0,'' );

		if ((select count(*) from #temp1 where atr_id = 14) = 0)
			insert into #temp1 (obj_id,clo_id,tip_id,atr_id,atr_atributo_nome,atr_descricao,atr_mascara_texto,atv_controle,atv_valor,atv_valores,ati_ids,atr_apresentacao_itens,nItens,atr_itens_todos)
			values (@obj_id, 3, -1, 14, 'Município', 'Município', NULL, 'cmb_atr_id_14', NULL, NULL, -1, 'combobox',645,'001Adamantina;002Adolfo;003Aguaí;004Águas da Prata;005Águas de Lindóia;006Águas de Santa Bárbara;007Águas de São Pedro;008Agudos;009Alambari;010Alfredo Marcondes;011Altair;012Altinópolis;013Alto Alegre;014Alumínio;015Álvares Florence;016Álvares Machado;017Álvaro de Carvalho;018Alvinlândia;019Americana;020Américo Brasiliense;021Américo de Campos;022Amparo;023Analândia;024Andradina;025Angatuba;026Anhembi;027Anhumas;028Aparecida;029Aparecida D´oeste;030Apiaí;031Araçariguama;032Araçatuba;033Araçoiaba da Serra;034Aramina;035Arandu;036Arapeí;037Araraquara;038Araras;039Arco-íris;040Arealva;041Areias;042Areiópolis;043Ariranha;044Artur Nogueira;045Arujá;046Aspásia;047Assis;048Atibaia;049Auriflama;050Avaí;051Avanhandava;052Avaré;053Bady Bassitt;054Balbinos;055Bálsamo;056Bananal;057Barão de Antonina;058Barbosa;059Bariri;060Barra Bonita;061Barra do Chapéu;062Barra do Turvo;063Barretos;064Barrinha;065Barueri;066Bastos;067Batatais;068Bauru;069Bebedouro;070Bento de Abreu;071Bernardino de Campos;072Bertioga;073Bilac;074Birigui;075Biritiba-mirim;076Boa Esperança do Sul;077Bocaina;078Bofete;079Boituva;080Bom Jesus Dos Perdões;081Bom Sucesso de Itararé;082Borá;083Boracéia;084Borborema;085Borebi;086Botucatu;087Bragança Paulista;088Braúna;089Brejo Alegre;090Brodowski;091Brotas;092Buri;093Buritama;094Buritizal;095Cabrália Paulista;096Cabreúva;097Caçapava;098Cachoeira Paulista;099Caconde;100Cafelândia;101Caiabu;102Caieiras;103Caiuá;104Cajamar;105Cajati;106Cajobi;107Cajuru;108Campina do Monte Alegre;109Campinas;110Campo Limpo Paulista;111Campos do Jordão;112Campos Novos Paulista;113Cananéia;114Canas;115Cândido Mota;116Cândido Rodrigues;117Canitar;118Capão Bonito;119Capela do Alto;120Capivari;121Caraguatatuba;122Carapicuíba;123Cardoso;124Casa Branca;125Cássia Dos Coqueiros;126Castilho;127Catanduva;128Catiguá;129Cedral;130Cerqueira César;131Cerquilho;132Cesário Lange;133Charqueada;134Chavantes;135Clementina;136Colina;137Colômbia;138Conchal;139Conchas;140Cordeirópolis;141Coroados;142Coronel Macedo;143Corumbataí;144Cosmópolis;145Cosmorama;146Cotia;147Cravinhos;148Cristais Paulista;149Cruzália;150Cruzeiro;151Cubatão;152Cunha;153Descalvado;154Diadema;155Dirce Reis;156Divinolândia;157Dobrada;158Dois Córregos;159Dolcinópolis;160Dourado;161Dracena;162Duartina;163Dumont;164Echaporã;165Eldorado;166Elias Fausto;167Elisiário;168Embaúba;169Embu;170Embu-guaçu;171Emilianópolis;172Engenheiro Coelho;173Espírito Santo do Pinhal;174Espírito Santo do Turvo;175Estiva Gerbi;176Estrela D´oeste;177Estrela do Norte;178Euclides da Cunha Paulista;179Fartura;180Fernando Prestes;181Fernandópolis;182Fernão;183Ferraz de Vasconcelos;184Flora Rica;185Floreal;186Flórida Paulista;187Florínia;188Franca;189Francisco Morato;190Franco da Rocha;191Gabriel Monteiro;192Gália;193Garça;194Gastão Vidigal;195Gavião Peixoto;196General Salgado;197Getulina;198Glicério;199Guaiçara;200Guaimbê;201Guaíra;202Guapiaçu;203Guapiara;204Guará;205Guaraçaí;206Guaraci;207Guarani D´oeste;208Guarantã;209Guararapes;210Guararema;211Guaratinguetá;212Guareí;213Guariba;214Guarujá;215Guarulhos;216Guatapará;217Guzolândia;218Herculândia;219Holambra;220Hortolândia;221Iacanga;222Iacri;223Iaras;224Ibaté;225Ibirá;226Ibirarema;227Ibitinga;228Ibiúna;229Icém;230Iepê;231Igaraçu do Tietê;232Igarapava;233Igaratá;234Iguape;235Ilha Comprida;236Ilha Solteira;237Ilhabela;238Indaiatuba;239Indiana;240Indiaporã;241Inúbia Paulista;242Ipaussu;243Iperó;244Ipeúna;245Ipiguá;246Iporanga;247Ipuã;248Iracemápolis;249Irapuã;250Irapuru;251Itaberá;252Itaí;253Itajobi;254Itaju;255Itanhaém;256Itaóca;257Itapecerica da Serra;258Itapetininga;259Itapeva;260Itapevi;261Itapira;262Itapirapuã Paulista;263Itápolis;264Itaporanga;265Itapuí;266Itapura;267Itaquaquecetuba;268Itararé;269Itariri;270Itatiba;271Itatinga;272Itirapina;273Itirapuã;274Itobi;275Itu;276Itupeva;277Ituverava;278Jaborandi;279Jaboticabal;280Jacareí;281Jaci;282Jacupiranga;283Jaguariúna;284Jales;285Jambeiro;286Jandira;287Jardinópolis;288Jarinu;289Jaú;290Jeriquara;291Joanópolis;292João Ramalho;293José Bonifácio;294Júlio Mesquita;295Jumirim;296Jundiaí;297Junqueirópolis;298Juquiá;299Juquitiba;300Lagoinha;301Laranjal Paulista;302Lavínia;303Lavrinhas;304Leme;305Lençóis Paulista;306Limeira;307Lindóia;308Lins;309Lorena;310Lourdes;311Louveira;312Lucélia;313Lucianópolis;314Luís Antônio;315Luiziânia;316Lupércio;317Lutécia;318Macatuba;319Macaubal;320Macedônia;321Magda;322Mairinque;323Mairiporã;324Manduri;325Marabá Paulista;326Maracaí;327Marapoama;328Mariápolis;329Marília;330Marinópolis;331Martinópolis;332Matão;333Mauá;334Mendonça;335Meridiano;336Mesópolis;337Miguelópolis;338Mineiros do Tietê;339Mira Estrela;340Miracatu;341Mirandópolis;342Mirante do Paranapanema;343Mirassol;344Mirassolândia;345Mococa;346Mogi Guaçu;347Moji Das Cruzes;348Moji-mirim;349Mombuca;350Monções;351Mongaguá;352Monte Alegre do Sul;353Monte Alto;354Monte Aprazível;355Monte Azul Paulista;356Monte Castelo;357Monte Mor;358Monteiro Lobato;359Morro Agudo;360Morungaba;361Motuca;362Murutinga do Sul;363Nantes;364Narandiba;365Natividade da Serra;366Nazaré Paulista;367Neves Paulista;368Nhandeara;369Nipoã;370Nova Aliança;371Nova Campina;372Nova Canaã Paulista;373Nova Castilho;374Nova Europa;375Nova Granada;376Nova Guataporanga;377Nova Independência;378Nova Luzitânia;379Nova Odessa;380Novais;381Novo Horizonte;382Nuporanga;383Ocauçu;384Óleo;385Olímpia;386Onda Verde;387Oriente;388Orindiúva;389Orlândia;390Osasco;391Oscar Bressane;392Osvaldo Cruz;393Ourinhos;394Ouro Verde;395Ouroeste;396Pacaembu;397Palestina;398Palmares Paulista;399Palmeira D´oeste;400Palmital;401Panorama;402Paraguaçu Paulista;403Paraibuna;404Paraíso;405Paranapanema;406Paranapuã;407Parapuã;408Pardinho;409Pariquera-açu;410Parisi;411Patrocínio Paulista;412Paulicéia;413Paulínia;414Paulistânia;415Paulo de Faria;416Pederneiras;417Pedra Bela;418Pedranópolis;419Pedregulho;420Pedreira;421Pedrinhas Paulista;422Pedro de Toledo;423Penápolis;424Pereira Barreto;425Pereiras;426Peruíbe;427Piacatu;428Piedade;429Pilar do Sul;430Pindamonhangaba;431Pindorama;432Pinhalzinho;433Piquerobi;434Piquete;435Piracaia;436Piracicaba;437Piraju;438Pirajuí;439Pirangi;440Pirapora do Bom Jesus;441Pirapozinho;442Pirassununga;443Piratininga;444Pitangueiras;445Planalto;446Platina;447Poá;448Poloni;449Pompéia;450Pongaí;451Pontal;452Pontalinda;453Pontes Gestal;454Populina;455Porangaba;456Porto Feliz;457Porto Ferreira;458Potim;459Potirendaba;460Pracinha;461Pradópolis;462Praia Grande;463Pratânia;464Presidente Alves;465Presidente Bernardes;466Presidente Epitácio;467Presidente Prudente;468Presidente Venceslau;469Promissão;470Quadra;471Quatá;472Queiroz;473Queluz;474Quintana;475Rafard;476Rancharia;477Redenção da Serra;478Regente Feijó;479Reginópolis;480Registro;481Restinga;482Ribeira;483Ribeirão Bonito;484Ribeirão Branco;485Ribeirão Corrente;486Ribeirão do Sul;487Ribeirão Dos Índios;488Ribeirão Grande;489Ribeirão Pires;490Ribeirão Preto;491Rifaina;492Rincão;493Rinópolis;494Rio Claro;495Rio Das Pedras;496Rio Grande da Serra;497Riolândia;498Riversul;499Rosana;500Roseira;501Rubiácea;502Rubinéia;503Sabino;504Sagres;505Sales;506Sales Oliveira;507Salesópolis;508Salmourão;509Saltinho;510Salto;511Salto de Pirapora;512Salto Grande;513Sandovalina;514Santa Adélia;515Santa Albertina;516Santa Bárbara D´oeste;517Santa Branca;518Santa Clara D´oeste;519Santa Cruz da Conceição;520Santa Cruz da Esperança;521Santa Cruz Das Palmeiras;522Santa Cruz do Rio Pardo;523Santa Ernestina;524Santa fé do Sul;525Santa Gertrudes;526Santa Isabel;527Santa Lúcia;528Santa Maria da Serra;529Santa Mercedes;530Santa Rita D´oeste;531Santa Rita do Passa Quatro;532Santa Rosa de Viterbo;533Santa Salete;534Santana da Ponte Pensa;535Santana de Parnaíba;536Santo Anastácio;537Santo André;538Santo Antônio da Alegria;539Santo Antônio de Posse;540Santo Antônio do Aracanguá;541Santo Antônio do Jardim;542Santo Antônio do Pinhal;543Santo Expedito;544Santópolis do Aguapeí;545Santos;546São Bento do Sapucaí;547São Bernardo do Campo;548São Caetano do Sul;549São Carlos;550São Francisco;551São João da Boa Vista;552São João Das Duas Pontes;553São João de Iracema;554São João do Pau D´alho;555São Joaquim da Barra;556São José da Bela Vista;557São José do Barreiro;558São José do Rio Pardo;559São José do Rio Preto;560São José Dos Campos;561São Lourenço da Serra;562São Luís do Paraitinga;563São Manuel;564São Miguel Arcanjo;565São Paulo;566São Pedro;567São Pedro do Turvo;568São Roque;569São Sebastião;570São Sebastião da Grama;571São Simão;572São Vicente;573Sarapuí;574Sarutaiá;575Sebastianópolis do Sul;576Serra Azul;577Serra Negra;578Serrana;579Sertãozinho;580Sete Barras;581Severínia;582Silveiras;583Socorro;584Sorocaba;585Sud Mennucci;586Sumaré;587Suzanápolis;588Suzano;589Tabapuã;590Tabatinga;591Taboão da Serra;592Taciba;593Taguaí;594Taiaçu;595Taiúva;596Tambaú;597Tanabi;598Tapiraí;599Tapiratiba;600Taquaral;601Taquaritinga;602Taquarituba;603Taquarivaí;604Tarabai;605Tarumã;606Tatuí;607Taubaté;608Tejupá;609Teodoro Sampaio;610Terra Roxa;611Tietê;612Timburi;613Torre de Pedra;614Torrinha;615Trabiju;616Tremembé;617Três Fronteiras;618Tuiuti;619Tupã;620Tupi Paulista;621Turiúba;622Turmalina;623Ubarana;624Ubatuba;625Ubirajara;626Uchoa;627União Paulista;628Urânia;629Uru;630Urupês;631Valentim Gentil;632Valinhos;633Valparaíso;634Vargem;635Vargem Grande do Sul;636Vargem Grande Paulista;637Várzea Paulista;638Vera Cruz;639Vinhedo;640Viradouro;641Vista Alegre do Alto;642Vitória Brasil;643Votorantim;644Votuporanga;645Zacarias' );

		if ((select count(*) from #temp1 where atr_id = 15) = 0)
			insert into #temp1 (obj_id,clo_id,tip_id,atr_id,atr_atributo_nome,atr_descricao,atr_mascara_texto,atv_controle,atv_valor,atv_valores,ati_ids,atr_apresentacao_itens,nItens,atr_itens_todos)
			values (@obj_id, 3, -1, 15, 'TipoPista', 'TipoPista', NULL, 'cmb_atr_id_15', NULL, NULL, -1, 'combobox',2,'016simples;017dupla' );

		if ((select count(*) from #temp1 where atr_id = 16) = 0)
			insert into #temp1 (obj_id,clo_id,tip_id,atr_id,atr_atributo_nome,atr_descricao,atr_mascara_texto,atv_controle,atv_valor,atv_valores,ati_ids,atr_apresentacao_itens,nItens,atr_itens_todos)
			values (@obj_id, 3, -1, 16, 'Pista', 'Pista', NULL, 'cmb_atr_id_16', NULL, NULL, -1, 'combobox',3,'018Direita;019Esquerda;020Ambas' );

		if ((select count(*) from #temp1 where atr_id = 102) = 0)
			insert into #temp1 (obj_id,clo_id,tip_id,atr_id,atr_atributo_nome,atr_descricao,atr_mascara_texto,atv_controle,atv_valor,atv_valores,ati_ids,atr_apresentacao_itens,nItens,atr_itens_todos)
			values (@obj_id, 3, -1, 102, 'IDENTIFICAÇÃO DA OBRA DE ARTE / CÓDIGO', 'IDENTIFICAÇÃO DA OBRA DE ARTE / CÓDIGO', NULL, 'lblatr_id_102', NULL, NULL, -1, '',0,'' );

		if ((select count(*) from #temp1 where atr_id = 105) = 0)
			insert into #temp1 (obj_id,clo_id,tip_id,atr_id,atr_atributo_nome,atr_descricao,atr_mascara_texto,atv_controle,atv_valor,atv_valores,ati_ids,atr_apresentacao_itens,nItens,atr_itens_todos)
			values (@obj_id, 3, -1, 106, 'Código da Rodovia', 'Código da Rodovia', NULL, 'lblatr_id_106', @CodigoRodovia, @CodigoRodovia, -1, '',0,'' );
	
		if ((select count(*) from #temp1 where atr_id = 107) = 0)
			insert into #temp1 (obj_id,clo_id,tip_id,atr_id,atr_atributo_nome,atr_descricao,atr_mascara_texto,atv_controle,atv_valor,atv_valores,ati_ids,atr_apresentacao_itens,nItens,atr_itens_todos)
			values (@obj_id, 3, -1, 107, 'Nome Rodovia', 'Nome Rodovia', NULL, 'lblatr_id_107', '', '', -1, '',0,'' );

		if ((select count(*) from #temp1 where atr_id = 98) = 0)
			insert into #temp1 (obj_id,clo_id,tip_id,atr_id,atr_atributo_nome,atr_descricao,atr_mascara_texto,atv_controle,atv_valor,atv_valores,ati_ids,atr_apresentacao_itens,nItens,atr_itens_todos)
			values (@obj_id, 3, -1, 98, 'Tipo de OAE', 'Tipo de OAE', NULL, 'cmb_atr_id_98', '', '', -1, 'combobox',15,'009Passagem Inferior (PSI);121Passagem Inferior Direita (PSID);120Passagem Inferior Esquerda (PSIE);010Passagem Superior (PSU);123Passagem Superior Direita (PSUD);122Passagem Superior Esquerda (PSUE);027Passarela (PAS);125Passarela Direita (PASD);124Passarela Esquerda (PASE);007Ponte (PTC);117Ponte Direita (PTCD);116Ponte Esquerda (PTCE);008Viaduto (VDT);119Viaduto Direito (VDTD);118Viaduto Esquerdo (VDTE)' );


	   -- IDENTIFICAÇÃO DA OBRA DE ARTE / CÓDIGO
		update #temp1 set atv_valor = (select top 1 obj_codigo from dbo.tab_objetos where obj_id = @obj_id_TipoObraDeArte) where atr_id = 102;

	   -- Nome da Obra de Arte
		update #temp1 set atv_valor = (select top 1 obj_descricao from dbo.tab_objetos where obj_id = @obj_id_TipoObraDeArte) where atr_id = 105;

	   -- Nome Rodovia
	    update #temp1 set atv_valor = (select top 1 obj_descricao from dbo.tab_objetos where obj_id = @obj_id_Rodovia) where atr_id = 107;

	   -- Código da Rodovia
		update #temp1 set atv_valor = @CodigoRodovia where atr_id = 106;

	   -- Localização - KM
		update #temp1 set atv_valor = @KM_ObraDeArte where atr_id = 13;

	   -- municipio
		update #temp1 set atv_valor = @municipio where atr_id = 14;

	   -- combo tipoOAE

	    declare @tipoOAEValor varchar (10)= '';
		 set @clo_id_aux = (select clo_id from dbo.tab_objetos where obj_id = @obj_id_orig);

		 --if (@clo_id_aux > 2)
		 	set @tipoOAEValor = (select top 1 RIGHT('000'+CAST(tip_id AS VARCHAR(3)),3) from dbo.tab_objetos where obj_id = @obj_id_TipoObraDeArte);

	   declare @cmbOEA_NItens int= 0;
	   set @cmbOEA_NItens = (select count(*) from dbo.tab_objeto_tipos	where clo_id = 3 and tip_ativo = 1 and tip_deletado is null);

	    declare @cmbOEAItens varchar(max)= '';
		select  @cmbOEAItens = COALESCE(@cmbOEAItens + ';', '') + RIGHT('000'+CAST(tip_id AS VARCHAR(3)),3) + tip_nome + ' (' + tip_codigo + ')'
		 from dbo.tab_objeto_tipos 
		  where clo_id = 3 and tip_ativo = 1 and tip_deletado is null
		order by tip_nome;
		set @cmbOEAItens = substring(@cmbOEAItens,2, 1000000); -- retira o 1o ponto e virgula

		--select @tipoOAEValor, @obj_id_TipoObraDeArte

		update #temp1 set
				atv_valor = @tipoOAEValor,
				atr_apresentacao_itens = 'combobox', 
				nItens = @cmbOEA_NItens,
				atr_itens_todos= @cmbOEAItens 
		 where atr_id = 98;





	  -- **********************	COLOCA OS DADOS ABA HISTORICO ******************
	  		alter table #temp1 alter column atv_controle varchar(150) null;

			declare @tos_descricao_1 varchar(255) ='';
			declare @tos_descricao_2 varchar(255) ='';
			declare @tos_descricao_3 varchar(255) ='';
			declare	@ins_documento nvarchar(255) = null
			declare	@ins_data nvarchar(15) = null
			declare	@ins_executantes nvarchar(500) = null
			declare	@ins_pontuacaoOAE nvarchar(5) = null
			declare	@ins_documento_2 nvarchar(255) = null
			declare	@ins_data_2 nvarchar(15) = null
			declare	@ins_executantes_2 nvarchar(500) = null
			declare	@ins_pontuacaoOAE_2 nvarchar(5) = null
			declare	@ins_documento_3 nvarchar(255) = null
			declare	@ins_data_3 nvarchar(15) = null
			declare	@ins_executantes_3 nvarchar(500) = null
			declare	@ins_pontuacaoOAE_3 nvarchar(5) = null

		SELECT  
			@tos_descricao_1 = isnull((select tos_descricao from [dbo].[tab_ordem_servico_tipos] where tos_id = ins.ins_tos_id_1),''),
			@tos_descricao_2 = isnull((select tos_descricao from [dbo].[tab_ordem_servico_tipos] where tos_id = ins.ins_tos_id_2),''),
			@tos_descricao_3 = isnull((select tos_descricao from [dbo].[tab_ordem_servico_tipos] where tos_id = ins.ins_tos_id_3),''),
			@ins_documento = ins_documento, 
			@ins_data = ins_data, 
			@ins_executantes = ins_executantes, 
			@ins_pontuacaoOAE = ins_pontuacaoOAE, 
			@ins_documento_2 = ins_documento_2, 
			@ins_data_2 = ins_data_2, 
			@ins_executantes_2 = ins_executantes_2, 
			@ins_pontuacaoOAE_2 = ins_pontuacaoOAE_2, 
			@ins_documento_3 = ins_documento_3, 
			@ins_data_3 = ins_data_3, 
			@ins_executantes_3 = ins_executantes_3, 
			@ins_pontuacaoOAE_3 = ins_pontuacaoOAE_3
		FROM dbo.tab_ordens_servico ord
		inner join dbo.tab_inspecoes ins on ins.ord_id = ord.ord_id and ins.ins_deletado is null --and ins.ins_ativo = 1 --or ins.ins_data is null
		inner join dbo.tab_ordem_servico_tipos tos on tos.tos_id = ord.tos_id and tos.tos_deletado is null --and tos.tos_ativo = 1
		where ins.ins_deletado is null 
			and  ord.ord_id = @ord_id;
	
			if (@tos_descricao_1 <> '')
			  set @tos_descricao_1 = @tos_descricao_1 + ' Atual';

			if (@tos_descricao_2 <> '')
			  set @tos_descricao_2 = @tos_descricao_2 + ' Anterior';

			if (@tos_descricao_3 <> '')
			  set @tos_descricao_3 = @tos_descricao_3 + ' Anterior';


			insert into #temp1 (obj_id,  clo_id,    tip_id, atr_id, atr_atributo_nome   ,atr_descricao                    ,atr_mascara_texto, atv_controle  ,atv_valor                 ,atv_valores      ,ati_ids, atr_apresentacao_itens, nItens, atr_itens_todos)
			values (@obj_id, @clo_id_aux,-1, -1001 , @tos_descricao_1, (@tos_descricao_1), NULL, 'lbl_historico_Inspecao_1', isnull(@tos_descricao_1,''), isnull(@tos_descricao_1,''), -1    ,  '' ,0, '' );
		
			insert into #temp1 (obj_id,clo_id,tip_id,atr_id,atr_atributo_nome,atr_descricao,atr_mascara_texto,atv_controle,atv_valor,atv_valores,ati_ids,atr_apresentacao_itens,nItens,atr_itens_todos)
			values (@obj_id, @clo_id_aux,-1, -1001 , 'HistoricoDocumento_1', 'Documento da Inspecao', NULL, 'txt_historico_documento_1', isnull(@ins_documento,''), isnull(@ins_documento,''), -1    ,  ''                   ,      0, '' );

			insert into #temp1 (obj_id,clo_id,tip_id,atr_id,atr_atributo_nome,atr_descricao,atr_mascara_texto,atv_controle,atv_valor,atv_valores,ati_ids,atr_apresentacao_itens,nItens,atr_itens_todos)
			values (@obj_id, @clo_id_aux, -1, -1001, 'HistoricoData_1', 'Data da Inspecao', NULL, 'txt_historico_data_1', isnull(@ins_data,''), isnull(@ins_data,''), -1, '',0,'' );
		
			insert into #temp1 (obj_id,clo_id,tip_id,atr_id,atr_atributo_nome,atr_descricao,atr_mascara_texto,atv_controle,atv_valor,atv_valores,ati_ids,atr_apresentacao_itens,nItens,atr_itens_todos)
			values (@obj_id, @clo_id_aux, -1, -1001, 'HistoricoExecutantes_1', 'Executantes', NULL, 'txt_historico_executantes_1', isnull(@ins_executantes,''), isnull(@ins_executantes,''), -1, '',0,'' );

			insert into #temp1 (obj_id,clo_id,tip_id,atr_id,atr_atributo_nome,atr_descricao,atr_mascara_texto,atv_controle,atv_valor,atv_valores,ati_ids,atr_apresentacao_itens,nItens,atr_itens_todos)
			values (@obj_id, @clo_id_aux, -1, -1001, 'PontuacaoOAE_1', 'Pontuacao OAE', NULL,  'txt_historico_Pontuacao_Geral_OAE_1', isnull(@ins_pontuacaoOAE,''), isnull(@ins_pontuacaoOAE,''), -1, '',0,'' );

	
			insert into #temp1 (obj_id,  clo_id,    tip_id, atr_id, atr_atributo_nome   ,atr_descricao                    ,atr_mascara_texto, atv_controle  ,atv_valor                 ,atv_valores      ,ati_ids, atr_apresentacao_itens, nItens, atr_itens_todos)
			values (@obj_id, @clo_id_aux,-1, -1001 , @tos_descricao_2, @tos_descricao_2, NULL, 'lbl_historico_Inspecao_2', isnull(@tos_descricao_2,''), isnull(@tos_descricao_2,''), -1    ,  '' ,0, '' );
		
			insert into #temp1 (obj_id,clo_id,tip_id,atr_id,atr_atributo_nome,atr_descricao,atr_mascara_texto,atv_controle,atv_valor,atv_valores,ati_ids,atr_apresentacao_itens,nItens,atr_itens_todos)
			values (@obj_id, @clo_id_aux,-1, -1001 , 'HistoricoDocumento_2', 'Documento da Inspecao', NULL, 'txt_historico_documento_2', isnull(@ins_documento_2,''), isnull(@ins_documento_2,''), -1    ,  ''                   ,      0, '' );

			insert into #temp1 (obj_id,clo_id,tip_id,atr_id,atr_atributo_nome,atr_descricao,atr_mascara_texto,atv_controle,atv_valor,atv_valores,ati_ids,atr_apresentacao_itens,nItens,atr_itens_todos)
			values (@obj_id, @clo_id_aux, -1, -1001, 'HistoricoData_2', 'Data da Inspecao', NULL, 'txt_historico_data_2', isnull(@ins_data_2,''), isnull(@ins_data_2,''), -1, '',0,'' );
		
			insert into #temp1 (obj_id,clo_id,tip_id,atr_id,atr_atributo_nome,atr_descricao,atr_mascara_texto,atv_controle,atv_valor,atv_valores,ati_ids,atr_apresentacao_itens,nItens,atr_itens_todos)
			values (@obj_id, @clo_id_aux, -1, -1001, 'HistoricoExecutantes_2', 'Executantes', NULL, 'txt_historico_executantes_2', isnull(@ins_executantes_2,''), isnull(@ins_executantes_2,''), -1, '',0,'' );

			insert into #temp1 (obj_id,clo_id,tip_id,atr_id,atr_atributo_nome,atr_descricao,atr_mascara_texto,atv_controle,atv_valor,atv_valores,ati_ids,atr_apresentacao_itens,nItens,atr_itens_todos)
			values (@obj_id, @clo_id_aux, -1, -1001, 'PontuacaoOAE_2', 'Pontuacao OAE', NULL,  'txt_historico_Pontuacao_Geral_OAE_2', isnull(@ins_pontuacaoOAE_2,''), isnull(@ins_pontuacaoOAE_2,''), -1, '',0,'' );

	
			insert into #temp1 (obj_id,  clo_id,    tip_id, atr_id, atr_atributo_nome   ,atr_descricao                    ,atr_mascara_texto, atv_controle  ,atv_valor                 ,atv_valores      ,ati_ids, atr_apresentacao_itens, nItens, atr_itens_todos)
			values (@obj_id, @clo_id_aux,-1, -1001 , @tos_descricao_3, @tos_descricao_3, NULL, 'lbl_historico_Inspecao_3', isnull(@tos_descricao_3,''), isnull(@tos_descricao_3,''), -1    ,  '' ,0, '' );
		
			insert into #temp1 (obj_id,clo_id,tip_id,atr_id,atr_atributo_nome,atr_descricao,atr_mascara_texto,atv_controle,atv_valor,atv_valores,ati_ids,atr_apresentacao_itens,nItens,atr_itens_todos)
			values (@obj_id, @clo_id_aux,-1, -1001 , 'HistoricoDocumento_3', 'Documento da Inspecao', NULL, 'txt_historico_documento_3', isnull(@ins_documento_3,''), isnull(@ins_documento_3,''), -1    ,  ''                   ,      0, '' );

			insert into #temp1 (obj_id,clo_id,tip_id,atr_id,atr_atributo_nome,atr_descricao,atr_mascara_texto,atv_controle,atv_valor,atv_valores,ati_ids,atr_apresentacao_itens,nItens,atr_itens_todos)
			values (@obj_id, @clo_id_aux, -1, -1001, 'HistoricoData_3', 'Data da Inspecao', NULL, 'txt_historico_data_3', isnull(@ins_data_2,''), isnull(@ins_data_3,''), -1, '',0,'' );
		
			insert into #temp1 (obj_id,clo_id,tip_id,atr_id,atr_atributo_nome,atr_descricao,atr_mascara_texto,atv_controle,atv_valor,atv_valores,ati_ids,atr_apresentacao_itens,nItens,atr_itens_todos)
			values (@obj_id, @clo_id_aux, -1, -1001, 'HistoricoExecutantes_3', 'Executantes', NULL, 'txt_historico_executantes_3', isnull(@ins_executantes_3,''), isnull(@ins_executantes_3,''), -1, '',0,'' );

			insert into #temp1 (obj_id,clo_id,tip_id,atr_id,atr_atributo_nome,atr_descricao,atr_mascara_texto,atv_controle,atv_valor,atv_valores,ati_ids,atr_apresentacao_itens,nItens,atr_itens_todos)
			values (@obj_id, @clo_id_aux, -1, -1001, 'PontuacaoOAE_3', 'Pontuacao OAE', NULL,  'txt_historico_Pontuacao_Geral_OAE_3', isnull(@ins_pontuacaoOAE_3,''), isnull(@ins_pontuacaoOAE_3,''), -1, '',0,'' );


	
-- ****************	 documentos de referencia **************************************************

if (@ord_id > 0)
begin

		declare @docs varchar(max) = '';
					select 
						 @docs = COALESCE(@docs + '', '') + (doc_codigo + ' - ' +  doc_descricao + ';<br />')
					from [dbo].[tab_documento_ordens_servicos] dos
						inner join [dbo].[tab_documentos] doc on doc.doc_id = dos.doc_id and doc.doc_deletado is null and doc.doc_ativo =1
					where dos_deletado is null
						and [ord_id] = @ord_id
						and dos_referencia = 1
					order by  doc_codigo;

		alter table #temp1 alter column atv_valor varchar(max);
		alter table #temp1 alter column atv_valores varchar(max);

		insert into #temp1 (obj_id ,clo_id, tip_id,atr_id,     atr_atributo_nome,         atr_descricao,          atv_controle,atv_valor, atv_valores, ati_ids, atr_apresentacao_itens, nItens, atr_itens_todos)
		values             (@obj_id, 3    ,     -1, -1000, 'lbl_docs_referencia', 'lbl_docs_referencia', 'lbl_docs_referencia',    @docs,       @docs,      '',                     '',      0, '' );

end

	-- ============== RECALCULA AS NOTAS DA INSPECAO ROTINEIRA ============================================================================================================
		update #temp1 set
				atv_valor = dbo.calcula_nota_ficha_rotineira (-1, @ins_id, 1)
				,atv_valores = dbo.calcula_nota_ficha_rotineira (-1, @ins_id, 1)
		 where atr_id = 151; -- parcial 1

		update #temp1 set
				atv_valor = dbo.calcula_nota_ficha_rotineira (-1, @ins_id, 2)
				,atv_valores = dbo.calcula_nota_ficha_rotineira (-1, @ins_id, 2)
		 where atr_id = 152; -- parcial 2

		update #temp1 set
				atv_valor = dbo.calcula_nota_ficha_rotineira (-1, @ins_id, 3)
				,atv_valores = dbo.calcula_nota_ficha_rotineira (-1, @ins_id, 3)
		 where atr_id = 157; -- total
	-- ================================================================================================================================
	

	  -- *********************************************************************
		-- retorna
		select * 
		from #temp1
		order by atr_id;

		-- checa se as tmp table existe e a exclui
		IF OBJECT_ID('tempdb..#temp1') IS NOT NULL
			DROP TABLE #temp1;


end ;
