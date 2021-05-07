var cabecalhoTabela =   '         <tr style="text-align:center; border-top:solid 1px black "> ' +
                        '            <td class="borderLeft borderBottom fundoSilver" rowspan="2" > ' +
                        '                <table style="width:100%;"> ' +
                        '                  <tr> ' +
                        '                    <td style="width:26px"></td> ' +
                        '                    <td> ' +
                        '                      <label class="lblsBold"> Local / Elemento </label> ' +
                        '                    </td> ' +
                        '                  </tr> ' +
                        '                </table> ' +
                        '             </td > ' +
                        '             <td class="borderLeft borderBottom  fundoSilver" rowspan = "2" > <label class="lblsBold" id="lblatr_id_82a">Variáveis</label></td > ' +
                        '             <td class="borderLeft borderBottom  fundoSilver" rowspan = "2" > <label class="lblsBold" id="lblatr_id_82b">Caracterização da Situação</label></td > ' +
                        '             <td class="borderLeft borderBottom  fundoSilver" rowspan = "2" > <label class="lblsBold" id="lblatr_id_82c">Condição para Inspeção</label></td > ' +
                        '             <td class="borderLeft borderBottom  fundoSilver" rowspan = "2" colspan = "2" > <label class="lblsBold" id="lblatr_id_82d">Observações gerais </label></td > ' +
                        '             <td class="borderLeft borderBottom  fundoSilver borderRight" colspan = "3" > <label class="lblsBold" id="lblatr_id_82e">Estimativa de Serviços - Conserva</label></td > ' +
                        '             </tr > ' +
                        '               <tr style = "text-align:center"  id="Ficha2_PROVIDENCIAS_tr_Cabecalho_YYY" > ' +
                        '               <td class="borderLeft borderBottom fundoSilver " style = "width:40% !important" > <label class="lblsBold" id="lblatr_id_82f">Descrição do Serviço</label></td > ' +
                        '               <td class="borderLeft borderBottom  fundoSilver" style = "width:30% !important" > <label class="lblsBold" id="lblatr_id_82g">Unid</label></td > ' +
                        '               <td class="borderLeft borderBottom  fundoSilver borderRight" style = "width:30% !important" > <label class="lblsBold" id="lblatr_id_82h">Quantidade</label></td > ' +
                        '           </tr>';

var cabecalho0_Providencias = '<tr id="trFICHA2_OOBBJJIIDD_GGG_VVV_YYY" class="BorderTop borderBottom" >' +
                                '<td  style="height:40px !important; font-size:13pt; text-align:center; border-top: solid 1px black !important;" class="borderLeft BorderTop borderBottom borderRight " colspan="9">' +
    '                                <label class="lblsBold" > XXXXX</label >' +
        '                            <button title="Imprimir" onclick="Ficha2_PROVIDENCIAS_Imprimir(PRT_IDDD)" ><i class="fa fa-print" ></i></button>  ' +
    '                           </td >' +
    '                          </tr > ';

var rodape0_Providencias = '<tr id="trFICHA2_OOBBJJIIDD_GGG_VVV_YYY" class="BorderTop borderBottom" ><td style="height:50px !important" class="BorderTop borderBottom"  colspan="9"></td></tr>';

var cabecalho1_Providencias = '<tr id="trFICHA2_OOBBJJIIDD_GGG_VVV_YYY"><td class="borderLeft borderBottomPt borderRight subdivisao1_fundo" colspan="9"><label class="lblsBold" >XXXXX</label></td></tr>';

var cabecalho2_Providencias =
    ' <tr id="trFICHA2_OOBBJJIIDD_GGG_VVV_YYY">' +
    ' <td class="borderLeft borderBottomPt borderRight subdivisao2_fundo" colspan = "9" >' +
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


var cabecalho3_Providencias = '<tr id="trFICHA2_OOBBJJIIDD_GGG_VVV_YYY"><td class="borderLeft borderBottomPt borderRight subdivisao3_fundo" colspan="9"><label class="lblsBold" >XXXXX</label></td></tr>';

var MesclarGrupo_Providencias =
    '   <td class="borderLeft borderTop borderRight borderBottomPt" rowspan=N_ROWSPAN > ' +
    '     <table style="width:100%"> ' +
    '      <tr> ' +
    '        <td> ' +
    '           <label id="lbl_Elemento_GGG_VVV" class="lblsNormal">lbl_Elemento_XXXXX</label> ' +
    '        </td> ' +
    '     </tr> ' +
    '   </table> ' +
    '  </td> ';

var Mesclar_Condicao_Inspecao_Providencias =
    '  <td class="borderTop borderRight borderBottomPt centroH" rowspan=N_ROWSPAN > ' +
    '    <select class="cmbs" id="cmb_condicao_GGG_VVV" disabled="true"> ' +
    '          OPCOES_cmb_condicao ' +
    '    </select> ' +
    '  </td> ';

var linhaGrupos_Providencias =
    ' <tr id="trFICHA2_OOBBJJIIDD_GGG_VVV_YYY"> ' +

    '  MesclarGrupo_Providencias  ' +

    '  <td class="borderTop borderRight borderBottomPt"> ' +
    '    <label id="lbl_Variaveis_GGG_VVV" class="lblsNormal">lbl_Variaveis_XXXXX</label> ' +
    '  </td> ' +
    '  <td class="borderTop borderRight borderBottomPt centroH"> ' +
    '    <select class="cmbs" id="cmb_situacao_GGG_VVV" title="TOOLTIP_cmb_situacao"  disabled="true"> ' +
    '          OPCOES_cmb_situacao ' +
    '    </select> ' +
    '  </td> ' +

    ' Mesclar_Condicao_Inspecao_Providencias ' +

    '  <td class="borderTop borderRight borderBottomPt" colspan="2" style="text-align:center"> ' +
    '    <textarea id="txt_obs_GGG_VVV" class="txts" rows="2" cols="50"  maxlength="255" style="width:98%; overflow:auto" disabled="true" >txt_obs_XXXXX</textarea> ' +
    '  </td> ' +
    '  <td class="borderTop borderRight borderBottomPt"> ' +
    '    <label id="lbl_servico_GGG_VVV" class="txts" style="border:none; width:100%; text-align:left" title="txt_servico_XXXXX">txt_servico_XXXXX</label> ' +
    '    <select class="cmbs" id="cmb_tpu_descricao_itens_GGG_VVV" style="display:none"> ' +
    '          OPCOES_cmb_tpu_descricao_itens ' +
    '    </select> ' +
    '  </td> ' +
    '  <td class="borderTop borderRight borderBottomPt  centroH"> ' +
    '    <input id="txt_unidade_GGG_VVV" class="txts  centroH" value="txt_unidade_XXXXX" maxlength="15"  disabled="true"  /> ' +
    '  </td> ' +
    '  <td class="borderTop borderRight borderBottomPt borderRight centroH"> ' +
    '    <input id="txt_quantidade_GGG_VVV" class="txts  centroH" value="txt_quantidade_XXXXX" disabled="true"  /> ' +
    '  </td> ' +
    ' </tr> ';

function Ficha2_PROVIDENCIAS_Imprimir(prt_id) {

    var liFichaInspecao1aRotineira = document.getElementById("liFichaInspecao1aRotineira");
    var liFichaInspecaoRotineira = document.getElementById("liFichaInspecaoRotineira");
    var tos_id = 1;

    if ((liFichaInspecao1aRotineira) && (liFichaInspecaoRotineira))
    {
        if ((liFichaInspecao1aRotineira.style.display == "none") && (liFichaInspecaoRotineira.style.display != "none"))
            tos_id = 2;
    }

    window.open('../../Reports/frmReport.aspx?relatorio=rptFichaInspecaoRotineira_Providencias&id=99999999&tos_id=' + tos_id + '&ord_id=' + selectedId_ord_id + '&prt_id=' + prt_id, '_blank');

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

function preenchetblFicha2_PROVIDENCIAS() {
    // a cada refresh, reseta o timer
    resetTimeout();


    if ((paginaPai == "OrdemServico") || (paginaPai == "Inspecao")) {

            Ficha2_PROVIDENCIAS_CriarTabelaGrupos();
    }

}

function Ficha2_PROVIDENCIAS_CriarTabelaGrupos() {

    // limpa as linhas se houver
    var table = document.getElementById("tblFicha2_PROVIDENCIAS_GRUPOS");
    var rowCount = table.rows.length;
    for (var i = rowCount - 1; i >= 0; i--) {
        if (table.rows[i].id.indexOf("trFICHA2_") >= 0)
            table.deleteRow(i);
    }


    var linhas = '';
    var celulaPai = document.getElementById("Ficha2_PROVIDENCIAS_tr_Cabecalho_TUDO");

    var ord_id = 0;
    if ((paginaPai == "OrdemServico") || (paginaPai == "Inspecao"))
        ord_id = selectedId_ord_id;

    $.ajax({
        "url": "/Objeto/GruposVariaveisValores_ListAll",
        "type": "GET",
        "datatype": "json",
        "data": {
            "obj_id": selectedId_obj_id, "ord_id": ord_id, "ehProvidencia": 1, "filtro_prt_id":99999 },
        "success": function (result) {

            // varre as linhas do dataset
            for (var i = 0; i < result.data.length; i++)
            {
                if (parseInt(result.data[i].nCabecalhoGrupo) == 5)  // CABECALHO 0
                {
                  //  celulaPai = document.getElementById("Ficha2_PROVIDENCIAS_tr_Cabecalho_" + i);
                    if (i > 0) linhas = linhas + rodape0_Providencias;

                    linhas = linhas + cabecalho0_Providencias.replace("XXXXX", result.data[i].prt_descricao).replace("YYY", i).replace("PRT_IDDD", result.data[i].prt_id);
                    linhas = linhas + cabecalhoTabela.replace("YYY", i);
                }
                else
                  if (parseInt(result.data[i].nCabecalhoGrupo) == 1)  // CABECALHO 1
                    linhas = linhas + cabecalho1_Providencias.replace("XXXXX", result.data[i].nome_pai).replace("YYY", i);
                  else
                    if (parseInt(result.data[i].nCabecalhoGrupo) == 2) // CABECALHO 2
                    {
                        var visibilitySub2 = "hidden";
                        linhas = linhas + cabecalho2_Providencias.replace(/TIPP_IIDD/g, result.data[i].tip_pai).replace(/XXXXX/g, result.data[i].nome_pai).replace(/YYY/g, i).replace("visibilitySubdivisao2", visibilitySub2);
                    }
                    else
                        if (parseInt(result.data[i].nCabecalhoGrupo) == 3) // CABECALHO 3
                            linhas = linhas + cabecalho3_Providencias.replace("XXXXX", result.data[i].nome_pai).replace("YYY", i);
                        else
                            if (parseInt(result.data[i].nCabecalhoGrupo) == 0) // LINHA NORMAL
                            {
                                var linhaAux = linhaGrupos_Providencias;
                                linhaAux = linhaAux.replace("YYY", i);

                                // MESCLA CELULAS SE NECESSARIO
                                if ((result.data[i].nomeGrupo.trim() != "") && (parseInt(result.data[i].mesclarLinhas) > 0)) {
                                    linhaAux = linhaAux.replace(/MesclarGrupo_Providencias/g, MesclarGrupo_Providencias);
                                    linhaAux = linhaAux.replace(/Mesclar_Condicao_Inspecao_Providencias/g, Mesclar_Condicao_Inspecao_Providencias);
                                    linhaAux = linhaAux.replace(/N_ROWSPAN/g, result.data[i].mesclarLinhas);
                                }
                                else {
                                    linhaAux = linhaAux.replace(/MesclarGrupo_Providencias/g, "");
                                    linhaAux = linhaAux.replace(/Mesclar_Condicao_Inspecao_Providencias/g, "");
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

        }
    });


    travaBotoes();
}

