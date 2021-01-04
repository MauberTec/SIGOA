
var selectedaca_id;

function AnomCausa_Inserir() {
    var corBranca = "rgb(255, 255, 255)";
    $('#txtaca_codigo').css("background-color", corBranca);
    $('#txtaca_descricao').css("background-color", corBranca);

    $('#txtaca_codigo').val("");
    $('#txtaca_descricao').val("");
    $('#chkaca_ativo').prop('checked', true);

    $('#txtAnomCausa').css('border-color', 'lightgrey');

    $('#chkaca_ativo').css('border-color', 'lightgrey');

    $("#modalSalvarRegistro").modal('show');
    document.getElementById("lblModalHeader").innerText = "Nova Causa de Anomalia";

    selectedaca_id = -1;
}

function AnomCausa_Salvar() {

    var cmbAnomLegenda = document.getElementById('cmbAnomLegenda');
    var txtcodigo = document.getElementById('txtaca_codigo');
    var txtdescricao = document.getElementById('txtaca_descricao');

    txtaca_codigo.value = txtaca_codigo.value.trim();
    txtaca_descricao.value = txtaca_descricao.value.trim();


    // checa campos vazios
    if (cmbAnomLegenda.selectedIndex <= 0) {
        cmbAnomLegenda.style.backgroundColor = corVermelho;
        swal({
            type: 'error',
            title: 'Aviso',
            text: 'Legenda é obrigatória'
        }).then(
            function () {
                return false;
            });
        return false;
    }
    else
        //if (txtcodigo.value.trim() == "") {
        //    txtcodigo.style.backgroundColor = corVermelho;
        //    swal({
        //        type: 'error',
        //        title: 'Aviso',
        //        text: 'O Código é obrigatório'
        //    }).then(
        //        function () {
        //            return false;
        //        });
        //    return false;
        //}
        //else
            if (txtdescricao.value.trim() == "") {
                txtdescricao.style.backgroundColor = corVermelho;
                swal({
                    type: 'error',
                    title: 'Aviso',
                    text: 'A Descrição é obrigatória'
                }).then(
                    function () {
                        return false;
                    });
                return false;
            }

    var selectedLeg_id = $('#cmbAnomLegenda').val();

    var cmbAnomLegenda = document.getElementById('cmbAnomLegenda');
    var leg_texto = cmbAnomLegenda.options[cmbAnomLegenda.selectedIndex].text;
    var selectedleg_codigo = leg_texto.substring(0, leg_texto.indexOf("-") - 1);

    var param = { aca_descricao: $('#txtaca_descricao').val(), leg_id: selectedLeg_id};

    $.ajax({
        url: "/Anomalia/AnomCausa_ListAll",
        data: param,
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var tem = 0
            if (result.data.length == 0) // nao tem repetido
            {
                param = {
                        aca_id: selectedaca_id,
                        leg_id: selectedLeg_id,
                    leg_codigo: selectedleg_codigo,
                        //aca_codigo: $('#txtaca_codigo').val(),
                        aca_descricao: $('#txtaca_descricao').val(),
                        aca_ativo: $('#chkaca_ativo').prop('checked') ? 1 : 0 
                    };
                $.ajax({
                    url: "/Anomalia/AnomCausa_Salvar",
                    data: JSON.stringify(param),
                    type: "POST",
                    contentType: "application/json;charset=utf-8",
                    dataType: "json",
                    success: function (result) {

                        if (parseInt(result) > 0) {
                            $('#tblAnomCausas').DataTable().ajax.reload(null, false);  //false = sem reload na pagina.

                            // espera 1 segundo para dar tempo do reload, nao funcionou colocando no "Null" do reload                            
                            setTimeout(function () { posicionaLinha('tblAtributos', result, 0); }, 1500);
                        }

                        $("#modalSalvarRegistro").modal('hide');

                        return false;
                    },
                    error: function (errormessage) {
                        alert(errormessage.responseText);
                        return false;
                    }
                });
            }
            else {
                swal({
                    type: 'error',
                    title: 'Aviso',
                    text: 'Causa já cadastrada'
                }).then(
                    function () {
                        return false;
                    });

                $("#modalSalvarRegistro").modal('show');
                return false;
            }
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
            return false;
        }
    });


    return false;
}

function AnomCausa_Excluir(id) {
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
            var response = POST("/Anomalia/AnomCausa_Excluir", JSON.stringify({ id: id }))
            if (response.erroId >= 1) {
                swal({
                    type: 'success',
                    title: 'Sucesso',
                    text: 'Registro excluído com sucesso'
                });

                $('#tblAnomCausas').DataTable().ajax.reload();
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

function AnomCausa_AtivarDesativar(id, ativar) {

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
                var response = POST("/Anomalia/AnomCausa_AtivarDesativar", JSON.stringify({ id: id }))
                if (response.erroId == 1) {
                    swal({
                        type: 'success',
                        title: 'Sucesso',
                        text: ativar == 1 ? msgAtivacaoOK : msgDesativacaoOK
                    });

                    $('#tblAnomCausas').DataTable().ajax.reload();
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

function AnomCausa_Editar(id) {
    document.getElementById("lblModalHeader").innerText = "Editar Causa de Anomalia";

    var corBranca = "rgb(255, 255, 255)";
    $('#txtaca_codigo').css("background-color", corBranca);
    $('#txtaca_descricao').css("background-color", corBranca);

    $('#txtaca_descricao').css('border-color', 'lightgrey');
    $.ajax({
        url: "/Anomalia/AnomCausa_GetbyID/" + id,
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            selectedaca_id = id;
            $('#txtaca_codigo').val(result.aca_codigo);
            $('#txtaca_descricao').val(result.aca_descricao);
            $('#chkaca_ativo').prop('checked', (result.aca_ativo == '1' ? true : false));

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
        var rowId = $('#tblAnomCausas').DataTable().column(1).search(searchValue, true, false).rows({ filter: 'applied' }).data();
        if (rowId.length > 0) { // ja tem
            if (selectedaca_id != rowId[0]["aca_id"]) {
                $('#txtaca_descricao').css("background-color", corVermelho);
                swal({
                    type: 'error',
                    title: 'Aviso',
                    text: 'Causa já cadastrada'
                }).then(
                    function () {
                        return false;
                    });
            }
            else {
                $('#tblAnomCausas').DataTable().search('').columns().search('').draw();
                return true;
            }
        }
        else { // nao tem
            $('#txtaca_descricao').css("background-color", corBranca);
            $('#tblAnomCausas').DataTable().search('').columns().search('').draw();
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

    // ****************************GRID tblAnomCausas *****************************************************************************
    $('#tblAnomCausas').DataTable({
        "ajax": {
            "url": "/Anomalia/AnomCausa_ListAll",
            "type": "GET",
            "datatype": "json"
        }
        , "columns": [
            { data: "aca_id", "width": "30px", "className": "hide_column", "searchable": false },
            { data: "leg_id", "width": "30px", "className": "hide_column", "searchable": false },
            { data: "leg_codigo", "width": "80px", "searchable": true },
            { data: "aca_codigo", "width": "80px", "searchable": true },
            { data: "aca_descricao", "autoWidth": true, "searchable": true },
            {
                "title": "Opções",
                data: "aca_id",
                "searchable": false,
                "sortable": false,
                "render": function (data, type, row) {
                    var retorno = "";
                    if (permissaoEscrita > 0) {
                        retorno = '<a href="#" onclick="return AnomCausa_Editar(' + data + ')" title="Editar" ><span class="glyphicon glyphicon-pencil"></span></a>' + '  ';

                        if (row.aca_ativo == 1)
                            retorno += '<a href="#" onclick="return AnomCausa_AtivarDesativar(' + data + ', 0)" title="Ativo" ><span class="glyphicon glyphicon-ok text-success"></span></a>' + '  ';
                        else
                            retorno += '<a href="#" onclick="return AnomCausa_AtivarDesativar(' + data + ', 1)" title="Desativado" ><span class="glyphicon glyphicon-remove text-danger"></span></a>' + '  ';
                    }
                    else {
                        retorno = '<span class="glyphicon glyphicon-pencil desabilitado"  ></span>' + '  ';

                        if (row.aca_ativo == 1)
                            retorno += '<span class="glyphicon glyphicon-ok text-success desabilitado" ></span>' + '  ';
                        else
                            retorno += '<span class="glyphicon glyphicon-remove text-danger desabilitado"  ></span>' + '  ';

                    }

                    if (permissaoExclusao > 0)
                        retorno += '<a href="#" onclick="return AnomCausa_Excluir(' + data + ')" title="Excluir" ><span class="glyphicon glyphicon-trash"></span></a>';
                    else
                        retorno += '<span class="glyphicon glyphicon-trash desabilitado" title="Excluir" ></span>';

                    return retorno;
                }
            }
        ]
        , "rowId": "aca_id"
        , "rowCallback": function (row, data) {
            if (data.aca_id == selectedaca_id)
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

    var tblAnomCausas = $('#tblAnomCausas').DataTable();

    $('#tblAnomCausas tbody').on('click', 'tr', function () {
        if ($(this).hasClass('selected')) {
            $(this).removeClass('selected');
        }
        else {
            tblAnomCausas.$('tr.selected').removeClass('selected');
            $(this).addClass('selected');
        }

        var AnomCausa_id = tblAnomCausas.row(this).data();
        $('#hddnSelectedaca_id').val(AnomCausa_id["aca_id"]);
        selectedaca_id = AnomCausa_id["aca_id"];

    });


});
