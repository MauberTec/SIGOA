var cabecalho1 = '<tr id="trFICHA2_OOBBJJIIDD_GGG_VVV_YYY"><td class="borderLeft borderBottomPt borderRight subdivisao1_fundo" colspan="10"><label class="lblsBold" >XXXXX</label></td></tr>';

var cabecalho2 =
    ' <tr id="trFICHA2_OOBBJJIIDD_GGG_VVV_YYY">' +
    ' <td class="borderLeft borderBottomPt borderRight subdivisao2_fundo" colspan = "10" >' +
    '     <table style="width:100%"> ' +
    '      <tr> ' +
    '        <td> ' +
    '           <label class="lblsBold" > XXXXX</label >' +
    '       </td> ' +
    '        <td> ' +
    '           <label class="lblsBold" > </label >' +
    '       </td> ' +
    '     </tr> ' +
    '   </table> ' +

    ' </td >' +
    ' </tr > ';


var cabecalho3 = '<tr id="trFICHA2_OOBBJJIIDD_GGG_VVV_YYY"><td class="borderLeft borderBottomPt borderRight subdivisao3_fundo" colspan="10"><label class="lblsBold" >XXXXX</label></td></tr>';

var MesclarGrupo =
    '   <td class="borderLeft borderTop borderRight borderBottomPt" rowspan=N_ROWSPAN > ' +
    '     <table style="width:100%"> ' +
    '      <tr> ' +
    '        <td> ' +
    '           <label id="lbl_Elemento_GGG_VVV" class="lblsNormal">lbl_Elemento_XXXXX</label> ' +
    '        </td> ' +
    '     </tr> ' +
    '   </table> ' +
    '  </td> ';

var Mesclar_Condicao_Inspecao =
    '  <td class="borderTop borderRight borderBottomPt centroH" rowspan=N_ROWSPAN > ' +
    '    <select class="cmbs" id="cmb_condicao_GGG_VVV"> ' +
    '          OPCOES_cmb_condicao ' +
    '    </select> ' +
    '  </td> ';

var linhaGrupos =
    ' <tr id="trFICHA2_OOBBJJIIDD_GGG_VVV_YYY"> ' +

    '  MesclarGrupo  ' +

    '  <td class="borderTop borderRight borderBottomPt"> ' +
    '    <label id="lbl_Variaveis_GGG_VVV" class="lblsNormal">lbl_Variaveis_XXXXX</label> ' +
    '  </td> ' +
    '  <td class="borderTop borderRight borderBottomPt centroH"> ' +
    '    <select class="cmbs" id="cmb_situacao_GGG_VVV" title="TOOLTIP_cmb_situacao"  onchange="cmb_situacao_onchange(this)"> ' +
    '          OPCOES_cmb_situacao ' +
    '    </select> ' +
    '  </td> ' +

    ' Mesclar_Condicao_Inspecao ' +

    '  <td class="borderTop borderRight borderBottomPt" colspan="2" style="text-align:center;"> ' +
    '    <textarea id="txt_obs_GGG_VVV" class="txts" rows="2" cols="50"  maxlength="255" style="width:98%; overflow:auto">txt_obs_XXXXX</textarea> ' +
    '  </td> ' +
    '  <td class="borderTop borderRight borderBottomPt"> ' +
    '    <label id="lbl_servico_GGG_VVV" class="txts" style="border:none; width:100%; text-align:left" title="txt_servico_XXXXX">txt_servico_XXXXX</label> ' +
    '    <select class="cmbs" id="cmb_tpu_descricao_itens_GGG_VVV" style="display:none"> ' +
    '          OPCOES_cmb_tpu_descricao_itens ' +
    '    </select> ' +
    '  </td> ' +
    '  <td class="borderTop borderRight borderBottomPt  centroH"> ' +
    '    <input id="txt_unidade_GGG_VVV" class="txts  centroH" value="txt_unidade_XXXXX" maxlength="15"  /> ' +
    '  </td> ' +
    '  <td class="borderTop borderRight borderBottomPt centroH"> ' +
    '    <input id="txt_quantidade_GGG_VVV" class="txts  centroH" value="txt_quantidade_XXXXX" /> ' +
    '  </td> ' +
    '  <td class="borderTop borderRight borderBottomPt centroH"> ' +
    '    ' +
    '  </td> ' +

    ' </tr> ';

function Ficha2_PROVIDENCIAS_Imprimir(apt_id) {

    window.open('../../Reports/frmReport.aspx?relatorio=rptFichaInspecaoRotineira_Providencias&id=999999&ord_id=' + selectedId_ord_id + '&apt_id=' + apt_id, '_blank');

    return false;
}


function Ficha2_PROVIDENCIAS_limpar() {

    var tabela = document.getElementById("divFicha2_PROVIDENCIAS");

    // habilita ou desabilita todos os controles editaveis
    var lstTxtBoxes = tabela.getElementsByTagName('input');
    var lstCombos = tabela.getElementsByTagName('select');
    var lstTextareas = tabela.getElementsByTagName('textarea');

    for (var i = 0; i < lstTxtBoxes.length; i++)
        lstTxtBoxes[i].value = "";

    for (var i = 0; i < lstTextareas.length; i++)
        lstTextareas[i].value = "";

    for (var i = 0; i < lstCombos.length; i++)
        lstCombos[i].innerText = null;

}

function preenchetblFicha2_PROVIDENCIAS(obj_id, classe, tipo) {
    classe = parseInt(classe);
    tipo = parseInt(tipo);
    selectedId_clo_id = classe;
    selectedId_tip_id = tipo;


    // limpa antes de preencher
    Ficha2_PROVIDENCIAS_limpar();


    var ord_id = 0;
    if ((paginaPai == "OrdemServico") || (paginaPai == "Inspecao"))
        ord_id = selectedId_ord_id;

    var url = "/Objeto/ObjAtributoValores_ListAll";
    var data = { "obj_id": obj_id, "ord_id": ord_id };

    if (moduloCorrente == 'OrdemServico') {
        var StatusOS = parseInt(filtroStatusOS);
        // if (StatusOS == 11) {

        if (StatusOS <= 11) {
            url = "/Inspecao/InspecaoAtributosValores_ListAll";
            data = { "ord_id": selectedId_ord_id };
        }
    }

    $.ajax({
        "url": url,
        "type": "GET",
        "datatype": "json",
        "data": data,
        "success": function (result) {
            for (var i = 0; i < result.data.length; i++) {

                // preenche os LABELS
                var label = document.getElementById(result.data[i].atv_controle.replace("chk_", "lbl").replace("cmb_", "lbl"));
                if (label) {
                    // procura o label2
                    var label2 = document.getElementById((nome_segundo_cabecalho2(label.id)));

                    var texto = result.data[i].atr_atributo_nome;
                    if (texto.includes('|')) {
                        partes = texto.split("|");
                        texto = partes[partes.length - 1].trim();
                    }
                    label.innerText = texto;

                    if (label2)
                        label2.innerText = texto;
                }

                // preenche o valor se houver
                if (parseInt(result.data[i].nItens) == 0) {
                    var textbox = document.getElementById(result.data[i].atv_controle.replace("lbl", "txt_"));
                    var textbox2 = document.getElementById((nome_segundo_cabecalho2(result.data[i].atv_controle.replace("lbl", "txt_"))).replace("lbl", "txt_"));
                    var mascara = result.data[i].atr_mascara_texto;

                    if (textbox) {
                        textbox.value = result.data[i].atv_valor;

                        // coloca mascara no textbox
                        if (mascara != "") {
                            jQuery(textbox).mask(mascara);
                            jQuery(textbox).attr('placeholder', mascara.replace(/9/g, '0'));
                        }

                    }
                    if (textbox2) {
                        textbox2.value = result.data[i].atv_valor;

                        if (mascara != "") {
                            jQuery(textbox2).mask(mascara);
                            jQuery(textbox).attr('placeholder', mascara.replace(/9/g, '0'));
                        }
                    }
                }
                else
                    if (result.data[i].atr_apresentacao_itens == 'combobox') {
                        var combo = document.getElementById(result.data[i].atv_controle);
                        var combo2 = document.getElementById((nome_segundo_cabecalho2(result.data[i].atv_controle)));

                        if (combo) {
                            combo.innerText = null; // limpa

                            // preenche combo
                            var lista = result.data[i].atr_itens_todos.split(";");
                            for (var m = 0; m < lista.length; m++) {
                                var opt = document.createElement("option");
                                opt.value = lista[m].substring(0, 3);
                                opt.textContent = lista[m].substring(3);

                                combo.appendChild(opt);
                            }

                            combo.value = result.data[i].atv_valor;
                        }

                        if (combo2) {
                            combo2.innerText = null; // limpa

                            // preenche combo
                            var lista = result.data[i].atr_itens_todos.split(";");
                            for (var m = 0; m < lista.length; m++) {
                                var opt2 = document.createElement("option");
                                opt2.value = lista[m].substring(0, 3);
                                opt2.textContent = lista[m].substring(3);
                                combo2.appendChild(opt2);
                            }
                            combo2.value = result.data[i].atv_valor;
                        }

                    }
                    else
                        if (result.data[i].atr_apresentacao_itens == 'checkbox') {
                            var checkbox_prefixo = result.data[i].atv_controle;

                            var lista = result.data[i].atr_itens_todos.split(";");
                            for (var m = 0; m < lista.length; m++) {
                                var valor = lista[m].substring(0, 3);
                                var texto = lista[m].substring(3);
                                var checkbox = document.getElementById(checkbox_prefixo + "_" + parseInt(valor));
                                if (checkbox) {
                                    var label = document.getElementById(checkbox_prefixo.replace("chk_", "lbl") + "_" + parseInt(valor));
                                    if (label)
                                        label.innerText = texto;

                                    checkbox.value = valor;

                                    // preenche os valores correspondentes
                                    if (parseInt(valor) == parseInt(result.data[i].ati_ids)) {
                                        var tick = result.data[i].atv_valor.substring(0, 1);
                                        checkbox.checked = tick == "1" ? true : false;

                                        // procura um textbox correspondente, do tipo checkbox+textbox
                                        var txt = document.getElementById(checkbox_prefixo.replace("chk", "txt") + "_" + parseInt(valor));
                                        if (txt) {
                                            var texto = result.data[i].atv_valor.substr(2);
                                            if (texto != "") {
                                                txt.value = texto;
                                                checkbox.checked = true;
                                            }
                                        }
                                    }
                                }
                            }
                        }

            }

        }
    });



    if ((paginaPai == "OrdemServico") || (paginaPai == "Inspecao")) {

            Ficha2_PROVIDENCIAS_CriarTabelaGrupos();
    }

}

function Ficha2_PROVIDENCIAS_CriarTabelaGrupos(ehRead) {

    if (ehRead == null)
        ehRead = true;

    // limpa as linhas se houver
    var table = document.getElementById("tblFicha2_PROVIDENCIAS_GRUPOS");
    var rowCount = table.rows.length;
    for (var i = rowCount - 1; i >= 0; i--) {
        if (table.rows[i].id.indexOf("trFICHA2_") >= 0)
            table.deleteRow(i);
    }


    var linhas = '';
    var celulaPai = document.getElementById("Ficha2_PROVIDENCIAS_tr_Cabecalho");

    var ord_id = 0;
    if ((paginaPai == "OrdemServico") || (paginaPai == "Inspecao"))
        ord_id = selectedId_ord_id;

    $.ajax({
        "url": "/Objeto/GruposVariaveisValores_ListAll",
        "type": "GET",
        "datatype": "json",
        "data": { "obj_id": selectedId_obj_id, "ord_id": ord_id },
        "success": function (result) {
            for (var i = 0; i < result.data.length; i++) {
                if (parseInt(result.data[i].nCabecalhoGrupo) == 1)  // CABECALHO 1
                    linhas = linhas + cabecalho1.replace("XXXXX", result.data[i].nome_pai).replace("YYY", i);
                else
                    if (parseInt(result.data[i].nCabecalhoGrupo) == 2) // CABECALHO 2
                    {
                        var visibilitySub2 = "hidden";
                        //if ((parseInt(result.data[i].tip_pai) == 22) || (parseInt(result.data[i].tip_pai) == 23) )
                        //    visibilitySub2 = "visible";

                        linhas = linhas + cabecalho2.replace(/TIPP_IIDD/g, result.data[i].tip_pai).replace(/XXXXX/g, result.data[i].nome_pai).replace(/YYY/g, i).replace("visibilitySubdivisao2", visibilitySub2);
                    }
                    else
                        if (parseInt(result.data[i].nCabecalhoGrupo) == 3) // CABECALHO 3
                            linhas = linhas + cabecalho3.replace("XXXXX", result.data[i].nome_pai).replace("YYY", i);
                        else
                            if (parseInt(result.data[i].nCabecalhoGrupo) == 0) // LINHA NORMAL
                            {
                                var linhaAux = linhaGrupos;
                                linhaAux = linhaAux.replace("YYY", i);

                                // MESCLA CELULAS SE NECESSARIO
                                if ((result.data[i].nomeGrupo.trim() != "") && (parseInt(result.data[i].mesclarLinhas) > 0)) {
                                    linhaAux = linhaAux.replace(/MesclarGrupo/g, MesclarGrupo);
                                    linhaAux = linhaAux.replace(/Mesclar_Condicao_Inspecao/g, Mesclar_Condicao_Inspecao);
                                    linhaAux = linhaAux.replace(/N_ROWSPAN/g, result.data[i].mesclarLinhas);
                                }
                                else {
                                    linhaAux = linhaAux.replace(/MesclarGrupo/g, "");
                                    linhaAux = linhaAux.replace(/Mesclar_Condicao_Inspecao/g, "");
                                }


                                // cria os itens do combo tpu_descricao_itens_cmb ========================================
                                var str = result.data[i].tpu_descricao_itens_cmb;
                                var op = ' <option selectedXX value="valor">texto</option> ';
                                var total = op;

                                if (str != "") {
                                    var selectedValue = result.data[i].ogi_id_caracterizacao_situacao;
                                    total = total.replace("selectedXX", "");

                                    var pedacos = str.split(";");
                                    for (k = 0; k < pedacos.length; k++) {
                                        var aux = pedacos[k].split(":");
                                        var opt = op;
                                        opt = opt.replace("valor", aux[0]).replace("texto", aux[1]);

                                        // checa se é o item selecionado
                                        if (parseInt(selectedValue) == parseInt(aux[0]))
                                            opt = opt.replace("selectedXX", "selected");
                                        else
                                            opt = opt.replace("selectedXX", "");

                                        total = total + opt;
                                    }
                                    linhaAux = linhaAux.replace(/OPCOES_cmb_tpu_descricao_itens/g, total);
                                }


                                // cria os itens do combo cmb_situacao ========================================
                                var op0 = ' <option selectedXX value="-1" disabled></option> ';
                                var op = ' <option selectedXX value="valor" title="tooltips" >texto</option> ';
                                var total = op0;
                                var str = result.data[i].caracterizacao_situacao_cmb;
                                var selectedValue = result.data[i].ogi_id_caracterizacao_situacao;

                                if (parseInt(selectedValue) == 0)
                                    total = total.replace("selectedXX", "selected");
                                else
                                    total = total.replace("selectedXX", "");

                                var pedacos = str.split(";");
                                for (k = 0; k < pedacos.length; k++) {
                                    var aux = pedacos[k].split(":");
                                    var aux2 = aux[1].split("|");

                                    var opt = op;
                                    opt = opt.replace("valor", aux[0]);
                                    opt = opt.replace("texto", aux2[0]);
                                    if (aux2.length > 1)
                                        opt = opt.replace("tooltips", aux2[1]);
                                    else
                                        opt = opt.replace("tooltips", "");

                                    // checa se é o item selecionado
                                    if (parseInt(selectedValue) == parseInt(aux[0])) {
                                        opt = opt.replace("selectedXX", "selected");
                                        if (aux2.length > 1)
                                            linhaAux = linhaAux.replace("TOOLTIP_cmb_situacao", aux2[1]);
                                    }
                                    else
                                        opt = opt.replace("selectedXX", "");

                                    total = total + opt;
                                }
                                linhaAux = linhaAux.replace("TOOLTIP_cmb_situacao", " ");
                                linhaAux = linhaAux.replace(/OPCOES_cmb_situacao/g, total);

                                // cria os itens do combo OPCOES_cmb_condicao============================================
                                var op0 = ' <option selectedXX value="0" disabled></option> ';
                                var op = '  <option selectedXX value="valor">texto</option> ';
                                total = op0;
                                if (parseInt(selectedValue) == 0)
                                    total = total.replace("selectedXX", "selected");
                                else
                                    total = total.replace("selectedXX", "");

                                var selectedValue = result.data[i].ati_id_condicao_inspecao;
                                var str = result.data[i].condicao_inspecao_cmb;
                                var pedacos = str.split(";");
                                for (k = 0; k < pedacos.length; k++) {
                                    var aux = pedacos[k].split(":");
                                    var opt = op;
                                    opt = opt.replace("valor", aux[0]).replace("texto", aux[1]);

                                    // checa se é o item selecionado
                                    if (parseInt(selectedValue) == parseInt(aux[0]))
                                        opt = opt.replace("selectedXX", "selected");
                                    else
                                        opt = opt.replace("selectedXX", "");

                                    total = total + opt;
                                }
                                linhaAux = linhaAux.replace(/OPCOES_cmb_condicao/g, total);


                                if (result.data[i].nomeGrupo.trim() == "") // LIXEIRA
                                    linhaAux = linhaAux.replace(/displayZYZ/g, "hidden");
                                else
                                    if ((parseInt(result.data[i].TemFilhos) == 0))
                                        linhaAux = linhaAux.replace(/displayZYZ/g, "visible");
                                    else
                                        linhaAux = linhaAux.replace(/displayZYZ/g, "hidden");

                                // COLOCA OS DADOS
                                linhaAux = linhaAux.replace(/OOBBJJIIDD/g, result.data[i].obj_id);
                                linhaAux = linhaAux.replace(/GGG/g, result.data[i].obj_id);
                                linhaAux = linhaAux.replace(/VVV/g, result.data[i].cgv_id);
                                linhaAux = linhaAux.replace(/lbl_Elemento_XXXXX/g, result.data[i].nomeGrupo);
                                linhaAux = linhaAux.replace(/lbl_Variaveis_XXXXX/g, result.data[i].variavel);
                                linhaAux = linhaAux.replace(/txt_obs_XXXXX/g, result.data[i].ovv_observacoes_gerais);
                                linhaAux = linhaAux.replace(/txt_servico_XXXXX/g, result.data[i].tpu_descricao);
                                linhaAux = linhaAux.replace(/txt_unidade_XXXXX/g, result.data[i].uni_unidade);
                                //  linhaAux = linhaAux.replace(/lbl_unidade_XXXXX/g, result.data[i].uni_unidade);
                                linhaAux = linhaAux.replace(/txt_quantidade_XXXXX/g, result.data[i].ovv_tpu_quantidade);

                                linhas = linhas + linhaAux;
                            }

            }

            // mescla na tabela existente
            celulaPai.insertAdjacentHTML('afterend', linhas);

            // coloca mascara no campo quantidade
            var qts = $('[id^="txt_quantidade_"]');
            for (var i = 0; i < qts.length; i++) {
                jQuery(qts[i]).attr('placeholder', "000.00");
                jQuery(qts[i]).mask("999.99");
            }


            //if (table)
            //    Ficha2_PROVIDENCIAS_setaReadWrite(table, ehRead);
        }
    });

}


function Ficha2_PROVIDENCIAS_setaReadWrite(tabela, ehRead) {
    // habilita ou desabilita todos os controles editaveis
    var lstTxtBoxes = tabela.getElementsByTagName('input');
    var lstCombos = tabela.getElementsByTagName('select');
    var lstTextareas = tabela.getElementsByTagName('textarea');
    var cmb_atr_id_98 = document.getElementById("cmb_atr_id_98");

    for (var i = 0; i < lstTxtBoxes.length; i++)
            lstTxtBoxes[i].disabled = ehRead;

    for (var i = 0; i < lstTextareas.length; i++)
            lstTextareas[i].disabled = ehRead;

    for (var i = 0; i < lstCombos.length; i++)
            lstCombos[i].disabled = ehRead;

}

