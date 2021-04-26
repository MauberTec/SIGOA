
var lstExcecoes = ['45:PF', '46:PR', '53:SIHO', '104:PF', '105:PR', '111:SIHO', '126:LA'];


var options2 = {
    onKeyPress: function (val, e, field, options) {
        var corBranca = "rgb(255, 255, 255)";
        field.css("background-color", corBranca);
    },
    onInvalid: function (val, e, f, invalid, options) {
        var corVermelho = "rgb(228, 88, 71)";
        //   f.css("background-color", corVermelho);
    },
    'translation': { // IGNORA A MASCARA "S" (STRING) E MASCARA O "Y" SOMENTE "D" ou "E"
        Y: { pattern: /[D-Ed-e]/, optional: true }
    }
};

jQuery("#txtNumeroObjeto").mask("00", options2);



function validaCodigoDigitavel() {
    var vazios = 0;
    var nao_vazios_apos = 0;
    var campoBuraco = "";

    var txt = $('#txtCodigoDigitavel');

    // checa Rodovia
    var cmbRodovia = $("#cmbRodovia").val()
    if (cmbRodovia == null) return "Selecione o Tipo de Rodovia";

    var rodovia = $('#txtRodovia').val();
    if (rodovia.trim() == "") return "Código da Rodovia é Obrigatório.";

    var rodoviaLado = ""; // $("#cmbRodoviaED").val().trim();
    if (document.getElementById("tdcmbRodoviaED").style.display != 'block') {
        rodoviaLado = "-" + $("#cmbRodoviaED").val();
    }

    // checa OAE
    var OAE = $("#txtOAE").val().trim();
    if (OAE == "")
        return "A Obra de Arte/Quilometragem é Obrigatória.";
    else
        if (OAE.length != 7)
            return "A Quilometragem da Obra de Arte está incompleta.";

    // checa TIPO OAE
    var tipoOAE = $("#cmbTipoOAE").val();
    if (tipoOAE == null) {
        campoBuraco = "O Tipo de Obra de Arte";
        vazios += 1;
    }

    // checa SUBDIVISAO1
    var subdivisao1 = $("#cmbSubdivisao1").val();
    if ((subdivisao1 == null) || (subdivisao1 < 0)) {
        campoBuraco = "A Subdivisão 1";
        vazios += 1;
    }
    else
        if (vazios > 0) {
            return ("Código com falhas na sequência." + campoBuraco + " está vazio e o combo Subdivisão 1 está selecionado");
            // nao_vazios_apos += 1;
        }

    // checa SUBDIVISAO2
    if (document.getElementById("divSubdivisao2").style.display == 'block') {
        var subdivisao2 = $("#cmbSubdivisao2").val();
        if ((subdivisao2 == null) || (subdivisao2 < 0)) {
            campoBuraco = "A Subdivisão 2";
            vazios += 1;
        }
        else
            if (vazios > 0) {
                return ("Código com falhas na sequência." + campoBuraco + " não possui seleção e o combo Subdivisão 2 está selecionado");
                // nao_vazios_apos += 1;
            }
    }


    // checa GRUPO DE OBJETOS
    var grupoObjetos = $("#cmbGrupoObjetos").val();
    if ((grupoObjetos == null) || (grupoObjetos < 0)) {
        campoBuraco = "O Grupo de Objetos";
        vazios += 1;
    }
    else
        if (vazios > 0) {
            return ("Código com falhas na sequência." + campoBuraco + " não possui seleção e o combo Grupo de Objetos está selecionado");
            // nao_vazios_apos += 1;
        }

    // checa txtNumeroObjeto
    var NumeroObjeto = $("#txtNumeroObjeto").val();
    if (!lstExcecoes.includes(grupoObjetos)) {
        if (NumeroObjeto == "") {
            campoBuraco = "O número do Objeto ";
            vazios += 1;
        }
        else
            if (vazios > 0) {
                return ("Código com falhas na sequência." + campoBuraco + " não possui seleção e o Número do Objeto possui");
                //nao_vazios_apos += 1;
            }
    }


    //  return "vazios: " + vazios + " naovazios: " + nao_vazios_apos;

    if (nao_vazios_apos > 0)
        return "Código com falhas na sequência";

    return "";
}

function PreenchetxtCodigoDigitavel() {
    var item = "";
    var txt = $('#txtCodigoDigitavel');

    var rodovia = $('#txtRodovia').val();

    var rodoviaLado = ""; // $("#cmbRodoviaED").val().trim();
    if (document.getElementById("tdcmbRodoviaED").style.display == 'block') {
        if ((rodoviaLado == "") || (rodoviaLado == null) || ($("#cmbRodoviaED").val() < 0))
            rodoviaLado = "";
        else
            rodoviaLado = "-" + $("#cmbRodoviaED").val();
    }


  //  var OAE = document.getElementById("txtOAE");
    var OAE = $("#txtOAE").val();
    if (OAE != "") {
        OAE = "-" + $("#txtOAE").val();
        item = "OAE Km " + $("#txtOAE").val();
    }
    else
        OAE = "";


    var cmbTipoOAE = document.getElementById("cmbTipoOAE");
    if ((cmbTipoOAE.options.length > 0) && (cmbTipoOAE.selectedIndex > 0))
    {
        tipoOAE = "-" + getTipoCodigo(cmbTipoOAE.options[cmbTipoOAE.selectedIndex].value);
        item = cmbTipoOAE.options[cmbTipoOAE.selectedIndex].text;
    }
    else
        tipoOAE = "";


    var cmbSubdivisao1 = document.getElementById("cmbSubdivisao1");
    if ((cmbSubdivisao1.options.length > 0) && (cmbSubdivisao1.selectedIndex > 0))
    {
        subdivisao1 = "-" + getTipoCodigo(cmbSubdivisao1.options[cmbSubdivisao1.selectedIndex].value);
        item = cmbSubdivisao1.options[cmbSubdivisao1.selectedIndex].text;
    }
    else
        subdivisao1 = "";


    var cmbSubdivisao2 = document.getElementById("cmbSubdivisao2");
    if ((cmbSubdivisao2.options.length > 0) && (cmbSubdivisao2.selectedIndex > 0))
    {
        subdivisao2 = "-" + getTipoCodigo(cmbSubdivisao2.options[cmbSubdivisao2.selectedIndex].value);
        item = cmbSubdivisao2.options[cmbSubdivisao2.selectedIndex].text;
    }
    else
        subdivisao2 = "";

    var cmbGrupoObjetos = document.getElementById("cmbGrupoObjetos");
    if ((cmbGrupoObjetos.options.length > 0) && (cmbGrupoObjetos.selectedIndex > 0))
    {
        grupoObjetos = "-" + getTipoCodigo(cmbGrupoObjetos.options[cmbGrupoObjetos.selectedIndex].value);
        item = cmbGrupoObjetos.options[cmbGrupoObjetos.selectedIndex].text;
        obj_tipoGrupo = cmbGrupoObjetos.options[cmbGrupoObjetos.selectedIndex].value ;
    }
    else
        grupoObjetos = "";


    var numeroObjeto = $("#txtNumeroObjeto").val() == "" ? "" : "-" + $("#txtNumeroObjeto").val();
    if (numeroObjeto != "")
        item = "Número Objeto ";


    txt.val(rodovia.trim() + rodoviaLado.trim() + OAE.trim() + tipoOAE.trim() + subdivisao1.trim() + subdivisao2.trim() + grupoObjetos.trim() + numeroObjeto.trim() );

    // preenche Descricao
    if (item.includes("OAE Km"))
        $("#txtNovoDescricao").val(item + " " + ' da Rodovia ' + rodovia + rodoviaLado);
    else
        if ((cmbGrupoObjetos.options.length > 0) && (cmbGrupoObjetos.selectedIndex > 0))
        {
            var cmbgrupoValue = cmbGrupoObjetos.options[cmbGrupoObjetos.selectedIndex].value;

            var idGrupo = parseInt(cmbgrupoValue.substring(0, cmbgrupoValue.indexOf(":")));
            var masculinos = [14, 15, 16,24, 25, 32, 33, 34, 36, 37, 38, 39, 40, 44, 45, 46, 47, 50, 52, 57, 72, 73, 76, 77, 78, 80, 81, 82, 84, 87, 90, 93, 104, 105, 106, 108, 110];
            var plural = [33, 39, 40, 43, 47, 56, 95, 98, 101, 106, 114];
            var ss = plural.includes(idGrupo) ? "s" : "";
            var pedacosCodigo = txt.val().split("-");
            //var codGrupo = pedacosCodigo[pedacosCodigo.length - 2];
            var nomeGrupo = cmbGrupoObjetos.options[cmbGrupoObjetos.selectedIndex].text;

                if ($("#txtNumeroObjeto").val() != "") {
                    $('#txtNovoDescricao').val(nomeGrupo + " #" + $('#txtNumeroObjeto').val() + " (" + txt.val() + "-" + $('#txtcodigo').val() + ")");
                }
                else
                {
                    $("#txtNovoDescricao").val(nomeGrupo + " " + txt.val());
                }
        }
        else
            $("#txtNovoDescricao").val(item + " " + txt.val());


}
function LimparCampos(aPartirDe) {

    if (aPartirDe <= 1) $("#cmbRodovia").val(-1);

    if (aPartirDe <= 2) {
        $('#txtRodovia').val("");
        $("#txtRodovia").attr('placeholder', "Código Rodovia");
        $("#cmbRodoviaED").val(null);
        document.getElementById("tdcmbRodoviaED").style.display = 'none';
    }


    if (aPartirDe <= 3) $('#txtOAE').val("");
    if (aPartirDe <= 4) {
        $("#cmbTipoOAE").val(null);
    }

    if (aPartirDe <= 5) $("#cmbSubdivisao1").val(null);

    if (aPartirDe <= 6) {
        $("#cmbSubdivisao2").html("");
    }

    if (aPartirDe <= 8) $("#cmbGrupoObjetos").html(""); // limpa os itens existentes;
    if (aPartirDe <= 9) {
        $('#txtNumeroObjeto').val("");
        $('#txtNumeroObjetoAte_Novo').val("");
    }


    $('#txtNovoDescricao').val("");

    jQuery("#txtNumeroObjeto").mask("00", options2);

    PreenchetxtCodigoDigitavel();

    jQuery("#txtOAE").mask("000,000", options);
    jQuery("#txtOAE").attr("placeholder", "000,000 - Quilometragem");

    return false;
}

function bntNovo_click() {

    jQuery("#txtOAE").mask("000,000", options);
    jQuery("#txtOAE").attr("placeholder", "000,000 - Quilometragem");

    $("#modalNovoRegistro").modal('show');
}

function bntSalvarNovo_click() {

    var retorno = validaCodigoDigitavel();
    if (retorno != "") {
        swal({
            type: 'error',
            title: 'Aviso',
            text: retorno
        });

        // $("#modalNovoRegistro").modal('show');
        return false;
    }
    else {
        var param = {
            obj_codigo: $('#txtCodigoDigitavel').val(),
            obj_descricao: $('#txtNovoDescricao').val(),
            obj_NumeroObjetoAte: $('#txtNumeroObjetoAte_Novo').val(),
            obj_localizacaoAte:""
        };

        $.ajax({
            url: "/Objeto/Objeto_Inserir",
            data: JSON.stringify(param),
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                // fecha o modal
                $("#modalNovoRegistro").modal('hide');
                ehInsercao = 0;

                var objid = result.substring(0, result.indexOf(";"));
                var retornomsg = result.substring(result.indexOf(";") + 1, 10000);
                selectedId_obj_id = parseInt(objid);

                if (retornomsg.trim() != "") {
                    swal({
                        type: 'error',
                        title: 'Aviso',
                        text: retornomsg
                    });
                    return false;
                }
                else {
                    swal({
                        type: 'success',
                        title: 'Sucesso',
                        text: 'Objeto(s) Inserido(s) com sucesso'
                    });
                }

                // recarrega o grid
                carregaGrid(selectedId_obj_id);

                //  preenche Ficha inspecao cadastral 
                preenchetblFicha(selectedId_obj_id, 3, -1);

                return false;
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
                ehInsercao = 0;
                return false;
            }
        });
    }


    return false;
}

function preencheCombo(clo_id, qualCombo, txtPlaceholder, tip_pai) {
    if (tip_pai == null)
        tip_pai = -1;

    var cmb = $("#" + qualCombo);
    cmb.html(""); // limpa os itens existentes
    cmb.append($('<option selected ></option>').val(-1).html(txtPlaceholder)); // 1o item vazio

    $.ajax({
        url: '/Objeto/PreenchecmbTiposObjeto',
        type: "POST",
        dataType: "JSON",
        data: { clo_id: clo_id, tip_pai: tip_pai },
        success: function (lstSubNiveis) {

            $.each(lstSubNiveis, function (i, subNivel) {
                cmb.append($('<option></option>').val(subNivel.Value.trim()).html(subNivel.Text.trim()));
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

function getTipoMascara(aux) {
    var saida = "";
    var tip_id = parseInt(aux.substring(0, aux.indexOf(":")));

    switch (parseInt(tip_id)) {
        case 1: saida = "SP 000"; break; // Rodovia
        case 2: saida = "SPM 000-Y"; break; // Marginal
        case 3: saida = "SPA 000/000"; break; // Acesso
        case 136: saida = "SPC 000/000"; break; // Contorno
        case 4: saida = "SPI 000/000"; break; // Interligacao
        case 5: saida = "SPD 000/000"; break; //Dispositivo
        case 6: saida = "SPV 000-000"; break; // vicinal
        case 28: saida = "000,000"; break; // Obra de Arte quilometragem
    }

    return saida;
}

function txtRodovia_onkeyup() {
    PreenchetxtCodigoDigitavel();

    var txt = document.getElementById('txtRodovia');
    if (txt.value.length == txt.placeholder.length) {
        if (document.getElementById("tdcmbRodoviaED").style.display != 'block')
            $('#txtOAE').focus();
    }
}

function cmbRodovia_onchange() {
    LimparCampos(2);

    // preenche proximo combo
    preencheCombo(3, 'cmbTipoOAE', '--Selecione--');

    var valor = document.getElementById("cmbRodovia").value;

    // oculta/mostra o combo Esquerda/Direita
    if (getTipoId(valor) == 2)
        document.getElementById("tdcmbRodoviaED").style.display = 'block';
    else
        document.getElementById("tdcmbRodoviaED").style.display = 'none';

    // coloca mascara no textbox
    var mascara = getTipoMascara(valor);
    if (mascara.endsWith("Y")) {
        mascara = mascara.substring(0, mascara.length - 2);
    }

    if (getTipoId(valor)  > -1)
        document.getElementById("txtRodovia").disabled = false;
    else
        document.getElementById("txtRodovia").disabled = true;

    $('#txtRodovia').val("");
    jQuery("#txtRodovia").mask(mascara, options);
    jQuery("#txtRodovia").attr('placeholder', mascara);
    $('#txtRodovia').focus();
}
function cmbTipoOAE_onchange() {

    LimparCampos(5);

    // preenche proximo combo
    preencheCombo(6, 'cmbSubdivisao1', '--Selecione--');
}

function cmbSubdivisao1_onchange() {

    LimparCampos(6);

    // preenche proximo combo
    var valor = document.getElementById("cmbSubdivisao1").value;
    var ivalor = getTipoId(valor);

    $("#cmbSubdivisao2").html(""); // limpa tudo
    $("#cmbSubdivisao2").append($('<option selected ></option>').val(-1).html('--Selecione--'));

    // oculta o divs Subdivisao2
    document.getElementById("divSubdivisao2").style.display = 'none';

    // superestrutura
    if (ivalor == 11) {
        // mostra Subdivisao2
        document.getElementById("divSubdivisao2").style.display = 'block';

        // preenche combo manualmente, tip_id = 15 (tabuleiro face superior) e 16 (tabuleiro face inferior)
        for (var i = 0; i < cmbFiltroTiposObjeto.options.length; i++) {
            if ((getTipoId(cmbFiltroTiposObjeto.options[i].value) == 15)
                || (getTipoId(cmbFiltroTiposObjeto.options[i].value) == 16)) {
                var option = document.createElement("option");
                option.text = cmbFiltroTiposObjeto.options[i].text;
                option.value = cmbFiltroTiposObjeto.options[i].value;
                $("#cmbSubdivisao2").append($('<option></option>').val(option.value).html(option.text));
            }
        }
    }
    else
        // ENCONTROS
        if (ivalor == 14) {
            // mostra Subdivisao2 e 3
            document.getElementById("divSubdivisao2").style.display = 'block';

            // preenche combo manualmente, tip_id = 22,23,24 ESTRUTURAS DE TERRRA, DE CONCRETO E ACESSOS
            for (var i = 0; i < cmbFiltroTiposObjeto.options.length; i++) {
                if ((getTipoId(cmbFiltroTiposObjeto.options[i].value) == 22)
                    || (getTipoId(cmbFiltroTiposObjeto.options[i].value) == 23)
                    || (getTipoId(cmbFiltroTiposObjeto.options[i].value) == 24)
                ) {
                    var option = document.createElement("option");
                    option.text = cmbFiltroTiposObjeto.options[i].text;
                    option.value = cmbFiltroTiposObjeto.options[i].value;
                    $("#cmbSubdivisao2").append($('<option></option>').val(option.value).html(option.text));
                }
            }
        }

        else
            preencheCombo(9, 'cmbGrupoObjetos', '--Selecione--', ivalor);

    if (ivalor == 14)// Encontro
        $("#cmbAEVCVG_Novo").append($('<option selected disabled></option>').val("E").html("E"));
    else
       if ((ivalor == 12) || (ivalor == 13)) // Mesoestrutura, Infraestrutura
          $("#cmbAEVCVG_Novo").append($('<option selected disabled></option>').val("A").html("A"));


}

function cmbSubdivisao2_onchange() {

    // preenche proximo combo
    var valor = document.getElementById("cmbSubdivisao2").value;
    var ivalor = getTipoId(valor);

        LimparCampos(9);
        preencheCombo(9, 'cmbGrupoObjetos', '--Selecione--', ivalor)

}

function cmbGrupoObjetos_onchange() {

    // preenche proximo combo
    var valor = document.getElementById("cmbGrupoObjetos").value;

    if (lstExcecoes.includes(valor)) {
        document.getElementById("divNumeroObjeto").style.display = 'none';

    }
    else
    {
        document.getElementById("divNumeroObjeto").style.display = 'block';
    }

    PreenchetxtCodigoDigitavel();
}


