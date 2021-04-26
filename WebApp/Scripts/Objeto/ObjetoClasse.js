var selectedId_ObjTipo;
var selectedId_ObjClasse;
var corVermelho = "rgb(228, 88, 71)";
var corBranca = "rgb(255, 255, 255)";

function Inserir(qual) {
    var corBranca = "rgb(255, 255, 255)";
    $('#txtcodigo').css("background-color", corBranca);
    $('#txtnome').css("background-color", corBranca);
    $('#txtdescricao').css("background-color", corBranca);

    $('#txt_id').val("");
    $('#txtcodigo').val("");
    $('#txtnome').val("");
    $('#txtdescricao').val("");
    $('#chkativo').prop('checked', true);

    $('#btnUpdate').hide();
    $('#btnAdd').show();
    $('#chkativo').css('border-color', 'lightgrey');

    $("#modalSalvarRegistro").modal('show');

    document.getElementById("divTip_Pai").style.display = 'none';
   if (qual == 1) {
        document.getElementById("div_tip_codigo").style.display = 'block';
        document.getElementById("lblModalHeader").innerText = "Novo Tipo de Objeto";
        selectedId_ObjTipo = -1;

            
        $.ajax({
            url: "/Objeto/lstTipos_da_Classe",
            type: "GET",
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            "data": { "clo_id": selectedId_ObjClasse },
            success: function (result) {
                    var combo = document.getElementById("cmbtip_pai");
                    if (combo) {
                        combo.innerText = null; // limpa
                        // preenche combo
                        var lista = result.data.split(";");

                        for (var m = 0; m < lista.length; m++) {
                            var opt = document.createElement("option");
                            if (lista[m].indexOf(":") > 0) {
                                var aux = lista[m].split(":");
                                opt.value = aux[0];
                                opt.textContent = aux[1];

                                combo.appendChild(opt);
                            }
                        }
                    }


                    switch (selectedId_ObjClasse)
                    {
                    case -1: case 1: case 2: case 3: case 6: case 10: case 11: document.getElementById("divTip_Pai").style.display = 'none'; break;
                    default: document.getElementById("divTip_Pai").style.display = 'block';
                }
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });


    }
    else {
        document.getElementById("div_tip_codigo").style.display = 'none';
        document.getElementById("lblModalHeader").innerText = "Nova Classe de Objeto";
        selectedId_ObjClasse = -1;
    }

}


function Salvar() {
    var tabela;
    if (document.getElementById("lblModalHeader").innerText.indexOf("Tipo") > 0) {
        qual = 1;
        tabela = '#tblObjTipos';
    }
    else {
        qual = 2;
        tabela = '#tblObjClasses';
    }

    var txtcodigo = document.getElementById('txtcodigo');
    var txtnome = document.getElementById('txtnome');
    var txtdescricao = document.getElementById('txtdescricao');

    // checa vazios
    if ((qual == 1) && (txtcodigo.value.trim() == "")) {
        swal({
            type: 'error',
            title: 'Aviso',
            text: 'Código está vazio'
        }).then(
            function () {
                txtcodigo.style.backgroundColor = corVermelho;
                return false;
            });

        txtcodigo.style.backgroundColor = corVermelho;

        $("#modalSalvarRegistro").modal('show');
        return false;
    }

    if ((txtnome.value.trim() == ""))
    {
        swal({
            type: 'error',
            title: 'Aviso',
            text: 'Nome está vazio'
        }).then(
            function () {
                txtnome.style.backgroundColor = corVermelho;
                return false;
            });

        txtnome.style.backgroundColor = corVermelho;

        $("#modalSalvarRegistro").modal('show');
        return false;
    }

    if ((txtdescricao.value.trim() == "")) {
        swal({
            type: 'error',
            title: 'Aviso',
            text: 'Descrição está vazia'
        }).then(
            function () {
                txtdescricao.style.backgroundColor = corVermelho;
                return false;
            });

        txtdescricao.style.backgroundColor = corVermelho;

        $("#modalSalvarRegistro").modal('show');
        return false;
    }

    // checa repeticoes
     //if (qual == 1) {
     //   if (!ChecaRepetido(txtcodigo))
     //       return false;
     //}

    if (qual == 2) {
        if (!ChecaRepetido(txtnome))
            return false;
    }


    var validatxtcodigo = validaAlfaNumerico(txtcodigo, 1,0);
    var validatxtnome = validaAlfaNumerico(txtnome, 1,0);
    var validatxtdescricao = checaVazio(txtdescricao, 1);

    if (((qual == 1) && (validatxtcodigo) && (validatxtnome) && (validatxtdescricao))
        || ((qual == 2) && (validatxtnome) && (validatxtdescricao))
       )
    {
       var param;
        var url;
        if (qual == 1) {
            url = "/Objeto/ObjTipo_Salvar";
            param = {
                tip_id: $('#txt_id').val(),
                clo_id: $('#hddnSelectedclo_id').val(),
                tip_codigo: $('#txtcodigo').val(),
                tip_nome: $('#txtnome').val(),
                tip_descricao: $('#txtdescricao').val(),
                tip_pai: $('#cmbtip_pai').val(),
                tip_ativo: $('#chkativo').prop('checked') ? 1 : 0 //,
            };
        }
        else {
            url = "/Objeto/ObjClasse_Salvar";
            param = {
                clo_id: $('#txt_id').val(),
                clo_nome: $('#txtnome').val(),
                clo_descricao: $('#txtdescricao').val(),
                clo_ativo: $('#chkativo').prop('checked') ? 1 : 0 //,
            };
        }

        $.ajax({
            url: url,
            data: JSON.stringify(param),
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                $("#modalSalvarRegistro").modal('hide');
                $(tabela).DataTable().ajax.reload(null, false);  //false = sem reload na pagina.


                if (qual == 2) {
                    selectedId_ObjClasse = result;
                    $('#hddnSelectedclo_id').val(result);
                    selectedId_ObjTipo = -1;
                    document.getElementById('HeaderObjTipos').innerText = "Tipos da Classe: " + $('#txtnome').val();
                    $('#tblObjTipos').DataTable().ajax.reload(null, false);
                }
                else
                {
                    selectedId_ObjTipo = result;
                }
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

function Deletar(qual, id) {
    var url;
    var tabela;
    var params;
    if (qual == 1) {
        tabela = '#tblObjTipos';
        url = "/Objeto/ObjTipo_Excluir";
        params = JSON.stringify({ id: id, clo_id: $('#hddnSelectedclo_id').val() });
    }
    else {
        tabela = '#tblObjClasses';
        url = "/Objeto/ObjClasse_Excluir";
        params = JSON.stringify({ id: id });
    }

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
            var response = POST(url, params)
            if (response.erroId >= 1) {
                swal({
                    type: 'success',
                    title: 'Sucesso',
                    text: 'Registro excluído com sucesso'
                });

                $(tabela).DataTable().ajax.reload();
                //document.getElementById('subGrids').style.visibility = "hidden";
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

function AtivarDesativar(qual, id, ativar) {

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
                var url;
                var tabela;

                if (qual == 1) {
                    tabela = '#tblObjTipos';
                    url = "/Objeto/ObjTipo_AtivarDesativar";
                    params = JSON.stringify({ id: id, clo_id: $('#hddnSelectedclo_id').val() });
                }
                else {
                    tabela = '#tblObjClasses';
                    url = "/Objeto/ObjClasse_AtivarDesativar";
                    params = JSON.stringify({ id: id });
                }

                var response = POST(url, params)
                if (response.erroId == 1) {
                    swal({
                        type: 'success',
                        title: 'Sucesso',
                        text: ativar == 1 ? msgAtivacaoOK : msgDesativacaoOK
                    });

                    $(tabela).DataTable().ajax.reload();
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

function Editar(qual, id) {
    var corBranca = "rgb(255, 255, 255)";
    $('#txtcodigo').css("background-color", corBranca);
    $('#txtdescricao').css("background-color", corBranca);

    $('#txtcodigo').css('border-color', 'lightgrey');
    $('#txtdescricao').css('border-color', 'lightgrey');

    var url;
    var params;
    if (qual == 1) {
        document.getElementById("lblModalHeader").innerText = "Editar Tipo de Objeto";
        url = "/Objeto/ObjTipo_GetbyID";
        var clo_id = parseInt($('#hddnSelectedclo_id').val());
        params = { "ID": id, "clo_id": clo_id };

        document.getElementById("div_tip_codigo").style.display = 'block';
    }
    else {
        document.getElementById("div_tip_codigo").style.display = 'none';

        document.getElementById("lblModalHeader").innerText = "Editar Classe de Objeto";
        url = "/Objeto/ObjClasse_GetbyID";
        params = { "ID": id };
    }

    $.ajax({
        url: url,
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        "data": params,
        success: function (result) {
            if (qual == 1) {
                $('#txt_id').val(result.tip_id);
                $('#txtcodigo').val(result.tip_codigo);
                $('#txtnome').val(result.tip_nome);
                $('#txtdescricao').val(result.tip_descricao);
                $('#chkativo').prop('checked', (result.tip_ativo == '1' ? true : false));

                var combo = document.getElementById("cmbtip_pai");
                if (combo)
                {
                    combo.innerText = null; // limpa
                    // preenche combo
                    var lista = result.lsttip_pai.split(";");

                    for (var m = 0; m < lista.length; m++) {
                        var opt = document.createElement("option");
                        if (lista[m].indexOf(":") > 0) {
                            var aux = lista[m].split(":");
                            opt.value = aux[0];
                            opt.textContent = aux[1];

                            combo.appendChild(opt);
                        }
                    }
                    combo.value = result.tip_pai;

                    var divTip_Pai = document.getElementById("divTip_Pai");
                    if (divTip_Pai) {
                        if (parseInt(result.tip_pai) < 0)
                            divTip_Pai.style.display = 'none';
                        else
                            divTip_Pai.style.display = 'block';
                    }

                }


            }
            else {
                $('#txt_id').val(result.clo_id);
                $('#txtnome').val(result.clo_nome);
                $('#txtdescricao').val(result.clo_descricao);
                $('#chkativo').prop('checked', (result.clo_ativo == '1' ? true : false));
            }

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

function ChecaRepetido(txtBox) {

    var selectedId;
    var tabela;
    var campoId;
    var texto;
    if (document.getElementById("lblModalHeader").innerText.indexOf("Tipo") > 0) {
        selectedId = selectedId_ObjTipo;
        tabela = '#tblObjTipos';
        campoId = "tip_id";
        texto = 'Tipo já cadastrado';
        txt = "#txtcodigo";
        coluna = 2;
    }
    else {
        selectedId = selectedId_ObjClasse;
        tabela = '#tblObjClasses';
        campoId = "clo_id";
        texto = 'Classe já cadastrada';
        txt = "#txtnome";
        coluna = 1;
    }


    //if (validaAlfaNumerico(txtBox, 0))
    //{
        var corVermelho = "rgb(228, 88, 71)";
        var corBranca = "rgb(255, 255, 255)";
        var searchValue = '\\b' + txtBox.value + '\\b';

        var rowId = $(tabela).DataTable().column(coluna).search(searchValue, true, false).rows({ filter: 'applied' }).data();
        if (rowId.length > 0) { // ja tem
            if (selectedId != rowId[0][campoId]) {
                $(txt).css("background-color", corVermelho);

                swal({
                    type: 'error',
                    title: 'Aviso',
                    text: texto
                }).then(
                    function () {
                        $(tabela).DataTable().search('').columns().search('').draw();

                        return false;
                    });
            }
            else {
                $(tabela).DataTable().search('').columns().search('').draw();
                return true;
            }
        }
        else { // nao tem
            $('#txtcodigo').css("background-color", corBranca);
            $('#txtdescricao').css("background-color", corBranca);
            $(tabela).DataTable().search('').columns().search('').draw();
            return true;
        }
    //}
    //else {
    //    $("#modalSalvarRegistro").modal('show');
    //    return false;
    //}

}


function txtnome_onKeyUP(txt) {

    txt.style.backgroundColor = corBranca;

    validaAlfaNumerico(txt, 0);

    if (txt.value == "")
        txt.style.backgroundColor = corBranca;
}

function txtdescricao_onKeyUP(txt) {
    txt.style.backgroundColor = corBranca;
  //  validaAlfaNumerico(txt, 0);

    if (txt.value == "")
        txt.style.backgroundColor = corBranca;
}



// montagem do gridview
$(document).ready(function () {


    // ****************************GRID tblObjClasses *****************************************************************************
    $('#tblObjClasses').DataTable({
        "ajax": {
            "url": "/Objeto/ObjClasse_ListAll",
            "type": "GET",
            "datatype": "json"
        }
        , "columns": [
            { data: "clo_id", "width": "30px", "className": "hide_column", "searchable": false },
            { data: "clo_nome", "autoWidth": true, "searchable": true },
            { data: "clo_descricao", "autoWidth": true, "searchable": true },
            {
                "title": "Opções",
                data: "clo_id",
                "searchable": false,
                "sortable": false,
                "render": function (data, type, row) {
                    var retorno = "";
                    if (permissaoEscrita > 0) {
                        retorno = '<a href="#" onclick="return Editar(2,' + data + ')" title="Editar" ><span class="glyphicon glyphicon-pencil"></span></a>' + '  ';

                        if (row.clo_ativo == 1)
                            retorno += '<a href="#" onclick="return AtivarDesativar(2,' + data + ', 0)" title="Ativo" ><span class="glyphicon glyphicon-ok text-success"></span></a>' + '  ';
                        else
                            retorno += '<a href="#" onclick="return AtivarDesativar(2,' + data + ', 1)" title="Desativado" ><span class="glyphicon glyphicon-remove text-danger"></span></a>' + '  ';
                    }
                    else {
                        retorno = '<span class="glyphicon glyphicon-pencil desabilitado"  ></span>' + '  ';

                        if (row.clo_ativo == 1)
                            retorno += '<span class="glyphicon glyphicon-ok text-success desabilitado" ></span>' + '  ';
                        else
                            retorno += '<span class="glyphicon glyphicon-remove text-danger desabilitado"  ></span>' + '  ';

                    }

                    if (permissaoExclusao > 0)
                        retorno += '<a href="#" onclick="return Deletar(2,' + data + ')" title="Excluir" ><span class="glyphicon glyphicon-trash"></span></a>';
                    else
                        retorno += '<span class="glyphicon glyphicon-trash desabilitado" title="Excluir" ></span>';

                    return retorno;
                }
            }
        ]
        , "rowId": "clo_id"
        , "rowCallback": function (row, data) {
            if (data.clo_id == selectedId_ObjClasse)
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

    var tblObjClasses = $('#tblObjClasses').DataTable();
    $('#tblObjClasses tbody').on('click', 'tr', function () {
        if ($(this).hasClass('selected')) {
            //$(this).removeClass('selected');
        }
        else {
            tblObjClasses.$('tr.selected').removeClass('selected');
            $(this).addClass('selected');
        }

        var clo_id = tblObjClasses.row(this).data();
        $('#hddnSelectedclo_id').val(clo_id["clo_id"]);
        selectedId_ObjClasse = clo_id["clo_id"];

        document.getElementById('HeaderObjTipos').innerText = "Tipos da Classe: " + clo_id["clo_nome"];

        $('#tblObjTipos').DataTable().ajax.reload();
        document.getElementById('subGrids').style.visibility = "visible";


        // oculta/mostra coluna TIpPAI
        var table = $('#tblObjTipos').DataTable();
        var column = table.column(5);
        column.visible(false);

        switch(selectedId_ObjClasse)
        {
            case 1: case 2: case 3: case 6: case 10: case 11: column.visible(false); break;
                default:column.visible(true);
        }

        

    });



    // ****************************GRID tblObjTipos *****************************************************************************
    $('#tblObjTipos').DataTable({
        "ajax": {
            "url": "/Objeto/ObjTipo_ListAll",
            "data": function (d) {
                d.clo_id = $('#hddnSelectedclo_id').val();
            },
            "type": "GET",
            "datatype": "json"
        }
        , "columns": [
            { data: "tip_id", "className": "hide_column", "searchable": false },
            { data: "clo_id", "className": "hide_column", "searchable": false },
            { data: "tip_codigo", "width": "50px", "searchable": true },
            { data: "tip_nome", "width": "150px", "searchable": true },
            { data: "tip_descricao", "autoWidth": true, "searchable": true },
            { data: "tip_pai_nome", "autoWidth": true, "searchable": true },
            { data: "tip_pai", "className": "hide_column" },
            {
                "title": "Opções",
                data: "tip_id",
                "searchable": false,
                "sortable": false,
                "render": function (data, type, row) {
                    var retorno = "";
                    if (permissaoEscrita > 0) {
                        retorno = '<a href="#" onclick="return Editar(1,' + data + ')" title="Editar" ><span class="glyphicon glyphicon-pencil"></span></a>' + '  ';

                        if (row.tip_ativo == 1)
                            retorno += '<a href="#" onclick="return AtivarDesativar(1,' + data + ', 0)" title="Ativo" ><span class="glyphicon glyphicon-ok text-success"></span></a>' + '  ';
                        else
                            retorno += '<a href="#" onclick="return AtivarDesativar(1,' + data + ', 1)" title="Desativado" ><span class="glyphicon glyphicon-remove text-danger"></span></a>' + '  ';
                    }
                    else {
                        retorno = '<span class="glyphicon glyphicon-pencil desabilitado"  ></span>' + '  ';

                        if (row.tip_ativo == 1)
                            retorno += '<span class="glyphicon glyphicon-ok text-success desabilitado" ></span>' + '  ';
                        else
                            retorno += '<span class="glyphicon glyphicon-remove text-danger desabilitado"  ></span>' + '  ';

                    }

                    if (permissaoExclusao > 0)
                        retorno += '<a href="#" onclick="return Deletar(1,' + data + ')" title="Excluir" ><span class="glyphicon glyphicon-trash"></span></a>';
                    else
                        retorno += '<span class="glyphicon glyphicon-trash desabilitado" title="Excluir" ></span>';

                    return retorno;
                }
            }
        ]
        , "rowId": "tip_id"
        , "rowCallback": function (row, data) {
            if (data.tip_id == selectedId_ObjTipo)
                $(row).addClass('selected');
        }
        , "lengthMenu": [[10, 25, 50, 100], [10, 25, 50, 100]]
        , select: {
            style: 'single'
        }
        , "order": [2, "asc"]
        , searching: true
        , "oLanguage": idioma
        , "pagingType": "input"
        , "sDom": '<"top">rt<"bottom"pfli><"clear">'
    });

    var tblObjTipos = $('#tblObjTipos').DataTable();
    $('#tblObjTipos tbody').on('click', 'tr', function () {
        if ($(this).hasClass('selected')) {
            $(this).removeClass('selected');
        }
        else {
            tblObjTipos.$('tr.selected').removeClass('selected');
            $(this).addClass('selected');
        }

        var tip_id = tblObjTipos.row(this).data();
        $('#hddnSelectedtip_id').val(tip_id["tip_id"]);
        selectedId_ObjTipo = tip_id["tip_id"];
    });

});
