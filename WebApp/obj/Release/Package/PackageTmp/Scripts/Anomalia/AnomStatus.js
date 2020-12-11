
var selectedast_id;

function AnomStatus_Inserir() {
    var corBranca = "rgb(255, 255, 255)";
    $('#txtast_codigo').css("background-color", corBranca);
    $('#txtast_descricao').css("background-color", corBranca);

    $('#txtast_codigo').val("");
    $('#txtast_descricao').val("");
    $('#chkast_ativo').prop('checked', true);

    $('#txtAnomStatus').css('border-color', 'lightgrey');
    $('#chkast_ativo').css('border-color', 'lightgrey');

    $("#modalSalvarRegistro").modal('show');
    document.getElementById("lblModalHeader").innerText = "Novo Status de Anomalia";

    selectedast_id = -1;
}

function AnomStatus_Salvar() {
    var txtast_codigo = document.getElementById('txtast_codigo');
    txtast_codigo.value = txtast_codigo.value.trim();

    var txtast_descricao = document.getElementById('txtast_descricao');
    txtast_descricao.value = txtast_descricao.value.trim();

    if (validaAlfaNumerico(txtast_descricao) && (ChecaRepetido(txtast_codigo)) && validaAlfaNumericoSemAcentosNemEspaco(txtast_codigo, 0)) {

        var AnomStatus = {
            ast_id: selectedast_id,
            ast_codigo: $('#txtast_codigo').val(),
            ast_descricao: $('#txtast_descricao').val(),
            ast_ativo: $('#chkast_ativo').prop('checked') ? 1 : 0 
        };

        $.ajax({
            url: "/Anomalia/AnomStatus_Salvar",
            data: JSON.stringify(AnomStatus),
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                $("#modalSalvarRegistro").modal('hide');
                $('#tblAnomStatus').DataTable().ajax.reload(null, false);  //false = sem reload na pagina.

                return false;
            },
            error: function (errormessage) {
                astrt(errormessage.responseText);
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

function AnomStatus_Excluir(id) {
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
            var response = POST("/Anomalia/AnomStatus_Excluir", JSON.stringify({ id: id }))
            if (response.erroId >= 1) {
                swal({
                    type: 'success',
                    title: 'Sucesso',
                    text: 'Registro excluído com sucesso'
                });

                $('#tblAnomStatus').DataTable().ajax.reload();
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

function AnomStatus_AtivarDesativar(id, ativar) {

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
                var response = POST("/Anomalia/AnomStatus_AtivarDesativar", JSON.stringify({ id: id }))
                if (response.erroId == 1) {
                    swal({
                        type: 'success',
                        title: 'Sucesso',
                        text: ativar == 1 ? msgAtivacaoOK : msgDesativacaoOK
                    });

                    $('#tblAnomStatus').DataTable().ajax.reload();
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

function AnomStatus_Editar(id) {
    document.getElementById("lblModalHeader").innerText = "Editar Status de Anomalia";

    var corBranca = "rgb(255, 255, 255)";
    $('#txtast_codigo').css("background-color", corBranca);
    $('#txtast_descricao').css("background-color", corBranca);

    $('#txtast_descricao').css('border-color', 'lightgrey');
    $.ajax({
        url: "/Anomalia/AnomStatus_GetbyID/" + id,
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            selectedast_id = id;
            $('#txtast_codigo').val(result.ast_codigo);
            $('#txtast_descricao').val(result.ast_descricao);
            $('#chkast_ativo').prop('checked', (result.ast_ativo == '1' ? true : false));

            $("#modalSalvarRegistro").modal('show');
        },
        error: function (errormessage) {
            astrt(errormessage.responseText);
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
        var rowId = $('#tblAnomStatus').DataTable().column(1).search(searchValue, true, false).rows({ filter: 'applied' }).data();
        if (rowId.length > 0) { // ja tem
            if (selectedast_id != rowId[0]["ast_id"]) {
                $('#txtast_descricao').css("background-color", corVermelho);
                swal({
                    type: 'error',
                    title: 'Aviso',
                    text: 'Status já cadastrada'
                }).then(
                    function () {
                        return false;
                    });
            }
            else {
                $('#tblAnomStatus').DataTable().search('').columns().search('').draw();
                return true;
            }
        }
        else { // nao tem
            $('#txtast_descricao').css("background-color", corBranca);
            $('#tblAnomStatus').DataTable().search('').columns().search('').draw();
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

    // ****************************GRID tblAnomStatus *****************************************************************************
    $('#tblAnomStatus').DataTable({
        "ajax": {
            "url": "/Anomalia/AnomStatus_ListAll",
            "type": "GET",
            "datatype": "json"
        }
        , "columns": [
            { data: "ast_id", "width": "30px", "className": "hide_column", "searchable": false },
            { data: "ast_codigo", "width": "80px", "searchable": true },
            { data: "ast_descricao", "autoWidth": true, "searchable": true },
            {
                "title": "Opções",
                data: "ast_id",
                "searchable": false,
                "sortable": false,
                "render": function (data, type, row) {
                    var retorno = "";
                    if (permissaoEscrita > 0) {
                        retorno = '<a href="#" onclick="return AnomStatus_Editar(' + data + ')" title="Editar" ><span class="glyphicon glyphicon-pencil"></span></a>' + '  ';

                        if (row.ast_ativo == 1)
                            retorno += '<a href="#" onclick="return AnomStatus_AtivarDesativar(' + data + ', 0)" title="Ativo" ><span class="glyphicon glyphicon-ok text-success"></span></a>' + '  ';
                        else
                            retorno += '<a href="#" onclick="return AnomStatus_AtivarDesativar(' + data + ', 1)" title="Desativado" ><span class="glyphicon glyphicon-remove text-danger"></span></a>' + '  ';
                    }
                    else {
                        retorno = '<span class="glyphicon glyphicon-pencil desabilitado"  ></span>' + '  ';

                        if (row.ast_ativo == 1)
                            retorno += '<span class="glyphicon glyphicon-ok text-success desabilitado" ></span>' + '  ';
                        else
                            retorno += '<span class="glyphicon glyphicon-remove text-danger desabilitado"  ></span>' + '  ';

                    }

                    if (permissaoExclusao > 0)
                        retorno += '<a href="#" onclick="return AnomStatus_Excluir(' + data + ')" title="Excluir" ><span class="glyphicon glyphicon-trash"></span></a>';
                    else
                        retorno += '<span class="glyphicon glyphicon-trash desabilitado" title="Excluir" ></span>';

                    return retorno;
                }
            }
        ]
        , "rowId": "ast_id"
        , "rowCallback": function (row, data) {
            if (data.ast_id == selectedast_id)
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

    var tblAnomStatus = $('#tblAnomStatus').DataTable();

    $('#tblAnomStatus tbody').on('click', 'tr', function () {
        if ($(this).hasClass('selected')) {
            $(this).removeClass('selected');
        }
        else {
            tblAnomStatus.$('tr.selected').removeClass('selected');
            $(this).addClass('selected');
        }

        var AnomStatus_id = tblAnomStatus.row(this).data();
        $('#hddnSelectedast_id').val(AnomStatus_id["ast_id"]);
        selectedast_id = AnomStatus_id["ast_id"];

    });


});
