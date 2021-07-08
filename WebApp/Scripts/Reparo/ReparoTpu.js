
function closeModalReparo() {
    $("#modalEdit").modal('hide');
}

function verifica() {

    $('#divPrecoUnitario').show();
    $('#divDataBase').show();

    if ($('#fonte_ad').val() === "1") {
        $('#codDer').hide();
        $('#divPrecoUnitario').hide();
        $('#divDataBase').hide();
    }
    else if ($('#fonte_ad').val() === "2") {
        $('#codDer').hide();
    }
    else if ($('#fonte_ad').val() === "3") {
        $('#codDer').show();
    }
    else if ($('#fonte_ad').val() === "0") {
        $('#codDer').hide();
    }
}

function GetAll() {
    carregaGridReparoTpu();
}

$(document).ready(function () {
    carregaGridReparoTpu();
});

function carregaGridReparoTpu()
{
    $('#tblReparoTpu').DataTable().destroy();
    $('#tblReparoTpu').DataTable({
        "ajax": {
            "url": "/Reparo/ReparoTpu_ListAll",
            "type": "GET",
            "datatype": "json",
        }
         , "columns": [
                { data: "rtu_id", "className": "hide_column" },
                { data: "rpt_id", "className": "hide_column" },
                { data: "rtu_preco_unitario", "className": "hide_column" },
                { data: "rtu_fonte_txt", "className": "hide_column" },
                { data: "rtu_codigo_tpu", "className": "hide_column" },
                { data: "datastring", "className": "hide_column" },
                { data: "fon_id", "className": "hide_column" },
                { data: "rpt_descricao", "autoWidth": true, "searchable": true },
                { data: "unidade", "autoWidth": true, "className": "Centralizado", "searchable": true },
                { data: "rtu_codigo_tpu", "className": "Centralizado", "autoWidth": true, "searchable": true },
                { data: "fon_nome", "autoWidth": true, "className": "Centralizado", "searchable": true },
                { data: "rtu_preco_unitario", "autoWidth": true, "className": "Centralizado", "searchable": true },
                { data: "rtu_data_base", "className": "Centralizado", "autoWidth": true, "searchable": true },
                {
                    "title": "Opções",
                    data: "rtu_id",
                    "searchable": false,
                    "sortable": false,
                    "render": function (data, type, row) {
                        var retorno = "";
                        if (permissaoEscrita > 0) {
                            var apostrofo = "'";
                            var inputs = row["rtu_id"] + ',' + row["rpt_id"] + ',' + apostrofo + (row["rtu_preco_unitario"] + '').replace(',', '.') + apostrofo + ',' + apostrofo + row["rtu_fonte_txt"] + apostrofo + ',' + apostrofo + row["rtu_codigo_tpu"] + apostrofo + ',' + apostrofo + row["datastring"] + apostrofo + ',' + row["fon_id"];

                            retorno = '<a href="#" onclick="return ReparoTpu_Editar(' + inputs + ')" title="Editar" ><span class="glyphicon glyphicon-pencil"></span></a>' + '  ';

                            if (row.rtu_ativo == 1)
                                retorno += '<a href="#" onclick="return ReparoTpu_AtivarDesativar(' + data + ', 0)" title="Ativo" ><span class="glyphicon glyphicon-ok text-success"></span></a>' + '  ';
                            else
                                retorno += '<a href="#" onclick="return ReparoTpu_AtivarDesativar(' + data + ', 1)" title="Desativado" ><span class="glyphicon glyphicon-remove text-danger"></span></a>' + '  ';
                        }
                        else {
                            retorno = '<span class="glyphicon glyphicon-pencil desabilitado"  ></span>' + '  ';

                            if (row.rtu_ativo == 1)
                                retorno += '<span class="glyphicon glyphicon-ok text-success desabilitado" ></span>' + '  ';
                            else
                                retorno += '<span class="glyphicon glyphicon-remove text-danger desabilitado"  ></span>' + '  ';
                        }

                        return retorno;
                    }
                }
             ]
            , "rowId": "rtu_id"
            , "lengthMenu": [[10, 25, 50, 100], [10, 25, 50, 100]]
            , select: {
                style: 'single'
            }
            , searching: true
            , "oLanguage": idioma
            , "pagingType": "input"
            , "sDom": '<"top">rt<"bottom"pfli><"clear">'
          , "initComplete": function (settings, json) {
          }
    });
}


function IntegracaoTPU() {

    if ($('#data_ad').val().length === 10 || $('#codigo_ad').val().length > 2) {
        $.ajax({
            url: '/Reparo/IntegracaoTPU?ano=' + $("#data_ad").val() + '&codItem=' + $('#codigo_ad').val(),
            type: "Get",
            dataType: "JSON",
            success: function (data) {
                if (data != 0) {
                    $('#preco_ad').val(data);
                }
            },
            error: function (erro) {
                alert(erro);
            }
        });
    }


}

function ReparoTpu_AtivarDesativar(id, ativar) {

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
                var response = POST("/Reparo/ReparoTpu_AtivarDesativar", JSON.stringify({ id: id }))
                if (response.erroId == 1) {
                    swal({
                        type: 'success',
                        title: 'Sucesso',
                        text: ativar == 1 ? msgAtivacaoOK : msgDesativacaoOK
                    });

                    $('#tblReparoTpu').DataTable().ajax.reload(null,false);
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

function ReparoTpu_Editar(rtu_id, rpt_id, valor, fonte_txt, codigo, datastring, fonId)
{
    $("#modalEdit").modal('show');
    $('#rtu_id').val(rtu_id);
    $('#reparo_ad').val(rpt_id).change();
    $('#preco_ad').val(valor);
    $('#fonteTxt_ad').val(fonte_txt);
    $('#codigo_ad').val(codigo);
    $('#data_ad').val(datastring.substring(0, 10));
    $('#fonte_ad').val(fonId).change();
    verifica();  
  //  IntegracaoTPU();
    jQuery('#preco_ad').mask('999999.99');
}

function ReparoTPU_Salvar()
{
            $.ajax({
                url: '/Reparo/ReparoTpu_Salvar',
                data: {
                    rpt_id: $('#reparo_ad').val(),
                    fon_id: $('#fonte_ad').val(),
                    rtu_fonte_txt: $('#fonteTxt_ad').val(),
                    rtu_codigo_tpu: $('#codigo_ad').val(),
                    rtu_preco_unitario: $('#preco_ad').val().replace('.',','),
                    rtu_data_base: $('#data_ad').val(),
                    rtu_id: $('#rtu_id').val()
                },
                type: "Post",
                dataType: "JSON",
                success: function (data) {

                    $('#tblReparoTpu').DataTable().ajax.reload(null, false);  //false = sem reload na pagina.

                    $("#modalEdit").modal('hide');
                    return false;
                },
                error: function (erro) {
                    swal({
                        type: 'error',
                        title: 'Aviso',
                        text: 'Erro ao salvar registro'
                    });
                    return false;
                }
            });
            return false;
}