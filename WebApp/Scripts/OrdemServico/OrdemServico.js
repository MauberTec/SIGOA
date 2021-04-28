
var selectedId_ord_id = 0;
var selectedId_obj_id = 0;
var selectedId_tos_id = 0;
var selectedId_sos_id = 0;
var selected_obj_codigo = '';

var selectedId_orc_id = 0;

var filtroOrdemServico_codigo = '';
var filtroObj_codigo = '';
var filtroTiposOS = -1;
var filtroStatusOS = -1;
var filtroord_data_De = '';
var filtroord_data_Ate = '';
var ehEdicao = false;

var totalParcial_Reparos = 0;
var totalParcial_ServicosAdicionados = 0;

// *********** modal Nova O.S. ******************************************
function OrdemServico_LimparDetalhes_Novo()
{
    $('#txtord_codigo_Novo').val("");
    $('#cmbTiposOS_Novo').val(null);
    $('#txtord_descricao_Novo').val("");
    $('#txtobj_codigo_Novo').val("");
    $('#txtord_data_planejada_Novo').val("");

   // $("#txtusu_nome_Novo").removeAttr("title");

 //   $('#txtusu_nome_Novo').val("");
 //   $('#txtord_data_abertura_Novo').val("");
    //   $('#txtord_data_planejada_Novo').datepicker('setDate', new Date())

     $('#cmbTiposOS_Novo').css('background-color', corBranca);
   $('#txtord_descricao_Novo').css('background-color', corBranca);
    $('#txtobj_codigo_Novo').css('background-color', corBranca);
    $('#txtord_data_planejada_Novo').css('background-color', corBranca);

}

function btnOSInserir_onclick()
{
    OrdemServico_LimparDetalhes_Novo();
    $("#modalSalvarRegistro").modal('show');

}

function bntSalvar_Novo_onclick()
{
    // faz as validacoes
    // TIPO OS
    var cmbTiposOS_Novo = document.getElementById("cmbTiposOS_Novo");
    if (cmbTiposOS_Novo.selectedIndex <= 0) {

        cmbTiposOS_Novo.style.backgroundColor = corVermelho;
        swal({
            type: 'error',
            title: 'Aviso',
            text: 'O Tipo de O.S. é obrigatório.'
        }).then(
            function () {
                return false;
            });
        return false;
    }

    // DESCRICAO
    var txtord_descricao_NovoVal = $("#txtord_descricao_Novo").val().trim();
    if (txtord_descricao_NovoVal.length == 0) {

        $('#txtord_descricao_Novo').css('background-color', corVermelho);
        swal({
            type: 'error',
            title: 'Aviso',
            text: 'A Descrição é obrigatória.'
        }).then(
            function () {
                return false;
            });
        return false;
    }

    // OBJETO
    var txtobj_codigo_Novo = $("#txtobj_codigo_Novo").val().trim();
    if (txtobj_codigo_Novo.length == 0) {

        $('#txtobj_codigo_Novo').css('background-color', corVermelho);
        swal({
            type: 'error',
            title: 'Aviso',
            text: 'O Objeto é obrigatório.'
        }).then(
            function () {
                return false;
            });
        return false;
    }

    // DATA Planejada
    var txtord_data_planejada_Novo = $("#txtord_data_planejada_Novo").val().trim();
    if (txtord_data_planejada_Novo.length == 0) {

        $('#txtord_data_planejada_Novo').css('background-color', corVermelho);
        swal({
            type: 'error',
            title: 'Aviso',
            text: 'A Data Início Planejada é obrigatória.'
        }).then(
            function () {
                return false;
            });
        return false;
    }

    var sel_tos_id = $('#cmbTiposOS_Novo').val();
    var Obj_codigo = $('#txtobj_codigo_Novo').val();
    var ord_data_planejada_Novo = $("#txtord_data_planejada_Novo").val();
    Obj_codigo = Obj_codigo.substring(0, Obj_codigo.indexOf("(") - 1);

    var cmbTiposOS_Novo = $("#cmbTiposOS_Novo option:selected").text();

    var texto = 'Já existe O.S. ' + cmbTiposOS_Novo + ' com Data Início Planejada ' + ord_data_planejada_Novo + ' para o Objeto ' + Obj_codigo;
    // checa se ja existe O.S. do mesmo tipo, objeto e data planejada
    $.ajax({
        url: "/OrdemServico/OrdemServico_ListAll",
        "data": {
            "ord_id": 0,
            "filtroOrdemServico_codigo": 0,
            "filtroObj_codigo": Obj_codigo,
            "filtroTiposOS": sel_tos_id,
            "filtroStatusOS": "",
            "filtroData": "Inicio_Programada",
            "filtroord_data_De": ord_data_planejada_Novo,
            "filtroord_data_Ate": ord_data_planejada_Novo
        },
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {


            if (result.data.length > 0) {
                // entao já tem mesmo tipo para a data planejada e mesmo objeto
                swal({
                    type: 'error',
                    title: 'Aviso',
                    text: texto
                }).then(
                    function () {
                        return false;
                    });

                return false;
            }
            else {
                // se nao tem, entao pode salvar
                var params = {
                    ord_id: -1,
                    ord_codigo: $('#txtord_codigo_Novo').val().trim(),
                    ord_descricao: txtord_descricao_NovoVal,
                    tos_id: sel_tos_id,
                    obj_id: selectedId_obj_id,
                    ord_ativo: 1,
                    ord_data_inicio_programada: ord_data_planejada_Novo,
                    ord_aberta_por: $("#txtusu_nome_Novo").attr("title"),
                    ord_data_abertura: $('#txtord_data_abertura_Novo').val()
                };
                $.ajax({
                    url: "/OrdemServico/OrdemServico_Inserir_Novo",
                    data: JSON.stringify(params),
                    type: "POST",
                    contentType: "application/json;charset=utf-8",
                    dataType: "json",
                    success: function (result) {

                        // recarrega o grid
                        selectedId_ord_id = result;

                        carregaGridOS(selectedId_ord_id);

                        // preenche aba detalhes O.S.
                        OrdemServico_PreencheDetalhes(selectedId_ord_id);

                        //desabilita os controles da tab Detalhes
                        OrdemServico_setaReadWrite(divDetalhes, true);


                        //preenchetblFicha_Inspecao_Cadastral(selectedobj_id, 3, -1); // nao precisa pois isso é feito ao selecionar o objeto

                        //desabilita os controles da tab ficha e habilita a aba para edicao
                        var tblFicha_Inspecao_Cadastral_DADOS_GERAIS = document.getElementById("tblFicha_Inspecao_Cadastral_DADOS_GERAIS");
                        //EditarDados_FichaInspecaoCadastral(tblFicha_Inspecao_Cadastral_DADOS_GERAIS);

                        ////abre posiciona (scroll)
                        //gotoHash("#tabDetalhesOSheader");

                        // mostra e abre a Aba da ficha pertinente 
                        mostraAba(sel_tos_id, true);

                        switch (parseInt(sel_tos_id))
                        {
                            case 7: abreFicha(1); break;  // inspecao cadastral   // abreFicha já tem preencheficha embutido: preenchetblFicha(selectedId_obj_id, -1, 7);
                            case 8: abreFicha(3); break;  // inspecao rotineira
                            case 9: abreFicha(4); break;  // inspecao especial
                            case 18: abreFicha(6); break;  // notificacao ocorrencia
                            default:
                                //abre posiciona (scroll)
                                gotoHash("#tabDetalhesOSheader");

                        }

                        // fecha o modal
                        $("#modalSalvarRegistro").modal('hide');

                        return false;
                    },
                    error: function (errormessage) {
                        alert(errormessage.responseText);
                        return false;
                    }
                });
            }


            // recarrega o grid
            selectedId_ord_id = result;

            return false;
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
            return false;
        }
    });


    return false;
}

function cmbTiposOS_Novo_onchange()
{
    $('#cmbTiposOS_Novo').css('background-color', corBranca);
    var cmbTiposOS_Novo = document.getElementById("cmbTiposOS_Novo"); 
    if (cmbTiposOS_Novo.selectedIndex > 0) {
        var selectedVal = cmbTiposOS_Novo.options[cmbTiposOS_Novo.selectedIndex].value;

        $.ajax({
            url: '/OrdemServico/OrdemServico_ProximoCodigo',
            type: "POST",
            dataType: "JSON",
            data: { tos_id: selectedVal },
            success: function (codigo) {
                $("#txtord_codigo_Novo").val(codigo.data);
                return false;
            }
        });




    }
    else
        $("#txtord_codigo_Novo").val("");
}


// *****************************************************************

function mostraAba(selectedId_tos_id, bool_posicionar) {

    var liDetalhesOS = document.getElementById("liDetalhesOS");
    var liFichaInspecaoCadastral = document.getElementById("liFichaInspecaoCadastral");
    var liFichaInspecao1aRotineira = document.getElementById("liFichaInspecao1aRotineira");
    var liFichaInspecaoRotineira = document.getElementById("liFichaInspecaoRotineira");
    var liFichaInspecaoRotineiraProvidencias = document.getElementById("liFichaInspecaoRotineiraProvidencias");

    var liFichaInspecaoEspecial = document.getElementById("liFichaInspecaoEspecial");
    var liFichaInspecaoEspecialAnomalias = document.getElementById("liFichaInspecaoEspecialAnomalias");
    var liFichaInspecaoEspecialAnomaliasProvidencias = document.getElementById("liFichaInspecaoEspecialAnomaliasProvidencias");

    var liFichaNotificacaoOcorrencia = document.getElementById("liFichaNotificacaoOcorrencia");

    var liIndicacaoServicos = document.getElementById("liIndicacaoServicos");

    var liFichaOrcamento = document.getElementById("liFichaOrcamento");
    var liDocumentosAssociadosOBJ = document.getElementById("liDocumentosAssociadosOBJ");
    var liDocumentosAssociadosOS = document.getElementById("liDocumentosAssociadosOS");


    //if (liDetalhesOS)
    //    $('[href="#tabDetalhesOS"]').tab('show');

    if (liFichaInspecaoCadastral)
        liFichaInspecaoCadastral.style.display = "none";
    if (liFichaInspecao1aRotineira)
        liFichaInspecao1aRotineira.style.display = "none";

    if (liFichaInspecaoRotineira)
        liFichaInspecaoRotineira.style.display = "none";

    if (liFichaInspecaoRotineiraProvidencias)
        liFichaInspecaoRotineiraProvidencias.style.display = "none";

    if (liFichaInspecaoEspecial)
        liFichaInspecaoEspecial.style.display = "none";

    if (liFichaInspecaoEspecialAnomalias)
        liFichaInspecaoEspecialAnomalias.style.display = "none";

    if (liFichaInspecaoEspecialAnomaliasProvidencias)
        liFichaInspecaoEspecialAnomaliasProvidencias.style.display = "none";


    if (liFichaNotificacaoOcorrencia)
        liFichaNotificacaoOcorrencia.style.display = "none";

    if (liIndicacaoServicos)
        liIndicacaoServicos.style.display = "none";

    if (liFichaOrcamento)
        liFichaOrcamento.style.display = "none";


    switch (parseInt(selectedId_tos_id)) {
        case 7:
            liFichaInspecaoCadastral.style.display = "unset";
            liFichaInspecao1aRotineira.style.display = "unset";
            liFichaInspecaoRotineiraProvidencias.style.display = "unset";

            $("#tabFichaInspecaoCadastralheader").val("Ficha Básica - Inspeção Cadastral");
            $("#lblModalHeaderAbaCadastral").text("FICHA BÁSICA PARA EXECUÇÃO DA INSPEÇÃO CADASTRAL");

            if (bool_posicionar)
                $('[href="#tabFichaInspecaoCadastral"]').tab('show');
            break;

        case 8:
            liFichaInspecaoCadastral.style.display = "unset";
            $("#tabFichaInspecaoCadastralheader").text("Ficha Básica de Cadastro");
            $("#lblModalHeaderAbaCadastral").text("FICHA BÁSICA DE CADASTRO");

            liFichaInspecaoRotineira.style.display = "unset";
            liFichaInspecaoRotineiraProvidencias.style.display = "unset";
            if (bool_posicionar)
                $('[href="#tabFichaInspecaoRotineira"]').tab('show');
            break;

        case 9:
            liFichaInspecaoEspecial.style.display = "unset";

            liFichaInspecaoEspecialAnomalias.style.display = "unset";
             if (bool_posicionar)
                $('[href="#tabFichaInspecaoEspecial"]').tab('show');

            liFichaInspecaoEspecialAnomaliasProvidencias.style.display = "unset";
            if (bool_posicionar)
                $('[href="#tabFichaInspecaoEspecialProvidencias"]').tab('show');

            break;

        case 11: // orcamento
            liFichaOrcamento.style.display = "unset";
            if (liDocumentosAssociadosOBJ)
                liDocumentosAssociadosOBJ.style.display = "none";

            if (liDocumentosAssociadosOS)
                liDocumentosAssociadosOS.style.display = "none";

            break;

        case 18:
            liFichaNotificacaoOcorrencia.style.display = "unset";
            liIndicacaoServicos.style.display = "unset";
             if (bool_posicionar)
                 $('[href="#tabFichaNotificacaoOcorrencia"]').tab('show');
            break;

        case 24: // projeto obra nova
            liFichaInspecaoCadastral.style.display = "unset";
            $("#tabFichaInspecaoCadastralheader").text("Ficha Básica de Cadastro");
            $("#lblModalHeaderAbaCadastral").text("FICHA BÁSICA DE CADASTRO");

            liIndicacaoServicos.style.display = "unset";
            break;

        default:
            liIndicacaoServicos.style.display = "unset";
            $('[href="#tabDetalhesOS"]').tab('show');

    }

}


function travaBotoes()
{
    var controle_none = ["btnSalvar_Detalhes", "btnCancelar_Detalhes",
                            "btn_Editar_NOTIFICACAO_OCORRENCIA", "btn_Editar_DADOS_GERAIS2", "btn_Editar_HISTORICO_INSPECOES", "btn_Editar_INSPECAO_ROTINEIRA",
                            "btn_Editar_CRITERIO_DE_CLASSIFICACAO", "btn_Editar_NOTA_OAE_PARAMETRO_FUNCIONAL", "btn_Editar_POLITICA_ACOES_A_IMPLEMENTAR",
                            "btn_Editar_INSPECAO_ESPECIAL_CAMPO", "btn_Editar_DADOS_GERAIS", "btn_Editar_HISTORICO_INSPECOES",
                            "btn_Editar_ATRIBUTOS_FUNCIONAIS", "btn_Editar_ATRIBUTOS_FIXOS", "btn_Editar_SUPERESTRUTURA", "btn_Editar_MESOESTRUTURA",
                            "btn_Editar_INFRAESTRUTURA", "btn_Editar_ENCONTROS", "btn_Editar_CRITERIO_DE_CLASSIFICACAO", "btn_Editar_NOTA_OAE_PARAMETRO_FUNCIONAL",
                            "btn_Editar_POLITICA_ACOES_A_IMPLEMENTAR", "btn_Editar_DADOS_GERAIS", "btn_Editar_ATRIBUTOS_FUNCIONAIS", "btn_Editar_ATRIBUTOS_FIXOS",
                            "btn_Editar_SUPERESTRUTURA", "btn_Editar_MESOESTRUTURA", "btn_Editar_INFRAESTRUTURA", "btn_Editar_ENCONTROS", "btn_Editar_HISTORICO_INTERVENCOES",
                            "btnAssociarDocumentoOS", "btnEditar_IndicacaoServicos",
                            "btn_Cancelar_NOTIFICACAO_OCORRENCIA", "btn_Cancelar_DADOS_GERAIS2", "btn_Cancelar_HISTORICO_INSPECOES", "btn_Cancelar_INSPECAO_ROTINEIRA",
                            "btn_Cancelar_CRITERIO_DE_CLASSIFICACAO", "btn_Cancelar_NOTA_OAE_PARAMETRO_FUNCIONAL", "btn_Cancelar_POLITICA_ACOES_A_IMPLEMENTAR",
                            "btn_Cancelar_INSPECAO_ESPECIAL_CAMPO", "btn_Cancelar_INSPECAO_ESPECIAL_CAMPO", "btn_Cancelar_DADOS_GERAIS", "btn_Cancelar_HISTORICO_INSPECOES",
                            "btn_Cancelar_ATRIBUTOS_FUNCIONAIS", "btn_Cancelar_ATRIBUTOS_FIXOS", "btn_Cancelar_SUPERESTRUTURA", "btn_Cancelar_MESOESTRUTURA",
                            "btn_Cancelar_INFRAESTRUTURA", "btn_Cancelar_ENCONTROS", "btn_Cancelar_CRITERIO_DE_CLASSIFICACAO", "btn_Cancelar_NOTA_OAE_PARAMETRO_FUNCIONAL",
                            "btn_Cancelar_POLITICA_ACOES_A_IMPLEMENTAR", "btn_Cancelar_DADOS_GERAIS", "btn_Cancelar_ATRIBUTOS_FUNCIONAIS", "btn_Cancelar_ATRIBUTOS_FIXOS",
                            "btn_Cancelar_SUPERESTRUTURA", "btn_Cancelar_MESOESTRUTURA", "btn_Cancelar_INFRAESTRUTURA", "btn_Cancelar_ENCONTROS", "btn_Cancelar_HISTORICO_INTERVENCOES",
                            "btn_Salvar_NOTIFICACAO_OCORRENCIA", "btn_Salvar_DADOS_GERAIS2", "btn_Salvar_HISTORICO_INSPECOES", "btn_Salvar_INSPECAO_ROTINEIRA",
                            "btn_Salvar_CRITERIO_DE_CLASSIFICACAO", "btn_Salvar_NOTA_OAE_PARAMETRO_FUNCIONAL", "btn_Salvar_POLITICA_ACOES_A_IMPLEMENTAR",
                            "btn_Salvar_INSPECAO_ESPECIAL_CAMPO", "btn_Salvar_INSPECAO_ESPECIAL_CAMPO", "btn_Salvar_DADOS_GERAIS", "btn_Salvar_HISTORICO_INSPECOES",
                            "btn_Salvar_ATRIBUTOS_FUNCIONAIS", "btn_Salvar_ATRIBUTOS_FIXOS", "btn_Salvar_SUPERESTRUTURA", "btn_Salvar_MESOESTRUTURA", "btn_Salvar_INFRAESTRUTURA",
                            "btn_Salvar_ENCONTROS", "btn_Salvar_CRITERIO_DE_CLASSIFICACAO", "btn_Salvar_NOTA_OAE_PARAMETRO_FUNCIONAL", "btn_Salvar_POLITICA_ACOES_A_IMPLEMENTAR",
                            "btn_Salvar_DADOS_GERAIS", "btn_Salvar_ATRIBUTOS_FUNCIONAIS", "btn_Salvar_ATRIBUTOS_FIXOS", "btn_Salvar_SUPERESTRUTURA", "btn_Salvar_MESOESTRUTURA",
                            "btn_Salvar_INFRAESTRUTURA", "btn_Salvar_ENCONTROS", "btn_Salvar_HISTORICO_INTERVENCOES", "btnSalvar_IndicacaoServicos", "btnCancelar_IndicacaoServicos"
                          ];

    var controles_block = ["btnEditar_Detalhes"];


    var cmbStatusOS = $('#cmbStatusOS').val();
    if (parseInt(cmbStatusOS) == 14) // encerrada
    {
        // oculta os botoes 
        for (var i=0; i < controle_none.length; i++)
        {
            var btn = document.getElementById(controle_none[i]);
            if (btn)
                btn.style.display = 'none';
        }

        // mostra os botoes permitidos 
        for (var i = 0; i < controles_block.length; i++) {
            var btn = document.getElementById(controles_block[i]);
            if (btn)
                btn.style.display = 'block';
        }
    }


}


function OrdemServico_setaReadWrite(ehRead) {
    var ehEncerrada = false;
    var ehReadOrig = ehRead;

    var cmbStatusOS = $('#cmbStatusOS').val();
    if (parseInt(cmbStatusOS) == 14) // encerrada
    {
        ////document.getElementById("cmbStatusOS").disabled = ehRead;
        ehRead = true;
        ehEncerrada = true;
    }

    var tabela = document.getElementById("divDetalhes");

    // habilita ou desabilita todos os controles editaveis
    var lstTxtBoxes = tabela.getElementsByTagName('input');
    var lstCombos = tabela.getElementsByTagName('select');
    var lstTextareas = tabela.getElementsByTagName('textarea');
    var lstButtons = tabela.getElementsByTagName('button');

    var controlesReadOnly = ["txtord_codigo", "txtobj_codigo_Novo2", "btnAbrirLocalizarObjetos", "txtord_criticidade", "txtord_data_atualizacao_status", "txtord_data_abertura"];
    if (ehEdicao)
        controlesReadOnly.push("cmbTiposOS");

    // textboxes
    for (var i = 0; i < lstTxtBoxes.length; i++) {

        if ((lstTxtBoxes[i].readOnly) && (!lstTxtBoxes[i].disabled))
        {
            if (!lstTxtBoxes[i].classList.contains('backgrdTransparent'))
                lstTxtBoxes[i].classList.add("backgrdTransparent");
        }
        else
           lstTxtBoxes[i].classList.remove("backgrdTransparent");

        if (lstTxtBoxes[i].style.backgroundColor == "rgb(255, 255, 255)")
            lstTxtBoxes[i].style.backgroundColor = "";

        if (!controlesReadOnly.includes(lstTxtBoxes[i].id))
            lstTxtBoxes[i].disabled = ehRead;
    }

    // textareas
    for (var i = 0; i < lstTextareas.length; i++)
        lstTextareas[i].disabled = ehRead;

    // comboboxes
    for (var i = 0; i < lstCombos.length; i++)
     if (!controlesReadOnly.includes(lstCombos[i].id))
        lstCombos[i].disabled = ehRead;

    // buttons
    for (var i = 0; i < lstButtons.length; i++)
        if (!controlesReadOnly.includes(lstButtons[i].id))
        lstButtons[i].disabled = ehRead;

    
    if ((ehRead) && (!ehEncerrada))
    {
        ehEdicao = false;
        //document.getElementById("btnNovo_Detalhes").style.display = "inline";
        document.getElementById("btnEditar_Detalhes").style.display = "inline";
        document.getElementById("btnSalvar_Detalhes").style.display = "none";
        document.getElementById("btnCancelar_Detalhes").style.display = "none";
    }
    else
    {
        document.getElementById("lblModalHeader").innerText = "Editar Ordem de Serviço: " + $('#txtord_codigo').val().trim();
    }


    document.getElementById("cmbStatusOS").disabled = ehReadOrig;
}

function OrdemServico_LimparDetalhes()
{
    $('#lblModalHeader').text("");

    $('#txtord_id').val("");
    $('#txtord_codigo').val("");
    $('#txtord_descricao').val("");
    $('#cmbTiposOS').val(null);

    $('#txtobj_codigo').val("");
    $('#txtobj_codigo_Novo2').val("");

    $('#txtord_aberta_por').val("");
    $('#txtord_responsavel_der').val("");
    $('#txtord_responsavel_fiscalizacao').val("");
    $('#txtord_responsavel_execucao').val("");

    $('#txtord_data_inicio_programada').val("");
    $('#txtord_data_inicio_execucao').val("");
    $('#txtord_data_suspensao').val("");
    $('#txtord_data_cancelamento').val("");
    $('#txtord_data_reinicio').val("");

    $('#txtord_criticidade').val("");


  // $('#txtord_quantidade_estimada').val("");
    $('#txtord_custo_estimado').val("");

  //  $('#txttpu_codigo_der').val("");
  //  $('#txttpu_descricao').val("");
  //  $('#txttpu_data_base_der').val("");

    $('#cmbClassesOS').val(null);
    $('#txtord_codigo_pai').val("");
    $('#txtord_data_abertura').val("");
    $('#txtcon_codigofiscalizacao').val("");
    $('#txtcon_codigoexecucao').val("");

    $('#txtord_data_termino_programada').val("");
    $('#txtord_data_termino_execucao').val("");
    $('#txtord_responsavel_suspensao').val("");
    $('#txtord_responsavel_cancelamento').val("");

    $('#cmbStatusOS').val(null);

  //  $('#txtord_quantidade_executada').val("");
    $('#txtord_custo_final').val("");

    $('#chkord_ativo').prop('checked', true);

    mostraAba(-1, false);
}

function OrdemServico_Inserir() {

    // limpa os campos
    OrdemServico_LimparDetalhes();

    // habilita
    OrdemServico_setaReadWrite(false);

    //document.getElementById("btnNovo_Detalhes").style.display = 'none';
    document.getElementById("btnEditar_Detalhes").style.display = 'none';
    document.getElementById("btnSalvar_Detalhes").style.display = 'inline';
    document.getElementById("btnCancelar_Detalhes").style.display = 'inline';

    document.getElementById("lblModalHeader").innerText = "Nova Ordem de Serviço";

    selectedord_id = -1;
}
function OrdemServico_Cancelar()
{
    ehEdicao = false;

    // recarrega e desabilita os controles
    OrdemServico_PreencheDetalhes(selectedId_ord_id);

    //document.getElementById("btnNovo_Detalhes").style.display = "inline";
    document.getElementById("btnEditar_Detalhes").style.display = "inline";
    document.getElementById("btnSalvar_Detalhes").style.display = "none";
    document.getElementById("btnCancelar_Detalhes").style.display = "none";

}

// usando esta funcao para Edicao e bntSalvar_Novo_onclick para insercao
function OrdemServico_Salvar() {

    var txtord_descricao = document.getElementById('txtord_descricao');
    txtord_descricao.value = txtord_descricao.value.trim();

    if (ChecaRepetido(txtord_codigo) && validaAlfaNumericoAcentosAfins(txtord_descricao)) {

        var OrdemServico = {
            ord_id: selectedId_ord_id,
            ord_codigo: $('#txtord_codigo').val().trim(),
            ord_descricao: $('#txtord_descricao').val().trim(),
            ord_pai: $('#txtord_pai').val(),
            ocl_id: $('#cmbClassesOS').val(),
            tos_id: $('#cmbTiposOS').val(),
            sos_id: $('#cmbStatusOS').val(),
            obj_id: selectedId_obj_id,
            ord_ativo: 1,
            ord_criticidade: $('#txtord_criticidade').val().trim(),
            con_id: $('#txtcon_id').val(),
            ord_data_inicio_programada: $('#txtord_data_inicio_programada').val(),
            ord_data_termino_programada: $('#txtord_data_termino_programada').val(),
            ord_data_inicio_execucao: $('#txtord_data_inicio_execucao').val(),
            ord_data_termino_execucao: $('#txtord_data_termino_execucao').val(),
          //  ord_quantidade_estimada: $('#txtord_quantidade_estimada').val(),
            uni_id_qt_estimada: $('#txtuni_id_qt_estimada').val(),
         //   ord_quantidade_executada: $('#txtord_quantidade_executada').val(),
            uni_id_qt_executada: $('#txtuni_id_qt_executada').val(),
            ord_custo_estimado: $('#txtord_custo_estimado').val(),
            ord_custo_final: $('#txtord_custo_final').val(),
            ord_aberta_por: $('#txtord_aberta_por').val(),
            ord_data_abertura: $('#txtord_data_abertura').val(),
            ord_responsavel_der: $('#txtord_responsavel_der').val().trim(),
            ord_responsavel_fiscalizacao: $('#txtord_responsavel_fiscalizacao').val().trim(),
            con_id_fiscalizacao: $('#txtcon_id_fiscalizacao').val(),
            ord_responsavel_execucao: $('#txtord_responsavel_execucao').val().trim(),
            con_id_execucao: $('#txtcon_id_execucao').val(),
            ord_responsavel_suspensao: $('#txtord_responsavel_suspensao').val().trim(),
            ord_data_suspensao: $('#txtord_data_suspensao').val(),
            ord_responsavel_cancelamento: $('#txtord_responsavel_cancelamento').val().trim(),
            ord_data_cancelamento: $('#txtord_data_cancelamento').val(),
            ord_data_reinicio: $('#txtord_data_reinicio').val(),
            con_id_orcamento: $('#txtcon_id_orcamento').val(),
            tpt_id: $('#txttpt_id').val(),
            tpu_data_base_der: $('#txttpu_data_base_der').val(),
            tpu_id: $('#txttpu_id').val(),
            tpu_preco_unitario: $('#txttpu_preco_unitario').val()
        };

        $.ajax({
            url: "/OrdemServico/OrdemServico_Salvar",
            data: JSON.stringify(OrdemServico),
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                // preenche as caixas de filtro
              //  filtroOrdemServico_codigo = $('#txtord_codigo').val().trim(); // codigo sem as tabulacoes
                $('#txtFiltroOrdemServico_codigo').val($('#txtord_codigo').val().trim());
                ////$('#txtFiltroObj_codigo').val(filtroObj_codigo);
                $("#cmbFiltroTiposOS").val($('#cmbTiposOS').val());
                $("#cmbFiltroStatusOS").val($('#cmbStatusOS').val());
                $('#txtfiltroord_data_De').val("");
                $('#txtfiltroord_data_Ate').val("");

                selectedId_sos_id = $('#cmbStatusOS').val();

                carregaGridOS(selectedId_ord_id);
              //  $('#tblOrdemServicos').DataTable().ajax.reload(null, false);  //false = sem reload na pagina.

                //desabilita os controles
                OrdemServico_setaReadWrite(divDetalhes, true);

                return false;
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
                return false;
            }
        });
    }
    else {
        //$("#modalSalvarRegistro").modal('show');
        return false;
    }
    return false;
}
function OrdemServico_Editar(id, origem) {
    event.preventDefault(); // cancel default behavior
    //document.getElementById("btnNovo_Detalhes").style.display = "none";
    document.getElementById("btnEditar_Detalhes").style.display = "none";
    document.getElementById("btnSalvar_Detalhes").style.display = "inline";
    document.getElementById("btnCancelar_Detalhes").style.display = "inline";

    // segue para o evento row selected 
    if (parseInt(selectedId_ord_id) != parseInt(id))
        selectedId_ord_id = parseInt(id);

    ehEdicao = true;
    if (origem == 'btnEditar_Detalhes')
        OrdemServico_PreencheDetalhes(selectedId_ord_id);
    return false;
}

 function OrdemServico_PreencheDetalhes(id) {
    event.preventDefault(); // cancel default behavior

    var corBranca = "rgb(255, 255, 255)";
    $('#txtord_descricao').css("background-color", corBranca);

    $('#txtord_descricao').css('border-color', 'lightgrey');
    $.ajax({
        url: "/OrdemServico/OrdemServico_GetbyID/" + id,
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
//
          //  $('#txtord_id').val(result.);
            $('#txtord_codigo').val(result.ord_codigo);
            $('#txtord_descricao').val(result.ord_descricao);

            $('#cmbTiposOS').val(result.tos_id);
            selectedId_obj_id = result.obj_id;
            $('#txtobj_codigo').val(result.obj_codigo.trim());
            $('#txtobj_codigo_Novo2').val(result.obj_codigo.trim());
            $('#txtord_data_atualizacao_status').val(result.ord_data_atualizacao_status);

            $('#txtord_aberta_por').val(result.ord_aberta_por_nome.trim());
            $('#txtord_responsavel_der').val(result.ord_responsavel_der.trim());
            $('#txtord_responsavel_fiscalizacao').val(result.ord_responsavel_fiscalizacao.trim());
            $('#txtord_responsavel_execucao').val(result.ord_responsavel_execucao.trim());

            $('#txtord_data_inicio_programada').val(result.ord_data_inicio_programada.substring(0,10));
            $('#txtord_data_inicio_execucao').val(result.ord_data_inicio_execucao.substring(0, 10));
            $('#txtord_data_suspensao').val(result.ord_data_suspensao.substring(0, 10));
            $('#txtord_data_cancelamento').val(result.ord_data_cancelamento.substring(0, 10));
            $('#txtord_data_reinicio').val(result.ord_data_reinicio.substring(0, 10));

            $('#txtord_criticidade').val( parseFloat(result.ord_criticidade).toFixed(2));

        //    $('#txtord_quantidade_estimada').val(result.ord_quantidade_estimada);
            $('#txtord_custo_estimado').val(result.ord_custo_estimado);

        //    $('#txttpu_codigo_der').val(result.tpu_codigo_der.trim());
        //    $('#txttpu_descricao').val(result.tpu_descricao.trim());
        //    $('#txttpu_data_base_der').val(result.tpu_data_base_der.substring(0, 10));

            $('#cmbClassesOS').val(result.ocl_id);

            $('#txtord_codigo_pai').val(result.ord_codigo_pai);
            $('#txtord_data_abertura').val(result.ord_data_abertura.substring(0, 10));
            $('#txtcon_codigofiscalizacao').val(result.con_codigofiscalizacao.trim());
            $('#txtcon_codigoexecucao').val(result.con_codigoexecucao.trim());

            $('#txtord_data_termino_programada').val(result.ord_data_termino_programada.substring(0, 10));
            $('#txtord_data_termino_execucao').val(result.ord_data_termino_execucao.substring(0, 10));
            $('#txtord_responsavel_suspensao').val(result.ord_responsavel_suspensao.trim());
            $('#txtord_responsavel_cancelamento').val(result.ord_responsavel_cancelamento.trim());
            $('#txt_IndicacaoServico').val(result.ord_indicacao_servico.trim());

            // preenche o combo de Status
            var cmbStatusOS = document.getElementById("cmbStatusOS");
            if (cmbStatusOS) {
                $("#cmbStatusOS").html(""); // limpa os itens existentes

                var lst_proximos_status = result.lst_proximos_status.split(";");
                for (var j = 0; j < lst_proximos_status.length; j++) {
                    var pedacos = lst_proximos_status[j].split(":");
                    $("#cmbStatusOS").append($('<option></option>').val(parseInt(pedacos[0])).html(pedacos[1]));
                }

            }

            $('#cmbStatusOS').val(result.sos_id);

        //    $('#txtord_quantidade_executada').val(result.ord_quantidade_executada);
            $('#txtord_custo_final').val(result.ord_custo_final);

            $('#chkord_ativo').prop('checked', true);

            document.getElementById("lblModalHeader").innerText = "Ordem de Serviço: " + result.ord_codigo;

            //Desabilita/habilita os controles
            OrdemServico_setaReadWrite(!ehEdicao);

            if (ehEdicao == true)
                gotoHash("#tabDetalhesOSheader");

            ehEdicao = false;
            return false;
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
    return false;
}

// posiciona a pagina
function gotoHash(newHash) {
    location.hash = newHash;
    //location.hash = 'someHashThatDoesntExist';
}

function OrdemServico_Excluir(id) {
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
            var response = POST("/OrdemServico/OrdemServico_Excluir", JSON.stringify({ id: id }))
            if (response.erroId >= 1) {
                swal({
                    type: 'success',
                    title: 'Sucesso',
                    text: 'Registro excluído com sucesso'
                });

                LimparFiltro();
                OrdemServico_LimparDetalhes();
                //limpatblFicha_Inspecao_Cadastral();
              //  $('#tblOrdemServicos').DataTable().ajax.reload();

            }
            else {
                swal({
                    type: 'error',
                    title: 'Aviso',
                    text: 'Erro ao excluir registro'
                });
            }
            return true;
        } else {
            return false;
        }
    })



    return false;
}
function OrdemServico_AtivarDesativar(id, ativar) {

    if (id >= 0) {
        var form = this;
        swal({
            title: (ativar == 1 ? "Ativar" : "Desativar") + ". Tem certeza?",
            icon: "warning",
            buttons: [
                'Não',
                'Sim'
            ],
            dangerMode: true,
            focusCancel: true
        }).then(function (isConfirm) {
            if (isConfirm) {
                var response = POST("/OrdemServico/OrdemServico_AtivarDesativar", JSON.stringify({ id: id }))
                if (response.erroId == 1) {
                    swal({
                        type: 'success',
                        title: 'Sucesso',
                        text: ativar == 1 ? msgAtivacaoOK : msgDesativacaoOK
                    });

                    $('#tblOrdemServicos').DataTable().ajax.reload();
                }
                else {
                    swal({
                        type: 'error',
                        title: 'Aviso',
                        text: ativar == 1 ? msgAtivacaoErro : msgDesativacaoErro
                    });
                }
                return true;
            } else {
                return false;
            }
        })
    }

    return false;
}

function ChecaRepetido(txtBox) {
    txtBox.value = txtBox.value.trim();

    if (validaAlfaNumerico(txtBox)) {
        var corVermelho = "rgb(228, 88, 71)";
        var corBranca = "rgb(255, 255, 255)";
        var searchValue = '\\b' + txtBox.value + '\\b';
        var rowId = $('#tblOrdemServicos').DataTable().column(1).search(searchValue, true, false).rows({ filter: 'applied' }).data();
        if (rowId.length > 0) { // ja tem
            if (selectedord_id != rowId[0]["ord_id"]) {
                $('#txtord_descricao').css("background-color", corVermelho);
                swal({
                    type: 'error',
                    title: 'Aviso',
                    text: 'Tipo já cadastrado'
                }).then(
                    function () {
                        return false;
                    });
            }
            else {
                $('#tblOrdemServicos').DataTable().search('').columns().search('').draw();
                return true;
            }
        }
        else { // nao tem
            $('#txtord_descricao').css("background-color", corBranca);
            $('#tblOrdemServicos').DataTable().search('').columns().search('').draw();
            return true;
        }
    }
    else {
        $("#modalSalvarRegistro").modal('show');
        return false;
    }

}

function LimparFiltro() {

    $('#txtFiltroOrdemServico_codigo').val('');
    $('#txtFiltroObj_codigo').val('');
    $('#filtroord_data_De').val('');
    $('#filtroord_data_Ate').val('');

    $("#cmbFiltroTiposOS").val(null);
    $("#cmbFiltroStatusOS").val(null);

    $("#cmbDatas").val('');

    filtroRow_numeroMin = 0;
    filtroRow_numeroMax = 1000;

    selectedId_ord_id = 0;
    selectedId_ord_pai = -1;

    selectedId_tos_id = 0;
    selectedId_sos_id = 0;

    filtroOrdemServico_codigo = '';
    filtroObj_codigo = '';
    filtroTiposOS = -1;
    filtroStatusOS = -1;
    filtroord_data_De = '';
    filtroord_data_Ate = '';

    carregaGridOS(0);

    OrdemServico_LimparDetalhes();

    $('[href="#tabDetalhesOS"]').tab('show');

    return false;
}
function ExecutarFiltro() {

    filtroRow_numeroMin = 0;
    filtroRow_numeroMax = 1000;

    selectedId_ord_id = 0;
    filtroOrdemServico_codigo = $('#txtFiltroOrdemServico_codigo').val().trim();
    filtroObj_codigo = $('#txtFiltroObj_codigo').val().trim();
    filtroTiposOS = $("#cmbFiltroTiposOS").val() == "" ? -1 : $("#cmbFiltroTiposOS").val();
    filtroStatusOS = $("#cmbFiltroStatusOS").val() == "" ? -1 : $("#cmbFiltroStatusOS").val();
    filtroord_data_De = $('#filtroord_data_De').val();
    filtroord_data_Ate = $('#filtroord_data_Ate').val();

    carregaGridOS(selectedId_ord_id);

}

// ********* ORCAMENTO ******************************************

function preenchecmbAno(quem) {
    var anoAtual = new Date().getFullYear();
    var cmbAno = quem.get(0);
 
    // se ja tem nao precisa preencher novamente
    if (cmbAno.options.length > 1)
        return false;

    var idx = 0;
    for (var i = 2040; i >= 2001; i--) {
        var opt = document.createElement("option");
        opt.value = i;
        opt.text = i;
        cmbAno.add(opt);

        if (i == anoAtual)
            cmbAno.selectedIndex = idx + 1;

        idx = idx + 1;
    }

}


function calculaTotal(orc_id)
{
    $.ajax({
        url: "/Orcamento/Orcamento_Total",
        data: JSON.stringify({ "orc_id": orc_id }),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var total = parseFloat(result.data);
            $("#txtorc_valor_total").val(new Intl.NumberFormat('pt-BR', { maximumFractionDigits: 2, minimumFractionDigits: 2 }).format(total));
            $("#txtorc_valor_total").attr('title', (new Intl.NumberFormat('pt-BR', { maximumFractionDigits: 2, minimumFractionDigits: 2 }).format(total)));

            return false;
        },
        error: function (errormessage) {
            swal({
                type: 'error',
                title: 'Aviso',
                text: errormessage.responseText
            }).then(
                function () {
                    return false;
                });
            return false;
        }
    });

}

function carregaGridOrcamentoDetalhes(orc_id, tos_id)
{
    totalParcial_Reparos = 0;

    orc_valor_total = 0;

    // limpa os campos antes de preencher
    $("#txtorc_codigo_Detalhes").val("");
    $("#txtorc_versaoDetalhes").val("");
    $("#txtorc_descricao_Detalhes").val("");
    $("#txtobj_codigoDetalhes").val("");
    $("#txtorc_data_Validade_Detalhes").val("");
    $("#txtorc_valor_total").val("");
    $('#cmbStatusOrcamentoDetalhes').val("");

    // CARREGA OS DADOS DO GRID MAIS DA TABELA HEADER COM OS DADOS DA 1A LINHA
    if ($.fn.dataTable.isDataTable("#tblOrcamentoDetalhes")) {
        $("#tblOrcamentoDetalhes").DataTable().destroy();
    }

    $("#tblOrcamentoDetalhes").DataTable({
        "ajax": {
            "url": "/Orcamento/OrcamentoDetalhes_ListAll",
            "type": "GET",
            "datatype": "json",
            "data": function (d) {
                d.orc_id = orc_id;
                d.ore_ativo = 1;
            }
        }
        , "columns": [
            { data: "ian_ordem_apresentacao", "className": "hide_column" },
            { data: "ore_id", "className": "hide_column" },
            { data: "orc_id", "className": "hide_column" },
            { data: "orc_cod_orcamento", "className": "hide_column" },
            { data: "orc_descricao", "className": "hide_column" },
            { data: "orc_versao", "className": "hide_column" },
            { data: "ocs_id", "className": "hide_column" },
            { data: "ocs_codigo", "className": "hide_column" },
            { data: "ocs_descricao", "className": "hide_column" },
            { data: "orc_data_validade", "className": "hide_column" },
            { data: "orc_valor_total", "className": "hide_column" },

            { data: "orc_id_pai", "className": "hide_column" },
            { data: "obj_codigoOAE", "className": "Centralizado", "autoWidth": true, "searchable": false },
            { data: "obj_codigoElemento", "className": "Centralizado", "autoWidth": true, "searchable": false },
            { data: "ian_numero", "className": "Centralizado", "autoWidth": true, "searchable": false },
            { data: "atp_codigo", "className": "Centralizado", "autoWidth": true, "searchable": false },
            { data: "leg_codigo", "className": "Centralizado", "autoWidth": true, "searchable": false },
            { data: "ale_codigo", "className": "Centralizado", "autoWidth": true, "searchable": false },
            { data: "aca_codigo", "className": "Centralizado", "autoWidth": true, "searchable": false },
            { data: "ian_quantidade", "className": "Centralizado", "autoWidth": true, "searchable": false },
            { data: "rpt_id_sugerido_codigo", "className": "borderLeft Centralizado", "autoWidth": true, "searchable": false },

            { data: "ian_quantidade_sugerida", "className": "Centralizado", "autoWidth": true, "searchable": false },
            { data: "rpt_id_sugerido_unidade", "className": "Centralizado", "autoWidth": true, "searchable": false },
            { data: "rtu_preco_unitario_sugerido", "className": "Centralizado", "autoWidth": true, "searchable": false },
            { data: "rtu_valor_total_sugerido", "className": "Centralizado", "autoWidth": true, "searchable": false },
            { data: "rpt_id_adotado_codigo", "className": "borderLeft Centralizado", "autoWidth": true, "searchable": false },
            { data: "ian_quantidade_adotada", "className": "Centralizado", "autoWidth": true, "searchable": false },
            { data: "rpt_id_adotado_unidade", "className": "Centralizado", "autoWidth": true, "searchable": false },
            { data: "rtu_preco_unitario_adotado", "className": "Centralizado", "autoWidth": true, "searchable": false },
            { data: "rtu_valor_total_adotado", "className": "Centralizado borderRight", "autoWidth": true, "searchable": false },
            { data: "orc_objetos_associados", "className": "hide_column" },

            { data: "pri_ids_associados", "className": "hide_column" },
            { data: "orc_ativo", "className": "hide_column" },
            { data: "orc_obj_ids_associados", "className": "hide_column" },
            {
                data: "ore_id", "searchable": false, "className": "Centralizado", "sortable": false,
                title: tos_id != 11 ? "Reparado?" : "",
                "render": function (data, type, row) {
                    var retorno = "";
                    if (tos_id != 11) {
                        if (permissaoEscrita > 0) {
                            //  if (permissaoEscrita > 0) {
                            if (row.ore_ativo == 1)
                                retorno = '<a href="#" onclick="return OrcamentoDetalhes_AtivarDesativar(' + orc_id + ',' + data + ', 0)" title="Ativo" ><span class="glyphicon glyphicon-ok text-success"></span></a>' + '  ';
                            else
                                retorno = '<a href="#" onclick="return OrcamentoDetalhes_AtivarDesativar(' + orc_id + ',' + data + ', 1)" title="Desativado" ><span class="glyphicon glyphicon-remove text-danger"></span></a>' + '  ';
                        }
                        else
                            retorno = '<span class="glyphicon glyphicon-ok text-success desabilitado" ></span>' + '  ';
                    }

                    return retorno;
                }
            },
            { data: "orc_data_base", "className": "hide_column" },
            { data: "tpt_id", "className": "hide_column" },
            { data: "tpt_descricao", "className": "hide_column" },
            { data: "lstStatusOrcamento", "className": "hide_column" }
        ]
             , "createdRow": function (row, data, dataIndex) {

                 if ((data["obj_codigoOAE"] != "") && (dataIndex > 1)) {
                     for (var i = 12; i <= 37; i++)
                         $(row.childNodes[i]).addClass("borderTop");
                 }

                 // TABELA HEADER COM OS DADOS DA 1A LINHA
                 if ((data["obj_codigoOAE"] != "") && (dataIndex == 0)) {
                     $("#txtorc_codigo_Detalhes").val((row.childNodes[3].innerText));
                     $("#txtorc_versaoDetalhes").val((row.childNodes[5].innerText.trim()));
                     $("#txtorc_descricao_Detalhes").val((row.childNodes[4].innerText));
                     $("#txtobj_codigoDetalhes").val((row.childNodes[30].innerText));
                     $("#txtobj_ids_Detalhes").val((row.childNodes[33].innerText));
                     $("#txtorc_id_Detalhes").val((row.childNodes[2].innerText));
                     $("#txtorc_data_Validade_Detalhes").val((row.childNodes[9].innerText));
                     $('#chkorc_ativoDetalhes').prop('checked', (row.childNodes[32].innerText == '1' ? true : false));

                     $("#txtpri_ids_associados").val(row.childNodes[31].innerText);

                     $('#lblDataBase').text(row.childNodes[35].innerText);
                     //$('#lblDesonerado').text(row.childNodes[37].innerText); // "D" ou "O"

                     var quem = $("#cmbAno");
                     preenchecmbAno(quem);

                     var auxd = row.childNodes[35].innerText.split('/'); // 30/06/2020
                     $('#cmbMes').val(auxd[1]);
                     $('#cmbAno').val(auxd[2]);
                     $('#cmbDesonerado').val(row.childNodes[36].innerText); // "D" ou "O"

                     // preenche o combo de Status
                     var cmbStatusOrcamentoDetalhes = $("#cmbStatusOrcamentoDetalhes")[0]; //document.getElementById("cmbStatusOrcamentoDetalhes");
                     if (cmbStatusOrcamentoDetalhes) {
                         $("#cmbStatusOrcamentoDetalhes").html(""); // limpa os itens existentes

                         var lst_proximos_status = row.childNodes[38].innerText.split(";");
                         for (var j = 0; j < lst_proximos_status.length; j++) {
                             var pedacos = lst_proximos_status[j].split(":");
                             $("#cmbStatusOrcamentoDetalhes").append($('<option></option>').val(parseInt(pedacos[0])).html(pedacos[1]));
                         }

                         $('#cmbStatusOrcamentoDetalhes').val((row.childNodes[6].innerText));
                     }


                 }

                 var valorSugerido = parseFloat(data["rtu_valor_total_sugerido"]);
                 var valorAdotado = parseFloat(data["rtu_valor_total_adotado"]);

                 if (data["ore_ativo"] == 1)
                     totalParcial_Reparos = totalParcial_Reparos + parseFloat(valorAdotado > 0 ? valorAdotado : valorSugerido);

                 $("#txtReparos_valor_total").val(new Intl.NumberFormat('pt-BR', { maximumFractionDigits: 2, minimumFractionDigits: 2 }).format(totalParcial_Reparos));
                 $("#txtReparos_valor_total").attr('title', (new Intl.NumberFormat('pt-BR', { maximumFractionDigits: 2, minimumFractionDigits: 2 }).format(totalParcial_Reparos)));

                 //var Reparos_vtotal = parseFloat($("#txtReparos_valor_total").val().replaceAll(".", "").replace(",", "."));
                 var ServicosAdicionais_vtotal = parseFloat($("#txtServicosAdicionais_valor_total").val().replaceAll(".", "").replace(",", "."));
                 $("#txtorc_valor_total").val(new Intl.NumberFormat('pt-BR', { maximumFractionDigits: 2, minimumFractionDigits: 2 }).format(totalParcial_Reparos + ServicosAdicionais_vtotal));
            }

        , "order": [2, "asc"]
        , "ordering": false
        , 'columnDefs': [
            {
                targets: [12] // tooltip obj_codigoOAE
                , "createdCell": function (td, cellData, rowData, row, col) {
                    $(td).attr('title', rowData["obj_descricaoOAE"]);

                }
            }
            , {
                targets: [13] // tooltip obj_codigoElemento
                , "createdCell": function (td, cellData, rowData, row, col) {
                    $(td).attr('title', rowData["obj_descricaoElemento"]);
                }
            }
            , {
                targets: [15] // tooltip atp_codigo
                , "createdCell": function (td, cellData, rowData, row, col) {
                    $(td).attr('title', rowData["atp_descricao"]);
                }
            }
            , {
                targets: [16] // tooltip ian_sigla = leg_codigo
                , "createdCell": function (td, cellData, rowData, row, col) {
                    $(td).attr('title', rowData["leg_descricao"]);
                }
            }
            , {
                targets: [17] // tooltip ale_codigo
                , "createdCell": function (td, cellData, rowData, row, col) {
                    $(td).attr('title', rowData["ale_descricao"]);
                }
            }
            , {
                targets: [18] // tooltip aca_codigo
                , "createdCell": function (td, cellData, rowData, row, col) {
                    $(td).attr('title', rowData["aca_descricao"]);
                }
            }
            , {
                targets: [20] // tooltip rpt_id_sugerido_codigo
                , "createdCell": function (td, cellData, rowData, row, col) {
                    $(td).attr('title', rowData["rpt_id_sugerido_descricao"]);
                }
            }
            , {
                targets: [21] // quantidade sugerida
                , "render": function (data, type, row) {
                    return new Intl.NumberFormat('pt-BR').format(data);
                }
            }
            , {
                targets: [23] // valor unitario sugerido
                , "render": function (data, type, row) {
                    return new Intl.NumberFormat('pt-BR', { maximumFractionDigits: 2, minimumFractionDigits: 2 }).format(data);
                }
            }
            , {
                targets: [24] // valor total sugerido
                , "render": function (data, type, row) {
                    return new Intl.NumberFormat('pt-BR', { maximumFractionDigits: 2, minimumFractionDigits: 2 }).format(data);
                }
            }
            , {
                targets: [25] // tooltip rpt_id_adotado_codigo
                , "createdCell": function (td, cellData, rowData, row, col) {
                    $(td).attr('title', rowData["rpt_id_adotado_descricao"]);
                }
            }
            , {
                targets: [26] // quantidade adotada
                , "render": function (data, type, row) {
                    return new Intl.NumberFormat('pt-BR').format(data);
                }
            }
            , {
                targets: [28] // valor unitario adotado
                , "render": function (data, type, row) {
                    return new Intl.NumberFormat('pt-BR', { maximumFractionDigits: 2, minimumFractionDigits: 2 }).format(data);
                }
            }
            , {
                targets: [29] // valor total adotado
                , "render": function (data, type, row) {
                    return new Intl.NumberFormat('pt-BR', { maximumFractionDigits: 2, minimumFractionDigits: 2 }).format(data);
                }
            }

        ]
        , "lengthMenu": [[10, 25, 50, 100], [10, 25, 50, 100]]
        , select: { style: 'single' }
        , searching: false
        , "oLanguage": idioma
        , "pagingType": "input"
        , "sDom": '<"top">rt<"bottom"pfli><"clear">'
    });

    var  ehRead = true;
    orc_servicos_valor_total = 0;

    if ($.fn.dataTable.isDataTable('#tblServicosAdicionados')) {
        $('#tblServicosAdicionados').DataTable().destroy();
    }

    totalParcial_ServicosAdicionados = 0;
    var param = { "orc_id": orc_id, "obj_id": 0 };
    $('#tblServicosAdicionados').DataTable({
        "ajax": {
            "url": "/Orcamento/Orcamento_Servicos_Adicionados_ListAll",
            "type": "GET",
            "datatype": "json",
            "data": param
            //"data": function (d) {
            //    d.orc_id = orc_id;
            //    d.obj_id = 0;
            //}
        }
        , "columns": [
            { data: "ose_id", "className": "hide_column" },
            { data: "obj_codigo", "className": "Centralizado", "autoWidth": true, "searchable": false },
            { data: "ose_quantidade", "className": "Centralizado", "autoWidth": true, "searchable": false },
            { data: "UnidMed", "className": "Centralizado", "autoWidth": true, "searchable": false },
            { data: "CodSubItem", "className": "Centralizado", "autoWidth": true, "searchable": false },
            { data: "NomeSubItem", "className": "Centralizado", "autoWidth": true, "searchable": false },
            { data: "DataTpu", "className": "hide_column" },
            { data: "ose_fase", "className": "Centralizado", "autoWidth": true, "searchable": false },
            { data: "PrecoUnit", "className": "Centralizado", "autoWidth": true, "searchable": false },
            { data: "ValorTotal", "className": "Centralizado", "autoWidth": true, "searchable": false },
            {
                data: "ose_id",
                "className": "Centralizado",
                "title":  tos_id != 11 ? "Reparado?" : "",
                "searchable": false,
                "sortable": false,
                "render": function (data, type, row) {
                    var retorno = "";
                    if (tos_id != 11) {
                        if ((permissaoExclusao > 0) && (!ehRead))
                            retorno += '<a href="#" onclick="return ServicosAdicionados_Deletar(' + aba + ',' + orc_id + ',' + data + ')" title="Excluir" ><span class="glyphicon glyphicon-trash"></span></a>';
                        else
                            retorno += '<span class="glyphicon glyphicon-trash desabilitado" title="Excluir" ></span>';
                    }
                    return retorno;
                }
            }
        ]
        , 'columnDefs': [
            {
                'targets': [2],
                'searchable': false,
                'orderable': false,
                'className': 'dt-body-center',
                'render': function (data, type, row) {
                    if (!isNaN(data)) {
                        var idd = 'txtQt_' + row.ose_id; // (row[0]);
                        var qtde = Intl.NumberFormat('pt-BR').format(data);
                        return '<input type="text" name=' + idd + ' id=' + idd + ' disabled title="' + qtde + '" style="text-align:center; width:100px" onfocus="txtQt_onfocus(this)" onkeyup="txtQt_onkeyup(this)"   value="' + qtde + '" >';
                    }
                    else
                        return 0;

                }
            }

            , {
                targets: [8] // PrecoUnit
                , "render": function (data, type, row) {
                    if (!isNaN(data)) {
                        var dado = parseFloat(data);
                        return new Intl.NumberFormat('pt-BR', { maximumFractionDigits: 2, minimumFractionDigits: 2 }).format(dado);
                    }
                    else
                        return 0;

                }
            }
            , {
                targets: [9] // ValorTotal
                , "render": function (data, type, row) {
                    if (!isNaN(data)) {
                        var dado = parseFloat(data);
                        return new Intl.NumberFormat('pt-BR', { maximumFractionDigits: 2, minimumFractionDigits: 2 }).format(dado);
                    }
                    else
                        return 0;
                }
            }

        ]
        , "lengthMenu": [[10, 25, 50, 100], [10, 25, 50, 100]]
        , select: { style: 'single' }
        , searching: false
        , "ordering": false
        , "oLanguage": idioma
        , "pagingType": "input"
        , "sDom": '<"top">rt<"bottom"pfli><"clear">'

        , "createdRow": function (row, data, dataIndex) {
            if (parseInt(data.DataTpu) == -1) {
                swal({
                    type: 'error',
                    title: 'Erro',
                    text: data.CodSubItem
                }).then(
                    function () {
                        return false;
                    });
            }

            if ((data["obj_codigoOAE"] != "") && (dataIndex == 0)) {
                $("#lblDataBase").text((row.childNodes[6].innerText));
            }

            totalParcial_ServicosAdicionados = totalParcial_ServicosAdicionados + parseFloat(data["ValorTotal"]);
            $("#txtServicosAdicionais_valor_total").val(new Intl.NumberFormat('pt-BR', { maximumFractionDigits: 2, minimumFractionDigits: 2 }).format(totalParcial_ServicosAdicionados));
            $("#txtServicosAdicionais_valor_total").attr('title', (new Intl.NumberFormat('pt-BR', { maximumFractionDigits: 2, minimumFractionDigits: 2 }).format(totalParcial_ServicosAdicionados)));

            var Reparos_vtotal = parseFloat($("#txtReparos_valor_total").val().replaceAll(".", "").replace(",", "."));
            var ServicosAdicionais_vtotal = parseFloat($("#txtServicosAdicionais_valor_total").val().replaceAll(".", "").replace(",", "."));
            $("#txtorc_valor_total").val(new Intl.NumberFormat('pt-BR', { maximumFractionDigits: 2, minimumFractionDigits: 2 }).format(Reparos_vtotal + ServicosAdicionais_vtotal));

        }
    });


    calculaTotal(orc_id);

    return false;
}





// ********* ABA INDICACAO DE SERVICOS ******************************************

function OrdemServico_Indicacao_Servico_ListAll(selectedId_ord_id) {
    event.preventDefault(); // cancel default behavior

    $.ajax({
        url: "/OrdemServico/OrdemServico_Indicacao_Servico_ListAll",
        type: "GET",
        data: { ord_id: selectedId_ord_id },
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {

            $('#txt_IndicacaoServico').val(result.data.trim());

            document.getElementById("txt_IndicacaoServico").disabled = true;
            document.getElementById("btnEditar_IndicacaoServicos").style.display = "inline";
            document.getElementById("btnSalvar_IndicacaoServicos").style.display = "none";
            document.getElementById("btnCancelar_IndicacaoServicos").style.display = "none";

             return false;
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
    return false;
}
function IndicacaoServicos_Editar()
{
    document.getElementById("btnEditar_IndicacaoServicos").style.display = "none";
    document.getElementById("btnSalvar_IndicacaoServicos").style.display = "inline";
    document.getElementById("btnCancelar_IndicacaoServicos").style.display = "inline";

    document.getElementById("txt_IndicacaoServico").disabled = false;

    return false;
}
function IndicacaoServicos_Cancelar() {
    document.getElementById("btnEditar_IndicacaoServicos").style.display = "inline";
    document.getElementById("btnSalvar_IndicacaoServicos").style.display = "none";
    document.getElementById("btnCancelar_IndicacaoServicos").style.display = "none";

    OrdemServico_PreencheDetalhes(selectedId_ord_id);

    document.getElementById("txt_IndicacaoServico").disabled = true;

    return false;
}
function IndicacaoServicos_Salvar() {


        $.ajax({
            url: "/OrdemServico/OrdemServico_Indicacao_Servico_Salvar",
            data: JSON.stringify({ ord_id: selectedId_ord_id, ord_indicacao_servico: $("#txt_IndicacaoServico").val()}),
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {

                OrdemServico_Indicacao_Servico_ListAll(selectedId_ord_id);

                document.getElementById("txt_IndicacaoServico").disabled = true;
                document.getElementById("btnEditar_IndicacaoServicos").style.display = "inline";
                document.getElementById("btnSalvar_IndicacaoServicos").style.display = "none";
                document.getElementById("btnCancelar_IndicacaoServicos").style.display = "none";

                swal({
                    type: 'success',
                    title: 'Sucesso',
                    text: 'Registro salvo com sucesso'
                });

                return false;
            },
            error: function (errormessage) {
                swal({
                    type: 'error',
                    title: 'Aviso',
                    text: 'Erro ao excluir registro'
                });
                return false;
            }
        });


return false;
}


// ******** selecao de objetos ***********************

function abrirLocalizarObjetos()
{
    // limpa o texbox
    $('#txtLocalizarObjeto').val("");
    $('#txtobj_codigo_Novo').css('background-color', corBranca);

    // limpa o listbox
    var lstObjetosLocalizados = document.getElementById("lstObjetosLocalizados");
    lstObjetosLocalizados.innerHTML = "";

    $('#txtLocalizarObjeto').focus();

    $('#modalLocalizarObjeto').modal('show');


    return false;
}
function LocalizarObjetos() {

    var sel_tos_id = $('#cmbTiposOS_Novo').val();
    var filtro_clo_id = 3;

    if (parseInt(sel_tos_id) == 7)
        filtro_clo_id = -13;

    var params = { doc_id: -1,
        filtro_obj_codigo: $('#txtLocalizarObjeto').val(),
        filtro_obj_descricao: '',
        filtro_clo_id: filtro_clo_id, // -13 ===> 2 = OAE = quilometragem; ou 3 = Tipo OAE
        filtro_tip_id:-1};

    $.ajax({
        url: '/Documento/PreencheCmbObjetosLocalizados',
        type: "POST",
        dataType: "JSON",
        data: params,
        success: function (lstObjetos) {

            // limpa o listbox
            var lstObjetosLocalizados = document.getElementById("lstObjetosLocalizados");
            lstObjetosLocalizados.innerHTML = "";

            var i = 0;
            $.each(lstObjetos, function (i, objeto) {
                i++;
                if (i < 50) {
                    $('#lstObjetosLocalizados')
                        .append($("<option></option>")
                            .attr("value", objeto.Value)
                            .text(objeto.Text)); 
                }
            });
            return false;
        }
    });


    return false;
}
function OrdemServico_Objeto_Salvar() {

   // selectedId_clo_id = 2;
    //selectedId_tip_id = 9;

    var lstObjetosLocalizados = document.getElementById("lstObjetosLocalizados");
    if (lstObjetosLocalizados.selectedIndex >= 0) {

        $('#txtobj_codigo_Novo').val(lstObjetosLocalizados.options[lstObjetosLocalizados.selectedIndex].text);
        $('#txtobj_codigo_Novo2').val(lstObjetosLocalizados.options[lstObjetosLocalizados.selectedIndex].text);


        var selObj_id = lstObjetosLocalizados.options[lstObjetosLocalizados.selectedIndex].value;
        var sel_txt = lstObjetosLocalizados.options[lstObjetosLocalizados.selectedIndex].text;
        var selObj_codigo = sel_txt.substring(0, sel_txt.indexOf("(")-1);

    //    var selTipoOS = $("#cmbTiposOS_Novo").val() == "" ? -1 : $("#cmbFiltroTiposOS").val();
        var selTipoOS = $("#cmbTiposOS_Novo").val() == "" ? -1 : $("#cmbTiposOS_Novo option:selected").val();
        var selTipoOStxt = $("#cmbTiposOS_Novo").val() == "" ? "" : $("#cmbTiposOS_Novo option:selected").text();

        // checa se ja tem O.S. com esse Objeto e Tipo O.S.
        $.ajax({
            "url": "/OrdemServico/OrdemServico_ListAll",
            "type": "GET",
            dataType: "JSON",
            "data": {
                "ord_id": 0,
                "filtroOrdemServico_codigo": '',
                "filtroObj_codigo": selObj_codigo,
                "filtroTiposOS": selTipoOS,
                "filtroStatusOS": -1,
                "filtroData": "Inicio_Programada",
                "filtroord_data_De": "01/01/2000",
                "filtroord_data_Ate": "01/01/2100"
            }
            , success: function (result) {
                if (result.data.length > 0) {
                    //// retorno = lista de O.S. do tipo selecionado e com Objeto selecionado. Verifica se é possivel criar nova, checa o status das O.S.s retornadas
                    ////2	Emergencial
                    ////6	Superada
                    ////7	Cancelada
                    ////11	Executada
                    ////12	Parcialmente Executada
                    ////14	Encerrada

                    //// verifica a ultima O.S. desse objeto
                    //var podecriar = false;
                    //var listasos_ids = [2,6,7,11,12,14];
                    //for (k = 0; k < result.data.length; k++) {

                    //    if (listasos_ids.includes(parseInt(result.data[k].sos_id)))
                    //    {

                    //    }
                    //}

                    // verifica a ultima O.S. desse Objeto
                    var ultimoStatus = 0;
                    var ultimaOS_id = -1;
                    for (k = 0; k < result.data.length; k++) {
                        if (parseInt(result.data[k].ord_id) > ultimaOS_id) {
                            ultimaOS_id = parseInt(result.data[k].ord_id);
                            ultimoStatus = parseInt(result.data[k].sos_id);
                        }
                    }

                    if (ultimoStatus > 0) {
                        swal({
                            type: 'error',
                            title: 'Aviso',
                            text: 'Já existe O.S. do tipo ' + selTipoOStxt + ' com o Objeto ' + selObj_codigo
                        }).then(
                            function () {
                                return false;
                            });
                    }

                }
                else
                {
                    // se for zero, entao nao tem O.S. com esse Objeto e do Tipo selecionado
                }
            }
        });


     //   $('#txtobj_codigo').val(lstObjetosLocalizados.options[lstObjetosLocalizados.selectedIndex].text);

        selectedId_obj_id = lstObjetosLocalizados.options[lstObjetosLocalizados.selectedIndex].value;

        $('#modalLocalizarObjeto').modal('hide');

        if (selectedId_obj_id != 0) {

            //  preenche Ficha inspecao cadastral 
            //preenchetblFicha_Inspecao_Cadastral(selectedId_obj_id, 3, -1);
        }
    }

    return false;
}



// ***************************************************

// montagem do gridview
function carregaGridOS(selectedId_ord_id) {
    filtroOrdemServico_codigo = $('#txtFiltroOrdemServico_codigo').val().trim();
    filtroObj_codigo = $('#txtFiltroObj_codigo').val().trim();
    filtroTiposOS = $("#cmbFiltroTiposOS").val() == "" ? -1 : $("#cmbFiltroTiposOS").val();
    filtroStatusOS = $("#cmbFiltroStatusOS").val() == "" ? -1 : $("#cmbFiltroStatusOS").val();
    filtroord_data_De = $('#filtroord_data_De').val();
    filtroord_data_Ate = $('#filtroord_data_Ate').val();

    $('#tblOrdemServicos').DataTable().destroy();
    $('#tblOrdemServicos').DataTable({
        "ajax": {
            "url": "/OrdemServico/OrdemServico_ListAll",
            "type": "GET",
            "datatype": "json",
            "data": {
                "ord_id": selectedId_ord_id,
                "filtroOrdemServico_codigo": filtroOrdemServico_codigo,
                "filtroObj_codigo": filtroObj_codigo,
                "filtroTiposOS": filtroTiposOS,
                "filtroStatusOS": filtroStatusOS,
                "filtroData": $("#cmbDatas").val(),
                "filtroord_data_De": filtroord_data_De,
                "filtroord_data_Ate": filtroord_data_Ate
            }
        }
        , "columns": [
            { data: "row_numero", "className": "hide_column", "width": "15px", "searchable": true, "sortable": true },
            { data: "temFilhos", "className": "hide_column", "width": "15px", "searchable": true, "sortable": true },
            { data: "row_expandida", "className": "hide_column", "width": "2px", "searchable": false, "sortable": false },
            { data: "ord_id", "className": "hide_column", "searchable": true },
            { data: "sos_id", "className": "hide_column", "searchable": true },
            { data: "tos_id", "className": "hide_column", "searchable": true },
            { data: "obj_id", "className": "hide_column", "searchable": true },
            {
                data: "temFilhos",
                "width": "35%", //"autoWidth": true,
                "searchable": true,
                "sortable": false,
                "render": function (data, type, row) {
                    var nTabs = "";
                    if (selectedId_ord_id != 0)
                        for (var i = 0; i < (row.nNivel); i++)
                            nTabs += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";

                    var retorno = "";
                    if (row.temFilhos > 0) {
                        if (row.row_expandida == 0)
                            retorno = '<div style="text-align:left" >&nbsp;&nbsp;' + nTabs + '<i class="fa fa-caret-right"></i> ' + row.ord_codigo + '</div >';
                        else
                            retorno = '<div style="text-align:left" >&nbsp;&nbsp;' + nTabs + '<i class="fa fa-caret-down"></i> ' + row.ord_codigo + '</div >';
                    }
                    else
                        retorno = '<div title="" style="text-align:left" >&nbsp;&nbsp;' + nTabs + ' ' + row.ord_codigo + '</div >';


                    return retorno;
                }
            },
            { data: "ord_descricao", "autoWidth": true, "searchable": true, "sortable": false },
            { data: "obj_codigo", "autoWidth": true, "searchable": false, "sortable": false },
            { data: "tos_codigo", "width": "60px", "searchable": false, "sortable": false },
            { data: "sos_descricao", "width": "60px", "searchable": false, "sortable": false },
            //{ data: "ocl_codigo", "width": "60px", "searchable": false, "sortable": false },
            { data: "nNivel", "className": "hide_column", "searchable": false, "sortable": false },
            {
                "width": "60px",
                data: "ord_id",
                "searchable": false,
                "sortable": false,
                "render": function (data, type, row) {
                    var retorno = "";
                    //if (permissaoInsercao > 0) {
                    //        var objcod = "'" + row["ord_codigo"] + "'";
                    //        retorno = '<a href="#" onclick="return Inserir(' + data + ',' + row["ocl_id"] + ',' + row["tos_id"] + ',' + objcod + ')" title="Nova O.S. Filha" ><span class="glyphicon glyphicon-plus"></span></a>' + '  ';
                    //}
                    //else {
                    //    retorno = '<span class="glyphicon glyphicon-plus desabilitado "  ></span>' + '  ';
                    //}

                    if (permissaoEscrita > 0) {
                        var objcod = "'" + row["ord_codigo"] + "'";
                        var objdesc = "'" + row["ord_descricao"] + "'";
                        var clo_nome = "'" + row["ocl_codigo"] + "'";
                        var tip_nome = "'" + row["tos_codigo"] + "'";

                        retorno += '<a href="#" onclick="return OrdemServico_Editar(' + data + ')" title="Editar" ><span class="glyphicon glyphicon-pencil"></span></a>' + '  ';

                        if (row.ord_ativo == 1)
                            retorno += '<a href="#" onclick="return OrdemServico_AtivarDesativar(' + data + ', 0)" title="Ativo" ><span class="glyphicon glyphicon-ok text-success"></span></a>' + '  ';
                        else
                            retorno += '<a href="#" onclick="return OrdemServico_AtivarDesativar(' + data + ', 1)" title="Desativado" ><span class="glyphicon glyphicon-remove text-danger"></span></a>' + '  ';
                    }
                    else {
                        retorno = '<span class="glyphicon glyphicon-pencil desabilitado"  ></span>' + '  ';

                        if (row.ord_ativo == 1)
                            retorno += '<span class="glyphicon glyphicon-ok text-success desabilitado" ></span>' + '  ';
                        else
                            retorno += '<span class="glyphicon glyphicon-remove text-danger desabilitado"  ></span>' + '  ';
                    }

                    if (permissaoExclusao > 0) {
                        retorno += '<a href="#" onclick="return OrdemServico_Excluir(' + data + ')" title="Excluir" ><span class="glyphicon glyphicon-trash"></span></a>';
                    }
                    else
                        retorno += '<span class="glyphicon glyphicon-trash desabilitado" title="Excluir" ></span>';

                    return retorno;
                }
            }
            ,{ data: "orc_id", "className": "hide_column" }
       ]
        , "columnDefs": [
            {
                targets: [10] // coloca tooltip no campo tos_codigo
                , "createdCell": function (td, cellData, rowData, row, col) {
                    $(td).attr('title', rowData["tos_descricao"]);
                }
            },
            {
                targets: [11] // coloca tooltip no campo sos_codigo
                , "createdCell": function (td, cellData, rowData, row, col) {
                    $(td).attr('title', rowData["sos_codigo"]);
                }
            },

        ]
        , "order": [0, "asc"]
        , "rowId": "ord_id"
        , "rowCallback": function (row, data) {
            if (data.ord_id == selectedId_ord_id)
            {
                $(row).addClass('selected');
                selectedId_obj_id = data.obj_id;
                selectedId_sos_id= data.sos_id;
            }
        }
        , "lengthMenu": [[10, 20, 50, 100], [10, 20, 50, 100]]
        , select: {
            style: 'single'
        }
        , searching: true
        , "oLanguage": idioma
        , "pagingType": "input"
        , "sDom": '<"top">rt<"bottom"pfli><"clear">'
    });


    if ((selectedId_ord_id != 0) && (selectedId_obj_id > 0)) 
    {
       preenchetblFicha(selectedId_obj_id, 1, -1);
    }

}


$(document).ready(function () {

    //desabilita os controles
    OrdemServico_setaReadWrite(true);


    // ****************************GRID tblOrdemServicos *****************************************************************************
    carregaGridOS(selectedId_ord_id);

    var tblOrdemServicos = $('#tblOrdemServicos').DataTable();
    $('#tblOrdemServicos tbody').on('click', 'tr', function () {

        // expande ou encolhe os filhos
        var temFilhos = parseInt(tblOrdemServicos.row(this).selector.rows.children[1].innerText);
        var expandida = parseInt(tblOrdemServicos.row(this).selector.rows.children[2].innerText);
        var ord_rowId = tblOrdemServicos.row(this).selector.rows.id;

        // guarda os ids do objeto
        selectedId_ord_id = ord_rowId;
        selectedId_ocl_id = parseInt(tblOrdemServicos.row(this).selector.rows.children[4].innerText);
        selectedId_tos_id = parseInt(tblOrdemServicos.row(this).selector.rows.children[5].innerText);
        selectedId_sos_id = parseInt(tblOrdemServicos.row(this).selector.rows.children[4].innerText);

        selectedId_orc_id = parseInt(tblOrdemServicos.row(this).selector.rows.children[14].innerText);

        var ord_descricao = tblOrdemServicos.row(this).selector.rows.children[8].innerText;
        var ord_codigo = tblOrdemServicos.row(this).selector.rows.children[7].innerText;
        selectedord_descricao = ord_descricao;

        filtroOrdemServico_codigo = tblOrdemServicos.row(this).selector.rows.children[7].innerText;
        filtroObj_codigo = tblOrdemServicos.row(this).selector.rows.children[9].innerText;
        selected_obj_codigo  = tblOrdemServicos.row(this).selector.rows.children[9].innerText;
        filtroTiposOS = parseInt(tblOrdemServicos.row(this).selector.rows.children[5].innerText);
        filtroStatusOS = parseInt(tblOrdemServicos.row(this).selector.rows.children[4].innerText);
        filtroord_data_De = '';
        filtroord_data_Ate = '';

        selectedId_obj_id = parseInt(tblOrdemServicos.row(this).selector.rows.children[6].innerText);

        // limpa as tabulacoes e espacos
        ord_codigo = ord_codigo.replace(/&nbsp;/g, ' ').trim();
        selectedord_codigo = ord_codigo;
        document.getElementById('txt_codigo').value = ord_codigo;

        // deseleciona a linha
        if ($(this).hasClass('selected')) {
            $(this).removeClass('selected');
            //if (temFilhos == 0) {
            //    ord_codigo = "";
            //    selectedId_ord_id = 0;
            //    selectedId_ocl_id = -1;
            //    selectedId_tos_id = -1;
            //}
        }
        else {
            // seleciona a linha
            // remove a classe "selected" das linhas da tabela
            var els = document.getElementById("tblOrdemServicos_wrapper").getElementsByClassName("selected");
            for (var i = 0; i < els.length; i++)
                els[i].classList.remove('selected');

            $(this).addClass('selected');

            // preenche as caixas de filtro
            filtroOrdemServico_codigo = ord_codigo; // codigo sem as tabulacoes
            $('#txtFiltroOrdemServico_codigo').val(ord_codigo);
            $('#txtFiltroObj_codigo').val(filtroObj_codigo);
            $("#cmbFiltroTiposOS").val(filtroTiposOS);
            $("#cmbFiltroStatusOS").val(filtroStatusOS);
            $('#txtfiltroord_data_De').val("");
            $('#txtfiltroord_data_Ate').val("");

        }


        // se estiver expandida, manda o codigo negativo (positivo = expandir / negativo = encolher)
        // filtra pelo codigo do objeto selecionado (metodo logo acima deste)
        if (temFilhos > 0) {
            carregaGridOS(expandida == 1 ? -selectedId_ord_id : selectedId_ord_id);
            var filtroRow_numero = Math.trunc(tblOrdemServicos.row(this).selector.rows.children[0].innerText);
            filtroRow_numeroMin = parseInt(filtroRow_numero);
            filtroRow_numeroMax = filtroRow_numeroMin + 1;
        }
        else {
            carregaGridOS(selectedId_ord_id);
        }

        if (selectedId_ord_id != 0) {

            //  preenche Ficha inspecao cadastral 
            //preenchetblFicha_Inspecao_Cadastral(selectedId_obj_id, 3, -1);

            //  atualiza grid documentos
            document.getElementById('HeaderOS_Documentos').innerText = "Documentos associados à O.S.: " + ord_codigo;
            $('#tblDocumentosAssociados').DataTable().ajax.reload();

            //  atualiza grid documentos
            var obj_codigo = filtroObj_codigo;
            var texto = "Documentos associados ao Objeto: " + obj_codigo; //obj_id_TipoOAE_codigo + "<br /> e ao Objeto: " + obj_codigo;

            $("#HeaderOS_DocumentosOBJ").html(texto);
            $('#tblDocumentosAssociadosOBJ').DataTable().ajax.reload();

        }
     //   else
       //     accordion_encolher(0);

    //    document.getElementById('subGrids').style.visibility = "visible";

        if (!ehEdicao)
            $(window).scrollTop(0); 

        // preenche os campos
        OrdemServico_PreencheDetalhes(selectedId_ord_id);

        // mostra as abas pertinentes ao tipo da O.S.
        mostraAba(selectedId_tos_id, false);

    });

});





// **************************** DOCUMENTOS *****************************************************************************
function AssociarDocumento() {

    if (selectedId_ord_id > 0) {
        // limpa o texbox de input
        $("#txtLocalizarDocumento").val("");

        // limpa a lista de documentos
        $("#divDocumentosLocalizados").empty();

        document.getElementById("txtLocalizarDocumento").focus();
        $("#modalAssociarDocumento").modal('show');
    }
    else {
        swal({
            type: 'error',
            title: 'Aviso',
            text: 'Nenhuma O.S. selecionada'
        }).then(
            function () {
                return false;
            });
    }
}

function DesassociarDocumento(doc_id) {

    if (doc_id >= 0) {
        var form = this;

        swal({
            title: "Desassociar Documento. Tem certeza?",
            icon: "warning",
            buttons: [
                'Não',
                'Sim'
            ],
            dangerMode: true,
            focusCancel: true
        }).then(function (isConfirm) {
            if (isConfirm) {
                var response = POST("/OrdemServico/OrdemServico_DesassociarDocumento", JSON.stringify({ "doc_id": doc_id, "obj_id":"-1", "ord_id": selectedId_ord_id }))
                if (response.erroId >= 1) {
                    swal({
                        type: 'success',
                        title: 'Sucesso',
                        text: 'Documento Desassociado com Sucesso'
                    });

                    $('#tblDocumentosAssociados').DataTable().ajax.reload(null, false);  //false if you don't want to refresh paging else true.
                    $('#tblDocumentosAssociadosOBJ').DataTable().ajax.reload(null, false);  //false if you don't want to refresh paging else true.
                    carregaGrid_tblDocumentosReferencia(-1);
                }
                else {
                    swal({
                        type: 'error',
                        title: 'Aviso',
                        text: 'Erro ao Desassociar documento'
                    });
                }
                return true;
            } else {
                return false;
            }
        })
    }

    return false;
}

function OrdemServico_AssociarDocumento_Salvar() {


    // cria lista dos IDs de Documentos selecionados
    var selchks = [];
    var doc_ids = "";
    $('#divDocumentosLocalizados input:checked').each(function () {
        selchks.push($(this).attr('value'));
    });

    for (var i = 0; i < selchks.length; i++)
        if (i == 0)
            doc_ids = selchks[i];
        else
            doc_ids = doc_ids + ";" + selchks[i];

    doc_ids = doc_ids + ";"; // acrescenta um delimitador no final da string

    if (selchks.length > 0) {
        var associacao = {
            "doc_ids": doc_ids,
            "ord_id": selectedId_ord_id
        };

        $.ajax({
            url: "/OrdemServico/OrdemServico_AssociarDocumentos",
            data: JSON.stringify(associacao),
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                $("#modalAssociarDocumento").modal('hide');
                $('#tblDocumentosAssociados').DataTable().ajax.reload(null, false);  //false if you don't want to refresh paging else true.
                $('#tblDocumentosAssociadosOBJ').DataTable().ajax.reload(null, false);  //false if you don't want to refresh paging else true.
                carregaGrid_tblDocumentosReferencia(-1);
                return false;
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
                $("#modalAssociarDocumento").modal('show');
                return false;
            }
        });
    }
    else {
        swal({
            type: 'error',
            title: 'Aviso',
            text: 'Documento não selecionado'
        }).then(
            function () {
                return false;
            });
    }


    return false;
}

function LocalizarDocumentos() {

    // desabilita os controles
    document.body.style.cursor = "wait";
    document.getElementById('txtLocalizarDocumento').disabled = true;
    document.getElementById('btnLocalizarDocumentos').disabled = true;

    var txtDocumento = $('#txtLocalizarDocumento').val();
    $.ajax({
        url: '/OrdemServico/PreencheCmbDocumentosLocalizados',
        type: "POST",
        dataType: "JSON",
        data: { ord_id: selectedId_obj_id, codDoc: txtDocumento },
        success: function (lstDocumentos) {
            var i = 0;

            document.getElementById("divDocumentosLocalizados").innerHTML = "";
            $.each(lstDocumentos, function (i, Documento) {
                i++;
                if (i == 1) {
                    var texto;
                    var total_registros = parseInt(Documento.Value);
                    if (total_registros > 100)
                        texto = "Total " + total_registros.toLocaleString('pt-br') + " documentos. <br /> Apresentando 100 primeiros";
                    else
                        if (total_registros > 1)
                            texto = "Total " + total_registros + " documentos.";
                        else
                            if (total_registros == 1)
                                texto = "Total 1 documento.";
                            else
                                texto = "Documento não localizado.";

                    $("#lblTotalLocalizados").html(texto);

                }
                else
                    if (i < 100) {
                        var tagchk = '<input type="checkbox" id="idXXX" nome="nameXXX" value="valueXXX" style="margin-right:5px">';
                        tagchk = tagchk.replace("idXXX", "chk" + i);
                        tagchk = tagchk.replace("nameXXX", "chk" + i);
                        tagchk = tagchk.replace("valueXXX", Documento.Value);

                        var taglbl = '<label for="idXXX" class="chklst" >TextoXXX</label> <br />';
                        taglbl = taglbl.replace("idXXX", "chk" + i);
                        taglbl = taglbl.replace("TextoXXX", Documento.Text);

                        $("#divDocumentosLocalizados").append(tagchk + taglbl);
                    }

            });


            //habilita os controles
            document.body.style.cursor = "default";
            document.getElementById('txtLocalizarDocumento').disabled = false;
            document.getElementById('btnLocalizarDocumentos').disabled = false;
            return false;
        }
    });

    return false;
}

// ****GRId DocumentosAssociados ao OBJETO ****************************
$('#tblDocumentosAssociadosOBJ').DataTable({
    "ajax": {
        "url": "/OrdemServico/OrdemServico_Objeto_Documentos_ListAll",
        "data": function (d) { d.ord_id = selectedId_ord_id},
        "type": "GET",
        "datatype": "json"
    }
    , "columns": [
        { data: "tpd_id", "className": "hide_column", "searchable": false },
        {
            "title": "De Referência",
            data: "doc_id",
            "width": "30px", "searchable": false, "sortable": false,
            "className": "text-center",
            render: function (data, type, row) {
                var retorno = '';
                if ( parseInt(row["dos_referencia"]) == 1)
                    retorno = '<a href="#" onclick="return OrdemServico_AssociarDocumentoReferencia_Salvar(' + row.doc_id + ',0)" ><span class="glyphicon glyphicon-ok text-success" style="font-size: 1.3em;" ></span></a>' + '  ';
                else
                    retorno += '<a href="#" onclick="return OrdemServico_AssociarDocumentoReferencia_Salvar(' + row.doc_id + ',1)" title="Associar como Referência" ><span class="fa fa-square-o"></span></a>';

                return retorno;
            }
        },

        {
            data: "doc_codigo",
            "autoWidth": true,
            "searchable": true,
            "render": function (data, type, row) {
                var retorno = row["doc_codigo"];
                if (row["doc_codigo"].substring(0, 1) == "*")
                    retorno = row["doc_codigo"].substring(1, 100);

                return retorno;
            }

        },
        { data: "tpd_descricao", "autoWidth": true, "searchable": false },
        { data: "doc_descricao", "autoWidth": true, "searchable": false },
        {
            data: "doc_caminho", "autoWidth": true, "searchable": false
            , "title": "Arquivo"
            , "render": function (data, type, row) {
                var retorno = "";
                var caminho = "'" + data + "'";
                retorno = data.split("/").pop();
                return retorno;
            }
        },
        {
            "title": "Opções",
            data: "doc_id",
            "searchable": false,
            "sortable": false,
            "render": function (data, type, row) {
                var retorno = "";
                //retorno = '<span class="glyphicon glyphicon-pencil desabilitado"  ></span>' + '  ';
                //retorno += '<span class="glyphicon glyphicon-ok text-success desabilitado" ></span>' + '  ';
                //retorno += '<span class="glyphicon glyphicon-trash desabilitado" title="Excluir" ></span>';

                var doc_caminho = row["doc_caminho"];
                var caminho = "'" + row["doc_caminho"] + "'";
                if (permissaoLeitura > 0) {
                    if (doc_caminho != "") {
                        retorno += '<a href="#" onclick="window.open(' + caminho + ')"  title="Abrir Documento" ><span class="fa fa-search" style="padding-left:5px"></span></a>' + '  ';
                    }

                }
                else {
                    retorno += '<span class="fa fa-search desabilitado"  style="padding-left:5px" ></span>' + '  ';
                }
                return retorno;
            }
        }
    ]
    , "searching": true
    , "rowId": "doc_id"
    , "lengthMenu": [[10, 25, 50, 100], [10, 25, 50, 100]]
    , select: {
        style: 'single'
    }
    , "oLanguage": idioma
    , "pagingType": "input"
    , "sDom": '<"top">rt<"bottom"pfli><"clear">'
});



// ****GRId DocumentosAssociados à Ordem Serviço ****************************
$('#tblDocumentosAssociados').DataTable({
    "ajax": {
        "url": "/OrdemServico/OrdemServico_Documentos_ListAll",
        "data": function (d) { d.ord_id = selectedId_ord_id; d.obj_id = -1 },
        "type": "GET",
        "datatype": "json"
    }
    , "columns": [
        { data: "tpd_id", "className": "hide_column", "searchable": false },

        { // COLUNA DESASSOCIAR DOCUMENTO
            "title": "",
            "className": "text-center",
            data: "doc_id", "width": "30px", "searchable": false,
            "orderable": false,
            render: function (data, type, row) {
                var retorno = '';
                if (permissaoEscrita > 0)
                    retorno = '<a href="#" onclick="return DesassociarDocumento(' + row.doc_id + ')" ><span class="glyphicon glyphicon-trash text-success"></span></a>' + '  ';
                else
                    retorno = '<span class="glyphicon glyphicon-trash text-success desabilitado"></span>' + '  ';

                return retorno;
            }
        },

        {
            "title": "De Referência",
            data: "doc_id",
            "width": "30px", "searchable": false, "sortable": false,
            "className": "text-center",
            render: function (data, type, row) {
                var retorno = '';
                if (parseInt(row["dos_referencia"]) == 1)
                    retorno = '<a href="#" onclick="return OrdemServico_AssociarDocumentoReferencia_Salvar(' + row.doc_id + ',0)" ><span class="glyphicon glyphicon-ok text-success" style="font-size: 1.3em;" ></span></a>' + '  ';
                else
                    retorno += '<a href="#" onclick="return OrdemServico_AssociarDocumentoReferencia_Salvar(' + row.doc_id + ',1)" title="" ><span class="fa fa-square-o"></span></a>';

                return retorno;
            }
        },
        { data: "doc_codigo", "autoWidth": true, "searchable": true },
        { data: "tpd_descricao", "autoWidth": true, "searchable": false },
        { data: "doc_descricao", "autoWidth": true, "searchable": false },
        {
            data: "doc_caminho", "autoWidth": true, "searchable": false
            , "title": "Arquivo"
            , "render": function (data, type, row) {
                var retorno = "";
                var caminho = "'" + data + "'";
                retorno = data.split("/").pop();
                return retorno;
            }
        },
        {
            "title": "Opções",
            data: "doc_id",
            "searchable": false,
            "sortable": false,
            "render": function (data, type, row) {
                var retorno = "";
                //retorno = '<span class="glyphicon glyphicon-pencil desabilitado"  ></span>' + '  ';
                //retorno += '<span class="glyphicon glyphicon-ok text-success desabilitado" ></span>' + '  ';
                //retorno += '<span class="glyphicon glyphicon-trash desabilitado" title="Excluir" ></span>';

                var doc_caminho = row["doc_caminho"];
                var caminho = "'" + row["doc_caminho"] + "'";
                if (permissaoLeitura > 0) {
                    if (doc_caminho != "") {
                        retorno += '<a href="#" onclick="window.open(' + caminho + ')"  title="Abrir Documento" ><span class="fa fa-search" style="padding-left:5px"></span></a>' + '  ';
                    }

                }
                else {
                    retorno += '<span class="fa fa-search desabilitado"  style="padding-left:5px" ></span>' + '  ';
                }
                return retorno;
            }
        }
    ]

    , "searching": true
    , "rowId": "doc_id"
    , "lengthMenu": [[10, 25, 50, 100], [10, 25, 50, 100]]
    , select: {
        style: 'single'
    }
    , "oLanguage": idioma
    , "pagingType": "input"
    , "sDom": '<"top">rt<"bottom"pfli><"clear">'
});


function OrdemServico_AssociarDocumentoReferencia_Salvar(doc_id, dos_referencia) {

    if (doc_id >= 0) {
        var form = this;
        var titles = "Associar Documento como Referência à O.S. Tem certeza?";
        if (dos_referencia == 0)
            titles = "Desassociar Documento de Referência da O.S. Tem certeza?";

        swal({
            title: titles ,
            icon: "warning",
            buttons: [
                'Não',
                'Sim'
            ],
            dangerMode: true,
            focusCancel: true
        }).then(function (isConfirm) {
            if (isConfirm) {
                    var associacao = {
                        "doc_id": doc_id,
                        "ord_id": selectedId_ord_id,
                        "dos_referencia": dos_referencia
                    };
                    $.ajax({
                        url: "/OrdemServico/OrdemServico_AssociarDocumentosReferencia",
                        data: JSON.stringify(associacao),
                        type: "POST",
                        contentType: "application/json;charset=utf-8",
                        dataType: "json",
                        success: function (result) {
                            $('#tblDocumentosAssociados').DataTable().ajax.reload(null, false);  //false if you don't want to refresh paging else true.
                            $('#tblDocumentosAssociadosOBJ').DataTable().ajax.reload(null, false);  //false if you don't want to refresh paging else true.
                            carregaGrid_tblDocumentosReferencia(-1);

                            titles = "Documento Associado com Sucesso";
                            if (dos_referencia == 0)
                                titles = "Documento desassociado com Sucesso";

                            swal({
                                type: 'success',
                                title: 'Sucesso',
                                text: titles
                            });

                            return false;
                        },
                        error: function (errormessage) {
                            alert(errormessage.responseText);
                            return false;
                        }
                    });

                return false;
            } else {
                return false;
            }
        })
    }




    return false;
}



