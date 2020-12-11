
var selectedfst_id;


function cmbStatusDe_onchange(quem) {

    var selectedVal = $("#cmbStatusDe").val(); 

    // filtra o grid para saber se há repeticoes

    var searchValue = '\\b' + selectedVal + '\\b';
    $('#tblAnomFluxoStatus').DataTable().column(1).search(selectedVal).draw();

    var arr_encontrados = [];
    $('#tblAnomFluxoStatus').DataTable().column(2, { search: 'applied' }).data().each(function (value, index) {
        if (parseInt(value) != parseInt(quem)) // só acrescenta se for adicao. Edicao nao
            arr_encontrados.push(value);
    });

    // acrescenta o valor selecionado
    if (quem != 1) // só acrescenta se for adicao. Edicao nao
        arr_encontrados.push(parseInt(selectedVal));


    // busca no outro combo e desabilita
    var op = document.getElementById("cmbStatusPara").getElementsByTagName("option");

    // habilita todos os valores 
    for (var i = 0; i < op.length; i++)
        op[i].disabled = false;

    // desabilita os encontrados
    for (var i = 0; i < op.length; i++) {
        for (var j = 0; j < arr_encontrados.length; j++) {
            if (parseInt(op[i].value) == parseInt(arr_encontrados[j]))
                op[i].disabled = true;
        }
    }

    $("#cmbStatusPara").val(null);

    // remove o filtro
    $('#tblAnomFluxoStatus').DataTable().column(1).search('').draw();
    $('#tblAnomFluxoStatus').DataTable().column(2).search('').draw();

}

function AnomFluxoStatus_Inserir() {
    var corBranca = "rgb(255, 255, 255)";
    $('#txtfst_descricao').css("background-color", corBranca);

    $('#txtfst_id').val("");

    $('#cmbStatusDe').val(null);
    $('#cmbStatusPara').val(null);

    $('#txtfst_descricao').val("");
    $('#chkfst_ativo').prop('checked', true);

    $('#btnUpdate').hide();
    $('#btnAdd').show();
    $('#txtAnomFluxoStatus').css('border-color', 'lightgrey');
    $('#chkfst_ativo').css('border-color', 'lightgrey');

    $("#modalSalvarRegistro").modal('show');
    document.getElementById("lblModalHeader").innerText = "Novo Fluxo de Status de Anomalia";

    selectedfst_id = -1;
}

function AnomFluxoStatus_Salvar() {

    var txtfst_descricao = document.getElementById('txtfst_descricao');
    txtfst_descricao.value = txtfst_descricao.value.trim();

    if (validaAlfaNumerico(txtfst_descricao)) {

        var AnomFluxoStatus = {
            fst_id: $('#txtfst_id').val(),
            ast_id_de: $('#cmbStatusDe').val(),
            ast_id_para: $('#cmbStatusPara').val(),
            fst_descricao: $('#txtfst_descricao').val(),
            fst_ativo: $('#chkfst_ativo').prop('checked') ? 1 : 0 
        };

        $.ajax({
            url: "/Anomalia/AnomFluxoStatus_Salvar",
            data: JSON.stringify(AnomFluxoStatus),
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                $("#modalSalvarRegistro").modal('hide');
                $('#tblAnomFluxoStatus').DataTable().ajax.reload(null, false);  //false = sem reload na pagina.

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

function AnomFluxoStatus_Excluir(id) {

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
            var response = POST("/Anomalia/AnomFluxoStatus_Excluir", JSON.stringify({ id: id }))
            if (response.erroId >= 1) {
                swal({
                    type: 'success',
                    title: 'Sucesso',
                    text: 'Registro excluído com sucesso'
                });

                $('#tblAnomFluxoStatus').DataTable().ajax.reload();
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

function AnomFluxoStatus_AtivarDesativar(id, ativar) {

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
                var response = POST("/Anomalia/AnomFluxoStatus_AtivarDesativar", JSON.stringify({ id: id }))
                if (response.erroId == 1) {
                    swal({
                        type: 'success',
                        title: 'Sucesso',
                        text: ativar == 1 ? msgAtivacaoOK : msgDesativacaoOK
                    });

                    $('#tblAnomFluxoStatus').DataTable().ajax.reload();
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

function AnomFluxoStatus_Editar(id) {
    document.getElementById("lblModalHeader").innerText = "Editar Fluxo de Status de Anomalia";

    var corBranca = "rgb(255, 255, 255)";
    $('#txtfst_descricao').css("background-color", corBranca);
    $('#txtfst_descricao').css('border-color', 'lightgrey');

    $.ajax({
        url: "/Anomalia/AnomFluxoStatus_GetbyID/" + id,
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            $('#txtfst_id').val(result.fst_id);

            $('#cmbStatusDe').val(result.ast_id_de);

            $('#txtfst_descricao').val(result.fst_descricao);
            $('#chkfst_ativo').prop('checked', (result.fst_ativo == '1' ? true : false));

            cmbStatusDe_onchange(result.ast_id_para);
            $('#cmbStatusPara').val(result.ast_id_para);

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
        var rowId = $('#tblAnomFluxoStatus').DataTable().column(1).search(searchValue, true, false).rows({ filter: 'applied' }).data();
        if (rowId.length > 0) { // ja tem
            if (selectedfst_id != rowId[0]["fst_id"]) {
                $('#txtfst_descricao').css("background-color", corVermelho);
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
                $('#tblAnomFluxoStatus').DataTable().search('').columns().search('').draw();
                return true;
            }
        }
        else { // nao tem
            $('#txtfst_descricao').css("background-color", corBranca);
            $('#tblAnomFluxoStatus').DataTable().search('').columns().search('').draw();
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

    // ****************************GRID tblAnomFluxoStatus *****************************************************************************
    $('#tblAnomFluxoStatus').DataTable({
        "ajax": {
            "url": "/Anomalia/AnomFluxoStatus_ListAll",
            "type": "GET",
            "datatype": "json"
        }
        , "columns": [
            { data: "fst_id", "className": "hide_column", "searchable": true },
            { data: "ast_id_de", "className": "hide_column", "searchable": true },

            { data: "ast_id_para", "className": "hide_column", "searchable": true },
            { data: "ast_de_descricao", "autoWidth": true, "searchable": false },
            { data: "fst_id", "searchable": false,"sortable": false,
                "render": function (data, type, row) {
                    return '<span class="glyphicon glyphicon-arrow-right"  ></span>';
                }
            },
            { data: "ast_para_descricao", "autoWidth": true, "searchable": false },
            { data: "fst_descricao", "autoWidth": true, "searchable": false },
            {
                "title": "Opções",
                data: "fst_id",
                "searchable": false,
                "sortable": false,
                "render": function (data, type, row) {
                    var retorno = "";
                    if (permissaoEscrita > 0) {
                        retorno = '<a href="#" onclick="return AnomFluxoStatus_Editar(' + data + ')" title="Editar" ><span class="glyphicon glyphicon-pencil"></span></a>' + '  ';
                    }
                    else {
                        retorno = '<span class="glyphicon glyphicon-pencil desabilitado"  ></span>' + '  ';
                    }

                    retorno += '<span class="glyphicon glyphicon-ok text-success desabilitado" ></span>' + '  ';

                    if (permissaoExclusao > 0)
                        retorno += '<a href="#" onclick="return AnomFluxoStatus_Excluir(' +  data + ')" title="Excluir" ><span class="glyphicon glyphicon-trash"></span></a>';
                    else
                        retorno += '<span class="glyphicon glyphicon-trash desabilitado" title="Excluir" ></span>';

                    return retorno;
                }
            }
        ]
        , "rowId": "fst_id"
        , "order": [1, "asc"]
        , "rowCallback": function (row, data) {
            if (data.fst_id == selectedfst_id)
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

    var tblAnomFluxoStatus = $('#tblAnomFluxoStatus').DataTable();

    $('#tblAnomFluxoStatus tbody').on('click', 'tr', function () {
        if ($(this).hasClass('selected')) {
            $(this).removeClass('selected');
        }
        else {
            tblAnomFluxoStatus.$('tr.selected').removeClass('selected');
            $(this).addClass('selected');
        }

        var AnomFluxoStatus_id = tblAnomFluxoStatus.row(this).data();
        $('#hddnSelectedfst_id').val(AnomFluxoStatus_id["fst_id"]);
        selectedfst_id = AnomFluxoStatus_id["fst_id"];

    });


});
