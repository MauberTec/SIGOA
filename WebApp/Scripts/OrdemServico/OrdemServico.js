
var selectedId_ord_id = 0;
var selectedId_obj_id = 0;
var selectedId_tos_id = 0;
var selected_obj_codigo = '';

var filtroOrdemServico_codigo = '';
var filtroObj_codigo = '';
var filtroTiposOS = -1;
var filtroStatusOS = -1;
var filtroord_data_De = '';
var filtroord_data_Ate = '';
var ehEdicao = false;

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
    var liFichaInspecaoEspecial = document.getElementById("liFichaInspecaoEspecial");
    var liFichaInspecaoEspecialAnomalias = document.getElementById("liFichaInspecaoEspecialAnomalias");
    var liFichaNotificacaoOcorrencia = document.getElementById("liFichaNotificacaoOcorrencia");

    //if (liDetalhesOS)
    //    $('[href="#tabDetalhesOS"]').tab('show');

    if (liFichaInspecaoCadastral)
        liFichaInspecaoCadastral.style.display = "none";
    if (liFichaInspecao1aRotineira)
        liFichaInspecao1aRotineira.style.display = "none";

    if (liFichaInspecaoRotineira)
        liFichaInspecaoRotineira.style.display = "none";

    if (liFichaInspecaoEspecial)
        liFichaInspecaoEspecial.style.display = "none";

    if (liFichaInspecaoEspecialAnomalias)
        liFichaInspecaoEspecialAnomalias.style.display = "none";

    if (liFichaNotificacaoOcorrencia)
        liFichaNotificacaoOcorrencia.style.display = "none";


    switch (parseInt(selectedId_tos_id)) {
        case 7:
            liFichaInspecaoCadastral.style.display = "unset";
            liFichaInspecao1aRotineira.style.display = "unset";
            if (bool_posicionar)
                $('[href="#tabFichaInspecaoCadastral"]').tab('show');
            break;

        case 8: liFichaInspecaoRotineira.style.display = "unset";
            if (bool_posicionar)
                $('[href="#tabFichaInspecaoRotineira"]').tab('show');
            break;

        case 9: liFichaInspecaoEspecial.style.display = "unset";
            liFichaInspecaoEspecialAnomalias.style.display = "unset";
             if (bool_posicionar)
                 $('[href="#tabFichaInspecaoEspecial"]').tab('show');
            break;

        case 18: liFichaNotificacaoOcorrencia.style.display = "unset";
             if (bool_posicionar)
                 $('[href="#tabFichaNotificacaoOcorrencia"]').tab('show');
            break;


        default: $('[href="#tabDetalhesOS"]').tab('show');

    }

}


function OrdemServico_setaReadWrite(ehRead) {
    var tabela = document.getElementById("divDetalhes");

    // habilita ou desabilita todos os controles editaveis
    var lstTxtBoxes = tabela.getElementsByTagName('input');
    var lstCombos = tabela.getElementsByTagName('select');
    var lstTextareas = tabela.getElementsByTagName('textarea');
    var lstButtons = tabela.getElementsByTagName('button');

    var controlesReadOnly = ["txtord_codigo", "txtobj_codigo_Novo2", "btnAbrirLocalizarObjetos"];
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

    
    if (ehRead) {
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


    $('#txtord_quantidade_estimada').val("");
    $('#txtord_custo_estimado').val("");

    $('#txttpu_codigo_der').val("");
    $('#txttpu_descricao').val("");
    $('#txttpu_data_base_der').val("");

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

    $('#txtord_quantidade_executada').val("");
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
            ord_quantidade_estimada: $('#txtord_quantidade_estimada').val(),
            uni_id_qt_estimada: $('#txtuni_id_qt_estimada').val(),
            ord_quantidade_executada: $('#txtord_quantidade_executada').val(),
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

            $('#txtord_aberta_por').val(result.ord_aberta_por_nome.trim());
            $('#txtord_responsavel_der').val(result.ord_responsavel_der.trim());
            $('#txtord_responsavel_fiscalizacao').val(result.ord_responsavel_fiscalizacao.trim());
            $('#txtord_responsavel_execucao').val(result.ord_responsavel_execucao.trim());

            $('#txtord_data_inicio_programada').val(result.ord_data_inicio_programada.substring(0,10));
            $('#txtord_data_inicio_execucao').val(result.ord_data_inicio_execucao.substring(0, 10));
            $('#txtord_data_suspensao').val(result.ord_data_suspensao.substring(0, 10));
            $('#txtord_data_cancelamento').val(result.ord_data_cancelamento.substring(0, 10));
            $('#txtord_data_reinicio').val(result.ord_data_reinicio.substring(0, 10));

            $('#txtord_criticidade').val(result.ord_criticidade);
            $('#txtord_quantidade_estimada').val(result.ord_quantidade_estimada);
            $('#txtord_custo_estimado').val(result.ord_custo_estimado);

            $('#txttpu_codigo_der').val(result.tpu_codigo_der.trim());
            $('#txttpu_descricao').val(result.tpu_descricao.trim());
            $('#txttpu_data_base_der').val(result.tpu_data_base_der.substring(0, 10));

            $('#cmbClassesOS').val(result.ocl_id);

            $('#txtord_codigo_pai').val(result.ord_codigo_pai);
            $('#txtord_data_abertura').val(result.ord_data_abertura.substring(0, 10));
            $('#txtcon_codigofiscalizacao').val(result.con_codigofiscalizacao.trim());
            $('#txtcon_codigoexecucao').val(result.con_codigoexecucao.trim());

            $('#txtord_data_termino_programada').val(result.ord_data_termino_programada.substring(0, 10));
            $('#txtord_data_termino_execucao').val(result.ord_data_termino_execucao.substring(0, 10));
            $('#txtord_responsavel_suspensao').val(result.ord_responsavel_suspensao.trim());
            $('#txtord_responsavel_cancelamento').val(result.ord_responsavel_cancelamento.trim());

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

            $('#txtord_quantidade_executada').val(result.ord_quantidade_executada);
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



