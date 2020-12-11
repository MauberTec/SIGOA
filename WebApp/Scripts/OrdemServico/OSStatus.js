
var selectedsos_id;

function OSStatus_Inserir() {
    var corBranca = "rgb(255, 255, 255)";
    $('#txtsos_descricao').css("background-color", corBranca);

    $('#txtsos_id').val("");
    $('#txtsos_codigo').val("");
    $('#txtsos_descricao').val("");
    $('#chksos_ativo').prop('checked', true);

    $('#btnUpdate').hide();
    $('#btnAdd').show();
    $('#txtOSStatus').css('border-color', 'lightgrey');
    $('#chksos_ativo').css('border-color', 'lightgrey');

    $("#modalSalvarRegistro").modal('show');
    document.getElementById("lblModalHeader").innerText = "Novo Status de Ordem de Serviço";

    selectedsos_id = -1;
}

function OSStatus_Salvar() {

    var txtsos_id = document.getElementById('txtsos_id');
    txtsos_id.value = txtsos_id.value.trim();

    var txtsos_codigo = document.getElementById('txtsos_codigo');
    txtsos_codigo.value = txtsos_codigo.value.trim();

    var txtsos_descricao = document.getElementById('txtsos_descricao');
    txtsos_descricao.value = txtsos_descricao.value.trim();

    if (validaAlfaNumericoSemAcentosNemEspaco(txtsos_codigo) && (ChecaRepetido(txtsos_codigo))
        && validaAlfaNumerico(txtsos_descricao)) {

        var OSStatus = {
            sos_id: $('#txtsos_id').val(),
            sos_codigo: $('#txtsos_codigo').val(),
            sos_descricao: $('#txtsos_descricao').val(),
            sos_ativo: $('#chksos_ativo').prop('checked') ? 1 : 0 
        };

        $.ajax({
            url: "/OrdemServico/OSStatus_Salvar",
            data: JSON.stringify(OSStatus),
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                $("#modalSalvarRegistro").modal('hide');
                $('#tblOSStatus').DataTable().ajax.reload(null, false);  //false = sem reload na pagina.

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

function OSStatus_Excluir(id) {

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
            var response = POST("/OrdemServico/OSStatus_Excluir", JSON.stringify({ id: id }))
            if (response.erroId >= 1) {
                swal({
                    type: 'success',
                    title: 'Sucesso',
                    text: 'Registro excluído com sucesso'
                });

                $('#tblOSStatus').DataTable().ajax.reload();
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

function OSStatus_AtivarDesativar(id, ativar) {

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
                var response = POST("/OrdemServico/OSStatus_AtivarDesativar", JSON.stringify({ id: id }))
                if (response.erroId == 1) {
                    swal({
                        type: 'success',
                        title: 'Sucesso',
                        text: ativar == 1 ? msgAtivacaoOK : msgDesativacaoOK
                    });

                    $('#tblOSStatus').DataTable().ajax.reload();
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

function OSStatus_Editar(id) {
    document.getElementById("lblModalHeader").innerText = "Editar Status de Ordem de Serviço";

    var corBranca = "rgb(255, 255, 255)";
    $('#txtsos_descricao').css("background-color", corBranca);

    $('#txtsos_descricao').css('border-color', 'lightgrey');
    $.ajax({
        url: "/OrdemServico/OSStatus_GetbyID/" + id,
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            $('#txtsos_id').val(result.sos_id);
            $('#txtsos_codigo').val(result.sos_codigo);
            $('#txtsos_descricao').val(result.sos_descricao);
            $('#chksos_ativo').prop('checked', (result.sos_ativo == '1' ? true : false));

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
        var rowId = $('#tblOSStatus').DataTable().column(1).search(searchValue, true, false).rows({ filter: 'applied' }).data();
        if (rowId.length > 0) { // ja tem
            if (selectedsos_id != rowId[0]["sos_id"]) {
                $('#txtsos_descricao').css("background-color", corVermelho);
                swal({
                    type: 'error',
                    title: 'Aviso',
                    text: 'Status já cadastrado'
                }).then(
                    function () {
                        return false;
                    });
            }
            else {
                $('#tblOSStatus').DataTable().search('').columns().search('').draw();
                return true;
            }
        }
        else { // nao tem
            $('#txtsos_descricao').css("background-color", corBranca);
            $('#tblOSStatus').DataTable().search('').columns().search('').draw();
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

    // ****************************GRID tblOSStatus *****************************************************************************
    $('#tblOSStatus').DataTable({
        "ajax": {
            "url": "/OrdemServico/OSStatus_ListAll",
            "type": "GET",
            "datatype": "json"
        }
        , "columns": [
            { data: "sos_id", "className": "hide_column", "searchable": false },
            { data: "sos_codigo", "width": "50px", "searchable": true },
            { data: "sos_descricao", "autoWidth": true, "searchable": true },
            {
                "title": "Opções",
                data: "sos_id",
                "searchable": false,
                "sortable": false,
                "render": function (data, type, row) {
                    var retorno = "";
                    if (permissaoEscrita > 0) {
                        retorno = '<a href="#" onclick="return OSStatus_Editar(' + data + ')" title="Editar" ><span class="glyphicon glyphicon-pencil"></span></a>' + '  ';

                        if (row.sos_ativo == 1)
                            retorno += '<a href="#" onclick="return OSStatus_AtivarDesativar(' + data + ', 0)" title="Ativo" ><span class="glyphicon glyphicon-ok text-success"></span></a>' + '  ';
                        else
                            retorno += '<a href="#" onclick="return OSStatus_AtivarDesativar(' + data + ', 1)" title="Desativado" ><span class="glyphicon glyphicon-remove text-danger"></span></a>' + '  ';
                    }
                    else {
                        retorno = '<span class="glyphicon glyphicon-pencil desabilitado"  ></span>' + '  ';

                        if (row.sos_ativo == 1)
                            retorno += '<span class="glyphicon glyphicon-ok text-success desabilitado" ></span>' + '  ';
                        else
                            retorno += '<span class="glyphicon glyphicon-remove text-danger desabilitado"  ></span>' + '  ';

                    }
                    if (permissaoExclusao > 0)
                        retorno += '<a href="#" onclick="return OSStatus_Excluir(' +  data + ')" title="Excluir" ><span class="glyphicon glyphicon-trash"></span></a>';
                    else
                        retorno += '<span class="glyphicon glyphicon-trash desabilitado" title="Excluir" ></span>';

                    return retorno;
                }
            }
        ]
        , "rowId": "sos_id"
        , "order": [1, "asc"]
        , "rowCallback": function (row, data) {
            if (data.sos_id == selectedsos_id)
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

    var tblOSStatus = $('#tblOSStatus').DataTable();

    $('#tblOSStatus tbody').on('click', 'tr', function () {
        if ($(this).hasClass('selected')) {
            $(this).removeClass('selected');
        }
        else {
            tblOSStatus.$('tr.selected').removeClass('selected');
            $(this).addClass('selected');
        }

        var OSStatus_id = tblOSStatus.row(this).data();
        $('#hddnSelectedsos_id').val(OSStatus_id["sos_id"]);
        selectedsos_id = OSStatus_id["sos_id"];

    });


});
