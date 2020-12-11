
var selectedId;

function Usuarios_Inserir() {
    var corBranca = "rgb(255, 255, 255)";
    $('#txtusu_id').css("background-color", corBranca);
    $('#txtusu_nome').css("background-color", corBranca);
    $('#txtusu_usuario').css("background-color", corBranca);
    $('#txtusu_email').css("background-color", corBranca);

    $('#txtusu_id').val("");
    $('#txtusu_nome').val("");
    $('#txtusu_usuario').val("");
    $('#txtusu_email').val("");
    $('#chkusu_ativo').prop('checked', true);

    $('#btnUpdate').hide();
    $('#btnAdd').show();
    $('#chkusu_ativo').css('border-color', 'lightgrey');

    $("#modalSalvarRegistro").modal('show');

    document.getElementById("btnTrocarUsu_img").style.visibility = "hidden";
    document.getElementById("btnTrocarUsu_img").disabled = true; 

    document.getElementById("lblModalHeader").innerText = "Novo usuário";

    selectedId = -1;
}

function Usuario_Salvar() {
    var txtusu_usuario = document.getElementById('txtusu_usuario');
    var txtusu_nome = document.getElementById('txtusu_nome');
    var txtusu_email = document.getElementById('txtusu_email');

    txtusu_usuario.value = txtusu_usuario.value.trim();
    txtusu_nome.value = txtusu_nome.value.trim();
    txtusu_email.value = txtusu_email.value.trim();

    document.body.style.cursor = 'wait';

    if (ChecaUsername(txtusu_usuario, 1, 3, 1) && validaAlfabetico(txtusu_nome, 1, 3, 1) && validaEmail(txtusu_email, 1)) {
        var usuario = {
            usu_id: $('#txtusu_id').val(),
            usu_nome: $('#txtusu_nome').val(),
            usu_usuario: $('#txtusu_usuario').val(),
            usu_email: $('#txtusu_email').val(),
            usu_ativo: $('#chkusu_ativo').prop('checked') ? 1 : 0,
            usu_foto: ($('#txtusu_foto').val() == null ? "" : $('#txtusu_foto').val())
        };

        $.ajax({
            url: "/Usuario/Usuario_Salvar",
            data: JSON.stringify(usuario),
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.status == true) {
                        $('#modalSalvarRegistro').modal('hide');
                } else {
                    document.body.style.cursor = 'default';
                    swal({
                        type: 'error',
                        title: 'Aviso',
                        text: result.erroId
                    });
                }   

                $('#tblUsuarios').DataTable().ajax.reload();  //false if you don't want to refresh paging else true.

                var textoHeaderPerfisdoUsuario = "Perfis do Usuário: " + $('#txtusu_nome').val();
                document.getElementById('HeaderPerfisdoUsuario').innerText = textoHeaderPerfisdoUsuario;

                var textoHeaderGruposdoUsuario = "Grupos do Usuário: " + $('#txtusu_nome').val();
                document.getElementById('HeaderGruposdoUsuario').innerText = textoHeaderGruposdoUsuario;

                document.body.style.cursor = 'default';
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

function Usuarios_Deletar(id) {
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
            var response = POST("/Usuario/Usuario_Excluir", JSON.stringify({ id: id }))
            if (response.erroId >= 1) {
                swal({
                    type: 'success',
                    title: 'Sucesso',
                    text: 'Registro excluído com sucesso'
                });

                $('#tblUsuarios').DataTable().ajax.reload();
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

function Usuario_AtivarDesativar(id, ativar) {

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
                var response = POST("/Usuario/Usuario_AtivarDesativar", JSON.stringify({ id: id }))
                if (response.erroId == 1) {
                    swal({
                        type: 'success',
                        title: 'Sucesso',
                        text: ativar == 1 ? msgAtivacaoOK : msgDesativacaoOK
                    });

                    $('#tblUsuarios').DataTable().ajax.reload();
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

function Usuarios_Editar(id) {
    var corBranca = "rgb(255, 255, 255)";

    $('#txtusu_nome').css('border-color', 'lightgrey');

    $('#txtusu_id').css("background-color", corBranca);
    $('#txtusu_nome').css("background-color", corBranca);
    $('#txtusu_usuario').css("background-color", corBranca);
    $('#txtusu_email').css("background-color", corBranca);


    document.getElementById("lblModalHeader").innerText = "Editar usuário";

    $.ajax({
        url: "/Usuario/Usuario_GetbyID/" + id,
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            $('#txtusu_id').val(result.usu_id);
            usu_id_sel = result.usu_id;

            $('#txtusu_nome').val(result.usu_nome);
            $('#txtusu_usuario').val(result.usu_usuario);
            $('#txtusu_email').val(result.usu_email);
            $('#chkusu_ativo').prop('checked', (result.usu_ativo == '1' ? true : false));
            $('#txtusu_foto').val(result.usu_foto);

            document.getElementById("btnTrocarUsu_img").style.visibility = "visible";
            document.getElementById("btnTrocarUsu_img").disabled = false; 

            $("#modalSalvarRegistro").modal('show');
            $('#btnUpdate').show();
            $('#btnAdd').hide();
            try {
                var src = result.usu_foto;
                $("#imgUsuario").attr("src", src);
            }
            catch (ex) {
                $("#imgUsuario").attr("src", "/images/default.png");
            }

        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
    return false;
}

function ChecaUsername(txtBox, comPopup, minLength, validarVazio) {
    txtBox.value = txtBox.value.trim();

    if (comPopup == null) comPopup = 1; // parametro opcional. Se nao passar assume zero
    if (validarVazio == null) validarVazio = 1; // parametro opcional. Se nao passar assume 1
    if (minLength == null) minLength = 3;

    var corVermelho = "rgb(228, 88, 71)";
    var corBranca = "rgb(255, 255, 255)";

    if ((validarVazio == 1) && ($.trim(txtBox.value) == "")) {
        txtBox.style.backgroundColor = corVermelho;
        swal({
            type: 'error',
            title: 'Aviso',
            text: 'Valores vazios/brancos não são permitidos'
        }).then(
            function () {
                $("#modalSalvarRegistro").modal('show');
                return false;
            });

        $("#modalSalvarRegistro").modal('show');
        return false;
    }
    else
        txtBox.style.backgroundColor = corBranca;


    if ((txtBox.value.length < minLength) && (txtBox.value.length > 0)) {
        txtBox.style.backgroundColor = corVermelho;
        if (comPopup == 1) {
            swal({
                type: 'error',
                title: 'Aviso',
                text: 'Comprimento mínimo para Usuário é ' + minLength
            }).then(
                function () {
                    $("#modalSalvarRegistro").modal('show');
                    return false;
                });
        }
        $("#modalSalvarRegistro").modal('show');
        return false;
    }
    else
        if (txtBox.value.length >= minLength) {
            if (validaAlfaNumericoSemAcentosNemEspaco(txtusu_usuario, comPopup, validarVazio)) {

                // checa repeticoes
                var searchValue = '\\b' + txtusu_usuario.value + '\\b';
                var rowId = $('#tblUsuarios').DataTable().column(2).search(searchValue, true, false).rows({ filter: 'applied' }).data();

                if (rowId.length > 0) { // ja tem
                    if (selectedId != rowId[0]["usu_id"]) {
                        $('#txtusu_usuario').css("background-color", corVermelho);
                        swal({
                            type: 'error',
                            title: 'Aviso',
                            text: 'Usuário já cadastrado'
                        }).then(
                            function () {
                                $('#tblUsuarios').DataTable().search('').columns().search('').draw();
                                return false;
                            });
                    }
                    else {
                        $('#tblUsuarios').DataTable().search('').columns().search('').draw();
                        return true; // nao tem repetido
                    }
                }
                else { // nao tem
                    $('#txtusu_usuario').css("background-color", corBranca);
                    $('#tblUsuarios').DataTable().search('').columns().search('').draw();
                    return true; // nao tem repetido
                }
            }
            else {
                txtBox.style.backgroundColor = corVermelho;
                $("#modalSalvarRegistro").modal('show');
                return false;
            }
        }
        else {
            txtBox.style.backgroundColor = corBranca;
            return true; 
        }


}

function validaEmail(txtBox, validarVazio) {
    var corVermelho = "rgb(228, 88, 71)";
    var corBranca = "rgb(255, 255, 255)";

    if (validarVazio == null) validarVazio = 1; // parametro opcional. Se nao passar assume 1

    if ((validarVazio == 1) && ($.trim(txtBox.value) == "")) {
        txtBox.style.backgroundColor = corVermelho;
        swal({
            type: 'error',
            title: 'Aviso',
            text: 'Valores vazios/brancos não são permitidos'
        }).then(
            function () {
                $("#modalSalvarRegistro").modal('show');
                return false;
            });

        $("#modalSalvarRegistro").modal('show');
        return false;    }
    else
        txtBox.style.backgroundColor = corBranca;

    var letters = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;
    if (txtBox.value.length > 5) {

        if ($.trim(txtBox.value).match(letters)) // || ($.trim(txtBox.value).length == 0)) {
        {
            txtBox.style.backgroundColor = corBranca;
        }
        else {
            txtBox.style.backgroundColor = corVermelho;
            swal({
                type: 'error',
                title: 'Aviso',
                text: 'Email inválido'
            }).then(
                function () {
                    return false;
                });

            return false;
        }


     //   var searchValue = '\\b' + ($.trim(txtBox.value.toLowerCase())) + '\\b';
        var searchValue = '^' + ($.trim(txtBox.value.toLowerCase())) + '$';
        var rowId = $('#tblUsuarios').DataTable().column(4).search(searchValue, true, false).rows({ filter: 'applied' }).data();

        if (rowId.length > 0) { // ja tem
            if (selectedId != rowId[0]["usu_id"]) {
                txtBox.style.backgroundColor = corVermelho;

                swal({
                    type: 'error',
                    title: 'Aviso',
                    text: 'Email já cadastrado'
                }).then(
                    function () {
                        $('#tblUsuarios').DataTable().search('').columns().search('').draw();
                        return false;
                    });
                return false;
            }
            else {
                txtBox.style.backgroundColor = corBranca;
                $('#tblUsuarios').DataTable().search('').columns().search('').draw();
                return true;
            }
            return false;
        }
        else { // nao tem
            txtBox.style.backgroundColor = corBranca;
            $('#tblUsuarios').DataTable().search('').columns().search('').draw();
            return true;
        }
    }
    else
        if ($.trim(txtBox.value) != "") {
            txtBox.style.backgroundColor = corVermelho;
            swal({
                type: 'error',
                title: 'Aviso',
                text: 'Comprimento mínimo são 10 caracteres'
            }).then(
                function () {
                    return false;
                });

            return false;
        }

    return true;
}




// ********************  GRID Perfisusuario *****************************
function Usuario_AtivarDesativarPerfil(per_id) {
    var selusu_id = $('#hddnSelectedusu_id').val();

    if ((selusu_id >= 0) && (per_id >= 0)) {
        var response = POST("/usuario/Usuario_AtivarDesativarPerfil", JSON.stringify({ "usu_id": selusu_id, "per_id": per_id }))

        $('#tblPerfisdoUsuario').DataTable().ajax.reload(null, false);  //false if you don't want to refresh paging else true.
    }
    return false;
}

// ********************  GRID GrupoUSUARIOS *****************************
function Usuario_AtivarDesativarGrupo(gru_id) {
    var selusu_id = $('#hddnSelectedusu_id').val();

    if ((selusu_id >= 0) && (gru_id >= 0)) {
        var response = POST("/usuario/Usuario_AtivarDesativarGrupo", JSON.stringify({ "usu_id": selusu_id, "gru_id": gru_id }))

        $('#tblUsuarioGrupos').DataTable().ajax.reload(null, false);  //false if you don't want to refresh paging else true.
    }

    return false;
}


// montagem do gridview
$(document).ready(function () {

    // ****************************GRID usuarioS *****************************************************************************
    $('#tblUsuarios').DataTable({
        "ajax": {
            "url": "/Usuario/Usuario_ListAll",
            "type": "GET",
            "datatype": "json"
        }
        , "columns": [
            { data: "usu_id", "width": "30px", "className": "hide_column", "searchable": false },
            {
                "data": "usu_foto", "width": "40px", "searchable": false,
                "render": function (data) {
                    var defaultURL = "'/images/default.png'";
                    var imagem = '<img  class="img-circle" width="30px" height="30px" src="xxxxx" />' ;
                    var retorno = imagem.replace("xxxxx", data);

                    return retorno;
                }
            },
            { data: "usu_usuario", "autoWidth": true, "searchable": true },
            { data: "usu_nome", "autoWidth": true, "searchable": false },
            { data: "usu_email", "autoWidth": true, "searchable": true },
            {
                "title": "Opções",
                data: "usu_id",
                "searchable": false,
                "sortable": false,
                "render": function (data, type, row) {
                    var retorno = "";
                    if (permissaoEscrita > 0) {
                        retorno = '<a href="#" onclick="return Usuarios_Editar(' + data + ')" title="Editar" ><span class="glyphicon glyphicon-pencil"></span></a>' + '  ';

                        if (row.usu_ativo == 1)
                            retorno += '<a href="#" onclick="return Usuario_AtivarDesativar(' + data + ', 0)" title="Ativo" ><span class="glyphicon glyphicon-ok text-success"></span></a>' + '  ';
                        else
                            retorno += '<a href="#" onclick="return Usuario_AtivarDesativar(' + data + ', 1)" title="Desativado" ><span class="glyphicon glyphicon-remove text-danger"></span></a>' + '  ';
                    }
                    else {
                        retorno = '<span class="glyphicon glyphicon-pencil desabilitado"  ></span>' + '  ';

                        if (row.usu_ativo == 1)
                            retorno += '<span class="glyphicon glyphicon-ok text-success desabilitado" ></span>' + '  ';
                        else
                            retorno += '<span class="glyphicon glyphicon-remove text-danger desabilitado"  ></span>' + '  ';

                    }

                    if (permissaoExclusao > 0)
                        retorno += '<a href="#" onclick="return Usuarios_Deletar(' + data + ')" title="Excluir" ><span class="glyphicon glyphicon-trash"></span></a>';
                    else
                        retorno += '<span class="glyphicon glyphicon-trash desabilitado" title="Excluir" ></span>';

                    return retorno;
                }
            }
        ]
        , "rowId": "usu_id"
        , "rowCallback": function (row, data) {
            if (data.usu_id == selectedId)
                $(row).addClass('selected');
        }
        , "lengthMenu": [[10, 25, 50, 100], [10, 25, 50, 100]]
        , select: {
            style: 'single'
        }
        , "oLanguage": idioma
        , "pagingType": "input"
        , "sDom": '<"top">rt<"bottom"pfli><"clear">'
    });

    var tblUsuarios = $('#tblUsuarios').DataTable();

    $('#tblUsuarios tbody').on('click', 'tr', function () {
        if ($(this).hasClass('selected')) {
            $(this).removeClass('selected');
        }
        else {
            tblUsuarios.$('tr.selected').removeClass('selected');
            $(this).addClass('selected');
        }

        var usuario_id = tblUsuarios.row(this).data();
        selectedId = usuario_id["usu_id"];
        $('#hddnSelectedusu_id').val(usuario_id["usu_id"]);

        var textoHeaderPerfisdoUsuario = "Perfis do Usuário: " + usuario_id["usu_nome"];
        document.getElementById('HeaderPerfisdoUsuario').innerText = textoHeaderPerfisdoUsuario;

        var textoHeaderGruposdoUsuario = "Grupos do Usuário: " + usuario_id["usu_nome"];
        document.getElementById('HeaderGruposdoUsuario').innerText = textoHeaderGruposdoUsuario;

        $('#tblPerfisdoUsuario').DataTable().ajax.reload();
        $('#tblUsuarioGrupos').DataTable().ajax.reload();

        //  alert($('#hddnSelectedusu_id').val());

        document.getElementById('subGrids').style.visibility = "visible";

    });



    // ****************************GRID  PERFIS do usuario *****************************************************************************
    $('#tblPerfisdoUsuario').DataTable(
        {
            "ajax": {
                "url": '/Usuario/Usuario_ListAllPerfis',
                "data": function (d) {
                    d.ID = $('#hddnSelectedusu_id').val();
                },
                "type": "GET",
                "datatype": "json"
            }
            , "columns": [
                { data: "per_id", "width": "30px", "className": "hide_column" },
                { data: "per_Associado", "width": "30px", "className": "centro-horizontal" },
                { data: "per_descricao", "autoWidth": true }

            ]
            , "columnDefs": [
                {
                    targets: [1], // COLUNA ASSOCIADO
                    "orderable": false,
                    render: function (data, type, row) {
                        var retorno = '';
                        if (permissaoEscrita > 0) {
                            if (row.per_Associado == 1)
                                retorno += '<a href="#" onclick="return Usuario_AtivarDesativarPerfil(' + row.per_id + ')" ><span class="glyphicon glyphicon-ok text-success"></span></a>' + '  ';
                            else
                                retorno += '<a href="#" onclick="return Usuario_AtivarDesativarPerfil(' + row.per_id + ')" ><span class="glyphicon glyphicon-remove text-danger"></span></a>' + '  ';
                        }
                        else {
                            if (row.per_Associado == 1)
                                retorno += '<span class="glyphicon glyphicon-ok text-success desabilitado "></span>' + '  ';
                            else
                                retorno += '<span class="glyphicon glyphicon-remove text-danger desabilitado "></span>' + '  ';
                        }

                        return retorno;
                    }
                }
            ]
            , "lengthMenu": [[10, 25, 50, 100], [10, 25, 50, 100]]
            , select: {
                style: 'single'
            }
            , searching: false
            , "oLanguage": idioma
            , "pagingType": "input"
            , "sDom": '<"top">rt<"bottom"pfli><"clear">'
        });



    // ****************************GRID Grupos do USUARIO  *****************************************************************************
    $('#tblUsuarioGrupos').DataTable({
        "ajax": {
            "url": '/Usuario/Usuario_ListAllGrupos',
            "data": function (d) {
                d.ID = $('#hddnSelectedusu_id').val();
            },
            "type": "GET",
            "datatype": "json"
        }
        , "columns": [
            { data: "gru_id", "className": "hide_column" },
            { data: "gru_Associado", "width": "30px", "className": "centro-horizontal" },
            { data: "gru_descricao", "autoWidth": true },
        ]
        , "columnDefs": [
            {
                targets: [1], // COLUNA ASSOCIADO
                "orderable": false,
                render: function (data, type, row) {
                    var retorno = '';

                    if (permissaoEscrita > 0) {
                        if (row.gru_Associado == 1)
                            retorno += '<a href="#" onclick="return Usuario_AtivarDesativarGrupo(' + row.gru_id + ')" ><span class="glyphicon glyphicon-ok text-success"></span></a>' + '  ';
                        else
                            retorno += '<a href="#" onclick="return Usuario_AtivarDesativarGrupo(' + row.gru_id + ')" ><span class="glyphicon glyphicon-remove text-danger"></span></a>' + '  ';
                    }
                    else {
                        if (row.gru_Associado == 1)
                            retorno += '<span class="glyphicon glyphicon-ok text-success desabilitado"></span>' + '  ';
                        else
                            retorno += '<span class="glyphicon glyphicon-remove text-danger desabilitado"></span>' + '  ';
                    }

                    return retorno;
                }
            }

        ]

        , "lengthMenu": [[10, 25, 50, 100], [10, 25, 50, 100]]
        , select: {
            style: 'single'
        }
        , searching: false
        , "oLanguage": idioma
        , "pagingType": "input"
        , "sDom": '<"top">rt<"bottom"pfli><"clear">'
    });


    // ****************************POPUP USUARIOS *****************************************************************************
    $("#imageBrowse2").change(function () {
        var File = this.files;
        if (File && File[0]) {
            var usu_id_sel = $('#txtusu_id').val();
            var fotoAnterior = $('#imgUsuario').attr("src");
            Uploadimage(usu_id_sel, "imageBrowse2", fotoAnterior);
        }
    })

});





