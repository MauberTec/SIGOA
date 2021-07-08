
var selectedtos_id;

function OSTipo_Inserir() {
    var corBranca = "rgb(255, 255, 255)";
    $('#txttos_descricao').css("background-color", corBranca);

    $('#txttos_id').val("");
    $('#txttos_codigo').val("");
    $('#txttos_descricao').val("");
    $('#chktos_ativo').prop('checked', true);

    $('#btnUpdate').hide();
    $('#btnAdd').show();
    $('#txtOSTipo').css('border-color', 'lightgrey');
    $('#chktos_ativo').css('border-color', 'lightgrey');

    $("#modalSalvarRegistro").modal('show');
    document.getElementById("lblModalHeader").innerText = "Novo Tipo de Ordem de Serviço";

    selectedtos_id = -1;
}

function OSTipo_Salvar() {
    var txttos_codigo = document.getElementById('txttos_codigo');
    txttos_codigo.value = txttos_codigo.value.trim();

    var txttos_descricao = document.getElementById('txttos_descricao');
    txttos_descricao.value = txttos_descricao.value.trim();

    if (validaAlfaNumericoSemAcentosNemEspaco(txttos_codigo) && (ChecaRepetido(txttos_codigo))
        && validaAlfaNumericoAcentosAfins(txttos_descricao)) {

        var OSTipo = {
            tos_id: $('#txttos_id').val(),
            tos_codigo: $('#txttos_codigo').val(),
            tos_descricao: $('#txttos_descricao').val(),
            tos_ativo: $('#chktos_ativo').prop('checked') ? 1 : 0 
        };

        $.ajax({
            url: "/OrdemServico/OSTipo_Salvar",
            data: JSON.stringify(OSTipo),
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                $("#modalSalvarRegistro").modal('hide');
                $('#tblOSTipos').DataTable().ajax.reload(null, false);  //false = sem reload na pagina.

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

function OSTipo_Excluir(id) {
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
            var response = POST("/OrdemServico/OSTipo_Excluir", JSON.stringify({ id: id }))
            if (response.erroId >= 1) {
                swal({
                    type: 'success',
                    title: 'Sucesso',
                    text: 'Registro excluído com sucesso'
                });

                $('#tblOSTipos').DataTable().ajax.reload();
                document.getElementById('subGrids').style.visibility = "hidden";

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

function OSTipo_AtivarDesativar(id, ativar) {

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
                var response = POST("/OrdemServico/OSTipo_AtivarDesativar", JSON.stringify({ id: id }))
                if (response.erroId == 1) {
                    swal({
                        type: 'success',
                        title: 'Sucesso',
                        text: ativar == 1 ? msgAtivacaoOK : msgDesativacaoOK
                    });

                    $('#tblOSTipos').DataTable().ajax.reload();
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

function OSTipo_Editar(id) {
    document.getElementById("lblModalHeader").innerText = "Editar Tipo de Ordem de Serviço";

    var corBranca = "rgb(255, 255, 255)";
    $('#txttos_descricao').css("background-color", corBranca);

    $('#txttos_descricao').css('border-color', 'lightgrey');
    $.ajax({
        url: "/OrdemServico/OSTipo_GetbyID/" + id,
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            $('#txttos_id').val(result.tos_id);
            $('#txttos_codigo').val(result.tos_codigo);
            $('#txttos_descricao').val(result.tos_descricao);
            $('#chktos_ativo').prop('checked', (result.tos_ativo == '1' ? true : false));

            $("#modalSalvarRegistro").modal('show');
            $('#btnUpdate').show();
            $('#btnAdd').hide();
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
        var rowId = $('#tblOSTipos').DataTable().column(1).search(searchValue, true, false).rows({ filter: 'applied' }).data();
        if (rowId.length > 0) { // ja tem
            if (selectedtos_id != rowId[0]["tos_id"]) {
                $('#txttos_descricao').css("background-color", corVermelho);
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
                $('#tblOSTipos').DataTable().search('').columns().search('').draw();
                return true;
            }
        }
        else { // nao tem
            $('#txttos_descricao').css("background-color", corBranca);
            $('#tblOSTipos').DataTable().search('').columns().search('').draw();
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

    // ****************************GRID tblOSTipos *****************************************************************************
    $('#tblOSTipos').DataTable({
        "ajax": {
            "url": "/OrdemServico/OSTipo_ListAll",
            "type": "GET",
            "datatype": "json"
        }
        , "columns": [
            { data: "tos_id",  "className": "hide_column", "searchable": false },
            { data: "tos_codigo", "width": "50px", "searchable": true },
            { data: "tos_descricao", "autoWidth": true, "searchable": true },
            {
                "title": "Opções",
                data: "tos_id",
                "searchable": false,
                "sortable": false,
                "render": function (data, type, row) {
                    var retorno = "";
                    if (permissaoEscrita > 0) {
                        retorno = '<a href="#" onclick="return OSTipo_Editar(' + data + ')" title="Editar" ><span class="glyphicon glyphicon-pencil"></span></a>' + '  ';

                        if (row.tos_ativo == 1)
                            retorno += '<a href="#" onclick="return OSTipo_AtivarDesativar(' + data + ', 0)" title="Ativo" ><span class="glyphicon glyphicon-ok text-success"></span></a>' + '  ';
                        else
                            retorno += '<a href="#" onclick="return OSTipo_AtivarDesativar(' + data + ', 1)" title="Desativado" ><span class="glyphicon glyphicon-remove text-danger"></span></a>' + '  ';
                    }
                    else {
                        retorno = '<span class="glyphicon glyphicon-pencil desabilitado"  ></span>' + '  ';

                        if (row.tos_ativo == 1)
                            retorno += '<span class="glyphicon glyphicon-ok text-success desabilitado" ></span>' + '  ';
                        else
                            retorno += '<span class="glyphicon glyphicon-remove text-danger desabilitado"  ></span>' + '  ';

                    }

                    if (permissaoExclusao > 0)
                        retorno += '<a href="#" onclick="return OSTipo_Excluir(' + data + ')" title="Excluir" ><span class="glyphicon glyphicon-trash"></span></a>';
                    else
                        retorno += '<span class="glyphicon glyphicon-trash desabilitado" title="Excluir" ></span>';

                    return retorno;
                }
            }
        ]
        , "rowId": "tos_id"
        , "order": [1, "asc"]
        , "rowCallback": function (row, data) {
            if (data.tos_id == selectedtos_id)
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

    var tblOSTipos = $('#tblOSTipos').DataTable();

    $('#tblOSTipos tbody').on('click', 'tr', function () {
        if ($(this).hasClass('selected')) {
            $(this).removeClass('selected');
        }
        else {
            tblOSTipos.$('tr.selected').removeClass('selected');
            $(this).addClass('selected');
        }

        var OSTipo_id = tblOSTipos.row(this).data();
        $('#hddnSelectedtos_id').val(OSTipo_id["tos_id"]);
        selectedtos_id = OSTipo_id["tos_id"];

    });


});
