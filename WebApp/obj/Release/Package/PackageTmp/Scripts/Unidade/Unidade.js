
var Id_Unidade_Tipo_TPU = -1;
var selecteduni_id = -1;
var selectedunt_id = -31415;
var selunt_nome = "";

function Unidade_Tipo_LimparFiltro() {
    selunt_nome = "";
    selectedunt_id = null;
    $('#txtLocalizarUnidade_Tipo').val('');
    $('#tblUnidade_Tipos').DataTable().ajax.reload();
    document.getElementById('subGrids').style.visibility = "hidden";
}

function Unidade_Tipo_Filtrar() {

    selunt_nome = $('#txtLocalizarUnidade_Tipo').val();

    selectedunt_id = null;
    $('#tblUnidade_Tipos').DataTable().ajax.reload();
    document.getElementById('subGrids').style.visibility = "hidden";
}

function Unidade_Tipo_Inserir() {
    var corBranca = "rgb(255, 255, 255)";
    $('#txtunt_nome').css("background-color", corBranca);
    $('#txtunt_nome').val("");

    $("#modalSalvarUnidade_Tipo").modal('show');
    document.getElementById("lblModalHeaderUnidade_Tipo").innerText = "Novo Tipo de Unidade";

    selectedunt_id = -1;
}

function Unidade_Tipo_Salvar() {
    var txtunt_nome = document.getElementById('txtunt_nome');
    txtunt_nome.value = txtunt_nome.value.trim();

    if (validaAlfaNumerico(txtunt_nome) && (ChecaRepetido(txtunt_nome))) {

        var tipo = {
            unt_id: selectedunt_id,
            unt_nome: $('#txtunt_nome').val()
        };

        $.ajax({
            url: "/Unidade/Unidade_Tipo_Salvar",
            data: JSON.stringify(tipo),
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                $("#modalSalvarUnidade_Tipo").modal('hide');

                $('#tblUnidade_Tipos').DataTable().ajax.reload(null, false);  //false = sem reload na pagina.
                document.getElementById('HeaderUnidade_Tipo').innerText = "Unidades do Tipo: " + $('#txtunt_nome').val();

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

function Unidade_Tipo_Excluir(id) {
    swal({
        title: "Excluir Unidade_Tipo e suas Unidades. Tem certeza?",
        icon: "warning",
        buttons: [
            'Não',
            'Sim'
        ],
        dangerMode: true,
        focusCancel: true
    }).then(function (isConfirm) {
        if (isConfirm) {
            var response = POST("/Unidade/Unidade_Tipo_Excluir", JSON.stringify({ "unt_id": id }))
            if (response.erroId >= 1) {
                swal({
                    type: 'success',
                    title: 'Sucesso',
                    text: 'Registro excluído com sucesso'
                });

                $("#modalSalvarUnidade_Tipo").modal('hide');
                $('#tblUnidade_Tipos').DataTable().ajax.reload();
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

function Unidade_Tipo_Editar(id) {
    document.getElementById("lblModalHeaderUnidade_Tipo").innerText = "Editar Tipo de Unidade";

    var corBranca = "rgb(255, 255, 255)";
    $('#txtunt_nome').css("background-color", corBranca);
    $('#txtunt_nome').css('border-color', 'lightgrey');

    $.ajax({
        url: "/Unidade/Unidade_Tipo_GetbyID/" + id,
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            $('#txtunt_nome').val(result.unt_nome);

            $("#modalSalvarUnidade_Tipo").modal('show');
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
        var rowId = $('#tblUnidade_Tipos').DataTable().column(1).search(searchValue, true, false).rows({ filter: 'applied' }).data();
        if (rowId.length > 0) { // ja tem
            if (selectedunt_id != rowId[0]["unt_id"]) {
                $('#txtunt_nome').css("background-color", corVermelho);
                swal({
                    type: 'error',
                    title: 'Aviso',
                    text: 'Unidade_Tipo já cadastrado'
                }).then(
                    function () {
                        return false;
                    });
            }
            else {
                $('#tblUnidade_Tipos').DataTable().search('').columns().search('').draw();
                return true;
            }
        }
        else { // nao tem
            $('#txtunt_nome').css("background-color", corBranca);
            $('#tblUnidade_Tipos').DataTable().search('').columns().search('').draw();
            return true;
        }
    }
    else {
        $("#modalSalvarRegistro").modal('show');
        return false;
    }

}

// **************************** tblUnidades *****************************************************************************

function Unidade_Inserir() {
    var corBranca = "rgb(255, 255, 255)";

    $.ajax({
        url: "/Unidade/Unidade_GetbyID/12", // faz uma chamada somente para preencher o combo
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            $('#txtuni_unidade').css("background-color", corBranca);
            $('#txtuni_descricao').css("background-color", corBranca);

            $('#txtuni_id').val("");
            $('#txtuni_unidade').val("");
            $('#txtuni_descricao').val("");

            // preenche combo cmbUnidade_Tipo
            $("#cmbUnidade_Tipo").html("");
            $("#cmbUnidade_Tipo").append($('<option></option>').val(-1).html(" ")); // 1o item vazio
            $.each(result.lstUnidade_Tipo, function (i, subNivel) {
                $("#cmbUnidade_Tipo").append($('<option></option>').val(subNivel.Value).html(subNivel.Text));
            });

            $("#cmbUnidade_Tipo").val(selectedunt_id);
            selecteduni_id = -1;


            document.getElementById("lblModalHeader").innerText = "Nova unidade";
            $("#modalSalvarRegistro").modal('show');
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });

    selecteduni_id = -1;
}

function Unidade_Salvar() {
    var txtuni_unidade = document.getElementById('txtuni_unidade');
    txtuni_unidade.value = txtuni_unidade.value.trim();

    var txtuni_descricao = document.getElementById('txtuni_descricao');
    txtuni_descricao.value = txtuni_descricao.value.trim();

    var selectedUnidade_TipoVal = $('#cmbUnidade_Tipo').val();

    if (validaAlfaNumerico(txtuni_descricao) && (ChecaRepetido(txtuni_descricao))) {

        var unidade = {
            uni_id: $('#txtuni_id').val(),
            uni_unidade: $('#txtuni_unidade').val(),
            uni_descricao: $('#txtuni_descricao').val(),
            unt_id: (selectedUnidade_TipoVal == null ? -1 : selectedUnidade_TipoVal)
        };

        $.ajax({
            url: "/Unidade/Unidade_Salvar",
            data: JSON.stringify(unidade),
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                $("#modalSalvarRegistro").modal('hide');
                $('#tblUnidades').DataTable().ajax.reload(null, false);  //false = sem reload na pagina.
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

function Unidade_Excluir(id) {
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
            var response = POST("/Unidade/Unidade_Excluir", JSON.stringify({ "uni_id": id }))
            if (response.erroId >= 1) {
                swal({
                    type: 'success',
                    title: 'Sucesso',
                    text: 'Registro excluído com sucesso'
                });

                $("#modalSalvar").modal('hide');
                $('#tblUnidades').DataTable().ajax.reload();
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

function Unidade_Editar(id) {
    document.getElementById("lblModalHeader").innerText = "Editar unidade";

    var corBranca = "rgb(255, 255, 255)";
    $('#txtuni_unidade').css("background-color", corBranca);
    $('#txtuni_descricao').css("background-color", corBranca);

    $('#txtuni_unidade').css('border-color', 'lightgrey');
    $('#txtuni_descricao').css('border-color', 'lightgrey');

    $.ajax({
        url: "/Unidade/Unidade_GetbyID/" + id,
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            $('#txtuni_id').val(result.uni_id);
            $('#txtuni_unidade').val(result.uni_unidade);
            $('#txtuni_descricao').val(result.uni_descricao);

            // preenche combo cmbUnidade_Tipo
            $("#cmbUnidade_Tipo").html("");
            $("#cmbUnidade_Tipo").append($('<option></option>').val(-1).html(" ")); // 1o item vazio
            $.each(result.lstUnidade_Tipo, function (i, subNivel) {
                $("#cmbUnidade_Tipo").append($('<option></option>').val(subNivel.Value).html(subNivel.Text));
            });

            $("#cmbUnidade_Tipo").val(result.unt_id);


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
        var rowId = $('#tblUnidades').DataTable().column(1).search(searchValue, true, false).rows({ filter: 'applied' }).data();
        if (rowId.length > 0) { // ja tem
            if (selecteduni_id != rowId[0]["uni_id"]) {
                $('#txtuni_descricao').css("background-color", corVermelho);
                swal({
                    type: 'error',
                    title: 'Aviso',
                    text: 'Unidade já cadastrada'
                }).then(
                    function () {
                        return false;
                    });
            }
            else {
                $('#tblUnidades').DataTable().search('').columns().search('').draw();
                return true;
            }
        }
        else { // nao tem
            $('#txtuni_descricao').css("background-color", corBranca);
            $('#tblUnidades').DataTable().search('').columns().search('').draw();
            return true;
        }
    }
    else {
        $("#modalSalvarRegistro").modal('show');
        return false;
    }

}


// ajuste para ordenacao com palavras acentuadas
jQuery.extend(jQuery.fn.dataTableExt.oSort, {
    "pt-string-asc": function (s1, s2) {
        return s1.localeCompare(s2);
    },

    "pt-string-desc": function (s1, s2) {
        return s2.localeCompare(s1);
    }
});

// montagem do gridview +  gridUnidade_Tipos
$(document).ready(function () {

    // ****************************GRID tblUnidade_Tipos *****************************************************************************
    $('#tblUnidade_Tipos').DataTable({
        "ajax": {
            "url": "/Unidade/Unidade_Tipo_ListAll",
            "data": function (d) { d.ID = selectedunt_id; d.unt_nome = selunt_nome; },
            "type": "GET",
            "datatype": "json"
        }
        , "fnInitComplete": function (oSettings, json) {
            $('#tblUnidade_Tipos tbody tr:eq(0)').click();
        }
        , "columns": [
            { data: "unt_id", "className": "hide_column", "searchable": true },
            { data: "unt_nome", "autoWidth": true, "searchable": true },
            {
                "title": "Opções",
                data: "unt_id",
                "searchable": false,
                "sortable": false,
                "render": function (data, type, row) {
                    var retorno = "";
                    if (permissaoEscrita > 0) {
                        retorno = '<a href="#" onclick="return Unidade_Tipo_Editar(' + data + ')" title="Editar" ><span class="glyphicon glyphicon-pencil"></span></a>' + '  ';
                    }
                    else {
                        retorno = '<span class="glyphicon glyphicon-pencil desabilitado"  ></span>' + '  ';
                    }

                    retorno += '<span class="glyphicon glyphicon-ok text-success desabilitado" ></span>' + '  ';

                    if (permissaoExclusao > 0)
                        retorno += '<a href="#" onclick="return Unidade_Tipo_Excluir(' + data + ')" title="Excluir" ><span class="glyphicon glyphicon-trash"></span></a>';
                    else
                        retorno += '<span class="glyphicon glyphicon-trash desabilitado" title="Excluir" ></span>';

                    return retorno;
                }
            }
        ]
        , columnDefs: [
            { type: 'pt-string', targets: 1 }
        ]

        , "order": [1, "asc"]
        , "rowId": "unt_id"
        , "rowCallback": function (row, data) {
            if (data.unt_id == selectedunt_id)
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

    var tblUnidade_Tipos = $('#tblUnidade_Tipos').DataTable();
    $('#tblUnidade_Tipos tbody').on('click', 'tr', function () {
        if ($(this).hasClass('selected')) {
            $(this).removeClass('selected');
        }
        else {
            tblUnidade_Tipos.$('tr.selected').removeClass('selected');
            $(this).addClass('selected');
        }

        var tipo_id = tblUnidade_Tipos.row(this).data();

        //  if (selectedunt_id == -31415)
        $('#txtLocalizarUnidade_Tipo').val(tipo_id["unt_nome"]);

        $('#hddnSelectedunt_id').val(tipo_id["unt_id"]);
        selectedunt_id = tipo_id["unt_id"];


        var textoHeaderUnidade_Tipo = "Unidades do Tipo: " + tipo_id["unt_nome"];
        document.getElementById('HeaderUnidade_Tipo').innerText = textoHeaderUnidade_Tipo;

        $('#tblUnidades').DataTable().ajax.reload();
        document.getElementById('subGrids').style.visibility = "visible";
    });

}); // document.ready


// ****************************GRID tblUnidades *****************************************************************************
$('#tblUnidades').DataTable({
    "ajax": {
        "url": "/Unidade/Unidade_ListAll",
        "data": function (d) { d.unt_id = selectedunt_id; },
        "type": "GET",
        "datatype": "json"
    }
    , "columns": [
        { data: "uni_id", "className": "hide_column", "searchable": false },
        { data: "unt_id", "className": "hide_column", "searchable": false },
        { data: "uni_unidade", "autoWidth": true, "searchable": true },
        { data: "uni_descricao", "autoWidth": true, "searchable": true },
        { data: "unt_nome", "autoWidth": true, "searchable": true },
        {
            "title": "Opções",
            data: "uni_id",
            "searchable": false,
            "sortable": false,
            "render": function (data, type, row) {
                var retorno = "";
                if (permissaoEscrita > 0)
                    retorno = '<a href="#" onclick="return Unidade_Editar(' + data + ')" title="Editar" ><span class="glyphicon glyphicon-pencil"></span></a>' + '  ';
                else
                    retorno = '<span class="glyphicon glyphicon-pencil desabilitado"  ></span>' + '  ';

                retorno += '<span class="glyphicon glyphicon-ok text-success desabilitado" ></span>' + '  ';

                if (permissaoExclusao > 0)
                    retorno += '<a href="#" onclick="return Unidade_Excluir(' + data + ')" title="Excluir" ><span class="glyphicon glyphicon-trash"></span></a>';
                else
                    retorno += '<span class="glyphicon glyphicon-trash desabilitado" title="Excluir" ></span>';

                return retorno;
            }
        }
    ]
    , "rowId": "uni_id"
    , "rowCallback": function (row, data) {
        if (data.uni_id == selecteduni_id)
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

var tblUnidades = $('#tblUnidades').DataTable();
$('#tblUnidades tbody').on('click', 'tr', function () {
    if ($(this).hasClass('selected')) {
        $(this).removeClass('selected');
    }
    else {
        tblUnidades.$('tr.selected').removeClass('selected');
        $(this).addClass('selected');
    }

    var unidade_id = tblUnidades.row(this).data();
    $('#hddnSelecteduni_id').val(unidade_id["uni_id"]);
    selecteduni_id = unidade_id["uni_id"];

    document.getElementById('subGrids').style.visibility = "visible";
});


