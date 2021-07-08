
function OpenComboModal() {
    $('#modalSalvarRegistro').modal('show');
}


// montagem do gridview
$(document).ready(function () {
    carregaGrid ();
});


// ****************************GRID tblPoliticaConserva + combos de Filtro *****************************************************************************
function carregaGrid() {

    $.ajax({
        "url": "/Conserva/PoliticaConserva_ListAll",
        "type": "GET",
        "data": {
            cot_id:$('#ComboConserva').val() == null ? 0 : $('#ComboConserva').val(),
            tip_nome: $('#ComboGrupo').val() == null ? "" : $('#ComboGrupo').val(),
            cov_nome: $('#ComboVariavel').val() == null ? "" : $('#ComboVariavel').val()
        },
        dataType: "JSON",
        success: function (data) {

                // preenche o grid
                $('#tblPoliticaConserva').DataTable().destroy();
                $('#tblPoliticaConserva').DataTable({
                    "data": data.dtMain,
                    "columns": [
                    { data: "cop_id", "className": "hide_column"},
                    { data: "ogi_id_caracterizacao_situacao", "className": "hide_column" },
                    { data: "cot_id", "className": "hide_column" },
                    { data: "cov_id", "className": "hide_column" },
                    { data: "cot_descricao", "autoWidth": true, "searchable": true },
                    { data: "tip_nome", "autoWidth": true, "searchable": true },
                    { data: "cov_nome", "autoWidth": true, "searchable": true },
                    { data: "ogi_item", "autoWidth": true, "searchable": true },
                    {
                        "title": "Opções",
                        data: "cop_id",
                        "searchable": false,
                        "sortable": false,
                        "render": function (data, type, row) {
                            var retorno = "";
                            //if (permissaoEscrita > 0) {
                            //    var apostrofo = "'";
                            //    var params = row["cop_id"] + ',' + row["cot_id"] + ',' + apostrofo + row["tip_nome"] + apostrofo + ',' + row["cov_id"] + ',' + row["ogi_id_caracterizacao_situacao"];
                            //    retorno = '<a href="#" onclick="return PoliticaConserva_Editar(' + params + ')" title="Editar" ><span class="glyphicon glyphicon-pencil"></span></a>' + '  ';
                            //}
                            //else {
                            //    retorno = '<span class="glyphicon glyphicon-pencil desabilitado"  ></span>' + '  ';
                            //}

                            if (permissaoExclusao > 0)
                                retorno += '<a href="#" onclick="return PoliticaConserva_Excluir(' + data + ')" title="Excluir" ><span class="glyphicon glyphicon-trash"></span></a>';
                            else
                                retorno += '<span class="glyphicon glyphicon-trash desabilitado" title="Excluir" ></span>';

                            return retorno;
                        }
                    }
                ]
                , "rowId": "cop_id"
                , "lengthMenu": [[10, 25, 50, 100], [10, 25, 50, 100]]
                , select: {
                    style: 'single'
                }
                , "order": [ 1, "asc" ]
                , searching: true
                , "oLanguage": idioma
                , "pagingType": "input"
                , "sDom": '<"top">rt<"bottom"pfli><"clear">'
            });

            // preenche o combos de filtro
                PoliticaConserva_preencheCombo("ComboConserva", data.dtConserva, $('#ComboConserva').val());
                PoliticaConserva_preencheCombo("ComboGrupo", data.dtGrupo, $('#ComboGrupo').val());
                PoliticaConserva_preencheCombo("ComboVariavel", data.dtVariavel, $('#ComboVariavel').val());
                PoliticaConserva_preencheCombo("divAlerta2", data.dtAlerta, "");
        }
    });


}

function PoliticaConserva_preencheCombo(qualCombo, lstSubNiveis, valorSelecionado)
{
    var cmb = $("#" + qualCombo);

    // limpa os itens existentes
    if (qualCombo.indexOf("div") >= 0) {
        cmb.empty();
    }
    else {
        cmb.html("");
        cmb.append($('<option selected ></option>').val("").html("-- Selecione --")); // 1o item vazio
    }

    
    $.each(lstSubNiveis, function (i, subNivel) {
        if (qualCombo.indexOf("Combo") >= 0) {
            cmb.append($('<option></option>').val(subNivel.Value.trim()).html(subNivel.Text.trim()));
        }
        else
            if (qualCombo.indexOf("div") >= 0) {
                var tagchk = '<input type="checkbox" id="idXXX" nome="nameXXX" value="valueXXX" style="margin-right:5px">';
                tagchk = tagchk.replace("idXXX", "chk" + i);
                tagchk = tagchk.replace("nameXXX", "chk" + i);
                tagchk = tagchk.replace("valueXXX", subNivel.Value.trim());

                var taglbl = '<label for="idXXX" class="chklst" >TextoXXX</label> <br />';
                taglbl = taglbl.replace("idXXX", "chk" + i);
                taglbl = taglbl.replace("TextoXXX", subNivel.Text.trim());
                cmb.append(tagchk + taglbl);
            }
    });

    if (lstSubNiveis.length == 1)
        document.getElementById(qualCombo).selectedIndex = 1;
    else
        cmb.val(valorSelecionado);

}


// ******************** filtros ***************************************

function btnLimpar_onclick() {
    $("#ComboConserva").val("");
    $("#ComboGrupo").val("");
    $("#ComboVariavel").val("");
    carregaGrid();
}

function btnPesquisar_onclick()
{
    carregaGrid ();
}

// ******************** acoes  ***************************************

function PoliticaConserva_Excluir(id)
{
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
            var response = POST("/Conserva/PoliticaConserva_Excluir", JSON.stringify({ cop_id: id }))
            if (response.erroId >= 1) {
                swal({
                    type: 'success',
                    title: 'Sucesso',
                    text: 'Registro excluído com sucesso'
                });

                $('#tblPoliticaConserva').DataTable().ajax.reload(null, false);
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

function PoliticaConserva_Inserir() {

    if ($('#ComboGrupo_ad').val() == null) {
        swal({
            type: 'error',
            title: 'Aviso',
            text: 'Selecione Grupo'
        }).then(
                     function () {
                         return false;
                     });
        return false;
    }

    // cria lista lst_cov_descricao
    var lst_cov_descricao = '';
    $('#divVariavel2 input:checked').each(function () {
        lst_cov_descricao = lst_cov_descricao + ';' + $(this).parent().text().trim();
    });
    lst_cov_descricao = lst_cov_descricao.substr(1, lst_cov_descricao.length);

    if (lst_cov_descricao == '') {
        swal({
            type: 'error',
            title: 'Aviso',
            text: 'Selecione Variável'
        }).then(
             function () {
                 return false;
             });
        return false;
    }


    // cria lista lst_ogi_id_caracterizacao_situacao
    var lst_ogi_id_caracterizacao_situacao = '';
    $('#divAlerta2 input:checked').each(function () {
        lst_ogi_id_caracterizacao_situacao = lst_ogi_id_caracterizacao_situacao + ';' + $(this).attr('value');
    });
    lst_ogi_id_caracterizacao_situacao = lst_ogi_id_caracterizacao_situacao.substr(1, lst_ogi_id_caracterizacao_situacao.length);

    if (lst_ogi_id_caracterizacao_situacao == '') {
        swal({
            type: 'error',
            title: 'Aviso',
            text: 'Selecione Alerta'
        }).then(
             function () {
                 return false;
             });
        return false;
    }

    if ($('#ComboConserva2').val() == null) {
            swal({
                type: 'error',
                title: 'Aviso',
                text: 'Selecione a Conserva'
            }).then(
                 function () {
                     return false;
                 });
            return false;
        }



    var params = {
        tip_nome: $('#ComboGrupo_ad').val(),
        lst_ogi_id_caracterizacao_situacao: lst_ogi_id_caracterizacao_situacao,
        lst_cov_descricao: lst_cov_descricao,
        cot_id: $('#ComboConserva2').val()
    };

        $.ajax({
            url: "/Conserva/PoliticaConserva_Inserir",
            data: JSON.stringify(params),
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {

                $("#modalSalvarRegistro").modal('hide');
                $('#tblPoliticaConserva').DataTable().ajax.reload(null, false);  //false = sem reload na pagina.

                return false;
            },
            error: function (errormessage) {
                swal({
                    type: 'error',
                    title: 'Aviso',
                    text: errormessage.responseText
                });

                return false;
            }
        });


    return false;
}

// ******************** eventos  ***************************************

function ComboGrupo_ad_onchange(quem)
{
    $.ajax({
        url: "/Conserva/PreenchecmbVariavel_tip_nome",
        data: JSON.stringify({ "tip_nome": $("#ComboGrupo_ad").val() }),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {

            PoliticaConserva_preencheCombo("divVariavel2", result, "");
            return false;
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
            return false;
        }
    });
}







