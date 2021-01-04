
var selectedale_id;

function AnomAlerta_Inserir() {
    var corBranca = "rgb(255, 255, 255)";
    $('#txtale_codigo').css("background-color", corBranca);
    $('#txtale_descricao').css("background-color", corBranca);

    $('#txtale_codigo').val("");
    $('#txtale_descricao').val("");
    $('#chkale_ativo').prop('checked', true);

    $('#txtAnomAlerta').css('border-color', 'lightgrey');
    $('#chkale_ativo').css('border-color', 'lightgrey');

    $("#modalSalvarRegistro").modal('show');
    document.getElementById("lblModalHeader").innerText = "Novo Nível de Alerta";

    selectedale_id = -1;
}

function AnomAlerta_Salvar() {
    var txtale_codigo = document.getElementById('txtale_codigo');
    txtale_codigo.value = txtale_codigo.value.trim();

    var txtale_descricao = document.getElementById('txtale_descricao');
    txtale_descricao.value = txtale_descricao.value.trim();

    if (validaAlfaNumerico(txtale_descricao) && (ChecaRepetido(txtale_codigo)) && validaAlfaNumericoSemAcentosNemEspaco(txtale_codigo, 0)) {

        var AnomAlerta = {
            ale_id: selectedale_id,
            ale_codigo: $('#txtale_codigo').val(),
            ale_descricao: $('#txtale_descricao').val(),
            ale_ativo: $('#chkale_ativo').prop('checked') ? 1 : 0 
        };

        $.ajax({
            url: "/Anomalia/AnomAlerta_Salvar",
            data: JSON.stringify(AnomAlerta),
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                $("#modalSalvarRegistro").modal('hide');
                $('#tblAnomAlertas').DataTable().ajax.reload(null, false);  //false = sem reload na pagina.

                return false;
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
                return false;
            }
        });
    }
    else {
        $("#modalSalvarRegistro").modal('show');
        return false;
    }
    return false;
}

function AnomAlerta_Excluir(id) {
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
            var response = POST("/Anomalia/AnomAlerta_Excluir", JSON.stringify({ id: id }))
            if (response.erroId >= 1) {
                swal({
                    type: 'success',
                    title: 'Sucesso',
                    text: 'Registro excluído com sucesso'
                });

                $('#tblAnomAlertas').DataTable().ajax.reload();
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

function AnomAlerta_AtivarDesativar(id, ativar) {

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
                var response = POST("/Anomalia/AnomAlerta_AtivarDesativar", JSON.stringify({ id: id }))
                if (response.erroId == 1) {
                    swal({
                        type: 'success',
                        title: 'Sucesso',
                        text: ativar == 1 ? msgAtivacaoOK : msgDesativacaoOK
                    });

                    $('#tblAnomAlertas').DataTable().ajax.reload();
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

function AnomAlerta_Editar(id) {
    document.getElementById("lblModalHeader").innerText = "Editar Nível de Alerta";

    var corBranca = "rgb(255, 255, 255)";
    $('#txtale_codigo').css("background-color", corBranca);
    $('#txtale_descricao').css("background-color", corBranca);

    $('#txtale_descricao').css('border-color', 'lightgrey');
    $.ajax({
        url: "/Anomalia/AnomAlerta_GetbyID/" + id,
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            selectedale_id = id;
            $('#txtale_codigo').val(result.ale_codigo);
            $('#txtale_descricao').val(result.ale_descricao);
            $('#chkale_ativo').prop('checked', (result.ale_ativo == '1' ? true : false));

            $("#modalSalvarRegistro").modal('show');
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
    return false;
}

function ChecaRepetido(txtBox) {
    txtBox.value = txtBox.value.trim();

    if (validaAlfaNumerico(txtBox)) {
        var corVermelho = "rgb(228, 88, 71)";
        var corBranca = "rgb(255, 255, 255)";
        var searchValue = '\\b' + txtBox.value + '\\b';
        var rowId = $('#tblAnomAlertas').DataTable().column(1).search(searchValue, true, false).rows({ filter: 'applied' }).data();
        if (rowId.length > 0) { // ja tem
            if (selectedale_id != rowId[0]["ale_id"]) {
                $('#txtale_descricao').css("background-color", corVermelho);
                swal({
                    type: 'error',
                    title: 'Aviso',
                    text: 'Alerta já cadastrada'
                }).then(
                    function () {
                        return false;
                    });
            }
            else {
                $('#tblAnomAlertas').DataTable().search('').columns().search('').draw();
                return true;
            }
        }
        else { // nao tem
            $('#txtale_descricao').css("background-color", corBranca);
            $('#tblAnomAlertas').DataTable().search('').columns().search('').draw();
            return true;
        }
    }
    else {
        $("#modalSalvarRegistro").modal('show');
        return false;
    }

}

// montagem do gridview
$(document).ready(function () {

    // ****************************GRID tblAnomAlertas *****************************************************************************
    $('#tblAnomAlertas').DataTable({
        "ajax": {
            "url": "/Anomalia/AnomAlerta_ListAll",
            "type": "GET",
            "datatype": "json"
        }
        , "columns": [
            { data: "ale_id", "width": "30px", "className": "hide_column", "searchable": false },
            { data: "ale_codigo", "width": "80px", "searchable": true },
            { data: "ale_descricao", "autoWidth": true, "searchable": true },
            {
                "title": "Opções",
                data: "ale_id",
                "searchable": false,
                "sortable": false,
                "render": function (data, type, row) {
                    var retorno = "";
                    if (permissaoEscrita > 0) {
                        retorno = '<a href="#" onclick="return AnomAlerta_Editar(' + data + ')" title="Editar" ><span class="glyphicon glyphicon-pencil"></span></a>' + '  ';

                        if (row.ale_ativo == 1)
                            retorno += '<a href="#" onclick="return AnomAlerta_AtivarDesativar(' + data + ', 0)" title="Ativo" ><span class="glyphicon glyphicon-ok text-success"></span></a>' + '  ';
                        else
                            retorno += '<a href="#" onclick="return AnomAlerta_AtivarDesativar(' + data + ', 1)" title="Desativado" ><span class="glyphicon glyphicon-remove text-danger"></span></a>' + '  ';
                    }
                    else {
                        retorno = '<span class="glyphicon glyphicon-pencil desabilitado"  ></span>' + '  ';

                        if (row.ale_ativo == 1)
                            retorno += '<span class="glyphicon glyphicon-ok text-success desabilitado" ></span>' + '  ';
                        else
                            retorno += '<span class="glyphicon glyphicon-remove text-danger desabilitado"  ></span>' + '  ';

                    }

                    if (permissaoExclusao > 0)
                        retorno += '<a href="#" onclick="return AnomAlerta_Excluir(' + data + ')" title="Excluir" ><span class="glyphicon glyphicon-trash"></span></a>';
                    else
                        retorno += '<span class="glyphicon glyphicon-trash desabilitado" title="Excluir" ></span>';

                    return retorno;
                }
            }
        ]
        , "rowId": "ale_id"
        , "rowCallback": function (row, data) {
            if (data.ale_id == selectedale_id)
                $(row).addClass('selected');
        }
        , "lengthMenu": [[10, 25, 50, 100], [10, 25, 50, 100]]
        , select: {
            style: 'single'
        }
        , searching: true
        , "oLanguage": idioma
        , "pagingType": "input"
        , "sDom": '<"top">rt<"bottom"pfli><"clear">'
    });

    var tblAnomAlertas = $('#tblAnomAlertas').DataTable();

    $('#tblAnomAlertas tbody').on('click', 'tr', function () {
        if ($(this).hasClass('selected')) {
            $(this).removeClass('selected');
        }
        else {
            tblAnomAlertas.$('tr.selected').removeClass('selected');
            $(this).addClass('selected');
        }

        var AnomAlerta_id = tblAnomAlertas.row(this).data();
        $('#hddnSelectedale_id').val(AnomAlerta_id["ale_id"]);
        selectedale_id = AnomAlerta_id["ale_id"];

    });


});
