
var tamanhoPagina = 10;

var dados = [];


//  var controlesReadOnlyFicha2 = ["txt_atr_id_13", "txt_atr_id_102", "txt_atr_id_105", "txt_atr_id_106", "txt_atr_id_107", "cmb_atr_id_130", "cmb_atr_id_131",
//"cmb_atr_id_1020", "cmb_atr_id_1084", "cmb_atr_id_1085", "cmb_atr_id_1087", "cmb_atr_id_1088", "cmb_atr_id_1089", "cmb_atr_id_1091", "cmb_atr_id_1092", "cmb_atr_id_1093", "cmb_atr_id_1094",
var controlesReadOnlyFicha2 = ["txt_atr_id_13", "txt_atr_id_102", "txt_atr_id_106", "cmb_atr_id_130", "cmb_atr_id_131", "cmb_atr_id_135", "cmb_atr_id_136", "cmb_atr_id_137", "cmb_atr_id_138", "cmb_atr_id_139", "cmb_atr_id_140", "cmb_atr_id_141", "cmb_atr_id_142", "cmb_atr_id_143", "cmb_atr_id_144", "txt_atr_id_151", "txt_atr_id_152", "txt_atr_id_153"
    //,"txt_historico_Pontuacao_Geral_OAE_1", "txt_historico_documento_2", "txt_historico_data_2", "txt_historico_executantes_2", "txt_historico_Pontuacao_Geral_OAE_2", "txt_historico_documento_3", "txt_historico_data_3", "txt_historico_executantes_3", "txt_historico_Pontuacao_Geral_OAE_3"
];

var controlesExcecoes_Salvar = ["cmb_atr_id_130", "cmb_atr_id_131", "cmb_atr_id_135", "cmb_atr_id_136", "cmb_atr_id_137", "cmb_atr_id_138", "cmb_atr_id_139", "cmb_atr_id_140", "cmb_atr_id_141", "cmb_atr_id_142", "cmb_atr_id_143", "cmb_atr_id_144", "cmb_atr_id_148", "cmb_atr_id_150", "txt_atr_id_151", "txt_atr_id_152", "txt_atr_id_153", "txt_atr_id_157"];

GetAll();

function preenchecmbTiposObjeto_FichaInspecaoRotineira() {
    $("#cmbTiposObjeto_FichaInspecaoRotineira").html(""); // limpa os itens existentes

    $.ajax({
        url: '/Objeto/PreenchecmbTiposObjeto',
        type: "POST",
        dataType: "JSON",
        data: { clo_id: 0, tip_pai: 0, excluir_existentes: 0, obj_id: 0 },
        success: function (lstSubNiveis) {
            $("#cmbTiposObjeto_FichaInspecaoRotineira").append($('<option selected disabled></option>').val(-1).html("--Selecione--")); // 1o item vazio
            $.each(lstSubNiveis, function (i, subNivel) {
                $("#cmbTiposObjeto_FichaInspecaoRotineira").append($('<option></option>').val(subNivel.Value).html(subNivel.Text));
            });

        }
    });
}

function getTipoId(aux) {
    return parseInt(aux.substring(0, aux.indexOf(":")));
}
function getTipoCodigo(aux) {
    return mascara = aux.substring(aux.indexOf(":") + 1, 150);
}

function cmb_situacao_onchange(quem) {
    var valor = quem.value;
    var lbl_id = quem.id.replace("cmb_situacao", "lbl_servico");
    var lbl = document.getElementById(lbl_id);
    lbl.innerText = "";

    var cmb2_id = quem.id.replace("cmb_situacao", "cmb_tpu_descricao_itens");
    var cmb2 = document.getElementById(cmb2_id);
    if (cmb2) {
        if ((parseInt(valor) <= 3) && (cmb2.options.length >= parseInt(valor))) {
            cmb2.value = valor;
            lbl.innerText = cmb2.options[cmb2.selectedIndex].text;
        }
    }
}

function Ficha2_ExcluirSubDivisao2(tip_id) {
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
            var response = POST("/Objeto/Objeto_Subdivisao2_Excluir", JSON.stringify({ tip_id: tip_id, obj_id_tipoOAE: selectedId_obj_id }))
            if (response.erroId.trim() == "") {
                swal({
                    type: 'success',
                    title: 'Sucesso',
                    text: 'Registro excluído com sucesso'
                });

                // atualiza tabela grupos
                Ficha2_CriarTabelaGrupos();
                return false;
            }
            else {
                swal({
                    type: 'error',
                    title: 'Aviso',
                    text: 'Erro ao excluir registro:' + response.erroId
                });
            }
            return false;
        } else {
            return false;
        }
    })
    return false;
}

// popup GRUPOS
function Ficha2_preencheCombo(clo_id, qualCombo, txtPlaceholder, tip_pai) {
    if (tip_pai == null)
        tip_pai = -1;

    //   var excluir_existentes = qualCombo == 'divFicha2_GrupoObjetos' ? 1 : 0;
    var excluir_existentes = 0;

    var cmb = $("#" + qualCombo);

    $.ajax({
        url: '/Objeto/PreenchecmbTiposObjeto',
        type: "POST",
        dataType: "JSON",
        data: { clo_id: clo_id, tip_pai: tip_pai, excluir_existentes: excluir_existentes, obj_id: selectedId_obj_id, somente_com_variaveis_inspecao: 1 },
        success: function (lstSubNiveis) {

            if (clo_id != 9) {
                cmb.empty();
                cmb.append($('<option selected disabled></option>').val(-1).html("--Selecione--")); // 1o item vazio
                $.each(lstSubNiveis, function (i, subNivel) {
                    cmb.append($('<option></option>').val(subNivel.Value.trim()).html(subNivel.Text.trim()));
                });
            }
            else {
                var i = 0;
                //$("#cmbSub2Div").empty();
                $("#cmbGrupos").empty();
                $("#cmbGrupos").append("<option value=''>--Selecione--</option>");
                $.each(lstSubNiveis, function (i, objeto) {
                    i++;
                    $("#cmbGrupos").append("<option value='" + objeto.Value + "'>" + objeto.Text + "</option>");

                    //if (i < 50) {
                    //    var tagchk = '<input type="checkbox" id="idXXX" nome="nameXXX" value="valueXXX" style="margin-right:5px">';
                    //    tagchk = tagchk.replace("idXXX", "chk" + i);
                    //    tagchk = tagchk.replace("nameXXX", "chk" + i);
                    //    tagchk = tagchk.replace("valueXXX", objeto.Value);

                    //    var taglbl = '<label for="idXXX" class="chklst" >TextoXXX</label> <br />';
                    //    taglbl = taglbl.replace("idXXX", "chk" + i);
                    //    taglbl = taglbl.replace("TextoXXX", objeto.Text);


                    //}
                });
            }
        }
    });
}

function cmbSub1Proximo_onchange() {

    document.getElementById("cmbSub2Div").style.display = 'none';
    // preenche proximo combo
    var valor = document.getElementById("cmbSub1").value;
    var ivalor = parseInt(valor);

    // superestrutura
    if (ivalor == 11) {
        // mostra Subdivisao2
        document.getElementById("cmbSub2Div").style.display = 'block';
        Ficha2_preencheCombo(7, 'cmbSub2', '--Selecione--', ivalor);

    }
    else
        document.getElementById("cmbSub2Div").style.display = 'none';
    document.getElementById("cmbSub3Div").style.display = 'none';
    document.getElementById("cmbGrupos").style.display = 'block';
    // ENCONTROS
    if (ivalor == 14) {
        // oculta tudo e deixa somente o botao salvar
        document.getElementById("cmbGrupos").style.display = 'none';

    }
    else
        Ficha2_preencheCombo(9, 'cmbGrupos', '--Selecione--', ivalor);

}

function cmbSub2_onchange() {

    // oculta o divs Subdivisao3
    document.getElementById("cmbSub3Div").style.display = 'none';

    // preenche proximo combo
    var valor = document.getElementById("cmbSub2").value;
    var ivalor = getTipoId(valor);

    if ((ivalor == 24) || (ivalor == 15) || (ivalor == 16)) { // 15 = Tabuleiro Face Superior; 16=Tabuleiro Face Inferior; 24 = Acesso

        Ficha2_preencheCombo(9, 'cmbGrupos', '--Selecione--', ivalor)
    }
    else {

        document.getElementById("cmbSub3div").style.display = 'block';
        Ficha2_preencheCombo(8, 'cmbSub3', '--Selecione--', ivalor)
    }


}

function cmbSub3_onchange() {

    // preenche proximo combo
    var valor = document.getElementById("cmbSub3").value;
    var ivalor = getTipoId(valor);

    Ficha2_LimparCampos(8);
    Ficha2_preencheCombo(9, 'cmbGrupos', '--Selecione--', ivalor)
}

function GetAll() {

    $.ajax({
        url: '/Politicaconserva/getAllConserva',
        type: "Get",
        dataType: "JSON",
        success: function (data) {
            console.log(data);
            $("#corpo").html("");
          
            $.each(data, function (i, item) {

                $("#divform").append(
                    $.each(item.Conservas, function (x, valor) {
                        $("#corpo").append($('<tr><td>' + valor.tip_nome + '</td><td>' + valor.Grupo + '</td><td>' + valor.Variavel +'</td><td style="text-align:center">' + valor.ale_codigo + '</td><td>' + valor.Alerta + '</td><td>' + valor.Servico + '</td><td style="text-align:center"><a href="#" onclick="return btnEdit_onclick(\'' + valor.Alerta + '\',\'' + valor.Servico + '\', \'' + valor.ocp_id + '\')" title="Editar"><span class="glyphicon glyphicon-pencil"></span></a></td></tr>'));
                    })
                );

            });
            paginar();
        }
    });
}

function paginar() {
    $(document).ready(function () {
        $('#tblSubs').DataTable({
            "language": {
                "lengthMenu": "Mostrando _MENU_ registros por página",
                "zeroRecords": "Nada encontrado",
                "info": "Mostrando página _PAGE_ de _PAGES_",
                "infoEmpty": "Nenhum registro disponível",
                "infoFiltered": "(filtrado de _MAX_ registros no total)"
            }
        });
    });
}
function cmbGrupos_onchange() {

    $("#cmbVrInspec").html("");
    $("#cmbVarInspec").empty();
    $("#cmbVarInspec").append("<option value=''>--Selecione--</option>");
    $.ajax({
        url: '/Politicaconserva/Variaveis?Id=' + $('#cmbGrupos option:selected').val(),
        type: "Get",
        dataType: "JSON",
        success: function (data) {

            if (data.length !== 0) {

                $.each(data, function (i, item) {
                    $("#cmbVarInspec").append($('<option value=' + item.ogv_id + '>' + item.variavel + '</option>'));
                });
            }


        }
    });
}

function pesquisar_click() {


    $.ajax({
        url: '/Politicaconserva/Listar?sub1=' + $('#cmbSub1 option:selected').text()
            + '&sub2=' + $('#cmbSub2 option:selected').text()
            + '&sub3=' + $('#cmbSub3 option:selected').text()
            + '&grupo=' + $('#cmbGrupos option:selected').text()
            + '&variavel=' + $('#cmbVarInspec option:selected').text()
            + '&variavelId=' + $('#cmbVarInspec option:selected').val()
        ,
        type: "Get",
        dataType: "JSON",
        success: function (data) {
            if (data.length !== 0) {
                $("#corpo").html("");
                $.each(data, function (i, item) {
                    $("#corpo").append($('<tr><td>' + item.tip_nome + '</td><td>' + item.Grupo + '</td><td>' + item.Variavel + '</td><td style="text-align:center">' + item.tipo + '</td><td>' + item.alerta + '</td><td>' + item.servico + '</td><td style="text-align:center"><a href="#" onclick="return btnEdit_onclick(\'' + item.alerta + '\',\'' + item.servico + '\', \'' + item.ocp_id + '\')" title="Editar"><span class="glyphicon glyphicon-pencil"></span></a></td></tr>'));
                });
            }


        }
    });
}


function btnEdit_onclick(descri, conserva, id) {

    $("#modalEdit").modal('show');

    $("#ocp_idEdit").val(id);    
    $("#descricaoEdit").val(descri);
    $("#conservaEdit").val(conserva);

}

function btnEditSair_onclick() {

   

}

function btnEditSalvar_onclick() {
    
    $.ajax({
        url: '/Politicaconserva/Edti?ocp_id=' + $("#ocp_idEdit").val() + '&alerta=' + $("#descricaoEdit").val() + '&conserva=' + $("#conservaEdit").val(),
        type: "Get",
        dataType: "JSON",
        success: function (data) {
            if (data == 'ok') {
                $("#modalEdit").modal('hide');
                GetAll();
                return false;
            }
            else {
                alert(data)
            }
          
        },
        error: function (erro) {
            alert(erro);
        }
    });
}
