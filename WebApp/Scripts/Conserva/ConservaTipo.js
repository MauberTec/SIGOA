
var selectedcot_id;

function ConservaTipo_Inserir() {
    var corBranca = "rgb(255, 255, 255)";
    $('#txtcot_descricao').css("background-color", corBranca);

    $('#txtcot_codigo').val("");
    $('#txtcot_descricao').val("");
    $('#chkcot_ativo').prop('checked', true);

    $('#txtConservaTipo').css('border-color', 'lightgrey');
    $('#chkcot_ativo').css('border-color', 'lightgrey');

    $("#modalSalvarRegistro").modal('show');
    document.getElementById("lblModalHeader").innerText = "Novo Tipo de Conserva";

    selectedcot_id = -1;
}

function ConservaTipo_Salvar() {
    var txtcot_codigo = document.getElementById('txtcot_codigo');
    txtcot_codigo.value = txtcot_codigo.value.trim();

    var txtcot_descricao = document.getElementById('txtcot_descricao');
    txtcot_descricao.value = txtcot_descricao.value.trim();

    if (validaAlfaNumericoSemAcentosNemEspaco(txtcot_codigo) && (ChecaRepetido(txtcot_codigo))
        && validaAlfaNumericoAcentosAfins(txtcot_descricao,1,1)) {

        var ConservaTipo = {
            cot_id: selectedcot_id,
            cot_codigo: $('#txtcot_codigo').val(),
            cot_descricao: $('#txtcot_descricao').val(),
            cot_ativo: $('#chkcot_ativo').prop('checked') ? 1 : 0 
        };

        $.ajax({
            url: "/Conserva/ConservaTipo_Salvar",
            data: JSON.stringify(ConservaTipo),
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                $('#tblConservaTipos').DataTable().ajax.reload(null, false);  //false = sem reload na pagina.
                $("#modalSalvarRegistro").modal('hide');
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

function ConservaTipo_Excluir(id) {
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
            var response = POST("/Conserva/ConservaTipo_Excluir", JSON.stringify({ id: id }))
            if (response.erroId >= 1) {
                swal({
                    type: 'success',
                    title: 'Sucesso',
                    text: 'Registro excluído com sucesso'
                });

                selectedcot_id = -1;
                $('#tblConservaTipos').DataTable().ajax.reload();
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

function ConservaTipo_AtivarDesativar(id, ativar) {
    selectedcot_id = id;

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
                var response = POST("/Conserva/ConservaTipo_AtivarDesativar", JSON.stringify({ id: id }))
                if (response.erroId == 1) {
                    swal({
                        type: 'success',
                        title: 'Sucesso',
                        text: ativar == 1 ? msgAtivacaoOK : msgDesativacaoOK
                    });

                    $('#tblConservaTipos').DataTable().ajax.reload();
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

function ConservaTipo_Editar(id) {
    selectedcot_id = id;

    document.getElementById("lblModalHeader").innerText = "Editar Tipo de Conserva";

    var corBranca = "rgb(255, 255, 255)";
    $('#txtcot_descricao').css("background-color", corBranca);

    $('#txtcot_descricao').css('border-color', 'lightgrey');
    $.ajax({
        url: "/Conserva/ConservaTipo_GetbyID/" + id,
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            $('#txtcot_id').val(result.cot_id);
            $('#txtcot_codigo').val(result.cot_codigo);
            $('#txtcot_descricao').val(result.cot_descricao);
            $('#chkcot_ativo').prop('checked', (result.cot_ativo == '1' ? true : false));

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
        var rowId = $('#tblConservaTipos').DataTable().column(1).search(searchValue, true, false).rows({ filter: 'applied' }).data();
        if (rowId.length > 0) { // ja tem
            if (selectedcot_id != rowId[0]["cot_id"]) {
                $('#txtcot_descricao').css("background-color", corVermelho);
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
                $('#tblConservaTipos').DataTable().search('').columns().search('').draw();
                return true;
            }
        }
        else { // nao tem
            $('#txtcot_descricao').css("background-color", corBranca);
            $('#tblConservaTipos').DataTable().search('').columns().search('').draw();
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

    // ****************************GRID tblConservaTipos *****************************************************************************
    $('#tblConservaTipos').DataTable({
        "ajax": {
            "url": "/Conserva/ConservaTipo_ListAll",
            "type": "GET",
            "datatype": "json"
        }
        , "columns": [
            { data: "cot_id",  "className": "hide_column", "searchable": false },
            { data: "cot_codigo", "width": "50px", "searchable": true },
            { data: "cot_descricao", "autoWidth": true, "searchable": true },
            {
                "title": "Opções",
                data: "cot_id",
                "searchable": false,
                "sortable": false,
                "render": function (data, type, row) {
                    var retorno = "";
                    if (permissaoEscrita > 0) {
                        retorno = '<a href="#" onclick="return ConservaTipo_Editar(' + data + ')" title="Editar" ><span class="glyphicon glyphicon-pencil"></span></a>' + '  ';

                        if (row.cot_ativo == 1)
                            retorno += '<a href="#" onclick="return ConservaTipo_AtivarDesativar(' + data + ', 0)" title="Ativo" ><span class="glyphicon glyphicon-ok text-success"></span></a>' + '  ';
                        else
                            retorno += '<a href="#" onclick="return ConservaTipo_AtivarDesativar(' + data + ', 1)" title="Desativado" ><span class="glyphicon glyphicon-remove text-danger"></span></a>' + '  ';
                    }
                    else {
                        retorno = '<span class="glyphicon glyphicon-pencil desabilitado"  ></span>' + '  ';

                        if (row.cot_ativo == 1)
                            retorno += '<span class="glyphicon glyphicon-ok text-success desabilitado" ></span>' + '  ';
                        else
                            retorno += '<span class="glyphicon glyphicon-remove text-danger desabilitado"  ></span>' + '  ';

                    }

                    if (permissaoExclusao > 0)
                        retorno += '<a href="#" onclick="return ConservaTipo_Excluir(' + data + ')" title="Excluir" ><span class="glyphicon glyphicon-trash"></span></a>';
                    else
                        retorno += '<span class="glyphicon glyphicon-trash desabilitado" title="Excluir" ></span>';

                    return retorno;
                }
            }
        ]
        , "rowId": "cot_id"
        , "order": [1, "asc"]
        , "rowCallback": function (row, data) {
            if (data.cot_id == selectedcot_id)
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


});
