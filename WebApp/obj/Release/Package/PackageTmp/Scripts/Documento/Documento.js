
var selectedId = -1;
var ehInsercao = -1;
var obj_codigo_old = "";
var corVermelho = "rgb(228, 88, 71)";
var corBranca = "rgb(255, 255, 255)";


// Padrão de codificação de Documentos: S (alfabético), A (alfanumérico), 0 (numérico), 9 (opcional numérico), outros = Letra Fixa
var mascara_doc_especifico_Principal = "SS-ZPQQQQ000-000.000-AAA-S00/000-S9";
var mascara_doc_especifico_Vicinal = "SS-ZSSQQQ000-000.000-AAA-S00/000-S9";
var mascara_doc_especifico_Acesso = "SS-ZPA000000-000.000-AAA-S00/000-S9";
var mascara_doc_especifico_Marginal = "SS-ZPMQQ000Y-000.000-AAA-S00/000-S9";
var mascara_doc_especifico_Interligacao = "SS-ZPI000000-000.000-AAA-S00/000-S9";
var mascara_doc_especifico_Dispositivo = "SS-ZPD000000-000.000-AAA-S00/000-S9";


var optionsdocs = {

    onKeyPress: function (val, e, field, options) {
        //$('#txtdoc_Grupo21').val($('#txtdoc_Grupo21').val().toUpperCase());
        //var valor = $('#txtdoc_Grupo21').cleanVal();

       // alert(valor);
        field.css("background-color", corBranca);
        //if (val.length > 2) {
        //    var mascara = qualMascara(valor, 1);
        //    $('#txtdoc_Grupo21').mask(mascara, options);
        //    $('#txtdoc_Grupo21').attr('placeholder', mascara.replace(/Z/g, 'S'));
        //}
    },
    onInvalid: function (val, e, f, invalid, options) {
        var corVermelho = "rgb(228, 88, 71)";
        //   f.css("background-color", corVermelho);
    },
    'translation': {
        Y: { pattern: /[D-Ed-e]/, optional: true }, // "Y" SOMENTE "D" ou "E"
        Z: { pattern: /S/, optional: true }, // "Z"para letra "S"
        Q: { pattern: /0/, fallback: '0', optional: false }, // "Q"para digito Zero
      //  A: { pattern: /A/, fallback: 'A', optional: false }, // "A"para  letra "A"
    }

};

jQuery("#txtdoc_Grupo22a").mask("000", options);
jQuery("#txtdoc_Grupo22b").mask("000", options);
jQuery("#txtdoc_Grupo23").mask("AAA", options);

$('#txtdoc_Grupo23').attr('placeholder', "os dois primeiros dígitos referem-se ao subtrecho, lote, fase ou prioridade; o terceiro dígito refere-se à identificação de obra de arte especial ou itens, todos definidos pelo DER/SP");

function colocaMascara_Grupo21() {

    var txtdoc_Grupo21 = document.getElementById("txtdoc_Grupo21");
    txtdoc_Grupo21.style.backgroundColor = corBranca;



    //$('#txtdoc_Grupo21').val($('#txtdoc_Grupo21').val().toUpperCase());

    //var valor = $('#txtdoc_Grupo21').val();

    //if (valor.trim() != "") {
    //    var mascara = qualMascara(jQuery("#txtdoc_Grupo21").val(), 1);
    //    if (mascara != "") {
    //        jQuery("#txtdoc_Grupo21").mask(mascara, optionsdocs);
    //    }
    //    else {
    //        $('#txtdoc_Grupo21').unmask();
    //    }
    //    $('#txtdoc_Grupo21').attr('placeholder', mascara);
    //}
    //else {
    //    $('#txtdoc_Grupo21').unmask();
    //    $('#txtdoc_Grupo21').attr('placeholder', "");
    //}

    var DocTipoIdx = document.getElementById("cmbTiposDocumento").selectedIndex;
    var DocTipoValue = $('#cmbTiposDocumento').val();
    if (DocTipoValue != "NC")
    {
        if (DocTipoIdx <= 5)
            MontaLblCodigo(0);// DOCUMENTO TECNICO GERAL
        else
            MontaLblCodigo(1);// DOCUMENTO TECNICO especifico
    }

}
function qualMascara(txt, ehGrupo21) {
    var digito3 = "";
    var saida = "";
    if (txt.length > 5)
        digito3 = txt.substring(5, 6);

    if (ehGrupo21 == 1)
        digito3 = txt.substring(2, 3);


    switch (digito3) {
        case "0": saida = mascara_doc_especifico_Principal; break;
        case "A": saida = mascara_doc_especifico_Acesso; break;
        case "M": saida = mascara_doc_especifico_Marginal; break;
        case "I": saida = mascara_doc_especifico_Interligacao; break;
        case "D": saida = mascara_doc_especifico_Dispositivo; break;
        case "V": saida = mascara_doc_especifico_Vicinal; break;
        default: saida = "";
    }

    if (ehGrupo21 == 1)
        saida = saida.substring(3, 12);

   // alert(saida);
    return saida;

}

function limpaCampos()
{
    document.getElementById("divNaoAssociado").style.display = "none";

    var cmbTiposDocumento = document.getElementById("cmbTiposDocumento");
    cmbTiposDocumento.options[cmbTiposDocumento.options.length - 1].disabled = true; //desabilita a opcao "NC"

    $('#txtCodigoDigitavel').css('background-color', corBranca);
    $('#cmbTiposDocumento').css('background-color', corBranca);
    $('#txtdoc_Grupo21').css('background-color', corBranca);
    $('#txtdoc_Grupo22a').css('background-color', corBranca);
    $('#txtdoc_Grupo22b').css('background-color', corBranca);
    $('#txtdoc_Grupo23').css('background-color', corBranca);
    $('#cmbClasseProjeto').css('background-color', corBranca);
    $('#txtSequencial').css('background-color', corBranca);
    $('#txtRevisao').css('background-color', corBranca);
    $('#txtdoc_descricao').css('background-color', corBranca);
    $('#txtdoc_caminho').css('background-color', corBranca);

    $('#txtdoc_codigo').val("");
    $('#txtCodigoDigitavel').val("");
    $('#txt10').text("xx");
    $('#txt11').text("xxx");
    $('#txt12').text("xxx");
    $('#txt13').text("x0");
    $('#txt20').text("xx");
    $('#txt21').text("xxxxxxxxx");
    $('#txt22A').text("000");
    $('#txt22B').text("000");
    $('#txt23').text("xxx");
    $('#txt24').text("x00");
    $('#txt25').text("000");
    $('#txt26').text("x0");

    $('#txtSequencial').val("");
    $('#txtRevisao').val("");
    $('#txtdoc_descricao').val("");

    $('#txtdoc_caminho').val("");
    $('#txtdoc_caminho').removeAttr("title");

    $('#txtdoc_caminho2').val("");
    $('#txtdoc_caminho3').val("");
    $('#chkdoc_ativo').prop('checked', true);

    $('#txtdoc_Grupo21').val("");
    $('#txtdoc_Grupo22a').val("");
    $('#txtdoc_Grupo22b').val("");
    $('#txtdoc_Grupo23').val("");

    var cmbTiposDocumento = document.getElementById("cmbTiposDocumento");
    cmbTiposDocumento.selectedIndex = 0;

    $('#txtdoc_id').val("");
    $('#cmbClasseProjeto').val(0);


}

function checaNull(test) {
    return test == null ? '' : test;
}

function MontaLblCodigo(ehEspecifico, sender) {
    $('#txtCodigoDigitavel').css('background-color', corBranca);

    var cmbTiposDocumento = document.getElementById("cmbTiposDocumento");
    var Nivel1 = cmbTiposDocumento.selectedIndex > 0 ? cmbTiposDocumento.options[cmbTiposDocumento.selectedIndex].value : "XX";

    var Nivel21 = (checaNull($('#txtdoc_Grupo21').val().toUpperCase()) + "xxxxxxxxx").substr(0, 9);
    var Nivel22A = ("000" + checaNull($('#txtdoc_Grupo22a').val())).slice(-3);
    var Nivel22B = ("000" + checaNull($('#txtdoc_Grupo22b').val())).slice(-3);
    var Nivel23 = (checaNull($('#txtdoc_Grupo23').val().toUpperCase()) + "xxx").substr(0, 3);

    var cmbClasseProjetoVal = $('#cmbClasseProjeto').val();
    var Nivel31 = ((cmbClasseProjetoVal < 0) || (cmbClasseProjetoVal == null)) ? "x00" : cmbClasseProjetoVal;
    var Nivel32 = txtSequencial.value;
    var Nivel32 = ("000" + checaNull(txtSequencial.value)).slice(-3);

    var Nivel33 = $('#txtRevisao').val().toUpperCase();
    if (Nivel33 == "")
        Nivel33 = "x0";

    var total = Nivel1;
    if (ehEspecifico == 1)
        total = total + "-" + checaNull(Nivel21) + "-" + Nivel22A + "." + Nivel22B + "-" + checaNull(Nivel23);
    else
        total = total + "-DE";

    // junta todos os pedacos
    total = total + "-" + checaNull(Nivel31) + "/" + checaNull(Nivel32) + "-" + (Nivel33);

    $('#txtdoc_codigo').val(total);

    if (ehEspecifico == 1) {
        $('#txt20').text(Nivel1);
        $('#txt21').text(Nivel21);
        $('#txt22A').text(Nivel22A);
        $('#txt22B').text(Nivel22B);
        $('#txt23').text(Nivel23);
        $('#txt24').text(Nivel31);
        $('#txt25').text(Nivel32);
        $('#txt26').text(Nivel33);

        document.getElementById("lblNivel31").style.display = "inline-block";
        document.getElementById("lblNivel32").style.display = "inline-block";
        document.getElementById("lblNivel21").style.display = "none";
        document.getElementById("lblNivel22").style.display = "none";

        document.getElementById("tblDocTecEspecifico").style.display = "inline-block";
        document.getElementById("tblDocTecGeral").style.display = "none";
    }
    else {
        $('#txt10').text(Nivel1);
        $('#txt11').text(Nivel31);
        $('#txt12').text(Nivel32);
        $('#txt13').text(Nivel33);

        document.getElementById("lblNivel21").style.display = "inline-block";
        document.getElementById("lblNivel22").style.display = "inline-block";
        document.getElementById("lblNivel31").style.display = "none";
        document.getElementById("lblNivel32").style.display = "none";

        document.getElementById("tblDocTecEspecifico").style.display = "none";
        document.getElementById("tblDocTecGeral").style.display = "inline-block";
    }

    if (sender != "txtCodigoDigitavel") {
        $("#txtCodigoDigitavel").val(total);
    }

}

function ChecaTxt(texto, textBoxID, ehNumerico, ehAlfabetico, ehAlfanumerico, checarVazio, qualCampo) {
    var corVermelho = "rgb(228, 88, 71)";
    var corBranca = "rgb(255, 255, 255)";
    var txtBox = document.getElementById(textBoxID);

    // texto vazio
    if (checarVazio == 1)

        if ((checarVazio == 1) && ($.trim(texto) == "")) {
            txtBox.style.backgroundColor = corVermelho;

            swal({
                type: 'error',
                title: 'Aviso',
                text: ('O ' + qualCampo + ' não pode ser vazio.')
            }).then(
                function () {
                    return false;
                });
            return false;
        };

    if (ehNumerico) {
        if (!isNaN($.trim(texto))) {
            txtBox.style.backgroundColor = corBranca;
            return true;
        }
        else {
            txtBox.style.backgroundColor = corVermelho;
            return false;
        }
    }
    else // checa ehAlfabetico
        if (ehAlfabetico) {
            var letters = /^[a-z\u00C0-\u00ff A-Z]+$/;
            if ($.trim(texto).match(letters)) {
                txtBox.style.backgroundColor = corBranca;
                return true;
            }
            else {
                txtBox.style.backgroundColor = corVermelho;
                return false;
            }
        }
        else // checa alfanumerico
        {
            if (ehAlfanumerico) {
                var letters = /^[a-zA-Z0-9]*$/;
                if ($.trim(texto).match(letters)) {
                    txtBox.style.backgroundColor = corBranca;
                    return true;
                }
                else {
                    txtBox.style.backgroundColor = corVermelho;
                    return false;
                }
            }
        }
}

function validaDocTecGeral()
{
    // checa cmbClasseProjeto
    var cmbTiposDocumentoIdx = document.getElementById("cmbClasseProjeto").selectedIndex;
    if (cmbTiposDocumentoIdx > 0) {

        // checa sequencial
        var txtSequencial = document.getElementById("txtSequencial");
        if (txtSequencial.value.trim() != "")
        {
            // checa revisao
            var txtRevisao = document.getElementById("txtRevisao");
            if (txtRevisao.value.trim() != "") 
            {
                txtRevisaovalue = txtRevisao.value;
                if ((txtRevisao.value.length == 1) && (isNaN(txtRevisaovalue.substring(0, 1)))
                    || ((txtRevisao.value.length == 2) && (isNaN(txtRevisaovalue.substring(0, 1))) && (!isNaN(txtRevisaovalue.substring(1, 1))))) 
                {
                    // checa a Descricao
                    var txtdoc_descricao = document.getElementById("txtdoc_descricao");
                    if (txtdoc_descricao.value.trim() != "") {
                        if (validaAlfaNumericoAcentosAfins(txtdoc_descricao, 0, 0))
                        //  if (txtdoc_descricao.value.trim() != "")
                        {
                            // checa o anexo
                            var txtdoc_caminho = document.getElementById("txtdoc_caminho");
                            if (txtdoc_caminho.value.trim() != "") {
                                // tudo ok entao retorna
                                return true;
                            }
                            else {
                                txtdoc_caminho.style.backgroundColor = corVermelho;
                                swal({
                                    type: 'error',
                                    title: 'Aviso',
                                    text: 'O anexo do documento é obrigatório.'
                                }).then(
                                    function () {
                                        $("#modalSalvarRegistro").modal('show');
                                        return false;
                                    });
                                return false;
                            }
                        }
                        else {
                            txtdoc_descricao.style.backgroundColor = corVermelho;
                            swal({
                                type: 'error',
                                title: 'Aviso',
                                text: 'A Descrição possui caracteres não permitidos'
                            }).then(
                                function () {
                                    $("#modalSalvarRegistro").modal('show');
                                    return false;
                                });
                            return false;;
                        }
                    }
                    else {
                        txtdoc_descricao.style.backgroundColor = corVermelho;
                        swal({
                            type: 'error',
                            title: 'Aviso',
                            text: 'A Descrição é obrigatória.'
                        }).then(
                            function () {
                                $("#modalSalvarRegistro").modal('show');
                                return false;
                            });
                        return false;
                    }
                }
                else
                {
                    txtRevisao.style.backgroundColor = corVermelho;
                    swal({
                        type: 'error',
                        title: 'Aviso',
                        text: 'Revisão é composta por 1 Letra ou 1 Letra + 1 Número'
                    }).then(
                        function () {
                            return false;
                        });
                    return false;
                }
            }
            else {
                    txtRevisao.style.backgroundColor = corVermelho;
                    swal({
                        type: 'error',
                        title: 'Aviso',
                        text: 'A Revisão é obrigatória.'
                    }).then(
                        function () {
                            return false;
                        });
                    return false;
            }
        }
        else {
                txtSequencial.style.backgroundColor = corVermelho;
                swal({
                    type: 'error',
                    title: 'Aviso',
                    text: 'O 2º Nível Grupo 2 (Sequencial) é obrigatório.'
                }).then(
                    function () {
                        return false;
                    });
                return false;
        }
    }
    else {
        document.getElementById("cmbClasseProjeto").style.backgroundColor = corVermelho;

        swal({
            type: 'error',
            title: 'Aviso',
            text: 'O 2º Nível Grupo 1 (Classe de Projeto) é obrigatório.'
        }).then(
            function () {
                return false;
            });

        return false;
    }
}

function validaDocTecEspecifico()
{
    // checa 2o nivel grupo 1
    var txtdoc_Grupo21 = document.getElementById("txtdoc_Grupo21");
    if (txtdoc_Grupo21.value.trim() != "") {

        // checa 2o nivel grupo 2A
        var txtdoc_Grupo22a = document.getElementById("txtdoc_Grupo22a");
        if (txtdoc_Grupo22a.value.trim() != "") {

            // checa 2o nivel grupo 2B
            var txtdoc_Grupo22b = document.getElementById("txtdoc_Grupo22b");
            if (txtdoc_Grupo22b.value.trim() != "") {

                // checa 2o nivel grupo 3
                var txtdoc_Grupo23 = document.getElementById("txtdoc_Grupo23");
                if (txtdoc_Grupo23.value.trim() != "") {

                    // daqui pra frente é igual ao DOC TEC GERAL
                    return  validaDocTecGeral();

                }
                else {
                    txtdoc_Grupo23.style.backgroundColor = corVermelho;
                    swal({
                        type: 'error',
                        title: 'Aviso',
                        text: 'O 2º Nível Grupo 3 é obrigatório.'
                    }).then(
                        function () {
                            return false;
                        });
                    return false;
                }
            }
            else {
                txtdoc_Grupo22b.style.backgroundColor = corVermelho;
                swal({
                    type: 'error',
                    title: 'Aviso',
                    text: 'O 2º Nível Grupo B - Km Final é obrigatório.'
                }).then(
                    function () {
                        return false;
                    });
                return false;
            }
        }
        else {
            txtdoc_Grupo22a.style.backgroundColor = corVermelho;
            swal({
                type: 'error',
                title: 'Aviso',
                text: 'O 2º Nível Grupo A - Km Inicial é obrigatório.'
            }).then(
                function () {
                    return false;
                });
            return false;
        }
    }
    else {
        txtdoc_Grupo21.style.backgroundColor = corVermelho;
        swal({
            type: 'error',
            title: 'Aviso',
            text: 'O 2º Nível Grupo 1 é obrigatório.'
        }).then(
            function () {
                return false;
            });
        return false;
    }

}


function Documento_Inserir() {

    limpaCampos();

    document.getElementById("divNaoAssociado").style.display = "block";

    ehInsercao = 1;

    $('#txtCodigoDigitavel').unmask();
    document.getElementById("divDocTecEspecificoNivel2").style.display = "none";
    MontaLblCodigo(0, "txtCodigoDigitavel");

    $("#modalSalvarRegistro").modal('show');
    document.getElementById("lblModalHeader").innerText = "Novo Documento";

    //document.getElementById('subGrids').style.visibility = "hidden";

    var cmbTiposDocumento = document.getElementById("cmbTiposDocumento");
    cmbTiposDocumento.options[cmbTiposDocumento.options.length - 1].disabled = true; // desabilita a opcao "NC"

    return false;
}

function Documento_Editar(id) {
    document.getElementById("lblModalHeader").innerText = "Editar Documento";
    ehInsercao = 0;

    $.ajax({
        url: "/Documento/Documento_GetbyID/" + id,
        type: "POST",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            // limpa
            limpaCampos();

            // campos comuns para qualquer tipo
            $("#cmbTiposDocumento").val(result.tpd_id);
            $('#txtdoc_codigo').val(result.doc_codigo);
            $('#txtCodigoDigitavel').val(result.doc_codigo.replace(" ", ""));
            $('#txtdoc_descricao').val(result.doc_descricao);
            $("#txtdoc_caminho").val(result.doc_caminho.split('/').pop());
            $("#txtdoc_caminho").attr('title', result.doc_caminho);
            $('#chkdoc_ativo').prop('checked', (result.doc_ativo == '1' ? true : false));


            if (result.tpd_id != "NC") // NC = nao codificado
            {
                document.getElementById("divNaoAssociado").style.display = "block";

                // preenche Combo Sequencial
                preencheCmbSequencial();

                // preenche combo Classe Projeto
                $("#cmbClasseProjeto").html("");
                $("#cmbClasseProjeto").append($('<option></option>').val(-1).html(" ")); // 1o item vazio
                $.each(result.lstClasseProjeto, function (i, subNivel) {
                    $("#cmbClasseProjeto").append($('<option></option>').val(subNivel.Value).html(subNivel.Text));
                });

                // preenche os outros campos
                $('#txtdoc_id').val(result.doc_id);

                $("#cmbClasseProjeto").val(result.doc_classe_projeto);
                $("#cmbDocClasse").val(result.dcl_id);
                $('#txtSequencial').val(result.doc_sequencial);
                $('#txtRevisao').val(result.doc_revisao);

                if (result.doc_codigo.length > 20) {
                    $('#txtdoc_Grupo21').val(result.doc_subNivel21);
                    $('#txtdoc_Grupo22a').val(result.doc_subNivel22a);
                    $('#txtdoc_Grupo22b').val(result.doc_subNivel22b);
                    $('#txtdoc_Grupo23').val(result.doc_subNivel23);
                    document.getElementById("divDocTecEspecificoNivel2").style.display = "block";

                    jQuery("#txtCodigoDigitavel").mask(mascaraDocumentoTecnicoEspecifico, options);
                    MontaLblCodigo(1);
                }
                else {
                    $('#txtdoc_Grupo21').val("");
                    $('#txtdoc_Grupo22a').val("");
                    $('#txtdoc_Grupo22b').val("");
                    $('#txtdoc_Grupo23').val("");
                    document.getElementById("divDocTecEspecificoNivel2").style.display = "none";

                    jQuery("#txtCodigoDigitavel").mask(mascaraDocumentoTecnicoGeral, options);
                    MontaLblCodigo(0);
                }

                var cmbTiposDocumento = document.getElementById("cmbTiposDocumento");
                cmbTiposDocumento.options[cmbTiposDocumento.options.length-1].disabled = true; //desabilita a opcao "NC"
                document.getElementById("txtCodigoDigitavel").disabled = false; //habilita o campo digitavel
            }
            else {
                var cmbTiposDocumento = document.getElementById("cmbTiposDocumento");
                cmbTiposDocumento.options[cmbTiposDocumento.options.length - 1].disabled = false; //habilita a opcao "NC"
                document.getElementById("txtCodigoDigitavel").disabled = true; //desabilita o campo digitavel
            }

            $("#modalSalvarRegistro").modal('show');
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
    return false;
}


function mouseON(NomeControle, simNao)
{
   var nodes = document.getElementById(NomeControle).getElementsByTagName('*');
   if (simNao) {
        // habilita os controles
        document.body.style.cursor = "default";
        for (var i = 0; i < nodes.length; i++) {
            nodes[i].disabled = false;
        }
    }
    else {
        // desabilita os controles
        document.body.style.cursor = "wait";

        for (var i = 0; i < nodes.length; i++) {
            nodes[i].disabled = true;
        }

    }
}

function Documento_Salvar() {

    mouseON("modalSalvarRegistro", false);

    var doctecGeralOK = false;
    var doctecEspecificoOK = false;

    var doctecNaoCodificadoOK = false;

    // identifica se é docTecGeral ou Especifico
    // valida 1o nivel - comum para os dois
    var DocTipoIdx = document.getElementById("cmbTiposDocumento").selectedIndex;
    var DocTipoValue = $('#cmbTiposDocumento').val();
    if (DocTipoIdx > 0) {
        if (DocTipoValue == "NC")
        {
            doctecGeralOK = true;
            doctecEspecificoOK = true;
            doctecNaoCodificadoOK = true;
        }
        else {
            if (DocTipoIdx <= 5) {
                doctecGeralOK = validaDocTecGeral();
                if (!doctecGeralOK) {
                    //habilita os controles
                    mouseON("modalSalvarRegistro", true);

                    return false;
                }
                doctecEspecificoOK = true;
                doctecNaoCodificadoOK = true;
            }
            else {
                doctecGeralOK = true;
                doctecEspecificoOK = validaDocTecEspecifico();
                doctecNaoCodificadoOK = true;
            }
        }
    }
    else
    {
        document.getElementById("cmbTiposDocumento").style.backgroundColor = corVermelho;
        swal({
            type: 'error',
            title: 'Aviso',
            text: 'O 1º Nível é obrigatório.'
        }).then(
            function () {
                //habilita os controles
                mouseON("modalSalvarRegistro", true);

                return false;
            });

        //habilita os controles
        mouseON("modalSalvarRegistro", true);

        return false;
    }

    //var info = $('#tblDocumentos').DataTable().page.info();
    //var pagina_atual = info.page;

    // continua e salva
    if (doctecGeralOK && doctecEspecificoOK && doctecNaoCodificadoOK) {
        // Checa REPETICOES e salva
        var params = {
            "doc_id": null,
            "doc_codigo": $('#txtdoc_codigo').val().trim(),
            "doc_descricao": '',
            "tpd_id": '',
            "dcl_codigo": ''
        };
        $.ajax({
            type: "GET",
            "url": "/Documento/Documento_ListAll",
            "data": params,
            contentType: "application/json; charset=utf-8",
            dataType: 'json',
            success: function (result) {
                if ((result.data.length == 0) // nao tem repetido
                    || (   (ehInsercao == 0) // está alterando o mesmo registro
                        && (result.data.length > 0)
                        && (parseInt(selectedId) == parseInt(result.data[0]["doc_id"]))
                        && (obj_codigo_old == $('#txtdoc_codigo').val().trim()) 
                    )
                    || ($('#cmbTiposDocumento').val() == "NC") 
                ) {

                    var params_doc = {
                        doc_id: $('#txtdoc_id').val(),
                        tpd_id: document.getElementById("cmbTiposDocumento").options[document.getElementById("cmbTiposDocumento").selectedIndex].value, // $('#cmbTiposDocumento').val(),
                        doc_codigo: $('#txtdoc_codigo').val().toUpperCase(),
                        doc_descricao: $('#txtdoc_descricao').val(),
                        doc_caminho: $('#txtdoc_caminho').attr("title"),
                        doc_ativo: $('#chkdoc_ativo').prop('checked') ? 1 : 0,
                        dcl_id: $('#cmbClasseProjeto').val()
                    };

                    var doc_codigo_filtro = $('#txtFiltroDoc_Codigo').val().trim();
                    var doc_descricao_filtro = $('#txtFiltroDoc_Descricao').val().trim();
                    var tpd_id_filtro = $('#cmbFiltroTipoDocumento :selected').val();
                    var dcl_codigo_filtro = $('#cmbFiltroClasseProjeto :selected').val();

                    var limpar = false;
                    // se o documento inserido nao fizer parte do filtro, entao limpa o filtro
                    if ((doc_codigo_filtro != "") && (doc_codigo.indexOf(doc_codigo_filtro) < 0))
                    {
                        $('#txtFiltroDoc_Codigo').val("");
                        doc_codigo_filtro = "";
                        limpar = true;
                    }

                    if ((doc_descricao_filtro != "") && (doc_descricao.indexOf(doc_descricao_filtro) < 0))
                    {
                        $('#txtFiltroDoc_Descricao').val("");
                        doc_descricao_filtro = "";
                        limpar = true;
                    }

                    if ((tpd_id_filtro != "") && ($('#cmbTiposDocumento').val() != tpd_id_filtro))
                    {
                        $('#cmbFiltroTipoDocumento').val(null);
                        tpd_id_filtro = "";
                        limpar = true;
                    }

                    if ((dcl_codigo_filtro != "") && ($('#cmbClasseProjeto').val() != dcl_codigo_filtro))
                    {
                        $('#cmbClasseProjeto').val(null);
                        dcl_codigo_filtro = "";
                        limpar = true;                        
                    }
 
                  
                    var table = $('#tblDocumentos').DataTable();
                    var info = table.page.info();
                    var qt_por_pagina = info.length;

                    var order = table.order();
                    var ordenado_por = 'doc_codigo asc';

                    switch (order[0][0]) {
                        case "": ordenado_por = "doc_codigo " + order[0][1]; break;
                        case "0": ordenado_por = "tpd_id " + order[0][1]; break;
                        case "1": ordenado_por = "doc_id " + order[0][1]; break;
                        case "2": ordenado_por = "doc_codigo " + order[0][1]; break;
                        case "3": ordenado_por = "dcl_codigo " + order[0][1]; break;
                        case "4": ordenado_por = "tpd_descricao " + order[0][1]; break;
                        case "5": ordenado_por = "doc_descricao " + order[0][1]; break;
                        case "6": ordenado_por = "doc_caminho " + order[0][1]; break;
                        default: ordenado_por = 'doc_codigo asc';
                    }

                    // atualiza o grid sem filtros
                    if (limpar == true)
                        $('#tblDocumentos').DataTable().ajax.reload(null, true);  //false if you don't want to refresh paging else true.

                    param = JSON.stringify({ 'doc': params_doc, 'doc_codigo_filtro': doc_codigo_filtro, 'doc_descricao_filtro': doc_descricao_filtro, tpd_id_filtro: tpd_id_filtro, dcl_codigo_filtro: dcl_codigo_filtro, qt_por_pagina: qt_por_pagina, ordenado_por: ordenado_por  });

                    $.ajax({
                        url: "/Documento/Documento_Salvar",
                        data: param,
                        type: "POST",
                        contentType: "application/json;charset=utf-8",
                        dataType: "json",
                        success: function (result) {

                            if (ehInsercao == 0) {
                              $('#tblDocumentos').DataTable().ajax.reload(null, false);  //false if you don't want to refresh paging else true.
                            }
                            else
                            {
                                var str = result.split(":");
                                var pagina = parseInt(str[1]);
                                selectedId = parseInt(str[0]);

                                var table = $('#tblDocumentos').DataTable();

                                // posiciona a pagina
                                table.page(pagina).draw('page');

                                // deseleciona todas as linhas
                                $('#tblDocumentos tbody tr').removeClass('selected');

                                // seleciona a linha pelo rowid = doc_id
                                var tblDocumentos = $('#tblDocumentos');
                                var row = tblDocumentos.find('tr[data-row-id="' + selectedId + '"]')
                                row.addClass('selected');
                            }

                            // atualiza os grids de baixo
                            var textoHeaderDocumentoObjetos = "Objetos Associados ao Documento: " + $('#txtdoc_codigo').val().toUpperCase();
                            document.getElementById('HeaderObjetosAssociados').innerText = textoHeaderDocumentoObjetos;

                            var textoHeaderDocumentoOSs = "OSs Associadas ao Documento: " + $('#txtdoc_codigo').val().toUpperCase();
                            document.getElementById('HeaderOSsAssociadas').innerText = textoHeaderDocumentoOSs;

                            $('#tblObjetosAssociados').DataTable().ajax.reload(null, false);  //false if you don't want to refresh paging else true.
                            $('#tblOSsAssociadas').DataTable().ajax.reload(null, false); 

                            ehInsercao = 0;
                            $("#modalSalvarRegistro").modal('hide');

                            //habilita os controles
                            mouseON("modalSalvarRegistro", true);

                            return false;
                        },
                        error: function (errormessage) {
                            ehInsercao = 0;
                            alert(errormessage.responseText);

                            //habilita os controles
                            mouseON("modalSalvarRegistro", true);

                            return false;
                        }
                    });
                }
                else {
                    $("#modalSalvarRegistro").modal('show');

                    swal({
                        type: 'error',
                        title: 'Aviso',
                        text: 'Código de Documento já cadastrado'
                    }).then(
                        function () {
                            ehInsercao = 0;

                            //habilita os controles
                            mouseON("modalSalvarRegistro", true);
                            return false;
                        });

                    //habilita os controles
                    mouseON("modalSalvarRegistro", true);

                    return false;
                }
            },
            error: function (xhr, status, err) {
                alert(err.toString(), 'Error - LoadListItemsHelper');
                //habilita os controles
                mouseON("modalSalvarRegistro", true);

            }
        });

    }

    ////habilita os controles
    //mouseON("modalSalvarRegistro", true);
    return false;
}

function Documento_Excluir(id) {
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
            var response = POST("/Documento/Documento_Excluir", JSON.stringify({ id: id }))
            if (response.erroId >= 1) {
                swal({
                    type: 'success',
                    title: 'Sucesso',
                    text: 'Registro excluído com sucesso'
                });

                $('#tblDocumentos').DataTable().ajax.reload();
            }
            else {
                swal({
                    type: 'error',
                    title: 'Aviso',
                    text: 'Erro ao excluir registro'
                });
            }
            return false;
        } else {
            return false;
        }
    })
    return false;
}

function Documento_AtivarDesativar(id, ativar) {
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
                var response = POST("/Documento/Documento_AtivarDesativar", JSON.stringify({ id: id }))
                if (response.erroId == 1) {
                    swal({
                        type: 'success',
                        title: 'Sucesso',
                        text: ativar == 1 ? msgAtivacaoOK : msgDesativacaoOK
                    }).then(
                        function () {
                            $('#tblDocumentos').DataTable().ajax.reload();
                            return false;
                        });

                }
                else {
                    swal({
                        type: 'error',
                        title: 'Aviso',
                        text: ativar == 1 ? msgAtivacaoErro : msgDesativacaoErro
                    });
                }
                return false;
            } else {
                return false;
            }
        })
    }

    return false;
}


function Documento_Filtrar() {
    selectedId = -1;

    $('#tblDocumentos').DataTable().ajax.reload(null, true);  //false if you don't want to refresh paging else true.
    $('#tblObjetosAssociados').DataTable().ajax.reload(null, true);  //false if you don't want to refresh paging else true.
    $('#tblOSsAssociadas').DataTable().ajax.reload(null, true); 
    document.getElementById('subGrids').style.visibility = "hidden";

    return false;
}

function Documento_LimparFiltro() {
    selectedId = -1;

    $('#txtFiltroDoc_Codigo').val(null);
    $('#txtFiltroDoc_Descricao').val(null);
    $('#cmbFiltroClasseProjeto').val("");
    $('#cmbFiltroTipoDocumento').val("");

    $('#tblDocumentos').DataTable().ajax.reload(null, true);  //false if you don't want to refresh paging else true.

    return false;
}

function cmbTiposDocumento_onchange() {

    document.getElementById("cmbTiposDocumento").style.backgroundColor = corBranca;

    $('#txtdoc_codigo').val("");

    var DocTipo = $('#cmbTiposDocumento').val();
    $('#cmbClasseProjeto').val("");
    $('#txtSequencial').val("");


    var DocTipoIdx = document.getElementById("cmbTiposDocumento").selectedIndex;
    var DocTipoValue = $('#cmbTiposDocumento').val();
    if (DocTipoValue == "NC")
    {
        document.getElementById("divNaoAssociado").style.display = "none";
        document.getElementById("txtCodigoDigitavel").disabled = true;
        document.getElementById("txtCodigoDigitavel").value = "";

    }
    else {
        document.getElementById("divNaoAssociado").style.display = "block";
        document.getElementById("txtCodigoDigitavel").disabled = false;

        if (DocTipoIdx <= 5)   // DOCUMENTO TECNICO GERAL
        {
            document.getElementById("divDocTecEspecificoNivel2").style.display = "none";
            MontaLblCodigo(0);

            if (DocTipoIdx == 0)
                $('#txtdoc_codigo').val("");
        }
        else {
            document.getElementById("divDocTecEspecificoNivel2").style.display = "block";
            MontaLblCodigo(1);
        }
    }

    $.ajax({
        url: '/Documento/PreencheCmbClasseProjeto',
        type: "POST",
        dataType: "JSON",
        data: { tipo: DocTipo },
        success: function (lstSubNiveis) {

            $("#cmbClasseProjeto").html(""); // clear before appending new list
            $("#cmbClasseProjeto").append($('<option selected ></option>').val(-1).html("Selecionar Classe de Projeto")); // 1o item vazio
            $.each(lstSubNiveis, function (i, subNivel) {
                $("#cmbClasseProjeto").append($('<option></option>').val(subNivel.Value).html(subNivel.Text));
            });

        }
    });
}
function cmbClasseProjeto_onchange() {

    document.getElementById("cmbClasseProjeto").style.backgroundColor = corBranca;

    var DocTipoIdx = document.getElementById("cmbTiposDocumento").selectedIndex;
    var DocTipoValue = $('#cmbTiposDocumento').val();
    if (DocTipoValue != "NC")
    {
        if (DocTipoIdx <= 5)  // DOCUMENTO TECNICO GERAL
            MontaLblCodigo(0);
        else
            MontaLblCodigo(1);

        // preenche o cmbo sequencial
        preencheCmbSequencial();
    }
}

function preencheCmbSequencial() {
    $('#txtSequencial').val("");
    $('#listSequencial').val("");
    $("#listSequencial").html(""); // clear before appending new list
    for (var i = 1; i <= 100; i++) {
        $("#listSequencial").append($('<option></option>').val(('00' + i).slice(-3)));
    }
}

function txtSequencial_onKeyUP(txtSequencial) {

    txtSequencial.style.backgroundColor = corBranca;

    if ($.trim(txtSequencial.value) == "000")
        txtSequencial.style.backgroundColor = corVermelho;
    else {
      if (validaNumero(txtSequencial, 0)) {
          var DocTipoIdx = document.getElementById("cmbTiposDocumento").selectedIndex;
              if (DocTipoIdx <= 5)
                  MontaLblCodigo(0);// DOCUMENTO TECNICO GERAL
              else
                  MontaLblCodigo(1);// DOCUMENTO TECNICO especifico
        }
    }
}
function txtRevisao_onKeyUP(txt) {

    txt.style.backgroundColor = corBranca;

    var DocTipoIdx = document.getElementById("cmbTiposDocumento").selectedIndex;
    if (DocTipoIdx <= 5)
        MontaLblCodigo(0);// DOCUMENTO TECNICO GERAL
    else
        MontaLblCodigo(1);// DOCUMENTO TECNICO especifico
}
function txtdoc_descricao_onKeyUP(txt) {
    txt.style.backgroundColor = corBranca;
    validaAlfaNumericoAcentosAfins(txt, 0,0);

    var cmbTiposDocumento = document.getElementById("cmbTiposDocumento");
    if (cmbTiposDocumento.selectedIndex == cmbTiposDocumento.options.length -1)
    {
        document.getElementById("txtCodigoDigitavel").value = txt.value;
    }

    if (txt.value.trim() == "")
        txt.style.backgroundColor = corBranca;

}
function txtdoc_Grupo22a_onKeyUP(txt) {

    txt.style.backgroundColor = corBranca;

    var DocTipoIdx = document.getElementById("cmbTiposDocumento").selectedIndex;
    if (DocTipoIdx <= 5)
        MontaLblCodigo(0);// DOCUMENTO TECNICO GERAL
    else
        MontaLblCodigo(1);// DOCUMENTO TECNICO especifico
}
function txtdoc_Grupo22b_onKeyUP(txt) {

    txt.style.backgroundColor = corBranca;

    var DocTipoIdx = document.getElementById("cmbTiposDocumento").selectedIndex;
    if (DocTipoIdx <= 5)
        MontaLblCodigo(0);// DOCUMENTO TECNICO GERAL
    else
        MontaLblCodigo(1);// DOCUMENTO TECNICO especifico
}
function txtdoc_Grupo23_onKeyUP(txt) {

    txt.style.backgroundColor = corBranca;

    var DocTipoIdx = document.getElementById("cmbTiposDocumento").selectedIndex;
    if (DocTipoIdx <= 5)
        MontaLblCodigo(0);// DOCUMENTO TECNICO GERAL
    else
        MontaLblCodigo(1);// DOCUMENTO TECNICO especifico
}

function txtAlfaNumerico_onKeyUP(txt, qualGrupo) {

    if (validaAlfaNumerico(txt, 0)) {

        txt.value = txt.value.toUpperCase();
        MontaLblCodigo(1);
    }

}
function txtNumerico_onKeyUP(txt, qualGrupo) {

    if (validaNumero(txt, 0)) {
        MontaLblCodigo(1);
    }

}


function txtCodigoDigitavel_onKeyUP(valorColado) {

    var valor = $("#txtCodigoDigitavel").val();
    if (valor.trim().length >= 2) {
        var prefx = valor.toUpperCase().substring(0, 2);
        ////if ((prefx == "NC"))
        ////{
        ////    document.getElementById("divNaoAssociado").style.display = "none";
        ////    $('#txtCodigoDigitavel').unmask();
        ////    document.getElementById("cmbTiposDocumento").selectedIndex = 1;
        ////    return;
        ////}


        // checa os 2 primeiros digitos e coloca a mascara correspondente
        if ((prefx == "ET") || (prefx == "IP") || (prefx == "PP") || (prefx == "NT")) {
            jQuery("#txtCodigoDigitavel").mask(mascaraDocumentoTecnicoGeral, options);

            document.getElementById("divNaoAssociado").style.display = "block";
       }
        else {
            jQuery("#txtCodigoDigitavel").mask(mascaraDocumentoTecnicoEspecifico, options);
            document.getElementById("divNaoAssociado").style.display = "block";
        }
    }
    else {
        $('#txtCodigoDigitavel').unmask();
        return;
    }

    var corVermelho = "rgb(228, 88, 71)";
    var corBranca = "rgb(255, 255, 255)";
    $("#txtCodigoDigitavel").val(($("#txtCodigoDigitavel").val()).toUpperCase());

    var valorSemMascara = $("#txtCodigoDigitavel").cleanVal();
    var txtBox = document.getElementById("txtCodigoDigitavel");
    var DocTipoIdx = document.getElementById("cmbTiposDocumento").selectedIndex;
    var DocTipo = valorSemMascara.substring(0, 2).toUpperCase();


    txtBox.style.backgroundColor = corBranca;

    // posiciona os combos
    if (valorSemMascara.length >= 2) {
        txtBox.style.backgroundColor = corBranca;

        //checa os 2 primeiros digitos
        var tem = $("#cmbTiposDocumento option[value='" + DocTipo + "']").length > 0 && (prefx != "NC");
        if (tem > 0) {
            // posiciona o combo
            $('#cmbTiposDocumento').val(DocTipo);
            DocTipoIdx = document.getElementById("cmbTiposDocumento").selectedIndex;

            $.ajax({
                url: '/Documento/PreencheCmbClasseProjeto',
                type: "POST",
                dataType: "JSON",
                data: { tipo: DocTipo },
                success: function (lstSubNiveis) {

                    $("#cmbClasseProjeto").html(""); // clear before appending new list
                    $("#cmbClasseProjeto").append($('<option selected ></option>').val(-1).html("Selecionar Classe de Projeto")); // 1o item vazio
                    $.each(lstSubNiveis, function (i, subNivel) {
                        $("#cmbClasseProjeto").append($('<option></option>').val(subNivel.Value).html(subNivel.Text));
                    });

                    if (DocTipoIdx <= 5)   // DOCUMENTO TECNICO GERAL
                    {
                        jQuery("#txtCodigoDigitavel").mask(mascaraDocumentoTecnicoGeral, options);
                        document.getElementById("divDocTecEspecificoNivel2").style.display = "none";
                    }
                    else {
                        jQuery("#txtCodigoDigitavel").mask(mascaraDocumentoTecnicoEspecifico, options);
                        document.getElementById("divDocTecEspecificoNivel2").style.display = "block";
                    }

                    // DOCUMENTO TECNICO GERAL
                    if (DocTipoIdx <= 5) {

                        // 2o nivel = 1o grupo
                        if (valorSemMascara.length >= 8) {
                            var doc_classe_projeto = valorSemMascara.substr(2, 3);
                            tem = $("#cmbClasseProjeto option[value='" + doc_classe_projeto + "']").length > 0;
                            if (tem) {
                                $('#cmbClasseProjeto').val(doc_classe_projeto);
                            }
                            else {
                                txtBox.style.backgroundColor = corVermelho;
                            }
                        }

                        //  3o nivel = Sequencial
                        var sequencial = valorSemMascara.substr(5, 3);
                        if (sequencial == "000")
                            txtBox.style.backgroundColor = corVermelho;
                        $('#txtSequencial').val(sequencial)

                        //  3o nivel = Revisao
                        var revisao = valorSemMascara.substr(8, 2);
                        $('#txtRevisao').val(revisao)


                        MontaLblCodigo(0, "txtCodigoDigitavel");// zero = DOCUMENTO TECNICO GERAL
                    }
                    else // documento tecnico especifico
                    {
                        // 2o nivel
                        if (valorSemMascara.length >= 3) {
                            var doc_subNivel21 = valorSemMascara.substring(2, 50);
                            if (doc_subNivel21.length > 9)
                                doc_subNivel21 = valorSemMascara.substr(2, 9);
                            $('#txtdoc_Grupo21').val(doc_subNivel21);

                            var doc_subNivel22a = valorSemMascara.substring(11, 14);
                            if (doc_subNivel22a.length < 14)
                                doc_subNivel22a = valorSemMascara.substr(11, 3);
                            $('#txtdoc_Grupo22a').val(doc_subNivel22a);

                            var doc_subNivel22b = valorSemMascara.substring(14, 17);
                            if (doc_subNivel22b.length < 17)
                                doc_subNivel22b = valorSemMascara.substr(14, 3);
                            $('#txtdoc_Grupo22b').val(doc_subNivel22b);

                            var doc_subNivel23 = valorSemMascara.substring(17, 21);
                            if (doc_subNivel23.length < 21)
                                doc_subNivel23 = valorSemMascara.substr(17, 3);
                            $('#txtdoc_Grupo23').val(doc_subNivel23);


                            // 3o nivel = 1o grupo
                            if (valorSemMascara.length > 21) {
                                var doc_classe_projeto = valorSemMascara.substr(20, 3);
                                tem = $("#cmbClasseProjeto option[value='" + doc_classe_projeto + "']").length > 0;
                                if (tem) {
                                    $('#cmbClasseProjeto').val(doc_classe_projeto);
                                }
                                else {
                                    txtBox.style.backgroundColor = corVermelho;
                                }
                            }

                            //  3o nivel = Sequencial
                            var sequencial = valorSemMascara.substr(23, 3);
                            if (sequencial == "000")
                                txtBox.style.backgroundColor = corVermelho;
                            $('#txtSequencial').val(sequencial)

                            //  3o nivel = Revisao
                            if (valorSemMascara.length >= 26) {
                                var revisao = valorSemMascara.substr(26, 2);
                                $('#txtRevisao').val(revisao);

                                if (revisao.length < 2)
                                    txtBox.style.backgroundColor = corVermelho;
                            }
                        }

                            MontaLblCodigo(1, "txtCodigoDigitavel");
                    }

                }
            });

        }
        //else {
        //    txtBox.style.backgroundColor = corVermelho;
        //}
    }



}

// ************* ARQUIVOS ********************************************

function Selecionar(caminho, somentePastas) {
    if (somentePastas == 1) {
        // decodifica os acentos e espacos do html
        caminho = decodeURIComponent(caminho);

        if (caminho.endsWith("/"))
            caminho = caminho.substring(0, caminho.length - 1);

        $("#txtdoc_caminho3").val(caminho.split('/').pop());
        $("#txtdoc_caminho3").attr('title', caminho);

        document.getElementById("txtdoc_caminho").style.backgroundColor = corBranca;

        $("#modalListarPastasServidor").modal('hide');
    }
    else {
        $("#txtdoc_caminho").val(caminho.split('/').pop());
        $("#txtdoc_caminho").attr('title', caminho);
        $("#modalListarArquivos").modal('hide');

        document.getElementById("txtdoc_caminho").style.backgroundColor = corBranca;
    }
}

function ListarArquivos(url2, somentePastas) {
    if (url2 == null)
        url2 = "";


    $("#txtLocalizarArquivos").val("");

    $.ajax({
        url: "/Documento/Documento_ListaArquivosWeb",
        data: JSON.stringify({ caminho: url2 }),

        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {

            // Titulo
            if (somentePastas == 1)
                $("#txtTituloBrowser2").text(result.titulo);
            else
                $("#txtTituloBrowser").text(result.titulo);

            // Diretorios
            if (somentePastas == 1) {
                $('#tblPastasServidor').DataTable().destroy();
                $('#tblPastasServidor').DataTable({
                    "data": result.ListaDiretorios
                    , "columns": [
                        {
                            "title": "Pastas",
                            data: "Target",
                            "searchable": false,
                            "sortable": false,
                            "render": function (data, type, row, meta) {
                                var retorno = "";
                                var caminho = "'" + data + "'";
                                //  if (meta.row == 0) // entao target= "parent directory"
                                if (row["Texto"] == "Voltar")
                                    retorno += '<i class="fa fa-level-up fa-flip-horizontal" aria-hidden="true"></i><a href="#" style="margin-left:3px;" onclick="ListarArquivos(' + caminho + ',1 )"  title="Ir para Pasta" >' + row["Texto"] + '</a>' + '  ';
                                else {

                                    retorno += '<input type="checkbox" id="chkDocumento" name="chkDocumento" onclick="return Selecionar(' + caminho + ',1)"  style="margin-left:13px;" ><i class="fa fa-folder-o" aria-hidden="true" style="margin-left:5px;" ></i><a href="#" style="margin-left:3px;" onclick="ListarArquivos(' + caminho + ',1 )"  title="Ir para Pasta" >' + row["Texto"] + '</a>' + '  ';
                                }
                                return retorno;
                            }
                        }
                    ]
                    , searching: false
                    , "ordering": false
                    , "lengthMenu": [[10, 25, 50, 100], [10, 25, 50, 100]]
                    , select: { style: 'single' }
                    , paging: false, info: false
                    , "oLanguage": idioma
                    , "pagingType": "input"
                    , "sDom": '<"top">rt<"bottom"pfli><"clear">'
                });
            }
            else {
                $('#tblPastas').DataTable().destroy();
                $('#tblPastas').DataTable({
                    "data": result.ListaDiretorios
                    , "columns": [
                        {
                            "title": "Pastas",
                            data: "Target",
                            "searchable": false,
                            "sortable": false,
                            "render": function (data, type, row, meta) {
                                var retorno = "";
                                var caminho = "'" + data + "'";
                                //  if (meta.row == 0) // entao target= "parent directory"
                                if (row["Texto"] == "Voltar")
                                    retorno += '<i class="fa fa-level-up fa-flip-horizontal" aria-hidden="true"></i><a href="#" style="margin-left:3px;" onclick="ListarArquivos(' + caminho + ')"  title="Ir para Pasta" >' + row["Texto"] + '</a>' + '  ';
                                else
                                    retorno += '<i class="fa fa-folder-o" aria-hidden="true" style="margin-left:13px;" ></i><a href="#" style="margin-left:3px;" onclick="ListarArquivos(' + caminho + ')"  title="Ir para Pasta" >' + row["Texto"] + '</a>' + '  ';
                                return retorno;
                            }
                        }
                    ]
                    , searching: false
                    , "ordering": false
                    , "lengthMenu": [[10, 25, 50, 100], [10, 25, 50, 100]]
                    , select: { style: 'single' }
                    , paging: false, info: false
                    , "oLanguage": idioma
                    , "pagingType": "input"
                    , "sDom": '<"top">rt<"bottom"pfli><"clear">'
                });

                // Arquivos
                $('#tblArquivos').DataTable().destroy();
                $('#tblArquivos').DataTable({
                    "data": result.ListaArquivos
                    , "columns": [
                        { data: "Target", "className": "hide_column", "searchable": false },
                        {
                            data: "Texto",
                            "autoWidth": true,
                            "searchable": true,
                            "sortable": false,
                            "render": function (data, type, row) {
                                var retorno = "";
                                var caminho = "'" + row["Target"] + "'";
                                //  retorno += '<input type="checkbox" onclick="return Selecionar(' + caminho + ')"  title="Selecionar" /><label >Deslizante(empurrada)</label>' + '  ';

                                retorno += '<div><input type="checkbox" id="chkDocumento" name="chkDocumento" onclick="return Selecionar(' + caminho + ')" ><label  title="Selecionar" style="margin-left:3px" for="chkDocumento" >  ' + row["Texto"] + '</label></div>';
                                return retorno;
                            }
                        },
                        {
                            data: "Target",
                            "searchable": false,
                            "sortable": false,
                            "render": function (data, type, row) {
                                var retorno = "";
                                var caminho = "'" + data + "'";
                                //  retorno += '<a href="#" onclick="return Selecionar(' + caminho + ')"  title="Selecionar" ><span class="glyphicon glyphicon-unchecked"></span></a>' + '  ';
                                retorno += '<a href="#" onclick="window.open(' + caminho + ')"  title="Abrir" ><span class="fa fa-search"></span></a>' + '  ';

                                return retorno;
                            }
                        }
                    ]
                    , searching: true
                    , "ordering": false
                    , "rowId": "Target"
                    , "lengthMenu": [[10, 25, 50, 100], [10, 25, 50, 100]]
                    , select: true // { style: 'single' },
                    // , paging: false, info: false
                    , "oLanguage": idioma
                    , "pagingType": "input"
                    , "sDom": '<"top">rt<"bottom"pfli><"clear">'
                });

                var tblArquivos = $('#tblArquivos').DataTable();
                $('#tblArquivos tbody').on('click', 'tr', function () {
                    return false;
                    //var caminho = this.cells[0].innerText; // = tblArquivos.row(this).data();
                    //Selecionar(caminho );
                });
            }

            return false;
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
            // $("#modalAssociarOS").modal('show');
            return false;
        }
    });

    if (somentePastas == 1)
        $('#modalListarPastasServidor').modal('show');
    else
        $('#modalListarArquivos').modal('show');

    return false;
}

function btn_NovaPasta_click() {
    var caminho = document.getElementById("txtTituloBrowser2").innerText; // "192.168.15.11 - /UPLOAD/"

    // faz um tratamento 
    caminho = caminho.replace("https://", "").replace("http://", "");
    caminho = caminho.substring(caminho.indexOf("/"), 1000); // "/UPLOAD/"

    swal({
        text: "Nome da Pasta",
        content: "input", buttons: ["Cancelar", "OK"],
        closeOnConfirm: true
    }).then((nomePasta) => {
            // alert(nomePasta);
        if (nomePasta != null) 
        {
            var params = {
                "caminhoVirtual": caminho,
                "nomePasta": nomePasta.trim()
            };
            $.ajax({
                type: "POST",
                "url": "/Documento/Documento_NovaPasta",
                "data": JSON.stringify(params),
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                success: function (result) {
                    if (result.erro.startsWith("http")) // OK
                    {
                        swal({
                            type: 'success',
                            title: 'Sucesso',
                            text: 'Pasta Criada com sucesso'
                        });

                        ListarArquivos(result.erro, 1);
                    }
                    else {

                        swal({
                            type: 'error',
                            title: 'Aviso',
                            text: ('Erro na criação da pasta ' + nomePasta + ':' + result.data)
                        }).then(
                            function () {
                                return false;
                            });

                    }
                    return false;
                },
                error: function (xhr, status, err) {
                    alert(err.toString(), 'Error');
                }
            });
        }
        });

    return false;
}

function UploadArquivo(imgBrowseCtl) {

    //campo "DE" vazio
    if ($('#txtdoc_caminho2').val().trim() == "") {
        swal({
            type: 'error',
            title: 'Aviso',
            text: "Selecione o arquivo para upload"
        });
        return false;
    }

    //campo "para" vazio
    if ($('#txtdoc_caminho3').val().trim() == "") {
        swal({
            type: 'error',
            title: 'Aviso',
            text: "Selecione a pasta do Servidor para o upload"
        });
        return false;
    }





    var file = $('#' + imgBrowseCtl).get(0).files;
    var pastaDestino = $('#txtdoc_caminho3').attr('title');

    var porcentagem = 0;
    let timerInterval
    Swal.fire({
        title: 'Enviando arquivo para o servidor',
        html: '<b></b>%',
        timer: 200000,
        timerProgressBar: false,
        allowOutsideClick: false,
        onBeforeOpen: () => {
            Swal.showLoading()
            timerInterval = setInterval(() => {
                const content = Swal.getContent()
                if (content) {
                    const b = content.querySelector('b')
                    if (b) {
                        b.textContent = porcentagem;
                    }
                }
            }, 100)
        },
        onClose: () => {
            clearInterval(timerInterval)
            return;
        }
    }).then((result) => {
        Swal.close();
        return;
    })

    var data = new FormData;
    data.append("Arquivo", file[0]);
    data.append("CaminhoServidor", pastaDestino);

    $.ajax({
        xhr: function () {
            var xhr = new window.XMLHttpRequest();
            xhr.upload.addEventListener("progress", function (evt) {
                if (evt.lengthComputable) {
                    var percentComplete = evt.loaded / evt.total;
                    percentComplete = parseInt(percentComplete * 100);

                    porcentagem = percentComplete;
                    if (percentComplete === 100) {
                        Swal.close();
                    }

                }
            }, false);
            return xhr;
        },

        type: "Post",
        url: "/Documento/Documento_Upload",
        data: data,
        contentType: false,
        processData: false,
        success: function (response) {
            if (response == "") {
                //alert("ok");
                $('#txtdoc_caminho').val($('#txtdoc_caminho2').val().replace(/ /g, "_")); // nome do arquivo

                $('#txtdoc_caminho2').val("");// nome do arquivo fisico
                $('#txtdoc_caminho3').val(""); // pasta destino

                // caminho completo virtual
                var caminhoArquivo = $('#txtdoc_caminho3').attr('title') + "/" + $('#txtdoc_caminho').val();
                $('#txtdoc_caminho').attr('title', caminhoArquivo);
                $('#txtdoc_caminho3').attr('title', "");

                document.getElementById("txtdoc_caminho").style.backgroundColor = corBranca;

                swal({
                    type: 'success',
                    title: 'Sucesso',
                    text: 'Arquivo salvo com sucesso'
                });

                return false;
                ////if ($("#imgUsuario").length > 0) { // se tem imgUsuario, significa grid de Usuario
                ////    document.getElementById("imgUsuario").setAttribute('src', src);

                ////    $("#imgUsuario").attr("src", src);
                ////    $('#txtusu_foto').val(src);
                ////    $('#tblUsuarios').DataTable().ajax.reload(null, false);
                ////}
            }
            else {
                swal({
                    type: 'error',
                    title: 'Aviso',
                    text: response
                });
                return false;
            }
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
            return false;
        }
    })

    return false;
}



// ************* ASSOCIACAO A OBJETO ********************************************

function AssociarObjeto() {

    if (selectedId >= 0) {
        // limpa o texbox de input
        $("#txtLocalizarObjeto").val("");

        // limpa a lista de documentos
        $("#divObjetosLocalizados").empty();

        document.getElementById("txtLocalizarObjeto").focus();
        $("#modalAssociarObjeto").modal('show');
    }
    else {
        swal({
            type: 'error',
            title: 'Aviso',
            text: 'Nenhum documento selecionado'
        }).then(
            function () {
                return false;
            });
    }
}

function DesassociarObjeto(objID) {

    if (objID >= 0) {
        var form = this;

        swal({
            title: "Desassociar Objeto. Tem certeza?",
            icon: "warning",
            buttons: [
                'Não',
                'Sim'
            ],
            dangerMode: true,
            focusCancel: true
        }).then(function (isConfirm) {
            if (isConfirm) {
                var response = POST("/Documento/Documento_DesassociarObjeto", JSON.stringify({ "doc_id": selectedId, "obj_id": objID }))
                if (response.erroId >= 1) {
                    swal({
                        type: 'success',
                        title: 'Sucesso',
                        text: 'Objeto Desassociado com Sucesso'
                    });

                    $('#tblObjetosAssociados').DataTable().ajax.reload(null, false);  //false if you don't want to refresh paging else true.
                }
                else {
                    swal({
                        type: 'error',
                        title: 'Aviso',
                        text: 'Erro ao Desassociar registro'
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

function Documento_AssociarObjeto_Salvar() {

    // cria lista dos IDs de Objetos selecionados
    var selchks = [];
    var obj_ids = "";
    $('#divObjetosLocalizados input:checked').each(function () {
        selchks.push($(this).attr('value'));
    });

    for (var i = 0; i < selchks.length; i++)
        if (i == 0)
            obj_ids = selchks[i];
        else
            obj_ids = obj_ids + ";" + selchks[i];

    obj_ids = obj_ids + ";"; // acrescenta um delimitador no final da string

    if (selchks.length > 0) {
        var associacao = {
            doc_id: selectedId,
            obj_ids: obj_ids
        };

        $.ajax({
            url: "/Documento/Documento_AssociarObjetos",
            data: JSON.stringify(associacao),
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                $("#modalAssociarObjeto").modal('hide');
                $('#tblObjetosAssociados').DataTable().ajax.reload(null, false);  //false if you don't want to refresh paging else true.

                return false;
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
                $("#modalAssociarObjeto").modal('show');
                return false;
            }
        });
    }
    else {
        swal({
            type: 'error',
            title: 'Aviso',
            text: 'Objeto não selecionado'
        }).then(
            function () {
                return false;
            });
    }
    return false;
}

function LocalizarObjetos() {
    var txtObjeto = $('#txtLocalizarObjeto').val();
    $.ajax({
        url: '/Documento/PreencheCmbObjetosLocalizados',
        type: "POST",
        dataType: "JSON",
        data: { doc_id: selectedId, filtro_obj_codigo: txtObjeto },
        success: function (lstObjetos) {

            var i = 0;
            $("#divObjetosLocalizados").empty();
            $.each(lstObjetos, function (i, objeto) {
                i++;
                if (i < 50) {
                    var tagchk = '<input type="checkbox" id="idXXX" nome="nameXXX" value="valueXXX" style="margin-right:5px">';
                    tagchk = tagchk.replace("idXXX", "chk" + i);
                    tagchk = tagchk.replace("nameXXX", "chk" + i);
                    tagchk = tagchk.replace("valueXXX", objeto.Value);

                    var taglbl = '<label for="idXXX" class="chklst" >TextoXXX</label> <br />';
                    taglbl = taglbl.replace("idXXX", "chk" + i);
                    taglbl = taglbl.replace("TextoXXX", objeto.Text);

                    $("#divObjetosLocalizados").append(tagchk + taglbl);
                }
            });
            return false;
        }
    });

    return false;
}

function LimparLocalizarObjetos() {
    $('#txtLocalizarObjeto').val("");
    $("#divObjetosLocalizados").empty();
}


// ************* ASSOCIACAO A OS ********************************************
function AssociarOS() {
    if (selectedId >= 0) {

        LimparLocalizarOSs();

        $("#modalAssociarOS").modal('show');
    }
    else {
        swal({
            type: 'error',
            title: 'Aviso',
            text: 'Nenhum documento selecionado'
        }).then(
            function () {
                return false;
            });
    }
}

function DesassociarOS(OS_ID) {

    if (OS_ID >= 0) {
        var form = this;

        swal({
            title: "Desassociar OS. Tem certeza?",
            icon: "warning",
            buttons: [
                'Não',
                'Sim'
            ],
            dangerMode: true,
            focusCancel: true
        }).then(function (isConfirm) {
            if (isConfirm) {
                var response = POST("/Documento/Documento_DesassociarOrdemServico", JSON.stringify({ "doc_id": selectedId, "ord_id": OS_ID }))
                if (response.erroId >= 1) {
                    swal({
                        type: 'success',
                        title: 'Sucesso',
                        text: 'OS Desassociada com Sucesso'
                    });

                    $('#tblOSsAssociadas').DataTable().ajax.reload(null, false);  //false if you don't want to refresh paging else true.
                }
                else {
                    swal({
                        type: 'error',
                        title: 'Aviso',
                        text: 'Erro ao Desassociar registro'
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

function Documento_AssociarOS_Salvar() {
    // cria lista dos IDs de OSs selecionadas
    var selchks = [];
    var oss_ids = "";
    $('#divOSsLocalizadas input:checked').each(function () {
        selchks.push($(this).attr('value'));
    });

    for (var i = 0; i < selchks.length; i++)
        if (i == 0)
            oss_ids = selchks[i];
        else
            oss_ids = oss_ids + ";" + selchks[i];

    oss_ids = oss_ids + ";"; // acrescenta um delimitador no final da string

    if (selchks.length > 0) {
        var associacao = {
            doc_id: selectedId,
            ord_ids: oss_ids
        };

        $.ajax({
            url: "/Documento/Documento_AssociarOrdemServico",
            data: JSON.stringify(associacao),
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                $("#modalAssociarOS").modal('hide');
                $('#tblOSsAssociadas').DataTable().ajax.reload(null, false);  //false if you don't want to refresh paging else true.

                return false;
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
                $("#modalAssociarOS").modal('show');
                return false;
            }
        });
    }
    else {
        swal({
            type: 'error',
            title: 'Aviso',
            text: 'OS não selecionada'
        }).then(
            function () {
                return false;
            });
    }

    return false;
}

function LocalizarOSs() {

    var filtroOrdemServico_codigo = $('#txtFiltroOrdemServico_codigo').val().trim();
    var filtroObj_codigo = $('#txtFiltroObj_codigo').val().trim();
    var filtroTiposOS = $("#cmbFiltroTiposOS").val() == "" ? -1 : $("#cmbFiltroTiposOS").val();

    $.ajax({
        "url": "/Documento/PreencheCmbOSsLocalizadas",
            "type": "POST",
            "datatype": "json",
            "data": {
                "doc_id": selectedId,
                "filtroOrdemServico_codigo": filtroOrdemServico_codigo,
                "filtroObj_codigo": filtroObj_codigo,
                "filtroTiposOS": filtroTiposOS
            }
        ,success: function (lstOSs) {
            var i = 0;
            $("#divOSsLocalizadas").empty();
            $.each(lstOSs, function (i, ordem_servico) {
                i++;
                if (i < 50) {
                    var tagchk = '<input type="checkbox" id="idXXX" nome="nameXXX" value="valueXXX" style="margin-right:5px">';
                    tagchk = tagchk.replace("idXXX", "chk" + i);
                    tagchk = tagchk.replace("nameXXX", "chk" + i);
                    tagchk = tagchk.replace("valueXXX", ordem_servico.Value);

                    var taglbl = '<label for="idXXX"  class="chklst" >TextoXXX</label> <br />';
                    taglbl = taglbl.replace("idXXX", "chk" + i);
                    taglbl = taglbl.replace("TextoXXX", ordem_servico.Text);

                    $("#divOSsLocalizadas").append(tagchk + taglbl);
                }
            });

            return false;
        }
        ,error: function (errormessage) {
            alert(errormessage.responseText);
            $("#modalAssociarOS").modal('show');
            return false;
        }
    });

    return false;
}

function LimparLocalizarOSs()
{
    $('#txtFiltroOrdemServico_codigo').val("");
    $('#txtFiltroObj_codigo').val("");
    $('#cmbFiltroTiposOS').val("");
    $("#divOSsLocalizadas").empty();
}

// *********************************************************

    $("#txtCodigoDigitavel").bind('paste', function (e) {
        var self = this;
        setTimeout(function (e) {
            //alert($(self).val());
            txtCodigoDigitavel_onKeyUP();
        }, 0);
    });

// montagem do gridview
$(document).ready(function () {

    $.ajax({
        url: "/Documento/Documento_GetMascaras/",
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            mascaraDocumentoTecnicoGeral = result.data[0];
            mascaraDocumentoTecnicoEspecifico = result.data[1];
         //   jQuery("#txtCodigoDigitavel").mask(mascaraDocumentoTecnicoGeral, options);

        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });

    // ****************************GRID  tblDocumentos *****************************************************************************
    var params = {
        "doc_id": function () { return selectedId > 0 ? selectedId : 0 },
        "doc_codigo": function () { return $('#txtFiltroDoc_Codigo').val().trim() },
        "doc_descricao": function () { return $('#txtFiltroDoc_Descricao').val().trim() },
        "tpd_id": function () { return $('#cmbFiltroTipoDocumento :selected').val() },
        "dcl_codigo": function () { return $('#cmbFiltroClasseProjeto :selected').val() }
    };

    $('#tblDocumentos').DataTable({
        "processing": true,
        "serverSide": true,
       // "displayStart": displayStart,
        "ajax": {
            "url": "/Documento/LoadData",
            "type": "POST",
            "datatype": "json",
            "data": params,
        }
        , "columns": [
            { data: "tpd_id", "className": "hide_column", "searchable": false },
            { data: "doc_id", "className": "hide_column", "searchable": false },

            { data: "doc_codigo", "autoWidth": true, "searchable": false },
            { data: "dcl_codigo", "autoWidth": true, "searchable": false },
            { data: "tpd_descricao", "autoWidth": true, "searchable": false },
            { data: "doc_descricao", "autoWidth": true, "searchable": false },

            {
                data: "doc_caminho", "autoWidth": true, "searchable": false
                , "title": "Arquivo"
                , "render": function (data, type, row) {
                    var retorno = "";
                    var caminho = "'" + data + "'";
                    retorno = data.split("/").pop();
                    return retorno;
                }
            },
            {
                "title": "Opções",
                data: "doc_id",
                "searchable": false,
                "sortable": false,
                "render": function (data, type, row) {
                    var retorno = "";
                    if (permissaoEscrita > 0) {
                        retorno = '<a href="#" onclick="return Documento_Editar(' + data + ')" title="Editar" ><span class="glyphicon glyphicon-pencil"></span></a>' + '  ';

                        if (row.doc_ativo == 1)
                            retorno += '<a href="#" onclick="return Documento_AtivarDesativar(' + data + ', 0)" title="Ativo" ><span class="glyphicon glyphicon-ok text-success"></span></a>' + '  ';
                        else
                            retorno += '<a href="#" onclick="return Documento_AtivarDesativar(' + data + ', 1)" title="Desativado" ><span class="glyphicon glyphicon-remove text-danger"></span></a>' + '  ';
                    }
                    else {
                        retorno = '<span class="glyphicon glyphicon-pencil desabilitado"  ></span>' + '  ';

                        if (row.doc_ativo == 1)
                            retorno += '<span class="glyphicon glyphicon-ok text-success desabilitado" ></span>' + '  ';
                        else
                            retorno += '<span class="glyphicon glyphicon-remove text-danger desabilitado"  ></span>' + '  ';

                    }

                    if (permissaoExclusao > 0)
                        retorno += '<a href="#" onclick="return Documento_Excluir(' + data + ')" title="Excluir" ><span class="glyphicon glyphicon-trash"></span></a>';
                    else
                        retorno += '<span class="glyphicon glyphicon-trash desabilitado" title="Excluir" ></span>';

                    var doc_caminho = row["doc_caminho"];
                    var caminho = "'" + row["doc_caminho"] + "'";
                    if (permissaoLeitura > 0) {
                        if (doc_caminho != "") {
                            retorno += '<a href="#" onclick="window.open(' + caminho + ')"  title="Abrir Documento" ><span class="fa fa-search" style="padding-left:5px"></span></a>' + '  ';
                        }

                    }
                    else {
                        retorno += '<span class="fa fa-search desabilitado"  style="padding-left:5px" ></span>' + '  ';
                    }

                    return retorno;
                }
            }
        ]
        , 'columnDefs': [
            {
                targets: [3] // dcl_codigo
                , "createdCell": function (td, cellData, rowData, row, col) {
                    $(td).attr('title', rowData["dcl_descricao"]);
                }
            }
        ]
        ,"order": [[2, "asc"]]
        , "searching": true
        , "rowId": "doc_id"
        , "rowCallback": function (row, data) {
            if (data.doc_id == selectedId) {
                $(row).addClass('selected');
                //alert(selectedId);
            }
        }
        , "lengthMenu": [[10, 25, 50, 100], [10, 25, 50, 100]]
        , select: {
            style: 'single'
        }
        , "oLanguage": idioma
        , "pagingType": "input"
        , "sDom": '<"top">rt<"bottom"pfli><"clear">'
    });

    var tblDocumentos = $('#tblDocumentos').DataTable();
    $('#tblDocumentos tbody').on('click', 'tr', function () {
        if ($(this).hasClass('selected')) {
            $(this).removeClass('selected');
            obj_codigo_old = "";
        }
        else {
            tblDocumentos.$('tr.selected').removeClass('selected');
            $(this).addClass('selected');
        }

        document.getElementById('subGrids').style.visibility = "visible";

        var doc_id = tblDocumentos.row(this).data();
        selectedId = doc_id["doc_id"];
        obj_codigo_old = doc_id["doc_codigo"];
        $('#txtdoc_codigo2').val(doc_id["doc_codigo"]);
        $('#txtdoc_codigo2a').val(doc_id["doc_codigo"]);

        $('#tblObjetosAssociados').DataTable().ajax.reload(null, false);  //false if you don't want to refresh paging else true.
        $('#tblOSsAssociadas').DataTable().ajax.reload(null, false); 

        var textoHeaderDocumentoObjetos = "Objetos Associados ao Documento: " + doc_id["doc_codigo"];
        document.getElementById('HeaderObjetosAssociados').innerText = textoHeaderDocumentoObjetos;

        var textoHeaderDocumentoOSs = "OSs Associadas ao Documento: " + doc_id["doc_codigo"];
        document.getElementById('HeaderOSsAssociadas').innerText = textoHeaderDocumentoOSs;

    });

    $("#docBrowse").change(function (e) {
        var File = this.files;
        if (File && File[0]) {
            //   $('#txtdoc_caminho').val(e.target.files[0].name);
            $('#txtdoc_caminho2').val(e.target.files[0].name);
        }
    })

});


// ****************************GRID OBJETOS ASSOCIADOS *****************************************************************************

$('#tblObjetosAssociados').DataTable({
    "ajax": {
        "url": '/Documento/Documento_Objetos_ListAll',
        "data": function (d) {
            d.ID = selectedId;
        },
        "type": "GET",
        "datatype": "json"
    }
    , "columns": [
        { data: "obj_id", "className": "hide_column" },
        { data: "obj_Associado", "width": "30px", "className": "centro-horizontal" },
        { data: "obj_codigo", "autoWidth": true },
        { data: "obj_descricao", "autoWidth": true }
    ]
    , "columnDefs": [
        {
            targets: [1], // COLUNA ASSOCIADO
            "orderable": false,
            render: function (data, type, row) {
                var retorno = '';

                if (permissaoEscrita > 0)
                    retorno = '<a href="#" onclick="return DesassociarObjeto(' + row.obj_id + ')" ><span class="glyphicon glyphicon-trash text-success"></span></a>' + '  ';
                else
                    retorno = '<span class="glyphicon glyphicon-trash text-success desabilitado"></span>' + '  ';

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


// ****************************GRID OSs ASSOCIADAS *****************************************************************************

$('#tblOSsAssociadas').DataTable({
    "ajax": {
        "url": '/Documento/Documento_OrdemServico_ListAll',
        "data": function (d) {
            d.ID = selectedId;
        },
        "type": "GET",
        "datatype": "json"
    }
    , "columns": [
        { data: "ord_id", "className": "hide_column" },
        { data: "ord_Associada", "width": "30px", "className": "centro-horizontal" },
        { data: "ord_codigo", "autoWidth": true },
        { data: "ord_descricao", "autoWidth": true }
    ]
    , "columnDefs": [
        {
            targets: [1], // COLUNA ASSOCIADO
            "orderable": false,
            render: function (data, type, row) {
                var retorno = '';

                if (permissaoEscrita > 0)
                    retorno = '<a href="#" onclick="return DesassociarOS(' + row.ord_id + ')" ><span class="glyphicon glyphicon-trash text-success"></span></a>' + '  ';
                else
                    retorno = '<span class="glyphicon glyphicon-trash text-success desabilitado"></span>' + '  ';

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


