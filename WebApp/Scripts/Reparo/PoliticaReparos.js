

preencheRep();
preencheLegenda();
GetAll($('#GridHome'));

function OpenModalReparo() {
    $("#modalNovo").modal('show');
    preencheRep();
    preencheLegenda();
}

function closeModalReparo() {
    $("#modalNovo").modal('hide');
}

function preencheLegenda() {
    $.ajax({
        url: '/PoliticaReparos/PreencheLeg',
        type: "Get",
        dataType: "JSON",
        success: function (data) {
            $('#cmdLegAdd').empty();
            $('#cmdLegAdd').append($('<option selected disabled></option>').val(-1).html("--Selecione--")); // 1o item vazio
            $('#cmdLegAdd_in').empty();
            $('#cmdLegAdd_in').append($('<option selected disabled></option>').val(-1).html("--Selecione--")); // 1o item vazio
            $.each(data, function (i, item) {
                $('#cmdLegAdd').append($('<option value=' + item.Id + '> ' + item.leg_codigo + ' - ' + item.leg_descricao + '</option>'));
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
                taglbl = taglbl.replace("TextoXXX", item.atp_descricao);

                $("#divCodAnomaliaUp").append(tagchk + taglbl);
              
            });
            //$('select[multiple]').multiselect();
        }
        
    });

    prencheAlCa();
}

function preencheAnomalia_in() {
    $.ajax({
        url: '/PoliticaReparos/PreencheAno?id=' + $('#cmdLegAdd_in option:selected').val(),
        type: "Get",
        dataType: "JSON",
        success: function (data) {
            $('#cmdCodAnomaliaAdd_in').empty();
            $('#cmdCodAnomaliaAdd_in').append($('<option selected disabled></option>').val(-1).html("--Selecione--")); // 1o item vazio
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
           
            $('#cmdAlertaAdd_in').empty();
            $('#cmdAlertaAdd_in').append($('<option selected disabled></option>').val(-1).html("--Selecione--")); // 1o item vazio
            $.each(data, function (i, item) {
                var tagchk = '<input type="checkbox" id="idXXX" nome="nameXXX" value="valueXXX" style="margin-right:5px">';
                tagchk = tagchk.replace("idXXX", "chk" + i);
                tagchk = tagchk.replace("nameXXX", "chk" + i);
                tagchk = tagchk.replace("valueXXX", item.ale_id);
                var taglbl = '<label for="idXXX" class="chklst" >TextoXXX</label> <br />';
                taglbl = taglbl.replace("idXXX", "chk" + i);
                taglbl = taglbl.replace("TextoXXX", item.ale_descricao);
                $("#divAlertaAdd").append(tagchk + taglbl);

                $('#cmdAlertaAdd_in').append($('<option value=' + item.ale_id + '> ' + item.ale_codigo + ' - ' + item.ale_descricao + ' </option>'));
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
                taglbl = taglbl.replace("TextoXXX", item.aca_descricao);
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
            $('#cmdCausaAdd_in').append($('<option selected disabled></option>').val(-1).html("--Selecione--")); // 1o item vazio

            $.each(data, function (i, item) {
                $('#cmdCausaAdd_in').append($('<option value=' + item.aca_id + '>' + item.aca_descricao + ' </option>'));
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
            $('#cmbReparoAdd').append($('<option selected disabled></option>').val(-1).html("--Selecione--")); // 1o item vazio
            $('#cmbReparoAdd_in').empty();
            $('#cmbReparoAdd_in').append($('<option selected disabled></option>').val(-1).html("--Selecione--")); // 1o item vazio
            $.each(data, function (i, item) {
                $('#cmbReparoAdd').append($('<option value=' + item.rpt_id + '> ' + item.rpt_codigo + ' - ' + item.rpt_descricao + ' </option>'));
                $('#cmbReparoAdd_in').append($('<option value=' + item.rpt_id + '> ' + item.rpt_codigo + ' - ' + item.rpt_descricao + ' </option>'));
            });
        }
    });
}

function GetAll(table) {

    $.ajax({
        url: '/PoliticaReparos/PreencheRepAll',
        type: "Get",
        dataType: "JSON",
        success: function (data) {
            table.html("");
            $.each(data, function (i, valor) {
                table.append($('<tr><td tyle="text-align:center" title="' + valor.rpt_descricao + '">' + valor.rpt_codigo + '</td><td tyle="text-align:center" title="' + valor.leg_descricao + '">' + valor.leg_codigo + '</td><td tyle="text-align:center" title="' + valor.atp_descricao + '">' + valor.atp_codigo + '</td><td style="text-align:center">' + valor.ale_codigo + '</td><td tyle="text-align:center" >' + valor.aca_descricao + '</td><td style="text-align:center"><a href="#" onclick="return DeleteReparo(' + valor.rpp_id + ')" title="Editar"><span class="glyphicon glyphicon-trash"></span></a></td></tr>'));
            });
            paginar();  
            
        }
    });
}

function paginar() {
    $(document).ready(function () {
        $('#tblSubs').DataTable({
            "language": {
                "lengthMenu": "Mostrando _MENU_ registros por página",
                "zeroRecords": "Nada encontrado",
                "info": "Mostrando página _PAGE_ de _PAGES_",
                "infoEmpty": "Nenhum registro disponível",
                "infoFiltered": "(filtrado de _MAX_ registros no total)"
            }
        });
    });
}

function btnAddReparo() {
    
  
    // cria lista dos IDs de Cod Anomalia
    var CodAnomalia = [];
    $('#divCodAnomaliaUp input:checked').each(function () {
        CodAnomalia.push($(this).attr('value'));
    });

    //cria lista de Ids do Alerta
    var IdAlerta = [];
    $('#divAlertaAdd input:checked').each(function () {
        IdAlerta.push($(this).attr('value'));
    });

    //cria lista de Ids do Causa
    var IdCausa = [];
    $('#cmdCausaAdd input:checked').each(function () {
        IdCausa.push($(this).attr('value'));
    });    

    var Grupo = [
        { Nome: 'Alerta', Qtd: IdAlerta.length, Ids: [IdAlerta] },
        { Nome: 'Causa', Qtd: IdCausa.length, Ids: [IdCausa] },
        { Nome: 'Anomalia', Qtd: CodAnomalia.length, Ids: [CodAnomalia] }
    ];

    function sortfunction(a, b) {
        if (a.Qtd < b.Qtd) return -1;
        if (a.Qtd > b.Qtd) return 1;
        return 0;
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

    $('#divAlertaAdd').empty();
    $('#cmdCausaAdd').empty();
    $('#divCodAnomaliaUp').empty();
    alert('Reparos incluido com sucesso!');
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
            $('#GridHome').html("");
            $.each(data, function (i, valor) {
                $('#GridHome').append($('<tr><td tyle="text-align:center" title="' + valor.rpt_descricao + '">' + valor.rpt_codigo + '</td><td tyle="text-align:center" title="' + valor.leg_descricao + '">' + valor.leg_codigo + '</td><td tyle="text-align:center" title="' + valor.atp_descricao + '">' + valor.atp_codigo + '</td><td style="text-align:center">' + valor.ale_codigo + '</td><td tyle="text-align:center" >' + valor.aca_descricao + '</td><td style="text-align:center"><a href="#" onclick="return DeleteReparo(' + valor.rpp_id + ')" title="Editar"><span class="glyphicon glyphicon-trash"></span></a></td></tr>'));
            });

        },
        error: function (erro) {

        }
    });
}