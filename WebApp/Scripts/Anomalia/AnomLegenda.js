
var selectedleg_id;

function AnomLegenda_Inserir() {
    var corBranca = "rgb(255, 255, 255)";
    $('#txtleg_codigo').css("background-color", corBranca);
    $('#txtleg_descricao').css("background-color", corBranca);

    $('#txtleg_codigo').val("");
    $('#txtleg_descricao').val("");
    $('#chkleg_ativo').prop('checked', true);

    $('#txtAnomLegenda').css('border-color', 'lightgrey');

    $('#chkleg_ativo').css('border-color', 'lightgrey');

    $("#modalSalvarRegistro").modal('show');
    document.getElementById("lblModalHeader").innerText = "Nova Legenda de Anomalia";

    selectedleg_id = -1;
}

function AnomLegenda_Salvar() {
    var txtleg_codigo = document.getElementById('txtleg_codigo');
    txtleg_codigo.value = txtleg_codigo.value.trim();

    var txtleg_descricao = document.getElementById('txtleg_descricao');
    txtleg_descricao.value = txtleg_descricao.value.trim();

    if (validaAlfaNumerico(txtleg_descricao) && (ChecaRepetido(txtleg_codigo)) && validaAlfaNumericoSemAcentosNemEspaco(txtleg_codigo, 0)) {

        var AnomLegenda = {
            leg_id: selectedleg_id,
            leg_codigo: $('#txtleg_codigo').val(),
            leg_descricao: $('#txtleg_descricao').val(),
            leg_ativo: $('#chkleg_ativo').prop('checked') ? 1 : 0 
        };

        $.ajax({
            url: "/Anomalia/AnomLegenda_Salvar",
            data: JSON.stringify(AnomLegenda),
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                $("#modalSalvarRegistro").modal('hide');
                $('#tblAnomLegendas').DataTable().ajax.reload(null, false);  //false = sem reload na pagina.

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

function AnomLegenda_Excluir(id) {
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
            var response = POST("/Anomalia/AnomLegenda_Excluir", JSON.stringify({ id: id }))
            if (response.erroId >= 1) {
                swal({
                    type: 'success',
                    title: 'Sucesso',
                    text: 'Registro excluído com sucesso'
                });

                $('#tblAnomLegendas').DataTable().ajax.reload();
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

function AnomLegenda_AtivarDesativar(id, ativar) {

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
                var response = POST("/Anomalia/AnomLegenda_AtivarDesativar", JSON.stringify({ id: id }))
                if (response.erroId == 1) {
                    swal({
                        type: 'success',
                        title: 'Sucesso',
                        text: ativar == 1 ? msgAtivacaoOK : msgDesativacaoOK
                    });

                    $('#tblAnomLegendas').DataTable().ajax.reload();
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

function AnomLegenda_Editar(id) {
    document.getElementById("lblModalHeader").innerText = "Editar Legenda de Anomalia";

    var corBranca = "rgb(255, 255, 255)";
    $('#txtleg_codigo').css("background-color", corBranca);
    $('#txtleg_descricao').css("background-color", corBranca);

    $('#txtleg_descricao').css('border-color', 'lightgrey');
    $.ajax({
        url: "/Anomalia/AnomLegenda_GetbyID/" + id,
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            selectedleg_id = id;
            $('#txtleg_codigo').val(result.leg_codigo);
            $('#txtleg_descricao').val(result.leg_descricao);
            $('#chkleg_ativo').prop('checked', (result.leg_ativo == '1' ? true : false));

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
        var rowId = $('#tblAnomLegendas').DataTable().column(1).search(searchValue, true, false).rows({ filter: 'applied' }).data();
        if (rowId.length > 0) { // ja tem
            if (selectedleg_id != rowId[0]["leg_id"]) {
                $('#txtleg_descricao').css("background-color", corVermelho);
                swal({
                    type: 'error',
                    title: 'Aviso',
                    text: 'Legenda já cadastrada'
                }).then(
                    function () {
                        return false;
                    });
            }
            else {
                $('#tblAnomLegendas').DataTable().search('').columns().search('').draw();
                return true;
            }
        }
        else { // nao tem
            $('#txtleg_descricao').css("background-color", corBranca);
            $('#tblAnomLegendas').DataTable().search('').columns().search('').draw();
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

    // ****************************GRID tblAnomLegendas *****************************************************************************
    $('#tblAnomLegendas').DataTable({
        "ajax": {
            "url": "/Anomalia/AnomLegenda_ListAll",
            "type": "GET",
            "datatype": "json"
        }
        , "columns": [
            { data: "leg_id", "width": "30px", "className": "hide_column", "searchable": false },
            { data: "leg_codigo", "width": "80px", "searchable": true },
            { data: "leg_descricao", "autoWidth": true, "searchable": true },
            {
                "title": "Opções",
                data: "leg_id",
                "searchable": false,
                "sortable": false,
                "render": function (data, type, row) {
                    var retorno = "";
                    if (permissaoEscrita > 0) {
                        retorno = '<a href="#" onclick="return AnomLegenda_Editar(' + data + ')" title="Editar" ><span class="glyphicon glyphicon-pencil"></span></a>' + '  ';

                        if (row.leg_ativo == 1)
                            retorno += '<a href="#" onclick="return AnomLegenda_AtivarDesativar(' + data + ', 0)" title="Ativo" ><span class="glyphicon glyphicon-ok text-success"></span></a>' + '  ';
                        else
                            retorno += '<a href="#" onclick="return AnomLegenda_AtivarDesativar(' + data + ', 1)" title="Desativado" ><span class="glyphicon glyphicon-remove text-danger"></span></a>' + '  ';
                    }
                    else {
                        retorno = '<span class="glyphicon glyphicon-pencil desabilitado"  ></span>' + '  ';

                        if (row.leg_ativo == 1)
                            retorno += '<span class="glyphicon glyphicon-ok text-success desabilitado" ></span>' + '  ';
                        else
                            retorno += '<span class="glyphicon glyphicon-remove text-danger desabilitado"  ></span>' + '  ';

                    }

                    if (permissaoExclusao > 0)
                        retorno += '<a href="#" onclick="return AnomLegenda_Excluir(' + data + ')" title="Excluir" ><span class="glyphicon glyphicon-trash"></span></a>';
                    else
                        retorno += '<span class="glyphicon glyphicon-trash desabilitado" title="Excluir" ></span>';

                    return retorno;
                }
            }
        ]
        , "rowId": "leg_id"
        , "rowCallback": function (row, data) {
            if (data.leg_id == selectedleg_id)
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

    var tblAnomLegendas = $('#tblAnomLegendas').DataTable();

    $('#tblAnomLegendas tbody').on('click', 'tr', function () {
        if ($(this).hasClass('selected')) {
            $(this).removeClass('selected');
        }
        else {
            tblAnomLegendas.$('tr.selected').removeClass('selected');
            $(this).addClass('selected');
        }

        var AnomLegenda_id = tblAnomLegendas.row(this).data();
        $('#hddnSelectedleg_id').val(AnomLegenda_id["leg_id"]);
        selectedleg_id = AnomLegenda_id["leg_id"];

    });


});
