function showMessage(type, title, text) {
    swal({
        type: type,
        title: title,
        text: text
    })
}

function Logout() {
    swal({
        title: 'Saindo do sistema'
    });
    var response = POST("/Login/Logout")
    if (response.status == true) {
        swal.close();
        window.location.href = '/Login/Index';
    }
}
/// FUNÇÃO QUE PEGA URL DA PAGINA

$.urlParam = function (name) {
    var results = new RegExp('[\?&]' + name + '=([^&#]*)').exec(window.location.href);
    return results[1] || 0;
}

//FORMATA DA DATA DE /date(1515841251894)/ -> dd/MM/yyyy
function FormatDate(paramValue) {
    var date, day, month, year;

    date = new Date(parseInt(paramValue.substr(6)));

    day = date.getDate().toString().padStart(2, '0');
    month = (date.getMonth() + 1).toString().padStart(2, '0')
    year = date.getFullYear();

    return day + '/' + month + '/' + year;
}

function ModalTrocaSenha() {
    $("#txtSenhaAtual").val("");
    $("#txtSenhaNova").val("");
    $("#txtSenhaNovaConfirmar").val("");

    $("#modalTrocarSenhaAtual").modal('show');
}

function AlterarSenhaAtual() {
    event.preventDefault();

    var objSenha = {
        txtSenhaAtual: $("#txtSenhaAtual").val(),
        txtSenhaNova: $("#txtSenhaNova").val(),
        txtSenhaNovaConfirmar: $("#txtSenhaNovaConfirmar").val()
    };

    var txtSenha = document.getElementById("txtSenhaNova");
    var val_validaTxtSenha = validaTxtSenha(txtSenha);

    if (validarSenha()) {
        if ((objSenha.txtSenhaNova == objSenha.txtSenhaNovaConfirmar) && (val_validaTxtSenha)) {

            if ((objSenha.txtSenhaNova.trim().length > 0) && (objSenha.txtSenhaNova.trim().length < 5)) {
                swal({
                    type: 'error',
                    title: 'Aviso',
                    text: 'O comprimento da senha mínimo é 5 caracteres'
                }).then(
                    function () {
                        return false;
                    });
                return false;
            }

            var response = POST("/Login/Usuario_AlterarSenha", JSON.stringify({ txtSenhaAtual: objSenha.txtSenhaAtual, txtSenhaNova: objSenha.txtSenhaNova }))
            if (parseInt(response.erroId) == -10) {
                swal({
                    type: 'error',
                    title: 'Aviso',
                    text: 'Não é permitido alterar para a última senha'
                }).then(
                    function () {
                        return false;
                    });
            }
            else {
                // if (response.senha != false) {
                if (response.status == true) {
                    swal({
                        type: 'success',
                        title: 'Sucesso',
                        text: 'Senha alterada com sucesso!'
                    });
                    $("#modalTrocarSenhaAtual").modal('hide');
                    return true;
                }
                else {
                    swal({
                        type: 'error',
                        title: 'Aviso',
                        text: 'Erro ao alterar senha'
                    });
                    $("#modalTrocarSenhaAtual").modal('show');
                    return false;
                   
                }
                //if (response.retorno == false) {
                //    swal({
                //        type: 'error',
                //        title: 'Aviso',
                //        text: 'Senha atual não confere!'
                //    }).then(
                //        function () {
                //            return false;
                //        });
                //}
                // }
                //else {
                //    swal({
                //        type: 'error',
                //        title: 'Aviso',
                //        text: 'A nova senha não pode ser igual a anterior!'
                //    }).then(
                //        function () {
                //            return false;
                //        });
                //}

            }
        }
    }
}

function validarSenha() {
    var mensagem = "";
    var resultadoMensagem = "";

    var txtSenhaAtual = $("#txtSenhaAtual").val();
    var txtSenhaNova = $("#txtSenhaNova").val();
    var txtSenhaNovaConfirmar = $("#txtSenhaNovaConfirmar").val();

    if ((txtSenhaAtual.trim() == txtSenhaNova.trim()) && (txtSenhaNova.trim() == txtSenhaNovaConfirmar.trim())) {
        mensagem = 'O Campo "Senha Atual" está com o mesmo valor do campo "Nova Senha" ';
    }
    else
        if (txtSenhaNova.trim() != txtSenhaNovaConfirmar.trim()) {
            mensagem = 'Nova senha e Senha de confirmação estão diferentes!';
        }
        else
        {
        var response = POST("/Login/checaSenhaAtual", JSON.stringify({ "senhaAtual": txtSenhaAtual.trim() }))
            if (!response.status) {
                mensagem = 'A senha Atual não confere';
            }
        }

    if (mensagem != "") {
        swal({
            type: 'error',
            title: 'Aviso',
            text: mensagem
        }).then(
            function () {
                $("#modalTrocarSenhaAtual").modal('show');
                return false;
            });
        return false;
    }
    else
        return true;

}

// *******************************

function validaTxtSenha(txtSenha) {
    var corVermelho = "rgb(228, 88, 71)";
    var corBranca = "rgb(255, 255, 255)";


    var letters = /^[0-9A-Za-z\u002A\u0023\u0040\u0024\u0025\u005F\u002D]+$/;
    if ($.trim(txtSenha.value).match(letters) || ($.trim(txtSenha.value).length == 0)) {
        txtSenha.style.backgroundColor = corBranca;
        return true;
    }
    else {
        txtSenha.style.backgroundColor = corVermelho;

        swal({
            type: 'error',
            title: 'Aviso',
            text: 'São permitidos somente os caracteres Alfanuméricos e @ # $ % * _ - '
        }).then(
            function () {
                return false;
            });
    }



}

function validaTxt(txtBox, comPopup) {
    txtBox.value = $.trim(txtBox.value);

    var corVermelho = "rgb(228, 88, 71)";
    var corBranca = "rgb(255, 255, 255)";

    if (comPopup == null) comPopup = 1; // parametro opcional. Se nao passar assume zero

    if ($.trim(txtBox.value) == "") {
        txtBox.style.backgroundColor = corVermelho;
        if (comPopup == 1) {
            swal({
                type: 'error',
                title: 'Aviso',
                text: 'Valores vazios/brancos não são permitidos'
            }).then(
              function () {
                return false;
            });
        }
        return false;
    };

    var letters = /^[0-9A-Za-z\u002A\u0023\u0040\u0024\u0025\u005F\u002D]+$/;
    if ($.trim(txtBox.value).match(letters)) // || ($.trim(txtBox.value).length == 0)) {
    {
        txtBox.style.backgroundColor = corBranca;
    }
    else {
        txtBox.style.backgroundColor = corVermelho;
        if (comPopup == 1) {
            swal({
                type: 'error',
                title: 'Aviso',
                text: 'São permitidos somente os caracteres Alfanuméricos e @@ # $ % * _ - '
            }).then(
                function () {
                    return false;
                });
        }
        return false;
    }

    return true;
}

function validaAlfabetico(txtBox, comPopup, minLength, validarVazio) {
    var corVermelho = "rgb(228, 88, 71)";
    var corBranca = "rgb(255, 255, 255)";

    if (comPopup == null) comPopup = 1; // parametro opcional. Se nao passar assume 1
    if (validarVazio == null) validarVazio = 1; // parametro opcional. Se nao passar assume 1

    txtBox.value = $.trim(txtBox.value);
    if ((minLength != null) && (txtBox.value.length < minLength) && (txtBox.value.length > 0)) {
        txtBox.style.backgroundColor = corVermelho;
        if (comPopup == 1) {
            swal({
                type: 'error',
                title: 'Aviso',
                text: 'Comprimento mínimo é ' + minLength
            }).then(
                function () {
                    return false;
                });
        }
        $("#modalSalvarRegistro").modal('show');
        return false;
    }
    else {
        txtBox.style.backgroundColor = corBranca;
    }

    if ((validarVazio == 1) && ($.trim(txtBox.value) == "")) {
        txtBox.style.backgroundColor = corVermelho;

        if (comPopup == 1) {
            swal({
                type: 'error',
                title: 'Aviso',
                text: 'Valores vazios/brancos não são permitidos'
            }).then(
                function () {
                    $("#modalSalvarRegistro").modal('show');
                    return false;
                });
        }
        $("#modalSalvarRegistro").modal('show');
        return false;
    };


    var letters = /^[a-z\u00C0-\u00ff A-Z]+$/;
    if (txtBox.value.length >= minLength) {
        if ($.trim(txtBox.value).match(letters)) // || ($.trim(txtBox.value).length == 0)) {
        {
            txtBox.style.backgroundColor = corBranca;
        }
        else {
            txtBox.style.backgroundColor = corVermelho;
            if (comPopup == 1) {
                swal({
                    type: 'error',
                    title: 'Aviso',
                    text: 'São permitidos somente caracteres Alfabeticos'
                }).then(
                    function () {
                        $("#modalSalvarRegistro").modal('show');
                        return false;
                    });
            }
            return false;
        }
    }

    return true;
}

function validaAlfaNumerico(txtBox, comPopup, validarVazio) {

    if (comPopup == null) comPopup = 1; // parametro opcional. Se nao passar assume zero
    if (validarVazio == null) validarVazio = 1; // parametro opcional. Se nao passar assume 1

    var corVermelho = "rgb(228, 88, 71)";
    var corVerde = "rgb(61, 177, 75)";
    var corBranca = "rgb(255, 255, 255)";

    if ((validarVazio == 1) && ($.trim(txtBox.value) == "")) {
        txtBox.style.backgroundColor = corVermelho;

        if (comPopup == 1) {
            swal({
                type: 'error',
                title: 'Aviso',
                text: 'Valores vazios/brancos não são permitidos'
            }).then(
                function () {
                    return false;
                });
        }

        return false;
    };


    var letters = /^[a-zA-Z0-9 \u00C0-\u017F\u005F\u002D]*$/;
    if ($.trim(txtBox.value).match(letters)) // || ($.trim(txtBox.value).length == 0)) {
    {
        txtBox.style.backgroundColor = corBranca;
    }
    else {
        txtBox.style.backgroundColor = corVermelho;
        if (comPopup == 1) {
            swal({
                type: 'error',
                title: 'Aviso',
                text: 'São permitidos somente caracteres Alfanuméricos'
            }).then(
                function () {
                    return false;
                });
        }
        return false;
    }

    return true;
}

function validaAlfaNumericoSemAcentosNemEspaco(txtBox, comPopup, validarVazio) {

    if (comPopup == null) comPopup = 1; // parametro opcional. Se nao passar assume zero
    if (validarVazio == null) validarVazio = 1; // parametro opcional. Se nao passar assume 1

    var corVermelho = "rgb(228, 88, 71)";
    var corVerde = "rgb(61, 177, 75)";
    var corBranca = "rgb(255, 255, 255)";

    if ((validarVazio == 1) && ($.trim(txtBox.value) == "")) {
        txtBox.style.backgroundColor = corVermelho;

        if (comPopup == 1) {
            swal({
                type: 'error',
                title: 'Aviso',
                text: 'Valores vazios/brancos não são permitidos'
            }).then(
                function () {
                    return false;
                });
        }

        return false;
    };


    var letters = "^[a-zA-Z0-9_]*$";
    if ($.trim(txtBox.value).match(letters)) // || ($.trim(txtBox.value).length == 0)) {
    {
        txtBox.style.backgroundColor = corBranca;
    }
    else {
        txtBox.style.backgroundColor = corVermelho;
        if (comPopup == 1) {
            swal({
                type: 'error',
                title: 'Aviso',
                text: 'São permitidos somente caracteres Alfanuméricos. Espaços, acentos e caracteres especiais não são permitidos.'
            }).then(
                function () {
                    return false;
                });
        }
        return false;
    }

    return true;
}


function validaAlfaNumericoAcentosAfins(txtBox, comPopup, validarVazio) {

    if (comPopup == null) comPopup = 1; // parametro opcional. Se nao passar assume zero
    if (validarVazio == null) validarVazio = 1; // parametro opcional. Se nao passar assume 1

    var corVermelho = "rgb(228, 88, 71)";
    var corVerde = "rgb(61, 177, 75)";
    var corBranca = "rgb(255, 255, 255)";

    if ((validarVazio == 1) && ($.trim(txtBox.value) == "")) {
        txtBox.style.backgroundColor = corVermelho;

        if (comPopup == 1) {
            swal({
                type: 'error',
                title: 'Aviso',
                text: 'Valores vazios/brancos não são permitidos'
            }).then(
                function () {
                    return false;
                });
        }

        return false;
    };


    var letters = /^[A-Za-z0-9 \u002D\u005B\u005D@#$%&()_´`/ {}",.;:+|áàâãéèêíïóôõöúçñÁÀÂÃÉÈÍÏÓÔÕÖÚÇÑ'\s]+$/;
    if (letters.test($.trim(txtBox.value)))
    {
   // if ($.trim(txtBox.value).match(letters)) // || ($.trim(txtBox.value).length == 0)) {
   // {
        txtBox.style.backgroundColor = corBranca;
    }
    else
    if (($.trim(txtBox.value) == "") && (validarVazio == 0))
            txtBox.style.backgroundColor = corBranca;
    else
    {
        txtBox.style.backgroundColor = corVermelho;
        if (comPopup == 1) {
            swal({
                type: 'error',
                title: 'Aviso',
                text: 'São permitidos somente caracteres Alfanuméricos e Acentuados'
            }).then(
                function () {
                    return false;
                });
        }
        return false;
    }


    return true;
}

function validaNumero(txtBox, comPopup) {

    if (comPopup == null) comPopup = 1; // parametro opcional. Se nao passar assume zero

    var corVermelho = "rgb(228, 88, 71)";
    var corVerde = "rgb(61, 177, 75)";
    var corBranca = "rgb(255, 255, 255)";

    if ($.trim(txtBox.value) == "") {
        txtBox.style.backgroundColor = corVermelho;
        if (comPopup == 1) {
            swal({
                type: 'error',
                title: 'Aviso',
                text: 'Valores vazios/brancos não são permitidos'
            }).then(
                function () {
                    return false;
                });
        }

        return false;
    };


    if (!isNaN($.trim(txtBox.value)) )
    {
        txtBox.style.backgroundColor = corBranca;
    }
    else {
        txtBox.style.backgroundColor = corVermelho;
        if (comPopup == 1) {
            swal({
                type: 'error',
                title: 'Aviso',
                text: 'São permitidos somente caracteres Numéricos'
            }).then(
                function () {
                    return false;
                });
        }
        return false;
    }

    return true;
}

function checaVazio(txtBox, comPopup)
{
    var corVermelho = "rgb(228, 88, 71)";
    var corBranca = "rgb(255, 255, 255)";

    if (comPopup == null) comPopup = 1; // parametro opcional. Se nao passar assume zero

    if ($.trim(txtBox.value) == "") {
        txtBox.style.backgroundColor = corVermelho;
        if (comPopup == 1) {
            swal({
                type: 'error',
                title: 'Aviso',
                text: 'Valores vazios/brancos não são permitidos'
            }).then(
                function () {
                    return false;
                });
        }
        return false;
    }
    else {
        txtBox.style.backgroundColor = corBranca;
        return true;
    }
}

// *******************************
// traducao dos botoes de navegação dos datatables
var idioma = {
    "oPaginate": {
        "sNext": '<i class="fa fa-forward"></i>',
        "sPrevious": '<i class="fa fa-backward"></i>',
        "sFirst": '<i class="fa fa-step-backward"></i>',
        "sLast": '<i class="fa fa-step-forward"></i>'
    },
    "sEmptyTable": "Nenhum registro encontrado",
    "sInfo": "Registro(s) _START_-_END_",
    "sInfoEmpty": "0 registros",
    "sInfoFiltered": "(Filtrados de _MAX_ registros)",
    "sInfoPostFix": "",
    "sInfoThousands": ".",
    "sLengthMenu": "_MENU_ ",
    "sLoadingRecords": "Carregando...",
    "sProcessing": "Processando...",
    "sZeroRecords": "Nenhum registro encontrado",
    "sSearch": "Pesquisar",
    "oAria": {
        "sSortAscending": ": Ordenar colunas de forma ascendente",
        "sSortDescending": ": Ordenar colunas de forma descendente"
    },
    "select": {
        "rows": {
            "_": "Selecionado %d linhas",
            "0": "Nenhuma linha selecionada",
            "1": "Selecionado 1 linha"
        }
    }
};

var msgAtivacaoOK = "Registro Ativado com Sucesso";
var msgDesativacaoOK = "Registro Desativado com Sucesso";
var msgAtivacaoErro = "Erro ao Ativar registro";
var msgDesativacaoErro = "Erro ao Desativar registro";

var datepicker_regional = {
    closeText: 'Fechar',
    prevText: '&#x3c;Anterior',
    nextText: 'Pr&oacute;ximo&#x3e;',
    currentText: 'Hoje',
    monthNames: ['Janeiro', 'Fevereiro', 'Mar&ccedil;o', 'Abril', 'Maio', 'Junho',
        'Julho', 'Agosto', 'Setembro', 'Outubro', 'Novembro', 'Dezembro'],
    monthNamesShort: ['Jan', 'Fev', 'Mar', 'Abr', 'Mai', 'Jun',
        'Jul', 'Ago', 'Set', 'Out', 'Nov', 'Dez'],
    dayNames: ['Domingo', 'Segunda-feira', 'Ter&ccedil;a-feira', 'Quarta-feira', 'Quinta-feira', 'Sexta-feira', 'Sabado'],
    dayNamesShort: ['Dom', 'Seg', 'Ter', 'Qua', 'Qui', 'Sex', 'Sab'],
    dayNamesMin: ['Dom', 'Seg', 'Ter', 'Qua', 'Qui', 'Sex', 'Sab'],
    weekHeader: 'Sm',
    dateFormat: 'dd/mm/yy',
    firstDay: 0,
    isRTL: false,
    showMonthAfterYear: false,
    yearSuffix: ''
};

var corVermelho = "rgb(228, 88, 71)";
var corBranca = "rgb(255, 255, 255)";


