var linhaCabecalhosProvidencia = ' <tr id="trFicha4_PROVIDENCIAS_ian_id_ZZZ" > ' +
        ' <td class="qualClasse" id="tdFICHA4_CodObjeto_ian_id_ZZZ" style="display:EhOrdemServico" > <label class="lblsBold" id="lbl_ObjCodigo_ian_id_ZZZ">lbl_ObjCodigo_VVV</label></td > ' +
        ' <td class="borderLeft centroH qualClasse" title="lbl_Localizacao_tooltip" ><label class="lblsBold" id="lbl_Localizacao_ian_id_ZZZ"></label>lbl_Localizacao_VVV</td> ' +
        ' <td class="borderLeft " ></td> ' +
        ' <td class="borderLeft " ></td> ' +
        ' <td class="borderLeft " ></td> ' +
        ' <td class="borderLeft " ></td> ' +
        ' <td class="borderLeft " ></td> ' +
        ' <td class="borderLeft " ></td> ' +
        ' <td class="borderLeft " ></td> ' +
        ' <td class="borderLeft " ></td> ' +
        ' <td class="borderLeft " ></td> ' +
        ' <td class="borderLeft " ></td> ' +
        ' <td class="borderLeft " ></td> ' +
        ' <td class="borderLeft " ></td> ' +
        ' <td class="borderLeft " ></td> ' +
   //     ' <td class="borderLeft " ></td> ' +
        ' <td class="borderLeft " ></td> ' +
        ' <td class="borderLeft  borderRight" ></td> ' +
    ' </tr>';

    var linhaCabecalhoProvidencia = ' <tr id="trFicha4_PROVIDENCIAS_ian_id_ZZZ" > ' +
        ' <td class="borderLeft borderTop borderBottom borderRight cabecalhos_providencias" colspan="17" >' +
            '<label class="lblsBold" style="vertical-align: middle;" id="lbl_apt_id_ZZZ">lbl_apt_descricao_VVV</label > ' +
        '<button title="Imprimir" onclick="Ficha4_PROVIDENCIAS_Imprimir(apr_id_VVV)" ><i class="fa fa-print" ></i></button>  ' +
    '</td > </tr>';

    var linhaObjetosProvidencia = ' <tr id="trFicha4_PROVIDENCIAS_ian_id_ZZZ" > ' +
        ' <td class=" qualClasse" id="tdFICHA4_CodObjeto_ian_id_ZZZ" style="display:EhOrdemServico" > <label class="lblsBold" id="lbl_ObjCodigo_ian_id_ZZZ">lbl_ObjCodigo_VVV</label></td > ' +
        ' <td class="borderLeft centroH qualClasse" title="lbl_Localizacao_tooltip" ><label class="lblsBold" id="lbl_Localizacao_ian_id_ZZZ" >lbl_Localizacao_VVV</label></td> ' +
        ' <td class="borderLeft centroH qualClasse" > <input disabled id="txt_Localizacao_Especifica_ian_id_ZZZ" class="centroH txts2" style="width:94%; " value="txt_Localizacao_Especifica_VVV"  /></td> ' +
        ' <td class="borderLeft centroH qualClasse" > <input disabled id="txt_Numero_ian_id_ZZZ" class="centroH txts2" style="width:94%; " value="txt_Numero_VVV" /></td> ' +
        ' <td class="borderLeft centroH qualClasse" > <select disabled id="cmb_Sigla_ian_id_ZZZ" class="cmbs_anom" title="TOOLTIP_cmb_Sigla" >OPCOES_cmb_Sigla</select></td > ' +
        ' <td class="borderLeft centroH qualClasse" > <select disabled id="cmb_Cod_ian_id_ZZZ" class="cmbs_anom"  >OPCOES_cmb_Cod</select></td> ' +
        ' <td class="borderLeft centroH qualClasse" > <select disabled id="cmb_Alerta_ian_id_ZZZ" class="cmbs_anom" title="TOOLTIP_cmb_Alerta"  >OPCOES_cmb_Alerta</select></td > ' +
        ' <td class="borderLeft centroH qualClasse"><input disabled id="txt_Quantidade_ian_id_ZZZ" class="centroH txts2" style="width:94%; " value="txt_Quantidade_VVV" /></td> ' +
        ' <td class="borderLeft centroH qualClasse"><input disabled id="txt_EspacamentoMedio_ian_id_ZZZ" class="centroH txts2" style="width:94%; " value="txt_EspacamentoMedio_VVV" /></td> ' +
        ' <td class="borderLeft centroH qualClasse"><input disabled id="txt_Largura_ian_id_ZZZ" class="centroH txts2" style="width:94%; " value="txt_Largura_VVV" /></td> ' +
        ' <td class="borderLeft centroH qualClasse"><input disabled id="txt_Comprimento_ian_id_ZZZ" class="centroH txts2" style="width:94%; " value="txt_Comprimento_VVV" /></td> ' +
        ' <td class="borderLeft centroH qualClasse"><input disabled id="txt_AberturaMinima_ian_id_ZZZ" class="centroH txts2" style="width:94%; " value="txt_AberturaMinima_VVV" /></td> ' +
        ' <td class="borderLeft centroH qualClasse"><input disabled id="txt_AberturaMaxima_ian_id_ZZZ" class="centroH txts2" style="width:94%; " value="txt_AberturaMaxima_VVV" /></td> ' +
        ' <td class="borderLeft centroH qualClasse" > <select disabled id="cmb_Causa_ian_id_ZZZ" class="cmbs_anom" title="TOOLTIP_cmb_Causa" >OPCOES_cmb_Causa</select></td > ' +

        ' <td class="borderLeft centroH qualClasse"><input disabled id="txt_Foto_ian_id_ZZZ" class="centroH txts2" style="width:94%; " value="txt_Foto_VVV" /></td> ' +
        ' <td class="borderLeft centroH qualClasse"><input disabled id="txt_Croqui_ian_id_ZZZ" class="centroH txts2" style="width:94%; " value="txt_Croqui_VVV" /></td> ' +
        //' <td class="borderLeft centroH qualClasse"><input disabled id="txt_Desenho_ian_id_ZZZ" class="centroH txts2" style="width:94%; " value="txt_Desenho_VVV" /></td> ' +
        ' <td class="borderLeft centroH qualClasse borderRight"><input disabled id="txt_Obs_ian_id_ZZZ" class="centroH txts2" style="width:94%; " value="txt_Obs_VVV" /></td> ' +
        ' </tr>';


function Ficha4_PROVIDENCIAS_ExportarXLS() {
    $.ajax({
        "url": "/Inspecao/FichaInspecaoEspecialAnomalias_ExportarXLS",
        "type": "GET",
        "datatype": "json",
        "data": { "ord_id": selectedId_ord_id },
        "success": function (result) {
            // return result = caminho da planilha preenchida e salva para download

            window.open("../../Reports/frmDownloadFile.aspx?filename=" + result.data);
            //  window.open("../../temp/" + result.data);
            return false;
        }
    });

    return false;
}

function limpatblFicha4_PROVIDENCIAS() {

    var tabela = document.getElementById("divFicha4_PROVIDENCIAS");

    // habilita ou desabilita todos os controles editaveis
    if (tabela) {
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

}

function tblFicha4_PROVIDENCIA_prenchetdCombos(qualCombo, listadeValores, selectedValue, linhaAux) {
    var tooltip = 'TOOLTIP_' + qualCombo;
    var opcoes = 'OPCOES_' + qualCombo;

    var op0 = ' <option selectedXX value="-1" ></option> ';
    var op = ' <option selectedXX value="valor" title="tooltip">texto</option> ';
    var total = op0;

    if (parseInt(selectedValue) == -1) {
        total = total.replace("selectedXX", "selected");
        linhaAux = linhaAux.replace(tooltip, "");
    }
    else
        total = total.replace("selectedXX", "");

    var pedacos = listadeValores.split(";");
    for (k = 0; k < pedacos.length; k++) {
        var aux = pedacos[k].split(":");
        if (aux.length > 1) {
            var opt = op;
            opt = opt.replace("valor", aux[0].trim()); //.replace("texto", aux[1]);
            opt = opt.replace("texto", aux[0].trim()); //.replace("texto", aux[1]);
            opt = opt.replace("tooltip", aux[1].trim());

            // checa se é o item selecionado
            if ((selectedValue) == (aux[0].trim())) {
                opt = opt.replace("selectedXX", "selected");
                linhaAux = linhaAux.replace(tooltip, aux[1]);
            }
            //else {
            //    opt = opt.replace("selectedXX", "");
            //    linhaAux = linhaAux.replace(tooltip, aux[1]);
            //}
            total = total + opt;
        }
    }

    if (linhaAux.includes(tooltip))
        linhaAux = linhaAux.replace(tooltip, "");

    linhaAux = linhaAux.replace(opcoes, total);

    return linhaAux;
}

function preenchetblFicha4_PROVIDENCIAS() {

    $("#lblOAE").text(selected_obj_codigo);
    $('#txt_ins_anom_data').datepicker({ dateFormat: 'dd/mm/yy' });

    $.ajax({
        "url": "/Inspecao/InspecaoAnomalias_Valores_Providencias_ListAll",
        "type": "GET",
        "datatype": "json",
        "data": { "ord_id": selectedId_ord_id },
        "success": function (result) {

            $('#txt_ins_anom_data').val("");
            $('#txt_ins_anom_Responsavel').val("");

            // limpa as linhas se houver
            var table = document.getElementById("tblFicha4_INSPECAO_ESPECIAL_PROVIDENCIAS");
            var rowCount = table.rows.length;
            for (var i = rowCount - 1; i >= 0; i--) {
                if (table.rows[i].id.indexOf("trFicha4_PROVIDENCIAS_ian_id") >= 0)
                    table.deleteRow(i);
            }

            var linhas = '';
            var celulaPai = document.getElementById("tr_pai");

            for (var i = 0; i < result.data.length; i++) {
                if (i == 0) {
                    $('#txt_ins_anom_Responsavel').val(result.data[i].ins_anom_Responsavel);
                    $('#txt_ins_anom_data').val(result.data[i].ins_anom_data);
                }

                var linhaAux = linhaObjetosProvidencia;
                if (parseInt(result.data[i].clo_id) < 10)
                    linhaAux = linhaCabecalhosProvidencia;

                if (result.data[i].ehCabecalho == "1") {
                    linhaAux = linhaCabecalhoProvidencia;
                    linhaAux = linhaAux.replace(/lbl_apt_id_ZZZ/g, result.data[i].apt_id).replace(/lbl_apt_descricao_VVV/g, result.data[i].apt_descricao);
                    linhaAux = linhaAux.replace(/apr_id_VVV/g, result.data[i].apt_id);                    
                }
                else {

                    if (!isNaN(result.data[i].obj_id)) // LINHA NORMAL
                    {
                        linhaAux = linhaAux.replace(/YYY/g, result.data[i].rownum).replace(/XXX/g, result.data[i].obj_id).replace(/qualClasse/g, "linha_normal");
                    }

                    linhaAux = linhaAux.replace(/ZZZ/g, result.data[i].ian_id); // id da linha

                    linhaAux = linhaAux.replace(/lbl_ObjCodigo_VVV/g, result.data[i].obj_codigo);
                    linhaAux = linhaAux.replace(/lbl_Item_VVV/g, result.data[i].item);

                    if (!isNaN(result.data[i].obj_id))
                        if (parseInt(result.data[i].clo_id) < 10) {
                            linhaAux = linhaAux.replace(/lbl_Localizacao_VVV/g, result.data[i].tip_nome);
                            linhaAux = linhaAux.replace(/lbl_Localizacao_tooltip/g, result.data[i].obj_codigo);
                        }
                        else {
                            linhaAux = linhaAux.replace(/lbl_Localizacao_VVV/g, result.data[i].obj_descricao);
                            linhaAux = linhaAux.replace(/lbl_Localizacao_tooltip/g, result.data[i].obj_codigo);
                        }
                    else {
                        linhaAux = linhaAux.replace(/lbl_Localizacao_VVV/g, result.data[i].tip_nome);
                        linhaAux = linhaAux.replace(/lbl_Localizacao_tooltip/g, result.data[i].obj_codigo);
                    }

                    if (parseInt(result.data[i].clo_id) >= 10) {
                        linhaAux = linhaAux.replace(/txt_Localizacao_Especifica_VVV/g, result.data[i].ian_localizacao_especifica);
                        linhaAux = linhaAux.replace(/txt_Numero_VVV/g, result.data[i].ian_numero);
                        linhaAux = linhaAux.replace(/txt_Quantidade_VVV/g, result.data[i].ian_quantidade);
                        linhaAux = linhaAux.replace(/txt_EspacamentoMedio_VVV/g, result.data[i].ian_espacamento);
                        linhaAux = linhaAux.replace(/txt_Largura_VVV/g, result.data[i].ian_largura);
                        linhaAux = linhaAux.replace(/txt_Comprimento_VVV/g, result.data[i].ian_comprimento);
                        linhaAux = linhaAux.replace(/txt_AberturaMinima_VVV/g, result.data[i].ian_abertura_minima);
                        linhaAux = linhaAux.replace(/txt_AberturaMaxima_VVV/g, result.data[i].ian_abertura_maxima);
                        linhaAux = linhaAux.replace(/txt_Foto_VVV/g, result.data[i].ian_fotografia);
                        linhaAux = linhaAux.replace(/txt_Croqui_VVV/g, result.data[i].ian_croqui);
                  //      linhaAux = linhaAux.replace(/txt_Desenho_VVV/g, result.data[i].ian_desenho);
                        linhaAux = linhaAux.replace(/txt_Obs_VVV/g, result.data[i].ian_observacoes);

                        if (result.data[i].apt_id == 0)
                            linhaAux = linhaAux.replace(/lbl_Providencia_VVV/g, '');
                        else
                            linhaAux = linhaAux.replace(/lbl_Providencia_VVV/g, result.data[i].apt_id);

                        linhaAux = linhaAux.replace(/TOOLTIP_Providencia_UUUU/g, result.data[i].apt_descricao);


                        linhaAux = linhaAux.replace(/-1/g, '');

                        // cria os itens dos combos ========================================

                        linhaAux = tblFicha4_PROVIDENCIA_prenchetdCombos('cmb_Sigla', result.data[i].lstLegendas, result.data[i].leg_codigo, linhaAux);

                        linhaAux = tblFicha4_PROVIDENCIA_prenchetdCombos('cmb_Alerta', result.data[i].lstAlertas, result.data[i].ale_codigo, linhaAux);

                        linhaAux = tblFicha4_PROVIDENCIA_prenchetdCombos('cmb_Cod', result.data[i].lstTipos, result.data[i].atp_codigo, linhaAux);

                        linhaAux = tblFicha4_PROVIDENCIA_prenchetdCombos('cmb_Causa', result.data[i].lstCausas, result.data[i].aca_codigo, linhaAux);
                    }
                }

                linhas = linhas + linhaAux;
            }

              
            // mescla na tabela existente
            celulaPai.insertAdjacentHTML('afterend', linhas);

            // coloca mascara no campo quantidade
            var qts = $('[id^="txt_Quantidade"]');
            for (var i = 0; i < qts.length; i++) {
                jQuery(qts[i]).attr('placeholder', "000.00");
                jQuery(qts[i]).mask("999.99");
            }

        }
    });

  //  Ficha4_PROVIDENCIAS_setaReadWrite($("#tblFicha4_INSPECAO_ESPECIAL_PROVIDENCIAS"), true);
}

function Ficha4_PROVIDENCIAS_setaReadWrite(tabela, ehRead) {
    // habilita ou desabilita todos os controles editaveis
    var lstTxtBoxes = tabela.getElementsByTagName('input');
    var lstCombos = tabela.getElementsByTagName('select');
    var lstTextareas = tabela.getElementsByTagName('textarea');

    for (var i = 0; i < lstTxtBoxes.length; i++) {
        lstTxtBoxes[i].disabled = ehRead;

        if (!ehRead) {
            var mascara = "";
            var str = lstTxtBoxes[i].id;
            if (str.startsWith("txt_Quantidade_ian_id_"))
                mascara = "999";
            else
                if (str.startsWith("txt_EspacamentoMedio_ian_id_"))
                    mascara = "9999";
                else
                    if (str.startsWith("txt_Largura_ian_id_"))
                        mascara = "9999";
                    else
                        if (str.startsWith("txt_Comprimento_ian_id_"))
                            mascara = "9999";
                        else
                            if (str.startsWith("txt_AberturaMinima_ian_id_"))
                                mascara = "99.99";
                            else
                                if (str.startsWith("txt_AberturaMaxima_ian_id_"))
                                    mascara = "99.99";

            if (mascara != "")
                jQuery("#" + str).mask(mascara);
        }
    }

    for (var i = 0; i < lstTextareas.length; i++)
        lstTextareas[i].disabled = ehRead;

    for (var i = 0; i < lstCombos.length; i++)
        lstCombos[i].disabled = ehRead;

    // =============== ALTERNA OS BOTOES SALVAR/CANCELAR/EDITAR =============================================================
    if (ehRead) {
        document.getElementById("btn_Salvar_INSPECAO_ESPECIAL_CAMPO").style.display = 'none';
        document.getElementById("btn_Cancelar_INSPECAO_ESPECIAL_CAMPO").style.display = 'none';
        document.getElementById("btn_Editar_INSPECAO_ESPECIAL_CAMPO").style.display = 'block';
        document.getElementById("btn_Adicionar_Objeto_Anomalia").style.display = 'none';
    }
    else {
        document.getElementById("btn_Salvar_INSPECAO_ESPECIAL_CAMPO").style.display = 'block';
        document.getElementById("btn_Cancelar_INSPECAO_ESPECIAL_CAMPO").style.display = 'block';
        document.getElementById("btn_Editar_INSPECAO_ESPECIAL_CAMPO").style.display = 'none';
        document.getElementById("btn_Adicionar_Objeto_Anomalia").style.display = 'block';
    }

    // botoes da lixeira criados dinamicamente
    var tblFicha4_INSPECAO_ESPECIAL_PROVIDENCIAS = document.getElementById("tblFicha4_INSPECAO_ESPECIAL_PROVIDENCIAS");
    var lstButtons = tblFicha4_INSPECAO_ESPECIAL_PROVIDENCIAS.getElementsByTagName('button');

    for (var i = 0; i < lstButtons.length; i++)
        if (lstButtons[i].id.includes("btn_ExcluirObjeto_rownum")) {
            lstButtons[i].style.display = ehRead ? 'none' : 'inline'; // aqui display block/none; na criacao da tabela visibility: visible/hidden (para nao misturar porque la existe validacao)
        }

}

function Ficha4_PROVIDENCIAS_preencheLocalizacao(tip_id_Subdivisao1) {

    $.ajax({
        url: '/Objeto/PreencheCmbObjetoLocalizacao',
        type: "POST",
        dataType: "JSON",
        data: { obj_id_TipoOAE: selectedId_obj_id, tip_id_Subdivisao1: tip_id_Subdivisao1 },
        success: function (lstSubNiveis) {

            var i = 0;
            $("#divFicha4_PROVIDENCIAS_LocalizacaoObjeto").empty();
            $.each(lstSubNiveis, function (i, objeto) {
                i++;
                if (i < 100) {
                    var tagchk = '<input type="checkbox" id="idXXX" nome="nameXXX" value="valueXXX" style="margin-right:5px">';
                    tagchk = tagchk.replace("idXXX", "chk" + i);
                    tagchk = tagchk.replace("nameXXX", "chk" + i);
                    tagchk = tagchk.replace("valueXXX", objeto.Value);

                    var taglbl = '<label for="idXXX" class="chklst" >TextoXXX</label> <br />';
                    taglbl = taglbl.replace("idXXX", "chk" + i);
                    taglbl = taglbl.replace("TextoXXX", objeto.Text);

                    $("#divFicha4_PROVIDENCIAS_LocalizacaoObjeto").append(tagchk + taglbl);
                }
            });
        }
    });
}
function Ficha4_PROVIDENCIAS_LimparCampos(aPartirDe) {

    if (aPartirDe <= 6) {
        $("#Ficha4_PROVIDENCIAS_cmbSubdivisao2").html("");
    }

    if (aPartirDe <= 7) $("#Ficha4_PROVIDENCIAS_cmbSubdivisao3").html("");
    if (aPartirDe <= 8) $("#Ficha4_PROVIDENCIAS_cmbGrupoObjetos").html("");

}

function Ficha4_PROVIDENCIAS_Imprimir(apt_id) {

    window.open('../../Reports/frmReport.aspx?relatorio=rptFichaInspecaoEspecial_Providencias&id=999999&ord_id=' + selectedId_ord_id + '&apt_id=' + apt_id, '_blank');

    return false;
}



