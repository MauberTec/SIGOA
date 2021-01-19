
var selectedocf_id;


function cmbStatusDe_onchange(quem) {

    var selectedVal = $("#cmbStatusDe").val(); 

    // filtra o grid para saber se há repeticoes

    var searchValue = '\\b' + selectedVal + '\\b';
    $('#tblOrcamentoFluxoStatus').DataTable().column(1).search(selectedVal).draw();

    var arr_encontrados = [];
    $('#tblOrcamentoFluxoStatus').DataTable().column(2, { search: 'applied' }).data().each(function (value, index) {
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
    $('#tblOrcamentoFluxoStatus').DataTable().column(1).search('').draw();
    $('#tblOrcamentoFluxoStatus').DataTable().column(2).search('').draw();

}

function OrcamentoFluxoStatus_Inserir() {
    var corBranca = "rgb(255, 255, 255)";
    $('#txtocf_descricao').css("background-color", corBranca);

    $('#txtocf_id').val("");

    $('#cmbStatusDe').val(null);
    $('#cmbStatusPara').val(null);

    $('#txtocf_descricao').val("");
    $('#chkocf_ativo').prop('checked', true);

    $('#btnUpdate').hide();
    $('#btnAdd').show();
    $('#txtOrcamentoFluxoStatus').css('border-color', 'lightgrey');
    $('#chkocf_ativo').css('border-color', 'lightgrey');

    $("#modalSalvarRegistro").modal('show');
    document.getElementById("lblModalHeader").innerText = "Novo Fluxo de Status de Orçamento";

    selectedocf_id = -1;
}

function OrcamentoFluxoStatus_Salvar() {

    var txtocf_descricao = document.getElementById('txtocf_descricao');
    txtocf_descricao.value = txtocf_descricao.value.trim();

    if (validaAlfaNumerico(txtocf_descricao)) {

        var OrcamentoFluxoStatus = {
            ocf_id: $('#txtocf_id').val(),
            ocs_id_de: $('#cmbStatusDe').val(),
            ocs_id_para: $('#cmbStatusPara').val(),
            ocf_descricao: $('#txtocf_descricao').val(),
            ocf_ativo: $('#chkocf_ativo').prop('checked') ? 1 : 0 
        };

        $.ajax({
            url: "/Orcamento/OrcamentoFluxoStatus_Salvar",
            data: JSON.stringify(OrcamentoFluxoStatus),
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                $("#modalSalvarRegistro").modal('hide');
                $('#tblOrcamentoFluxoStatus').DataTable().ajax.reload(null, false);  //false = sem reload na pagina.

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

function OrcamentoFluxoStatus_Excluir(id) {

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
            var response = POST("/Orcamento/OrcamentoFluxoStatus_Excluir", JSON.stringify({ id: id }))
            if (response.erroId >= 1) {
                swal({
                    type: 'success',
                    title: 'Sucesso',
                    text: 'Registro excluído com sucesso'
                });

                $('#tblOrcamentoFluxoStatus').DataTable().ajax.reload();
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

function OrcamentoFluxoStatus_AtivarDesativar(id, ativar) {

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
                var response = POST("/Orcamento/OrcamentoFluxoStatus_AtivarDesativar", JSON.stringify({ id: id }))
                if (response.erroId == 1) {
                    swal({
                        type: 'success',
                        title: 'Sucesso',
                        text: ativar == 1 ? msgAtivacaoOK : msgDesativacaoOK
                    });

                    $('#tblOrcamentoFluxoStatus').DataTable().ajax.reload();
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

function OrcamentoFluxoStatus_Editar(id) {
    document.getElementById("lblModalHeader").innerText = "Editar Fluxo de Status de Orçamento";

    var corBranca = "rgb(255, 255, 255)";
    $('#txtocf_descricao').css("background-color", corBranca);
    $('#txtocf_descricao').css('border-color', 'lightgrey');

    $.ajax({
        url: "/Orcamento/OrcamentoFluxoStatus_GetbyID/" + id,
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            $('#txtocf_id').val(result.ocf_id);

            $('#cmbStatusDe').val(result.ocs_id_de);

            $('#txtocf_descricao').val(result.ocf_descricao);
            $('#chkocf_ativo').prop('checked', (result.ocf_ativo == '1' ? true : false));

            cmbStatusDe_onchange(result.ocs_id_para);
            $('#cmbStatusPara').val(result.ocs_id_para);

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
        var rowId = $('#tblOrcamentoFluxoStatus').DataTable().column(1).search(searchValue, true, false).rows({ filter: 'applied' }).data();
        if (rowId.length > 0) { // ja tem
            if (selectedocf_id != rowId[0]["ocf_id"]) {
                $('#txtocf_descricao').css("background-color", corVermelho);
                swal({
                    type: 'error',
                    title: 'Aviso',
                    text: 'Status já cadocsrado'
                }).then(
                    function () {
                        return false;
                    });
            }
            else {
                $('#tblOrcamentoFluxoStatus').DataTable().search('').columns().search('').draw();
                return true;
            }
        }
        else { // nao tem
            $('#txtocf_descricao').css("background-color", corBranca);
            $('#tblOrcamentoFluxoStatus').DataTable().search('').columns().search('').draw();
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

    // ****************************GRID tblOrcamentoFluxoStatus *****************************************************************************
    $('#tblOrcamentoFluxoStatus').DataTable({
        "ajax": {
            "url": "/Orcamento/OrcamentoFluxoStatus_ListAll",
            "type": "GET",
            "datatype": "json"
        }
        , "columns": [
            { data: "ocf_id", "className": "hide_column", "searchable": true },
            { data: "ocs_id_de", "className": "hide_column", "searchable": true },

            { data: "ocs_id_para", "className": "hide_column", "searchable": true },
            { data: "ocs_de_descricao", "autoWidth": true, "searchable": false },
            { data: "ocf_id", "searchable": false,"sortable": false,
                "render": function (data, type, row) {
                    return '<span class="glyphicon glyphicon-arrow-right"  ></span>';
                }
            },
            { data: "ocs_para_descricao", "autoWidth": true, "searchable": false },
            { data: "ocf_descricao", "autoWidth": true, "searchable": false },
            {
                "title": "Opções",
                data: "ocf_id",
                "searchable": false,
                "sortable": false,
                "render": function (data, type, row) {
                    var retorno = "";
                    if (permissaoEscrita > 0) {
                        retorno = '<a href="#" onclick="return OrcamentoFluxoStatus_Editar(' + data + ')" title="Editar" ><span class="glyphicon glyphicon-pencil"></span></a>' + '  ';
                    }
                    else {
                        retorno = '<span class="glyphicon glyphicon-pencil desabilitado"  ></span>' + '  ';
                    }

                    retorno += '<span class="glyphicon glyphicon-ok text-success desabilitado" ></span>' + '  ';

                    if (permissaoExclusao > 0)
                        retorno += '<a href="#" onclick="return OrcamentoFluxoStatus_Excluir(' +  data + ')" title="Excluir" ><span class="glyphicon glyphicon-trash"></span></a>';
                    else
                        retorno += '<span class="glyphicon glyphicon-trash desabilitado" title="Excluir" ></span>';

                    return retorno;
                }
            }
        ]
        , "rowId": "ocf_id"
        , "order": [1, "asc"]
        , "rowCallback": function (row, data) {
            if (data.ocf_id == selectedocf_id)
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

    var tblOrcamentoFluxoStatus = $('#tblOrcamentoFluxoStatus').DataTable();

    $('#tblOrcamentoFluxoStatus tbody').on('click', 'tr', function () {
        if ($(this).hasClass('selected')) {
            $(this).removeClass('selected');
        }
        else {
            tblOrcamentoFluxoStatus.$('tr.selected').removeClass('selected');
            $(this).addClass('selected');
        }

        var OrcamentoFluxoStatus_id = tblOrcamentoFluxoStatus.row(this).data();
        $('#hddnSelectedocf_id').val(OrcamentoFluxoStatus_id["ocf_id"]);
        selectedocf_id = OrcamentoFluxoStatus_id["ocf_id"];

    });


});
