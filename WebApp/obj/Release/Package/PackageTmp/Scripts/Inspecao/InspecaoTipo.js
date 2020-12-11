
var selectedipt_id;

function InspecaoTipo_Inserir() {
    var corBranca = "rgb(255, 255, 255)";
    $('#txtipt_codigo').css("background-color", corBranca);
    $('#txtipt_descricao').css("background-color", corBranca);

    $('#txtipt_codigo').val("");
    $('#txtipt_descricao').val("");
    $('#chkipt_ativo').prop('checked', true);

    $('#txtInspecaoTipo').css('border-color', 'lightgrey');
    $('#chkipt_ativo').css('border-color', 'lightgrey');

    $("#modalSalvarRegistro").modal('show');
    document.getElementById("lblModalHeader").innerText = "Novo Tipo de Inspeção";

    selectedipt_id = -1;
}

function InspecaoTipo_Salvar() {
    var txtipt_codigo = document.getElementById('txtipt_codigo');
    txtipt_codigo.value = txtipt_codigo.value.trim();

    var txtipt_descricao = document.getElementById('txtipt_descricao');
    txtipt_descricao.value = txtipt_descricao.value.trim();

    if (validaAlfaNumerico(txtipt_descricao) && (ChecaRepetido(txtipt_codigo)) && validaAlfaNumericoSemAcentosNemEspaco(txtipt_codigo, 0)) {

        var InspecaoTipo = {
            ipt_id: selectedipt_id,
            ipt_codigo: $('#txtipt_codigo').val(),
            ipt_descricao: $('#txtipt_descricao').val(),
            ipt_ativo: $('#chkipt_ativo').prop('checked') ? 1 : 0 
        };

        $.ajax({
            url: "/Inspecao/InspecaoTipo_Salvar",
            data: JSON.stringify(InspecaoTipo),
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                $("#modalSalvarRegistro").modal('hide');
                $('#tblInspecaoTipos').DataTable().ajax.reload(null, false);  //false = sem reload na pagina.

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

function InspecaoTipo_Excluir(id) {
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
            var response = POST("/Inspecao/InspecaoTipo_Excluir", JSON.stringify({ id: id }))
            if (response.erroId >= 1) {
                swal({
                    type: 'success',
                    title: 'Sucesso',
                    text: 'Registro excluído com sucesso'
                });

                $('#tblInspecaoTipos').DataTable().ajax.reload();
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

function InspecaoTipo_AtivarDesativar(id, ativar) {

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
                var response = POST("/Inspecao/InspecaoTipo_AtivarDesativar", JSON.stringify({ id: id }))
                if (response.erroId == 1) {
                    swal({
                        type: 'success',
                        title: 'Sucesso',
                        text: ativar == 1 ? msgAtivacaoOK : msgDesativacaoOK
                    });

                    $('#tblInspecaoTipos').DataTable().ajax.reload();
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

function InspecaoTipo_Editar(id) {
    document.getElementById("lblModalHeader").innerText = "Editar Tipo de Inspeção";

    var corBranca = "rgb(255, 255, 255)";
    $('#txtipt_codigo').css("background-color", corBranca);
    $('#txtipt_descricao').css("background-color", corBranca);

    $('#txtipt_descricao').css('border-color', 'lightgrey');
    $.ajax({
        url: "/Inspecao/InspecaoTipo_GetbyID/" + id,
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            selectedipt_id = id;
            $('#txtipt_codigo').val(result.ipt_codigo);
            $('#txtipt_descricao').val(result.ipt_descricao);
            $('#chkipt_ativo').prop('checked', (result.ipt_ativo == '1' ? true : false));

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
        var rowId = $('#tblInspecaoTipos').DataTable().column(1).search(searchValue, true, false).rows({ filter: 'applied' }).data();
        if (rowId.length > 0) { // ja tem
            if (selectedipt_id != rowId[0]["ipt_id"]) {
                $('#txtipt_descricao').css("background-color", corVermelho);
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
                $('#tblInspecaoTipos').DataTable().search('').columns().search('').draw();
                return true;
            }
        }
        else { // nao tem
            $('#txtipt_descricao').css("background-color", corBranca);
            $('#tblInspecaoTipos').DataTable().search('').columns().search('').draw();
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

    // ****************************GRID tblInspecaoTipos *****************************************************************************
    $('#tblInspecaoTipos').DataTable({
        "ajax": {
            "url": "/Inspecao/InspecaoTipo_ListAll",
            "type": "GET",
            "datatype": "json"
        }
        , "columns": [
            { data: "ipt_id", "width": "30px", "className": "hide_column", "searchable": false },
            { data: "ipt_codigo", "width": "80px", "searchable": true },
            { data: "ipt_descricao", "autoWidth": true, "searchable": true },
            {
                "title": "Opções",
                data: "ipt_id",
                "searchable": false,
                "sortable": false,
                "render": function (data, type, row) {
                    var retorno = "";
                    if (permissaoEscrita > 0) {
                        retorno = '<a href="#" onclick="return InspecaoTipo_Editar(' + data + ')" title="Editar" ><span class="glyphicon glyphicon-pencil"></span></a>' + '  ';

                        if (row.ipt_ativo == 1)
                            retorno += '<a href="#" onclick="return InspecaoTipo_AtivarDesativar(' + data + ', 0)" title="Ativo" ><span class="glyphicon glyphicon-ok text-success"></span></a>' + '  ';
                        else
                            retorno += '<a href="#" onclick="return InspecaoTipo_AtivarDesativar(' + data + ', 1)" title="Desativado" ><span class="glyphicon glyphicon-remove text-danger"></span></a>' + '  ';
                    }
                    else {
                        retorno = '<span class="glyphicon glyphicon-pencil desabilitado"  ></span>' + '  ';

                        if (row.ipt_ativo == 1)
                            retorno += '<span class="glyphicon glyphicon-ok text-success desabilitado" ></span>' + '  ';
                        else
                            retorno += '<span class="glyphicon glyphicon-remove text-danger desabilitado"  ></span>' + '  ';

                    }

                    if (permissaoExclusao > 0)
                        retorno += '<a href="#" onclick="return InspecaoTipo_Excluir(' + data + ')" title="Excluir" ><span class="glyphicon glyphicon-trash"></span></a>';
                    else
                        retorno += '<span class="glyphicon glyphicon-trash desabilitado" title="Excluir" ></span>';

                    return retorno;
                }
            }
        ]
        , "rowId": "ipt_id"
        , "rowCallback": function (row, data) {
            if (data.ipt_id == selectedipt_id)
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

    var tblInspecaoTipos = $('#tblInspecaoTipos').DataTable();

    $('#tblInspecaoTipos tbody').on('click', 'tr', function () {
        if ($(this).hasClass('selected')) {
            $(this).removeClass('selected');
        }
        else {
            tblInspecaoTipos.$('tr.selected').removeClass('selected');
            $(this).addClass('selected');
        }

        var InspecaoTipo_id = tblInspecaoTipos.row(this).data();
        $('#hddnSelectedipt_id').val(InspecaoTipo_id["ipt_id"]);
        selectedipt_id = InspecaoTipo_id["ipt_id"];

    });


});
