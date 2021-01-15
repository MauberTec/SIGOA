
var cabecalho3 = '<tr id="trFICHA2_OOBBJJIIDD_GGG_VVV_YYY"><td class="borderLeft borderBottomPt borderRight subdivisao3_fundo" colspan="9"><label class="lblsBold" >XXXXX</label></td></tr>';

var MesclarGrupo =
    '   <td class="borderLeft borderTop borderRight borderBottomPt" rowspan=N_ROWSPAN > ' +
    '     <table style="width:100%"> ' +
    '      <tr> ' +
    '        <td style="width:26px"> ' +
    '          <button id="btn_ExcluirGrupo_INSPECAO_ROTINEIRA_GGG_VVV" ' +
    '            type="button" ' +
    '             onclick="return Ficha2_ExcluirGrupo(OOBBJJIIDD)" ' +
    '             title="Excluir Grupo" ' +
    '             style="border:none; box-shadow:none; background-color:transparent; visibility:displayZYZ"> ' +
    '             <span class="glyphicon glyphicon-trash text-success contornoBranco"></span> ' +
    '          </button> ' +
    '        </td> ' +
    '        <td> ' +
    '           <label id="lbl_Elemento_GGG_VVV" class="lblsNormal">lbl_Elemento_XXXXX</label> ' +
    '       </td> ' +
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
    '    <select class="cmbs" id="cmb_situacao_GGG_VVV" onchange="cmb_situacao_onchange(this)"> ' +
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
    //   '    <input id="txt_servico_GGG_VVV" class="txts  centroH" value="txt_servico_XXXXX" maxlength="255" onkeyup="validaAlfaNumericoAcentosAfins(this, 0, 0)" /> ' +
    //'    <button id="btn_LocalizarGrupo_INSPECAO_ROTINEIRA_GGG_VVV" ' +
    //'            type="button" ' +
    //'            onclick="return Selecionar_TPU(this)" ' +
    //'            title="Selecionar Serviço" ' +
    //'            style="border:none; box-shadow:none; background-color:transparent; "> ' +
    //'        <span class="fa fa-search"></span> ' +
    //'    </button> ' +
    '  </td> ' +
    '  <td class="borderTop borderRight borderBottomPt  centroH"> ' +
    //    '    <label class="lblsBold" id="lbl_unidade_GGG_VVV">lbl_unidade_XXXXX</label> ' +
    '    <input id="txt_unidade_GGG_VVV" class="txts  centroH" value="txt_unidade_XXXXX" maxlength="15"  /> ' +
    '  </td> ' +
    '  <td class="borderTop borderRight borderBottomPt centroH"> ' +
    '    <input id="txt_quantidade_GGG_VVV" class="txts  centroH" value="txt_quantidade_XXXXX" /> ' +
    '  </td> ' +
    ' </tr> ';


//  var controlesReadOnlyFicha2 = ["txt_atr_id_13", "txt_atr_id_102", "txt_atr_id_105", "txt_atr_id_106", "txt_atr_id_107", "cmb_atr_id_130", "cmb_atr_id_131",
//"cmb_atr_id_1020", "cmb_atr_id_1084", "cmb_atr_id_1085", "cmb_atr_id_1087", "cmb_atr_id_1088", "cmb_atr_id_1089", "cmb_atr_id_1091", "cmb_atr_id_1092", "cmb_atr_id_1093", "cmb_atr_id_1094",
var controlesReadOnlyFicha2 = ["txt_atr_id_13", "txt_atr_id_102", "txt_atr_id_106", "cmb_atr_id_130", "cmb_atr_id_131", "cmb_atr_id_135", "cmb_atr_id_136", "cmb_atr_id_137", "cmb_atr_id_138", "cmb_atr_id_139", "cmb_atr_id_140", "cmb_atr_id_141", "cmb_atr_id_142", "cmb_atr_id_143", "cmb_atr_id_144", "txt_atr_id_151", "txt_atr_id_152", "txt_atr_id_153"
    //,"txt_historico_Pontuacao_Geral_OAE_1", "txt_historico_documento_2", "txt_historico_data_2", "txt_historico_executantes_2", "txt_historico_Pontuacao_Geral_OAE_2", "txt_historico_documento_3", "txt_historico_data_3", "txt_historico_executantes_3", "txt_historico_Pontuacao_Geral_OAE_3"
];

var controlesExcecoes_Salvar = ["cmb_atr_id_130", "cmb_atr_id_131", "cmb_atr_id_135", "cmb_atr_id_136", "cmb_atr_id_137", "cmb_atr_id_138", "cmb_atr_id_139", "cmb_atr_id_140", "cmb_atr_id_141", "cmb_atr_id_142", "cmb_atr_id_143", "cmb_atr_id_144", "cmb_atr_id_148", "cmb_atr_id_150", "txt_atr_id_151", "txt_atr_id_152", "txt_atr_id_153", "txt_atr_id_157"];

function Ficha2_header_click(quem, expandir) {
    if (expandir == null)
        expandir = 0;

    if (selectedId_clo_id <= 2) {
        expandir = 0;
        accordion_encolher(2);
    }
    else
        switch (quem.id) {

            case "btn_Toggle_HISTORICO_INSPECOES":
                {
                    // alterna os campos para leitura
                    Ficha2_setaReadWrite(tblFicha2_HISTORICO_INSPECOES, true);

                    // mostra o botao editar
                    if ((quem.getAttribute('aria-expanded') == "false") || (expandir == 1)) {
                        document.getElementById("btn_Salvar_HISTORICO_INSPECOES").style.display = 'none';
                        document.getElementById("btn_Cancelar_HISTORICO_INSPECOES").style.display = 'none';
                        document.getElementById("btn_Editar_HISTORICO_INSPECOES").style.display = 'block';
                        //  document.getElementById("btn_SelecionarGrupo_HISTORICO_INSPECOES").style.display = 'block';
                    }
                    else {
                        document.getElementById("btn_Editar_HISTORICO_INSPECOES").style.display = 'none';
                        //document.getElementById("btn_SelecionarGrupo_HISTORICO_INSPECOES").style.display = 'none';

                        if (quem.getAttribute('aria-expanded') == "true") {
                            document.getElementById("btn_Salvar_HISTORICO_INSPECOES").style.display = 'none';
                            document.getElementById("btn_Cancelar_HISTORICO_INSPECOES").style.display = 'none';
                        }
                        else {
                            document.getElementById("btn_Salvar_HISTORICO_INSPECOES").style.display = 'block';
                            document.getElementById("btn_Cancelar_HISTORICO_INSPECOES").style.display = 'block';
                        }

                    }

                    // roda o icone 90graus
                    document.getElementById("iconAngle_HISTORICO_INSPECOES").classList.toggle('rotate');
                    break;
                }

            case "btn_Toggle_INSPECAO_ROTINEIRA":
                {
                    // alterna os campos para leitura
                    Ficha2_setaReadWrite(tblFicha2_INSPECAO_ROTINEIRA, true);

                    // mostra o botao editar
                    if ((quem.getAttribute('aria-expanded') == "false") || (expandir == 1)) {
                        document.getElementById("btn_Salvar_INSPECAO_ROTINEIRA").style.display = 'none';
                        document.getElementById("btn_Cancelar_INSPECAO_ROTINEIRA").style.display = 'none';
                        document.getElementById("btn_Editar_INSPECAO_ROTINEIRA").style.display = 'block';
                        //  document.getElementById("btn_SelecionarGrupo_INSPECAO_ROTINEIRA").style.display = 'block';
                    }
                    else {
                        document.getElementById("btn_Editar_INSPECAO_ROTINEIRA").style.display = 'none';
                        document.getElementById("btn_SelecionarGrupo_INSPECAO_ROTINEIRA").style.display = 'none';

                        if (quem.getAttribute('aria-expanded') == "true") {
                            document.getElementById("btn_Salvar_INSPECAO_ROTINEIRA").style.display = 'none';
                            document.getElementById("btn_Cancelar_INSPECAO_ROTINEIRA").style.display = 'none';
                        }
                        else {
                            document.getElementById("btn_Salvar_INSPECAO_ROTINEIRA").style.display = 'block';
                            document.getElementById("btn_Cancelar_INSPECAO_ROTINEIRA").style.display = 'block';
                        }

                    }

                    // roda o icone 90graus
                    document.getElementById("iconAngle_INSPECAO_ROTINEIRA").classList.toggle('rotate');
                    break;
                }

            case "btn_Toggle_CRITERIO_DE_CLASSIFICACAO":
                {
                    // alterna os campos para leitura
                    Ficha2_setaReadWrite(tblFicha2_CRITERIO_DE_CLASSIFICACAO, true);

                    // mostra o botao editar
                    if ((quem.getAttribute('aria-expanded') == "false") || (expandir == 1)) {
                        document.getElementById("btn_Salvar_CRITERIO_DE_CLASSIFICACAO").style.display = 'none';
                        document.getElementById("btn_Cancelar_CRITERIO_DE_CLASSIFICACAO").style.display = 'none';
                        document.getElementById("btn_Editar_CRITERIO_DE_CLASSIFICACAO").style.display = 'block';
                    }
                    else {
                        document.getElementById("btn_Editar_CRITERIO_DE_CLASSIFICACAO").style.display = 'none';
                        if (quem.getAttribute('aria-expanded') == "true") {
                            document.getElementById("btn_Salvar_CRITERIO_DE_CLASSIFICACAO").style.display = 'none';
                            document.getElementById("btn_Cancelar_CRITERIO_DE_CLASSIFICACAO").style.display = 'none';
                        }
                        else {
                            document.getElementById("btn_Salvar_CRITERIO_DE_CLASSIFICACAO").style.display = 'block';
                            document.getElementById("btn_Cancelar_CRITERIO_DE_CLASSIFICACAO").style.display = 'block';
                        }
                    }

                    //calcula as notas
                    Ficha2_Calcula_Notas_2_Requisito();

                    // roda o icone 90graus
                    document.getElementById("iconAngle_CRITERIO_DE_CLASSIFICACAO").classList.toggle('rotate');
                    break;
                }

            case "btn_Toggle_NOTA_OAE_PARAMETRO_FUNCIONAL":
                {
                    // alterna os campos para leitura
                    Ficha2_setaReadWrite(tblFicha2_NOTA_OAE_PARAMETRO_FUNCIONAL, true);

                    // mostra o botao editar
                    if ((quem.getAttribute('aria-expanded') == "false") || (expandir == 1)) {
                        document.getElementById("btn_Salvar_NOTA_OAE_PARAMETRO_FUNCIONAL").style.display = 'none';
                        document.getElementById("btn_Cancelar_NOTA_OAE_PARAMETRO_FUNCIONAL").style.display = 'none';
                        document.getElementById("btn_Editar_NOTA_OAE_PARAMETRO_FUNCIONAL").style.display = 'block';
                    }
                    else {
                        document.getElementById("btn_Editar_NOTA_OAE_PARAMETRO_FUNCIONAL").style.display = 'none';
                        if (quem.getAttribute('aria-expanded') == "true") {
                            document.getElementById("btn_Salvar_NOTA_OAE_PARAMETRO_FUNCIONAL").style.display = 'none';
                            document.getElementById("btn_Cancelar_NOTA_OAE_PARAMETRO_FUNCIONAL").style.display = 'none';
                        }
                        else {
                            document.getElementById("btn_Salvar_NOTA_OAE_PARAMETRO_FUNCIONAL").style.display = 'block';
                            document.getElementById("btn_Cancelar_NOTA_OAE_PARAMETRO_FUNCIONAL").style.display = 'block';
                        }
                    }

                    // roda o icone 90graus
                    document.getElementById("iconAngle_NOTA_OAE_PARAMETRO_FUNCIONAL").classList.toggle('rotate');
                    break;
                }


            case "btn_Toggle_POLITICA_ACOES_A_IMPLEMENTAR":
                {
                    //// alterna os campos para leitura
                    //    Ficha2_setaReadWrite(tblFicha2_POLITICA_ACOES_A_IMPLEMENTAR, true);

                    //// mostra o botao editar
                    //if ((quem.getAttribute('aria-expanded') == "false") || (expandir == 1)) {
                    //    document.getElementById("btn_Salvar_POLITICA_ACOES_A_IMPLEMENTAR").style.display = 'none';
                    //    document.getElementById("btn_Cancelar_POLITICA_ACOES_A_IMPLEMENTAR").style.display = 'none';
                    //    document.getElementById("btn_Editar_POLITICA_ACOES_A_IMPLEMENTAR").style.display = 'block';
                    //}
                    //else {
                    //    document.getElementById("btn_Editar_POLITICA_ACOES_A_IMPLEMENTAR").style.display = 'none';
                    //    if (quem.getAttribute('aria-expanded') == "true") {
                    //        document.getElementById("btn_Salvar_POLITICA_ACOES_A_IMPLEMENTAR").style.display = 'none';
                    //        document.getElementById("btn_Cancelar_POLITICA_ACOES_A_IMPLEMENTAR").style.display = 'none';
                    //    }
                    //    else {
                    //        document.getElementById("btn_Salvar_POLITICA_ACOES_A_IMPLEMENTAR").style.display = 'block';
                    //        document.getElementById("btn_Cancelar_POLITICA_ACOES_A_IMPLEMENTAR").style.display = 'block';
                    //    }
                    //}

                    // roda o icone 90graus
                    document.getElementById("iconAngle_POLITICA_ACOES_A_IMPLEMENTAR").classList.toggle('rotate');
                    break;
                }

        }
}

function nome_segundo_cabecalho2(controleId) {
    var prefix = controleId.substring(0, controleId.lastIndexOf("_") + 1);
    var num = parseInt(controleId.substring(controleId.lastIndexOf("_") + 1)) + 1000;
    return (prefix + num);

}

function Ficha2_ExportarXLS() {
    $.ajax({
        "url": "/Objeto/ObjFichaInspecaoRotineira_ExportarXLS",
        "type": "GET",
        "datatype": "json",
        "data": { "obj_id": selectedId_obj_id, "ord_id": selectedId_ord_id },
        "success": function (result) {
            // return result = caminho da planilha preenchida e salva para download

            window.open("../../Reports/frmDownloadFile.aspx?filename=" + result.data);
            //  window.open("../../temp/" + result.data);
            return false;
        }
    });

    return false;
}

function Ficha2_limpar() {

    var tabela = document.getElementById("divFicha2");

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

function preenchetblFicha2(obj_id, classe, tipo) {
    classe = parseInt(classe);
    tipo = parseInt(tipo);
    selectedId_clo_id = classe;
    selectedId_tip_id = tipo;


    // limpa antes de preencher
    Ficha2_limpar();

    $('#txt_historico_data_1').datepicker({ dateFormat: 'dd/mm/yy' });
    $('#txt_historico_data_2').datepicker({ dateFormat: 'dd/mm/yy' });
    $('#txt_historico_data_3').datepicker({ dateFormat: 'dd/mm/yy' });

    jQuery("#txt_historico_Pontuacao_Geral_OAE_2").mask("99.99");
    jQuery("#txt_historico_Pontuacao_Geral_OAE_3").mask("99.99");

    var ord_id = 0;
    if (paginaPai == "OrdemServico")
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

            Ficha2_Calcula_Notas_Tudo();
        }
    });



    if (paginaPai == "OrdemServico") {
        if (qualFicha == 2) {
            preenchecmbTiposObjeto_FichaInspecaoRotineira();
        }

        if ((qualFicha == 2) || (qualFicha == 3)) {

            Ficha2_CriarTabelaGrupos();

            // document.getElementById("tblFicha2_HISTORICO_INSPECOES").style.display = "table";
            document.getElementById("btn_Toggle_INSPECAO_ROTINEIRA").innerText = "3 - INSPEÇÃO ROTINEIRA";
            document.getElementById("btn_Toggle_CRITERIO_DE_CLASSIFICACAO").innerText = "4 - NOTA GERAL DE AVALIAÇÃO E CLASSIFICAÇÃO DA OBRA DE ARTE (1º Requisito + 2º Requisito)";
            document.getElementById("btn_Toggle_NOTA_OAE_PARAMETRO_FUNCIONAL").innerText = "5 - NOTA DA OBRA DE ARTE PARA O PARÂMETRO FUNCIONAL (NBR 9452:2019) - Conforto e Segurança do Usuário";
            document.getElementById("btn_Toggle_POLITICA_ACOES_A_IMPLEMENTAR").innerText = "6 - POLÍTICA DE AÇÕES A SEREM IMPLEMENTADAS EM FUNÇÃO DAS NOTAS";

            //  if ((selectedId_tos_id == 2) && (qualFicha == 3))
            {
                if (qualFicha == 3)  // flag para "Inspecao Cadastral 2 = 1" ou "Inspecao Rotineira (campo) =2"
                {
                    document.getElementById("trFichaRotineiraHistorico_linha1").style.display = "default";
                    document.getElementById("trFichaRotineiraHistorico_linha2").style.display = "default";
                    document.getElementById("trFichaRotineiraHistorico_linha3").style.display = "default";
                }
                else
                    if (qualFicha == 2) {
                        document.getElementById("trFichaRotineiraHistorico_linha1").style.display = "default";
                        document.getElementById("trFichaRotineiraHistorico_linha2").style.display = "none";
                        document.getElementById("trFichaRotineiraHistorico_linha3").style.display = "none";
                    }
            }

            ////document.getElementById("tblFicha2_HISTORICO_INSPECOES").style.display = "none";
            ////// if ((selectedId_tos_id == 2) && (qualFicha == 3))
            ////if ((qualFicha == 3)) { // flag para "Inspecao Cadastral 1a rotineira = 1" ou "Inspecao Rotineira (campo) =2"
            ////    document.getElementById("tblFicha2_HISTORICO_INSPECOES").style.display = "table";
            ////    document.getElementById("btn_Toggle_INSPECAO_ROTINEIRA").innerText = "3 - INSPEÇÃO ROTINEIRA";
            ////    document.getElementById("btn_Toggle_CRITERIO_DE_CLASSIFICACAO").innerText = "4 - NOTA GERAL DE AVALIAÇÃO E CLASSIFICAÇÃO DA OBRA DE ARTE (1º Requisito + 2º Requisito)";
            ////    document.getElementById("btn_Toggle_NOTA_OAE_PARAMETRO_FUNCIONAL").innerText = "5 - NOTA DA OBRA DE ARTE PARA O PARÂMETRO FUNCIONAL (NBR 9452:2019) - Conforto e Segurança do Usuário";
            ////    document.getElementById("btn_Toggle_POLITICA_ACOES_A_IMPLEMENTAR").innerText = "6 - POLÍTICA DE AÇÕES A SEREM IMPLEMENTADAS EM FUNÇÃO DAS NOTAS";
            ////}
            ////else
            ////    // if ((selectedId_tos_id == 1) && (qualFicha == 2))
            ////    if ((qualFicha == 2)) {
            ////        document.getElementById("btn_Toggle_INSPECAO_ROTINEIRA").innerText = "2 - INSPEÇÃO ROTINEIRA";
            ////        document.getElementById("btn_Toggle_CRITERIO_DE_CLASSIFICACAO").innerText = "3 - NOTA GERAL DE AVALIAÇÃO E CLASSIFICAÇÃO DA OBRA DE ARTE (1º Requisito + 2º Requisito)";
            ////        document.getElementById("btn_Toggle_NOTA_OAE_PARAMETRO_FUNCIONAL").innerText = "4 - NOTA DA OBRA DE ARTE PARA O PARÂMETRO FUNCIONAL (NBR 9452:2019) - Conforto e Segurança do Usuário";
            ////        document.getElementById("btn_Toggle_POLITICA_ACOES_A_IMPLEMENTAR").innerText = "5 - POLÍTICA DE AÇÕES A SEREM IMPLEMENTADAS EM FUNÇÃO DAS NOTAS";
            ////    }
        }
    }

}

function Ficha2_setaReadWrite(tabela, ehRead) {
    // habilita ou desabilita todos os controles editaveis
    var lstTxtBoxes = tabela.getElementsByTagName('input');
    var lstCombos = tabela.getElementsByTagName('select');
    var lstTextareas = tabela.getElementsByTagName('textarea');

    var cmb_atr_id_98 = document.getElementById("cmb_atr_id_98");

    // trava o
    if ((moduloCorrente == 'Objetos') // se estiver em Cadastro de Objetos,
        || ((moduloCorrente == 'OrdemServico') && (cmb_atr_id_98.selectedIndex > 0)) // se estiver em OrdemServico e já houver tipo de OAE selecionada
    ) {
        controlesReadOnlyFicha2.push("cmb_atr_id_98"); // combo Tipo OAE
        controlesReadOnlyFicha2.push("txt_atr_id_105"); // descricao Tipo OAE
    }

    for (var i = 0; i < lstTxtBoxes.length; i++)
        if (!controlesReadOnlyFicha2.includes(lstTxtBoxes[i].id))
            lstTxtBoxes[i].disabled = ehRead;

    for (var i = 0; i < lstTextareas.length; i++)
        if (!controlesReadOnlyFicha2.includes(lstTextareas[i].id))
            lstTextareas[i].disabled = ehRead;

    for (var i = 0; i < lstCombos.length; i++)
        if (!controlesReadOnlyFicha2.includes(lstCombos[i].id))
            lstCombos[i].disabled = ehRead;
        else
            lstCombos[i].disabled = true;

    // botoes da lixeira da tabela GRUPOS criada dinamicamente
    if (tabela.id == "tblFicha2_INSPECAO_ROTINEIRA") // tabela de grupos da ficha 2
    {
        var tblFicha_2_GRUPOS = document.getElementById("tblFicha_2_GRUPOS");
        var lstButtons = tblFicha_2_GRUPOS.getElementsByTagName('button');

        for (var i = 0; i < lstButtons.length; i++)
            lstButtons[i].style.display = ehRead ? 'none' : 'block'; // aqui display block/none; na criacao da tabela visibility: visible/hidden (para nao misturar porque la existe validacao)

    }


    // =============== ALTERNA OS BOTOES SALVAR/CANCELAR/EDITAR =============================================================
    // alterna para os botoes de salvar/cancelar
    switch (tabela.id) {
        case "tblFicha2_DADOS_GERAIS2":
            {
                document.getElementById("btn_Salvar_DADOS_GERAIS2").style.display = 'none';
                document.getElementById("btn_Cancelar_DADOS_GERAIS2").style.display = 'none';
                document.getElementById("btn_Editar_DADOS_GERAIS2").style.display = 'block';
                break;
            }

        case "tblFicha2_HISTORICO_INSPECOES":
            {
                var btn_Toggle_HISTORICO_INSPECOES = $("#btn_Toggle_HISTORICO_INSPECOES");
                if (btn_Toggle_HISTORICO_INSPECOES.attr('aria-expanded') == "true")

                    document.getElementById("btn_Salvar_HISTORICO_INSPECOES").style.display = 'none';
                document.getElementById("btn_Cancelar_HISTORICO_INSPECOES").style.display = 'none';
                document.getElementById("btn_Editar_HISTORICO_INSPECOES").style.display = 'block';
                break;
            }

        case "tblFicha2_INSPECAO_ROTINEIRA":
            {
                var btn_Toggle_INSPECAO_ROTINEIRA = $("#btn_Toggle_INSPECAO_ROTINEIRA");
                if (btn_Toggle_INSPECAO_ROTINEIRA.attr('aria-expanded') == "true") {
                    document.getElementById("btn_Salvar_INSPECAO_ROTINEIRA").style.display = 'none';
                    document.getElementById("btn_Cancelar_INSPECAO_ROTINEIRA").style.display = 'none';
                    document.getElementById("btn_Editar_INSPECAO_ROTINEIRA").style.display = 'block';
                }
                break;
            }
        case "tblFicha2_CRITERIO_DE_CLASSIFICACAO":
            {
                document.getElementById("btn_Salvar_CRITERIO_DE_CLASSIFICACAO").style.display = 'none';
                document.getElementById("btn_Cancelar_CRITERIO_DE_CLASSIFICACAO").style.display = 'none';
                document.getElementById("btn_Editar_CRITERIO_DE_CLASSIFICACAO").style.display = 'block';
                break;
            }

        case "tblFicha2_POLITICA_ACOES_A_IMPLEMENTAR":
            {
                //document.getElementById("btn_Salvar_POLITICA_ACOES_A_IMPLEMENTAR").style.display = 'none';
                //document.getElementById("btn_Cancelar_POLITICA_ACOES_A_IMPLEMENTAR").style.display = 'none';
                //document.getElementById("btn_Editar_POLITICA_ACOES_A_IMPLEMENTAR").style.display = 'block';
                break;
            }

        case "tblFicha2_NOTA_OAE_PARAMETRO_FUNCIONAL":
            {
                document.getElementById("btn_Salvar_NOTA_OAE_PARAMETRO_FUNCIONAL").style.display = 'none';
                document.getElementById("btn_Cancelar_NOTA_OAE_PARAMETRO_FUNCIONAL").style.display = 'none';
                document.getElementById("btn_Editar_NOTA_OAE_PARAMETRO_FUNCIONAL").style.display = 'block';
                break;
            }
    }
}

function preenchecmbTiposObjeto_FichaInspecaoRotineira() {
    $("#cmbTiposObjeto_FichaInspecaoRotineira").html(""); // limpa os itens existentes

    $.ajax({
        url: '/Objeto/PreenchecmbTiposObjeto',
        type: "POST",
        dataType: "JSON",
        data: { clo_id: 0, tip_pai: 0, excluir_existentes: 0, obj_id: 0 },
        success: function (lstSubNiveis) {
            $("#cmbTiposObjeto_FichaInspecaoRotineira").append($('<option selected disabled></option>').val(-1).html("--Selecione--")); // 1o item vazio
            $.each(lstSubNiveis, function (i, subNivel) {
                $("#cmbTiposObjeto_FichaInspecaoRotineira").append($('<option></option>').val(subNivel.Value).html(subNivel.Text));
            });

        }
    });
}

function getTipoId(aux) {
    return parseInt(aux.substring(0, aux.indexOf(":")));
}

function getTipoCodigo(aux) {
    return mascara = aux.substring(aux.indexOf(":") + 1, 150);
}

function cmb_situacao_onchange(quem) {
    var valor = quem.value;
    var lbl_id = quem.id.replace("cmb_situacao", "lbl_servico");
    var lbl = document.getElementById(lbl_id);
    lbl.innerText = "";

    var cmb2_id = quem.id.replace("cmb_situacao", "cmb_tpu_descricao_itens");
    var cmb2 = document.getElementById(cmb2_id);
    if (cmb2) {
        if ((parseInt(valor) <= 3) && (cmb2.options.length >= parseInt(valor))) {
            cmb2.value = valor;
            lbl.innerText = cmb2.options[cmb2.selectedIndex].text;
        }
    }
}

function Ficha2_CriarTabelaGrupos(ehRead) {

    if (ehRead == null)
        ehRead = true;

    // limpa as linhas se houver
    var table = document.getElementById("tblFicha_2_GRUPOS");
    var rowCount = table.rows.length;
    for (var i = rowCount - 1; i >= 0; i--) {
        if (table.rows[i].id.indexOf("trFICHA2_") >= 0)
            table.deleteRow(i);
    }


    var linhas = '';
    var celulaPai = document.getElementById("Ficha2_tr_Cabecalho");

    var ord_id = 0;
    if (paginaPai == "OrdemServico")
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
                                var op0 = ' <option selectedXX value="0" disabled></option> ';
                                var op = ' <option selectedXX value="valor">texto</option> ';
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
                                    var opt = op;
                                    opt = opt.replace("valor", aux[0]).replace("texto", aux[1]);

                                    // checa se é o item selecionado
                                    if (parseInt(selectedValue) == parseInt(aux[0]))
                                        opt = opt.replace("selectedXX", "selected");
                                    else
                                        opt = opt.replace("selectedXX", "");

                                    total = total + opt;
                                }
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
                                linhaAux = linhaAux.replace(/VVV/g, result.data[i].ogv_id);
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

            ////// coloca mascara no campo observacao
            ////var qts = $('[id^="txt_obs_"]');

            ////var optionsObs = {
            ////    'translation': {
            ////        ';': { pattern: /;/, fallback: '.', optional: false }, // ";"para ponto por causa do "split"
            ////        '&': { pattern: /&/, fallback: 'e', optional: false } // "&"para  letra "e" por causa do "split"
            ////    }

            ////};

            ////for (var i = 0; i < qts.length; i++) {
            ////    jQuery(qts[i]).mask("999.99", optionsObs);
            ////}

            if (tblFicha2_INSPECAO_ROTINEIRA)
                Ficha2_setaReadWrite(tblFicha2_INSPECAO_ROTINEIRA, ehRead);
        }
    });

}

function Ficha2_ExcluirGrupo(obj_id) {
    var form = this;

    swal({
        title: "Excluir. Tem certeza?",
        icon: "warning",
        buttons: [
            'Não',
            'Sim'
        ],
        dangerMode: true,
        focusCancel: true
    }).then(function (isConfirm) {
        if (isConfirm) {
            var response = POST("/Objeto/Objeto_Excluir", JSON.stringify({ id: obj_id }))
            if (response.erroId >= 1) {
                swal({
                    type: 'success',
                    title: 'Sucesso',
                    text: 'Registro excluído com sucesso'
                });

                // atualiza tabela grupos
                Ficha2_CriarTabelaGrupos();
                return false;
            }
            else {
                swal({
                    type: 'error',
                    title: 'Aviso',
                    text: 'Erro ao excluir registro'
                });
            }
            return false;
        } else {
            return false;
        }
    })
    return false;
}

function Selecionar_TPU(id) {
    alert("Selecionar TPU - a definir");
}

function EditarDados_Ficha2(tabela) {

    // alterna os campos para escrita
    Ficha2_setaReadWrite(tabela, false);

    // alterna para os botoes de salvar/cancelar
    switch (tabela.id) {
        case "tblFicha2_DADOS_GERAIS2":
            {
                document.getElementById("btn_Salvar_DADOS_GERAIS2").style.display = 'block';
                document.getElementById("btn_Cancelar_DADOS_GERAIS2").style.display = 'block';
                document.getElementById("btn_Editar_DADOS_GERAIS2").style.display = 'none';
                break;
            }

        case "tblFicha2_HISTORICO_INSPECOES":
            {
                document.getElementById("btn_Salvar_HISTORICO_INSPECOES").style.display = 'block';
                document.getElementById("btn_Cancelar_HISTORICO_INSPECOES").style.display = 'block';
                document.getElementById("btn_Editar_HISTORICO_INSPECOES").style.display = 'none';
                break;
            }

        case "tblFicha2_INSPECAO_ROTINEIRA":
            {
                document.getElementById("btn_Salvar_INSPECAO_ROTINEIRA").style.display = 'block';
                document.getElementById("btn_Cancelar_INSPECAO_ROTINEIRA").style.display = 'block';
                document.getElementById("btn_Editar_INSPECAO_ROTINEIRA").style.display = 'none';
                break;
            }

        case "tblFicha2_CRITERIO_DE_CLASSIFICACAO":
            {
                document.getElementById("btn_Salvar_CRITERIO_DE_CLASSIFICACAO").style.display = 'block';
                document.getElementById("btn_Cancelar_CRITERIO_DE_CLASSIFICACAO").style.display = 'block';
                document.getElementById("btn_Editar_CRITERIO_DE_CLASSIFICACAO").style.display = 'none';
                break;
            }

        case "tblFicha2_NOTA_OAE_PARAMETRO_FUNCIONAL":
            {
                document.getElementById("btn_Salvar_NOTA_OAE_PARAMETRO_FUNCIONAL").style.display = 'block';
                document.getElementById("btn_Cancelar_NOTA_OAE_PARAMETRO_FUNCIONAL").style.display = 'block';
                document.getElementById("btn_Editar_NOTA_OAE_PARAMETRO_FUNCIONAL").style.display = 'none';
                break;
            }

        case "tblFicha2_POLITICA_ACOES_A_IMPLEMENTAR":
            {
                document.getElementById("btn_Salvar_POLITICA_ACOES_A_IMPLEMENTAR").style.display = 'block';
                document.getElementById("btn_Cancelar_POLITICA_ACOES_A_IMPLEMENTAR").style.display = 'block';
                document.getElementById("btn_Editar_POLITICA_ACOES_A_IMPLEMENTAR").style.display = 'none';
                break;
            }
    }

    return false;
}

function SalvarDados_Ficha2_GRUPOS_VALORES() {
    // pega os valores de Ponto1, Ponto2, Ponto3
    var P1 = document.getElementById("txt_atr_id_132").value;
    var P2 = document.getElementById("txt_atr_id_133").value;
    var P3 = document.getElementById("txt_atr_id_134").value;
    var OutrasInformacoes = document.getElementById("txt_atr_id_170").value;

    // monta lista de valores das linhas
    var saida = '';
    var ati_id_condicao_inspecao = 0;

    var table = document.getElementById("tblFicha_2_GRUPOS");
    var grupoAtual = 0; var nomeGrupoAtual = ""; var nomeGrupoAnterior = "";
    var grupoAnterior = 0; var grupoNOK = 0;
    var linhaGrupo = "";
    var linhaOK = true;

    for (var i = 0; i < table.rows.length; i++) {
        if ((table.rows[i].id.includes("trFICHA2_")) && (!table.rows[i].id.includes("trFICHA2_OOBBJJIIDD_GGG_VVV"))) {
            var GGG_VVV = table.rows[i].id.replace("trFICHA2_", "");
            var aux = GGG_VVV.split("_");
            var obj_id = aux[1];
            var ogv_id = aux[2];

            grupoAtual = aux[1];

            if (parseInt(aux[aux.length - 1]) == 0)
                grupoAnterior = aux[1];

            GGG_VVV = obj_id + "_" + ogv_id;
            var ogi_id_caracterizacao_situacao = document.getElementById("cmb_situacao_" + GGG_VVV).options[document.getElementById("cmb_situacao_" + GGG_VVV).selectedIndex].value;

            if (table.rows[i].cells.length > 6) {
                ati_id_condicao_inspecao = document.getElementById("cmb_condicao_" + GGG_VVV).options[document.getElementById("cmb_condicao_" + GGG_VVV).selectedIndex].value;

                // acha o nomeGrupoAtual
                var elemento = table.rows[i].cells[0].getElementsByTagName("label");
                if (elemento.length > 0) {
                    nomeGrupoAtual = elemento[0].innerText; // alert(nomeGrupoAtual);
                }
            }

            var ovv_observacoes_gerais = document.getElementById("txt_obs_" + GGG_VVV).value;
            var tpu_id = " ";

            var ovv_tpu_quantidade = document.getElementById("txt_quantidade_" + GGG_VVV).value;
            var tpu_unidade = document.getElementById("txt_unidade_" + GGG_VVV).value;

            if (ovv_observacoes_gerais.trim() == "")
                ovv_observacoes_gerais = " ";

            if (tpu_unidade.trim() == "")
                tpu_unidade = " ";

            if (isNaN(ovv_tpu_quantidade))
                ovv_tpu_quantidade = 0;

            var linhaMontada = '<tr_linha>' + obj_id + '<quebra>' + ogv_id + '<quebra>' + ogi_id_caracterizacao_situacao + '<quebra>' + ati_id_condicao_inspecao + '<quebra>' + ovv_observacoes_gerais + '<quebra>' + tpu_id + '<quebra>' + tpu_unidade + '<quebra>' + ovv_tpu_quantidade + '</tr_linha>';

            if (parseInt(ati_id_condicao_inspecao) == 0) {
                linhaOK = false;
                grupoNOK = grupoAtual;
            }

            if (grupoAnterior == grupoAtual) {
                linhaGrupo = linhaGrupo + linhaMontada;
            }
            else {
                saida = saida + linhaGrupo;
                if ((!linhaOK) && (grupoNOK == grupoAnterior)) {
                    swal({
                        type: 'error',
                        title: 'Aviso',
                        text: 'O Grupo [' + nomeGrupoAnterior + '] possui dados incompletos ou inválidos'
                    }).then(
                        function () {
                            //  alert(linhaGrupo);
                            nomeGrupoAnterior = nomeGrupoAtual;
                            nomeGrupoAtual = "";
                            linhaGrupo = linhaMontada;
                            linhaOK = true; grupoNOK = 0;
                            return false;
                        });

                    return false;
                    //alert("Grupo incompleto [" + nomeGrupoAnterior + "] i=" + i);
                    //alert(linhaGrupo);
                }

                nomeGrupoAnterior = nomeGrupoAtual;
                nomeGrupoAtual = "";
                linhaGrupo = linhaMontada;
                linhaOK = true; grupoNOK = 0;
            }
            grupoAnterior = grupoAtual;
        }
    }
    saida = saida + linhaGrupo;
    //alert(saida);

    // ********* ENVIA OS DADOS PARA O BANCO **********************************************************************
    //url: "/Objeto/GruposVariaveisValores_Salvar",

    var ord_id = 0;
    if (paginaPai == "OrdemServico")
        ord_id = selectedId_ord_id;

    $.ajax({
        url: "/Objeto/GruposVariaveisValores_Salvar",
        data: JSON.stringify({
            'obj_id_tipoOAE': selectedId_obj_id, 'Ponto1': P1, 'Ponto2': P2, 'Ponto3': P3, 'OutrasInformacoes': OutrasInformacoes, 'listaConcatenada': saida, 'ord_id': ord_id
        }),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {

            Ficha2_CriarTabelaGrupos(true);

            document.getElementById("btn_Salvar_INSPECAO_ROTINEIRA").style.display = 'none';
            document.getElementById("btn_Cancelar_INSPECAO_ROTINEIRA").style.display = 'none';
            document.getElementById("btn_Editar_INSPECAO_ROTINEIRA").style.display = 'block';


            return false;
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
            return false;
        }
    });


    return false;
}
function SalvarDados_Ficha2(tabela) {
    var saida = '';
    var array_filtrado = controlesReadOnlyFicha2.filter((el) => !controlesExcecoes_Salvar.includes(el));

    var lstInputsList = tabela.getElementsByTagName('input'); // lista de textbox

    // convert nodelist to array
    var lstInputs = [];
    for (var i = 0; i < lstInputsList.length; i++)
        lstInputs.push(lstInputsList[i].id);

    lstInputs.push("txt_historico_Pontuacao_Geral_OAE_1");
    lstInputs.push("txt_atr_id_157");

    for (var i = 0; i < lstInputs.length; i++) {
        if ((!array_filtrado.includes(lstInputs[i]))
            || (lstInputs[i] == "txt_historico_Pontuacao_Geral_OAE_1")) {
            var txtId = lstInputs[i];
            // tratamento para os itens id = 1000 + id;
            var atributo_id = parseInt(txtId.substring(txtId.lastIndexOf("_") + 1, 100));
            if (atributo_id > 1000)
                txtId = txtId.substring(0, txtId.lastIndexOf("_")) + "_" + (atributo_id - 1000);
            var txt = document.getElementById(txtId);
            if (txt)
                saida = saida + ";" + txtId + ":" + txt.value;
        }
    }


    var lstCombos = tabela.getElementsByTagName('select'); // lista de combos
    for (var i = 0; i < lstCombos.length; i++) {
        if (!array_filtrado.includes(lstCombos[i].id))
            if (lstCombos[i].selectedIndex > -1) {
                var cmbId = lstCombos[i].id;

                if ((cmbId == "")) {

                }
                else {
                    // tratamento para os itens cujo id = 1000 + id;
                    var atributo_id = parseInt(cmbId.substring(cmbId.lastIndexOf("_") + 1, 100));
                    if (atributo_id > 1000)
                        cmbId = cmbId.substring(0, cmbId.lastIndexOf("_")) + "_" + (atributo_id - 1000);
                }
                saida = saida + ";" + cmbId + ":" + lstCombos[i].options[lstCombos[i].selectedIndex].value;
            }
    }


    saida = saida.substr(1) + ";"; //  retira ponto e virgula do comeco acrescenta no final

    //alert(saida);
    // ******************** manda os dados para o banco *************************
    var param = {
        obj_id: selectedId_obj_id,
        atr_id: -2,
        ati_id: -2,
        atv_valores: saida,
        nome_aba: tabela.id
    };

    var ord_id = -1;
    if (moduloCorrente == 'OrdemServico')
        ord_id = selectedId_ord_id;

    var codigoOAE = txt_atr_id_106.value + "-" + txt_atr_id_13.value;

    var selidTipoOAE = cmb_atr_id_98.options[cmb_atr_id_98.selectedIndex].value;

    param = JSON.stringify({ 'ObjAtributoValor': param, 'codigoOAE': codigoOAE, 'selidTipoOAE': selidTipoOAE, ord_id: ord_id });
    //   url: "/Objeto/ObjAtributoValores_Salvar",

    $.ajax({
        url: "/Inspecao/InspecaoAtributoValores_Salvar",
        data: param,
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {

            preenchetblFicha2(selectedId_obj_id, selectedId_clo_id, selectedId_tip_id);

            // alterna os campos para leitura
            Ficha2_setaReadWrite(tabela, true);

            return false;
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
            return false;
        }
    });



}
function SalvarDados_Ficha2_CRITERIO_DE_CLASSIFICACAO() {
    var tabela = document.getElementById("tblFicha2_CRITERIO_DE_CLASSIFICACAO");
    SalvarDados_Ficha2(tabela);

    tabela = document.getElementById("tblFicha2_POLITICA_ACOES_A_IMPLEMENTAR");
    SalvarDados_Ficha2(tabela);
}
function SalvarDados_Ficha2_NOTA_OAE_PARAMETRO_FUNCIONAL() {
    var tabela = document.getElementById("tblFicha2_NOTA_OAE_PARAMETRO_FUNCIONAL");
    SalvarDados_Ficha2(tabela);

    tabela = document.getElementById("tblFicha2_POLITICA_ACOES_A_IMPLEMENTAR");
    SalvarDados_Ficha2(tabela);

    //var cmb = document.getElementById("cmb_atr_id_145");
    //var valor = cmb.options[cmb.selectedIndex].value;
    //var param = {
    //    obj_id: selectedId_obj_id,
    //    atr_id: 145,
    //    ati_id: valor,
    //    atv_valores: valor
    //};

    //param = JSON.stringify({ 'ObjAtributoValor': param });

    //$.ajax({
    //    url: "/Objeto/ObjAtributoValor_Salvar",
    //    data: param,
    //    type: "POST",
    //    contentType: "application/json;charset=utf-8",
    //    dataType: "json",
    //    success: function (result) {

    //        preenchetblFicha2(selectedId_obj_id, selectedId_clo_id, selectedId_tip_id);

    //        // alterna os campos para leitura
    //        Ficha2_setaReadWrite(tblFicha2_NOTA_OAE_PARAMETRO_FUNCIONAL, true);

    //        return false;
    //    },
    //    error: function (errormessage) {
    //        alert(errormessage.responseText);
    //        return false;
    //    }
    //});


}

function CancelarDados_Ficha2(tabela) {
    Ficha2_setaReadWrite(tabela, true);

    preenchetblFicha2(selectedId_obj_id, selectedId_clo_id, selectedId_tip_id);
}

function Ficha2_ExcluirSubDivisao2(tip_id) {
    var form = this;

    swal({
        title: "Excluir. Tem certeza?",
        icon: "warning",
        buttons: [
            'Não',
            'Sim'
        ],
        dangerMode: true,
        focusCancel: true
    }).then(function (isConfirm) {
        if (isConfirm) {
            var response = POST("/Objeto/Objeto_Subdivisao2_Excluir", JSON.stringify({ tip_id: tip_id, obj_id_tipoOAE: selectedId_obj_id }))
            if (response.erroId.trim() == "") {
                swal({
                    type: 'success',
                    title: 'Sucesso',
                    text: 'Registro excluído com sucesso'
                });

                // atualiza tabela grupos
                Ficha2_CriarTabelaGrupos();
                return false;
            }
            else {
                swal({
                    type: 'error',
                    title: 'Aviso',
                    text: 'Erro ao excluir registro:' + response.erroId
                });
            }
            return false;
        } else {
            return false;
        }
    })
    return false;
}

// popup GRUPOS
function Ficha2_preencheComboModal(clo_id, qualCombo, txtPlaceholder, tip_pai) {
   
    //document.getElementById("divFicha2_GrupoObjetos").style.display = 'block';

    if (tip_pai == null)
        tip_pai = -1;

    //   var excluir_existentes = qualCombo == 'divFicha2_GrupoObjetos' ? 1 : 0;
    var excluir_existentes = 0;

    var cmb = $("#" + qualCombo);
    //cmb.html(""); // limpa os itens existentes
    //cmb.append($('<option selected ></option>').val(-1).html(txtPlaceholder)); // 1o item vazio
    
    $.ajax({
        url: '/Objeto/PreenchecmbTiposObjeto',
        type: "POST",
        dataType: "JSON",
        data: { clo_id: clo_id, tip_pai: tip_pai, excluir_existentes: excluir_existentes, obj_id: selectedId_obj_id, somente_com_variaveis_inspecao: 1 },
        success: function (lstSubNiveis) {
           
            if (clo_id != 9) {
                cmb.empty();
                cmb.append($('<option selected disabled></option>').val(-1).html("--Selecione--")); // 1o item vazio
                $.each(lstSubNiveis, function (i, subNivel) {
                    cmb.append($('<option></option>').val(subNivel.Value.trim()).html(subNivel.Text.trim()));
                });
            }
            else {
                
                var i = 0;
                $("#cmbFicha2_GrupoObjetos").empty();
                $("#cmbFicha2_GrupoObjetos").append($('<option selected disabled></option>').val(-1).html("--Selecione--")); // 1o item vazio
                $.each(lstSubNiveis, function (i, objeto) {
                    i++;
                    $("#cmbFicha2_GrupoObjetos").append("<option value='" + objeto.Value + "'>" + objeto.Text + "</option>");
                });
            }
        }
    });
}

function cmbFicha2_GrupoObjetosModal_onchange() {
    //$("#cmbVrInspec11").html(""); // limpa os itens existentes 
   
    Ficha2_LimparCampos();
    $.ajax({
        url: '/Politicaconserva/Variaveis?Id=' + $('#cmbFicha2_GrupoObjetos option:selected').val(),
        type: "Get",
        dataType: "JSON",
        success: function (data) {
            $("#cmbVrInspec11").empty();
            $("#cmbVrInspec11").append($('<option selected disabled></option>').val(-1).html("--Selecione--")); // 1o item vazio
            $.each(data, function (i, item) {
                $("#cmbVrInspec11").append($('<option></option>').val(item.ogv_id).html(item.variavel));
            });

        }
    });
}

function preencheConserva_onchange() {
    //$("#cmbVrInspec11").html(""); // limpa os itens existentes 
    //alert($('#cmbFicha2_GrupoObjetos option:selected').val());
    Ficha2_LimparCampos(6);
    $.ajax({
        url: '/Politicaconserva/Conserva?Id=' + $('#cmbVrInspec11 option:selected').val(),
        type: "Get",
        dataType: "JSON",
        success: function (data) {
            $("#txtNormal").val('');
            $("#txtAtencao").val('');
            $("#txtCritica").val('');
            $.each(data, function (i, item) {                
                if (item.ogi_id_caracterizacao_situacao == 2) {
                    $("#txtNormal").val(item.ocp_descricao_alerta);
                    $("#ocp_id2").val(item.ocp_id);
                    $("#descri2").val(item.ocp_descricao_servico);
                    $("#txtNormal").prop('title', item.ocp_descricao_alerta);
                }
                else if (item.ogi_id_caracterizacao_situacao == 1) {
                    $("#txtAtencao").val(item.ocp_descricao_alerta);
                    $("#descri1").val(item.ocp_descricao_servico);
                    $("#ocp_id1").val(item.ocp_id);
                    $("#txtAtencao").prop('title', item.ocp_descricao_alerta);
                   
                }
                else if (item.ogi_id_caracterizacao_situacao == 3) {
                    $("#ocp_id3").val(item.ocp_id);
                    $("#descri3").val(item.ocp_descricao_servico);
                    $("#txtCritica").val(item.ocp_descricao_alerta);
                    $("#txtCritica").prop('title', item.ocp_descricao_alerta);
                    
                }

            });

        }
    });
}

function btn_SelecionarGrupo_INSPECAO_ROTINEIRA_onclick() {
    var Ficha2_cmbSubdivisao1 = document.getElementById("Ficha2_cmbSubdivisao1");
    Ficha2_cmbSubdivisao1.selectedIndex = -1;

    Ficha2_LimparCampos(6);
    $("#modalSelecionarGrupo").modal('show');

}


function Ficha2_cmbSubdivisao1_onchange() {

    Ficha2_LimparCampos();

    // preenche proximo combo
    var valor = document.getElementById("Ficha2_cmbSubdivisao1").value;
    var ivalor = parseInt(valor);

    //var cmbTiposObjeto_FichaInspecaoRotineira = document.getElementById("cmbTiposObjeto_FichaInspecaoRotineira");
    //$("#Ficha2_cmbSubdivisao2").html(""); // limpa tudo
    //$("#Ficha2_cmbSubdivisao2").append($('<option selected ></option>').val(-1).html('-- Selecione --'));

    // oculta o divs Ficha2_Subdivisao2
    document.getElementById("Ficha2_divSubdivisao2").style.display = 'none';
    document.getElementById("Ficha2_divSubdivisao3").style.display = 'none';
    document.getElementById("divFicha2_GrupoObjetos").style.display = 'block';

    // superestrutura
    if (ivalor == 11) {
        // mostra Subdivisao2
        document.getElementById("Ficha2_divSubdivisao2").style.display = 'block';
        Ficha2_preencheComboModal(7, 'Ficha2_cmbSubdivisao2', '--Selecione--', ivalor);

        //// preenche combo manualmente, tip_id = 15 (tabuleiro face superior) e 16 (tabuleiro face inferior)
        //for (var i = 0; i < cmbTiposObjeto_FichaInspecaoRotineira.options.length; i++) {
        //    if ((getTipoId(cmbTiposObjeto_FichaInspecaoRotineira.options[i].value) == 15)
        //        || (getTipoId(cmbTiposObjeto_FichaInspecaoRotineira.options[i].value) == 16)) {
        //        var option = document.createElement("option");
        //        option.text = cmbTiposObjeto_FichaInspecaoRotineira.options[i].text;
        //        option.value = cmbTiposObjeto_FichaInspecaoRotineira.options[i].value;
        //        $("#Ficha2_cmbSubdivisao2").append($('<option></option>').val(option.value).html(option.text));
        //    }
        //}
    }
    else
        // ENCONTROS
        if (ivalor == 14) {
            // oculta tudo e deixa somente o botao salvar
            document.getElementById("divFicha2_GrupoObjetos").style.display = 'none';

            //// mostra Subdivisao2 e 3
            //document.getElementById("Ficha2_divSubdivisao2").style.display = 'block';

            //// preenche combo manualmente, tip_id = 22,23,24 ESTRUTURAS DE TERRRA, DE CONCRETO E ACESSOS
            //$("#Ficha2_cmbSubdivisao2").append($('<option></option>').val("").html("--Selecione--"));
            //for (var i = 0; i < cmbTiposObjeto_FichaInspecaoRotineira.options.length; i++) {
            //    if ((getTipoId(cmbTiposObjeto_FichaInspecaoRotineira.options[i].value) == 22)
            //        || (getTipoId(cmbTiposObjeto_FichaInspecaoRotineira.options[i].value) == 23)
            //        || (getTipoId(cmbTiposObjeto_FichaInspecaoRotineira.options[i].value) == 24)
            //    ) {
            //        var option = document.createElement("option");
            //        option.text = cmbTiposObjeto_FichaInspecaoRotineira.options[i].text;
            //        option.value = cmbTiposObjeto_FichaInspecaoRotineira.options[i].value;
            //        $("#Ficha2_cmbSubdivisao2").append($('<option></option>').val(option.value).html(option.text));
            //    }
            //}
        }
        else
            Ficha2_preencheComboModal(9, 'divFicha2_GrupoObjetos', '--Selecione--', ivalor);

}
function Ficha2_cmbSubdivisao2_onchange() {

    // oculta o divs Subdivisao3
    document.getElementById("Ficha2_divSubdivisao3").style.display = 'none';

    // preenche proximo combo
    var valor = document.getElementById("Ficha2_cmbSubdivisao2").value;
    var ivalor = getTipoId(valor);

    if ((ivalor == 24) || (ivalor == 15) || (ivalor == 16)) { // 15 = Tabuleiro Face Superior; 16=Tabuleiro Face Inferior; 24 = Acesso
        Ficha2_LimparCampos();
      
        Ficha2_preencheComboModal(9, 'cmbFicha2_GrupoObjetos', '--Selecione--', ivalor)
    }
    else {
        Ficha2_LimparCampos();
        document.getElementById("Ficha2_divSubdivisao3").style.display = 'block';
        Ficha2_preencheComboModal(8, 'Ficha2_cmbSubdivisao3', '--Selecione--', ivalor)
    }


}
function Ficha2_cmbSubdivisao3_onchange() {

    // preenche proximo combo
    var valor = document.getElementById("Ficha2_cmbSubdivisao3").value;
    var ivalor = getTipoId(valor);

    Ficha2_LimparCampos();
    Ficha2_preencheComboModal(9, 'divFicha2_GrupoObjetos', '--Selecione--', ivalor)
}

function Ficha2_LimparCampos() {
    $("#txtNormal").val("");
    $("#txtAtencao").val("");
    $("#txtCritica").val("");
   
}

function updateConserva_Click() {
    var ocp_id1 = $('#ocp_id1').val();
    var ocp_id2 = $('#ocp_id2').val();
    var ocp_id3 = $('#ocp_id3').val();

    var descri1 = $('#descri1').val();
    var descri2 = $('#descri2').val();
    var descri3 = $('#descri3').val();


    Ficha2_LimparCampos();
    $.ajax({
        url: '/Politicaconserva/UpdateConserva?ocp_id1=' + ocp_id1 + '&ocp_id2=' + ocp_id2 + '&ocp_id3=' + ocp_id3 + '&descri1=' + descri1 + '&descri2=' + descri2 + '&descri3=' + descri3,
        type: "Get",
        dataType: "JSON",
        success: function (data) {
            alert('Dados atualizados com sucesso');
        },
        error: function () {
            alert('Não foi possivel atualizar');
        }
    });
}





