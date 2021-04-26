 var corBranca = "rgb(255, 255, 255)";
var selectedId_ObjAtributo = -1;
var selectedId_clo_id = -1;
var selectedId_tip_id = -1;
var selectedatr_apresentacao_itens = '';

var filtro_clo_id = -1;
var filtro_tip_id = -1;
var filtro_codigo = '';
var filtro_descricao = '';

var selectedId_ObjAtributoItem = -1;
var ajustarLargura = 1;

var moduloId = parseInt(window.location.href.split('/').pop());
var ehAtributoFuncional = moduloId == 130 ? 1 : 0;

var ehInsercao = 0;

function cmbClassesObjeto_onchange(quem) {
    cmbClassesObjeto.style.backgroundColor = corBranca;

    $("#cmbFiltroTiposObjeto").html(""); // clear before appending new list
    $("#cmbTiposObjeto").html(""); // clear before appending new list

    var selectedVal = quem.options[quem.selectedIndex].value;
    selectedId_clo_id = selectedVal;
    selectedId_tip_id = -1;

    $.ajax({
        url: '/Objeto/PreenchecmbTiposObjeto',
        type: "POST",
        dataType: "JSON",
        data: { clo_id: selectedVal },
        success: function (lstSubNiveis) {

            $("#cmbTiposObjeto").append($('<option selected ></option>').val(-1).html("-- Selecione --")); // 1o item vazio
            $("#cmbFiltroTiposObjeto").append($('<option selected ></option>').val(-1).html("-- Selecione --")); // 1o item vazio
            $.each(lstSubNiveis, function (i, subNivel) {
                $("#cmbTiposObjeto").append($('<option></option>').val(subNivel.Value.substring(0, subNivel.Value.indexOf(":"))).html(subNivel.Text));
                $("#cmbFiltroTiposObjeto").append($('<option></option>').val(subNivel.Value.substring(0, subNivel.Value.indexOf(":"))).html(subNivel.Text));
            });

        }
    });


}

function cmbApresentacaoItens_onchange(quem) {
    cmbClassesObjeto.style.backgroundColor = corBranca;

    document.getElementById("divConfiguracoes").style.display = 'none';

    if (quem.selectedIndex > 0) {
        document.getElementById("divMascaraTexto").style.display = 'none';
        document.getElementById("btnInserirItem").disabled = false;

      //  if ((permissaoConfiguracaoAtributosLeitura > 0) && (ehInsercao == 0))
        if (permissaoConfiguracaoAtributosLeitura > 0)

            document.getElementById("divConfiguracoes").style.display = 'block';
    }
    else {
        document.getElementById("divMascaraTexto").style.display = 'block';
        document.getElementById("btnInserirItem").disabled = true;
    }

}

function posicionaLinha(tabela, id_linha, coluna_do_id)
{
    var dttable = $('#' + tabela).DataTable();

    var coluna_do_id = 0; // indice da coluna que contem o id

    // vai para a pagina
    var pos = dttable.column(coluna_do_id, { order: 'current' }).data().indexOf(id_linha); 
    if (pos >= 0) {
        var page = Math.floor(pos / dttable.page.info().length); 
        dttable.page(page).draw(false);
    }

    // remove selecao
    dttable.$('tr.selected').removeClass('selected');

    // seleciona a linha
    var row = document.getElementById(id_linha);
    if (row)
      row.className += " selected";

}

function Inserir() {
    ehInsercao = 1;
    document.getElementById("divConfiguracoes").style.display = 'none';

    var corBranca = "rgb(255, 255, 255)";
    $('#txtcodigo').css("background-color", corBranca);
    $('#txtdescricao').css("background-color", corBranca);
    $('#cmbClassesObjeto').css("background-color", corBranca);
    $('#cmbTiposObjeto').css("background-color", corBranca);

    $("#cmbClassesObjeto").val(0);
    $("#cmbTiposObjeto").val(0);

    $('#txt_id').val("");
    $('#txtcodigo').val("");
    $('#txtdescricao').val("");
    $('#txtatr_mascara_texto').val("");
    $('#chkativo').prop('checked', true);
    $('#chkherdavel').prop('checked', true);

    $('#rd_atr_valor_int').prop('checked', true);
    $('#rd_atr_valor_real').prop('checked', false);
    $('#rd_atr_valor_str').prop('checked', false);

    $('#chkativo').css('border-color', 'lightgrey');
    $('#chkherdavel').css('border-color', 'lightgrey');

    $("#cmbApresentacaoItens").val("-1");
    selectedId_ObjAtributo = -1;
    $('#tblAtributosItens').DataTable().ajax.reload();  //false = sem reload na pagina.


    $("#modalSalvarRegistro").modal('show');

    document.getElementById("lblModalHeader").innerText = "Novo Atributo";
    selectedId_ObjAtributo = -1;
  //  document.getElementById("divItens").style.display = "none";
    document.getElementById("tblAtributosItens").disabled = true;
    document.getElementById("btnInserirItem").disabled = true;

}


function Salvar() {

    var cmbClassesObjeto = document.getElementById('cmbClassesObjeto');
    var txtcodigo = document.getElementById('txtcodigo');
    var txtdescricao = document.getElementById('txtdescricao');


    // checa campos vazios
    if (cmbClassesObjeto.selectedIndex <= 0) {
        cmbClassesObjeto.style.backgroundColor = corVermelho;
        swal({
            type: 'error',
            title: 'Aviso',
            text: 'Classe é obrigatória'
        }).then(
            function () {
                return false;
            });
        return false;
    }
    else
        if (txtcodigo.value.trim() == "") {
            txtcodigo.style.backgroundColor = corVermelho;
            swal({
                type: 'error',
                title: 'Aviso',
                text: 'O Nome é obrigatório'
            }).then(
                function () {
                    return false;
                });
            return false;
        }
        else
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



    var selectedClasseVal = $('#cmbClassesObjeto').val();
    selectedId_clo_id = selectedClasseVal;

    var cmbTiposObjeto = document.getElementById('cmbTiposObjeto');
    if (cmbTiposObjeto.selectedIndex > 0) {
        var selectedTipoVal = $('#cmbTiposObjeto').val();
        if (selectedTipoVal.includes(':'))
            selectedId_tip_id = parseInt(selectedTipoVal.substring(0, selectedTipoVal.indexOf(":")));
        else
            selectedId_tip_id = parseInt(selectedTipoVal);
    }
    else
        selectedId_tip_id = -1;


    var selectedatr_apresentacao_itens = "";
    switch ($("#cmbApresentacaoItens").val()) {
        case "-1": selectedatr_apresentacao_itens = ""; break;
        case "0": selectedatr_apresentacao_itens = "combobox"; break;
        case "1": selectedatr_apresentacao_itens = "checkbox"; break;
    }


        var param = {
            "atr_id": -1,
            "filtro_codigo": txtcodigo.value.trim(),
            "filtro_descricao": '',
            "filtro_clo_id": selectedId_clo_id,
            "filtro_tip_id": -1,
            "ehAtributoFuncional": ehAtributoFuncional
        };

        $.ajax({
            url: "/Objeto/ObjAtributo_ListAll",
            data: param,
            type: "GET",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                var tem = 0
                if (result.data.length == 0) // nao tem repetido
                    tem = 0;
                else {
                    // checa se o campo txtcodigo está repetido (pode ser que tenha parecido pois é usado like% na procedure)
                    for (j = 0; j < result.data.length; j++)
                        if ((result.data[j].atr_atributo_nome.trim() == txtcodigo.value.trim()) && ($('#txt_id').val() != result.data[j].atr_id)) 
                            tem = 1;                    
                }

                if (tem == 0) // entao pode salvar
                {
                    var param = {
                        clo_id: selectedId_clo_id,
                        tip_id: (selectedId_tip_id == null ? -1 : selectedId_tip_id),

                        atr_id: $('#txt_id').val(),
                        atr_atributo_nome: $('#txtcodigo').val(),
                        atr_descricao: $('#txtdescricao').val(),
                        atr_mascara_texto: $('#txtatr_mascara_texto').val(),
                        atr_ativo: $('#chkativo').prop('checked') ? 1 : 0,
                        atr_herdavel: $('#chkherdavel').prop('checked') ? 1 : 0,
                        atr_atributo_funcional: ehAtributoFuncional,
                        atr_apresentacao_itens: selectedatr_apresentacao_itens
                    };

                    $.ajax({
                        url: "/Objeto/ObjAtributo_Salvar",
                        data: JSON.stringify(param),
                        type: "POST",
                        contentType: "application/json;charset=utf-8",
                        dataType: "json",
                        success: function (result) {

                            if (parseInt(result) > 0) {
                                $('#tblAtributos').DataTable().ajax.reload(null, false);  //false = sem reload na pagina.

                                // espera 1 segundo para dar tempo do reload, nao funcionou colocando no "Null" do reload                            
                                setTimeout(function () { posicionaLinha('tblAtributos', result, 0); }, 1500);
                            }

                            //// apos salvar permite configurar os itens. Se o atributo for textbox, entao fecha
                            //if ((permissaoConfiguracaoAtributosLeitura > 0) && ($("#cmbApresentacaoItens").val() != "-1") && (ehInsercao == 1)) {
                            //    document.getElementById("divConfiguracoes").style.display = 'block';
                                selectedId_ObjAtributo = result;
                                $('#txt_id').val(result);
                            //}
                            //else {
                            //    document.getElementById("divConfiguracoes").style.display = 'none';
                                $("#modalSalvarRegistro").modal('hide');
                            //}
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
                        text: 'Atributo já cadastrado'
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

function Deletar(id) {
    var url;
    var tabela;
    tabela = '#tblAtributos';
    url = "/Objeto/ObjAtributo_Excluir";

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
            var response = POST(url, JSON.stringify({ id: id }))
            if (response.erroId >= 1) {
                swal({
                    type: 'success',
                    title: 'Sucesso',
                    text: 'Registro excluído com sucesso'
                });

                $(tabela).DataTable().ajax.reload();
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

function AtivarDesativar(id, ativar) {

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

                var url = "/Objeto/ObjAtributo_AtivarDesativar";
                var response = POST(url, JSON.stringify({ id: id }))
                if (response.erroId == 1) {
                    swal({
                        type: 'success',
                        title: 'Sucesso',
                        text: ativar == 1 ? msgAtivacaoOK : msgDesativacaoOK
                    });

                    $('#tblAtributos').DataTable().ajax.reload();
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

function Editar(id) {
    ehInsercao = 0;

    var corBranca = "rgb(255, 255, 255)";
    $('#txtcodigo').css("background-color", corBranca);
    $('#txtdescricao').css("background-color", corBranca);

    $('#txtcodigo').css('border-color', 'lightgrey');
    $('#txtdescricao').css('border-color', 'lightgrey');


    document.getElementById("lblModalHeader").innerText = "Editar Atributo";

    selectedId_ObjAtributo = id;
    $('#hddnSelectedatr_id').val(id);

    var url = "/Objeto/ObjAtributo_GetbyID";

    $.ajax({
        url: url,
        "data": {
            "id": id,
            "ehAtributoFuncional": ehAtributoFuncional
        },
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            $('#txt_id').val(result.atr_id);
            $('#txtcodigo').val(result.atr_atributo_nome);
            $('#txtdescricao').val(result.atr_descricao);
            $('#txtatr_mascara_texto').val(result.atr_mascara_texto);

            $("#cmbClassesObjeto").val(result.clo_id);

            $('#chkativo').prop('checked', (result.atr_ativo == '1' ? true : false));

            $("#cmbClassesObjeto").val(result.clo_id);
            selectedId_clo_id = result.clo_id;

            selectedatr_apresentacao_itens = result.atr_apresentacao_itens;
            switch (selectedatr_apresentacao_itens)
            {
                case "combobox": $("#cmbApresentacaoItens").val("0"); break;
                case "checkbox": $("#cmbApresentacaoItens").val("1"); break;
                default:
                    $("#cmbApresentacaoItens").val("-1"); break;
            }

            // preenche combo Tipos
            var cmbTiposSelValue = "";
            $("#cmbTiposObjeto").html("");
            $("#cmbTiposObjeto").append($('<option></option>').val(-1).html(" ")); // 1o item vazio
            $.each(result.lstTipos, function (i, subNivel) {
                $("#cmbTiposObjeto").append($('<option></option>').val(subNivel.Value).html(subNivel.Text));
                var aux = parseInt(subNivel.Value.substring(0, subNivel.Value.indexOf(":")));
                if (aux == parseInt(result.tip_id)) {
                    selectedId_tip_id = aux; //result.tip_id;
                    cmbTiposSelValue = subNivel.Value;
                }
            });

            $("#cmbTiposObjeto").val(cmbTiposSelValue);
            

            var cmbApresentacaoItens = document.getElementById("cmbApresentacaoItens");
            if (cmbApresentacaoItens.selectedIndex > 0) {
            //    document.getElementById("divMascaraTexto").style.visibility = 'hidden';
                document.getElementById("divMascaraTexto").style.display = 'none';

                document.getElementById("btnInserirItem").disabled = false;
            }
            else {
               // document.getElementById("divMascaraTexto").style.visibility = 'visible';
                document.getElementById("divMascaraTexto").style.display = 'block';
                document.getElementById("btnInserirItem").disabled = true;
            }


          //  document.getElementById("divItens").style.display = "block";
            document.getElementById("tblAtributosItens").disabled = false;

            // ****************************GRID tblAtributosItens *****************************************************************************
            selectedId_ObjAtributo = result.atr_id;
            $('#hddnSelectedatr_id').val(selectedId_ObjAtributo);
            $('#tblAtributosItens').DataTable().ajax.reload();  //false = sem reload na pagina.


            //*********************************************************************************************************************************
            ajustarLargura = 1;

            if ((permissaoConfiguracaoAtributosLeitura > 0) && ($("#cmbApresentacaoItens").val() != "-1"))
                document.getElementById("divConfiguracoes").style.display = 'block';
            else
                document.getElementById("divConfiguracoes").style.display = 'none';

            $("#modalSalvarRegistro").modal('show');
            return false;
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
    selectedId = selectedId_ObjAtributo;
    tabela = '#tblAtributos';
    campoId = "atr_id";
    texto = 'Atributo já cadastrado';

    if (checaVazio(txtBox)) {
        var corVermelho = "rgb(228, 88, 71)";
        var corBranca = "rgb(255, 255, 255)";
        var searchValue = '\\b' + txtBox.value + '\\b';

        var rowId = $(tabela).DataTable().column(1).search(searchValue, true, false).rows({ filter: 'applied' }).data();
        if (rowId.length > 0) { // ja tem
            if (selectedId != rowId[0][campoId]) {
                $('#txtdescricao').css("background-color", corVermelho);
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
            else { // nao tem
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
    }
    else {
        $("#modalSalvarRegistro").modal('show');
        return false;
    }

}

function LimparFiltro() {

    filtro_clo_id = -1;
    filtro_tip_id = -1;
    filtro_codigo = '';
    filtro_descricao = '';

    $('#txtFiltroCodigo').val('');
    $('#txtFiltroDescricao').val('');
    $("#cmbFiltroClassesObjeto").val(null);
    $("#cmbFiltroTiposObjeto").html(""); // apaga os itens existentes

    carregaGrid();

    return false;
}

function ExecutarFiltro() {

    filtro_clo_id = $("#cmbFiltroClassesObjeto").val() == "" ? -1 : $("#cmbFiltroClassesObjeto").val();
    filtro_tip_id = $("#cmbFiltroTiposObjeto").val();

    filtro_codigo = $('#txtFiltroCodigo').val().trim();
    filtro_descricao = $("#txtFiltroDescricao").val().trim();

    carregaGrid();
    return false;
}

function txtcodigo_onkeyup()
{
    $('#txtcodigo').css("background-color", corBranca);
}
function txtdescricao_onkeyup()
{
    $('#txtdescricao').css("background-color", corBranca);
}

function btnModalAtributoClose_onclick()
{
    $("#modalSalvarRegistro").modal('hide');
    if ((ehInsercao == 1) && (selectedId_ObjAtributo < 0))
    {
        //entao tem que apagar os registros temporarios 
        var tabela = '#tblAtributos';
        var url = "/Objeto/ObjAtributo_Excluir";
        var response = POST(url, JSON.stringify({ id: selectedId_ObjAtributo }))
                if (response.erroId != 1) {
                    swal({
                        type: 'error',
                        title: 'Aviso',
                        text: 'Erro ao excluir registro(s) temporário(s)'
                    });
                }
   }
}


// ************************************ ITENS DE ATRIBUTO  ********************************************************
function InserirItem() {
    var corBranca = "rgb(255, 255, 255)";
    $('#txtati_item').css("background-color", corBranca);

    $('#txt_ati_id').val("");
    $('#txtati_item').val("");
    $('#chkati_ativo').prop('checked', true);

    $('#chkati_ativo').css('border-color', 'lightgrey');
    $("#modalSalvarRegistroItem").modal('show');

    document.getElementById("lblModalHeaderItem").innerText = "Novo Item";
    selectedId_ObjAtributoItem = -1;
}

function SalvarItem() {
    var txtati_item = document.getElementById('txtati_item');
    if (txtati_item.value.trim() == "")
    {
        $('#txtati_item').css("background-color", corVermelho);
        swal({
            type: 'error',
            title: 'Aviso',
            text: 'O campo Item é obrigatório'
        }).then(
            function () {
                return false;
            });

        return false;
    }

    if (ChecaRepetidoItem(txtati_item))  {
        var param;
        var url;
        url = "/Objeto/ObjAtributoItem_Salvar";
        param = {
            ati_id: selectedId_ObjAtributoItem,
            atr_id: selectedId_ObjAtributo,
            ati_item: $('#txtati_item').val(),
            ati_ativo: $('#chkati_ativo').prop('checked') ? 1 : 0,
            atr_atributo_funcional: ehAtributoFuncional
        };

        $.ajax({
            url: url,
            data: JSON.stringify(param),
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {

                if (parseInt(result) < 0) {
                    $('#txt_id').val(parseInt(result));
                    selectedId_ObjAtributo = parseInt(result);
                }

                $("#modalSalvarRegistroItem").modal('hide');

                $('#tblAtributosItens').DataTable().ajax.reload();  //false = sem reload na pagina.
                $('#tblAtributos').DataTable().ajax.reload(null, false);  //false = sem reload na pagina.
                return false;
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
                return false;
            }
        });
    }
    else {

        $('#txtati_item').css("background-color", corVermelho);
        swal({
            type: 'error',
            title: 'Aviso',
            text: 'Item já cadastrado'
        }).then(
            function () {
                return false;
            });

     //   $("#modalSalvarRegistroItem").modal('show');

        return false;
    }
    return false;
}

function DeletarItem(id) {
    var url;
    var tabela;
    tabela = '#tblAtributosItens';
    url = "/Objeto/ObjAtributoItem_Excluir";

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
            var response = POST(url, JSON.stringify({ id: id }))
            if (response.erroId >= 1) {
                swal({
                    type: 'success',
                    title: 'Sucesso',
                    text: 'Registro excluído com sucesso'
                });

                $(tabela).DataTable().ajax.reload();
                $('#tblAtributos').DataTable().ajax.reload(null, false);  //false = sem reload na pagina.
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

function AtivarDesativarItem(id, ativar) {

    if (id >= 0) {
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
                var url = "/Objeto/ObjAtributoItem_AtivarDesativar";

                var response = POST(url, JSON.stringify({ id: id }))
                if (response.erroId == 1) {
                    swal({
                        type: 'success',
                        title: 'Sucesso',
                        text: ativar == 1 ? msgAtivacaoOK : msgDesativacaoOK
                    });

                    $('#tblAtributosItens').DataTable().ajax.reload(null, false);
                    $('#tblAtributos').DataTable().ajax.reload(null, false);  //false = sem reload na pagina.
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

function EditarItem(id) {
    var corBranca = "rgb(255, 255, 255)";
    $('#txtati_item').css("background-color", corBranca);

    $('#txtati_item').css('border-color', 'lightgrey');

    var url;
    document.getElementById("lblModalHeaderItem").innerText = "Editar Item";

    $.ajax({
        url: "/Objeto/ObjAtributoItem_GetbyID",
        data: { "ID": id, "atr_id": selectedId_ObjAtributo },
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            $('#txt_ati_id').val(result.ati_id);
            $('#txtati_item').val(result.ati_item);
            selectedId_ObjAtributoItem = result.ati_id;

            $('#chkati_ativo').prop('checked', (result.ati_ativo == '1' ? true : false));

            $("#modalSalvarRegistroItem").modal('show');
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
    return false;
}

function ChecaRepetidoItem(txtBox) {

    var selectedId;
    var tabela;
    var campoId;
    var texto;
    selectedId = selectedId_ObjAtributoItem;
    tabela = '#tblAtributosItens';
    campoId = "ati_id";
    texto = 'Item já cadastrado';

        if (txtBox.value.trim() != "") {
        var corVermelho = "rgb(228, 88, 71)";
        var corBranca = "rgb(255, 255, 255)";
        var searchValue = '\\b' + txtBox.value + '\\b';

        var rowId = $(tabela).DataTable().column(2).search(searchValue, true, false).rows({ filter: 'applied' }).data();
        if (rowId.length > 0) { // ja tem
            if (selectedId != rowId[0][campoId]) {
                $('#txtati_item').css("background-color", corVermelho);
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
            else { // nao tem
                $(tabela).DataTable().search('').columns().search('').draw();
                return true;
            }
        }
        else { // nao tem
            $('#txtati_item').css("background-color", corBranca);
            $(tabela).DataTable().search('').columns().search('').draw();
            return true;
        }
    }
    else {
        $("#modalSalvarRegistroItem").modal('show');
        return false;
    }

}

function txtati_item_onkeyup()
{
    var corBranca = "rgb(255, 255, 255)";
    $('#txtati_item').css("background-color", corBranca);
}

// ************************************ ********************** ********************************************************


function carregaGrid() {
    var classe = "";
    if (permissaoConfiguracaoAtributosLeitura > 0)
        classe = "";
    else
        classe = "hide_column";

    $('#tblAtributos').DataTable().destroy();
    $('#tblAtributos').DataTable({
        "ajax": {
            "url": "/Objeto/ObjAtributo_ListAll",
            "type": "GET",
            "datatype": "json",
            "data": {
                "atr_id": -1,
                "filtro_codigo": filtro_codigo,
                "filtro_descricao": filtro_descricao,
                "filtro_clo_id": filtro_clo_id,
                "filtro_tip_id": filtro_tip_id == null ? -1 : filtro_tip_id,
                "ehAtributoFuncional": ehAtributoFuncional
            }
        }
        ,"columns": [
            { data: "atr_id", "className": "hide_column", "searchable": false },
            { data: "clo_id", "className": "hide_column", "searchable": true },
            { data: "tip_id", "className": "hide_column", "searchable": true },
            { data: "clo_nome", tooltip: "clo_descricao", "width": "80px", "searchable": true },
            { data: "tip_nome", tooltip: "tip_descricao", "autoWidth": true, "searchable": true },
            { data: "atr_atributo_nome", "autoWidth": true, "searchable": true },
            { data: "atr_descricao", "autoWidth": true, "searchable": true },

            { data: "atr_itens_codigo", "autoWidth": true, "searchable": false, "sortable": true, "className": classe },
            {
                data: "atr_apresentacao_itens", "autoWidth": true, "searchable": false, "sortable": true, "className": classe,
                "render": function (data, type, row)
                {
                    if (data == "")
                        return "textbox";
                    else
                        return data;
                }
            },
            { data: "atr_mascara_texto", "autoWidth": true, "searchable": false, "sortable": true, "className": classe },


            {
                data: "atr_id",
                "searchable": false,
                "sortable": false,
                "render": function (data, type, row) {
                    var retorno = "";
                    if (permissaoEscrita > 0) {
                        retorno = '<a href="#" onclick="return Editar(' + data + ')" title="Editar" ><span class="glyphicon glyphicon-pencil"></span></a>' + '  ';

                        if (row.atr_ativo == 1)
                            retorno += '<a href="#" onclick="return AtivarDesativar(' + data + ', 0)" title="Ativo" ><span class="glyphicon glyphicon-ok text-success"></span></a>' + '  ';
                        else
                            retorno += '<a href="#" onclick="return AtivarDesativar(' + data + ', 1)" title="Desativado" ><span class="glyphicon glyphicon-remove text-danger"></span></a>' + '  ';
                    }
                    else {
                        retorno = '<span class="glyphicon glyphicon-pencil desabilitado"  ></span>' + '  ';

                        if (row.atr_ativo == 1)
                            retorno += '<span class="glyphicon glyphicon-ok text-success desabilitado" ></span>' + '  ';
                        else
                            retorno += '<span class="glyphicon glyphicon-remove text-danger desabilitado"  ></span>' + '  ';

                    }

                    if (permissaoExclusao > 0)
                        retorno += '<a href="#" onclick="return Deletar(' + data + ')" title="Excluir" ><span class="glyphicon glyphicon-trash"></span></a>';
                    else
                        retorno += '<span class="glyphicon glyphicon-trash desabilitado" title="Excluir" ></span>';

                    return retorno;
                }
            }
        ]
        , 'columnDefs': [
            {
                targets: [8] // atr_itens_codigo
                , "createdCell": function (td, cellData, rowData, row, col) {
                    $(td).attr('title', rowData["atr_itens_descricao"]);
                }
            }
        ]
        , "rowId": "atr_id"
        , "rowCallback": function (row, data) {
            if (data.atr_id == selectedId_ObjAtributo)
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


}

// montagem do gridview
$(document).ready(function () {

    moduloId = parseInt(window.location.href.split('/').pop());
    ehAtributoFuncional = moduloId == 130 ? 1 : 0;

    if (permissaoConfiguracaoAtributosLeitura > 0)
        document.getElementById("divConfiguracoes").style.display = 'block';
    else
        document.getElementById("divConfiguracoes").style.display = 'none';

    // ****************************GRID tblAtributos *****************************************************************************
    carregaGrid();

    var tblAtributos = $('#tblAtributos').DataTable();
    $('#tblAtributos tbody').on('click', 'tr', function () {

        // remove a classe "selected" de todas as linhas da tabela
        var els = document.getElementById("tblAtributos").getElementsByClassName("selected");
        for (var i = 0; i < els.length; i++)
            els[i].classList.remove('selected');

        if ($(this).hasClass('selected')) {
            $(this).removeClass('selected');
        }
        else {
            tblAtributos.$('tr.selected').removeClass('selected');
            $(this).addClass('selected');
        }

        var atr_id = parseInt(this.cells[0].innerText); //tblAtributos.row(this).data();
        $('#hddnSelectedatr_id').val(atr_id["atr_id"]);
        selectedId_ObjAtributo = atr_id["atr_id"];

    });


    // ****************************GRID tblAtributosItens *****************************************************************************

    $('#tblAtributosItens').DataTable({
        "ajax": {
            "url": "/Objeto/ObjAtributoItem_ListAll",
            "data": function (d) {
                d.atr_id = selectedId_ObjAtributo;
            },
            "type": "GET",
            "datatype": "json"
        }
        , "columns": [
            { data: "ati_id", "className": "hide_column", "searchable": false },
            { data: "atr_id", "className": "hide_column", "searchable": false },
            { data: "ati_item", "autoWidth": true, "searchable": true },
            {
                data: "ati_id",
                "autoWidth": true,
                "searchable": false,
                "sortable": false,
                "render": function (data, type, row) {
                    var retorno = "";
                    if (permissaoConfiguracaoAtributosEscrita > 0) {
                        retorno = '<a href="#" onclick="return EditarItem(' + data + ')" title="Editar" ><span class="glyphicon glyphicon-pencil"></span></a>' + '  ';

                        if (row.ati_ativo == 1)
                            retorno += '<a href="#" onclick="return AtivarDesativarItem(' + data + ', 0)" title="Ativo" ><span class="glyphicon glyphicon-ok text-success"></span></a>' + '  ';
                        else
                            retorno += '<a href="#" onclick="return AtivarDesativarItem(' + data + ', 1)" title="Desativado" ><span class="glyphicon glyphicon-remove text-danger"></span></a>' + '  ';
                    }
                    else {
                        retorno = '<span class="glyphicon glyphicon-pencil desabilitado"  ></span>' + '  ';

                        if (row.ati_ativo == 1)
                            retorno += '<span class="glyphicon glyphicon-ok text-success desabilitado" ></span>' + '  ';
                        else
                            retorno += '<span class="glyphicon glyphicon-remove text-danger desabilitado"  ></span>' + '  ';

                    }

                    if (permissaoConfiguracaoAtributosExclusao > 0)
                        retorno += '<a href="#" onclick="return DeletarItem(' + data + ')" title="Excluir" ><span class="glyphicon glyphicon-trash"></span></a>';
                    else
                        retorno += '<span class="glyphicon glyphicon-trash desabilitado" title="Excluir" ></span>';

                    return retorno;
                }
            }
        ]
        //  , "order": [2, "asc"]
        , "scrollY": "180px"
        , "scrollCollapse": true
        , "rowId": "ati_id"
        , "rowCallback": function (row, data) {
            if (data.ati_id == selectedId_ObjAtributoItem)
                $(row).addClass('selected');
        }
        , select: { style: 'single' }
        , searching: true
        , "oLanguage": idioma
        , "paging": false
        , "sDom": '<"top">rt<"bottom"pfli><"clear">'
    });

    var tblAtributosItens = $('#tblAtributosItens').DataTable();
    $('#tblAtributosItens tbody').on('click', 'tr', function () {

        if ($(this).hasClass('selected')) {
            $(this).removeClass('selected');
        }
        else {
            tblAtributosItens.$('tr.selected').removeClass('selected');
            $(this).addClass('selected');
        }

        selectedId_ObjAtributoItem = this.cells[0].innerText;
        $('#hddnSelectedatr_atributo_fixo_Item_id').val(selectedId_ObjAtributoItem);

        //var ati_id = tblAtributosItens.row(this).data();
        //$('#hddnSelectedatr_atributo_fixo_Item_id').val(ati_id["ati_id"]);
        //selectedId_ObjAtributoItem = ati_id["ati_id"];
    });




}); // document.ready

