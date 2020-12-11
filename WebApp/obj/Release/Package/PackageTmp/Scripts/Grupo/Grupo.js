
var selectedgru_id;

function Grupo_Inserir() {
    var corBranca = "rgb(255, 255, 255)";
    $('#txtgru_descricao').css("background-color", corBranca);

    $('#txtgru_id').val("");
    $('#txtgru_descricao').val("");
    $('#chkgru_ativo').prop('checked', true);

    $('#btnUpdate').hide();
    $('#btnAdd').show();
    $('#txtgrupo').css('border-color', 'lightgrey');
    $('#chkgru_ativo').css('border-color', 'lightgrey');

    $("#modalSalvarRegistro").modal('show');
    document.getElementById("lblModalHeader").innerText = "Novo grupo";

    selectedgru_id = -1;
}

function Grupo_Salvar() {
    var txtgru_descricao = document.getElementById('txtgru_descricao');
    txtgru_descricao.value = txtgru_descricao.value.trim();

    if (validaAlfaNumerico(txtgru_descricao) && (ChecaRepetido(txtgru_descricao))) {

        var grupo = {
            gru_id: $('#txtgru_id').val(),
            gru_descricao: $('#txtgru_descricao').val(),
            gru_ativo: $('#chkgru_ativo').prop('checked') ? 1 : 0 
        };

        $.ajax({
            url: "/Grupo/Grupo_Salvar",
            data: JSON.stringify(grupo),
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                $("#modalSalvarRegistro").modal('hide');
                $('#tblGrupos').DataTable().ajax.reload(null, false);  //false = sem reload na pagina.
                document.getElementById('HeaderPerfisdoGrupo').innerText = "Perfis do Grupo: " + $('#txtgru_descricao').val();
                document.getElementById('HeaderUsuariosdoGrupo').innerText =  "Usuários do Grupo: " + $('#txtgru_descricao').val();

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

function Grupo_Excluir(id) {
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
            var response = POST("/Grupo/Grupo_Excluir", JSON.stringify({ id: id }))
            if (response.erroId >= 1) {
                swal({
                    type: 'success',
                    title: 'Sucesso',
                    text: 'Registro excluído com sucesso'
                });

                $('#tblGrupos').DataTable().ajax.reload();
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

function Grupo_AtivarDesativar(id, ativar) {

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
                var response = POST("/Grupo/Grupo_AtivarDesativar", JSON.stringify({ id: id }))
                if (response.erroId == 1) {
                    swal({
                        type: 'success',
                        title: 'Sucesso',
                        text: ativar == 1 ? msgAtivacaoOK : msgDesativacaoOK
                    });

                    $('#tblGrupos').DataTable().ajax.reload();
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

function Grupo_Editar(id) {
    document.getElementById("lblModalHeader").innerText = "Editar grupo";

    var corBranca = "rgb(255, 255, 255)";
    $('#txtgru_descricao').css("background-color", corBranca);

    $('#txtgru_descricao').css('border-color', 'lightgrey');
    $.ajax({
        url: "/Grupo/Grupo_GetbyID/" + id,
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            $('#txtgru_id').val(result.gru_id);
            $('#txtgru_descricao').val(result.gru_descricao);
            $('#chkgru_ativo').prop('checked', (result.gru_ativo == '1' ? true : false));

            $("#modalSalvarRegistro").modal('show');
            $('#btnUpdate').show();
            $('#btnAdd').hide();
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
    return false;
}

function ChecaRepetido(txtBox, validarVazio) {
    txtBox.value = txtBox.value.trim();

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


    if ((txtBox.value.length > 0)&& ($.trim(txtBox.value) != "")) {
        if (validaAlfaNumerico(txtBox)) {
            var corVermelho = "rgb(228, 88, 71)";
            var corBranca = "rgb(255, 255, 255)";
            var searchValue = '\\b' + txtBox.value + '\\b';
            var rowId = $('#tblGrupos').DataTable().column(1).search(searchValue, true, false).rows({ filter: 'applied' }).data();
            if (rowId.length > 0) { // ja tem
                if (selectedgru_id != rowId[0]["gru_id"]) {
                    $('#txtgru_descricao').css("background-color", corVermelho);
                    swal({
                        type: 'error',
                        title: 'Aviso',
                        text: 'Grupo já cadastrado'
                    }).then(
                        function () {
                            return false;
                        });
                }
                else {
                    $('#tblGrupos').DataTable().search('').columns().search('').draw();
                    return true;
                }
            }
            else { // nao tem
                $('#txtgru_descricao').css("background-color", corBranca);
                $('#tblGrupos').DataTable().search('').columns().search('').draw();
                return true;
            }
        }
        else {
            $("#modalSalvarRegistro").modal('show');
            return false;
        }
    }


}


// ********************  GRID PerfisGRUPO *****************************
function Grupo_AtivarDesativarPerfil(per_id, ativar) {
    var selgru_id = $('#hddnSelectedgru_id').val();

    if ((selgru_id >= 0) && (per_id >= 0)) {
        var response = POST("/Grupo/Grupo_AtivarDesativarPerfil", JSON.stringify({ "gru_id": selgru_id, "per_id": per_id }))

        $('#tblPerfisDoGrupo').DataTable().ajax.reload(null, false);  //false if you don't want to refresh paging else true.
    }
    return false;
}

// ********************  GRID GrupoUSUARIOS *****************************
function Grupo_AtivarDesativarUsuario(usu_id, ativar) {
    var selgru_id = $('#hddnSelectedgru_id').val();

    if ((selgru_id >= 0) && (usu_id >= 0)) {
        var response = POST("/Grupo/Grupo_AtivarDesativarUsuario", JSON.stringify({ "gru_id": selgru_id, "usu_id": usu_id }))

        $('#tblGrupoUsuarios').DataTable().ajax.reload(null, false);  //false if you don't want to refresh paging else true.
    }

    return false;
}


// montagem do gridview
$(document).ready(function () {

    // ****************************GRID tblGrupos *****************************************************************************
    $('#tblGrupos').DataTable({
        "ajax": {
            "url": "/Grupo/Grupo_ListAll",
            "type": "GET",
            "datatype": "json"
        }
        , "columns": [
            { data: "gru_id", "width": "30px", "className": "hide_column", "searchable": false },
            { data: "gru_descricao", "autoWidth": true, "searchable": true },
            {
                "title": "Opções",
                data: "gru_id",
                "searchable": false,
                "sortable": false,
                "render": function (data, type, row) {
                    var retorno = "";
                    if (permissaoEscrita > 0) {
                        retorno = '<a href="#" onclick="return Grupo_Editar(' + data + ')" title="Editar" ><span class="glyphicon glyphicon-pencil"></span></a>' + '  ';

                        if (row.gru_ativo == 1)
                            retorno += '<a href="#" onclick="return Grupo_AtivarDesativar(' + data + ', 0)" title="Ativo" ><span class="glyphicon glyphicon-ok text-success"></span></a>' + '  ';
                        else
                            retorno += '<a href="#" onclick="return Grupo_AtivarDesativar(' + data + ', 1)" title="Desativado" ><span class="glyphicon glyphicon-remove text-danger"></span></a>' + '  ';
                    }
                    else {
                        retorno = '<span class="glyphicon glyphicon-pencil desabilitado"  ></span>' + '  ';

                        if (row.gru_ativo == 1)
                            retorno += '<span class="glyphicon glyphicon-ok text-success desabilitado" ></span>' + '  ';
                        else
                            retorno += '<span class="glyphicon glyphicon-remove text-danger desabilitado"  ></span>' + '  ';

                    }

                    if (permissaoExclusao > 0)
                        retorno += '<a href="#" onclick="return Grupo_Excluir(' + data + ')" title="Excluir" ><span class="glyphicon glyphicon-trash"></span></a>';
                    else
                        retorno += '<span class="glyphicon glyphicon-trash desabilitado" title="Excluir" ></span>';

                    return retorno;
                }
            }
        ]
        , "rowId": "gru_id"
        , "rowCallback": function (row, data) {
            if (data.gru_id == selectedgru_id)
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

    var tblGrupos = $('#tblGrupos').DataTable();

    $('#tblGrupos tbody').on('click', 'tr', function () {
        if ($(this).hasClass('selected')) {
            $(this).removeClass('selected');
        }
        else {
            tblGrupos.$('tr.selected').removeClass('selected');
            $(this).addClass('selected');
        }

        var grupo_id = tblGrupos.row(this).data();
        $('#hddnSelectedgru_id').val(grupo_id["gru_id"]);
        selectedgru_id = grupo_id["gru_id"];

        var textoHeaderPerfisdoGrupo = "Perfis do Grupo: " + grupo_id["gru_descricao"];
        document.getElementById('HeaderPerfisdoGrupo').innerText = textoHeaderPerfisdoGrupo;

        var textoHeaderUsuariosdogrupo = "Usuários do Grupo: " + grupo_id["gru_descricao"];
        document.getElementById('HeaderUsuariosdoGrupo').innerText = textoHeaderUsuariosdogrupo;

        $('#tblPerfisDoGrupo').DataTable().ajax.reload();
        $('#tblGrupoUsuarios').DataTable().ajax.reload();

        document.getElementById('subGrids').style.visibility = "visible";
    });



    // ****************************GRID PERFIS DO GRUPO  *****************************************************************************
    $('#tblPerfisDoGrupo').DataTable({
        "ajax": {
            "url": '/grupo/Grupo_ListAllPerfis',
            "data": function (d) {
                d.ID = $('#hddnSelectedgru_id').val();
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
                            retorno += '<a href="#" onclick="return Grupo_AtivarDesativarPerfil(' + row.per_id + ')" ><span class="glyphicon glyphicon-ok text-success"></span></a>' + '  ';
                        else
                            retorno += '<a href="#" onclick="return Grupo_AtivarDesativarPerfil(' + row.per_id + ')" ><span class="glyphicon glyphicon-remove text-danger"></span></a>' + '  ';
                    }
                    else {
                        if (row.per_Associado == 1)
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



    // ****************************GRID Grupo USUARIOS *****************************************************************************
    $('#tblGrupoUsuarios').DataTable({
        "ajax": {
            "url": '/Grupo/Grupo_ListAllUsuarios',
            "data": function (d) {
                d.ID = $('#hddnSelectedgru_id').val();
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
                            retorno += '<a href="#" onclick="return Grupo_AtivarDesativarUsuario(' + row.usu_id + ')" ><span class="glyphicon glyphicon-ok text-success"></span></a>' + '  ';
                        else
                            retorno += '<a href="#" onclick="return Grupo_AtivarDesativarUsuario(' + row.usu_id + ')" ><span class="glyphicon glyphicon-remove text-danger"></span></a>' + '  ';
                    }
                    else {
                        if (row.usu_Associado == 1)
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


});
