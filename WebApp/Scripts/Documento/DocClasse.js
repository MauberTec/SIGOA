var selecteddcl_id;
var corVermelho = "rgb(228, 88, 71)";
var corBranca = "rgb(255, 255, 255)";

function DocClasse_Inserir() {
    var corBranca = "rgb(255, 255, 255)";
    $('#txtdcl_codigo').css("background-color", corBranca);
    $('#txtdcl_descricao').css("background-color", corBranca);

    $('#txtdcl_codigo').val("");
    $('#txtdcl_descricao').val("");
    $('#chkdcl_ativo').prop('checked', true);

    $('#txtDocClasse').css('border-color', 'lightgrey');
    $('#chkdcl_ativo').css('border-color', 'lightgrey');

    $("#modalSalvarRegistro").modal('show');
    document.getElementById("lblModalHeader").innerText = "Nova Classe de Projeto";

    selecteddcl_id = -1;
}

function DocClasse_Salvar() {
    var txtdcl_codigo = document.getElementById('txtdcl_codigo');
    txtdcl_codigo.value = txtdcl_codigo.value.trim();

    var txtdcl_descricao = document.getElementById('txtdcl_descricao');
    txtdcl_descricao.value = txtdcl_descricao.value.trim();

    if (DocClasse_ChecaRepetido(txtdcl_codigo))
        if (txtdcl_descricao.value != "") {
            if (validaAlfaNumerico(txtdcl_descricao)) {

                var DocClasse = {
                    dcl_id: selecteddcl_id,
                    dcl_codigo: $('#txtdcl_codigo').val(),
                    dcl_descricao: $('#txtdcl_descricao').val(),
                    dcl_ativo: $('#chkdcl_ativo').prop('checked') ? 1 : 0
                };

                $.ajax({
                    url: "/Documento/DocClasse_Salvar",
                    data: JSON.stringify(DocClasse),
                    type: "POST",
                    contentType: "application/json;charset=utf-8",
                    dataType: "json",
                    success: function (result) {
                        $("#modalSalvarRegistro").modal('hide');
                        $('#tblDocClasses').DataTable().ajax.reload(null, false);  //false = sem reload na pagina.

                        return false;
                    },
                    error: function (errormessage) {
                        alert(errormessage.responseText);
                        return false;
                    }
                });
            }
        }
        else {
            swal({
                type: 'error',
                title: 'Aviso',
                text: 'Descrição está vazia'
            }).then(
                function () {
                    txtdcl_descricao.style.backgroundColor = corVermelho;
                    return false;
                });

            txtdcl_descricao.style.backgroundColor = corVermelho;
            return false;

            $("#modalSalvarRegistro").modal('show');
            return false;
        }
    else {
        $("#modalSalvarRegistro").modal('show');
        return false;
    }
    return false;
}

function DocClasse_Excluir(id) {
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
            var response = POST("/Documento/DocClasse_Excluir", JSON.stringify({ id: id }))
            if (response.erroId >= 1) {
                swal({
                    type: 'success',
                    title: 'Sucesso',
                    text: 'Registro excluído com sucesso'
                });

                $('#tblDocClasses').DataTable().ajax.reload();
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

function DocClasse_AtivarDesativar(id, ativar) {

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
                var response = POST("/Documento/DocClasse_AtivarDesativar", JSON.stringify({ id: id }))
                if (response.erroId == 1) {
                    swal({
                        type: 'success',
                        title: 'Sucesso',
                        text: ativar == 1 ? msgAtivacaoOK : msgDesativacaoOK
                    });

                    $('#tblDocClasses').DataTable().ajax.reload();
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

function DocClasse_Editar(id) {
    document.getElementById("lblModalHeader").innerText = "Editar Classe de Projeto";

    var corBranca = "rgb(255, 255, 255)";
    $('#txtdcl_codigo').css("background-color", corBranca);
    $('#txtdcl_descricao').css("background-color", corBranca);

    $('#txtdcl_descricao').css('border-color', 'lightgrey');
    $.ajax({
        url: "/Documento/DocClasse_GetbyID/" + id,
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            selecteddcl_id = id;
            $('#txtdcl_codigo').val(result.dcl_codigo);
            $('#txtdcl_descricao').val(result.dcl_descricao);
            $('#chkdcl_ativo').prop('checked', (result.dcl_ativo == '1' ? true : false));

            $("#modalSalvarRegistro").modal('show');
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
    return false;
}

function DocClasse_ChecaRepetido(txtBox) {
    txtBox.value = txtBox.value.trim();

    if ((txtBox.value.length < 3) || (txtBox.value == ""))
    {
        swal({
            type: 'error',
            title: 'Aviso',
            text: 'Código deve ser Letra + Número + Número'
        }).then(
            function () {
                 txtBox.style.backgroundColor = corVermelho;
                return false;
            });

         txtBox.style.backgroundColor = corVermelho;
        return false;
    }

    if (validaAlfaNumerico(txtBox)) {
        var searchValue = '\\b' + txtBox.value + '\\b';
        var rowId = $('#tblDocClasses').DataTable().column(1).search(searchValue, true, false).rows({ filter: 'applied' }).data();
        if (rowId.length > 0) { // ja tem
            if (selecteddcl_id != rowId[0]["dcl_id"]) {
                $('#txtdcl_codigo').css("background-color", corVermelho);
                swal({
                    type: 'error',
                    title: 'Aviso',
                    text: 'Código já cadastrado'
                }).then(
                    function () {
                        return false;
                    });
            }
            else {
                $('#tblDocClasses').DataTable().search('').columns().search('').draw();
                return true;
            }
        }
        else { // nao tem
            $('#txtdcl_codigo').css("background-color", corBranca);
            $('#tblDocClasses').DataTable().search('').columns().search('').draw();
            return true;
        }
    }
    else {
        $("#modalSalvarRegistro").modal('show');
        return false;
    }

}

function txtdcl_codigo_onKeyUP(txt) {

    txt.style.backgroundColor = corBranca;

    txt.value = txt.value.toUpperCase();
    
    validaAlfaNumerico(txt, 0);

}

function txtdcl_descricao_onKeyUP(txt) {
    txt.style.backgroundColor = corBranca;
    validaAlfaNumerico(txt, 0);

    if (txt.value == "")
        txt.style.backgroundColor = corBranca;
}


// montagem do gridview
$(document).ready(function () {

    // ****************************GRID tblDocClasses *****************************************************************************
    $('#tblDocClasses').DataTable({
        "ajax": {
            "url": "/Documento/DocClasse_ListAll",
            "type": "GET",
            "datatype": "json"
        }
        , "columns": [
            { data: "dcl_id", "width": "30px", "className": "hide_column", "searchable": false },
            { data: "dcl_codigo", "width": "80px", "searchable": true },
            { data: "dcl_descricao", "autoWidth": true, "searchable": true },
            {
                "title": "Opções",
                data: "dcl_id",
                "searchable": false,
                "sortable": false,
                "render": function (data, type, row) {
                    var retorno = "";
                    if (permissaoEscrita > 0) {
                        retorno = '<a href="#" onclick="return DocClasse_Editar(' + data + ')" title="Editar" ><span class="glyphicon glyphicon-pencil"></span></a>' + '  ';

                        if (row.dcl_ativo == 1)
                            retorno += '<a href="#" onclick="return DocClasse_AtivarDesativar(' + data + ', 0)" title="Ativo" ><span class="glyphicon glyphicon-ok text-success"></span></a>' + '  ';
                        else
                            retorno += '<a href="#" onclick="return DocClasse_AtivarDesativar(' + data + ', 1)" title="Desativado" ><span class="glyphicon glyphicon-remove text-danger"></span></a>' + '  ';
                    }
                    else {
                        retorno = '<span class="glyphicon glyphicon-pencil desabilitado"  ></span>' + '  ';

                        if (row.dcl_ativo == 1)
                            retorno += '<span class="glyphicon glyphicon-ok text-success desabilitado" ></span>' + '  ';
                        else
                            retorno += '<span class="glyphicon glyphicon-remove text-danger desabilitado"  ></span>' + '  ';

                    }

                    if (permissaoExclusao > 0)
                        retorno += '<a href="#" onclick="return DocClasse_Excluir(' + data + ')" title="Excluir" ><span class="glyphicon glyphicon-trash"></span></a>';
                    else
                        retorno += '<span class="glyphicon glyphicon-trash desabilitado" title="Excluir" ></span>';

                    return retorno;
                }
            }
        ]
        , "order": [1, "asc"]
        , "rowId": "dcl_id"
        , "rowCallback": function (row, data) {
            if (data.dcl_id == selecteddcl_id)
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

    var tblDocClasses = $('#tblDocClasses').DataTable();

    $('#tblDocClasses tbody').on('click', 'tr', function () {
        if ($(this).hasClass('selected')) {
            $(this).removeClass('selected');
        }
        else {
            tblDocClasses.$('tr.selected').removeClass('selected');
            $(this).addClass('selected');
        }

        var DocClasse_id = tblDocClasses.row(this).data();
        $('#hddnSelecteddcl_id').val(DocClasse_id["dcl_id"]);
        selecteddcl_id = DocClasse_id["dcl_id"];

    });


    jQuery("#txtdcl_codigo").mask("S00", options);
  //  $('#txtdcl_codigo').attr('placeholder', "X00");


});
