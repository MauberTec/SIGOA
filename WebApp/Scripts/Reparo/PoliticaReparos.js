preencheRep_in();
preencheLegenda_in();
GetAll($('#GridHome'));

function OpenModalReparo() {
    $("#modalNovo").modal('show');
    preencheRep();
    preencheLegenda();
}

function closeModalReparo() {
    $("#modalNovo").modal('hide');
    if ($('#cmdLegAdd').val() == "0-0") {
        GetAll();
    }
    else {
        Buscar();
    }

}

function preencheLegenda() {
    $.ajax({
        url: '/PoliticaReparos/PreencheLeg',
        type: "Get",
        dataType: "JSON",
        success: function (data) {
            $('#cmdLegAdd').empty();
            $('#cmdLegAdd').append($('<option selected></option>').val("0-0").html("--Selecione--")); // 1o item vazio
            $.each(data, function (i, item) {
                $('#cmdLegAdd').append($('<option value=' + item.Id + '> ' + item.leg_codigo + ' - ' + item.leg_descricao + '</option>'));
            });
        }
    });
}

function preencheLegenda_in() {
    $.ajax({
        url: '/PoliticaReparos/PreencheLeg',
        type: "Get",
        dataType: "JSON",
        success: function (data) {
            $('#cmdLegAdd_in').empty();
            $('#cmdLegAdd_in').append($('<option selected ></option>').val("0-0").html("--Selecione--")); // 1o item vazio
            $.each(data, function (i, item) {
                $('#cmdLegAdd_in').append($('<option value=' + item.Id + '> ' + item.leg_codigo + ' - ' + item.leg_descricao + '</option>'));
            });
        }
    });
}


function preencheAnomalia() {
    $.ajax({
        url: '/PoliticaReparos/PreencheAno?id=' + $('#cmdLegAdd option:selected').val(),
        type: "Get",
        dataType: "JSON",
        success: function (data) {
           
            $('#divCodAnomaliaUp').empty();           
            $.each(data, function (i, item) {
                var tagchk = '<input type="checkbox" id="idXXX" nome="nameXXX" value="valueXXX" style="margin-right:5px">';
                tagchk = tagchk.replace("idXXX", "chk" + i);
                tagchk = tagchk.replace("nameXXX", "chk" + i);
                tagchk = tagchk.replace("valueXXX", item.atp_id);

                var taglbl = '<label for="idXXX" class="chklst" >TextoXXX</label> <br />';
                taglbl = taglbl.replace("idXXX", "chk" + i);
                taglbl = taglbl.replace("TextoXXX", item.atp_codigo + '-' + item.atp_descricao);

                $("#divCodAnomaliaUp").append(tagchk + taglbl);
              
            });
            //$('select[multiple]').multiselect();
        }
        
    });

    prencheAlCa();
}

function preencheAnomalia_in() {
    preencheCausa_in();
    $.ajax({
        url: '/PoliticaReparos/PreencheAno?id=' + $('#cmdLegAdd_in option:selected').val(),
        type: "Get",
        dataType: "JSON",
        success: function (data) {
            $('#cmdCodAnomaliaAdd_in').empty();
            $('#cmdCodAnomaliaAdd_in').append($('<option selected ></option>').val("0-0").html("--Selecione--")); // 1o item vazio
            $.each(data, function (i, item) {
                $('#cmdCodAnomaliaAdd_in').append($('<option value=' + item.atp_id + '> ' + item.atp_codigo + ' - ' + item.atp_descricao + ' </option>'));
                
            });
        }
    });
}

function prencheAlCa() {
    preencheAlerta();
    preencheCausa($('#cmdLegAdd').val());
   
}

function preencheAlerta() {
    $.ajax({
        url: '/PoliticaReparos/PreencheAlerta',
        type: "Get",
        dataType: "JSON",
        success: function (data) {
            $('#divAlertaAdd').empty();
            $.each(data, function (i, item) {
                var tagchk = '<input type="checkbox" id="idXXX" nome="nameXXX" value="valueXXX" style="margin-right:5px">';
                tagchk = tagchk.replace("idXXX", "chk" + i);
                tagchk = tagchk.replace("nameXXX", "chk" + i);
                tagchk = tagchk.replace("valueXXX", item.ale_id);
                var taglbl = '<label for="idXXX" class="chklst" >TextoXXX</label> <br />';
                taglbl = taglbl.replace("idXXX", "chk" + i);
                taglbl = taglbl.replace("TextoXXX", item.ale_codigo + '-'+ item.ale_descricao);
                $("#divAlertaAdd").append(tagchk + taglbl);

               
            });
        }
    });
}
function preencheCausa(id) {
   
    $.ajax({
        url: '/PoliticaReparos/PreencheCausa?id=' + id,
        type: "Get",
        dataType: "JSON",
        success: function (data) {
            $('#cmdCausaAdd').empty();
           
            $.each(data, function (i, item) {
                var tagchk = '<input type="checkbox" id="idXXX" nome="nameXXX" value="valueXXX" style="margin-right:5px">';
                tagchk = tagchk.replace("idXXX", "chk" + i);
                tagchk = tagchk.replace("nameXXX", "chk" + i);
                tagchk = tagchk.replace("valueXXX", item.aca_id);
                var taglbl = '<label for="idXXX" class="chklst" >TextoXXX</label> <br />';
                taglbl = taglbl.replace("idXXX", "chk" + i);
                taglbl = taglbl.replace("TextoXXX",item.aca_codigo + '-'+ item.aca_descricao);
                $("#cmdCausaAdd").append(tagchk + taglbl);
                
            });
        }
    });
}

function preencheCausa_in() {

    $.ajax({
        url: '/PoliticaReparos/PreencheCausa?id=' + $('#cmdLegAdd_in').val(),
        type: "Get",
        dataType: "JSON",
        success: function (data) {
           
            $('#cmdCausaAdd_in').empty();
            $('#cmdCausaAdd_in').append($('<option selected ></option>').val("0-0").html("--Selecione--")); // 1o item vazio

            $.each(data, function (i, item) {
                $('#cmdCausaAdd_in').append($('<option value=' + item.aca_id + '>'+item.aca_codigo+' - ' + item.aca_descricao + ' </option>'));
            });
        }
    });
}

function preencheRep_in() {
    $.ajax({
        url: '/PoliticaReparos/PreencheRep',
        type: "Get",
        dataType: "JSON",
        success: function (data) {
            $('#cmbReparoAdd_in').empty();
            $('#cmbReparoAdd_in').append($('<option ></option>').val("0-0").html("--Selecione--")); // 1o item vazio
            $.each(data, function (i, item) {
                $('#cmbReparoAdd_in').append($('<option value=' + item.rpt_id + '> ' + item.rpt_codigo + ' - ' + item.rpt_descricao + ' </option>'));
            });
        }
    });
}

function preencheRep() {
    $.ajax({
        url: '/PoliticaReparos/PreencheRep',
        type: "Get",
        dataType: "JSON",
        success: function (data) {
            $('#cmbReparoAdd').empty();
            $('#cmbReparoAdd').append($('<option ></option>').val("0-0").html("--Selecione--")); // 1o item vazio
            $.each(data, function (i, item) {
                $('#cmbReparoAdd').append($('<option value=' + item.rpt_id + '> ' + item.rpt_codigo + ' - ' + item.rpt_descricao + ' </option>'));
            });
        }
    });
}

function GetAll() {
    $('#msg').val('Aguarde...');
    $.ajax({
        url: '/PoliticaReparos/PreencheRepAll',
        type: "Get",
        dataType: "JSON",
        success: function (data) {
            $('#DivGrid').empty();
            $('#DivGrid').append('<table id="tblSubs" class="no-footer dataTable">' +
                '<thead>' +
                '<tr>' +
                '<th style="width:40px; text-align:center">Reparo</th>' +
                '<th style="width:70px; text-align:center">Legenda</th>' +
                '<th style="width:270px; text-align:center">Anomalia</th>' +
                '<th style="width:70px; text-align:center">Alerta</th>' +
                '<th style="text-align:center">Causa</th>' +
                '<th style="text-align:center">Opções</th>' +
                '</tr>' +
                '</thead>' +
                '<tbody id="GridHome">' +
                '</tbody>' +
                '</table >');
            
            $.each(data, function (i, valor) {
                $('#GridHome').append($('<tr><td tyle="text-align:center" title="' + valor.rpt_descricao + '">' + valor.rpt_codigo + '</td><td tyle="text-align:center" title="' + valor.leg_descricao + '">' + valor.leg_codigo + '</td><td tyle="text-align:center" title="' + valor.atp_descricao + '">' + valor.atp_codigo + ' - ' + valor.atp_descricao + '  </td><td style="text-align:center">' + valor.ale_codigo + '</td><td tyle="text-align:center" >' + valor.aca_codigo + '- ' + valor.aca_descricao + '</td><td style="text-align:center"><a href="#" onclick="return DeleteReparo(' + valor.rpp_id + ')" title="Deletar"><span class="glyphicon glyphicon-trash"></span></a></td></tr>'));
            });
            $('#msg').hide();
            paginar();  
            
        }
    });
}

function paginar() {
    $(document).ready(function () {
        $('#tblSubs').DataTable({
            "oLanguage": idioma
            , "pagingType": "input"
            , "sDom": '<"top">rt<"bottom"pfli><"clear">'
        });
    });
}

function btnAddReparo() {
    if ($('#cmbReparoAdd').val() == null) {
        alert('Selecione Reparo');
        return;
    }
    if ($('#cmdLegAdd').val() == null)
    {
        alert('Selecione a Legenda');
        return;
    }
  
    // cria lista dos IDs de Cod Anomalia
    var CodAnomalia = [];
    $('#divCodAnomaliaUp input:checked').each(function () {
        CodAnomalia.push($(this).attr('value'));
    });
    if (CodAnomalia == '') {
        alert('Selecione Cod Anomalia');
        return;
    }

    //cria lista de Ids do Causa
    var IdCausa = [];
    $('#cmdCausaAdd input:checked').each(function () {
        IdCausa.push($(this).attr('value'));
    });   
    if (IdCausa == '') {
        alert('Selecione Causa');
        return;
    }

    //cria lista de Ids do Alerta
    var IdAlerta = [];
    $('#divAlertaAdd input:checked').each(function () {
        IdAlerta.push($(this).attr('value'));
    });
    if (IdAlerta == '') {
        alert('Selecione Alerta');
        return;
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
                            url: '/PoliticaReparos/InsertPoliticaReparo',
                            type: "Post",
                            data: { rpt_id: $('#cmbReparoAdd').val(), leg_codigo: $('#cmdLegAdd').val(), atp_codigo: CodAnomalia[an], ale_codigo: IdAlerta[a], aca_id: IdCausa[c] },
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
    if (Grupo[0].Nome == 'Alerta') {
        for (var a = 0; a < IdAlerta.length; a++) {
            if (Grupo[1].Nome == 'Anomalia') {
                console.log(IdAlerta);
                for (var an = 0; an < CodAnomalia.length; an++) {
                    for (var c = 0; c < IdCausa.length; c++) {
                        $.ajax({
                            url: '/PoliticaReparos/InsertPoliticaReparo',
                            type: "Post",
                            data: { rpt_id: $('#cmbReparoAdd').val(), leg_codigo: $('#cmdLegAdd').val(), atp_codigo: CodAnomalia[an], ale_codigo: IdAlerta[a], aca_id: IdCausa[c] },
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
                            url: '/PoliticaReparos/InsertPoliticaReparo',
                            type: "Post",
                            data: { rpt_id: $('#cmbReparoAdd').val(), leg_codigo: $('#cmdLegAdd').val(), atp_codigo: CodAnomalia[an], ale_codigo: IdAlerta[a], aca_id: IdCausa[c] },
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
                            url: '/PoliticaReparos/InsertPoliticaReparo',
                            type: "Post",
                            data: { rpt_id: $('#cmbReparoAdd').val(), leg_codigo: $('#cmdLegAdd').val(), atp_codigo: CodAnomalia[an], ale_codigo: IdAlerta[a], aca_id: IdCausa[c] },
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
                            url: '/PoliticaReparos/InsertPoliticaReparo',
                            type: "Post",
                            data: { rpt_id: $('#cmbReparoAdd').val(), leg_codigo: $('#cmdLegAdd').val(), atp_codigo: CodAnomalia[an], ale_codigo: IdAlerta[a], aca_id: IdCausa[c] },
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
                            url: '/PoliticaReparos/InsertPoliticaReparo',
                            type: "Post",
                            data: { rpt_id: $('#cmbReparoAdd').val(), leg_codigo: $('#cmdLegAdd').val(), atp_codigo: CodAnomalia[an], ale_codigo: IdAlerta[a], aca_id: IdCausa[c] },
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
    $('#cmdCausaAdd').empty();
    $('#divCodAnomaliaUp').empty();
    $("#divAlertaAdd").empty();

    $('#cmbReparoAdd').val("0-0").change();
    $('#cmdLegAdd').val("0-0").change();
   
    alert('Reparos incluido com sucesso!');
    if ($('#cmdLegAdd').val() == "0-0") {
        GetAll();
    }
    else {
        Buscar();
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
            $.ajax({
                url: '/PoliticaReparos/DeleteReparo?rpp_id=' + rpp_id,
                type: "Post",
                dataType: "JSON",
                success: function (data) {
                    alert('Reparo deletado com sucesso!'); 
                    $('#DivGrid').empty();
                    Buscar();
                },
                error: function (erro) {
                    alert(erro);
                }
            });
        }
    });
   
}

function Buscar() {

    $.ajax({
        url: '/PoliticaReparos/BuscaReparo',
        type: "Post",
        data: { rpt_id: $('#cmbReparoAdd_in').val(), leg_id: $('#cmdLegAdd_in').val(), atp_id: $('#cmdCodAnomaliaAdd_in').val(), ale_id: $('#cmdAlertaAdd_in').val(), aca_id: $('#cmdCausaAdd_in').val() },
        dataType: "JSON",
        success: function (data) {
            $('#DivGrid').empty();
            $('#DivGrid').append('<table id="tblSubs" class="no-footer dataTable">' +
                '<thead>' +
                '<tr>' +
                '<th style="width:40px; text-align:center">Reparo</th>' +
                '<th style="width:70px; text-align:center">Legenda</th>' +
                '<th style="width:270px; text-align:center">Anomalia</th>' +
                '<th style="width:70px; text-align:center">Alerta</th>' +
                '<th style="text-align:center">Causa</th>' +
                '<th style="text-align:center">Opções</th>' +
                '</tr>' +
                '</thead>' +
                '<tbody id="GridHome">' +
                '</tbody>' +
                '</table >');
            $.each(data, function (i, valor) {
                $('#GridHome').append($(


                    '<tr><td tyle="text-align:center" title="' + valor.rpt_descricao + '">' + valor.rpt_codigo + '</td><td tyle="text-align:center" title="' + valor.leg_descricao + '">' + valor.leg_codigo + '</td><td tyle="text-align:center" title="' + valor.atp_descricao + '">' + valor.atp_codigo + ' - ' + valor.atp_descricao + '  </td><td style="text-align:center">' + valor.ale_codigo + '</td><td tyle="text-align:center" >' + valor.aca_codigo + '- ' + valor.aca_descricao + '</td><td style="text-align:center"><a href="#" onclick="return DeleteReparo(' + valor.rpp_id + ')" title="Editar"><span class="glyphicon glyphicon-trash"></span></a></td></tr>'
                )

                );
            });
            paginar();

        },
        error: function (erro) {

        }
    });
}

function Limpar() {
    
    $("#cmbReparoAdd_in").val("0-0").change();
    $("#cmdLegAdd_in").val("0-0").change();
    $("#cmdCodAnomaliaAdd_in").val("0-0").change();
    $("#cmdAlertaAdd_in").val("0-0").change();
    GetAll($('#GridHome'));
}