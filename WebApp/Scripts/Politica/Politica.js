GetConserva();
GetGrupos();
GetVariavel();
GetAlerta();
GetGetBuscaTodos();

function GetConserva() {
    $.ajax({
        url: '/PoliticaConserva/GConserva',
        type: "Get",
        dataType: "JSON",
        success: function (data) {

            $('#ComboConserva2').empty();
            $('#ComboConserva').empty();
            $("#ComboConserva").append($('<option value="0">--Selecione--</option>'));
            $.each(data, function (i, item) {

                $("#ComboConserva3").append($('<option value=' + item.cot_id + '>' + item.cot_descricao + '  </option>'));
                $("#ComboConserva2").append($('<option value=' + item.cot_id + '>' + item.cot_descricao + '  </option>'));
                $("#ComboConserva").append($('<option value=' + item.cot_id + '>' + item.cot_descricao + '  </option>'));

            });
            //$('select[multiple]').multiselect();
        }

    });
}

function GetGrupos() {
    $.ajax({
        url: '/PoliticaConserva/GGrupo',
        type: "Get",
        dataType: "JSON",
        success: function (data) {

            $('#divCodAnomaliaUp').empty();
            $('#ComboGrupo').empty();
            $("#ComboGrupo").append($('<option value="0">--Selecione--</option>'));
            $.each(data, function (i, item) {
                var tagchk = '<input type="checkbox" id="idXXX" nome="nameXXX" value="valueXXX" style="margin-right:5px">';
                tagchk = tagchk.replace("idXXX", "chk" + i);
                tagchk = tagchk.replace("nameXXX", "chk" + i);
                tagchk = tagchk.replace("valueXXX", item.tip_id);

                var taglbl = '<label for="idXXX" class="chklst" >TextoXXX</label> <br />';
                taglbl = taglbl.replace("idXXX", "chk" + i);
                taglbl = taglbl.replace("TextoXXX", item.tip_id + '-' + item.tip_nome);

                $("#ComboGrupo2").append(tagchk + taglbl);
                $("#ComboGrupo").append($('<option value=' + item.tip_id + '>' + item.tip_nome + '  </option>'));
                $("#ComboGrupo3").append($('<option value=' + item.tip_id + '>' + item.tip_nome + '  </option>'));

            });
            //$('select[multiple]').multiselect();
        }

    });
}

function GetVariavel() {
    $.ajax({
        url: '/PoliticaConserva/GVariavel',
        type: "Get",
        dataType: "JSON",
        success: function (data) {

            $('#ComboVariavel2').empty();
            $('#ComboVariavel').empty();
            $("#ComboVariavel").append($('<option value="0">--Selecione--</option>'));
            $.each(data, function (i, item) {
                var tagchk = '<input type="checkbox" id="idXXX" nome="nameXXX" value="valueXXX" style="margin-right:5px">';
                tagchk = tagchk.replace("idXXX", "chk" + i);
                tagchk = tagchk.replace("nameXXX", "chk" + i);
                tagchk = tagchk.replace("valueXXX", item.cov_id);

                var taglbl = '<label for="idXXX" class="chklst" >TextoXXX</label> <br />';
                taglbl = taglbl.replace("idXXX", "chk" + i);
                taglbl = taglbl.replace("TextoXXX", item.cov_id + '-' + item.cov_nome);

                $("#ComboVariavel2").append(tagchk + taglbl);
                $("#ComboVariavel").append($('<option value=' + item.cov_id + '>' + item.cov_nome + '  </option>'));
                $("#ComboVariavel3").append($('<option value=' + item.cov_id + '>' + item.cov_nome + '  </option>'));

            });
            //$('select[multiple]').multiselect();
        }

    });
}

function GetAlerta() {
    $.ajax({
        url: '/PoliticaConserva/GAlerta',
        type: "Get",
        dataType: "JSON",
        success: function (data) {

            $('#divCodAnomaliaUp').empty();
            $.each(data, function (i, item) {
                var tagchk = '<input type="checkbox" id="idXXX" nome="nameXXX" value="valueXXX" style="margin-right:5px">';
                tagchk = tagchk.replace("idXXX", "chk" + i);
                tagchk = tagchk.replace("nameXXX", "chk" + i);
                tagchk = tagchk.replace("valueXXX", item.ale_id);

                var taglbl = '<label for="idXXX" class="chklst" >TextoXXX</label> <br />';
                taglbl = taglbl.replace("idXXX", "chk" + i);
                taglbl = taglbl.replace("TextoXXX", item.ale_id + '-' + item.ale_descricao);

                $("#ComboAlerta2").append(tagchk + taglbl);
                $("#ComboAlerta3").append($('<option value=' + item.ale_id + '>' + item.ale_descricao + '  </option>'));

            });
            //$('select[multiple]').multiselect();
        }

    });
}

function OpenComboModal() {
    $('#modalSalvarRegistro').modal('show');

}


function GetGetBusca() {

    $.ajax({
        url: '/Politicaconserva/Perquisar?cot_id=' + $('#ComboConserva option:selected').val() + '&cov_id=' + $('#ComboVariavel option:selected').val() + '&tip_id=' + $('#ComboGrupo option:selected').val(),
        type: "Get",
        dataType: "JSON",
        success: function (data) {
            $('#DivGrid').empty();
            $('#DivGrid').append('<table id="tblSubs">' +
                '<thead>' +
                '<tr>' +
                '<th style="width:80px">Conserva</th>' +
                '<th style="width:110px">Grupo de Objetos</th> ' +
                '<th style="width:110px">Variavel</th> ' +
                '<th style="text-align:center">Alerta</th> ' +
                '<th style="text-align:center">Opções</th> ' +
                '</tr>' +
                '</thead> ' +
                '<tbody id="body">' +
                '</tbody>' +
                '</table >');
            $.each(data, function (i, item) {
                $("#body").append($('<tr><td>' + item.conserva + '</td><td>' + item.GrupoOBJ + '</td><td>' + item.Variavel + '</td><td style="text-align:center">' + item.ale_codigo + '</td><td style="text-align:center"><a href="#" onclick="return btnEdit_onclick(\'' + item.tip_id + '\', \'' + item.cot_id + '\', \'' + item.cov_id + '\', \'' + item.ale_id + '\', \'' + item.cop_id + '\')" title="Editar"><span class="glyphicon glyphicon-pencil"></span></a><a href="#" onclick="return Deleta(' + item.cop_id + ')" title="Editar"><span class="glyphicon glyphicon-trash"></span></a></td></tr>'));
            });
            paginar();
        }
    });
}

function GetGetBuscaTodos() {

    $.ajax({
        url: '/Politicaconserva/Perquisar?cot_id=0&cov_id=0&tip_id=0',
        type: "Get",
        dataType: "JSON",
        success: function (data) {
            $('#DivGrid').empty();
            $('#DivGrid').append('<table id="tblSubs">' +
                '<thead>' +
                '<tr>' +
                '<th style="width:80px">Conserva</th>' +
                '<th style="width:110px">Grupo de Objetos</th> ' +
                '<th style="width:110px">Variavel</th> ' +
                '<th style="text-align:center">Alerta</th> ' +
                '<th style="text-align:center">Opções</th> ' +
                '</tr>' +
                '</thead> ' +
                '<tbody id="body">' +
                '</tbody>' +
                '</table >');
            $.each(data, function (i, item) {
                $("#body").append($('<tr><td>' + item.conserva + '</td><td>' + item.GrupoOBJ + '</td><td>' + item.Variavel + '</td><td style="text-align:center">' + item.ale_codigo + '</td><td style="text-align:center"><a href="#" onclick="return btnEdit_onclick(\'' + item.tip_id + '\', \'' + item.cot_id + '\', \'' + item.cov_id + '\', \'' + item.ale_id + '\', \'' + item.cop_id + '\')" title="Editar"><span class="glyphicon glyphicon-pencil"></span></a><a href="#" onclick="return Deleta(' + item.cop_id + ')" title="Deletar"><span class="glyphicon glyphicon-trash"></span></a></td></tr>'));
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

function cmbGrupos_onchange() {

    $("#cmbVrInspec").html("");
    $("#cmbVarInspec").empty();
    $("#cmbVarInspec").append("<option value=''>--Selecione--</option>");
    $.ajax({
        url: '/Politicaconserva/Variaveis?Id=' + $('#cmbGrupos option:selected').val(),
        type: "Get",
        dataType: "JSON",
        success: function (data) {

            if (data.length !== 0) {

                $.each(data, function (i, item) {
                    $("#cmbVarInspec").append($('<option value=' + item.ogv_id + '>' + item.variavel + '</option>'));
                });
            }


        }
    });
}

function pesquisar_click() {


    $.ajax({
        url: '/Politicaconserva/Listar?sub1=' + $('#cmbSub1 option:selected').text()
            + '&sub2=' + $('#cmbSub2 option:selected').text()
            + '&sub3=' + $('#cmbSub3 option:selected').text()
            + '&grupo=' + $('#cmbGrupos option:selected').text()
            + '&variavel=' + $('#cmbVarInspec option:selected').text()
            + '&variavelId=' + $('#cmbVarInspec option:selected').val()
        ,
        type: "Get",
        dataType: "JSON",
        success: function (data) {
            if (data.length !== 0) {
                $("#corpo").html("");
                $.each(data, function (i, item) {
                    $("#corpo").append($('<tr><td>' + item.tip_nome + '</td><td>' + item.Grupo + '</td><td>' + item.Variavel + '</td><td style="text-align:center">' + item.tipo + '</td><td>' + item.alerta + '</td><td>' + item.servico + '</td><td style="text-align:center"><a href="#" onclick="return btnEdit_onclick(\'' + item.alerta + '\',\'' + item.servico + '\', \'' + item.ocp_id + '\')" title="Editar"><span class="glyphicon glyphicon-pencil"></span></a></td></tr>'));
                });
            }


        }
    });
}

function btnEdit_onclick(tip_id, cot_id, cov_id, ale_id, cop_id) {

    $("#modalEdit").modal('show');
    $("#ComboConserva3 select").val(cot_id);
    $("#ComboGrupo3 select").val(tip_id);
    $("#ComboVariavel3 select").val(cov_id);
    $("#ComboAlerta3 select").val(ale_id);
    $("#lblCopId").val(cop_id);
}


function btnEditSalvar_onclick() {

    $.ajax({
        url: '/Politicaconserva/Edti?ocp_id=' + $("#ocp_idEdit").val() + '&alerta=' + $("#descricaoEdit").val() + '&conserva=' + $("#conservaEdit").val(),
        type: "Get",
        dataType: "JSON",
        success: function (data) {
            if (data == 'ok') {
                $("#modalEdit").modal('hide');
                GetAll();
                return false;
            }
            else {
                alert(data)
            }

        },
        error: function (erro) {
            alert(erro);
        }
    });
}

function btnAddConserva() {
    if ($('#ComboConserva2').val() == null) {
        alert('Selecione Conserva');
        return;
    }

    // cria lista dos IDs de Cod Anomalia
    var ComboGrupo2 = [];
    $('#ComboGrupo2 input:checked').each(function () {
        ComboGrupo2.push($(this).attr('value'));
    });
    if (ComboGrupo2 == '') {
        alert('Selecione Grupo');
        return;
    }

    //cria lista de Ids do Causa
    var ComboVariavel2 = [];
    $('#ComboVariavel2 input:checked').each(function () {
        ComboVariavel2.push($(this).attr('value'));
    });
    if (ComboVariavel2 == '') {
        alert('Selecione Variavel');
        return;
    }

    //cria lista de Ids do Alerta
    var ComboAlerta2 = [];
    $('#ComboAlerta2 input:checked').each(function () {
        ComboAlerta2.push($(this).attr('value'));
    });
    if (ComboAlerta2 == '') {
        alert('Selecione Alerta');
        return;
    }
    var Grupo = [
        { Nome: 'Alerta', Qtd: ComboAlerta2.length, Ids: [ComboAlerta2] },
        { Nome: 'Grupo', Qtd: ComboGrupo2.length, Ids: [ComboGrupo2] },
        { Nome: 'Variavel', Qtd: ComboVariavel2.length, Ids: [ComboVariavel2] }
    ];

    function sortfunction(a, b) {
        if (a.Qtd < b.Qtd) return -1;
        if (a.Qtd > b.Qtd) return 1;
        return 0;
    }
    Grupo.sort(sortfunction);
    if (Grupo[0].Nome == 'Alerta') {
       
        for (var a = 0; a < ComboAlerta2.length; a++) {
            if (Grupo[1].Nome == 'Grupo') {
                for (var g = 0; g < ComboGrupo2.length; g++) {
                    for (var va = 0; va < ComboVariavel2.length; va++) {
                        $.ajax({
                            url: '/PoliticaConserva/ConservaTipoSalvar?tipid=' + ComboGrupo2[g] + '&alerta=' + ComboAlerta2[a] + '&cotid=' + $('#ComboConserva2').val() + '&covid=' + ComboVariavel2[va] + '&copid=0',
                            type: "Post",
                            dataType: "JSON"
                        });
                    }
                }
            }
        }
    }
    if (Grupo[0].Nome == 'Grupo') {
      
        for (var g = 0; g < ComboGrupo2.length; g++) {
            if (Grupo[1].Nome == 'Alerta') {
                for (var a = 0; a < ComboAlerta2.length; a++) {
                    for (var va = 0; va < ComboVariavel2.length; va++) {
                        $.ajax({
                            url: '/PoliticaConserva/ConservaTipoSalvar?tipid=' + ComboGrupo2[g] + '&alerta=' + ComboAlerta2[a] + '&cotid=' + $('#ComboConserva2').val() + '&covid=' + ComboVariavel2[va] + '&copid=0',
                            type: "Post",                            
                            dataType: "JSON"
                        });
                    }
                }
            }
        }
        if (Grupo[0].Nome == 'Variavel') {
            for (var va = 0; va < ComboVariavel2.length; va++) {
                if (Grupo[1].Nome == 'Alerta') {
                    for (var a = 0; a < ComboAlerta2.length; a++) {
                        for (var g = 0; g < ComboGrupo2.length; g++) {
                            $.ajax({
                                url: '/PoliticaConserva/ConservaTipoSalvar?tipid=' + ComboGrupo2[g] + '&alerta=' + ComboAlerta2[a] + '&cotid=' + $('#ComboConserva2').val() + '&covid=' + ComboVariavel2[va] + '&copid=0',
                                type: "Post",                                
                                dataType: "JSON"
                            });
                        }
                    }
                }
            }
        }

        if (Grupo[0].Nome == 'Variavel') {
            for (var va = 0; va < ComboVariavel2.length; va++) {
                if (Grupo[1].Nome == 'Grupo') {
                    for (var g = 0; g < ComboGrupo2.length; g++) {
                        for (var a = 0; a < ComboAlerta2.length; a++) {
                            $.ajax({
                                url: '/PoliticaConserva/ConservaTipoSalvar?tipid=' + ComboGrupo2[g] + '&alerta=' + ComboAlerta2[a] + '&cotid=' + $('#ComboConserva2').val() + '&covid=' + ComboVariavel2[va] + '&copid=0',
                                type: "Post",
                                dataType: "JSON"
                            });
                        }
                    }
                }
            }
        }

    }

    $('#modalSalvarRegistro').modal('hide');
    alert('Conserva incluida com sucesso!');
    return;
}

function btnAditarSalvarConserva() {
    $.ajax({
        url: '/PoliticaConserva/ConservaTipoSalvar?tipid=' + $('#ComboGrupo3').val() + '&alerta=' + $('#ComboAlerta3').val() + '&cotid=' + $('#ComboConserva3').val() + '&covid=' + $('#ComboVariavel3').val() + '&copid=' + $('#lblCopId').val() ,
        type: "Post",        
        dataType: "JSON",
        success: function (data) {
            alert('Conserva atualizada com sucesso!');
            $('#modalEdit').modal('hide');
        },
        error: function (erro) {
            alert(erro)
        }
    });
}
function Deleta(id) {
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
                url: '/PoliticaConserva/Deleta?cop_id=' + id,
                type: "Post",
                dataType: "JSON",
                success: function (data) {
                    alert('Conserva excluida com sucesso ')
                },
                error: function (erro) {
                    alert(erro);
                }
            });
        }
    });
}
