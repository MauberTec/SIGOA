
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
jQuery("#txtLocalizacao").mask("00", options);
jQuery("#txtLocalizacaoAte_Novo").mask("00", options);
//jQuery("#txtLocalizacaoAte_Novo").attr('placeholder', "00");
//jQuery("#txtLocalizacao").attr('placeholder', "00");


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

    // checa SUBDIVISAO3
    if (document.getElementById("divSubdivisao3").style.display == 'block') {
        var subdivisao3 = $("#cmbSubdivisao3").val();
        if ((subdivisao3 == null) || (subdivisao3 < 0)) {
            campoBuraco = "A Subdivisão 3";
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

    // checa LOCALIZACAO
    var localizacao = $("#txtLocalizacao").val();
    if (localizacao == "") {
        campoBuraco = "A Localização";
        vazios += 1;
    }
    else
        if (vazios > 0) {
            return ("Código com falhas na sequência." + campoBuraco + " não possui seleção/valor e a Localização possui");
            //nao_vazios_apos += 1;
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

    var cmbSubdivisao3 = document.getElementById("cmbSubdivisao3");
    if ((cmbSubdivisao3.options.length > 0) && (cmbSubdivisao3.selectedIndex > 0))
    {
        subdivisao3 = "-" + getTipoCodigo(cmbSubdivisao3.options[cmbSubdivisao3.selectedIndex].value);
        item = cmbSubdivisao3.options[cmbSubdivisao3.selectedIndex].text;
    }
    else
        subdivisao3 = "";


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

    var cmbAEVCVG_Novo = document.getElementById("cmbAEVCVG_Novo");
    var localizacao = $("#txtLocalizacao").val() == "" ? "" : "-" + cmbAEVCVG_Novo.options[cmbAEVCVG_Novo.selectedIndex].value + $("#txtLocalizacao").val();

    txt.val(rodovia.trim() + rodoviaLado.trim() + OAE.trim() + tipoOAE.trim() + subdivisao1.trim() + subdivisao2.trim() + subdivisao3.trim() + grupoObjetos.trim() + numeroObjeto.trim() + localizacao.trim());

    if ((cmbAEVCVG_Novo.options.length >= 0) && (localizacao != "")) // possui valor ==> entao tem localizacao
        item = "Localização ";


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
            var localizacaoNome = "";
            var pedacosCodigo = txt.val().split("-");
            //var codGrupo = pedacosCodigo[pedacosCodigo.length - 2];
            //var localizacaoCodigo = obj_codigo + "-" + (cmbAEVCVG.selectedIndex > -1 ? cmbAEVCVG.options[cmbAEVCVG.selectedIndex].value : "") + $('#txtcodigo').val();
            var nomeGrupo = cmbGrupoObjetos.options[cmbGrupoObjetos.selectedIndex].text;

            if ((cmbAEVCVG_Novo.options.length >= 0) && (localizacao != "")) {
                switch (cmbAEVCVG_Novo.options[cmbAEVCVG_Novo.selectedIndex].value) {
                    case "A": localizacaoNome = "Apoio"; break;
                    case "E": localizacaoNome = "Encontro"; break;
                    case "V": localizacaoNome = "Vão"; break;
                    case "T": localizacaoNome = "Trecho"; break;
                    case "VC": localizacaoNome = "Vão Caixão Perdido"; break;
                    case "VG": localizacaoNome = "Vão em Grelha"; break;
                }

                // coloca a descricao
             //   $('#txtNovoDescricao').val(localizacaoNome + " #" + $('#txtLocalizacao').val() + (masculinos.includes(idGrupo) ? " do" + ss : " da" + ss) + " " + nomeGrupo + (lstExcecoes.includes(obj_tipoGrupo) ? " " : " #" + $("#txtNumeroObjeto").val()));
                var descricao = nomeGrupo + (lstExcecoes.includes(obj_tipoGrupo) ? " " : " #" + $("#txtNumeroObjeto").val()) + " " + localizacaoNome + " #" + $('#txtLocalizacao').val() ;
                $('#txtNovoDescricao').val(descricao);

               // set @obj_descricaoX = @grupo_objetos_nome + ' #' + right('00' + convert(varchar(2), @inum), 2) +  ' ' + @tip_nome + ' #' + right('00' + convert(varchar(2), @i), 2); 

            }
            else
                if ($("#txtNumeroObjeto").val() != "") {
                    // $('#txtNovoDescricao').val(nomeGrupo + " #" + txt.val().replace(localizacao,"") ) ;
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

    $("#cmbAEVCVG_Novo").html(""); // limpa 
    $('#txtLocalizacaoAte_Novo').val("00");

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

    if (aPartirDe <= 7) $("#cmbSubdivisao3").html(""); // limpa os itens existentes;
    if (aPartirDe <= 8) $("#cmbGrupoObjetos").html(""); // limpa os itens existentes;
    if (aPartirDe <= 9) {
        $('#txtNumeroObjeto').val("");
        $('#txtNumeroObjetoAte_Novo').val("");
    }

    if (aPartirDe <= 10) {
        $('#txtLocalizacao').val("");
        $('#txtLocalizacaoAte_Novo').val("");
    }
    $('#txtNovoDescricao').val("");

    jQuery("#txtNumeroObjeto").mask("00", options2);

    PreenchetxtCodigoDigitavel();

    jQuery("#txtOAE").mask("000,000", options);
    jQuery("#txtOAE").attr("placeholder", "000,000 - Quilometragem");

    return false;
}

function bntNovo_click() {

 /*  02/mar/2021 - nao limpar campos para facilitar cadastro
  *  
    LimparCampos(0);

    // oculta o divs Subdivisao2 e 3
    document.getElementById("divSubdivisao2").style.display = 'none';
    document.getElementById("divSubdivisao3").style.display = 'none';

    //// preenche combo
    //preencheCombo(1, 'cmbRodovia', '--Selecione--', null);
    
    */

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
            obj_localizacaoAte: $('#txtLocalizacaoAte_Novo').val()
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
            $

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
    document.getElementById("divSubdivisao3").style.display = 'none';

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

    // oculta o divs Subdivisao3
    document.getElementById("divSubdivisao3").style.display = 'none';

    // preenche proximo combo
    var valor = document.getElementById("cmbSubdivisao2").value;
    var ivalor = getTipoId(valor);

    if ((ivalor == 24) || (ivalor == 15) || (ivalor == 16)) { // 15 = Tabuleiro Face Superior; 16=Tabuleiro Face Inferior; 24 = Acesso
        LimparCampos(9);
        preencheCombo(9, 'cmbGrupoObjetos', '--Selecione--', ivalor)
    }
    else {
        LimparCampos(8);
        document.getElementById("divSubdivisao3").style.display = 'block';
        preencheCombo(8, 'cmbSubdivisao3', '--Selecione--', ivalor)
    }

    // preenche o combo localizacao
    if ((ivalor == 15) || (ivalor == 16)) {// 15 = Tabuleiro Face Superior; 16=Tabuleiro Face Inferior
        $("#cmbAEVCVG_Novo").html(""); // limpa os itens existentes
        switch (ivalor) {
            case 15: $("#cmbAEVCVG_Novo").append($('<option selected disabled></option>').val("T").html("T"));  break;// Tabuleiro Face Superior
            case 16: 
                    $("#cmbAEVCVG_Novo").append($('<option selected></option>').val("V").html("V"));
                    $("#cmbAEVCVG_Novo").append($('<option></option>').val("VC").html("VC"));
                    $("#cmbAEVCVG_Novo").append($('<option></option>').val("VG").html("VG"));
                break;// Tabuleiro Face Inferior
        }
    }

    // preenche novamente o cmbAEVCVG_Novo
    valor = document.getElementById("cmbSubdivisao1").value;
    ivalor = getTipoId(valor);

    if (ivalor == 14)// Encontro
        $("#cmbAEVCVG_Novo").append($('<option selected disabled></option>').val("E").html("E"));
    else
        if ((ivalor == 12) || (ivalor == 13)) // Mesoestrutura, Infraestrutura
            $("#cmbAEVCVG_Novo").append($('<option selected disabled></option>').val("A").html("A"));


}
function cmbSubdivisao3_onchange() {

    // preenche proximo combo
    var valor = document.getElementById("cmbSubdivisao3").value;
    var ivalor = getTipoId(valor);

    LimparCampos(8);
    preencheCombo(9, 'cmbGrupoObjetos', '--Selecione--', ivalor)

    // preenche novamente o cmbAEVCVG_Novo
    valor = document.getElementById("cmbSubdivisao1").value;
    ivalor = getTipoId(valor);

    if (ivalor == 14)// Encontro
        $("#cmbAEVCVG_Novo").append($('<option selected disabled></option>').val("E").html("E"));
    else
        if ((ivalor == 12) || (ivalor == 13)) // Mesoestrutura, Infraestrutura
            $("#cmbAEVCVG_Novo").append($('<option selected disabled></option>').val("A").html("A"));


}

function cmbGrupoObjetos_onchange() {

    // preenche proximo combo
    var valor = document.getElementById("cmbGrupoObjetos").value;

    if (lstExcecoes.includes(valor)) {
        document.getElementById("divNumeroObjeto").style.display = 'none';

        var mascara = '00';
        if (valor == "46:PR")   // se for pavimento rigido, entao tem 3 digitos
            mascara = '000';

        jQuery("#txtLocalizacao").mask(mascara, options);
        jQuery("#txtLocalizacao").attr('placeholder', mascara);
        jQuery("#txtLocalizacaoAte_Novo").mask(mascara, options);
        jQuery("#txtLocalizacaoAte_Novo").attr('placeholder', mascara);

    }
    else
    {
        document.getElementById("divNumeroObjeto").style.display = 'block';
    }

    PreenchetxtCodigoDigitavel();
}

function txtLocalizacao_onkeyup() {

    var txtLocalizacao = $("#txtLocalizacao");
    txtLocalizacao.val(txtLocalizacao.val().toUpperCase());

    PreenchetxtCodigoDigitavel();

}

function txtLocalizacao_onblur() {


    var cmbGrupoObjetosval = $("#cmbGrupoObjetos").val();

    var lblPrefixoval = $('#lblPrefixo').text();
    if (cmbGrupoObjetosval == "46:PR")  // se for pavimento rigido, os numeros possuem 3 digitos
    {
        var txtcodval = Right(("000" + $('#txtLocalizacao').val()), 3);
        $('#txtLocalizacao').val(txtcodval);

        var txtCodigoAte = Right(("000" + $('#txtLocalizacaoAte_Novo').val()), 3);
        $('#txtLocalizacaoAte_Novo').val(txtCodigoAte);
    }
    else {
        var txtcodval = Right(("00" + $('#txtLocalizacao').val()), 2);
        $('#txtLocalizacao').val(txtcodval);

        var txtCodigoAte = Right(("00" + $('#txtLocalizacaoAte_Novo').val()), 2);
        $('#txtLocalizacaoAte_Novo').val(txtCodigoAte);
    }

    // atualiza descricao
    PreenchetxtCodigoDigitavel();

}



