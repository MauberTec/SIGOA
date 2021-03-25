
//Função para Login -- Validando os campos login e senha preenchidos
function btnLogin_onclick()
{
    var result = validarCamposEditar();
    if (result == false) {
        return false;
    }
    var objetoLogin = {
        usu_usuario: $("#usu_usuario").val(),
        usu_senha: $("#usu_senha").val(),
    };

    $.ajax({
        url: "/Login/ValidarUsuario",
        data: JSON.stringify(objetoLogin),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.status == true) {
                window.location.href = "/Objeto/ObjPriorizacao/160";
            }
            else
                if (result.erroId == -1) {
                    swal({
                        position: 'top',
                        type: 'error',
                        text: 'Usuário ou senha incorretos',
                        title: 'Aviso'
                    });
                }
                else
                    if (result.erroId == -20)
                    {
                        swal({
                            position: 'top',
                            type: 'error',
                            text: 'Usuário desativado',
                            title: 'Aviso'
                        });
                    }
            return false;
        },
        error: function (errormessage) {
           // alert(errormessage.responseText);
            {
                swal({
                    type: 'error',
                    title: 'Erro',
                    text: errormessage.responseText
                });
            }

        }
    });

    return false;
}

//function Login() {
//    event.preventDefault();
//    var result = validarCamposEditar();
//    if (result == false) {
//        return false;
//    }
//    var objetoLogin = {
//        usu_usuario: $("#usu_usuario").val(),
//        usu_senha: $("#usu_senha").val(),
//    };

//    $.ajax({
//        url: "/Login/ValidarUsuario",
//        data: JSON.stringify(objetoLogin),
//        type: "POST",
//        contentType: "application/json;charset=utf-8",
//        dataType: "json",
//        success: function (result) {
//            if (result.status == true) {
//                window.location.href = "/Home/Index";
//            }
//            else
//                if (result.erroId == -1) {
//                    swal({
//                        position: 'top',
//                        type: 'error',
//                        text: 'Usuário ou senha incorretos',
//                        title: 'Aviso'
//                    });
//                }
//                else
//                    if (result.erroId == -20)
//                    {
//                        swal({
//                            position: 'top',
//                            type: 'error',
//                            text: 'Usuário desativado',
//                            title: 'Aviso'
//                        });
//                    }
//        },
//        error: function (errormessage) {
//           // alert(errormessage.responseText);
//            {
//                swal({
//                    type: 'error',
//                    title: 'Erro',
//                    text: errormessage.responseText
//                });
//            }

//        }
//    });
//}

//Validar campos 
function validarCamposEditar() {
    var mensagem = "";

    var usu_usuario = $("#usu_usuario").val();
    var usu_senha = $("#usu_senha").val();

    if (usu_usuario.trim() == "") {
        mensagem += 'Login é obrigatório' + '<br/>';
    }
    if (usu_senha.trim() == "") {
        mensagem += 'Senha é obrigatória' + '<br/>';
    }

    if (mensagem != "") {
        if (usu_usuario === "") {
            $("#usu_usuario").focus();
        } else if (usu_senha === "") {
            $("#usu_senha").focus();
        }
        swal({
            position: 'top',
            type: 'error',
            title: '<small>' + 'Por favor verificar o(s) campo(s).' + '</small>',
            html: mensagem
        }).then(
            function () {
                return false;
            });
        return false;
    }
    return true;
}

//Validar Senha
function validarSenha() {
    var mensagem = "";

    var txtSenhaNova = $("#txtSenhaNova").val();
    var txtSenhaNovaConfirmar = $("#txtSenhaNovaConfirmar").val();

    if (txtSenhaNova.trim() == "") {
        mensagem += 'Nova Senha' + '<br/>';
    }
    if (txtSenhaNovaConfirmar.trim() == "") {
        mensagem += 'Confirmação de Nova Senha' + '<br/>';
    }

    if (mensagem != "") {
        if (txtSenhaNova === "") {
            $("#txtSenhaNova").focus();
        } else if (txtSenhaNovaConfirmar === "") {
            $("#txtSenhaNovaConfirmar").focus();
        }
        swal({
            type: 'error',
            title: 'Por favor verificar o(s) campo(s).',
            html: mensagem
        }).then(
            function () {
                return false;
            });
        return false;
    }
    return true;
}

//abre a tela modal de esqueci a Senha
function EsqueciSenha() {
    $("#modalEsqueciSenha").modal('show');
    return false;
}

// cria e envia email com senha nova
function bntEnviar_click() {

    var txtEsqueci_login = $("#txtEsqueci_login").val();

    if (txtEsqueci_login.trim() == "") {
        swal({
            position: 'top',
            type: 'error',
            text: 'Preencher o campo Usuário',
            title: 'Aviso'
        });
        return false;
    }
    else {
        var response = POST("/Login/bntEnviar_click", JSON.stringify({ login: txtEsqueci_login }))
        if (response.status) {
            swal({
                type: 'success',
                title: 'Sucesso',
                text: 'Email enviado com Sucesso'
            });

            $("#modalEsqueciSenha").modal('hide');
            return false;
        }
        else {
            swal({
                type: 'error',
                title: 'Aviso',
                text: 'Erro ao enviar email'
            });

            $("#modalEsqueciSenha").modal('hide');
            return false;
        }
    }
}

