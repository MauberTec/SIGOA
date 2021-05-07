
var selectedrpt_id;

function ReparoTipo_Inserir() {
    var corBranca = "rgb(255, 255, 255)";
    $('#txtrpt_codigo').css("background-color", corBranca);
    $('#txtrpt_descricao').css("background-color", corBranca);

    $('#txtrpt_codigo').val("");
    $('#txtrpt_descricao').val("");
    $('#chkrpt_ativo').prop('checked', true);

    $('#txtReparoTipo').css('border-color', 'lightgrey');
    $('#chkrpt_ativo').css('border-color', 'lightgrey');

    $("#modalSalvarRegistro").modal('show');
    document.getElementById("lblModalHeader").innerText = "Novo Tipo de Reparo";

    selectedrpt_id = -1;
}

function ReparoTipo_Salvar() {
    var txtrpt_codigo = document.getElementById('txtrpt_codigo');
    txtrpt_codigo.value = txtrpt_codigo.value.trim();

    var txtrpt_descricao = document.getElementById('txtrpt_descricao');
    txtrpt_descricao.value = txtrpt_descricao.value.trim();

    if (validaAlfaNumericoAcentosAfins(txtrpt_descricao, 0, 0) && (ChecaRepetido(txtrpt_codigo)) && validaAlfaNumericoSemAcentosNemEspaco(txtrpt_codigo, 0))
{   

        var ReparoTipo = {
            rpt_id: selectedrpt_id,
            rpt_codigo: $('#txtrpt_codigo').val(),
            rpt_descricao: $('#txtrpt_descricao').val(),
            rpt_ativo: $('#chkrpt_ativo').prop('checked') ? 1 : 0 
        };

        $.ajax({
            url: "/Reparo/ReparoTipo_Salvar",
            data: JSON.stringify(ReparoTipo),
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                $("#modalSalvarRegistro").modal('hide');
                $('#tblReparoTipos').DataTable().ajax.reload(null, false);  //false = sem reload na pagina.

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

function ReparoTipo_Excluir(id) {
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
            var response = POST("/Reparo/ReparoTipo_Excluir", JSON.stringify({ id: id }))
            if (response.erroId >= 1) {
                swal({
                    type: 'success',
                    title: 'Sucesso',
                    text: 'Registro excluído com sucesso'
                });

                $('#tblReparoTipos').DataTable().ajax.reload();
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

function ReparoTipo_AtivarDesativar(id, ativar) {

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
                var response = POST("/Reparo/ReparoTipo_AtivarDesativar", JSON.stringify({ id: id }))
                if (response.erroId == 1) {
                    swal({
                        type: 'success',
                        title: 'Sucesso',
                        text: ativar == 1 ? msgAtivacaoOK : msgDesativacaoOK
                    });

                    $('#tblReparoTipos').DataTable().ajax.reload();
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

function ReparoTipo_Editar(id) {
    document.getElementById("lblModalHeader").innerText = "Editar Tipo de Reparo";

    var corBranca = "rgb(255, 255, 255)";
    $('#txtrpt_codigo').css("background-color", corBranca);
    $('#txtrpt_descricao').css("background-color", corBranca);

    $('#txtrpt_descricao').css('border-color', 'lightgrey');
    $.ajax({
        url: "/Reparo/ReparoTipo_GetbyID/" + id,
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            selectedrpt_id = id;
            $('#txtrpt_codigo').val(result.rpt_codigo);
            $('#txtrpt_descricao').val(result.rpt_descricao);
            $('#chkrpt_ativo').prop('checked', (result.rpt_ativo == '1' ? true : false));

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
        var rowId = $('#tblReparoTipos').DataTable().column(1).search(searchValue, true, false).rows({ filter: 'applied' }).data();
        if (rowId.length > 0) { // ja tem
            if (selectedrpt_id != rowId[0]["rpt_id"]) {
                $('#txtrpt_descricao').css("background-color", corVermelho);
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
                $('#tblReparoTipos').DataTable().search('').columns().search('').draw();
                return true;
            }
        }
        else { // nao tem
            $('#txtrpt_descricao').css("background-color", corBranca);
            $('#tblReparoTipos').DataTable().search('').columns().search('').draw();
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

    // ****************************GRID tblReparoTipos *****************************************************************************
    $('#tblReparoTipos').DataTable({
        "ajax": {
            "url": "/Reparo/ReparoTipo_ListAll",
            "type": "GET",
            "datatype": "json"
        }
        , "columns": [
            { data: "rpt_id", "width": "30px", "className": "hide_column", "searchable": false },
            { data: "rpt_codigo", "width": "80px", "searchable": true },
            { data: "rpt_descricao", "autoWidth": true, "searchable": true },
            {
                "title": "Opções",
                data: "rpt_id",
                "searchable": false,
                "sortable": false,
                "render": function (data, type, row) {
                    var retorno = "";
                    if (permissaoEscrita > 0) {
                        retorno = '<a href="#" onclick="return ReparoTipo_Editar(' + data + ')" title="Editar" ><span class="glyphicon glyphicon-pencil"></span></a>' + '  ';

                        if (row.rpt_ativo == 1)
                            retorno += '<a href="#" onclick="return ReparoTipo_AtivarDesativar(' + data + ', 0)" title="Ativo" ><span class="glyphicon glyphicon-ok text-success"></span></a>' + '  ';
                        else
                            retorno += '<a href="#" onclick="return ReparoTipo_AtivarDesativar(' + data + ', 1)" title="Desativado" ><span class="glyphicon glyphicon-remove text-danger"></span></a>' + '  ';
                    }
                    else {
                        retorno = '<span class="glyphicon glyphicon-pencil desabilitado"  ></span>' + '  ';

                        if (row.rpt_ativo == 1)
                            retorno += '<span class="glyphicon glyphicon-ok text-success desabilitado" ></span>' + '  ';
                        else
                            retorno += '<span class="glyphicon glyphicon-remove text-danger desabilitado"  ></span>' + '  ';

                    }

                    if (permissaoExclusao > 0)
                        retorno += '<a href="#" onclick="return ReparoTipo_Excluir(' + data + ')" title="Excluir" ><span class="glyphicon glyphicon-trash"></span></a>';
                    else
                        retorno += '<span class="glyphicon glyphicon-trash desabilitado" title="Excluir" ></span>';

                    return retorno;
                }
            }
        ]
        , "rowId": "rpt_id"
        , "rowCallback": function (row, data) {
            if (data.rpt_id == selectedrpt_id)
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

    var tblReparoTipos = $('#tblReparoTipos').DataTable();

    $('#tblReparoTipos tbody').on('click', 'tr', function () {
        if ($(this).hasClass('selected')) {
            $(this).removeClass('selected');
        }
        else {
            tblReparoTipos.$('tr.selected').removeClass('selected');
            $(this).addClass('selected');
        }

        var ReparoTipo_id = tblReparoTipos.row(this).data();
        $('#hddnSelectedrpt_id').val(ReparoTipo_id["rpt_id"]);
        selectedrpt_id = ReparoTipo_id["rpt_id"];

    });

    jQuery('#txtrpt_codigo').mask('999')
});
