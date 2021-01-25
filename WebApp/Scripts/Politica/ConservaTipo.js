GetAll();
function ConservaTipo_Salvar() {
    var txtoct_codigo = $('#txtoct_codigo').val();

    var txtoct_descricao = $('#txtoct_descricao').val();

    var ativo = $('#chkoct_ativo').prop('checked') ? 1 : 0 
    $.ajax({
        url: "/PoliticaConserva/ConservaTipo_Salvar?oct_cod=" + txtoct_codigo + '&oct_descricao=' + txtoct_descricao + '&oct_ativo=' + ativo + '&oct_id=' + $('#txtOctId').val(),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            $("#ModalConservaTipo").modal('hide');
            $('#tblReparoTipos').DataTable().ajax.reload(null, false);  //false = sem reload na pagina.
            GetAll();
            return false;
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
            return false;
        }
    });
    return false;
}

function Inserir() {
    $("#ModalConservaTipo").modal('show');
}

function Fechar() {
    $("#ModalConservaTipo").modal('hide');
    $('#txtoct_codigo').html('');
    $('#txtoct_descricao').html('');
    $('#txtOctId').html('');
}

function EditShowModal(id) {

   


}

function GetATipoEdit(id) {
    $("#ModalConservaTipo").modal('show');
    $.ajax({
        url: '/Politicaconserva/GetEdtit?id=' + id,
        type: "Get",
        dataType: "JSON",
        success: function (data) {
            $('#txtoct_codigo').val(data.oct_codigo);
            $('#txtoct_descricao').val(data.oct_descricao);
            $('#txtOctId').val(data.oct_id);
        }
    });
}

function GetAll() {

    $.ajax({
        url: '/Politicaconserva/GetAllConservaTipo',
        type: "Get",
        dataType: "JSON",
        success: function (data) {
            console.log(data);
            $("#corpo").html("");

            $.each(data, function (i, valor) {
                $("#corpo").append($('<tr><td>' + valor.oct_id + '</td><td>' + valor.oct_codigo + '</td> <td>  <input type="checkbox" id="horns" name="horns"> </td><td>' + valor.oct_descricao + '</td><td style="text-align:center"><a href="#" onclick="return GetATipoEdit(' + valor.oct_id + ')" title="Editar"><span class="glyphicon glyphicon-pencil"></span></a></td></tr>'));

            });
        
        }
    });
}


//function ReparoTipo_Excluir(id) {
//    var form = this;
//    swal({
//        title: "Excluir. Tem certeza?",
//        icon: "warning",
//        buttons: [
//            'Não',
//            'Sim'
//        ],
//        dangerMode: true,
//        focusCancel: true
//    }).then(function (isConfirm) {
//        if (isConfirm) {
//            var response = POST("/Reparo/ReparoTipo_Excluir", JSON.stringify({ id: id }))
//            if (response.erroId >= 1) {
//                swal({
//                    type: 'success',
//                    title: 'Sucesso',
//                    text: 'Registro excluído com sucesso'
//                });

//                $('#tblReparoTipos').DataTable().ajax.reload();
//            }
//            else {
//                swal({
//                    type: 'error',
//                    title: 'Aviso',
//                    text: 'Erro ao excluir registro'
//                });
//            }
//            return true;
//        } else {
//            return false;
//        }
//    })



//    return false;
//}

//function ReparoTipo_AtivarDesativar(id, ativar) {

//    if (id >= 0) {
//        var form = this;
//        swal({
//            title: (ativar == 1 ? "Ativar" : "Desativar") + ". Tem certeza?",
//            icon: "warning",
//            buttons: [
//                'Não',
//                'Sim'
//            ],
//            dangerMode: true,
//            focusCancel: true
//        }).then(function (isConfirm) {
//            if (isConfirm) {
//                var response = POST("/Reparo/ReparoTipo_AtivarDesativar", JSON.stringify({ id: id }))
//                if (response.erroId == 1) {
//                    swal({
//                        type: 'success',
//                        title: 'Sucesso',
//                        text: ativar == 1 ? msgAtivacaoOK : msgDesativacaoOK
//                    });

//                    $('#tblReparoTipos').DataTable().ajax.reload();
//                }
//                else {
//                    swal({
//                        type: 'error',
//                        title: 'Aviso',
//                        text: ativar == 1 ? msgAtivacaoErro : msgDesativacaoErro
//                    });
//                }
//                return true;
//            } else {
//                return false;
//            }
//        })
//    }

//    return false;
//}

//function ReparoTipo_Editar(id) {
//    document.getElementById("lblModalHeader").innerText = "Editar Tipo de Reparo";

//    var corBranca = "rgb(255, 255, 255)";
//    $('#txtrpt_codigo').css("background-color", corBranca);
//    $('#txtrpt_descricao').css("background-color", corBranca);

//    $('#txtrpt_descricao').css('border-color', 'lightgrey');
//    $.ajax({
//        url: "/Reparo/ReparoTipo_GetbyID/" + id,
//        type: "GET",
//        contentType: "application/json;charset=UTF-8",
//        dataType: "json",
//        success: function (result) {
//            selectedrpt_id = id;
//            $('#txtrpt_codigo').val(result.rpt_codigo);
//            $('#txtrpt_descricao').val(result.rpt_descricao);
//            $('#chkrpt_ativo').prop('checked', (result.rpt_ativo == '1' ? true : false));

//            $("#modalSalvarRegistro").modal('show');
//        },
//        error: function (errormessage) {
//            alert(errormessage.responseText);
//        }
//    });
//    return false;
//}

//function ChecaRepetido(txtBox) {
//    txtBox.value = txtBox.value.trim();

//    if (validaAlfaNumerico(txtBox)) {
//        var corVermelho = "rgb(228, 88, 71)";
//        var corBranca = "rgb(255, 255, 255)";
//        var searchValue = '\\b' + txtBox.value + '\\b';
//        var rowId = $('#tblReparoTipos').DataTable().column(1).search(searchValue, true, false).rows({ filter: 'applied' }).data();
//        if (rowId.length > 0) { // ja tem
//            if (selectedrpt_id != rowId[0]["rpt_id"]) {
//                $('#txtrpt_descricao').css("background-color", corVermelho);
//                swal({
//                    type: 'error',
//                    title: 'Aviso',
//                    text: 'Tipo já cadastrado'
//                }).then(
//                    function () {
//                        return false;
//                    });
//            }
//            else {
//                $('#tblReparoTipos').DataTable().search('').columns().search('').draw();
//                return true;
//            }
//        }
//        else { // nao tem
//            $('#txtrpt_descricao').css("background-color", corBranca);
//            $('#tblReparoTipos').DataTable().search('').columns().search('').draw();
//            return true;
//        }
//    }
//    else {
//        $("#modalSalvarRegistro").modal('show');
//        return false;
//    }

//}


