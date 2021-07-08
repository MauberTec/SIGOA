
var selectedocs_id;

function OrcamentoStatus_Inserir() {
    var corBranca = "rgb(255, 255, 255)";
    $('#txtocs_codigo').css("background-color", corBranca);
    $('#txtocs_descricao').css("background-color", corBranca);

    $('#txtocs_codigo').val("");
    $('#txtocs_descricao').val("");
    $('#chkocs_ativo').prop('checked', true);

    $('#txtOrcamentoStatus').css('border-color', 'lightgrey');
    $('#chkocs_ativo').css('border-color', 'lightgrey');

    $("#modalSalvarRegistro").modal('show');
    document.getElementById("lblModalHeader").innerText = "Novo Status de Orçamento";

    selectedocs_id = -1;
}

function OrcamentoStatus_Salvar() {
    var txtocs_codigo = document.getElementById('txtocs_codigo');
    txtocs_codigo.value = txtocs_codigo.value.trim();

    var txtocs_descricao = document.getElementById('txtocs_descricao');
    txtocs_descricao.value = txtocs_descricao.value.trim();

    if (validaAlfaNumerico(txtocs_descricao) && (ChecaRepetido(txtocs_codigo)) && validaAlfaNumericoSemAcentosNemEspaco(txtocs_codigo, 0)) {

        var OrcamentoStatus = {
            ocs_id: selectedocs_id,
            ocs_codigo: $('#txtocs_codigo').val(),
            ocs_descricao: $('#txtocs_descricao').val(),
            ocs_ativo: $('#chkocs_ativo').prop('checked') ? 1 : 0 
        };

        $.ajax({
            url: "/Orcamento/OrcamentoStatus_Salvar",
            data: JSON.stringify(OrcamentoStatus),
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                $("#modalSalvarRegistro").modal('hide');
                $('#tblOrcamentoStatus').DataTable().ajax.reload(null, false);  //false = sem reload na pagina.

                return false;
            },
            error: function (errormessage) {
                ocsrt(errormessage.responseText);
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

function OrcamentoStatus_Excluir(id) {
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
            var response = POST("/Orcamento/OrcamentoStatus_Excluir", JSON.stringify({ id: id }))
            if (response.erroId >= 1) {
                swal({
                    type: 'success',
                    title: 'Sucesso',
                    text: 'Registro excluído com sucesso'
                });

                $('#tblOrcamentoStatus').DataTable().ajax.reload();
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

function OrcamentoStatus_AtivarDesativar(id, ativar) {

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
                var response = POST("/Orcamento/OrcamentoStatus_AtivarDesativar", JSON.stringify({ id: id }))
                if (response.erroId == 1) {
                    swal({
                        type: 'success',
                        title: 'Sucesso',
                        text: ativar == 1 ? msgAtivacaoOK : msgDesativacaoOK
                    });

                    $('#tblOrcamentoStatus').DataTable().ajax.reload();
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

function OrcamentoStatus_Editar(id) {
    document.getElementById("lblModalHeader").innerText = "Editar Status de Orçamento";

    var corBranca = "rgb(255, 255, 255)";
    $('#txtocs_codigo').css("background-color", corBranca);
    $('#txtocs_descricao').css("background-color", corBranca);

    $('#txtocs_descricao').css('border-color', 'lightgrey');
    $.ajax({
        url: "/Orcamento/OrcamentoStatus_GetbyID/" + id,
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            selectedocs_id = id;
            $('#txtocs_codigo').val(result.ocs_codigo);
            $('#txtocs_descricao').val(result.ocs_descricao);
            $('#chkocs_ativo').prop('checked', (result.ocs_ativo == '1' ? true : false));

            $("#modalSalvarRegistro").modal('show');
        },
        error: function (errormessage) {
            ocsrt(errormessage.responseText);
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
        var rowId = $('#tblOrcamentoStatus').DataTable().column(1).search(searchValue, true, false).rows({ filter: 'applied' }).data();
        if (rowId.length > 0) { // ja tem
            if (selectedocs_id != rowId[0]["ocs_id"]) {
                $('#txtocs_descricao').css("background-color", corVermelho);
                swal({
                    type: 'error',
                    title: 'Aviso',
                    text: 'Status já cadocsrada'
                }).then(
                    function () {
                        return false;
                    });
            }
            else {
                $('#tblOrcamentoStatus').DataTable().search('').columns().search('').draw();
                return true;
            }
        }
        else { // nao tem
            $('#txtocs_descricao').css("background-color", corBranca);
            $('#tblOrcamentoStatus').DataTable().search('').columns().search('').draw();
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

    // ****************************GRID tblOrcamentoStatus *****************************************************************************
    $('#tblOrcamentoStatus').DataTable({
        "ajax": {
            "url": "/Orcamento/OrcamentoStatus_ListAll",
            "type": "GET",
            "datatype": "json"
        }
        , "columns": [
            { data: "ocs_id", "width": "30px", "className": "hide_column", "searchable": false },
            { data: "ocs_codigo", "width": "80px", "searchable": true },
            { data: "ocs_descricao", "autoWidth": true, "searchable": true },
            {
                "title": "Opções",
                data: "ocs_id",
                "searchable": false,
                "sortable": false,
                "render": function (data, type, row) {
                    var retorno = "";
                    if (permissaoEscrita > 0) {
                        retorno = '<a href="#" onclick="return OrcamentoStatus_Editar(' + data + ')" title="Editar" ><span class="glyphicon glyphicon-pencil"></span></a>' + '  ';

                        if (row.ocs_ativo == 1)
                            retorno += '<a href="#" onclick="return OrcamentoStatus_AtivarDesativar(' + data + ', 0)" title="Ativo" ><span class="glyphicon glyphicon-ok text-success"></span></a>' + '  ';
                        else
                            retorno += '<a href="#" onclick="return OrcamentoStatus_AtivarDesativar(' + data + ', 1)" title="Desativado" ><span class="glyphicon glyphicon-remove text-danger"></span></a>' + '  ';
                    }
                    else {
                        retorno = '<span class="glyphicon glyphicon-pencil desabilitado"  ></span>' + '  ';

                        if (row.ocs_ativo == 1)
                            retorno += '<span class="glyphicon glyphicon-ok text-success desabilitado" ></span>' + '  ';
                        else
                            retorno += '<span class="glyphicon glyphicon-remove text-danger desabilitado"  ></span>' + '  ';

                    }

                    if (permissaoExclusao > 0)
                        retorno += '<a href="#" onclick="return OrcamentoStatus_Excluir(' + data + ')" title="Excluir" ><span class="glyphicon glyphicon-trash"></span></a>';
                    else
                        retorno += '<span class="glyphicon glyphicon-trash desabilitado" title="Excluir" ></span>';

                    return retorno;
                }
            }
        ]
        , "rowId": "ocs_id"
        , "rowCallback": function (row, data) {
            if (data.ocs_id == selectedocs_id)
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

    var tblOrcamentoStatus = $('#tblOrcamentoStatus').DataTable();

    $('#tblOrcamentoStatus tbody').on('click', 'tr', function () {
        if ($(this).hasClass('selected')) {
            $(this).removeClass('selected');
        }
        else {
            tblOrcamentoStatus.$('tr.selected').removeClass('selected');
            $(this).addClass('selected');
        }

        var OrcamentoStatus_id = tblOrcamentoStatus.row(this).data();
        $('#hddnSelectedocs_id').val(OrcamentoStatus_id["ocs_id"]);
        selectedocs_id = OrcamentoStatus_id["ocs_id"];

    });


});
