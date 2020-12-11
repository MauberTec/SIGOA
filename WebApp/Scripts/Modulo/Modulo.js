var selectedId;

function Modulo_AtivarDesativar(id, ativar) {
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
            var response = POST("/Modulo/Modulo_AtivarDesativar", JSON.stringify({ id: id }))
            if (response.erroId == 1) {
                swal({
                    type: 'success',
                    title: 'Sucesso',
                    text: ativar == 1 ? msgAtivacaoOK : msgDesativacaoOK
                });

                location.reload(); // tem reload na pagina para reconstruir os menus
            }
            else {
                swal({
                    type: 'error',
                    title: 'Aviso',
                    text: ativar == 1 ? msgAtivacaoErro : msgDesativacaoErro
                });
            }
            return false;
        } else {
            return false;
        }
    })

    return false;
}

function Modulo_Editar(id) {
    $('#par_valor').css('border-color', 'lightgrey');
    $.ajax({
        url: "/Modulo/Modulo_getbyID/" + id,
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            $('#txtmod_id').val(result.mod_id);
            $('#txtmod_nome_modulo').val(result.mod_nome_modulo);
            $('#txtmod_descricao').val(result.mod_descricao);
            $('#chkmod_ativo').prop('checked', (result.mod_ativo == '1' ? true : false));

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

function Modulo_Salvar() {
    var txtmod_nome_modulo = document.getElementById('txtmod_nome_modulo');
    var txtmod_descricao = document.getElementById('txtmod_descricao');

    if (validaAlfaNumerico(txtmod_nome_modulo) && validaAlfaNumerico(txtmod_descricao)) {

        var modulo = {
            mod_id: $('#txtmod_id').val(),
            mod_nome_modulo: $('#txtmod_nome_modulo').val(),
            mod_descricao: $('#txtmod_descricao').val(),
            mod_ativo: $('#chkmod_ativo').prop('checked') ? 1 : 0
        };

        $.ajax({
            url: "/Modulo/Modulo_Salvar",
            data: JSON.stringify(modulo),
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                $("#modalSalvarRegistro").modal('hide');
                // window.location.href = (location.href + "?temp=" + new Date().getTime()); // faz reload na pagina para atualizar os menus
                location.reload();
                return true;
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
    else {
        $("#modalSalvarRegistro").modal('show');
        return false;
    }
    return false;
}

// montagem do gridview
$(document).ready(function () {
    $('#tblModulos').DataTable(
        {
            "ajax": {
                "url": "/Modulo/Modulo_ListAll",
                "type": "GET",
                "datatype": "json"
            }
            , "columns": [
                { data: "mod_id", "className": "hide_column" },
                { data: "mod_pai_id", "className": "hide_column" },
                { data: "mod_nome_modulo", "autoWidth": true, "sortable": false },
                { data: "mod_descricao", "autoWidth": true, "sortable": false },
                {
                    "title": "Opções",
                    data: "mod_id",
                    "searchable": false,
                    "sortable": false,
                    "render": function (data, type, row) {
                        var retorno = "";

                        if (permissaoEscrita > 0) {
                            retorno = '<a href="#" onclick="return Modulo_Editar(' + data + ')" title="Editar" ><span class="glyphicon glyphicon-pencil"></span></a>' + '  ';

                            if (row.mod_ativo == 1)
                                retorno += '<a href="#" onclick="return Modulo_AtivarDesativar(' + data + ', 0)" title="Ativo" ><span class="glyphicon glyphicon-ok text-success"></span></a>' + '  ';
                            else
                                retorno += '<a href="#" onclick="return Modulo_AtivarDesativar(' + data + ', 1)" title="Desativado" ><span class="glyphicon glyphicon-remove text-danger"></span></a>' + '  ';
                        }
                        else {
                            retorno = '<span class="glyphicon glyphicon-pencil desabilitado"  ></span>' + '  ';
                            if (row.mod_ativo == 1)
                                retorno += '<span class="glyphicon glyphicon-ok text-success desabilitado" ></span>' + '  ';
                            else
                                retorno += '<span class="glyphicon glyphicon-remove text-danger desabilitado"  ></span>' + '  ';

                        }
                        retorno += '<span class="glyphicon glyphicon-trash desabilitado" title="Excluir" ></span>';
                        return retorno;
                    }
                }
            ]
            , "columnDefs": [
                {
                    targets: [2, 3]
                    , "createdCell": function (td, cellData, rowData, row, col) {
                        if (rowData["mod_pai_id"] >= 0) {
                            $(td).addClass('moduloFilho');
                        }
                    }
                }
            ]
            , "createdRow": function (row, data, dataIndex) {
                if (data["mod_pai_id"] < 0) {
                    $(row).addClass('moduloPai');
                }
            }
            , "lengthMenu": [[10, 25, 50, 100], [10, 25, 50, 100]]
            , select: {
                style: 'single'
            }
            , searching: false
            , "oLanguage": idioma
            , "pagingType": "input"
            , "sDom": '<"top">rt<"bottom"pfli><"clear">'
        });
});

