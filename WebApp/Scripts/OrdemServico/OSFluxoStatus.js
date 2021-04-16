
var selected_tos_id = -1;
var selectedfos_id;


function cmbStatusDe_onchange(quem) {

    var selectedVal = $("#cmbStatusDe").val(); 

    // filtra o grid para saber se há repeticoes

    var searchValue = '\\b' + selectedVal + '\\b';
    $('#tblOSFluxoStatus').DataTable().column(1).search(selectedVal).draw();

    var arr_encontrados = [];
    $('#tblOSFluxoStatus').DataTable().column(2, { search: 'applied' }).data().each(function (value, index) {
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
    $('#tblOSFluxoStatus').DataTable().column(1).search('').draw();
    $('#tblOSFluxoStatus').DataTable().column(2).search('').draw();

    var selectedDe = $("#cmbStatusDe option:selected").text();
    if (selectedDe != "")
        $("#txtfos_descricao").val(selectedDe + " para ");
    else
        $("#txtfos_descricao").val("");

}

function cmbStatusPara_onchange(quem) {

    var selectedDe = $("#cmbStatusDe option:selected").text();
    var selectedPara = $("#cmbStatusPara option:selected").text();

    if ((selectedDe != "") && (selectedPara != ""))
        $("#txtfos_descricao").val(selectedDe + " para " + selectedPara);
    else
        $("#txtfos_descricao").val("");
}

function OSFluxoStatus_Inserir() {
    var corBranca = "rgb(255, 255, 255)";
    $('#txtfos_descricao').css("background-color", corBranca);

    $('#txtfos_id').val("");

    $('#cmbStatusDe').val(null);
    $('#cmbStatusPara').val(null);

    $('#txtfos_descricao').val("");
    $('#chkfos_ativo').prop('checked', true);

    $('#btnUpdate').hide();
    $('#btnAdd').show();
    $('#txtOSFluxoStatus').css('border-color', 'lightgrey');
    $('#chkfos_ativo').css('border-color', 'lightgrey');

    $("#modalSalvarRegistro").modal('show');
    document.getElementById("lblModalHeader").innerText = "Novo Fluxo de Status de Ordem de Serviço";

    selectedfos_id = -1;
}

function OSFluxoStatus_Salvar() {

    var txtfos_descricao = document.getElementById('txtfos_descricao');
    txtfos_descricao.value = txtfos_descricao.value.trim();

    if (validaAlfaNumericoAcentosAfins(txtfos_descricao)) {

        var OSFluxoStatus = {
            fos_id: $('#txtfos_id').val(),
            tos_id:selected_tos_id,
            sos_id_de: $('#cmbStatusDe').val(),
            sos_id_para: $('#cmbStatusPara').val(),
            fos_descricao: $('#txtfos_descricao').val(),
            fos_ativo: $('#chkfos_ativo').prop('checked') ? 1 : 0 
        };

        $.ajax({
            url: "/OrdemServico/OSFluxoStatus_Salvar",
            data: JSON.stringify(OSFluxoStatus),
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                $("#modalSalvarRegistro").modal('hide');
                $('#tblOSFluxoStatus').DataTable().ajax.reload(null, false);  //false = sem reload na pagina.

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

function OSFluxoStatus_Excluir(id) {

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
            var response = POST("/OrdemServico/OSFluxoStatus_Excluir", JSON.stringify({ id: id }))
            if (response.erroId >= 1) {
                swal({
                    type: 'success',
                    title: 'Sucesso',
                    text: 'Registro excluído com sucesso'
                });

                $('#tblOSFluxoStatus').DataTable().ajax.reload();
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

function OSFluxoStatus_AtivarDesativar(id, ativar) {

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
                var response = POST("/OrdemServico/OSFluxoStatus_AtivarDesativar", JSON.stringify({ id: id }))
                if (response.erroId == 1) {
                    swal({
                        type: 'success',
                        title: 'Sucesso',
                        text: ativar == 1 ? msgAtivacaoOK : msgDesativacaoOK
                    });

                    $('#tblOSFluxoStatus').DataTable().ajax.reload();
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

function OSFluxoStatus_Editar(id) {
    document.getElementById("lblModalHeader").innerText = "Editar Fluxo de Status de Ordem de Serviço";

    var corBranca = "rgb(255, 255, 255)";
    $('#txtfos_descricao').css("background-color", corBranca);
    $('#txtfos_descricao').css('border-color', 'lightgrey');

    $.ajax({
        url: "/OrdemServico/OSFluxoStatus_GetbyID/" + id,
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            $('#txtfos_id').val(result.fos_id);

            $('#cmbStatusDe').val(result.sos_id_de);

            $('#txtfos_descricao').val(result.fos_descricao);
            $('#chkfos_ativo').prop('checked', (result.fos_ativo == '1' ? true : false));

            cmbStatusDe_onchange(result.sos_id_para);
            $('#cmbStatusPara').val(result.sos_id_para);

             $("#txtfos_descricao").val(result.fos_descricao);


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
        var rowId = $('#tblOSFluxoStatus').DataTable().column(1).search(searchValue, true, false).rows({ filter: 'applied' }).data();
        if (rowId.length > 0) { // ja tem
            if (selectedfos_id != rowId[0]["fos_id"]) {
                $('#txtfos_descricao').css("background-color", corVermelho);
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
                $('#tblOSFluxoStatus').DataTable().search('').columns().search('').draw();
                return true;
            }
        }
        else { // nao tem
            $('#txtfos_descricao').css("background-color", corBranca);
            $('#tblOSFluxoStatus').DataTable().search('').columns().search('').draw();
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

    // ****************************GRID tblOSTipos *****************************************************************************
    $('#tblOSTipos').DataTable({
        "ajax": {
            "url": "/OrdemServico/OSTipo_ListAll",
            "type": "GET",
            "datatype": "json"
        }
        , "columns": [
            { data: "tos_id", "className": "hide_column", "searchable": false },
            { data: "tos_codigo", "width": "100px", "searchable": true },
            { data: "tos_descricao", "autoWidth": true, "searchable": true },
            {
                "title": "Opções",
                data: "tos_id",
                "searchable": false,
                "sortable": false,
                "render": function (data, type, row) {
                    var retorno = "";
                    if (permissaoEscrita > 0) {
                        retorno = '<a href="#" onclick="return OSTipo_Editar(' + data + ')" title="Editar" ><span class="glyphicon glyphicon-pencil"></span></a>' + '  ';

                        if (row.tos_ativo == 1)
                            retorno += '<a href="#" onclick="return OSTipo_AtivarDesativar(' + data + ', 0)" title="Ativo" ><span class="glyphicon glyphicon-ok text-success"></span></a>' + '  ';
                        else
                            retorno += '<a href="#" onclick="return OSTipo_AtivarDesativar(' + data + ', 1)" title="Desativado" ><span class="glyphicon glyphicon-remove text-danger"></span></a>' + '  ';
                    }
                    else {
                        retorno = '<span class="glyphicon glyphicon-pencil desabilitado"  ></span>' + '  ';

                        if (row.tos_ativo == 1)
                            retorno += '<span class="glyphicon glyphicon-ok text-success desabilitado" ></span>' + '  ';
                        else
                            retorno += '<span class="glyphicon glyphicon-remove text-danger desabilitado"  ></span>' + '  ';

                    }

                    if (permissaoExclusao > 0)
                        retorno += '<a href="#" onclick="return OSTipo_Excluir(' + data + ')" title="Excluir" ><span class="glyphicon glyphicon-trash"></span></a>';
                    else
                        retorno += '<span class="glyphicon glyphicon-trash desabilitado" title="Excluir" ></span>';

                    return retorno;
                }
            }
        ]
        , "rowId": "tos_id"
        , "order": [1, "asc"]
        , "rowCallback": function (row, data) {
            if (data.tos_id == selected_tos_id)
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

    var tblOSTipos = $('#tblOSTipos').DataTable();

    $('#tblOSTipos tbody').on('click', 'tr', function () {
        if ($(this).hasClass('selected')) {
            $(this).removeClass('selected');
         document.getElementById('subGrids').style.visibility = "hidden";
        }
        else {
            tblOSTipos.$('tr.selected').removeClass('selected');
            $(this).addClass('selected');
            document.getElementById('subGrids').style.visibility = "visible";
        }

        var OSTipo_id = tblOSTipos.row(this).data();
        selected_tos_id = OSTipo_id["tos_id"];

        var textoHeader = "Fluxo de Status O.S. de " + OSTipo_id["tos_descricao"] + " (" + OSTipo_id["tos_codigo"] + ") ";
        document.getElementById('HeaderFluxoOS').innerText = textoHeader;

        $('#lblModalHeaderTIPO_OS').text(textoHeader);
        $('#tblOSFluxoStatus').DataTable().ajax.reload(null, false);

    });


    // ****************************GRID tblOSFluxoStatus *****************************************************************************

    $('#tblOSFluxoStatus').DataTable({
        "ajax": {
            "url": "/OrdemServico/OSFluxoStatus_ListAll",
            "type": "GET",
            "data": function (d) {
                       d.tos_id = selected_tos_id;
                    }, 
            "datatype": "json"
        }
        , "columns": [
            { data: "fos_id", "className": "hide_column", "searchable": true },
            { data: "sos_id_de", "className": "hide_column", "searchable": true },
            { data: "sos_id_para", "className": "hide_column", "searchable": true },
            { data: "sos_de_descricao", "autoWidth": true, "searchable": false },
            {
                data: "fos_id", "searchable": false, "sortable": false,
                "render": function (data, type, row) {
                    return '<span class="glyphicon glyphicon-arrow-right"  ></span>';
                }
            },
            { data: "sos_para_descricao", "autoWidth": true, "searchable": false },
            { data: "fos_descricao", "autoWidth": true, "searchable": false },
            {
                "title": "Opções",
                data: "fos_id",
                "searchable": false,
                "sortable": false,
                "render": function (data, type, row) {
                    var retorno = "";
                    if (permissaoEscrita > 0) {
                        retorno = '<a href="#" onclick="return OSFluxoStatus_Editar(' + data + ')" title="Editar" ><span class="glyphicon glyphicon-pencil"></span></a>' + '  ';
                    }
                    else {
                        retorno = '<span class="glyphicon glyphicon-pencil desabilitado"  ></span>' + '  ';
                    }

                    retorno += '<span class="glyphicon glyphicon-ok text-success desabilitado" ></span>' + '  ';

                    if (permissaoExclusao > 0)
                        retorno += '<a href="#" onclick="return OSFluxoStatus_Excluir(' +  data + ')" title="Excluir" ><span class="glyphicon glyphicon-trash"></span></a>';
                    else
                        retorno += '<span class="glyphicon glyphicon-trash desabilitado" title="Excluir" ></span>';

                    return retorno;
                }
            }
        ]
        , "rowId": "fos_id"
        , "order": [1, "asc"]
        , "rowCallback": function (row, data) {
            if (data.fos_id == selectedfos_id)
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

    var tblOSFluxoStatus = $('#tblOSFluxoStatus').DataTable();
    $('#tblOSFluxoStatus tbody').on('click', 'tr', function () {
        if ($(this).hasClass('selected')) {
            $(this).removeClass('selected');
        }
        else {
            tblOSFluxoStatus.$('tr.selected').removeClass('selected');
            $(this).addClass('selected');
        }

        var OSFluxoStatus_id = tblOSFluxoStatus.row(this).data();
        $('#hddnSelectedfos_id').val(OSFluxoStatus_id["fos_id"]);
        selectedfos_id = OSFluxoStatus_id["fos_id"];

    });


});
