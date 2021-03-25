var obj_id_TipoOAE = -1;
var obj_id_TipoOAE_codigo = '';
var obj_id_TipoOAE_descricao = '';
var obj_tipoGrupoTexto = '';
var obj_tipoGrupo_id = -1;

var selectedId_obj_id = 0;
var selectedId_obj_pai = -1;
var selectedobj_codigo = "";
var selectedobj_descricao = "";

var selectedId_clo_id = -1;
var selectedId_tip_id = -1;
var selectedId_atr_id = -1;
var selectedId_afn_id = -1;

var selectedPageLen = 15;
var selectedPage = 0;

var selectedGrid = -1;
var filtro_obj_codigo = '';
var filtro_obj_descricao = '';
var filtro_clo_id = -1;
var filtro_tip_id = -1;

var ehInsercao = 0;
var lstExcecoes_Tipos = [45, 46, 53, 104, 105, 111, 126];


var options = {
        onKeyPress: function (val, e, field, options) {
            var corBranca = "rgb(255, 255, 255)";
            field.css("background-color", corBranca);
        },
        onInvalid: function (val, e, f, invalid, options) {
            var corVermelho = "rgb(228, 88, 71)";
         //   f.css("background-color", corVermelho);
        },
        'translation': { // IGNORA A MASCARA "S" (STRING) E MASCARA O "Y" SOMENTE "D" ou "E" 
            A: null,
            S: null,
            Y: { pattern: /[D-Ed-e]/, optional: true}
        }
};

var optionstxtCodigo = {
    onKeyPress: function (val, e, field, options) {
        var corBranca = "rgb(255, 255, 255)";
        field.css("background-color", corBranca);
    },
    onInvalid: function (val, e, f, invalid, options) {
        var corVermelho = "rgb(228, 88, 71)";
        //   f.css("background-color", corVermelho);
    },
    'translation': { // MASCARAR O "Y" SOMENTE "C" ou "G" ou "0"
        Y: { pattern: /[CcGg0]/, optional: true }
    }
};


function getcmbTipoValue(qualCombo, qualRetorno, idx, value)
{
    var valor = '';
    var mascara = '';
    var aux = '';
    var cmb = $('#' + qualCombo);
    var cmbFiltroTiposObjeto = document.getElementById("cmbFiltroTiposObjeto");

    if (idx != null) {// split por indice
        if (idx < 0)
            cmb.selectedIndex = idx;

        aux = $('#' + qualCombo).val();
        valor = parseInt(aux.substring(0, aux.indexOf(":")));
        mascara = aux.substring(aux.indexOf(":") + 1, 150);
    }
    else {
        if (value != null) // split por valor
        {
            for (var i = 1; i < cmbFiltroTiposObjeto.options.length; i++)
            {
                aux = cmbFiltroTiposObjeto.options[i].value;
                valor = parseInt(aux.substring(0, aux.indexOf(":")));
                if (valor == value) {
                    idx = i;
                    mascara = aux.substring(aux.indexOf(":") + 1, 150);
                    break;
                }
            }
        }
    }

    // retorna
    switch (qualRetorno)
    {
        case 'idx': return idx; break;
        case 'valor': return valor; break;
        case 'mascara': return mascara; break;
    }

}

function proximo_clo_id() {

    var retorno = -1;

    switch (selectedId_clo_id) {
        case -1: retorno = 1; break;
        case 1: retorno = 2; break; // 1= rodovia
        case 2: retorno = 3; break; // 2 = oae
        case 3: retorno = 6; break; // 3 = tipo oae
        case 6: // 6 = subdivisao 1
            if ((selectedId_tip_id == 11) || ((selectedId_tip_id == 14)))
                retorno = 7;
            else
                retorno = 9;
            break;
        case 7: // 7 = subdivisao 2
            if ((selectedId_tip_id == 22) || ((selectedId_tip_id == 23)))
                retorno = 8;
            else
                retorno = 9;
            break;
        case 8: // 8 = subdivisao 3
                retorno = 9;
            break;

        case 9: // 9 = grupo de Objetos
            if (lstExcecoes_Tipos.includes(selectedId_tip_id))
                retorno = 11;
            else
                retorno = 10;
            break;

        case 10: // 10 = numero do objeto
            retorno = 11;
            break;
    }

    return retorno;
}

function atualizatxtDescricao()
{

}


function cmbOBJClassesObjeto_onchange(quem) {
    $("#cmbTiposObjeto").html(""); // limpa os itens existentes

    var selectedVal = quem.options[quem.selectedIndex].value;
    ////if (quem.selectedIndex < 0) {
    ////    $('#tblObjetos').DataTable().columns(4).search('').draw();
    ////    $('#tblObjetos').DataTable().columns(5).search('').draw();

    ////    return; // retorna se o item selecionado for o "selecione a classe"
    ////}

    selectedId_clo_id = selectedVal;
    //selectedId_tip_id = -1;

    $("#lblClasseSelecionada").text(quem.options[quem.selectedIndex].text); // preenche o label classe

    var tip_pai = -1;
    if ((selectedId_clo_id >= 7) && (selectedId_clo_id <= 9))
        tip_pai = selectedId_tip_id;

    var excluir_existentes = 0;
    if ((selectedId_clo_id == 3) || (selectedId_clo_id == 9)) // || ((selectedId_clo_id == 7) && (selectedId_tip_id == 14)))
        excluir_existentes = 1;

    $.ajax({
        url: '/Objeto/PreenchecmbTiposObjeto',
        type: "POST",
        dataType: "JSON",
        data: { clo_id: selectedVal, tip_pai: tip_pai, excluir_existentes: excluir_existentes, obj_id: selectedId_obj_id },
        success: function (lstSubNiveis) {
            var tipo = '';
            if (lstSubNiveis.length > 1)
                $("#cmbTiposObjeto").append($('<option selected disabled></option>').val(-1).html("--Selecione--")); // 1o item vazio

            $.each(lstSubNiveis, function (i, subNivel) {
                if (lstSubNiveis.length == 1)
                    $("#cmbTiposObjeto").append($('<option selected disabled></option>').val(subNivel.Value).html(subNivel.Text));
                else
                    $("#cmbTiposObjeto").append($('<option></option>').val(subNivel.Value).html(subNivel.Text));
            });

             //$("#cmbTiposObjeto").val(-1);
  
            var cmbClassesObjeto = document.getElementById("cmbClassesObjeto");
            if (cmbClassesObjeto.options.length == 1) {
                cmbClassesObjeto.selectedIndex = 0;
                selectedId_clo_id = cmbClassesObjeto.options[cmbClassesObjeto.selectedIndex].value; 

                // dependendo da classe, mostra o textbox ou o combo ou somente o label
                var lblPrefixo = document.getElementById("lblPrefixo");
                lblPrefixo.innerText = selectedobj_codigo + "-";

                var tdTxtCodigo = document.getElementById("tdTxtCodigo");
                var cmbAEVCVG = document.getElementById("cmbAEVCVG");
                var lblAte = document.getElementById("lblAte");
                var txtCodigoAte = document.getElementById("txtCodigoAte");

                tdTxtCodigo.style.display = 'none';
                cmbAEVCVG.style.display = 'none';
                lblAte.style.display = 'none';
                txtCodigoAte.style.display = 'none';


                // coloca mascara no textbox se este estiver visivel
                switch (selectedId_clo_id) {
                    case "2": // OAE (quilometragem) = textbox
                        tdTxtCodigo.style.display = 'block';
                        jQuery("#txtcodigo").mask("000,000", options);
                        jQuery("#txtcodigo").attr('placeholder', "000,000");
                        $("#txtcodigo").focus();
                        break;

                    case "10": // numero do objeto = textbox
                        tdTxtCodigo.style.display = 'block';
                        var mascara = '00';


                        jQuery("#txtcodigo").mask(mascara, options2);
                        jQuery("#txtcodigo").attr('placeholder', mascara);

                        jQuery("#txtCodigoAte").mask(mascara, options);
                        jQuery("#txtCodigoAte").attr('placeholder', mascara);
                        lblAte.style.display = 'block';
                        txtCodigoAte.style.display = 'block';

                        $("#txtcodigo").focus();
                        break;


                    case "11": // Localizacao = textbox
                        tdTxtCodigo.style.display = 'block';
                        var lblPrefixo = document.getElementById("lblPrefixo").innerText;
                        var cmbAEVCVG = document.getElementById("cmbAEVCVG");

                        $("#cmbAEVCVG").html(""); // limpa os itens existentes

                        if (lblPrefixo.includes("-SE-FS"))
                            $("#cmbAEVCVG").append($('<option selected disabled></option>').val("T").html("T"));
                        else
                            if (lblPrefixo.includes("-SE-FI"))
                            {
                                $("#cmbAEVCVG").append($('<option selected></option>').val("V").html("V"));
                                $("#cmbAEVCVG").append($('<option></option>').val("VC").html("VC"));
                                $("#cmbAEVCVG").append($('<option></option>').val("VG").html("VG"));
                            }
                            else {
                                if (lblPrefixo.includes("-ENC-"))
                                    $("#cmbAEVCVG").append($('<option selected disabled></option>').val("E").html("E"));

                                else
                                    if ((lblPrefixo.includes("-ME-")) || (lblPrefixo.includes("-IE-")))
                                        $("#cmbAEVCVG").append($('<option selected disabled></option>').val("A").html("A"));
                            }

                        var mascara = '00';
                        if (lblPrefixo.includes("-SE-FS-PR"))
                            mascara = '000';

                        jQuery("#txtcodigo").mask(mascara, options);
                        jQuery("#txtcodigo").attr('placeholder', mascara);

                        jQuery("#txtCodigoAte").mask(mascara, options);
                        jQuery("#txtCodigoAte").attr('placeholder', mascara);

                        cmbAEVCVG.style.display = 'block';
                        lblAte.style.display = 'block';
                        txtCodigoAte.style.display = 'block';

                        $("#txtcodigo").focus();
                        break;
                }
            }

            //var cmbTiposObjeto = document.getElementById("cmbTiposObjeto");
            //if (cmbTiposObjeto.options.length == 2) {
            //    cmbTiposObjeto.selectedIndex = 1;
            //    preencheDescricao();
            //}
        }
    });
}

function preencheDescricao(vindo_de) {



    var lblPrefixo = document.getElementById("lblPrefixo");
    var obj_codigo = selectedobj_codigo;

    if (ehInsercao == 0)
        obj_codigo = selectedobj_codigo.substring(0, selectedobj_codigo.lastIndexOf("-")); //lblPrefixo.innerText.substring(0, lblPrefixo.innerText.length-1);

    if ((vindo_de == 'txtcodigo') && (parseInt(selectedId_clo_id) == 11) && (ehInsercao == 0))
    {
        var txtcodval = $('#txtcodigo').val();
        if ((txtcodval == "") || ((!isNaN(txtcodval)) && (parseInt(txtcodval) == 0)))
            if ($('#lblPrefixo').text().includes("-SE-FS-PR-")) 
                $('#txtcodigo').val("000");
        else
                $('#txtcodigo').val("00");

        var descvall = $('#txtdescricao').val();
        var descr = descvall.substring(0, descvall.lastIndexOf("#")) + "#" + $('#txtcodigo').val(); ;
        $('#txtdescricao').val(descr);

        return;
    }

    // limpa os campos
    if (vindo_de != 'txtcodigo')  $('#txtcodigo').val("");
    if (vindo_de != 'cmbEsquerdaDireita') $('#cmbEsquerdaDireita').val("-1");

    $('#txtdescricao').val("");
    $('#lblMascara').val("");

    // preenche o prefixo do novo objeto segundo o tipo selecionado
    var cmb = document.getElementById("cmbTiposObjeto");
    var selectedText = cmb.options[cmb.selectedIndex].text;
    var selectedVal = cmb.options[cmb.selectedIndex].value;

    var cmbFiltroClassesObjeto = document.getElementById("cmbFiltroClassesObjeto");
    var cmbFiltroTiposObjeto = document.getElementById("cmbFiltroTiposObjeto");

    var classe_value =  parseInt(cmbFiltroClassesObjeto.options[cmbFiltroClassesObjeto.selectedIndex].value);

    var tipoId = getTipoId(selectedVal);
    selectedId_tip_id = tipoId;

    var tip_Codigo = selectedVal.substring(selectedVal.indexOf(":") + 1, 150);
    if (tip_Codigo == "-1")
        tip_Codigo = "____";

    // procura o codigo do grupo no cmbFiltroTiposObjeto
    var pedacosCodigo = obj_codigo.split("-");

    var vv = cmbFiltroTiposObjeto.options[cmbFiltroTiposObjeto.selectedIndex].value;
    var idTipoGrupo = parseInt(vv.substring(0, vv.indexOf(":")));

    var codGrupo = pedacosCodigo[pedacosCodigo.length - 1];
    if (("115:Localização" == selectedVal) && (!lstExcecoes_Tipos.includes(idTipoGrupo)))
        codGrupo = pedacosCodigo[pedacosCodigo.length - 2];

    var nomeGrupo = "______";
    var idGrupo = 0;
    for (j = 0; j < cmbFiltroTiposObjeto.options.length - 1; j++) {
        var cc = cmbFiltroTiposObjeto.options[j].value;
        var dd = cc.substring(cc.indexOf(":") + 1, 8);
        if (dd.trim() == codGrupo.trim()) {
            idGrupo = parseInt(cc.substring(0, cc.indexOf(":")));
            nomeGrupo = cmbFiltroTiposObjeto.options[j].text;
            break;
        }
    }


    if ((tipoId != 28) && (tipoId != 25) && (tipoId != 115)) // DIFERENTES DE Quilometragem ou Número de Objeto ou Localização
    {
        $('#txtdescricao').val(selectedText + " " + obj_codigo + "-" + tip_Codigo );
        lblPrefixo.innerText = obj_codigo + "-" + tip_Codigo ;
    }
    else {// 25=NumObj 28=km 115=localizacao ==> textboxes

        if (parseInt(selectedId_clo_id) >= 9) // grupo de objetos ==> entao a descricao fica tipo do grupo + numero / tipo do grupo + numero + tipo da localizacao
        {

            // var tipoGrupoTexto = cmbTiposObjeto.options[cmbTiposObjeto.selectedIndex].text;
            if ((parseInt(selectedId_clo_id) == 9) || (parseInt(selectedId_clo_id) == 10))
                $('#txtdescricao').val(nomeGrupo + " #" + $('#txtcodigo').val() + " (" + obj_codigo + "-" + $('#txtcodigo').val() + ")");
            else
                if (parseInt(selectedId_clo_id) == 11) {
                    var cmbAEVCVG = document.getElementById("cmbAEVCVG");
                    var localizacaoNome = "";
                    var localizacaoCodigo = obj_codigo + "-" + (cmbAEVCVG.selectedIndex > -1 ? cmbAEVCVG.options[cmbAEVCVG.selectedIndex].value : "") + $('#txtcodigo').val();
                    switch (cmbAEVCVG.options[cmbAEVCVG.selectedIndex].value) {
                        case "A": localizacaoNome = "Apoio"; break;
                        case "E": localizacaoNome = "Encontro"; break;
                        case "V": localizacaoNome = "Vão"; break;
                        case "T": localizacaoNome = "Trecho"; break;
                        case "VC": localizacaoNome = "Vão Caixão Perdido"; break;
                        case "VG": localizacaoNome = "Vão em Grelha"; break;
                    }

                    // // procura o codigo do grupo no cmbFiltroTiposObjeto
                    // var pedacosCodigo = obj_codigo.split("-");
                    // var codGrupo = pedacosCodigo[pedacosCodigo.length - 1];
                    // var nomeGrupo = "______";
                    // var idGrupo = 0;
                    // for (j = 0; j < cmbFiltroTiposObjeto.options.length - 1; j++)
                    // {
                    //     var cc = cmbFiltroTiposObjeto.options[j].value;
                    //     var dd = cc.substring(cc.indexOf(":") + 1, 8);
                    //     if (dd.trim() == codGrupo.trim())
                    //     {
                    //         idGrupo = parseInt(cc.substring(0, cc.indexOf(":")));
                    //         nomeGrupo = cmbFiltroTiposObjeto.options[j].text;
                    //         break;
                    //     }
                    //}

                    var masculinos = [14, 15, 16, 24, 25, 32, 33, 34, 36, 37, 38, 39, 40, 44, 45, 46, 47, 49, 50, 52, 57, 72, 73, 76, 77, 78, 80, 81, 82, 84, 87, 90, 93, 104, 105, 106, 107, 108, 110];
                    var plural = [33, 39, 40, 43, 47, 56, 95, 98, 101, 106, 114];
                    var ss = plural.includes(idGrupo) ? "s" : "";

                    // coloca a descricao
                    //    $('#txtdescricao').val(localizacaoNome + " #" + $('#txtcodigo').val() + (masculinos.includes(idGrupo) ? " do" + ss : " da" + ss) + " " + nomeGrupo + " #" + pedacosCodigo[pedacosCodigo.length - 1]);
                    $('#txtdescricao').val(
                        nomeGrupo + (lstExcecoes_Tipos.includes(obj_tipoGrupo_id) ? " " : " #" + pedacosCodigo[pedacosCodigo.length - 1]) + " " + localizacaoNome + " #" + $('#txtcodigo').val()
                    );
                }
        }
        //else
        //    $('#txtdescricao').val(selectedText + " " + obj_codigo + "-" + $('#txtcodigo').val());

        lblPrefixo.innerText = obj_codigo + "-";
    }

}

function Inserir(obj_id, clo_id, tip_id, obj_codigo) {
   
    if (obj_id != null)
        selectedId_obj_id = obj_id;
    
    if (obj_codigo != null)
        selectedobj_codigo = obj_codigo;

    selectedId_clo_id = parseInt(clo_id);
    selectedId_tip_id = parseInt(tip_id);


    if ( isNaN(selectedId_clo_id))
        selectedId_clo_id = -1;

    if (isNaN(selectedId_tip_id))
        selectedId_tip_id = -1;

    var corBranca = "rgb(255, 255, 255)";
    $('#txtcodigo').css("background-color", corBranca);
    $('#txtdescricao').css("background-color", corBranca);

    // limpa os textboxes
    $('#txt_id').val("");
    $('#txtcodigo').val("");
    // $('#txtCodigoAte').val("00");

    if (tip_id == 46)
        document.getElementById('txtCodigoAte').value = "000";
    else
        document.getElementById('txtCodigoAte').value = "00";

    $('#txtdescricao').val("");
    $('#chkativo').prop('checked', true);
    $('#chkativo').css('border-color', 'lightgrey');


    // preenche o cmbClassesObjeto com a proxima opcao somente
    var cmbFiltroClassesObjeto = document.getElementById('cmbFiltroClassesObjeto');
    var cmbClassesObjeto = document.getElementById('cmbClassesObjeto');

    // limpa todas as Classes
    $("#cmbClassesObjeto").html("");
    $("#cmbTiposObjeto").html(""); // limpa todos os Tipos

    if (selectedId_clo_id == -1) {
        LimparFiltro();
    }

        var proximo = proximo_clo_id();

        // procura somente o proximo item e o preenche
    for (var i = 0; i < cmbFiltroClassesObjeto.options.length; i++) {

        if (
            ((selectedId_clo_id != -1) && (cmbFiltroClassesObjeto.options[i].value == proximo)) || // se tem "proximo"
            ((selectedId_clo_id == -1)) // veio do botao "NOVO"
        ) {
            var option = document.createElement("option");
            option.text = cmbFiltroClassesObjeto.options[i].text;
            option.value = cmbFiltroClassesObjeto.options[i].value;
            $("#cmbClassesObjeto").append($('<option></option>').val(option.value).html(option.text));
            $("#cmbClassesObjeto").val(option.Value);

            if (selectedId_clo_id != -1) {
                cmbClassesObjeto.selectedIndex = 0;

                cmbOBJClassesObjeto_onchange(cmbClassesObjeto);

                cmbClassesObjeto.selectedIndex = 0;


            }

            break;
        }
    }

    $("#modalSalvarRegistro").modal('show');

    $('#lblObjetoSelecionado').text("Filho de " + obj_codigo);
    var lblPrefixo = document.getElementById("lblPrefixo");
    lblPrefixo.innerText = obj_codigo + "-";
    lblPrefixo.style.textAlign = "right"; 

    document.getElementById("lblModalHeader").innerText = "Novo Objeto";
    if (document.getElementById("tdTxtCodigo").style.display == 'block')
        $("#txtcodigo").focus();

    var lblTipoSelecionado = document.getElementById("lblTipoSelecionado");
    lblTipoSelecionado.style.display = 'none';

    var cmbTiposObjeto = document.getElementById("cmbTiposObjeto");
    cmbTiposObjeto.style.display = 'block';

    var mascara = '00';
    if (selectedId_tip_id == 46)   // se for pavimento rigido, entao tem 3 digitos
        mascara = '000';

    jQuery("#txtcodigo").mask(mascara, options);
    jQuery("#txtcodigo").attr('placeholder', mascara);

    jQuery("#txtCodigoAte").mask(mascara, options);
    jQuery("#txtCodigoAte").attr('placeholder', mascara);

    selectedId_obj_pai = selectedId_obj_id;
    ehInsercao = 1;
}

function SalvarObjeto() {

   var cmbAEVCVG = document.getElementById("cmbAEVCVG");

    var param;
    var obj_codigo = "";
    switch (selectedId_clo_id + "")
    {
        case "2": obj_codigo = $('#lblPrefixo').text() + $('#txtcodigo').val(); break; // OAE
        case "3": obj_codigo = $('#lblPrefixo').text() ; break; // TIPO OAE
        case "10": obj_codigo = $('#lblPrefixo').text() + $('#txtcodigo').val(); break; // Numero Objeto
        case "11": obj_codigo = $('#lblPrefixo').text() + cmbAEVCVG.options[cmbAEVCVG.selectedIndex].value +  $('#txtcodigo').val(); break; // Localizacao
        default:
            obj_codigo = $('#lblPrefixo').text();
    }

    if ((selectedId_clo_id + "") == "11") {
        var lblPrefixoval = $('#lblPrefixo').text();
        if (lblPrefixoval.includes("-SE-FS-PR-")) // se for pavimento rigido, os numeros possuem 3 digitos
        {
            var txtcodval = Right(("000" + $('#txtcodigo').val()), 3);
            $('#txtcodigo').val(txtcodval);

            var txtCodigoAte = Right(("000" + $('#txtCodigoAte').val()), 3);
            $('#txtCodigoAte').val(txtCodigoAte);
        }
        else {
            var txtcodval = Right(("00" + $('#txtcodigo').val()), 2);
            $('#txtcodigo').val(txtcodval);

            var txtCodigoAte = Right(("00" + $('#txtCodigoAte').val()), 2);
            $('#txtCodigoAte').val(txtCodigoAte);
        }
    }

    var txtNumeroObjetoAte = "-1";
    var txtCodigoAteValue = "-1";

    if (selectedId_clo_id + "" == "10")
        txtNumeroObjetoAte = $('#txtCodigoAte').val();
            
    if (selectedId_clo_id + "" == "11")
        txtCodigoAteValue = $('#txtCodigoAte').val();




    if (ehInsercao == 1) {
        url = "/Objeto/Objeto_Inserir";
        param = {
            obj_codigo: obj_codigo,
            obj_descricao: $('#txtdescricao').val().trim(),
            obj_NumeroObjetoAte: txtNumeroObjetoAte,
            obj_localizacaoAte: txtCodigoAteValue
        };

        var selectedClasseVal = $('#cmbClassesObjeto').val();
        selectedId_clo_id = selectedClasseVal;

        // checa se tem Tipo selecionado
        var cmb = document.getElementById("cmbTiposObjeto");
        var selectedId_tip_id = cmb.options[cmb.selectedIndex].value;
        if (selectedId_tip_id == "-1") {
            swal({
                type: 'error',
                title: 'Aviso',
                text: 'Selecione Tipo de Objeto'
            });
            return false;
        }
        else {
            selectedId_tip_id = selectedId_tip_id.substring(0, selectedId_tip_id.indexOf(":"));
        }


        // checa se o textbox esta preenchido caso esteja visivel
        if (tdTxtCodigo.style.display == 'block') {
            var txtvalor = $('#txtcodigo').val().trim();
            if (txtvalor == "") {
                swal({
                    type: 'error',
                    title: 'Aviso',
                    text: 'O campo código é obrigatório'
                });
                return false;
            }
            else
                if ((txtvalor.length < 7) && (selectedId_clo_id == 2)) {
                    swal({
                        type: 'error',
                        title: 'Aviso',
                        text: 'O campo código está incompleto'
                    });
                    return false;
                }
                else
                    if ((txtvalor.length < 2) && (selectedId_clo_id == 10)) {
                        swal({
                            type: 'error',
                            title: 'Aviso',
                            text: 'O campo código do Objeto está incompleto'
                        });
                        return false;
                    }
                    else
                    if ((txtvalor.length < 2) && (selectedId_clo_id == 11)) {
                        swal({
                            type: 'error',
                            title: 'Aviso',
                            text: 'O campo código está incompleto'
                        });
                        return false;
                    }
        }

    }
    else {

        var cmbTiposObjeto = document.getElementById("cmbTiposObjeto");

        var selectedVal = -1;
        var tipoId = -1; 

        if ((selectedId_clo_id + "") == "2") {
            tipoId = 28; 
        }
        else
        {
            selectedVal = cmbTiposObjeto.options[cmbTiposObjeto.selectedIndex].value;
            tipoId = getTipoId(selectedVal);
        }


        var url = "/Objeto/Objeto_Salvar";
        param = {
            obj_id: selectedId_obj_id,
            obj_codigo: obj_codigo,
            obj_descricao: $('#txtdescricao').val(),
            tip_id: tipoId
        };
    }
        $.ajax({
            url: url,
            data: JSON.stringify(param),
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                // fecha o modal
                $("#modalSalvarRegistro").modal('hide');
                ehInsercao = 0;


                var objid = 0; 
                var retornomsg = '';
                var saida = result + '';

                if (saida.includes(";")) {
                    objid = result.substring(0, result.indexOf(";"));
                    retornomsg = result.substring(result.indexOf(";") + 1, 10000);
                }
                else
                    objid = result;

                // recarrega o grid
                selectedId_obj_id = parseInt(objid);
                carregaGrid(selectedId_obj_id);


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
                        text: 'Objeto(s) Salvo(s) com sucesso'
                    });
                }


                return false;
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
                ehInsercao = 0;
                return false;
            }
        });


    return false;
}

function DeletarObjeto(id) {

    swal({
        title: "Excluir este objeto e seus filhos. Tem certeza?",
        icon: "warning",
        buttons: [
            'Não',
            'Sim'
        ],
        dangerMode: true,
        focusCancel: true
    }).then(function (isConfirm) {
        if (isConfirm) {
            var response = POST("/Objeto/Objeto_Excluir", JSON.stringify({ id: id }))
            if (response.erroId >= 1) {
                swal({
                    type: 'success',
                    title: 'Sucesso',
                    text: 'Registro excluído com sucesso'
                });

                $('#tblObjetos').DataTable().ajax.reload();
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

function AtivarDesativarObjeto(id, ativar) {

    var msg = (ativar == 1 ? "Ativar" : "Desativar") + " este objeto e seus filhos. Tem certeza?"

    if (id >= 0) {
        var form = this;
        swal({
            title: msg,
            icon: "warning",
            buttons: [
                'Não',
                'Sim'
            ],
            dangerMode: true,
            focusCancel: true
        }).then(function (isConfirm) {
            if (isConfirm) {

                var url = "/Objeto/Objeto_AtivarDesativar";
                var response = POST(url, JSON.stringify({ id: id }))
                if (response.erroId > 0) {
                    swal({
                        type: 'success',
                        title: 'Sucesso',
                        text: ativar == 1 ? msgAtivacaoOK.replace("Registro", "Registros").replace("tivado", "tivados") : msgDesativacaoOK.replace("Registro", "Registros").replace("tivado", "tivados")
                    });

                    $('#tblObjetos').DataTable().ajax.reload(null, false);
                }
                else {
                    swal({
                        type: 'error',
                        title: 'Aviso',
                        text: ativar == 1 ? msgAtivacaoErro.replace("registro", "registros") : msgDesativacaoErro.replace("registro", "registros")
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

function EditarObjeto(obj_id, clo_id, tip_id, obj_codigo, objdesc, clo_nome, tip_nome) {
    ehInsercao = 0;

    var corBranca = "rgb(255, 255, 255)";
    $('#txtcodigo').css("background-color", corBranca);
    $('#txtdescricao').css("background-color", corBranca);

    $('#txtcodigo').css('border-color', 'lightgrey');
    $('#txtdescricao').css('border-color', 'lightgrey');

    document.getElementById("lblModalHeader").innerText = "Editar Objeto";
    
    $('#lblClasseSelecionada').text(clo_nome);

    var lblTipoSelecionado = document.getElementById("lblTipoSelecionado");
    var cmbTiposObjeto = document.getElementById("cmbTiposObjeto");

    cmbTiposObjeto.style.display = 'none';
    lblTipoSelecionado.style.display = 'block';
    lblTipoSelecionado.innerText = tip_nome;


    $('#txtdescricao').val(objdesc);

    selectedId_obj_id = obj_id;
    selectedobj_codigo = obj_codigo;
    selectedId_clo_id = clo_id;
    selectedId_tip_id = tip_id;

    if (clo_id == 3) { // tipo de OAE
        cmbTiposObjeto.style.display = 'block';
        lblTipoSelecionado.style.display = 'none';
    }
    else {
        cmbTiposObjeto.style.display = 'none';
        lblTipoSelecionado.style.display = 'block';
        lblTipoSelecionado.innerText = tip_nome;

        if (clo_id == 1)
            $('#lblObjetoSelecionado').text(selectedobj_codigo);
        else

            $('#lblObjetoSelecionado').text("Filho de " + obj_codigo.substring(0, obj_codigo.lastIndexOf("-")));
    }

    // preenche os combo de classe somente com o valor do objeto   
    $("#cmbClassesObjeto").html(""); // limpa as Classes
    var option = document.createElement("option");
    option.text = clo_nome;
    option.value = clo_id;
    $("#cmbClassesObjeto").append($('<option></option>').val(option.value).html(option.text));
    cmbClassesObjeto.selectedIndex = 0;



    // preenche os combo de tipo somente com o valor do objeto   

    // se a classe for 3 = tipo OAE ou 9 = grupo de Objetos, preenche o combo com valores que nao estao cadastrados no banco
    var tip_pai = -1;
    if ((clo_id >= 7) && (clo_id <= 9))
        tip_pai = selectedId_tip_id;

    if ((clo_id == 3) || (clo_id == 9))
    {
        var excluir_existentes = 0;
        if (selectedId_clo_id == 9)
        {
            excluir_existentes = 1;
        }


        $.ajax({
            url: '/Objeto/PreenchecmbTiposObjeto',
            type: "POST",
            dataType: "JSON",
            data: { clo_id: selectedId_clo_id, tip_pai: tip_pai, excluir_existentes: excluir_existentes, obj_id: selectedId_obj_id },
            success: function (lstSubNiveis) {
                $("#cmbTiposObjeto").html(""); // limpa o combo
                $.each(lstSubNiveis, function (i, subNivel) {
                    if (lstSubNiveis.length == 1)
                        $("#cmbTiposObjeto").append($('<option selected disabled></option>').val(subNivel.Value).html(subNivel.Text));
                    else
                        if (getTipoId(subNivel.Value) == tip_id) 
                            $("#cmbTiposObjeto").append($('<option selected ></option>').val(subNivel.Value).html(subNivel.Text));
                        else
                            $("#cmbTiposObjeto").append($('<option></option>').val(subNivel.Value).html(subNivel.Text));
                });
            }
        });
    }
    else // senao o valor é unico e basta copiar do combo filtro
    {
        $("#cmbTiposObjeto").html(""); // limpa o combo
            for (var i = 0; i < cmbFiltroTiposObjeto.options.length; i++) {
                var option = document.createElement("option");
                option.text = cmbFiltroTiposObjeto.options[i].text;
                option.value = cmbFiltroTiposObjeto.options[i].value;

                if (getTipoId(cmbFiltroTiposObjeto.options[i].value) == parseInt(tip_id)) {
                    $("#cmbTiposObjeto").append($('<option selected ></option>').val(option.value).html(option.text));
                        break; // sai do loop
                }
            }
    }




    // preenche o label Prefixo (codigo do objeto)
    var lblPrefixo = document.getElementById("lblPrefixo");
    lblPrefixo.innerText = obj_codigo ;
    lblPrefixo.style.textAlign = "left"; 

    var cmbAEVCVG = document.getElementById("cmbAEVCVG");
    var lblAte = document.getElementById("lblAte");
    var txtCodigoAte = document.getElementById("txtCodigoAte");

    cmbAEVCVG.style.display = 'none';
    lblAte.style.display = 'none';
    txtCodigoAte.style.display = 'none';

    txtCodigoAte.value = "";


    // coloca mascara no textbox se este estiver visivel
    var tdTxtCodigo = document.getElementById("tdTxtCodigo");
    tdTxtCodigo.style.display = 'none';

    switch (clo_id + "") {
        case "2": // OAE (quilometragem) = textbox
            tdTxtCodigo.style.display = 'block';
            jQuery("#txtcodigo").mask("000,000", options);
            jQuery("#txtcodigo").attr('placeholder', "000,000");

            lblPrefixo.innerText = obj_codigo.substring(0, obj_codigo.lastIndexOf("-") + 1);
            $("#txtcodigo").val(obj_codigo.substring(obj_codigo.lastIndexOf("-") + 1, 50));
            $("#txtcodigo").focus();
            break;

        case "3": // Tipo OAE
             // preenche o combo de tipo
            var ultimoDigito = obj_codigo.substring(obj_codigo.length - 1, obj_codigo.length);
            if ((ultimoDigito == "D") || (ultimoDigito == "E")) {
                lblPrefixo.innerText =  obj_codigo.substring(0, obj_codigo.length - 1);
            }
            break; // Tipo OAE 

        case "10": // numero objeto = textbox
            tdTxtCodigo.style.display = 'block';
            jQuery("#txtcodigo").mask("00", options2);
            jQuery("#txtcodigo").attr('placeholder', "00");

            lblPrefixo.innerText = obj_codigo.substring(0, obj_codigo.lastIndexOf("-") + 1);
            $("#txtcodigo").val(obj_codigo.substring(obj_codigo.lastIndexOf("-") + 1, 50));
            $("#txtcodigo").focus();
            break;


        case "11": // Localizacao = textbox
            tdTxtCodigo.style.display = 'block';
            var mascara = '';
            var lblPrefixo = document.getElementById("lblPrefixo").innerText;
            var cmbAEVCVG = document.getElementById("cmbAEVCVG");

            $("#cmbAEVCVG").html(""); // limpa os itens existentes


            if (((lblPrefixo.includes("-SE-FS"))) || ((lblPrefixo.includes("-SE-FI"))))
            {
                if (lblPrefixo.includes("-SE-FS"))
                    $("#cmbAEVCVG").append($('<option selected></option>').val("T").html("T"));
                else
                    $("#cmbAEVCVG").append($('<option selected></option>').val("V").html("V"));


                $("#cmbAEVCVG").append($('<option></option>').val("VC").html("VC"));
                $("#cmbAEVCVG").append($('<option></option>').val("VG").html("VG"));
            }
            else {
                if (lblPrefixo.includes("-ENC-"))
                    $("#cmbAEVCVG").append($('<option selected disabled></option>').val("E").html("E"));

                else
                    if ((lblPrefixo.includes("-ME-")) || (lblPrefixo.includes("-IE-")))
                        $("#cmbAEVCVG").append($('<option selected disabled></option>').val("A").html("A"));
            }

            var mascara = '00';

            if (lblPrefixo.includes("-SE-FS-PR")) {
                mascara = '000';
                $("#txtcodigo").val(obj_codigo.substr(obj_codigo.length -3));
            }
            else
                $("#txtcodigo").val(obj_codigo.substr(obj_codigo.length -2));

            jQuery("#txtcodigo").mask(mascara, options);
            jQuery("#txtcodigo").attr('placeholder', mascara);

            jQuery("#txtCodigoAte").mask(mascara, options);
            jQuery("#txtCodigoAte").attr('placeholder', mascara);

            jQuery("#lblPrefixo").text(obj_codigo.substring(0, obj_codigo.lastIndexOf("-") + 1));

            cmbAEVCVG.style.display = 'block';
            ////lblAte.style.display = 'block';
            ////txtCodigoAte.style.display = 'block';


            $("#txtcodigo").focus();
            break;
    }

    $("#modalSalvarRegistro").modal('show');
    return false;
}

function ChecaRepetido(txtBox) {

    var selectedId;
    var tabela;
    var campoId;
    var texto;
    selectedId = selectedId_obj_id;
    tabela = '#tblObjetos';
    campoId = "obj_id";
    texto = 'ATRIBUTO já cadastrado';

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
    $('#txtFiltroObj_Codigo').val('');
    $('#txtFiltroObj_Descricao').val('');
    filtroRow_numeroMin = 0;
    filtroRow_numeroMax = 1000;

    selectedId_obj_id = 0;
    selectedId_obj_pai = -1;

    selectedId_clo_id = -1;
    selectedId_tip_id = -1;
    selectedId_atr_id = -1;
    selectedId_afn_id = -1;
    selectedobj_codigo = "";

    filtro_obj_codigo = '';
    filtro_obj_descricao = '';
    filtro_clo_id = -1;
    filtro_tip_id = -1;

    carregaGrid(0);

    $('#lblObjetoSelecionado').text("");
    $("#cmbFiltroClassesObjeto").val(null);
    $("#cmbFiltroTiposObjeto").val(null); 

   $("#cmbClassesObjeto").val(null);
    $("#cmbTiposObjeto").html(""); // apaga os itens existentes


    // limpa as tabs

    //  preenche Ficha inspecao cadastral 
    var liFichaInspecaoCadastral = document.getElementById("liFichaInspecaoCadastral");
    var liDocumentosAssociados = document.getElementById("liDocumentosAssociados");

    if (liDocumentosAssociados)
        liDocumentosAssociados.style.display = "none";

    if (liFichaInspecaoCadastral)
        liFichaInspecaoCadastral.style.display = "none";

        $("#liDocumentosAssociados").removeClass("active");
        $("#tabDocumentosAssociados").removeClass("active in");

        $("#liFichaInspecaoCadastral").removeClass("active");
        $("#tabFichaInspecaoCadastral").removeClass("active in");



    return false;
}

function ExecutarFiltro() {

    filtroRow_numeroMin = 0;
    filtroRow_numeroMax = 1000;

    selectedId_obj_id = 0;
    selectedId_obj_pai = -1;

    selectedId_clo_id = $("#cmbFiltroClassesObjeto").val() == "" ? -1 : $("#cmbFiltroClassesObjeto").val();
//selectedId_tip_id = $("#cmbFiltroTiposObjeto").val() == "" ? -1 : $("#cmbFiltroTiposObjeto").val();
    selectedId_tip_id = $("#cmbFiltroTiposObjeto").val() == "" ? -1 : getcmbTipoValue('cmbFiltroTiposObjeto', 'valor', $("#cmbFiltroTiposObjeto").prop('selectedIndex'), null);

    selectedId_atr_id = -1;
    selectedId_afn_id = -1;


    filtro_obj_codigo = $('#txtFiltroObj_Codigo').val().trim();
    filtro_obj_descricao = $("#txtFiltroObj_Descricao").val().trim();
    filtro_clo_id = selectedId_clo_id;

    filtro_tip_id = selectedId_tip_id;
    filtro_tip_id = selectedId_tip_id;


    carregaGrid(selectedId_obj_id);

}

function txtcodigo_onKeyUp() {

    $("#txtcodigo").val(($("#txtcodigo").val()).toUpperCase());
    $("#txtCodigoAte").val($("#txtcodigo").val());

    // atualiza descricao
    preencheDescricao('txtcodigo');
}

function txtcodigo_onblur() {
    if ((selectedId_clo_id + "") == "11") {
        var lblPrefixoval = $('#lblPrefixo').text();
        if (lblPrefixoval.includes("-SE-FS-PR-")) // se for pavimento rigido, os numeros possuem 3 digitos
        {
            var txtcodval = Right(("000" + $('#txtcodigo').val()), 3);
            $('#txtcodigo').val(txtcodval);

            var txtCodigoAte = Right(("000" + $('#txtCodigoAte').val()), 3);
            $('#txtCodigoAte').val(txtCodigoAte);
        }
        else {
            var txtcodval = Right(("00" + $('#txtcodigo').val()), 2);
            $('#txtcodigo').val(txtcodval);

            var txtCodigoAte = Right(("00" + $('#txtCodigoAte').val()), 2);
            $('#txtCodigoAte').val(txtCodigoAte);
        }

     // atualiza descricao
        preencheDescricao('txtcodigo');
   }
}

// ****************************GRID tblObjetos *****************************************************************************
function carregaGrid(id) {

    // guarda os valores da paginacao
    var tblObjetos = $('#tblObjetos').DataTable();
    var info = tblObjetos.page.info();

    selectedPageLen = tblObjetos.page.len();
    selectedPage = info.page;

    $('#tblObjetos').DataTable().destroy();
    $('#tblObjetos').DataTable({
        "ajax": {
            "url": "/Objeto/Objeto_ListAll",
            "type": "GET",
            "datatype": "json",
            "data": {
                "obj_id": id,
                "filtro_obj_codigo" : filtro_obj_codigo,
                "filtro_obj_descricao": filtro_obj_descricao,
                "filtro_clo_id": filtro_clo_id,
                "filtro_tip_id" : filtro_tip_id
            }
        }
        , "columns": [
            { data: "row_numero", "className": "hide_column", "width": "15px", "searchable": true, "sortable": true },
            { data: "temFilhos", "className": "hide_column", "width": "15px", "searchable": true, "sortable": true },
            { data: "row_expandida", "className": "hide_column", "width": "2px", "searchable": false, "sortable": false },
            { data: "obj_id", "className": "hide_column", "searchable": true },
            { data: "clo_id", "className": "hide_column", "searchable": true },
            { data: "tip_id", "className": "hide_column", "searchable": true },
            {
                data: "temFilhos",
                "width": "45%", //"autoWidth": true,
                "searchable": true,
                "sortable": false,
                "render": function (data, type, row) {
                    var nTabs = "";
                    if (selectedId_obj_id != 0)
                        for (var i = 0; i < (row.nNivel); i++)
                            nTabs += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";

                    var retorno = "";
                    if (row.temFilhos > 0) {
                        if (row.row_expandida == 0)
                            retorno = '<div style="text-align:left" >&nbsp;&nbsp;' + nTabs + '<i class="fa fa-caret-right"></i> ' + row.obj_codigo + '</div >';
                        else
                            retorno = '<div style="text-align:left" >&nbsp;&nbsp;' + nTabs + '<i class="fa fa-caret-down"></i> ' + row.obj_codigo + '</div >';
                    }
                    else
                        retorno = '<div title="" style="text-align:left" >&nbsp;&nbsp;' + nTabs + ' ' + row.obj_codigo + '</div >';


                    return retorno;
                }
            },
            { data: "obj_descricao", "className": "maxwidthDescricao", "searchable": true, "sortable": false },
            { data: "clo_nome", "width": "60px", "searchable": false, "sortable": false },
            { data: "tip_nome", "width": "60px", "searchable": false, "sortable": false },
            { data: "nNivel", "className": "hide_column", "searchable": false, "sortable": false },
            {
                "width": "60px",
                data: "obj_id",
                "searchable": false,
                "sortable": false,
                "render": function (data, type, row) {
                    var retorno = "";
                    if (parseInt(row["clo_id"]) == 3) // tipo oae
                    {
                        obj_id_TipoOAE = parseInt(row["obj_id"]);
                        obj_id_TipoOAE_codigo = row["obj_codigo"];
                        obj_id_TipoOAE_descricao = row["obj_descricao"];
                    }

                    if (permissaoInsercao > 0) 
                    {

                        if ((parseInt(row["clo_id"]) == 3) // tipo oae
                            || (parseInt(row["tip_id"]) == 11) // Superestrutura
                            || (parseInt(row["tip_id"]) == 14) // Encontro Subdivisao1
                            || (parseInt(row["tip_id"]) == 19) // Encontro Subdivisao2
                            //|| (parseInt(row["tip_id"]) == 22) // Estruturas_De_Terra_Encontros
                            //|| (parseInt(row["tip_id"]) == 23) // Estruturas_De_Concreto_Encontros
                            || (parseInt(row["clo_id"]) == 11) // Localizacao
                           // || ((parseInt(row["clo_id"]) == 10) && (parseInt(row["temFilhos"]) > 0)) // Numero do Objeto
                        )
                            retorno = '<span class="glyphicon glyphicon-plus desabilitado visibilityHidden"  ></span>' + '  ';
                        else {
                            var objcod = "'" + row["obj_codigo"] + "'";
                            retorno = '<a href="#" onclick="return Inserir(' + data + ',' + row["clo_id"] + ',' + row["tip_id"] + ',' + objcod + ')" title="Novo Objeto Filho" ><span class="glyphicon glyphicon-plus"></span></a>' + '  ';
                        }
                    }
                    else {

                        if ((parseInt(row["clo_id"]) == 3) || (parseInt(row["tip_id"]) == 11) || (parseInt(row["tip_id"]) == 19))
                            retorno = '<span class="glyphicon glyphicon-plus desabilitado visibilityHidden"  ></span>' + '  ';
                        else 
                            retorno = '<span class="glyphicon glyphicon-plus desabilitado "  ></span>' + '  ';
                    }

                    if (permissaoEscrita > 0) {
                        var objcod = "'" + row["obj_codigo"] + "'";
                        var objdesc = "'" + row["obj_descricao"] + "'";
                        var clo_nome = "'" + row["clo_nome"] + "'";
                        var tip_nome = "'" + row["tip_nome"] + "'";

                        retorno += '<a href="#" onclick="return EditarObjeto(' + data + ',' + row["clo_id"] + ',' + row["tip_id"] + ',' + objcod + ',' + objdesc + ',' + clo_nome + ',' + tip_nome + ')" title="Editar" ><span class="glyphicon glyphicon-pencil"></span></a>' + '  ';

                        if (row.obj_ativo == 1)
                            retorno += '<a href="#" onclick="return AtivarDesativarObjeto(' + data + ', 0)" title="Ativo" ><span class="glyphicon glyphicon-ok text-success"></span></a>' + '  ';
                        else
                            retorno += '<a href="#" onclick="return AtivarDesativarObjeto(' + data + ', 1)" title="Desativado" ><span class="glyphicon glyphicon-remove text-danger"></span></a>' + '  ';
                    }
                    else {
                        retorno = '<span class="glyphicon glyphicon-pencil desabilitado"  ></span>' + '  ';

                        if (row.obj_ativo == 1)
                            retorno += '<span class="glyphicon glyphicon-ok text-success desabilitado" ></span>' + '  ';
                        else
                            retorno += '<span class="glyphicon glyphicon-remove text-danger desabilitado"  ></span>' + '  ';
                    }

                    if (permissaoExclusao > 0) {

                        if (row["obj_podeDeletar"] == "1")
                            retorno += '<a href="#" onclick="return DeletarObjeto(' + data + ')" title="Excluir" ><span class="glyphicon glyphicon-trash"></span></a>';
                         else
                           retorno += '<span class="glyphicon trash visibilityHidden"  ></span>' + '  ';
                    }
                    else
                        retorno += '<span class="glyphicon glyphicon-trash desabilitado" title="Excluir" ></span>';

                    return retorno;
                }
            }
        ]
        , 'columnDefs': [
            {
                targets: [6] // dcl_codigo
                , "createdCell": function (td, cellData, rowData, row, col) {
                    $(td).attr('title', rowData["obj_descricao"]);
                }
            }
        ]
        , "order": [0, "asc"]
        , "rowId": "obj_id"
        , "rowCallback": function (row, data) {
            if (data.obj_id == selectedId_obj_id)
                $(row).addClass('selected');
        }
        , "lengthMenu": [[15, 25, 50, 100], [15, 25, 50, 100]]
        , "displayLength": selectedPageLen
        , "displayStart": selectedPage * 10
        , select: {
            style: 'single'
        }
        , searching: true
        , "oLanguage": idioma
        , "pagingType": "input"
        , "sDom": '<"top">rt<"bottom"pfli><"clear">'
    });
}


var filtroRow_numeroMin = 0;
var filtroRow_numeroMax = 1000;

/* ajuste para filtrar os objetos e filhos */
$.fn.dataTable.ext.search.push(
    function (settings, data, dataIndex) {
        if (settings.nTable.id == "tblObjetos") {
            var row_numero = parseFloat(data[0]);

          //  if (filtroRow_numeroMin <= row_numero && row_numero < filtroRow_numeroMax)
            {
                return true;
            }
            return false;
        }
        else
            return true;
    }
);

// montagem do gridview
$(document).ready(function () {

    carregaGrid(selectedId_obj_id);

    var tblObjetos = $('#tblObjetos').DataTable();
    $('#tblObjetos tbody').on('click', 'tr', function () {

        // expande ou encolhe os filhos
        var temFilhos = parseInt(tblObjetos.row(this).selector.rows.children[1].innerText);
        var expandida = parseInt(tblObjetos.row(this).selector.rows.children[2].innerText);
        var obj_rowId = tblObjetos.row(this).selector.rows.id;

        // guarda os ids do objeto
        selectedId_obj_id = obj_rowId;
        selectedId_clo_id = parseInt(tblObjetos.row(this).selector.rows.children[4].innerText);
        selectedId_tip_id = parseInt(tblObjetos.row(this).selector.rows.children[5].innerText);
        obj_tipoGrupo_id = selectedId_tip_id;
        obj_tipoGrupoTexto = parseInt(tblObjetos.row(this).selector.rows.children[9].innerText);

        var obj_descricao = tblObjetos.row(this).selector.rows.children[7].innerText;
        var obj_codigo = tblObjetos.row(this).selector.rows.children[6].innerText;
        selectedobj_descricao = obj_descricao;

        // limpa as tabulacoes e espacos
        obj_codigo = obj_codigo.replace(/&nbsp;/g, ' ').trim();
        selectedobj_codigo = obj_codigo;

        ////if (selectedId_clo_id == 1)
        ////    $('#lblObjetoSelecionado').text(obj_codigo);
        ////else
        ////   $('#lblObjetoSelecionado').text("Filho de " + obj_codigo);

        // deseleciona a linha
        if ($(this).hasClass('selected')) 
        {
            $(this).removeClass('selected');

            if (temFilhos == 0) {
              //  $('#lblObjetoSelecionado').text(" ");
                obj_codigo = "";

                //$("#cmbClassesObjeto").val(null);
                //$("#cmbTiposObjeto").html(""); // limpa os itens existentes

                $('#txtFiltroObj_Codigo').val('');
                $('#txtFiltroObj_Descricao').val('');
                $("#cmbFiltroClassesObjeto").val(null);
                $("#cmbFiltroTiposObjeto").val(null);

                selectedId_obj_id = 0;
                selectedId_clo_id = -1;
                selectedId_tip_id = -1;
                //selectedobj_codigo = "";
            }
        }
        else {
            // seleciona a linha
            // remove a classe "selected" das linhas da tabela
            var els = document.getElementById("tblObjetos_wrapper").getElementsByClassName("selected");
            for (var i = 0; i < els.length; i++)
                els[i].classList.remove('selected');

            $(this).addClass('selected');

          // preenche as caixas de filtro
            $('#txtFiltroObj_Codigo').val(obj_codigo);
            $('#txtFiltroObj_Descricao').val(obj_descricao);
            $("#cmbFiltroClassesObjeto").val(selectedId_clo_id);
              document.getElementById("cmbFiltroTiposObjeto").selectedIndex = getcmbTipoValue('cmbFiltroTiposObjeto', 'idx', null, selectedId_tip_id);
        }


        // se estiver expandida, manda o codigo negativo (positivo = expandir / negativo = encolher)
        // filtra pelo codigo do objeto selecionado (metodo logo acima deste)
        if (temFilhos > 0) {
            carregaGrid(expandida == 1 ? -selectedId_obj_id : selectedId_obj_id);
            var filtroRow_numero = Math.trunc(tblObjetos.row(this).selector.rows.children[0].innerText);
            filtroRow_numeroMin = parseInt(filtroRow_numero);
            filtroRow_numeroMax = filtroRow_numeroMin + 1;
       }

        if (selectedId_obj_id != 0)
        {

            //  preenche Ficha inspecao cadastral 
            var liFichaInspecaoCadastral = document.getElementById("liFichaInspecaoCadastral");
            var liDocumentosAssociados = document.getElementById("liDocumentosAssociados");

            if (liDocumentosAssociados)
                liDocumentosAssociados.style.display = "none";

            if (liFichaInspecaoCadastral)
                liFichaInspecaoCadastral.style.display = "none";

            // ajusta as tabs
            if (selectedId_clo_id >= 3)
            {

                liDocumentosAssociados.style.display = "unset";
                liFichaInspecaoCadastral.style.display = "unset";

                $("#liDocumentosAssociados").removeClass("active");
                $("#tabDocumentosAssociados").removeClass("active in");

                if (!liFichaInspecaoCadastral.classList.contains("active"))
                {
                    $("#liFichaInspecaoCadastral").addClass("active");
                    $("#tabFichaInspecaoCadastral").addClass("active in");
                }

                preenchetblFicha(selectedId_obj_id, selectedId_clo_id, selectedId_tip_id);
            }
            else
            {
                $("#liFichaInspecaoCadastral").removeClass("active");
                $("#tabFichaInspecaoCadastral").removeClass("active in");

                if (!liDocumentosAssociados.classList.contains("active"))
                {
                    $("#liDocumentosAssociados").addClass("active");
                    $("#tabDocumentosAssociados").addClass("active in");
                }

                liDocumentosAssociados.style.display = "unset";
            }


            //  atualiza grid documentos
            var texto = "Documentos associados ao Tipo de OAE *" + obj_id_TipoOAE_codigo +  "<br /> e ao Objeto: " + obj_codigo;
            if (parseInt(obj_id_TipoOAE_codigo) == parseInt(selectedId_obj_id) || (selectedId_clo_id <= 3))
                texto = "Documentos associados ao Objeto: " + obj_codigo;

               // document.getElementById('HeaderObjDocumentos').innerHTML = texto;
            $("#HeaderObjDocumentos").html(texto);

            $('#tblDocumentosAssociados').DataTable().ajax.reload();

        }
        else
            accordion_encolher(0);

        document.getElementById('txt_codigo').value = obj_codigo;

    });



    // preenche combo
    preencheCombo(1, 'cmbRodovia', '--Selecione--', null);


}); // document.ready



// **************************** ATRIBUTOS POPUP *****************************************************************************


function LimparPopup() {
    $('#txtFiltroObj_Codigo').val('');
    $('#txtFiltroObj_Descricao').val('');
    filtroRow_numeroMin = 0;
    filtroRow_numeroMax = 1000;

    selectedId_obj_id = 0;
    selectedId_obj_pai = -1;

    selectedId_clo_id = -1;
    selectedId_tip_id = -1;
    selectedId_atr_id = -1;
    selectedId_afn_id = -1;
    selectedobj_codigo = "";

    filtro_obj_codigo = '';
    filtro_obj_descricao = '';
    filtro_clo_id = -1;
    filtro_tip_id = -1;

    carregaGrid(0);

    $('#lblObjetoSelecionado').text("");
    $("#cmbFiltroClassesObjeto").val(null);
    $("#cmbFiltroTiposObjeto").val(null);

    $("#cmbClassesObjeto").val(null);
    $("#cmbTiposObjeto").html(""); // apaga os itens existentes

    return false;
}

function SalvarTODOSValores() {
    var valor = -1;
    var unidade = -1;
    var ati = -1;
    var param;

    var atv_valores = "";

    // varre todas as linhas da tabela
    var table = document.getElementById("tblObjAtributosFixosPOPUP");
    for (var i = 1, row; row = table.rows[i]; i++) {

        var atr_id = row.cells[1].innerText; 

        var ati_ids ="";
        var atv_valor;

        var td = row.cells[5]; // varre somente a coluna de valores
        for (var j = 0; j < td.childNodes.length; j++)
        {
            // verifica os controles filhos
            if (td.childNodes[j].nodeName == "INPUT") { // textbox
                atv_valor = td.childNodes[j].value
               // alert(td.childNodes[j].nodeName + " <> " + atv_valor);
            }
            else
                if (td.childNodes[j].nodeName == "SELECT") { // combo
                    ati_ids = td.childNodes[j].value;
                  //  alert(td.childNodes[j].nodeName + " <> " + ati_ids );
                }
                else
                    if (td.childNodes[j].nodeName == "DIV") { // lista de checkboxes

                        for (var k = 0; k < td.childNodes[j].childElementCount; k++) {
                            if (td.childNodes[j].children[k].type == "checkbox") {
                                if (td.childNodes[j].children[k].checked) {
                                    ati_ids = ati_ids + ";" + td.childNodes[j].children[k].value;
                                }
                            }
                            else
                                if (td.childNodes[j].children[k].tagName == "DIV") // SIGNIFICA CHECKBOX + LABEL + TEXTBOX
                                {
                                    for (var m = 0; m < td.childNodes[j].children[k].childElementCount; m++)
                                    {
                                        if (td.childNodes[j].children[k].children[m].type == "checkbox")
                                            if (td.childNodes[j].children[k].children[m].checked) {
                                                ati_ids = ati_ids + ";" + td.childNodes[j].children[k].children[m].value;
                                            }

                                        if (td.childNodes[j].children[k].children[m].tagName == "INPUT")
                                             ati_ids = ati_ids + ";" + td.childNodes[j].children[k].children[m].value;
                                    }
                                }
                        }
                      //  alert(td.childNodes[j].nodeName + " <> " + ati_ids);
                    }

        }


    }

    return false;
}


// **************************** DOCUMENTOS *****************************************************************************
function AssociarDocumento() {

    if (selectedId_obj_id > 0) {
        // limpa o texbox de input
        $("#txtLocalizarDocumento").val("");

        // limpa a lista de documentos
        $("#divDocumentosLocalizados").empty();

        document.getElementById("txtLocalizarDocumento").focus();
        $("#modalAssociarDocumento").modal('show');
    }
    else {
        swal({
            type: 'error',
            title: 'Aviso',
            text: 'Nenhum Objeto selecionado'
        }).then(
            function () {
                return false;
            });
    }
}

function DesassociarDocumento(doc_id) {

    if (doc_id >= 0) {
        var form = this;

        swal({
            title: "Desassociar Documento. Tem certeza?",
            icon: "warning",
            buttons: [
                'Não',
                'Sim'
            ],
            dangerMode: true,
            focusCancel: true
        }).then(function (isConfirm) {
            if (isConfirm) {
                var response = POST("/Objeto/Objeto_DesassociarDocumento", JSON.stringify({ "doc_id": doc_id, "obj_id": selectedId_obj_id }))
                if (response.erroId >= 1) {
                    swal({
                        type: 'success',
                        title: 'Sucesso',
                        text: 'Documento Desassociado com Sucesso'
                    });

                    $('#tblDocumentosAssociados').DataTable().ajax.reload(null, false);  //false if you don't want to refresh paging else true.
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

function Objeto_AssociarDocumento_Salvar() {


    // cria lista dos IDs de Documentos selecionados
    var selchks = [];
    var doc_ids = "";
    $('#divDocumentosLocalizados input:checked').each(function () {
        selchks.push($(this).attr('value'));
    });

    for (var i = 0; i < selchks.length; i++)
      if (i == 0)
         doc_ids = selchks[i];
     else
         doc_ids = doc_ids + ";" + selchks[i];

    doc_ids = doc_ids + ";"; // acrescenta um delimitador no final da string

    if (selchks.length > 0) {
        var associacao = {
            "doc_ids": doc_ids,
            "obj_id": selectedId_obj_id
        };

        $.ajax({
            url: "/Objeto/Objeto_AssociarDocumentos",
            data: JSON.stringify(associacao),
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                $("#modalAssociarDocumento").modal('hide');
                $('#tblDocumentosAssociados').DataTable().ajax.reload(null, false);  //false if you don't want to refresh paging else true.

                return false;
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
                $("#modalAssociarDocumento").modal('show');
                return false;
            }
        });
    }
    else {
        swal({
            type: 'error',
            title: 'Aviso',
            text: 'Documento não selecionado'
        }).then(
            function () {
                return false;
            });
    }


    return false;
}

function LocalizarDocumentos() {
    // desabilita os controles
    document.body.style.cursor = "wait";
    document.getElementById('txtLocalizarDocumento').disabled = true;
    document.getElementById('btnLocalizarDocumentos').disabled = true;

    var txtDocumento = $('#txtLocalizarDocumento').val();
    $.ajax({
        url: '/Objeto/PreencheCmbDocumentosLocalizados',
        type: "POST",
        dataType: "JSON",
        data: { obj_id: selectedId_obj_id, codDoc: txtDocumento },
        success: function (lstDocumentos) {
            var i = 0;

            document.getElementById("divDocumentosLocalizados").innerHTML = "";
            $.each(lstDocumentos, function (i, Documento) {
                i++;
                if (i == 1)
                {
                    var texto;
                    var total_registros = parseInt(Documento.Value);
                    if (total_registros > 100) 
                        texto = "Total " + total_registros.toLocaleString('pt-br') + " documentos. <br /> Apresentando 100 primeiros";
                    else
                        if (total_registros > 1)
                            texto = "Total " + total_registros + " documentos." ;
                        else
                            if (total_registros == 1)
                                texto = "Total 1 documento.";
                            else
                                texto = "Documento não localizado.";

                    $("#lblTotalLocalizados").html(texto);

                }
                else
                if (i < 100) {
                    var tagchk = '<input type="checkbox" id="idXXX" nome="nameXXX" value="valueXXX" style="margin-right:5px">';
                    tagchk = tagchk.replace("idXXX", "chk" + i);
                    tagchk = tagchk.replace("nameXXX", "chk" + i);
                    tagchk = tagchk.replace("valueXXX", Documento.Value);

                    var taglbl = '<label for="idXXX" class="chklst" >TextoXXX</label> <br />';
                    taglbl = taglbl.replace("idXXX", "chk" + i);
                    taglbl = taglbl.replace("TextoXXX", Documento.Text);

                    $("#divDocumentosLocalizados").append(tagchk + taglbl);
                }

            });

            //habilita os controles
            document.body.style.cursor = "default";
            document.getElementById('txtLocalizarDocumento').disabled = false;
            document.getElementById('btnLocalizarDocumentos').disabled = false;
            return false;
        }
    });

    return false;
}




// ****GRId DocumentosAssociados ****************************
$('#tblDocumentosAssociados').DataTable({
    "ajax": {
        "url": "/Objeto/Objeto_Documentos_ListAll",
        "data": function (d) { d.obj_id = selectedId_obj_id; },
        "type": "GET",
        "datatype": "json"
    }
    , "columns": [
        { data: "tpd_id", "className": "hide_column", "searchable": false },
        { data: "doc_id", "width": "30px", "searchable": false },

        { data: "doc_codigo", "autoWidth": true, "searchable": true },
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
                //retorno = '<span class="glyphicon glyphicon-pencil desabilitado"  ></span>' + '  ';
                //retorno += '<span class="glyphicon glyphicon-ok text-success desabilitado" ></span>' + '  ';
                //retorno += '<span class="glyphicon glyphicon-trash desabilitado" title="Excluir" ></span>';

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
    , "columnDefs": [
        {
            targets: [1], // COLUNA DESASSOCIAR DOCUMENTO
            "orderable": false,
            render: function (data, type, row) {
                var retorno = '';

                if (permissaoEscrita > 0)
                    retorno = '<a href="#" onclick="return DesassociarDocumento(' + row.doc_id + ')" ><span class="glyphicon glyphicon-trash text-success"></span></a>' + '  ';
                else
                    retorno = '<span class="glyphicon glyphicon-trash text-success desabilitado"></span>' + '  ';

                return retorno;
            }
        }
    ]

    , "searching": true
    , "rowId": "doc_id"
    , "lengthMenu": [[10, 25, 50, 100], [10, 25, 50, 100]]
    , select: {
        style: 'single'
    }
    , "oLanguage": idioma
    , "pagingType": "input"
    , "sDom": '<"top">rt<"bottom"pfli><"clear">'
});



