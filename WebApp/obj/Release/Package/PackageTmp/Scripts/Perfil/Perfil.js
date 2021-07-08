var selectedId;

function Perfil_Inserir() {
    var corBranca = "rgb(255, 255, 255)";
    $('#txtper_descricao').css("background-color", corBranca);

    $('#txtper_id').val("");
    $('#txtper_descricao').val("");
    $('#chkper_ativo').prop('checked', true);

    $('#btnUpdate').hide();
    $('#btnAdd').show();
    $('#txtPerfil').css('border-color', 'lightgrey');
    $('#chkper_ativo').css('border-color', 'lightgrey');

    $("#modalSalvarRegistro").modal('show');
    document.getElementById("lblModalHeader").innerText = "Novo Perfil";
    selectedId = -1;
}

function Perfil_Salvar() {

    var txtper_descricao = document.getElementById('txtper_descricao');
    txtper_descricao.value = txtper_descricao.value.trim();

    if (validaAlfaNumerico(txtper_descricao) && (ChecaRepetido(txtper_descricao))) {

        var Perfil = {
            per_id: $('#txtper_id').val(),
            per_descricao: $('#txtper_descricao').val(),
            per_ativo: $('#chkper_ativo').prop('checked') ? 1 : 0 //,
        };

        $.ajax({
            url: "/Perfil/Perfil_Salvar",
            data: JSON.stringify(Perfil),
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (parseInt(result) > 0) {
                    selectedId = parseInt(result);
                    $('#hddnSelectedper_id').val(selectedId);

                    $('#tblPerfis').DataTable().ajax.reload(null, false);  //false = sem reload na pagina.


                    var textoHeaderModulosdoPerfil = "Módulos do Perfil: " + $('#txtper_descricao').val();
                    document.getElementById('HeaderModulosdoPerfil').innerText = textoHeaderModulosdoPerfil;

                    var textoHeaderGruposdoPerfil = "Grupos do Perfil: " + $('#txtper_descricao').val();
                    document.getElementById('HeaderGruposdoPerfil').innerText = textoHeaderGruposdoPerfil;

                    var textoHeaderUsuariosdoPerfil = "Usuários do Perfil: " + $('#txtper_descricao').val();
                    document.getElementById('HeaderUsuariosdoPerfil').innerText = textoHeaderUsuariosdoPerfil;

                    $('#tblModulosDoPerfil').DataTable().ajax.reload();
                    $('#tblGruposDoPerfil').DataTable().ajax.reload();
                    $('#tblUsuariosDoPerfil').DataTable().ajax.reload();

                    document.getElementById('subGrids').style.visibility = "visible";

                    $("#modalSalvarRegistro").modal('hide');
                }

                return false;
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
    else {
        $("#modalSalvarRegistro").modal('show');
        return false;
    }
    return false;
}

function Perfil_Deletar(id) {
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
            var response = POST("/Perfil/Perfil_Excluir", JSON.stringify({ id: id }))
            if (response.erroId >= 1) {
                swal({
                    type: 'success',
                    title: 'Sucesso',
                    text: 'Registro excluído com sucesso'
                });

                $('#tblPerfis').DataTable().ajax.reload();
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

function Perfil_AtivarDesativar(id, ativar) {

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
                var response = POST("/Perfil/Perfil_AtivarDesativar", JSON.stringify({ id: id }))
                if (response.erroId == 1) {
                    swal({
                        type: 'success',
                        title: 'Sucesso',
                        text: ativar == 1 ? msgAtivacaoOK : msgDesativacaoOK
                    });

                    $('#tblPerfis').DataTable().ajax.reload();
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

function Perfil_Editar(id) {
    document.getElementById("lblModalHeader").innerText = "Editar Perfil";

    var corBranca = "rgb(255, 255, 255)";
    $('#txtper_descricao').css("background-color", corBranca);

    $('#txtper_descricao').css('border-color', 'lightgrey');
    $.ajax({
        url: "/Perfil/Perfil_getbyID/" + id,
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            $('#txtper_id').val(result.per_id);
            $('#txtper_descricao').val(result.per_descricao);
            $('#chkper_ativo').prop('checked', (result.per_ativo == '1' ? true : false));

            $("#modalSalvarRegistro").modal('show');
            $('#btnUpdate').show();
            $('#btnAdd').hide();
            return false;
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
            return false;
        }
    });
    return false;
}

function ChecaRepetido(txtBox, validarVazio) {

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
        return false;
    }
    else
        txtBox.style.backgroundColor = corBranca;


    if ((txtBox.value.length > 0) && ($.trim(txtBox.value) != "")) {
        if (validaAlfaNumerico(txtBox, 1, 0)) {
            var corVermelho = "rgb(228, 88, 71)";
            var corBranca = "rgb(255, 255, 255)";
            var searchValue = '\\b' + txtBox.value + '\\b';
            var rowId = $('#tblPerfis').DataTable().column(1).search(searchValue, true, false).rows({ filter: 'applied' }).data();
            if (rowId.length > 0) { // ja tem
                if (selectedId != rowId[0]["per_id"]) {
                    $('#txtper_descricao').css("background-color", corVermelho);
                    swal({
                        type: 'error',
                        title: 'Aviso',
                        text: 'Perfil já cadastrado'
                    }).then(
                        function () {
                            $('#tblPerfis').DataTable().search('').columns().search('').draw();
                            return false;
                        });
                }
                else {
                    $('#tblPerfis').DataTable().search('').columns().search('').draw();
                    return true;
                }
            }
            else { // nao tem
                $('#txtper_descricao').css("background-color", corBranca);
                $('#tblPerfis').DataTable().search('').columns().search('').draw();
                return true;
            }
        }
        else {
            $("#modalSalvarRegistro").modal('show');
            return false;
        }
    }


}


// ********************  GRID Modulos do Perfil *****************************
function Perfil_AtivarDesativarModulo(id, idPai, coluna, ativar) {

    var selper_id = $('#hddnSelectedper_id').val();
    if ((selper_id >= 0) && (id >= 0)) {

        // se for top de item, avisa
        if (idPai == -1) {
            swal({
                title: "Ativar/Desativar Item Principal se replicará aos seus Subitens. Tem certeza?",
                icon: "warning",
                buttons: [
                    'Não',
                    'Sim'
                ],
                dangerMode: true,
                focusCancel: true
            }).then(function (isConfirm) {
                if (isConfirm) {

                    var response = POST("/Perfil/Perfil_AtivarDesativarModulo", JSON.stringify({ "per_id": selper_id, "mod_id": id, "mod_pai_id": idPai, "operacao": coluna }))
                    if (response.status) {
                        swal({
                            type: 'success',
                            title: 'Sucesso',
                            text: ativar == 1 ? msgAtivacaoOK : msgDesativacaoOK
                        });

                        $('#tblModulos').DataTable().ajax.reload();
                    }
                    else {
                        swal({
                            type: 'error',
                            title: 'Aviso',
                            text: ativar == 1 ? msgAtivacaoErro : msgDesativacaoErro
                        });
                    }
                    $('#tblModulosDoPerfil').DataTable().ajax.reload(null, false);  //false if you don't want to refresh paging else true.
                    return true;
                } else {
                    return false;
                }
            })
        }
        else {
            var response = POST("/Perfil/Perfil_AtivarDesativarModulo", JSON.stringify({ "per_id": selper_id, "mod_id": id, "mod_pai_id": idPai, "operacao": coluna }))
            $('#tblModulosDoPerfil').DataTable().ajax.reload(null, false);  //false if you don't want to refresh paging else true.
        }
    }

    //location.reload(); // tem reload na pagina para reconstruir os menus

    return false;
}

// ********************  GRID Grupos do Perfil *****************************
function Perfil_AtivarDesativarGrupo(gru_id) {
    var selper_id = $('#hddnSelectedper_id').val();

    if ((selper_id >= 0) && (gru_id >= 0)) {
        var response = POST("/Perfil/Perfil_AtivarDesativarGrupo", JSON.stringify({ "per_id": selper_id, "gru_id": gru_id }))

        $('#tblGruposDoPerfil').DataTable().ajax.reload(null, false);  //false if you don't want to refresh paging else true.
    }
    return false;
}

// ********************  GRID PERFILUSUARIOS *****************************
function Perfil_AtivarDesativarUsuario(usu_id) {
    var selper_id = $('#hddnSelectedper_id').val();

    if ((selper_id >= 0) && (usu_id >= 0)) {
        var response = POST("/Perfil/Perfil_AtivarDesativarUsuario", JSON.stringify({ "per_id": selper_id, "usu_id": usu_id }))

        $('#tblUsuariosDoPerfil').DataTable().ajax.reload(null, false);  //false if you don't want to refresh paging else true.
    }

    return false;
}


// montagem dos gridviews
$(document).ready(function () {
    // ****************************GRID PERFIS *****************************************************************************
    $('#tblPerfis').DataTable({
        "ajax": {
            "url": "/Perfil/Perfil_ListAll",
            "type": "GET",
            "datatype": "json"
        }
        , "columns": [
            { data: "per_id", "width": "30px", "className": "hide_column", "searchable": false },
            { data: "per_descricao", "autoWidth": true, "searchable": true },
            {
                "title": "Opções",
                data: "per_id",
                "searchable": false,
                "sortable": false,
                "render": function (data, type, row) {
                    var retorno = "";

                    if (permissaoEscrita > 0) {
                        retorno = '<a href="#" onclick="return Perfil_Editar(' + data + ')" title="Editar" ><span class="glyphicon glyphicon-pencil"></span></a>' + '  ';

                        if (row.per_ativo == 1)
                            retorno += '<a href="#" onclick="return Perfil_AtivarDesativar(' + data + ', 0)" title="Ativo" ><span class="glyphicon glyphicon-ok text-success"></span></a>' + '  ';
                        else
                            retorno += '<a href="#" onclick="return Perfil_AtivarDesativar(' + data + ', 1)" title="Desativado" ><span class="glyphicon glyphicon-remove text-danger"></span></a>' + '  ';
                    }
                    else {
                        retorno = '<span class="glyphicon glyphicon-pencil desabilitado"  ></span>' + '  ';

                        if (row.per_ativo == 1)
                            retorno += '<span class="glyphicon glyphicon-ok text-success desabilitado"></span>' + '  ';
                        else
                            retorno += '<span class="glyphicon glyphicon-remove text-danger desabilitado"  ></span>' + '  ';

                    }

                    if (permissaoExclusao > 0)
                        retorno += '<a href="#" onclick="return Perfil_Deletar(' + data + ')" title="Excluir" ><span class="glyphicon glyphicon-trash"></span></a>';
                    else
                        retorno += '<span class="glyphicon glyphicon-trash desabilitado" title="Excluir" ></span>';

                    return retorno;
                }
            }
        ]
        , "rowId": "per_id"
        , "rowCallback": function (row, data) {
            if (data.per_id == selectedId)
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

    var tblPerfis = $('#tblPerfis').DataTable();
    $('#tblPerfis tbody').on('click', 'tr', function () {

        if ($(this).hasClass('selected')) {
            $(this).removeClass('selected');
        }
        else {
            tblPerfis.$('tr.selected').removeClass('selected');
            $(this).addClass('selected');
        }

        var perfil_id = tblPerfis.row(this).data();
        selectedId = perfil_id["per_id"];

        $('#hddnSelectedper_id').val(perfil_id["per_id"]);

        var textoHeaderModulosdoPerfil = "Módulos do Perfil: " + perfil_id["per_descricao"];
        document.getElementById('HeaderModulosdoPerfil').innerText = textoHeaderModulosdoPerfil;

        var textoHeaderGruposdoPerfil = "Grupos do Perfil: " + perfil_id["per_descricao"];
        document.getElementById('HeaderGruposdoPerfil').innerText = textoHeaderGruposdoPerfil;

        var textoHeaderUsuariosdoPerfil = "Usuários do Perfil: " + perfil_id["per_descricao"];
        document.getElementById('HeaderUsuariosdoPerfil').innerText = textoHeaderUsuariosdoPerfil;

        $('#tblModulosDoPerfil').DataTable().ajax.reload();
        $('#tblGruposDoPerfil').DataTable().ajax.reload();
        $('#tblUsuariosDoPerfil').DataTable().ajax.reload();

        document.getElementById('subGrids').style.visibility = "visible";

    }

    );


    // ****************************GRID PERFILMODULOS *****************************************************************************
    var selper_id = $('#hddnSelectedper_id').val();
    //alert($('#hddnSelectedper_id').val());

    $('#tblModulosDoPerfil').DataTable({
        "ajax": {
            "url": '/Perfil/Perfil_ListAllModulos',
            "data": function (d) {
                d.ID = $('#hddnSelectedper_id').val();
            },
            "type": "GET",
            "datatype": "json"
        }
        , "columns": [
            { data: "mod_id", "width": "30px" },
            { data: "mod_pai_id", "width": "30px", "className": "hide_column" },
            { data: "mod_nome_modulo", "width": "200px" },
            { data: "mod_ativo", "autoWidth": true, "className": "centro-horizontal" },
            { data: "mfl_leitura", "autoWidth": true, "className": "centro-horizontal" },
            { data: "mfl_escrita", "autoWidth": true, "className": "centro-horizontal" },
            { data: "mfl_excluir", "autoWidth": true, "className": "centro-horizontal" },
            { data: "mfl_inserir", "autoWidth": true, "className": "centro-horizontal" }

        ]
        , "columnDefs": [
            {
                targets: [2], // modulo
                "orderable": false
                , "createdCell": function (td, cellData, rowData, row, col) {
                    if (rowData["mod_pai_id"] >= 0) {
                        $(td).addClass('moduloFilho');
                    }
                }
            }
            , {
                targets: [3], // modulo ativado / desativado -> somente leitura
                "orderable": false,
                render: function (data, type, row) {
                    var retorno = '';

                    if (row.mod_ativo == 1)
                        retorno += '<span class="glyphicon glyphicon-ok text-success desabilitado"  >' + '  ';
                    else
                        retorno += '<span class="glyphicon glyphicon-remove text-danger desabilitado"  ></span>' + '  ';
                    return retorno;
                }
            }
            , {
                targets: [4], // COLUNA LEITURA
                "orderable": false,
                render: function (data, type, row) {
                    var retorno = '';

                    if (permissaoEscrita > 0) {
                        if (row.mfl_leitura == 1)
                            retorno += '<a href="#" onclick="return Perfil_AtivarDesativarModulo(' + row.mod_id + ',' + row.mod_pai_id + ',4, 0)" ><span class="glyphicon glyphicon-ok text-success"></span></a>' + '  ';
                        else
                            retorno += '<a href="#" onclick="return Perfil_AtivarDesativarModulo(' + row.mod_id + ',' + row.mod_pai_id + ',4, 1)" ><span class="glyphicon glyphicon-remove text-danger"></span></a>' + '  ';
                    }
                    else {
                        if (row.mfl_leitura == 1)
                            retorno += '<span class="glyphicon glyphicon-ok text-success desabilitado"  ></span>' + '  ';
                        else
                            retorno += '<span class="glyphicon glyphicon-remove text-danger desabilitado"  ></span>' + '  ';
                    }

                    return retorno;
                }
            }
            , {
                targets: [5], // COLUNA ESCRITA
                "orderable": false,
                render: function (data, type, row) {
                    var retorno = '';

                    if (permissaoEscrita > 0) {
                        if (row.mfl_escrita == 1)
                            retorno += '<a href="#" onclick="return Perfil_AtivarDesativarModulo(' + row.mod_id + ',' + row.mod_pai_id + ',5, 0)" ><span class="glyphicon glyphicon-ok text-success"></span></a>' + '  ';
                        else
                            retorno += '<a href="#" onclick="return Perfil_AtivarDesativarModulo(' + row.mod_id + ',' + row.mod_pai_id + ',5, 1)" ><span class="glyphicon glyphicon-remove text-danger"></span></a>' + '  ';
                    }
                    else {
                        if (row.mfl_escrita == 1)
                            retorno += '<span class="glyphicon glyphicon-ok text-success desabilitado"  ></span>' + '  ';
                        else
                            retorno += '<span class="glyphicon glyphicon-remove text-danger desabilitado"  ></span>' + '  ';
                    }

                    return retorno;
                }
            }
            , {
                targets: [6], // COLUNA EXCLUSAO
                "orderable": false,
                render: function (data, type, row) {
                    var retorno = '';

                    if (permissaoEscrita > 0) {
                        if (row.mfl_excluir == 1)
                            retorno += '<a href="#" onclick="return Perfil_AtivarDesativarModulo(' + row.mod_id + ',' + row.mod_pai_id + ',6, 0)" ><span class="glyphicon glyphicon-ok text-success"></span></a>' + '  ';
                        else
                            retorno += '<a href="#" onclick="return Perfil_AtivarDesativarModulo(' + row.mod_id + ',' + row.mod_pai_id + ',6, 1)" ><span class="glyphicon glyphicon-remove text-danger"></span></a>' + '  ';
                    }
                    else {
                        if (row.mfl_excluir == 1)
                            retorno += '<span class="glyphicon glyphicon-ok text-success desabilitado"  ></span>' + '  ';
                        else
                            retorno += '<span class="glyphicon glyphicon-remove text-danger desabilitado" ></span>' + '  ';
                    }

                    return retorno;
                }
            }
            , {
                targets: [7], // COLUNA INSERCAO
                "orderable": false,
                render: function (data, type, row) {
                    var retorno = '';
                    if (permissaoEscrita > 0) {
                        if (row.mfl_inserir == 1)
                            retorno += '<a href="#" onclick="return Perfil_AtivarDesativarModulo(' + row.mod_id + ',' + row.mod_pai_id + ',7, 0)" ><span class="glyphicon glyphicon-ok text-success"></span></a>' + '  ';
                        else
                            retorno += '<a href="#" onclick="return Perfil_AtivarDesativarModulo(' + row.mod_id + ',' + row.mod_pai_id + ',7, 1)" ><span class="glyphicon glyphicon-remove text-danger"></span></a>' + '  ';
                    }
                    else {
                        if (row.mfl_inserir == 1)
                            retorno += '<span class="glyphicon glyphicon-ok text-success desabilitado" ></span>' + '  ';
                        else
                            retorno += '<span class="glyphicon glyphicon-remove text-danger desabilitado" ></span>' + '  ';
                    }
                    return retorno;
                }
            }
        ]
        , "createdRow": function (row, data, dataIndex) {
            if (data["mod_pai_id"] == -1) {
                $(row).addClass('moduloPai');
            }
        }
        , "lengthMenu": [[10, 25, 50, 100], [10, 25, 50, 100]]
        , select: {
            style: 'single'
        }
        , searching: false
        , "oLanguage": idioma
        , "pagingType": "input"
        , "sDom": '<"top">rt<"bottom"pfli><"clear">'
    });


    // ****************************GRID PERFIL Grupos *****************************************************************************
    $('#tblGruposDoPerfil').DataTable({
        "ajax": {
            "url": '/Perfil/Perfil_ListAllGrupos',
            "data": function (d) {
                d.ID = $('#hddnSelectedper_id').val();
            },
            "type": "GET",
            "datatype": "json"
        }
        , "columns": [
            { data: "gru_id", "width": "30px", "className": "hide_column" },
            { data: "gru_Associado", "width": "30px", "className": "centro-horizontal" },
            { data: "gru_descricao", "autoWidth": true }

        ]
        , "columnDefs": [
            {
                targets: [1], // COLUNA ASSOCIADO
                "orderable": false,
                render: function (data, type, row) {
                    var retorno = '';
                    if (permissaoEscrita > 0) {
                        if (row.gru_Associado == 1)
                            retorno += '<a href="#" onclick="return Perfil_AtivarDesativarGrupo(' + row.gru_id + ')" ><span class="glyphicon glyphicon-ok text-success"></span></a>' + '  ';
                        else
                            retorno += '<a href="#" onclick="return Perfil_AtivarDesativarGrupo(' + row.gru_id + ')" ><span class="glyphicon glyphicon-remove text-danger"></span></a>' + '  ';
                    }
                    else {
                        if (row.gru_Associado == 1)
                            retorno += '<span class="glyphicon glyphicon-ok text-success desabilitado"  ></span>' + '  ';
                        else
                            retorno += '<span class="glyphicon glyphicon-remove text-danger desabilitado" ></span>' + '  ';
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



    // ****************************GRID PERFIL USUARIOS *****************************************************************************
    $('#tblUsuariosDoPerfil').DataTable({
        "ajax": {
            "url": '/Perfil/Perfil_ListAllUsuarios',
            "data": function (d) {
                d.ID = $('#hddnSelectedper_id').val();
            },
            "type": "GET",
            "datatype": "json"
        }
        , "columns": [
            { data: "usu_id", "width": "30px", "className": "hide_column" },
            { data: "usu_Associado", "width": "30px", "className": "centro-horizontal" },
            { data: "usu_usuario", "width": "150px", },
            { data: "usu_nome", "autoWidth": true }

        ]
        , "columnDefs": [
            {
                targets: [1], // COLUNA ASSOCIADO
                "orderable": false,
                render: function (data, type, row) {
                    var retorno = '';
                    if (permissaoEscrita > 0) {
                        if (row.usu_Associado == 1)
                            retorno += '<a href="#" onclick="return Perfil_AtivarDesativarUsuario(' + row.usu_id + ')" ><span class="glyphicon glyphicon-ok text-success"></span></a>' + '  ';
                        else
                            retorno += '<a href="#" onclick="return Perfil_AtivarDesativarUsuario(' + row.usu_id + ')" ><span class="glyphicon glyphicon-remove text-danger"></span></a>' + '  ';
                    }
                    else {
                        if (row.usu_Associado == 1)
                            retorno += '<span class="glyphicon glyphicon-ok text-success desabilitado"  ></span>' + '  ';
                        else
                            retorno += '<span class="glyphicon glyphicon-remove text-danger desabilitado" ></span>' + '  ';
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


});

