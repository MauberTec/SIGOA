
var selectedatp_id;

function AnomTipo_Inserir() {
    var corBranca = "rgb(255, 255, 255)";
    $('#txtatp_codigo').css("background-color", corBranca);
    $('#txtatp_descricao').css("background-color", corBranca);

    $('#txtatp_codigo').val("");
    $('#txtatp_descricao').val("");
    $('#chkatp_ativo').prop('checked', true);

    $('#txtAnomTipo').css('border-color', 'lightgrey');
    $('#chkatp_ativo').css('border-color', 'lightgrey');

    $("#modalSalvarRegistro").modal('show');
    document.getElementById("lblModalHeader").innerText = "Novo Tipo de Anomalia";

    selectedatp_id = -1;
}

function AnomTipo_Salvar() {
    var txtatp_codigo = document.getElementById('txtatp_codigo');
    txtatp_codigo.value = txtatp_codigo.value.trim();

    var txtatp_descricao = document.getElementById('txtatp_descricao');
    txtatp_descricao.value = txtatp_descricao.value.trim();

    if (validaAlfaNumerico(txtatp_descricao) && (ChecaRepetido(txtatp_codigo)) && validaAlfaNumericoSemAcentosNemEspaco(txtatp_codigo, 0)) {

        var AnomTipo = {
            atp_id: selectedatp_id,
            atp_codigo: $('#txtatp_codigo').val(),
            atp_descricao: $('#txtatp_descricao').val(),
            atp_ativo: $('#chkatp_ativo').prop('checked') ? 1 : 0 
        };

        $.ajax({
            url: "/Anomalia/AnomTipo_Salvar",
            data: JSON.stringify(AnomTipo),
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                $("#modalSalvarRegistro").modal('hide');
                $('#tblAnomTipos').DataTable().ajax.reload(null, false);  //false = sem reload na pagina.

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

function AnomTipo_Excluir(id) {
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
            var response = POST("/Anomalia/AnomTipo_Excluir", JSON.stringify({ id: id }))
            if (response.erroId >= 1) {
                swal({
                    type: 'success',
                    title: 'Sucesso',
                    text: 'Registro excluído com sucesso'
                });

                $('#tblAnomTipos').DataTable().ajax.reload();
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

function AnomTipo_AtivarDesativar(id, ativar) {

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
                var response = POST("/Anomalia/AnomTipo_AtivarDesativar", JSON.stringify({ id: id }))
                if (response.erroId == 1) {
                    swal({
                        type: 'success',
                        title: 'Sucesso',
                        text: ativar == 1 ? msgAtivacaoOK : msgDesativacaoOK
                    });

                    $('#tblAnomTipos').DataTable().ajax.reload();
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

function AnomTipo_Editar(id) {
    document.getElementById("lblModalHeader").innerText = "Editar Tipo de Anomalia";

    var corBranca = "rgb(255, 255, 255)";
    $('#txtatp_codigo').css("background-color", corBranca);
    $('#txtatp_descricao').css("background-color", corBranca);

    $('#txtatp_descricao').css('border-color', 'lightgrey');
    $.ajax({
        url: "/Anomalia/AnomTipo_GetbyID/" + id,
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            selectedatp_id = id;
            $('#txtatp_codigo').val(result.atp_codigo);
            $('#txtatp_descricao').val(result.atp_descricao);
            $('#chkatp_ativo').prop('checked', (result.atp_ativo == '1' ? true : false));

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
        var rowId = $('#tblAnomTipos').DataTable().column(1).search(searchValue, true, false).rows({ filter: 'applied' }).data();
        if (rowId.length > 0) { // ja tem
            if (selectedatp_id != rowId[0]["atp_id"]) {
                $('#txtatp_descricao').css("background-color", corVermelho);
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
                $('#tblAnomTipos').DataTable().search('').columns().search('').draw();
                return true;
            }
        }
        else { // nao tem
            $('#txtatp_descricao').css("background-color", corBranca);
            $('#tblAnomTipos').DataTable().search('').columns().search('').draw();
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

    // ****************************GRID tblAnomTipos *****************************************************************************
    $('#tblAnomTipos').DataTable({
        "ajax": {
            "url": "/Anomalia/AnomTipo_ListAll",
            "type": "GET",
            "datatype": "json"
        }
        , "columns": [
            { data: "atp_id", "width": "30px", "className": "hide_column", "searchable": false },
            { data: "atp_codigo", "width": "80px", "searchable": true },
            { data: "atp_descricao", "autoWidth": true, "searchable": true },
            {
                "title": "Opções",
                data: "atp_id",
                "searchable": false,
                "sortable": false,
                "render": function (data, type, row) {
                    var retorno = "";
                    if (permissaoEscrita > 0) {
                        retorno = '<a href="#" onclick="return AnomTipo_Editar(' + data + ')" title="Editar" ><span class="glyphicon glyphicon-pencil"></span></a>' + '  ';

                        if (row.atp_ativo == 1)
                            retorno += '<a href="#" onclick="return AnomTipo_AtivarDesativar(' + data + ', 0)" title="Ativo" ><span class="glyphicon glyphicon-ok text-success"></span></a>' + '  ';
                        else
                            retorno += '<a href="#" onclick="return AnomTipo_AtivarDesativar(' + data + ', 1)" title="Desativado" ><span class="glyphicon glyphicon-remove text-danger"></span></a>' + '  ';
                    }
                    else {
                        retorno = '<span class="glyphicon glyphicon-pencil desabilitado"  ></span>' + '  ';

                        if (row.atp_ativo == 1)
                            retorno += '<span class="glyphicon glyphicon-ok text-success desabilitado" ></span>' + '  ';
                        else
                            retorno += '<span class="glyphicon glyphicon-remove text-danger desabilitado"  ></span>' + '  ';

                    }

                    if (permissaoExclusao > 0)
                        retorno += '<a href="#" onclick="return AnomTipo_Excluir(' + data + ')" title="Excluir" ><span class="glyphicon glyphicon-trash"></span></a>';
                    else
                        retorno += '<span class="glyphicon glyphicon-trash desabilitado" title="Excluir" ></span>';

                    return retorno;
                }
            }
        ]
        , "rowId": "atp_id"
        , "rowCallback": function (row, data) {
            if (data.atp_id == selectedatp_id)
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

    var tblAnomTipos = $('#tblAnomTipos').DataTable();

    $('#tblAnomTipos tbody').on('click', 'tr', function () {
        if ($(this).hasClass('selected')) {
            $(this).removeClass('selected');
        }
        else {
            tblAnomTipos.$('tr.selected').removeClass('selected');
            $(this).addClass('selected');
        }

        var AnomTipo_id = tblAnomTipos.row(this).data();
        $('#hddnSelectedatp_id').val(AnomTipo_id["atp_id"]);
        selectedatp_id = AnomTipo_id["atp_id"];

    });


});
