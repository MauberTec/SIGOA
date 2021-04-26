
function OpenModalReparo() {
    $("#modalNovo").modal('show');
}




// ********** filtros ************************************************
function cmbFiltroLegenda_onchange()
{
    id = $('#cmbFiltroLegenda option:selected').val();
    PoliticaReparos_preencheCombo('cmbFiltroAnomalia',id);
    PoliticaReparos_preencheCombo('cmbFiltroCausa', id);
}
function cmbFiltroAnomalia_onchange()
{
    id = $('#cmbFiltroLegenda option:selected').val();
    PoliticaReparos_preencheCombo('cmbFiltroCausa', id);
}
function btnExecutarFiltro_onclick() {
    carregaGridPoliticaReparos(1);
}
function btnLimparFiltro_onclick() {
    
    $("#cmbFiltroTiposReparo").val("");
    $("#cmbFiltroLegenda").val("");
    $("#cmbFiltroAlerta").val("");

    $("#cmbFiltroAnomalia").html("");
    $("#cmbFiltroCausa").html("");

    carregaGridPoliticaReparos(0);
}

// **********************************************************

function cmbLegenda_onchange()
{
    var id = $('#cmbLegenda option:selected').val();

    PoliticaReparos_preencheCombo('divCodAnomaliaUp', id);
    PoliticaReparos_preencheCombo('divAlertaAdd', id);
    PoliticaReparos_preencheCombo('divCausaAdd', id);
}


function PoliticaReparos_preencheCombo(qualCombo, id) {

    var url;

    if ((qualCombo == "cmbFiltroAnomalia") || (qualCombo == "divCodAnomaliaUp"))
    {
        url = '/Reparo/PreenchecmbFiltroAnomalia';
    }
    else
        if ((qualCombo == "cmbFiltroCausa") || (qualCombo == "divCausaAdd")) {
            url = '/Reparo/PreenchecmbFiltroCausa';
        }
    else
            if (qualCombo == "divAlertaAdd") {
                url = '/Reparo/PreenchecmbAlerta';
        }

    var cmb = $("#" + qualCombo);

    // limpa os itens existentes
    if (qualCombo.indexOf("div") >= 0) {
        cmb.empty();
    }
    else {
        cmb.html("");
        cmb.append($('<option selected ></option>').val(-1).html("-- Selecione --")); // 1o item vazio
    }

    $.ajax({
        url: url,
        type: "POST",
        dataType: "JSON",
        data: { id: id },
        success: function (lstSubNiveis) {
            $.each(lstSubNiveis, function (i, subNivel) {

                if (qualCombo.indexOf("cmb") >= 0) {
                    cmb.append($('<option></option>').val(subNivel.Value.trim()).html(subNivel.Text.trim()));
                }
                else
                    if (qualCombo.indexOf("div") >= 0) {
                        var tagchk = '<input type="checkbox" id="idXXX" nome="nameXXX" value="valueXXX" style="margin-right:5px">';
                        tagchk = tagchk.replace("idXXX", "chk" + i);
                        tagchk = tagchk.replace("nameXXX", "chk" + i);
                        tagchk = tagchk.replace("valueXXX", subNivel.Value.trim());

                        var taglbl = '<label for="idXXX" class="chklst" >TextoXXX</label> <br />';
                        taglbl = taglbl.replace("idXXX", "chk" + i);
                        taglbl = taglbl.replace("TextoXXX", subNivel.Text.trim());
                        cmb.append(tagchk + taglbl);
                    }

                });
        }
    });
}


function GetAll() {
    carregaGridPoliticaReparos();
}


// ********************   montagem do gridview  ************************************
$(document).ready(function () {
    carregaGridPoliticaReparos();
});

function carregaGridPoliticaReparos(filtrado)
{
    $('#msg').show();
    $('#msg').val('Aguarde...');

    var data = {};
    var url = "/Reparo/PoliticaReparo_ListAll";
    if (filtrado == 1) {
        url = "/Reparo/PoliticaReparo_GetbyID";
        data = {
            rpt_id: $('#cmbFiltroTiposReparo').val(),
            leg_id: $('#cmbFiltroLegenda').val(),
            atp_id: $('#cmbFiltroAnomalia').val(),
            ale_id: $('#cmbFiltroAlerta').val(),
            aca_id: $('#cmbFiltroCausa').val()
        };
    }

    $('#tblSubs2').DataTable().destroy();
    $('#tblSubs2').DataTable({
        "ajax": {
            "url": url,
            "type": "GET",
            "datatype": "json",
            data: data,
        }
         , "columns": [
                { data: "rpp_id", "className": "hide_column" },
                { data: "rpt_codigo", "autoWidth": true, "className": "Centralizado", "searchable": true },
                { data: "leg_codigo", "autoWidth": true, "className": "Centralizado", "searchable": true },

                {
                    data: "atp_codigo", "autoWidth": true, "searchable": true,
                    "render": function (data, type, row) {
                        return row["atp_codigo"] + " - " + row["atp_descricao"];
                    }
                },

                { data: "ale_codigo", "className": "Centralizado", "autoWidth": true, "searchable": true },
                {
                    data: "aca_codigo", "autoWidth": true, "searchable": true,
                    "render": function (data, type, row) {
                                    return row["aca_codigo"] + " - " + row["aca_descricao"];
                                }
                },
                {
                    "title": "Opções",
                    data: "rpp_id",
                    "className": "Centralizado",
                    "searchable": false,
                    "sortable": false,
                    "render": function (data, type, row) {
                        var retorno = "";
                        if (permissaoExclusao > 0)
                            retorno += '<a href="#" onclick="return DeleteReparo(' + data + ')" title="Excluir" ><span class="glyphicon glyphicon-trash"></span></a>';
                        else
                            retorno += '<span class="glyphicon glyphicon-trash desabilitado" title="Excluir" ></span>';

                        return retorno;
                    }
                }
            ]
        , "columnDefs": [
            {
                targets: [1] // coloca tooltip no campo rpt_codigo
                , "createdCell": function (td, cellData, rowData, row, col) {
                    $(td).attr('title', rowData["rpt_descricao"]);
                }
            },
            {
                targets: [2] // coloca tooltip no campo leg_codigo
                , "createdCell": function (td, cellData, rowData, row, col) {
                    $(td).attr('title', rowData["leg_descricao"]);
                }
            },
            {
                targets: [3] // coloca tooltip no campo atp_codigo
                , "createdCell": function (td, cellData, rowData, row, col) {
                    $(td).attr('title', rowData["atp_descricao"]);
                }
            },
            {
                targets: [4] // coloca tooltip no campo ale_codigo
                , "createdCell": function (td, cellData, rowData, row, col) {
                    $(td).attr('title', rowData["ale_descricao"]);
                }
            },
            {
                targets: [5] // coloca tooltip no campo aca_codigo
                , "createdCell": function (td, cellData, rowData, row, col) {
                    $(td).attr('title', rowData["aca_descricao"]);
                }
            }
        ]
            , "rowId": "rpp_id"
            , "lengthMenu": [[10, 25, 50, 100], [10, 25, 50, 100]]
            , select: {
                style: 'single'
            }
            , searching: true
            , "oLanguage": idioma
            , "pagingType": "input"
            , "sDom": '<"top">rt<"bottom"pfli><"clear">'
          , "initComplete": function (settings, json) {
              $('#msg').hide();
          }
    });

}

// ********************************************************************************

function btnSalvar_onclick() {

    if ($('#cmbTiposReparo').val() == null) {
       swal({
                type: 'error',
                title: 'Aviso',
                text: 'Selecione Reparo'
            }).then(
                    function () {
                        return false;
                    });
        return false;
    }
    else
    if ($('#cmbLegenda').val() == null)
    {
        swal({
            type: 'error',
            title: 'Aviso',
            text: 'Selecione a Legenda'
        }).then(
             function () {
                 return false;
             });
        return false;
    }
  

    // cria lista dos IDs de Cod Anomalia
    var CodAnomalia = [];
    $('#divCodAnomaliaUp input:checked').each(function () {
        CodAnomalia.push($(this).attr('value'));
    });

        if (CodAnomalia == '') {
            swal({
                type: 'error',
                title: 'Aviso',
                text: 'Selecione o Tipo de Anomalia'
            }).then(
                 function () {
                     return false;
                 });
            return false;
        }

    //cria lista de Ids do Causa
    var IdCausa = [];
    $('#divCausaAdd input:checked').each(function () {
        IdCausa.push($(this).attr('value'));
    });   
    if (IdCausa == '') {
        swal({
            type: 'error',
            title: 'Aviso',
            text: 'Selecione Causa de Anomalia'
        }).then(
             function () {
                 return false;
             });
        return false;
    }

    //cria lista de Ids do Alerta
    var IdAlerta = [];
    $('#divAlertaAdd input:checked').each(function () {
        IdAlerta.push($(this).attr('value'));
    });
    if (IdAlerta == '') {
        swal({
            type: 'error',
            title: 'Aviso',
            text: 'Selecione Alerta'
        }).then(
             function () {
                 return false;
             });
        return false;
    }
      

    var Grupo = [
        { Nome: 'Alerta', Qtd: IdAlerta.length, Ids: [IdAlerta] },
        { Nome: 'Causa', Qtd: IdCausa.length, Ids: [IdCausa] },
        { Nome: 'Anomalia', Qtd: CodAnomalia.length, Ids: [CodAnomalia] }
    ];

    function sortfunction(a, b) {
        return (a.Qtd > b.Qtd) ? 1 : ((b.Qtd > a.Qtd) ? -1 : 0);
    }   
    Grupo.sort(sortfunction);
    
    if (Grupo[0].Nome == 'Alerta') {
        for (var a = 0; a < IdAlerta.length; a++) {
            if (Grupo[1].Nome == 'Causa') {
                for (var c = 0; c < IdCausa.length; c++) {
                    for (var an = 0; an < CodAnomalia.length; an++) {
                        $.ajax({
                            url: '/Reparo/PoliticaReparo_Inserir',
                            type: "Post",
                            data: { rpt_id: $('#cmbTiposReparo').val(), leg_id: $('#cmbLegenda').val(), atp_id: CodAnomalia[an], ale_id: IdAlerta[a], aca_id: IdCausa[c] },
                            dataType: "JSON",
                            success: function (data) {
                               
                            },
                            error: function (erro) {
                                swal({
                                    type: 'error',
                                    title: 'Aviso',
                                    text: 'Erro ao inserir registro'
                                });
                            }
                        });
                    }
                }
            }
        }
    }
    if (Grupo[0].Nome == 'Alerta') {
        for (var a = 0; a < IdAlerta.length; a++) {
            if (Grupo[1].Nome == 'Anomalia') {
                //console.log(IdAlerta);
                for (var an = 0; an < CodAnomalia.length; an++) {
                    for (var c = 0; c < IdCausa.length; c++) {
                        $.ajax({
                            url: '/Reparo/PoliticaReparo_Inserir',
                            type: "Post",
                            data: { rpt_id: $('#cmbTiposReparo').val(), leg_id: $('#cmbLegenda').val(), atp_id: CodAnomalia[an], ale_id: IdAlerta[a], aca_id: IdCausa[c] },
                            dataType: "JSON",
                            success: function (data) {
                              
                            },
                            error: function (erro) {

                            }
                        });
                    }
                }
            }
        }
    }
    if (Grupo[0].Nome == 'Causa') {
        for (var c = 0; c < IdCausa.length; c++) {
            if (Grupo[1].Nome == 'Alerta') {
                for (var a = 0; a < IdAlerta.length; a++) {
                    for (var an = 0; an < CodAnomalia.length; an++) {
                        $.ajax({
                            url: '/Reparo/PoliticaReparo_Inserir',
                            type: "Post",
                            data: { rpt_id: $('#cmbTiposReparo').val(), leg_id: $('#cmbLegenda').val(), atp_id: CodAnomalia[an], ale_id: IdAlerta[a], aca_id: IdCausa[c] },
                            dataType: "JSON",
                            success: function (data) {
                              
                            },
                            error: function (erro) {
                               
                            }
                        });
                    }
                }
            }
        }
    }
    if (Grupo[0].Nome == 'Causa') {
        for (var c = 0; c < IdCausa.length; c++) {
            if (Grupo[1].Nome == 'Anomalia') {
                for (var an = 0; an < CodAnomalia.length; an++) {
                    for (var a = 0; a < IdAlerta.length; a++) {
                        $.ajax({
                            url: '/Reparo/PoliticaReparo_Inserir',
                            type: "Post",
                            data: { rpt_id: $('#cmbTiposReparo').val(), leg_id: $('#cmbLegenda').val(), atp_id: CodAnomalia[an], ale_id: IdAlerta[a], aca_id: IdCausa[c] },
                            dataType: "JSON",
                            success: function (data) {

                            },
                            error: function (erro) {

                            }
                        });
                    }
                }
            }
        }
    }
    if (Grupo[0].Nome == 'Anomalia') {
        for (var an = 0; an < CodAnomalia.length; an++) {
            if (Grupo[1].Nome == 'Causa') {
                for (var c = 0; c < IdCausa.length; c++) {
                    for (var a = 0; a < IdAlerta.length; a++) {
                        $.ajax({
                            url: '/Reparo/PoliticaReparo_Inserir',
                            type: "Post",
                            data: { rpt_id: $('#cmbTiposReparo').val(), leg_id: $('#cmbLegenda').val(), atp_id: CodAnomalia[an], ale_id: IdAlerta[a], aca_id: IdCausa[c] },
                            dataType: "JSON",
                            success: function (data) {
                                
                            },
                            error: function (erro) {
                               
                            }
                        });
                    }
                }
            }
        }
    }
    if (Grupo[0].Nome == 'Anomalia') {
        for (var an = 0; an < CodAnomalia.length; an++) {
            if (Grupo[1].Nome == 'Alerta') {
                for (var a = 0; a < IdAlerta.length; a++) {
                    for (var c = 0; c < IdCausa.length; c++) {
                        $.ajax({
                            url: '/Reparo/PoliticaReparo_Inserir',
                            type: "Post",
                            data: { rpt_id: $('#cmbTiposReparo').val(), leg_id: $('#cmbLegenda').val(), atp_id: CodAnomalia[an], ale_id: IdAlerta[a], aca_id: IdCausa[c] },
                            dataType: "JSON",
                            success: function (data) {

                            },
                            error: function (erro) {

                            }
                        });
                    }
                }
            }
        }
    }
   
    $('#divAlertaAdd').empty();
    $('#divCodAnomaliaUp').empty();

    $('#cmdCausaAdd').empty();
    $('#cmbTiposReparo').val("0-0").change();
    $('#cmbAlerta').val("0-0").change();
   
    //    alert('Reparos incluido com sucesso!');
    swal({
        type: 'success',
        title: 'Sucesso',
        text: 'Política(s) incluída(s) com sucesso'
    });

    $("#modalNovo").modal('hide');

    if ($('#cmbAlerta').val() == "0-0") {
        GetAll();
    }
    else {
        btnExecutarFiltro_onclick();
    }
}

function DeleteReparo(rpp_id) {


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
            var response = POST("/Reparo/PoliticaReparo_Excluir", JSON.stringify({ rpp_id: rpp_id }))
            if (response.erroId >= 1) {
                swal({
                    type: 'success',
                    title: 'Sucesso',
                    text: 'Registro excluído com sucesso'
                });

              //  carregaGridPoliticaReparos();
                $('#tblSubs2').DataTable().ajax.reload(null, false);
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

}




