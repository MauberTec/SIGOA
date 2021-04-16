    var linhaCabecalhos = ' <tr id="trFICHA4_CAMPO_ian_id_ZZZ" > ' +
        ' <td class="qualClasse" id="tdFICHA4_CodObjeto_ian_id_ZZZ" style="display:EhOrdemServico" > <label class="lblsBold" id="lbl_ObjCodigo_ian_id_ZZZ">lbl_ObjCodigo_VVV</label></td > ' +
        ' <td class="borderLeft qualClasse"><label class="lblsBold" id="lbl_Item_ian_id_ZZZ">lbl_Item_VVV</label></td> ' +
        ' <td class="borderLeft centroH qualClasse" title="lbl_Localizacao_tooltip" ><label class="lblsBold" id="lbl_Localizacao_ian_id_ZZZ">lbl_Localizacao_VVV</label></td> ' +
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
        ' <td class="borderLeft " ></td> ' +
        ' <td class="borderLeft "  style="display:none" ></td> ' +
        ' <td class="borderLeft  borderRight" ></td> ' +
        ' <td class="borderLeft " id="tdFICHA4_ReparoIndicado_ian_id_ZZZ" style="display:EhOrdemServico" ></td> ' +
        ' <td class="borderLeft " id="tdFICHA4_QtIndicada_ian_id_ZZZ" style="display:EhOrdemServico" ></td> ' +
        ' <td class="borderLeft " id="tdFICHA4_ReparoAdotado_ian_id_ZZZ" style="display:EhOrdemServico" ></td> ' +
        ' <td class="borderLeft borderRight" id="tdFICHA4_QtAdotada_ian_id_ZZZ" style="display:EhOrdemServico" ></td> ' +
        ' <td class="borderLeft borderRight" id="tdFICHA4_Providencia_ian_id_ZZZ" style="display:EhOrdemServico" ></td> ' +
        ' </tr>';

    var linhaObjetos = ' <tr id="trFICHA4_CAMPO_ian_id_ZZZ" > ' +
        ' <td class=" qualClasse" id="tdFICHA4_CodObjeto_ian_id_ZZZ" style="display:EhOrdemServico" > <label class="lblsBold" id="lbl_ObjCodigo_ian_id_ZZZ">lbl_ObjCodigo_VVV</label></td > ' +
        ' <td class="borderLeft qualClasse"> ' +
        '          <button id="btn_InserirAnomalia_ian_id_ZZZ" ' +
        '            type="button" ' +
//        '             onclick="return Ficha4_InserirAnomalia(ZZZ)" ' +
        '             onclick="return SalvarDados_Ficha4_CAMPO_VALORES(null, 1, ZZZ) " ' +
        '             title="Inserir Anomalia" ' +
        '             tabindex="tabindexValor" ' +
        '             style="border:none; box-shadow:none; background-color:transparent; display:none"> ' +
        '             <span class="glyphicon glyphicon-plus text-success contornoBranco"> ' +
        '          </button> ' +

        '          <button id="btn_ExcluirAnomalia_ian_id_ZZZ" ' +
        '            type="button" ' +
 //       '             onclick="return Ficha4_ExcluirAnomalia(ZZZ)" ' +
        '             onclick="return SalvarDados_Ficha4_CAMPO_VALORES(null, 2, ZZZ) " ' +
        '             title="Excluir Anomalia" ' +
        '             tabindex="tabindexValor" ' +
        '             style="border:none; box-shadow:none; background-color:transparent; display:none"> ' +
        '             <span class="glyphicon glyphicon-trash text-success contornoBranco"></span> ' +
        '          </button> ' +

        '           <label class="lblsBold" style="vertical-align: middle; display:inline" id="lbl_Item_ian_id_ZZZ">lbl_Item_VVV</label> ' +
        ' </td > ' +
        ' <td class="borderLeft centroH qualClasse" title="lbl_Localizacao_tooltip" ><label class="lblsBold" id="lbl_Localizacao_ian_id_ZZZ" >lbl_Localizacao_VVV</label></td> ' +
        ' <td class="borderLeft centroH qualClasse" > <input disabled id="txt_Localizacao_Especifica_ian_id_ZZZ" class="centroH txts2" style="width:94%; " value="txt_Localizacao_Especifica_VVV"  /></td> ' +
        ' <td class="borderLeft centroH qualClasse" ><input disabled id="txt_Numero_ian_id_ZZZ" class="centroH txts2" style="width:94%; " value="txt_Numero_VVV" onkeyup="txt_Numero_onchange(this)"  /></td> ' +
        ' <td class="borderLeft centroH qualClasse" > <select disabled id="cmb_Sigla_ian_id_ZZZ" class="cmbs_anom" title="TOOLTIP_cmb_Sigla" onchange="cmb_Sigla_onchange(this)" >OPCOES_cmb_Sigla</select></td > ' +
        ' <td class="borderLeft centroH qualClasse" > <select disabled id="cmb_Cod_ian_id_ZZZ" class="cmbs_anom"  title="TOOLTIP_cmb_Cod"  onchange="cmb_Codigo_onchange(this)"  >OPCOES_cmb_Cod</select></td> ' +
        ' <td class="borderLeft centroH qualClasse" > <select disabled id="cmb_Alerta_ian_id_ZZZ" class="cmbs_anom" title="TOOLTIP_cmb_Alerta"  onchange="cmb_Alerta_onchange(this)" >OPCOES_cmb_Alerta</select></td > ' +
        ' <td class="borderLeft centroH qualClasse"><input disabled id="txt_Quantidade_ian_id_ZZZ" class="centroH txts2" style="width:94%; " value="txt_Quantidade_VVV"  onkeyup="txt_Quantidade_onKeyUP(this);" /></td> ' +
        ' <td class="borderLeft centroH qualClasse"><input disabled id="txt_EspacamentoMedio_ian_id_ZZZ" class="centroH txts2" style="width:94%; " value="txt_EspacamentoMedio_VVV" /></td> ' +
        ' <td class="borderLeft centroH qualClasse"><input disabled id="txt_Largura_ian_id_ZZZ" class="centroH txts2" style="width:94%; " value="txt_Largura_VVV" /></td> ' +
        ' <td class="borderLeft centroH qualClasse"><input disabled id="txt_Comprimento_ian_id_ZZZ" class="centroH txts2" style="width:94%; " value="txt_Comprimento_VVV" onkeyup="txt_Comprimento_onKeyUP(this);" /></td> ' +
        ' <td class="borderLeft centroH qualClasse"><input disabled id="txt_AberturaMinima_ian_id_ZZZ" class="centroH txts2" style="width:94%; " value="txt_AberturaMinima_VVV" /></td> ' +
        ' <td class="borderLeft centroH qualClasse"><input disabled id="txt_AberturaMaxima_ian_id_ZZZ" class="centroH txts2" style="width:94%; " value="txt_AberturaMaxima_VVV" /></td> ' +
        ' <td class="borderLeft centroH qualClasse" > <select disabled id="cmb_Causa_ian_id_ZZZ" class="cmbs_anom" title="TOOLTIP_cmb_Causa"  onchange="cmb_Causa_onchange(this)" >OPCOES_cmb_Causa</select></td > ' +

        ' <td class="borderLeft centroH qualClasse"><input disabled id="txt_Foto_ian_id_ZZZ" class="centroH txts2" style="width:94%; " value="txt_Foto_VVV" /></td> ' +
        ' <td class="borderLeft centroH qualClasse"><input disabled id="txt_Croqui_ian_id_ZZZ" class="centroH txts2" style="width:94%; " value="txt_Croqui_VVV" /></td> ' +
        ' <td class="borderLeft centroH qualClasse" style="display:none" ><input disabled id="txt_Desenho_ian_id_ZZZ" class="centroH txts2" style="width:94%; " value="txt_Desenho_VVV" /></td> ' +
        ' <td class="borderLeft centroH qualClasse borderRight"><input disabled id="txt_Obs_ian_id_ZZZ" class="centroH txts2" style="width:94%; " value="txt_Obs_VVV" onkeydown="txt_Obs_onKeyDown(this)" /></td> ' +

        ' <td class="borderLeft centroH qualClasse" id="tdFICHA4_ReparoIndicado_ian_id_ZZZ" style="display:EhOrdemServico"  ><label class="centroH txts2" id="lbl_ReparoIndicado_ian_id_ZZZ" title="TOOLTIP_ReparoIndicado_ian_id" >lbl_ReparoIndicado_VVV</label></td> ' +
        ' <td class="borderLeft centroH qualClasse" id="tdFICHA4_QtIndicada_ian_id_ZZZ" style="display:EhOrdemServico" ><label class="centroH txts2" id="lbl_QuantidadeIndicada_ian_id_ZZZ" style="display:inline">lbl_QuantidadeIndicada_VVV</label><label class="centroH txts2" id="lbl_QuantidadeIndicadaUnidade_ian_id_ZZZ" style="display:inline">lbl_QuantidadeIndicadaUnidade_VVV</label></td> ' +
        ' <td class="borderLeft centroH qualClasse" id="tdFICHA4_ReparoAdotado_ian_id_ZZZ" style="display:EhOrdemServico" ><select disabled id="cmb_ReparoAdotado_ian_id_ZZZ" class="cmbs_anom" title="TOOLTIP_cmb_ReparoAdotado" >OPCOES_cmb_ReparoAdotado</select></td> ' +
        //' <td class="borderLeft centroH qualClasse" id="tdFICHA4_ReparoAdotado_ian_id_ZZZ" style="display:EhOrdemServico" ><input disabled id="txt_ReparoAdotado_ian_id_ZZZ" class="centroH" style="width:94%;" value="txt_ReparoAdotado_VVV" /></td> ' +
        ' <td class="borderLeft centroH  qualClasse" id="tdFICHA4_QtAdotada_ian_id_ZZZ" style="display:EhOrdemServico" ><input disabled id="txt_QuantidadeAdotada_ian_id_ZZZ" class="centroH txts2" style="width:94%; " value="txt_QuantidadeAdotada_VVV" /></td> ' +
        ' <td class="borderLeft centroH borderRight qualClasse" id="tdFICHA4_Providencia_ian_id_ZZZ" style="display:EhOrdemServico"  ><label class="centroH txts2" id="lbl_Providencia_ian_id_ZZZ" title="TOOLTIP_Providencia_UUUU" >lbl_Providencia_VVV</label></td> ' +
        ' </tr>';


function txt_Obs_onKeyDown(quem) {
    // Set self as the current item in focus
    var self = $(':focus'),
        // Set the form by the current item in focus
        form = $("#tblFicha4_INSPECAO_ESPECIAL_CAMPO"), Tabbable ; //self.parents('form:eq(0)'), focusable;

    // Array of Indexable/Tab-able items
    tabbable = form.find('input').filter(':visible');

    function enterKey() {
        if (event.which === 13 && !self.is('textarea')) { // [Enter] key

            if ($.inArray(self, tabbable) && (!self.is('a')) && (!self.is('button'))) {
                event.preventDefault();
            } 
            tabbable.eq(tabbable.index(self) + (event.shiftKey ? -1 : 1)).focus();

            return false;
        }
    }
    if (event.shiftKey) { enterKey() } else { enterKey() }
}




function txt_Comprimento_onKeyUP(quem) {
    quem.style.backgroundColor = corBranca;
}

function txt_Quantidade_onKeyUP(quem) {
    quem.style.backgroundColor = corBranca;
}
function txt_Numero_onchange(quem) {
    quem.style.backgroundColor = corBranca;
}

function Ficha4_CAMPO_ExportarXLS() {
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

function CancelarDados_Ficha4_CAMPO(tabela) {
    preenchetblFicha4_CAMPO(true);

}
function EditarDados_Ficha4_CAMPO(tabela) {
    // alterna os campos para escrita
    Ficha4_CAMPO_setaReadWrite(tabela, false);

    return false;
}

function SalvarDados_Ficha4_CAMPO_VALORES(tabela, qualEvento, qual_ian_id) {

    if (qualEvento == null)
        qualEvento = 0;

    if (qual_ian_id == null)
        qual_ian_id = -1;

    // monta lista de valores das linhas
    var saida = '';
    var table = document.getElementById("tblFicha4_INSPECAO_ESPECIAL_CAMPO");

    for (var i = 0; i < table.rows.length; i++) {
        if (table.rows[i].id.includes("trFICHA4_CAMPO_ian_id")) {
            var ian_id = table.rows[i].id.replace("trFICHA4_CAMPO_ian_id_", "");

          //  if ((parseInt(ian_id) != qual_ian_id) && (qual_ian_id > 0))
            {
                var txt_Numero_ian_id = document.getElementById("txt_Numero_ian_id_" + ian_id);
                if (txt_Numero_ian_id) {
                    var ian_localizacao_especifica = $("#txt_Localizacao_Especifica_ian_id_" + ian_id).val();
                    var ian_numero = $("#txt_Numero_ian_id_" + ian_id).val();
                    var leg_codigo = $("#cmb_Sigla_ian_id_" + ian_id).val() + '';
                    var atp_codigo = $("#cmb_Cod_ian_id_" + ian_id).val() + '';
                    var ale_codigo = $("#cmb_Alerta_ian_id_" + ian_id).val() + '';
                    var ian_quantidade = $("#txt_Quantidade_ian_id_" + ian_id).val();
                    var ian_espacamento = $("#txt_EspacamentoMedio_ian_id_" + ian_id).val();
                    var ian_largura = $("#txt_Largura_ian_id_" + ian_id).val();
                    var ian_comprimento = $("#txt_Comprimento_ian_id_" + ian_id).val();
                    var ian_abertura_minima = $("#txt_AberturaMinima_ian_id_" + ian_id).val();
                    var ian_abertura_maxima = $("#txt_AberturaMaxima_ian_id_" + ian_id).val();
                    var aca_codigo = $("#cmb_Causa_ian_id_" + ian_id).val() + '';
                    var ian_fotografia = $("#txt_Foto_ian_id_" + ian_id).val();
                    var ian_croqui = $("#txt_Croqui_ian_id_" + ian_id).val();
                    var ian_desenho = $("#txt_Desenho_ian_id_" + ian_id).val();
                    var ian_observacoes = $("#txt_Obs_ian_id_" + ian_id).val();
                    var rpt_id_sugerido = $("#lbl_ReparoIndicado_ian_id_" + ian_id).text();
                    var qt_sugerido = $("#lbl_QuantidadeIndicada_ian_id_" + ian_id).text();
                    //      var rpt_id_adotado = $("#txt_ReparoAdotado_ian_id_" + ian_id).val();
                    var rpt_id_adotado = $("#cmb_ReparoAdotado_ian_id_" + ian_id).val();
                    var qt_adotado = $("#txt_QuantidadeAdotada_ian_id_" + ian_id).val();

                    var ehLinhaVazia = true;
                    var ehLinhaOK = false;

                    if ((ian_numero != "")
                        || ((leg_codigo != "") && (leg_codigo != "null"))
                        || ((atp_codigo != "") && (atp_codigo != "null"))
                        || ((ale_codigo != "") && (ale_codigo != "null"))
                        || (ian_quantidade.replace(".", "").replace(",", "") != "")
                        || (ian_espacamento.replace(".", "").replace(",", "") != "")
                        || (ian_largura.replace(".", "").replace(",", "") != "")
                        || (ian_comprimento.replace(".", "").replace(",", "") != "")
                        || (ian_abertura_minima.replace(".", "").replace(",", "") != "")
                        || (ian_abertura_maxima.replace(".", "").replace(",", "") != "")
                        || ((aca_codigo != "") && (aca_codigo != "null"))
                        || (ian_fotografia.trim() != "")
                        || (ian_croqui.trim() != "")
                        || (ian_desenho.trim() != "")
                        || (ian_observacoes.trim() != "")
                    )
                        ehLinhaVazia = false;

                    ////if (ehLinhaVazia) // se for linha vazia, nao precisa salvar
                    ////    continue;

                    if ((ian_numero == "")
                        && ((leg_codigo == "") || (leg_codigo == "null"))
                        && ((atp_codigo == "") || (atp_codigo == "null"))
                        && ((ale_codigo == "") || (ale_codigo == "null"))
                        && (ian_quantidade.replace(".", "").replace(",", "") == "")
                        && (ian_espacamento.replace(".", "").replace(",", "") == "")
                        && (ian_largura.replace(".", "").replace(",", "") == "")
                        && (ian_comprimento.replace(".", "").replace(",", "") == "")
                        && (ian_abertura_minima.replace(".", "").replace(",", "") == "")
                        && (ian_abertura_maxima.replace(".", "").replace(",", "") == "")
                        && ((aca_codigo == "") || (aca_codigo == "null"))
                        && (ian_fotografia.trim() == "")
                        && (ian_croqui.trim() == "")
                        && (ian_desenho.trim() == "")
                        && (ian_observacoes.trim() != "")
                    )
                        ehLinhaOK = true;


                    // campo Numero
                    if ((ian_numero == "") && (!ehLinhaVazia) && (!ehLinhaOK))
                    {
                        txt_Numero_ian_id.style.backgroundColor = corVermelho;
                        swal({
                            type: 'error',
                            title: 'Aviso',
                            text: 'O Número da Anomalia é obrigatório'
                        }).then(
                            function () {
                                return false;
                            });
                        return false;
                    }

                    // campo Sigla
                    if ((leg_codigo == "-1") || (leg_codigo == "") || (leg_codigo == "null")) // campo Sigla
                    {
                        leg_codigo = "-1";
                        if ((ehLinhaOK == false) && (!ehLinhaVazia))
                        {
                            var cmb_Sigla = document.getElementById("cmb_Sigla_ian_id_" + ian_id);
                            cmb_Sigla.style.backgroundColor = corVermelho;
                            swal({
                                type: 'error',
                                title: 'Aviso',
                                text: 'A Sigla é obrigatória'
                            }).then(
                                function () {
                                    return false;
                                });
                            return false;
                        }
                    }

                    // campo Código
                    if ((atp_codigo == "-1") || (atp_codigo == "") || (atp_codigo == "null"))  // campo Codigo
                    {
                        atp_codigo = "-1";
                        if ((ehLinhaOK == false) && (!ehLinhaVazia))
                        {
                            var cmb_Cod = document.getElementById("cmb_Cod_ian_id_" + ian_id);
                            cmb_Cod.style.backgroundColor = corVermelho;
                            swal({
                                type: 'error',
                                title: 'Aviso',
                                text: 'O Código é obrigatório'
                            }).then(
                                function () {
                                    return false;
                                });
                            return false;
                        }
                    }

                    // campo Alerta
                    if ((ale_codigo == "-1") || (ale_codigo == "") || (ale_codigo == "null")) // campo Alerta
                    {
                        ale_codigo = "-1";
                        if ((ehLinhaOK == false) && (!ehLinhaVazia))
                        {
                            var cmb_Alerta = document.getElementById("cmb_Alerta_ian_id_" + ian_id);
                            cmb_Alerta.style.backgroundColor = corVermelho;
                            swal({
                                type: 'error',
                                title: 'Aviso',
                                text: 'O Alerta é obrigatório'
                            }).then(
                                function () {
                                    return false;
                                });
                            return false;
                        }
                    }

                    // campo Causa
                    if ((aca_codigo == "-1") || (aca_codigo == "") || (aca_codigo == "null"))  // campo Causa
                    {
                        aca_codigo = "-1";
                        if ((ehLinhaOK == false) && (!ehLinhaVazia))
                        {
                            var cmb_Causa = document.getElementById("cmb_Causa_ian_id_" + ian_id);
                            cmb_Causa.style.backgroundColor = corVermelho;
                            swal({
                                type: 'error',
                                title: 'Aviso',
                                text: 'A Causa é obrigatória'
                            }).then(
                                function () {
                                    return false;
                                });
                            return false;
                        }
                    }

                    // quantidade obrigatoria
                    var valor = ian_quantidade;
                    if ((valor.trim() == ",") || (valor.trim() == ".") || (valor.trim() == "")) {
                        ian_quantidade = " ";
                        if ((ehLinhaOK == false) && (!ehLinhaVazia))
                        {
                            var txt_Quantidade = document.getElementById("txt_Quantidade_ian_id_" + ian_id);
                            txt_Quantidade.style.backgroundColor = corVermelho;
                            swal({
                                type: 'error',
                                title: 'Aviso',
                                text: 'A Quantidade é obrigatória'
                            }).then(
                                function () {
                                    return false;
                                });
                            return false;
                        }
                    }

                    // comprimento é obrigatorio
                    valor = ian_comprimento;
                    if ((valor.trim() == ",") || (valor.trim() == ".") || (valor.trim() == "")) {
                        ian_comprimento = " ";

                        if ((ehLinhaOK == false) && (!ehLinhaVazia)) {
                            var txt_Comprimento_ian_id = document.getElementById("txt_Comprimento_ian_id_" + ian_id);
                            txt_Comprimento_ian_id.style.backgroundColor = corVermelho;
                            swal({
                                type: 'error',
                                title: 'Aviso',
                                text: 'O Comprimento é obrigatório'
                            }).then(
                                function () {
                                    return false;
                                });
                            return false;
                        }
                    }



                    valor = ian_espacamento;
                    if ((valor.trim() == ",") || (valor.trim() == ".") || (valor.trim() == ""))
                        ian_espacamento = " ";

                    // largura nao obrigatoria, metro linear para reparo rpt_id in (1,26,27,28,30,31)
                    valor = ian_largura;
                    if ((valor.trim() == ",") || (valor.trim() == ".") || (valor.trim() == ""))
                        ian_largura = " ";

                    if (ian_localizacao_especifica.trim() == "")
                        ian_localizacao_especifica = " ";

                    if (ian_numero.trim() == "")
                        ian_numero = " ";

                    valor = ian_abertura_minima;
                    if ((valor.trim() == ",") || (valor.trim() == ".") || (valor.trim() == ""))
                        ian_abertura_minima = " ";

                    valor = ian_abertura_maxima;
                    if ((valor.trim() == ",") || (valor.trim() == ".") || (valor.trim() == ""))
                        ian_abertura_maxima = " ";

                    if (ian_fotografia.trim() == "")
                        ian_fotografia = " ";

                    if (ian_croqui.trim() == "")
                        ian_croqui = " ";

                    if (ian_desenho.trim() == "")
                        ian_desenho = " ";

                    if (ian_observacoes.trim() == "")
                        ian_observacoes = " ";

                    if ((rpt_id_sugerido == null) || (rpt_id_sugerido.trim() == ""))
                        rpt_id_sugerido = " ";

                    if ((qt_sugerido == null) || (qt_sugerido.trim() == ""))
                        qt_sugerido = " ";

                    if ((rpt_id_adotado == null) || (rpt_id_adotado.trim() == ""))
                        rpt_id_adotado = " ";

                    if ((qt_adotado == null) || (qt_adotado.trim() == ""))
                        qt_adotado = " ";

                    if (aca_codigo == "-1")
                        aca_codigo = " ";

                    //if (isNaN(ovv_tpu_quantidade))
                    //    ovv_tpu_quantidade = 0;

                    var linhaMontada = '<tr_linha>'
                        + ian_id + '<quebra>'
                        + ian_numero + '<quebra>' + leg_codigo + '<quebra>'
                        + atp_codigo + '<quebra>' + ale_codigo + '<quebra>'
                        + ian_quantidade.replace(',', '.') + '<quebra>' + ian_espacamento.replace(',', '.') + '<quebra>'
                        + ian_largura.replace(',', '.') + '<quebra>' + ian_comprimento.replace(',', '.') + '<quebra>'
                        + ian_abertura_minima.replace(',', '.') + '<quebra>' + ian_abertura_maxima.replace(',', '.') + '<quebra>'
                        + aca_codigo + '<quebra>' + ian_fotografia + '<quebra>'
                        + ian_croqui + '<quebra>' + ian_desenho + '<quebra>'
                        + ian_observacoes + '<quebra>'
                        + rpt_id_sugerido + '<quebra>' + rpt_id_adotado + '<quebra>'
                        + qt_sugerido.replace(',', '.') + '<quebra>' + qt_adotado.replace(',', '.') + '<quebra>'
                        + i  + '<quebra>'
                        + ian_localizacao_especifica + '<quebra>'
                        + '</tr_linha>';

                    saida = saida + linhaMontada;
                }

            }
        }
    }
    //  alert(saida);

    // ********* ENVIA OS DADOS PARA O BANCO **********************************************************************
    var ord_id = 0;
    if ((paginaPai == "OrdemServico") || (paginaPai == "Inspecao"))
        ord_id = selectedId_ord_id;


    var ins_anom_Responsavel = $("#txt_ins_anom_Responsavel").val();
    var ins_anom_data = $("#txt_ins_anom_data").val();
    var ins_anom_quadroA_1 = $("#cmb_ins_anom_quadroA_1").val();
    var ins_anom_quadroA_2 = $("#txt_ins_anom_quadroA_2").val();

    if (ins_anom_quadroA_1 == 'nao')
        ins_anom_quadroA_2 = '';

    $.ajax({
        url: "/Inspecao/InspecaoAnomalias_Valores_Salvar",
        data: JSON.stringify({
            'ord_id': ord_id,
            'ins_anom_Responsavel': ins_anom_Responsavel, 'ins_anom_data': ins_anom_data,
            'ins_anom_quadroA_1': ins_anom_quadroA_1, 'ins_anom_quadroA_2': ins_anom_quadroA_2,
            'listaConcatenada': saida
        }),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {

            switch (qualEvento)
            {
                case 1: Ficha4_InserirAnomalia(qual_ian_id); break; // inserir anomalia
                case 2: Ficha4_ExcluirAnomalia(qual_ian_id); break; // excluir anomalia
                case 3: Ficha4_CAMPO_btn_Adicionar_Objeto_Anomalia_onclick(); break; // inserir OBJETO
                default:preenchetblFicha4_CAMPO(true);
            }

            return false;
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
            return false;
        }
    });


    return false;
}

function limpatblFicha4_CAMPO() {

    var tabela = document.getElementById("divFicha4_CAMPO");

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

function Ficha4_CAMPO_prenchetdCombos(qualCombo, listadeValores, selectedValue, linhaAux) {
    var tooltip = 'TOOLTIP_' + qualCombo;
    var opcoes = 'OPCOES_' + qualCombo;

    var op0 = ' <option selectedxx disabled value="-1" ></option> ';
    //if (qualCombo == 'cmb_Sigla')
    //    op0 = op0.replace('disabled', '');

    var op = ' <option selectedxx value="valor" title="tooltip">texto</option> ';

    var total = op0;

    if (parseInt(selectedValue) == -1) {
        total = total.replace("selectedxx", "selected");
        linhaAux = linhaAux.replace(tooltip, "");
    }
    else {
        total = total.replace("selectedxx", "");
    }

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
                opt = opt.replace("selectedxx", "selected");
                linhaAux = linhaAux.replace(tooltip, aux[1]);
            }
            else {
                opt = opt.replace("selectedxx", "");
            }
            total = total + opt;
        }
    }

    if (linhaAux.includes(tooltip))
        linhaAux = linhaAux.replace(tooltip, "");

    linhaAux = linhaAux.replace(opcoes, total);

    return linhaAux;
}

function preenchetblFicha4_CAMPO(ehRead) {
    if (ehRead == null)
        ehRead = true;

    $("#lblOAE").text(selected_obj_codigo);
    $('#txt_ins_anom_data').datepicker({ dateFormat: 'dd/mm/yy' });

    $.ajax({
        "url": "/Inspecao/InspecaoAnomalias_Valores_ListAll",
        "type": "GET",
        "datatype": "json",
        "data": { "ord_id": selectedId_ord_id },
        "success": function (result) {

            $('#txt_ins_anom_data').val("");
            $('#txt_ins_anom_Responsavel').val("");
            $('#cmb_ins_anom_quadroA_1').val("");
            $('#txt_ins_anom_quadroA_2').val("");

            // limpa as linhas se houver
            var table = document.getElementById("tblFicha4_INSPECAO_ESPECIAL_CAMPO");
            var rowCount = table.rows.length;
            for (var i = rowCount - 1; i >= 0; i--) {
                if (table.rows[i].id.indexOf("trFICHA4_CAMPO_ian_id") >= 0)
                    table.deleteRow(i);
            }

            var linhas = '';
            var celulaPai = document.getElementById("tr_pai");

            for (var i = 0; i < result.data.length; i++) {
                if (i == 0) {
                    $('#txt_ins_anom_Responsavel').val(result.data[i].ins_anom_Responsavel);
                    $('#txt_ins_anom_data').val(result.data[i].ins_anom_data);
                    $('#cmb_ins_anom_quadroA_1').val(result.data[i].ins_anom_quadroA_1);
                    $('#txt_ins_anom_quadroA_2').val(result.data[i].ins_anom_quadroA_2);
                }

                var linhaAux = linhaObjetos;
                if (parseInt(result.data[i].clo_id) < 10)
                    linhaAux = linhaCabecalhos;

                if (parseInt(result.data[i].clo_id) == 6)  // subdivisao1
                    linhaAux = linhaAux.replace(/YYY/g, result.data[i].rownum).replace(/qualClasse/g, "cabecalho_subdivisao1");
                else
                    if (parseInt(result.data[i].clo_id) == 7) // subdivisao2
                        linhaAux = linhaAux.replace(/YYY/g, result.data[i].rownum).replace(/qualClasse/g, "cabecalho_subdivisao2");
                    else
                        if (parseInt(result.data[i].clo_id) == 9) // grupo
                            linhaAux = linhaAux.replace(/YYY/g, result.data[i].rownum).replace(/qualClasse/g, "cabecalho_grupos");
                        else
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
                    linhaAux = linhaAux.replace(/txt_Desenho_VVV/g, result.data[i].ian_desenho);
                    linhaAux = linhaAux.replace(/txt_Obs_VVV/g, result.data[i].ian_observacoes);

                    linhaAux = linhaAux.replace(/lbl_ReparoIndicado_VVV/g, result.data[i].rpt_id_sugerido_codigo);
                    linhaAux = linhaAux.replace(/TOOLTIP_ReparoIndicado_ian_id/g, result.data[i].rpt_id_sugerido_descricao);

                    if (result.data[i].apt_id == 0) 
                        linhaAux = linhaAux.replace(/lbl_Providencia_VVV/g, '');
                    else
                        linhaAux = linhaAux.replace(/lbl_Providencia_VVV/g, result.data[i].apt_id);

                    linhaAux = linhaAux.replace(/TOOLTIP_Providencia_UUUU/g, result.data[i].apt_descricao);

                    if (!isNaN((result.data[i].ian_quantidade_adotada)))
                        linhaAux = linhaAux.replace(/lbl_QuantidadeIndicada_VVV/g, (((result.data[i].ian_quantidade_sugerida.toFixed(2)) + " ").replace(".", ",").trim()));
                    else
                        linhaAux = linhaAux.replace(/lbl_QuantidadeIndicada_VVV/g, '');

                    linhaAux = linhaAux.replace(/lbl_QuantidadeIndicadaUnidade_VVV/g, result.data[i].rpt_id_sugerido_unidade);

                   // linhaAux = linhaAux.replace(/txt_ReparoAdotado_VVV/g, result.data[i].rownum);
                    if (!isNaN((result.data[i].ian_quantidade_adotada)))
                        linhaAux = linhaAux.replace(/txt_QuantidadeAdotada_VVV/g, (((result.data[i].ian_quantidade_adotada.toFixed(2)) + " ").replace(".", ",").trim()));
                    else
                        linhaAux = linhaAux.replace(/txt_QuantidadeAdotada_VVV/g, '');

                    // substitui o valor -1 por vazio
                    linhaAux = linhaAux.replace(/-1/g, '');

                    // insere o valor "-1" para desabilitar algumas tabulacoes
                    linhaAux = linhaAux.replace(/tabindexValor/g, '-1');

                    // cria os itens dos combos ========================================
                    linhaAux = Ficha4_CAMPO_prenchetdCombos('cmb_Sigla', result.data[i].lstLegendas, result.data[i].leg_codigo, linhaAux);

                    linhaAux = Ficha4_CAMPO_prenchetdCombos('cmb_Alerta', result.data[i].lstAlertas, result.data[i].ale_codigo, linhaAux);

                    linhaAux = Ficha4_CAMPO_prenchetdCombos('cmb_Cod', result.data[i].lstTipos, result.data[i].atp_codigo, linhaAux);

                    linhaAux = Ficha4_CAMPO_prenchetdCombos('cmb_Causa', result.data[i].lstCausas, result.data[i].aca_codigo, linhaAux);

                    linhaAux = Ficha4_CAMPO_prenchetdCombos('cmb_ReparoAdotado', result.data[i].lstReparoTipos, result.data[i].rpt_id_adotado, linhaAux);
                }


                linhas = linhas + linhaAux;
            }

            // oculta algumas colunas se a pagina for O.S. ou Inspecao
            if (paginaPai == "OrdemServico")
                linhas = linhas.replace(/EhOrdemServico/g, 'none');
            else
                if (paginaPai == "Inspecao") 
                    linhas = linhas.replace(/display:EhOrdemServico/g, '');

               
            // mescla na tabela existente
            celulaPai.insertAdjacentHTML('afterend', linhas);

            // coloca mascara no campo quantidade
            var qts = $('[id^="txt_Quantidade"]');
            for (var i = 0; i < qts.length; i++) {
                if (!ehRead)
                    jQuery(qts[i]).attr('placeholder', "000,00");
                else
                    jQuery(qts[i]).attr('placeholder', "");

                jQuery(qts[i]).mask("999,99");
            }
            
            if (paginaPai == "Inspecao")
            {
                document.getElementById("tdFICHA4_CodObjeto_ian_id_header000a").style.display = '';
                document.getElementById("tdFICHA4_CodObjeto_ian_id_header001a").style.display = '';
                document.getElementById("tdFICHA4_CodObjeto_ian_id_header002a").style.display = '';
                document.getElementById("tdFICHA4_CodObjeto_ian_id_header003a").style.display = '';
                document.getElementById("tdFICHA4_CodObjeto_ian_id_header004a").style.display = '';
                document.getElementById("tdFICHA4_CodObjeto_ian_id_header005a").style.display = '';

                document.getElementById("tdFICHA4_CodObjeto_ian_id_header1a").style.display = '';
                document.getElementById("tdFICHA4_CodObjeto_ian_id_header1b").style.display = '';
                
                document.getElementById("tdFICHA4_CodObjeto_ian_id_header000b").style.display = '';
                document.getElementById("tdFICHA4_CodObjeto_ian_id_header001b").style.display = '';
                document.getElementById("tdFICHA4_CodObjeto_ian_id_header002b").style.display = '';
                document.getElementById("tdFICHA4_CodObjeto_ian_id_header003b").style.display = '';
                document.getElementById("tdFICHA4_CodObjeto_ian_id_header004b").style.display = '';
                document.getElementById("tdFICHA4_CodObjeto_ian_id_header005b").style.display = '';

                document.getElementById("tdFICHA4_CodObjeto_ian_id_header000c").style.display = '';
                document.getElementById("tdFICHA4_CodObjeto_ian_id_header001c").style.display = '';
                document.getElementById("tdFICHA4_CodObjeto_ian_id_header002c").style.display = '';
                document.getElementById("tdFICHA4_CodObjeto_ian_id_header003c").style.display = '';
                document.getElementById("tdFICHA4_CodObjeto_ian_id_header004c").style.display = '';
                document.getElementById("tdFICHA4_CodObjeto_ian_id_header005c").style.display = '';

                document.getElementById("tdFICHA4_CodObjeto_ian_id_header000d").style.display = '';
                document.getElementById("tdFICHA4_CodObjeto_ian_id_header001d").style.display = '';
                document.getElementById("tdFICHA4_CodObjeto_ian_id_header002d").style.display = '';
                document.getElementById("tdFICHA4_CodObjeto_ian_id_header003d").style.display = '';
                document.getElementById("tdFICHA4_CodObjeto_ian_id_header004d").style.display = '';
                document.getElementById("tdFICHA4_CodObjeto_ian_id_header005d").style.display = '';
            
                document.getElementById("tdFICHA4_CodObjeto_ian_id_header000e").style.display = '';
                document.getElementById("tdFICHA4_CodObjeto_ian_id_header001e").style.display = '';

                document.getElementById("tdFICHA4_CodObjeto_ian_id_header000f").style.display = '';
                document.getElementById("tdFICHA4_CodObjeto_ian_id_header001f").style.display = '';

                document.getElementById("tdFICHA4_CodObjeto_ian_id_header000g").style.display = '';
                document.getElementById("tdFICHA4_CodObjeto_ian_id_header001g").style.display = '';
            }
            else
                if (paginaPai == "OrdemServico")
                {
                    document.getElementById("tdFICHA4_CodObjeto_ian_id_header000a").style.display = 'none';
                    document.getElementById("tdFICHA4_CodObjeto_ian_id_header001a").style.display = 'none';
                    document.getElementById("tdFICHA4_CodObjeto_ian_id_header002a").style.display = 'none';
                    document.getElementById("tdFICHA4_CodObjeto_ian_id_header003a").style.display = 'none';
                    document.getElementById("tdFICHA4_CodObjeto_ian_id_header004a").style.display = 'none';
                    document.getElementById("tdFICHA4_CodObjeto_ian_id_header005a").style.display = 'none';

                    document.getElementById("tdFICHA4_CodObjeto_ian_id_header1a").style.display = 'none';
                    document.getElementById("tdFICHA4_CodObjeto_ian_id_header1b").style.display = 'none';

                    document.getElementById("tdFICHA4_CodObjeto_ian_id_header000b").style.display = 'none';
                    document.getElementById("tdFICHA4_CodObjeto_ian_id_header001b").style.display = 'none';
                    document.getElementById("tdFICHA4_CodObjeto_ian_id_header002b").style.display = 'none';
                    document.getElementById("tdFICHA4_CodObjeto_ian_id_header003b").style.display = 'none';
                    document.getElementById("tdFICHA4_CodObjeto_ian_id_header004b").style.display = 'none';
                    document.getElementById("tdFICHA4_CodObjeto_ian_id_header005b").style.display = 'none';

                    document.getElementById("tdFICHA4_CodObjeto_ian_id_header000c").style.display = 'none';
                    document.getElementById("tdFICHA4_CodObjeto_ian_id_header001c").style.display = 'none';
                    document.getElementById("tdFICHA4_CodObjeto_ian_id_header002c").style.display = 'none';
                    document.getElementById("tdFICHA4_CodObjeto_ian_id_header003c").style.display = 'none';
                    document.getElementById("tdFICHA4_CodObjeto_ian_id_header004c").style.display = 'none';
                    document.getElementById("tdFICHA4_CodObjeto_ian_id_header005c").style.display = 'none';

                    document.getElementById("tdFICHA4_CodObjeto_ian_id_header000d").style.display = 'none';
                    document.getElementById("tdFICHA4_CodObjeto_ian_id_header001d").style.display = 'none';
                    document.getElementById("tdFICHA4_CodObjeto_ian_id_header002d").style.display = 'none';
                    document.getElementById("tdFICHA4_CodObjeto_ian_id_header003d").style.display = 'none';
                    document.getElementById("tdFICHA4_CodObjeto_ian_id_header004d").style.display = 'none';
                    document.getElementById("tdFICHA4_CodObjeto_ian_id_header005d").style.display = 'none';

                    document.getElementById("tdFICHA4_CodObjeto_ian_id_header000e").style.display = 'none';
                    document.getElementById("tdFICHA4_CodObjeto_ian_id_header001e").style.display = 'none';

                    document.getElementById("tdFICHA4_CodObjeto_ian_id_header000f").style.display = 'none';
                    document.getElementById("tdFICHA4_CodObjeto_ian_id_header001f").style.display = 'none';

                    document.getElementById("tdFICHA4_CodObjeto_ian_id_header000g").style.display = 'none';
                    document.getElementById("tdFICHA4_CodObjeto_ian_id_header001g").style.display = 'none';
                }

            // alterna os campos para leitura/edicao
            var tblFicha4_INSPECAO_ESPECIAL_CAMPO = document.getElementById("tblFicha4_INSPECAO_ESPECIAL_CAMPO");
            if (tblFicha4_INSPECAO_ESPECIAL_CAMPO)
                Ficha4_CAMPO_setaReadWrite(tblFicha4_INSPECAO_ESPECIAL_CAMPO, ehRead);
        }

   });




}

function Ficha4_CAMPO_CalculaReparoIndicado(quem)
{
    var sufixo = quem.id.replace("cmb_Sigla", "").replace("cmb_Cod", "").replace("cmb_Alerta", "").replace("cmb_Causa", "");

    // combo Sigla
    var cmb_Sigla_id = "cmb_Sigla" + sufixo;
    var leg_codigo = $("#" + cmb_Sigla_id).val();

    // combo Codigo
    var cmb_Cod_id = "cmb_Cod" + sufixo;
    var atp_codigo = $("#" + cmb_Cod_id).val();

    // combo Alerta
    var cmb_Alerta_id = "cmb_Alerta" + sufixo;
    var ale_codigo = $("#" + cmb_Alerta_id).val();

    // combo Causa
    var cmb_Causa_id = "cmb_Causa" + sufixo;
    var aca_codigo = $("#" + cmb_Causa_id).val();

    // label ReparoIndicado
    var lbl_ReparoIndicado_id = "lbl_ReparoIndicado" + sufixo;
    var lbl_ReparoIndicado = document.getElementById(lbl_ReparoIndicado_id);

    // label Quantidade Indicada
    var lbl_QuantidadeIndicada_id = "lbl_QuantidadeIndicada" + sufixo;
    var lbl_QuantidadeIndicada = document.getElementById(lbl_QuantidadeIndicada_id);

    // label Unidade da Quantidade Indicada
    var lbl_QuantidadeIndicadaUnidade_id = "lbl_QuantidadeIndicadaUnidade" + sufixo;
    var lbl_QuantidadeIndicadaUnidade = document.getElementById(lbl_QuantidadeIndicadaUnidade_id);

    // label Providencia Indicada
    var lbl_Providencia_ian_id = "lbl_Providencia" + sufixo;
    var lbl_Providencia = document.getElementById(lbl_Providencia_ian_id);

    // txt_Quantidade_ian_id_
    var txt_Quantidade_ian_id =  "txt_Quantidade" + sufixo;
    var txt_Quantidade = document.getElementById(txt_Quantidade_ian_id);
    var f_Quantidade = parseFloat(txt_Quantidade.value);
    if (Number.isNaN(f_Quantidade))
        f_Quantidade = 0;

    // txt_Largura_ian_id_
    var txt_Largura_ian_id =  "txt_Largura" + sufixo;
    var txt_Largura = document.getElementById(txt_Largura_ian_id);
    var f_Largura = parseFloat(txt_Largura.value);
    if (Number.isNaN(f_Largura))
        f_Largura = 0;

    // txt_Comprimento_ian_id_
    var txt_Comprimento_ian_id =  "txt_Comprimento" + sufixo;
    var txt_Comprimento = document.getElementById(txt_Comprimento_ian_id);
    var f_Comprimento = parseFloat(txt_Comprimento.value);
    if (Number.isNaN(f_Comprimento))
        f_Comprimento = 0;

    var rpt_area = (f_Comprimento / 100) * (f_Largura / 100);

    $.ajax({
        url: '/Inspecao/InspecaoAnomalia_ReparoSugerido',
        type: "POST",
        dataType: "JSON",
        data: { leg_codigo: leg_codigo, atp_codigo: atp_codigo, ale_codigo: ale_codigo, aca_codigo: aca_codigo, rpt_area: rpt_area },
        success: function (result) {
            lbl_QuantidadeIndicada.innerHTML = "";
            lbl_ReparoIndicado.innerHTML = "";
            lbl_ReparoIndicado.title = "";
            lbl_QuantidadeIndicadaUnidade.innerHTML = "";

            lbl_Providencia.innerHTML = "";
            lbl_Providencia.title = "";

            if (result.data.length > 0) {
                if (result.data[0].rpt_codigo != "") {

                    lbl_ReparoIndicado.innerHTML = result.data[0].rpt_id;
                    lbl_ReparoIndicado.title = result.data[0].rpt_descricao;
                    lbl_QuantidadeIndicadaUnidade.innerHTML = result.data[0].rpt_unidade;

                    // metro linear para reparo rpt_id in (1, 26, 27, 28, 30, 31) 2021/fev/15
                    var rpt_id = parseInt(result.data[0].rpt_id);
                    if ((rpt_id == 1) || (rpt_id == 26) || (rpt_id == 27) || (rpt_id == 28) || (rpt_id == 30) || (rpt_id == 31)) {
                        lbl_QuantidadeIndicada.innerHTML = (((f_Quantidade * (f_Comprimento / 100)).toFixed(2)) + " ").replace(".", ",").trim();
                    }
                    else {
                        switch (result.data[0].rpt_unidade.toUpperCase()) {
                            case "M":
                                lbl_QuantidadeIndicada.innerHTML = (((f_Quantidade * (f_Comprimento / 100)).toFixed(2)) + " ").replace(".",",").trim();
                                break;

                            case "M2":
                                lbl_QuantidadeIndicada.innerHTML = (((f_Quantidade * (f_Comprimento / 100) * (f_Largura / 100)).toFixed(2)) + " ").replace(".", ",").trim();
                                break;

                            default: // UM, UN
                                lbl_QuantidadeIndicada.innerHTML = (((f_Quantidade * (f_Comprimento / 100)).toFixed(2)) + " ").replace(".", ",").trim();
                                break;
                        }
                    }
                }
                else {
                    lbl_Providencia.innerHTML = result.data[0].rpt_id;
                    lbl_Providencia.title = result.data[0].rpt_descricao;
                }
            }
        }
    });

}

function cmb_Sigla_onchange(quem) {

    quem.style.backgroundColor = corBranca;

    var seltooltip = quem.options[quem.selectedIndex].title;
    quem.title = seltooltip;

    var valor = quem.options[quem.selectedIndex].value;

    // preenche combo Codigo
    var cmb_Cod_id = quem.id.replace("cmb_Sigla", "cmb_Cod")
    var cmb_Cod = document.getElementById(cmb_Cod_id);

    $.ajax({
        url: '/Inspecao/InspecaoAnomaliaTipos_by_Legenda',
        type: "POST",
        dataType: "JSON",
        data: { leg_codigo: valor },
        success: function (result) {

            $('#' + cmb_Cod_id).html('');
            var pedacos = result.data.split(";");
            for (k = 0; k < pedacos.length; k++) {
                var aux = pedacos[k].split(":");
                var opt = document.createElement('option');
                opt.value = aux[0].trim();
                opt.innerHTML = aux[0].trim();
                if (aux.length > 1)
                    opt.title = aux[1].trim();
                cmb_Cod.appendChild(opt);
            }

            // preenche combo causas
            var cmb_Causa_id = quem.id.replace("cmb_Sigla", "cmb_Causa")
            var cmb_Causa = document.getElementById(cmb_Causa_id);
            $.ajax({
                url: '/Inspecao/InspecaoAnomaliaCausas_by_Legenda',
                type: "POST",
                dataType: "JSON",
                data: { leg_codigo: valor },
                success: function (result) {
                    $('#' + cmb_Causa_id).html('');
                    var pedacos = result.data.split(";");
                    for (k = 0; k < pedacos.length; k++) {
                        var aux = pedacos[k].split(":");
                        var opt = document.createElement('option');
                        opt.value = aux[0].trim();
                        opt.innerHTML = aux[0].trim();
                        if (aux.length > 1)
                            opt.title = aux[1].trim();
                        cmb_Causa.appendChild(opt);
                    }

                    // preenche alerta
                    var cmb_Alerta_id = quem.id.replace("cmb_Sigla", "cmb_Alerta")
                    var cmb_Alerta = document.getElementById(cmb_Alerta_id);
                    $.ajax({
                        url: '/Inspecao/InspecaoAnomaliaAlertas_by_Legenda',
                        type: "POST",
                        dataType: "JSON",
                        data: { leg_codigo: valor },
                        success: function (result) {
                            $('#' + cmb_Alerta_id).html('');
                            var pedacos = result.data.split(";");
                            for (k = 0; k < pedacos.length; k++) {
                                var aux = pedacos[k].split(":");
                                var opt = document.createElement('option');
                                opt.value = aux[0].trim();
                                opt.innerHTML = aux[0].trim();
                                if (aux.length > 1)
                                    opt.title = aux[1].trim();
                                cmb_Alerta.appendChild(opt);
                            }
                        }
                    })
                }
            })
        }
    });

}
function cmb_Codigo_onchange(quem) {
    quem.style.backgroundColor = corBranca;

    Ficha4_CAMPO_CalculaReparoIndicado(quem);
}
function cmb_Alerta_onchange(quem) {
    quem.style.backgroundColor = corBranca;

    Ficha4_CAMPO_CalculaReparoIndicado(quem);
}
function cmb_Causa_onchange(quem) {

    quem.style.backgroundColor = corBranca;

    Ficha4_CAMPO_CalculaReparoIndicado(quem);
}

function Ficha4_CAMPO_setaReadWrite(tabela, ehRead) {
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
                mascara = "9999";
            else
                if (str.startsWith("txt_EspacamentoMedio_ian_id_"))
                    mascara = "999,99";
                else
                    if (str.startsWith("txt_Largura_ian_id_"))
                        mascara = "999,99";
                    else
                        if (str.startsWith("txt_Comprimento_ian_id_"))
                            mascara = "9999,99";
                        else
                            if (str.startsWith("txt_AberturaMinima_ian_id_"))
                                mascara = "999,99";
                            else
                                if (str.startsWith("txt_AberturaMaxima_ian_id_"))
                                    mascara = "999,99";
                            else
                                if (str.startsWith("txt_Numero_ian_id_"))
                                    mascara = "99999";
            if (mascara != "")
                jQuery("#" + str).mask(mascara);

            //if (!ehRead)
            //    jQuery("#" + str).attr('placeholder', mascara.replace(/9/g,'0'));
            //else
            //    jQuery("#" + str).attr('placeholder', "");
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
    var tblFicha4_INSPECAO_ESPECIAL_CAMPO = document.getElementById("tblFicha4_INSPECAO_ESPECIAL_CAMPO");
    var lstButtons = tblFicha4_INSPECAO_ESPECIAL_CAMPO.getElementsByTagName('button');

    for (var i = 0; i < lstButtons.length; i++)
        if ((lstButtons[i].id.includes("btn_ExcluirAnomalia_ian_id_")) || (lstButtons[i].id.includes("btn_InserirAnomalia_ian_id_")) )
        {
            lstButtons[i].style.display = ehRead ? 'none' : 'inline'; // aqui display block/none; na criacao da tabela visibility: visible/hidden (para nao misturar porque la existe validacao)
        }

}

function chk00_click(chk00_checked)
{
    $('#divFicha4_CAMPO_LocalizacaoObjeto input').each(function () {
         $(this).prop("checked", chk00_checked);
    });

    return false;
}

function Ficha4_CAMPO_preencheCombo(clo_id, qualCombo, txtPlaceholder, tip_pai) {
    if (tip_pai == null)
        tip_pai = -1;

    //   var excluir_existentes = qualCombo == 'divFicha2_GrupoObjetos' ? 1 : 0;
    var excluir_existentes = 0;

    // limpa os itens da localizacao
    $("#divFicha4_CAMPO_LocalizacaoObjeto").empty();

    var cmb = $("#" + qualCombo);
    cmb.html(""); // limpa os itens existentes
    cmb.append($('<option selected ></option>').val(-1).html(txtPlaceholder)); // 1o item vazio

    $.ajax({
        url: '/Objeto/PreenchecmbTiposObjeto',
        type: "POST",
        dataType: "JSON",
        data: { clo_id: clo_id, tip_pai: tip_pai, excluir_existentes: excluir_existentes, obj_id: selectedId_obj_id },
        success: function (lstSubNiveis) {

            if (clo_id != 10) {
                $.each(lstSubNiveis, function (i, subNivel) {
                    cmb.append($('<option></option>').val(subNivel.Value.trim()).html(subNivel.Text.trim()));
                });
            }
            else {
                var i = 0;
                $.each(lstSubNiveis, function (i, objeto) {
                    i++;
                    if (i < 150) {
                        var tagchk = '<input type="checkbox" id="idXXX" nome="nameXXX" value="valueXXX" style="margin-right:5px">';
                        tagchk = tagchk.replace("idXXX", "chk" + i);
                        tagchk = tagchk.replace("nameXXX", "chk" + i);
                        tagchk = tagchk.replace("valueXXX", objeto.Value);

                        var taglbl = '<label for="idXXX" class="chklst" >TextoXXX</label> <br />';
                        taglbl = taglbl.replace("idXXX", "chk" + i);
                        taglbl = taglbl.replace("TextoXXX", objeto.Text);

                        $("#divFicha4_CAMPO_LocalizacaoObjeto").append(tagchk + taglbl);
                    }
                });
            }
        }
    });
}

function Ficha4_CAMPO_preencheLocalizacao(tip_id_Grupo) {

    $.ajax({
        url: '/Objeto/PreencheCmbObjetoLocalizacao',
        type: "POST",
        dataType: "JSON",
        data: { obj_id_TipoOAE: selectedId_obj_id, tip_id_Grupo: tip_id_Grupo },
        success: function (lstSubNiveis) {

            var i = 0;
            $("#divFicha4_CAMPO_LocalizacaoObjeto").empty();

            var tagchk00 = '<input type="checkbox" id="chk00" nome="chk00" value="-314159" style="margin-right:5px" onclick="chk00_click(this.checked)" >';
            var taglbl00 = '<label for="idchk00" class="chklst" >Todos</label> <br />';

            if (lstSubNiveis.length > 0) {
                $("#divFicha4_CAMPO_LocalizacaoObjeto").append(tagchk00 + taglbl00);

                $.each(lstSubNiveis, function (i, objeto) {
                    i++;
                    if (i < 1000) {
                        var tagchk = '<input type="checkbox" id="idXXX" nome="nameXXX" value="valueXXX" style="margin-right:5px">';
                        tagchk = tagchk.replace("idXXX", "chk" + i);
                        tagchk = tagchk.replace("nameXXX", "chk" + i);
                        tagchk = tagchk.replace("valueXXX", objeto.Value);

                        var taglbl = '<label for="idXXX" class="chklst" >TextoXXX</label> <br />';
                        taglbl = taglbl.replace("idXXX", "chk" + i);
                        taglbl = taglbl.replace("TextoXXX", objeto.Text);

                        $("#divFicha4_CAMPO_LocalizacaoObjeto").append(tagchk + taglbl);
                    }
                });
            }
            else
            {
                var taglbl00a = '<label class="chklst" >Objeto(s) não cadastrado(s)</label> <br />';
                $("#divFicha4_CAMPO_LocalizacaoObjeto").append(taglbl00a);

            }

        }
    });
}

function Ficha4_CAMPO_LimparCampos(aPartirDe) {

    if (aPartirDe <= 6) {
        $("#Ficha4_CAMPO_cmbSubdivisao2").html("");
    }

    if (aPartirDe <= 7) $("#Ficha4_CAMPO_cmbSubdivisao3").html("");
    if (aPartirDe <= 8) $("#Ficha4_CAMPO_cmbGrupoObjetos").html("");

}

function Ficha4_CAMPO_btn_Adicionar_Objeto_Anomalia_onclick() {
    var Ficha4_CAMPO_cmbSubdivisao1 = document.getElementById("Ficha4_CAMPO_cmbSubdivisao1");

    // limpa os itens existentes;
    ////Ficha4_CAMPO_cmbSubdivisao1.selectedIndex = -1;
    ////$("#Ficha4_CAMPO_cmbSubdivisao2").html("");
    ////$("#Ficha4_CAMPO_cmbSubdivisao3").html("");
    ////$("#Ficha4_CAMPO_cmbGrupoObjetos").html("");
    ////$("#divFicha4_CAMPO_LocalizacaoObjeto").html("");

    $("#modalSelecionarObjetoLocalizacao").modal('show');

}
function Ficha4_CAMPO_cmbSubdivisao1_onchange() {

    Ficha4_CAMPO_LimparCampos(6);

    // preenche proximo combo
    var valor = document.getElementById("Ficha4_CAMPO_cmbSubdivisao1").value;
    var ivalor = parseInt(valor);

    //Ficha4_CAMPO_preencheLocalizacao(ivalor);

    // oculta o divs Ficha2_Subdivisao2
    document.getElementById("Ficha4_CAMPO_divSubdivisao2").style.display = 'none';
    document.getElementById("Ficha4_CAMPO_divSubdivisao3").style.display = 'none';
    document.getElementById("Ficha4_CAMPO_divGrupoObjetos").style.display = 'block';
    document.getElementById("Ficha4_CAMPO_divLocalizacaoObjeto").style.display = 'block';

    // superestrutura
    if (ivalor == 11) {
        // mostra Subdivisao2
        document.getElementById("Ficha4_CAMPO_divSubdivisao2").style.display = 'block';
        Ficha4_CAMPO_preencheCombo(7, 'Ficha4_CAMPO_cmbSubdivisao2', '--Selecione--', ivalor);
    }
    else
        // ENCONTROS
        if (ivalor == 14) {
            // mostra Subdivisao2 e 3
            document.getElementById("Ficha4_CAMPO_divSubdivisao2").style.display = 'block';

            Ficha4_CAMPO_preencheCombo(7, 'Ficha4_CAMPO_cmbSubdivisao2', '--Selecione--', ivalor);

            //// preenche combo manualmente, tip_id = 22,23,24 ESTRUTURAS DE TERRRA, DE CONCRETO E ACESSOS
            //$("#Ficha4_CAMPO_cmbSubdivisao2").append($('<option></option>').val("").html("--Selecione--"));
            //for (var i = 0; i < cmbTiposObjeto_FichaInspecaoRotineira.options.length; i++) {
            //    if ((getTipoId(cmbTiposObjeto_FichaInspecaoRotineira.options[i].value) == 22)
            //        || (getTipoId(cmbTiposObjeto_FichaInspecaoRotineira.options[i].value) == 23)
            //        || (getTipoId(cmbTiposObjeto_FichaInspecaoRotineira.options[i].value) == 24)
            //    ) {
            //        var option = document.createElement("option");
            //        option.text = cmbTiposObjeto_FichaInspecaoRotineira.options[i].text;
            //        option.value = cmbTiposObjeto_FichaInspecaoRotineira.options[i].value;
            //        $("#Ficha4_CAMPO_cmbSubdivisao2").append($('<option></option>').val(option.value).html(option.text));
            //    }
            //}
        }
        else
            Ficha4_CAMPO_preencheCombo(9, 'Ficha4_CAMPO_cmbGrupoObjetos', '--Selecione--', ivalor);

}
function Ficha4_CAMPO_cmbSubdivisao2_onchange() {

    // oculta o divs Subdivisao3
    document.getElementById("Ficha4_CAMPO_divSubdivisao3").style.display = 'none';

    // preenche proximo combo
    var valor = document.getElementById("Ficha4_CAMPO_cmbSubdivisao2").value;
    var ivalor = getTipoId(valor);

    $("#Ficha4_CAMPO_cmbSubdivisao3").html("");
    $("#Ficha4_CAMPO_cmbGrupoObjetos").html("");
    $("#divFicha4_CAMPO_LocalizacaoObjeto").html("");

    if ((ivalor == 24) || (ivalor == 15) || (ivalor == 16)) { // 15 = Tabuleiro Face Superior; 16=Tabuleiro Face Inferior; 24 = Acesso
        Ficha4_CAMPO_LimparCampos(9);
        Ficha4_CAMPO_preencheCombo(9, 'Ficha4_CAMPO_cmbGrupoObjetos', '--Selecione--', ivalor)
    }
    else {
        Ficha4_CAMPO_LimparCampos(8);
        document.getElementById("Ficha4_CAMPO_divSubdivisao3").style.display = 'block';
        Ficha4_CAMPO_preencheCombo(8, 'Ficha4_CAMPO_cmbSubdivisao3', '--Selecione--', ivalor)
    }


}
function Ficha4_CAMPO_cmbSubdivisao3_onchange() {

    // preenche proximo combo
    var valor = document.getElementById("Ficha4_CAMPO_cmbSubdivisao3").value;
    var ivalor = getTipoId(valor);

    Ficha4_CAMPO_LimparCampos(8);
    Ficha4_CAMPO_preencheCombo(9, 'Ficha4_CAMPO_cmbGrupoObjetos', '--Selecione--', ivalor)
}

function Ficha4_CAMPO_cmbGrupoObjetos_onchange() {

    // preenche proximo combo
    var valor = document.getElementById("Ficha4_CAMPO_cmbGrupoObjetos").value;
    var ivalor = getTipoId(valor);

    //   Ficha4_CAMPO_preencheCombo(10, 'divFicha4_CAMPO_LocalizacaoObjeto', '--Selecione--', ivalor)
    Ficha4_CAMPO_preencheLocalizacao(ivalor);
}

function Ficha4_CAMPO_bntSalvar_Localizacao_click() {

    var Ficha4_CAMPO_cmbSubdivisao1 = document.getElementById("Ficha4_CAMPO_cmbSubdivisao1");

    // cria lista dos IDs de Objetos selecionados
    var selchks = [];
    $('#divFicha4_CAMPO_LocalizacaoObjeto input:checked').each(function () {
        var valor = $(this).attr('value')
        if (valor > 0 )
            selchks.push(valor);
    });

    //se houver grupos selecionados, salva
    if (selchks.length > 0) {
        if (Ficha4_CAMPO_cmbSubdivisao1.selectedIndex > 0) {

            //cria a lista de objetos
            var obj_ids = "";
            for (var i = 0; i < selchks.length; i++)
                if (i == 0)
                    obj_ids = selchks[i];
                else
                    obj_ids = obj_ids + ";" + selchks[i];

            obj_ids = obj_ids + ";"; // acrescenta um delimitador no final da string

            var listaObjs = {
                ord_id: selectedId_ord_id, obj_ids: obj_ids
            };

            $.ajax({
                url: "/Inspecao/InspecaoAnomaliaObjetos_Salvar",
                data: JSON.stringify(listaObjs),
                type: "POST",
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                success: function (result) {

                    // atualiza os dados
                    preenchetblFicha4_CAMPO(false);

                    $("#modalSelecionarObjetoLocalizacao").modal('hide');
                    return false;
                },
                error: function (errormessage) {
                    alert(errormessage.responseText);
                    $("#modalSelecionarObjetoLocalizacao").modal('show');
                    return false;
                }
            });
        }
    }
    else {
        swal({
            type: 'error',
            title: 'Aviso',
            text: 'Objeto(s) não selecionado(s)'
        }).then(
            function () {
                return false;
            });
    }
    return false;
}


function Ficha4_ExcluirAnomalia(qual_ian_id) {
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
            var response = POST("/Inspecao/InspecaoAnomalia_Excluir", JSON.stringify({ id: qual_ian_id }))
            if (response.erroId >= 1) {
                swal({
                    type: 'success',
                    title: 'Sucesso',
                    text: 'Registro excluído com sucesso'
                });

                // atualiza tabela
                preenchetblFicha4_CAMPO(false);

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
function Ficha4_InserirAnomalia(qual_ian_id) {

    var response = POST("/Inspecao/InspecaoAnomalia_Nova", JSON.stringify({ id: qual_ian_id }))
            if (response.erroId >= 1) {
                //swal({
                //    type: 'success',
                //    title: 'Sucesso',
                //    text: 'Registro Incluído com sucesso'
                //});

                // atualiza tabela
                preenchetblFicha4_CAMPO(false);

                return false;
            }
            else {
                swal({
                    type: 'error',
                    title: 'Aviso',
                    text: 'Erro ao Inserir Anomalia'
                });
            }
            return false;
}

