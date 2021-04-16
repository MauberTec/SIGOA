
// ***** SCRIPT FICHA 1 - CADASTRAL ***********************

var controlesReadOnly = ["txtord_codigo", "txtobj_codigo_Novo2", "btnAbrirLocalizarObjetos"];

function setaReadWrite_FichaInspecaoCadastral(tabela, ehRead) {

    // habilita ou desabilita todos os controles editaveis
    var lstTxtBoxes = tabela.getElementsByTagName('input');
    var lstCombos = tabela.getElementsByTagName('select');
    var lstTextareas = tabela.getElementsByTagName('textarea');

    var cmb_atr_id_98 = document.getElementById("cmb_atr_id_98");


    // trava o
    if ((moduloCorrente == 'Objetos') // se estiver em Cadastro de Objetos,
        || ((moduloCorrente == 'OrdemServico') && (cmb_atr_id_98.selectedIndex > 0)) // se estiver em OrdemServico e já houver tipo de OAE selecionada
    ) {
        controlesReadOnly.push("cmb_atr_id_98"); // combo Tipo OAE
        //controlesReadOnly.push("txt_atr_id_105"); // descricao Tipo OAE
    }
    for (var i = 0; i < lstTxtBoxes.length; i++)
        if (!controlesReadOnly.includes(lstTxtBoxes[i].id))
            lstTxtBoxes[i].disabled = ehRead;

    for (var i = 0; i < lstTextareas.length; i++)
        if (!controlesReadOnly.includes(lstTextareas[i].id))
            lstTextareas[i].disabled = ehRead;

    for (var i = 0; i < lstCombos.length; i++)
        if (!controlesReadOnly.includes(lstCombos[i].id))
            lstCombos[i].disabled = ehRead;
        else
            lstCombos[i].disabled = true;

    if (tabela.id == "tblFicha2_INSPECAO_ROTINEIRA") // tabela de grupos da ficha 2
    {
        // botoes da lixeira
        var tblFicha_2_GRUPOS = document.getElementById("tblFicha_2_GRUPOS");
        var lstButtons = tblFicha_2_GRUPOS.getElementsByTagName('button');

        for (var i = 0; i < lstButtons.length; i++)
            lstButtons[i].style.display = ehRead ? 'none' : 'block'; // aqui display block/none; na criacao da tabela visibility: visible/hidden (para nao misturar porque la existe validacao)

    }

    cmb_atr_id_58_onchange(); // por causa do textbox condicionado

    // coloca mascara nos campos txt de alguns checkboxes
    jQuery('#txt_atr_id_32_240').mask('99');
    jQuery('#txt_atr_id_32_58').mask('99');
    jQuery('#txt_atr_id_32_241').mask('99');


}

function accordion_encolher(todos_menos) {

    if ((typeof todos_menos == 'undefined') || (todos_menos == null))
        todos_menos = 1;

    var s_todos_menos = todos_menos + " ";

    if (qualFicha != 1)
        return;

    // reseta DADOS GERAIS
    setaReadWrite_FichaInspecaoCadastral(tblFicha_DADOS_GERAIS, 1);
    document.getElementById("btn_Editar_DADOS_GERAIS").style.display = 'block';


    // verifica os outros tabs
    if (s_todos_menos.includes("2")) 
    {
        $('#div_Ficha_DOCUMENTOS').collapse('show');
        document.getElementById("iconAngle_DOCUMENTOS").classList.add('rotate');
    }
    else {
        $('#div_Ficha_DOCUMENTOS').collapse('hide');
        document.getElementById("iconAngle_DOCUMENTOS").classList.remove('rotate');
    }


    if ((todos_menos == 3) || (todos_menos == -30) || (todos_menos == -33) || (s_todos_menos.includes("3")) || (s_todos_menos.includes("4"))) 
    {
        //$('#div_Ficha_DOCUMENTOS').collapse('hide');
        //document.getElementById("iconAngle_DOCUMENTOS").classList.add('rotate');

        if ((todos_menos == 3) || (todos_menos == -30) || (todos_menos.includes("3")))
        {
            $('#div_Ficha_ATRIBUTOS_FUNCIONAIS').collapse('show');
            document.getElementById("btn_Editar_ATRIBUTOS_FUNCIONAIS").style.display = 'block';
            document.getElementById("iconAngle_ATRIBUTOS_FUNCIONAIS").classList.add('rotate');
        }

        if ((todos_menos == 3) || (todos_menos == -33) || (todos_menos.includes("4")))
        {
            $('#div_Ficha_ATRIBUTOS_FIXOS').collapse('show');
            document.getElementById("btn_Editar_ATRIBUTOS_FIXOS").style.display = 'block';
            document.getElementById("iconAngle_ATRIBUTOS_FIXOS").classList.add('rotate');
        }
    }
    else {
        //$('#div_Ficha_DOCUMENTOS').collapse('hide');
        //document.getElementById("iconAngle_DOCUMENTOS").classList.remove('rotate');

        $('#div_Ficha_ATRIBUTOS_FUNCIONAIS').collapse('hide');
        document.getElementById("btn_Editar_ATRIBUTOS_FUNCIONAIS").style.display = 'none';
        document.getElementById("iconAngle_ATRIBUTOS_FUNCIONAIS").classList.remove('rotate');

        $('#div_Ficha_ATRIBUTOS_FIXOS').collapse('hide');
        document.getElementById("btn_Editar_ATRIBUTOS_FIXOS").style.display = 'none';
        document.getElementById("iconAngle_ATRIBUTOS_FIXOS").classList.remove('rotate');
    }

    if ((todos_menos == 11) || (s_todos_menos.includes("5")))
    {
        $('#div_Ficha_SUPERESTRUTURA').collapse('show');
        document.getElementById("btn_Editar_SUPERESTRUTURA").style.display = 'block';
        document.getElementById("iconAngle_SUPERESTRUTURA").classList.add('rotate');
    }
    else {
        $('#div_Ficha_SUPERESTRUTURA').collapse('hide');
        document.getElementById("btn_Editar_SUPERESTRUTURA").style.display = 'none';
        document.getElementById("iconAngle_SUPERESTRUTURA").classList.remove('rotate');
    }

    if ((todos_menos == 12) || (s_todos_menos.includes("6")))
    {
        $('#div_Ficha_MESOESTRUTURA').collapse('show');
        document.getElementById("btn_Editar_MESOESTRUTURA").style.display = 'block';
        document.getElementById("iconAngle_MESOESTRUTURA").classList.add('rotate');
    }
    else {
        $('#div_Ficha_MESOESTRUTURA').collapse('hide');
        document.getElementById("btn_Editar_MESOESTRUTURA").style.display = 'none';
        document.getElementById("iconAngle_MESOESTRUTURA").classList.remove('rotate');
    }

    if ((todos_menos == 13) || (s_todos_menos.includes("7")))
    { 
        $('#div_Ficha_INFRAESTRUTURA').collapse('show');
        document.getElementById("btn_Editar_INFRAESTRUTURA").style.display = 'block';
        document.getElementById("iconAngle_INFRAESTRUTURA").classList.add('rotate');
    }
    else {
        $('#div_Ficha_INFRAESTRUTURA').collapse('hide');
        document.getElementById("btn_Editar_INFRAESTRUTURA").style.display = 'none';
        document.getElementById("iconAngle_INFRAESTRUTURA").classList.remove('rotate');
    }

    if ((todos_menos == 14) || (s_todos_menos.includes("8")))
    {
        $('#div_Ficha_ENCONTROS').collapse('show');
        document.getElementById("btn_Editar_ENCONTROS").style.display = 'block';
        document.getElementById("iconAngle_ENCONTROS").classList.add('rotate');
    }
    else {
        $('#div_Ficha_ENCONTROS').collapse('hide');
        document.getElementById("btn_Editar_ENCONTROS").style.display = 'none';
        document.getElementById("iconAngle_ENCONTROS").classList.remove('rotate');
    }

    if ((todos_menos == 15) || (s_todos_menos.includes("9")))
    {
        $('#div_Ficha_HISTORICO_INTERVENCOES').collapse('show');
        document.getElementById("btn_Editar_HISTORICO_INTERVENCOES").style.display = 'block';
        document.getElementById("iconAngle_HISTORICO_INTERVENCOES").classList.add('rotate');
    }
    else {
        $('#div_Ficha_HISTORICO_INTERVENCOES').collapse('hide');
        document.getElementById("btn_Editar_HISTORICO_INTERVENCOES").style.display = 'none';
        document.getElementById("iconAngle_HISTORICO_INTERVENCOES").classList.remove('rotate');
    }




    // oculta os outros botoes
    document.getElementById("btn_Salvar_DADOS_GERAIS").style.display = 'none';
    document.getElementById("btn_Cancelar_DADOS_GERAIS").style.display = 'none';

    document.getElementById("btn_Salvar_ATRIBUTOS_FUNCIONAIS").style.display = 'none';
    document.getElementById("btn_Cancelar_ATRIBUTOS_FUNCIONAIS").style.display = 'none';
    document.getElementById("btn_Salvar_ATRIBUTOS_FIXOS").style.display = 'none';
    document.getElementById("btn_Cancelar_ATRIBUTOS_FIXOS").style.display = 'none';
    document.getElementById("btn_Salvar_SUPERESTRUTURA").style.display = 'none';
    document.getElementById("btn_Cancelar_SUPERESTRUTURA").style.display = 'none';
    document.getElementById("btn_Salvar_MESOESTRUTURA").style.display = 'none';
    document.getElementById("btn_Cancelar_MESOESTRUTURA").style.display = 'none';
    document.getElementById("btn_Salvar_INFRAESTRUTURA").style.display = 'none';
    document.getElementById("btn_Cancelar_INFRAESTRUTURA").style.display = 'none';
    document.getElementById("btn_Salvar_ENCONTROS").style.display = 'none';
    document.getElementById("btn_Cancelar_ENCONTROS").style.display = 'none';
    document.getElementById("btn_Salvar_HISTORICO_INTERVENCOES").style.display = 'none';
    document.getElementById("btn_Cancelar_HISTORICO_INTERVENCOES").style.display = 'none';

    if ((todos_menos != -30) && (todos_menos != -33) && (todos_menos <= 2)) // ENCOLHE TUDO
    {
        $('#div_Ficha_DOCUMENTOS').collapse('hide');
        document.getElementById("iconAngle_DOCUMENTOS").classList.remove('rotate');

        $('#div_Ficha_ATRIBUTOS_FUNCIONAIS').collapse('hide');
        document.getElementById("btn_Editar_ATRIBUTOS_FUNCIONAIS").style.display = 'none';
        document.getElementById("iconAngle_ATRIBUTOS_FUNCIONAIS").classList.remove('rotate');

        $('#div_Ficha_ATRIBUTOS_FIXOS').collapse('hide');
        document.getElementById("btn_Editar_ATRIBUTOS_FIXOS").style.display = 'none';
        document.getElementById("iconAngle_ATRIBUTOS_FIXOS").classList.remove('rotate');

        $('#div_Ficha_SUPERESTRUTURA').collapse('hide');
        document.getElementById("btn_Editar_SUPERESTRUTURA").style.display = 'none';
        document.getElementById("iconAngle_SUPERESTRUTURA").classList.remove('rotate');

        $('#div_Ficha_MESOESTRUTURA').collapse('hide');
        document.getElementById("btn_Editar_MESOESTRUTURA").style.display = 'none';
        document.getElementById("iconAngle_MESOESTRUTURA").classList.remove('rotate');

        $('#div_Ficha_INFRAESTRUTURA').collapse('hide');
        document.getElementById("btn_Editar_INFRAESTRUTURA").style.display = 'none';
        document.getElementById("iconAngle_INFRAESTRUTURA").classList.remove('rotate');

        $('#div_Ficha_ENCONTROS').collapse('hide');
        document.getElementById("btn_Editar_ENCONTROS").style.display = 'none';
        document.getElementById("iconAngle_ENCONTROS").classList.remove('rotate');

        $('#div_Ficha_HISTORICO_INTERVENCOES').collapse('hide');
        document.getElementById("btn_Editar_HISTORICO_INTERVENCOES").style.display = 'none';
        document.getElementById("iconAngle_HISTORICO_INTERVENCOES").classList.remove('rotate');


        //document.getElementById("btn_Editar_DADOS_GERAIS").style.display = 'none';
    }

}

function nome_segundo_cabecalho(controleId) {
    var prefix = controleId.substring(0, controleId.lastIndexOf("_") + 1);
    var num = parseInt(controleId.substring(controleId.lastIndexOf("_") + 1)) + 1000;
    return (prefix + num);

}

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

function nome_segundo_cabecalho2(controleId) {
    var prefix = controleId.substring(0, controleId.lastIndexOf("_") + 1);
    var num = parseInt(controleId.substring(controleId.lastIndexOf("_") + 1)) + 1000;
    return (prefix + num);

}



function mudaTitulosAbas() {

    if (paginaPai == "Objeto")
        return;

    var ehCadastral = 1;

    var liFichaInspecaoRotineira = document.getElementById("liFichaInspecaoRotineira");
    var liFichaInspecaoEspecial = document.getElementById("liFichaInspecaoEspecial");

    if ((liFichaInspecaoRotineira.style.display == "unset")
        || (liFichaInspecaoEspecial.style.display == "unset"))
        ehCadastral = 0;


    var tblFicha_DOCUMENTOS = document.getElementById("tblFicha_DOCUMENTOS");
    var btn_Toggle_ATRIBUTOS_FUNCIONAIS = document.getElementById("btn_Toggle_ATRIBUTOS_FUNCIONAIS");
    var btn_Toggle_ATRIBUTOS_FIXOS = document.getElementById("btn_Toggle_ATRIBUTOS_FIXOS");
    var btn_Toggle_HISTORICO_INTERVENCOES = document.getElementById("btn_Toggle_HISTORICO_INTERVENCOES");

    var btnTextos_Cadastral = ["unset", "3 - ATRIBUTOS DE IMPORTÂNCIA DA OBRA DE ARTE DENTRO DA MALHA VIÁRIA", "4 - ATRIBUTOS FIXOS", "5 - HISTÓRICO DE INTERVENÇÕES"];
    var btnTextos_Rotineira = ["none", , , ];

    if (ehCadastral == 0) {
        if (tblFicha_DOCUMENTOS)
            tblFicha_DOCUMENTOS.style.display = "none";
        if (btn_Toggle_ATRIBUTOS_FUNCIONAIS)
            btn_Toggle_ATRIBUTOS_FUNCIONAIS.innerHTML = "2 - ATRIBUTOS DE IMPORTÂNCIA DA OBRA DE ARTE DENTRO DA MALHA VIÁRIA";

        if (btn_Toggle_ATRIBUTOS_FIXOS)
            btn_Toggle_ATRIBUTOS_FIXOS.innerHTML = "3 - ATRIBUTOS FIXOS";

        if (btn_Toggle_HISTORICO_INTERVENCOES)
            btn_Toggle_HISTORICO_INTERVENCOES.innerHTML = "4 - HISTÓRICO DE INTERVENÇÕES";
    }
    else {
        if (tblFicha_DOCUMENTOS)
            tblFicha_DOCUMENTOS.style.display = "table";

        if (btn_Toggle_ATRIBUTOS_FUNCIONAIS)
            btn_Toggle_ATRIBUTOS_FUNCIONAIS.innerHTML = "3 - ATRIBUTOS DE IMPORTÂNCIA DA OBRA DE ARTE DENTRO DA MALHA VIÁRIA";

        if (btn_Toggle_ATRIBUTOS_FIXOS)
            btn_Toggle_ATRIBUTOS_FIXOS.innerHTML = "4 - ATRIBUTOS FIXOS";

        if (btn_Toggle_HISTORICO_INTERVENCOES)
            btn_Toggle_HISTORICO_INTERVENCOES.innerHTML = "5 - HISTÓRICO DE INTERVENÇÕES";
    }
}



function limpatblFicha() {

    var tabela;
    //$('#partialFicha' + qualFicha);// document.getElementById("divFicha1");
    switch (qualFicha) {
        case 1: tabela = document.getElementById("divFicha1"); break;
        case 2: tabela = document.getElementById("divFicha2"); break;
        case 3: tabela = document.getElementById("divFicha3"); break;
        case 4: tabela = document.getElementById("divFicha4"); break;
    }

    // habilita ou desabilita todos os controles editaveis
    if (tabela) {
        var lstTxtBoxes = tabela.getElementsByTagName('input');
        var lstCombos = tabela.getElementsByTagName('select');
        var lstTextareas = tabela.getElementsByTagName('textarea');

        for (var i = 0; i < lstTxtBoxes.length; i++)
            lstTxtBoxes[i].value = "";

        for (var i = 0; i < lstTextareas.length; i++)
            lstTextareas[i].value = "";

        for (var i = 0; i < lstCombos.length; i++)
            lstCombos[i].innerText = null;
    }
}
function preenchetblFicha(obj_id, classe, tipo, ins_id)
{
    classe = parseInt(classe);
    tipo = parseInt(tipo);
    selectedId_clo_id = classe;
    selectedId_tip_id = tipo;


    // limpa antes de preencher
    limpatblFicha();

    var ord_id = 0;
    if ((paginaPai == "OrdemServico") || (paginaPai == "Inspecao"))
        ord_id = selectedId_ord_id;

    var url = "/Objeto/ObjAtributoValores_ListAll";
    var data = { "obj_id": obj_id, "ord_id": ord_id};

    if (moduloCorrente == 'OrdemServico')
    {
        var StatusOS = parseInt(filtroStatusOS);
        if (StatusOS == 11) // executada
        {
            url = "/Inspecao/InspecaoAtributosValores_ListAll";
            data = { "ord_id": selectedId_ord_id };
        }
    }

    $.ajax({
        "url": url,
        "type": "GET",
        "datatype": "json",
        "data": data,
        "success": function (result) {

            mudaTitulosAbas();

            for (var i = 0; i < result.data.length; i++) {

                // preenche os LABELS
                var label = document.getElementById(result.data[i].atv_controle.replace("chk_", "lbl").replace("cmb_", "lbl"));
                if (label) {
                    // procura o label2
                    var label2 = document.getElementById((nome_segundo_cabecalho2(label.id)));

                    var texto = result.data[i].atr_atributo_nome;
                    if (texto.includes('|')) {
                        partes = texto.split("|");
                        texto = partes[partes.length - 1].trim();
                    }
                    label.innerText = texto;

                    if (label2)
                        label2.innerText = texto;
                }

                // preenche o valor se houver
                if (parseInt(result.data[i].nItens) == 0) {
                    var textbox = document.getElementById(result.data[i].atv_controle.replace("lbl", "txt_"));
                    var textbox2 = document.getElementById((nome_segundo_cabecalho2(result.data[i].atv_controle.replace("lbl", "txt_"))).replace("lbl", "txt_"));
                    var mascara = result.data[i].atr_mascara_texto;

                    if (textbox) {
                        textbox.value = result.data[i].atv_valor;

                        // coloca mascara no textbox
                        if (mascara != "") {
                            jQuery(textbox).mask(mascara);
                            jQuery(textbox).attr('placeholder', mascara.replace(/9/g, '0'));
                        }

                    }
                    if (textbox2) {
                        textbox2.value = result.data[i].atv_valor;

                        if (mascara != "") {
                            jQuery(textbox2).mask(mascara);
                            jQuery(textbox).attr('placeholder', mascara.replace(/9/g, '0'));
                        }
                    }
                }
                else
                    if (result.data[i].atr_apresentacao_itens == 'combobox') {
                        var combo = document.getElementById(result.data[i].atv_controle);
                        var combo2 = document.getElementById((nome_segundo_cabecalho2(result.data[i].atv_controle)));

                        if (combo) {
                            combo.innerText = null; // limpa

                            // preenche combo
                            var lista = result.data[i].atr_itens_todos.split(";");
                            for (var m = 0; m < lista.length; m++) {
                                var opt = document.createElement("option");
                                opt.value = lista[m].substring(0, 3);
                                opt.textContent = lista[m].substring(3);

                                combo.appendChild(opt);
                            }

                            combo.value = result.data[i].atv_valor;

                            if (parseInt(result.data[i].atr_id) == 58)
                                cmb_atr_id_58_onchange();
                        }

                        if (combo2) {
                            combo2.innerText = null; // limpa

                            // preenche combo
                            var lista = result.data[i].atr_itens_todos.split(";");
                            for (var m = 0; m < lista.length; m++) {
                                var opt2 = document.createElement("option");
                                opt2.value = lista[m].substring(0, 3);
                                opt2.textContent = lista[m].substring(3);
                                combo2.appendChild(opt2);
                            }
                            combo2.value = result.data[i].atv_valor;
                        }


                    }
                    else
                        if (result.data[i].atr_apresentacao_itens == 'checkbox') {
                            var checkbox_prefixo = result.data[i].atv_controle;

                            var lista = result.data[i].atr_itens_todos.split(";");
                            for (var m = 0; m < lista.length; m++) {
                                var valor = lista[m].substring(0, 3);
                                var texto = lista[m].substring(3);
                                var checkbox = document.getElementById(checkbox_prefixo + "_" + parseInt(valor));
                                if (checkbox) {
                                    var label = document.getElementById(checkbox_prefixo.replace("chk_", "lbl") + "_" + parseInt(valor));
                                    if (label)
                                        label.innerText = texto;

                                    checkbox.value = valor;

                                    // preenche os valores correspondentes
                                    if (parseInt(valor) == parseInt(result.data[i].ati_ids)) {
                                        var tick = result.data[i].atv_valor.substring(0, 1);
                                        checkbox.checked = tick == "1" ? true : false;

                                        // procura um textbox correspondente, do tipo checkbox+textbox
                                        var txt = document.getElementById(checkbox_prefixo.replace("chk", "txt") + "_" + parseInt(valor));
                                        if (txt) {
                                            var texto = result.data[i].atv_valor.substr(2);
                                            if (texto != "") {
                                                txt.value = texto;
                                                checkbox.checked = true;
                                            }
                                        }
                                    }
                                }
                            }
                        }

            }
        }
    });

    // reabre as abas que estavam abertas
    var abas_abertas = '';
    if ($('#div_Ficha_DOCUMENTOS').hasClass('collapse in')) abas_abertas = abas_abertas + ";2";
    if ($('#div_Ficha_ATRIBUTOS_FUNCIONAIS').hasClass('collapse in')) abas_abertas = abas_abertas + ";3";
    if ($('#div_Ficha_ATRIBUTOS_FIXOS').hasClass('collapse in')) abas_abertas = abas_abertas + ";4";
    if ($('#div_Ficha_SUPERESTRUTURA').hasClass('collapse in')) abas_abertas = abas_abertas + ";5";
    if ($('#div_Ficha_MESOESTRUTURA').hasClass('collapse in')) abas_abertas = abas_abertas + ";6";
    if ($('#div_Ficha_INFRAESTRUTURA').hasClass('collapse in')) abas_abertas = abas_abertas + ";7";
    if ($('#div_Ficha_ENCONTROS').hasClass('collapse in')) abas_abertas = abas_abertas + ";8";
    if ($('#div_Ficha_HISTORICO_INTERVENCOES').hasClass('collapse in')) abas_abertas = abas_abertas + ";9";

    accordion_encolher(abas_abertas);

    mudaTitulosAbas();

/*
    // expande o item correspondente
    //if (moduloCorrente == 'Objeto')
    {
        switch (classe) {
            case 1: { // rodovia
                accordion_encolher(1);
                break;
            }
            case 2: { // quilometragem
                accordion_encolher(2);
                break;
            }
            case 3: { // tipo de obra de arte

                if ((tipo == -30) || (tipo == -33))
                    accordion_encolher(tipo);
                else
                    if (moduloCorrente == 'Objetos')
                        accordion_encolher(3);
                    else
                        accordion_encolher(0);
               
                break;
            }
            case 6: {
                switch (tipo) {
                    case 11: { // superestrutura
                        accordion_encolher(11);
                        break;
                    }
                    case 12: { // mesoestrutura
                        accordion_encolher(12);
                        break;
                    }
                    case 13: {// infrastrutura
                        accordion_encolher(13);
                        break;
                    }
                    case 14: { // encontro
                        accordion_encolher(14);
                        break;
                    }

                    case -15: { // HISTORICO_INTERVENCOES
                        accordion_encolher(15);
                        break;
                    }
                }
                break;
            } // subdivisao1
            default:
                accordion_encolher(classe);
        }
    }
    //else
    //    accordion_encolher(0);
    */
    if (qualFicha == 1)
    {
        carregaGrid_tblDocumentosReferencia(obj_id);
    }
}

function CancelarDados_FichaInspecaoCadastral(tabela) {
    // preenchetblFicha(obj_id, classe, tipo);
    preenchetblFicha(selectedId_obj_id, selectedId_clo_id, selectedId_tip_id);

    // alterna os campos para leitura
    setaReadWrite_FichaInspecaoCadastral(tabela, true);

    // alterna para os botoes de salvar/cancelar
    switch (tabela.id) {
        case "tblFicha_DADOS_GERAIS":
            {
                document.getElementById("btn_Salvar_DADOS_GERAIS").style.display = 'none';
                document.getElementById("btn_Cancelar_DADOS_GERAIS").style.display = 'none';
                document.getElementById("btn_Editar_DADOS_GERAIS").style.display = 'block';
                break;
            }

        case "tblFicha_ATRIBUTOS_FUNCIONAIS":
            {
                document.getElementById("btn_Salvar_ATRIBUTOS_FUNCIONAIS").style.display = 'none';
                document.getElementById("btn_Cancelar_ATRIBUTOS_FUNCIONAIS").style.display = 'none';
                document.getElementById("btn_Editar_ATRIBUTOS_FUNCIONAIS").style.display = 'block';
                break;
            }

        case "tblFicha_ATRIBUTOS_FIXOS":
            {
                document.getElementById("btn_Salvar_ATRIBUTOS_FIXOS").style.display = 'none';
                document.getElementById("btn_Cancelar_ATRIBUTOS_FIXOS").style.display = 'none';
                document.getElementById("btn_Editar_ATRIBUTOS_FIXOS").style.display = 'block';
                break;
            }

        case "tblFicha_SUPERESTRUTURA":
            {
                document.getElementById("btn_Salvar_SUPERESTRUTURA").style.display = 'none';
                document.getElementById("btn_Cancelar_SUPERESTRUTURA").style.display = 'none';
                document.getElementById("btn_Editar_SUPERESTRUTURA").style.display = 'block';
                break;
            }

        case "tblFicha_MESOESTRUTURA":
            {
                document.getElementById("btn_Salvar_MESOESTRUTURA").style.display = 'none';
                document.getElementById("btn_Cancelar_MESOESTRUTURA").style.display = 'none';
                document.getElementById("btn_Editar_MESOESTRUTURA").style.display = 'block';
                break;
            }

        case "tblFicha_INFRAESTRUTURA":
            {
                document.getElementById("btn_Salvar_INFRAESTRUTURA").style.display = 'none';
                document.getElementById("btn_Cancelar_INFRAESTRUTURA").style.display = 'none';
                document.getElementById("btn_Editar_INFRAESTRUTURA").style.display = 'block';
                break;
            }

        case "tblFicha_ENCONTROS":
            {
                document.getElementById("btn_Salvar_ENCONTROS").style.display = 'none';
                document.getElementById("btn_Cancelar_ENCONTROS").style.display = 'none';
                document.getElementById("btn_Editar_ENCONTROS").style.display = 'block';
                break;
            }

        case "tblFicha_HISTORICO_INTERVENCOES":
            {
                document.getElementById("btn_Salvar_HISTORICO_INTERVENCOES").style.display = 'none';
                document.getElementById("btn_Cancelar_HISTORICO_INTERVENCOES").style.display = 'none';
                document.getElementById("btn_Editar_HISTORICO_INTERVENCOES").style.display = 'block';
                break;
            }

        case "tblFicha2_DADOS_GERAIS2":
            {
                document.getElementById("btn_Salvar_DADOS_GERAIS2").style.display = 'none';
                document.getElementById("btn_Cancelar_DADOS_GERAIS2").style.display = 'none';
                document.getElementById("btn_Editar_DADOS_GERAIS2").style.display = 'block';
                break;
            }

        case "tblFicha2_INSPECAO_ROTINEIRA":
            {
                document.getElementById("btn_Salvar_INSPECAO_ROTINEIRA").style.display = 'none';
                document.getElementById("btn_Cancelar_INSPECAO_ROTINEIRA").style.display = 'none';
                document.getElementById("btn_Editar_INSPECAO_ROTINEIRA").style.display = 'block';
                break;
            }

        case "tblFicha2_CRITERIO_DE_CLASSIFICACAO":
            {
                document.getElementById("btn_Salvar_CRITERIO_DE_CLASSIFICACAO").style.display = 'none';
                document.getElementById("btn_Cancelar_CRITERIO_DE_CLASSIFICACAO").style.display = 'none';
                document.getElementById("btn_Editar_CRITERIO_DE_CLASSIFICACAO").style.display = 'block';
                break;
            }

        case "tblFicha2_POLITICA_ACOES_A_IMPLEMENTAR":
            {
                document.getElementById("btn_Salvar_POLITICA_ACOES_A_IMPLEMENTAR").style.display = 'none';
                document.getElementById("btn_Cancelar_POLITICA_ACOES_A_IMPLEMENTAR").style.display = 'none';
                document.getElementById("btn_Editar_POLITICA_ACOES_A_IMPLEMENTAR").style.display = 'block';
                break;
            }

        case "tblFicha2_NOTA_OAE_PARAMETRO_FUNCIONAL":
            {
                document.getElementById("btn_Salvar_NOTA_OAE_PARAMETRO_FUNCIONAL").style.display = 'none';
                document.getElementById("btn_Cancelar_NOTA_OAE_PARAMETRO_FUNCIONAL").style.display = 'none';
                document.getElementById("btn_Editar_NOTA_OAE_PARAMETRO_FUNCIONAL").style.display = 'block';
                break;
            }
    }


}
function SalvarDados_FichaInspecaoCadastral(tabela) {
    var obj_id = selectedId_obj_id;
    var atr_id = -1;
    var ati_id = -1;
    var atv_valor = -1;
    var saida = '';

    //var abas_abertas = '';
    //if ($('#div_Ficha_DOCUMENTOS').hasClass('collapse in')) abas_abertas = abas_abertas + ";2";
    //if ($('#div_Ficha_ATRIBUTOS_FUNCIONAIS').hasClass('collapse in')) abas_abertas = abas_abertas + ";3";
    //if ($('#div_Ficha_ATRIBUTOS_FIXOS').hasClass('collapse in')) abas_abertas = abas_abertas + ";4";
    //if ($('#div_Ficha_SUPERESTRUTURA').hasClass('collapse in')) abas_abertas = abas_abertas + ";5";
    //if ($('#div_Ficha_MESOESTRUTURA').hasClass('collapse in')) abas_abertas = abas_abertas + ";6";
    //if ($('#div_Ficha_INFRAESTRUTURA').hasClass('collapse in')) abas_abertas = abas_abertas + ";7";
    //if ($('#div_Ficha_ENCONTROS').hasClass('collapse in')) abas_abertas = abas_abertas + ";8";
    //if ($('#div_Ficha_HISTORICO_INTERVENCOES').hasClass('collapse in')) abas_abertas = abas_abertas + ";9";

    var lstTextareas = tabela.getElementsByTagName('textarea');
    for (var i = 0; i < lstTextareas.length; i++) {
        if (!controlesReadOnly.includes(lstTextareas[i].id))
            saida = saida + ";" + lstTextareas[i].id + ":" + lstTextareas[i].value;
    }

    var lstCombos = tabela.getElementsByTagName('select');
    for (var i = 0; i < lstCombos.length; i++) {
        if (!controlesReadOnly.includes(lstCombos[i].id))
            if (lstCombos[i].selectedIndex > -1)
                saida = saida + ";" + lstCombos[i].id + ":" + lstCombos[i].options[lstCombos[i].selectedIndex].value;
    }


    var lstInputs = tabela.getElementsByTagName('input'); // lista de textbox + checkbox
    for (var i = 0; i < lstInputs.length; i++) {
        if (!controlesReadOnly.includes(lstInputs[i].id)) {
            if (lstInputs[i].type == "checkbox") {
                // verifica se tem um texbox correspondente (para o caso de checkbox+textbox)
                var txt = document.getElementById(lstInputs[i].id.replace("chk", "txt"));
                if (txt) // existe o textbox
                    saida = saida + ";" + lstInputs[i].id + ":" + (lstInputs[i].checked ? 1 : 0) + ":" + txt.value;
                else
                    saida = saida + ";" + lstInputs[i].id + ":" + (lstInputs[i].checked ? 1 : 0) + ":" + "";
            }
            else // textbox
            {
                // verifica se tem um checkbox correspondente (para o caso de checkbox+textbox). Caso positivo, entao nao faz
                var chk = document.getElementById(lstInputs[i].id.replace("txt", "chk"));
                if (!chk)  // nao tem
                {
                    var valor = lstInputs[i].value;
                    if ((valor.trim() == ",") || (valor.trim() == "."))
                        valor = "";
                    else
                        if (((valor.startsWith(",")) || (valor.startsWith("."))) && (!isNaN(valor.replace(",", "").replace(".", "")))) {
                            valor = "0" + valor;
                          //  valor = valor.replace(",", ".")
                        }

                    saida = saida + ";" + lstInputs[i].id + ":" + valor;
                }
            }
        }
    }

    saida = saida.substr(1) + ";"; // acrescenta um ponto e virgula no final e retira do comeco


    // ********* ENVIA OS DADOS PARA O BANCO **********************************************************************
    var nome_aba = tabela.id.replace("tblFicha_", "");
    var clo_id2 = selectedId_clo_id;
    var tip_id2 = selectedId_tip_id;

    var param = {
        obj_id: selectedId_obj_id,
        atr_id: -2,
        ati_id: -2,
        atv_valores: saida,
        nome_aba: nome_aba
    };



    var txt_atr_id_13 = document.getElementById("txt_atr_id_13");
    var txt_atr_id_106 = document.getElementById("txt_atr_id_106");
    var cmb_atr_id_98 = document.getElementById("cmb_atr_id_98");


    var ord_id = -1;
    if (moduloCorrente == 'OrdemServico')
        ord_id = selectedId_ord_id;

    var codigoOAE = txt_atr_id_106.value + "-" + txt_atr_id_13.value;
    var selidTipoOAE = cmb_atr_id_98.options[cmb_atr_id_98.selectedIndex].value;
    param = JSON.stringify({ 'ObjAtributoValor': param, 'codigoOAE': codigoOAE, 'selidTipoOAE': selidTipoOAE, ord_id: ord_id });

    $.ajax({
        url: "/Objeto/ObjAtributoValores_Salvar",
        data: param,
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            //$("#modalSalvarAtributoFixoValor").modal('hide');
            //if (selectedGrid == 1)
            //    $('#tblObjAtributosFixos').DataTable().ajax.reload(null, false);  //false = sem reload na pagina.
            //else
            //    $('#tblObjAtributosFuncionais').DataTable().ajax.reload(null, false);  //false = sem reload na pagina.
            if (moduloCorrente == 'OrdemServico') {
                if (nome_aba == "DADOS_GERAIS")
                    carregaGridOS(selectedId_ord_id);
                else {
                    //switch (nome_aba) {
                    //    case 'ATRIBUTOS_FUNCIONAIS': clo_id2 = 3; tip_id2 = -30; break;
                    //    case 'ATRIBUTOS_FIXOS': clo_id2 = 3; tip_id2 = -33; break;
                    //    case 'SUPERESTRUTURA': clo_id2 = 6; tip_id2 = 11; break;
                    //    case 'MESOESTRUTURA': clo_id2 = 6; tip_id2 = 12; break;
                    //    case 'INFRAESTRUTURA': clo_id2 = 6; tip_id2 = 13; break;
                    //    case 'ENCONTROS': clo_id2 = 6; tip_id2 = 14; break;
                    //    case 'HISTORICO_INTERVENCOES': clo_id2 = 6; tip_id2 = -15; break;
                    //}



                    preenchetblFicha(selectedId_obj_id, clo_id2, tip_id2); // ja esta embutido no carregaGrid
                }
            }
            else {
                preenchetblFicha(selectedId_obj_id, selectedId_clo_id, selectedId_tip_id);
            }
            return false;
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
            return false;
        }
    });


    // alterna os campos para leitura
    setaReadWrite_FichaInspecaoCadastral(tabela, true);

    // alterna para os botoes de salvar/cancelar
    switch (tabela.id) {
        case "tblFicha_DADOS_GERAIS":
            {
                document.getElementById("btn_Salvar_DADOS_GERAIS").style.display = 'none';
                document.getElementById("btn_Cancelar_DADOS_GERAIS").style.display = 'none';
                document.getElementById("btn_Editar_DADOS_GERAIS").style.display = 'block';
                break;
            }

        case "tblFicha_ATRIBUTOS_FUNCIONAIS":
            {
                document.getElementById("btn_Salvar_ATRIBUTOS_FUNCIONAIS").style.display = 'none';
                document.getElementById("btn_Cancelar_ATRIBUTOS_FUNCIONAIS").style.display = 'none';
                document.getElementById("btn_Editar_ATRIBUTOS_FUNCIONAIS").style.display = 'block';

                break;
            }

        case "tblFicha_ATRIBUTOS_FIXOS":
            {
                document.getElementById("btn_Salvar_ATRIBUTOS_FIXOS").style.display = 'none';
                document.getElementById("btn_Cancelar_ATRIBUTOS_FIXOS").style.display = 'none';
                document.getElementById("btn_Editar_ATRIBUTOS_FIXOS").style.display = 'block';
                break;
            }

        case "tblFicha_SUPERESTRUTURA":
            {
                document.getElementById("btn_Salvar_SUPERESTRUTURA").style.display = 'none';
                document.getElementById("btn_Cancelar_SUPERESTRUTURA").style.display = 'none';
                document.getElementById("btn_Editar_SUPERESTRUTURA").style.display = 'block';
                break;
            }

        case "tblFicha_MESOESTRUTURA":
            {
                document.getElementById("btn_Salvar_MESOESTRUTURA").style.display = 'none';
                document.getElementById("btn_Cancelar_MESOESTRUTURA").style.display = 'none';
                document.getElementById("btn_Editar_MESOESTRUTURA").style.display = 'block';
                break;
            }

        case "tblFicha_INFRAESTRUTURA":
            {
                document.getElementById("btn_Salvar_INFRAESTRUTURA").style.display = 'none';
                document.getElementById("btn_Cancelar_INFRAESTRUTURA").style.display = 'none';
                document.getElementById("btn_Editar_INFRAESTRUTURA").style.display = 'block';
                break;
            }

        case "tblFicha_ENCONTROS":
            {
                document.getElementById("btn_Salvar_ENCONTROS").style.display = 'none';
                document.getElementById("btn_Cancelar_ENCONTROS").style.display = 'none';
                document.getElementById("btn_Editar_ENCONTROS").style.display = 'block';
                break;
            }

        case "tblFicha_HISTORICO_INTERVENCOES":
            {
                document.getElementById("btn_Salvar_HISTORICO_INTERVENCOES").style.display = 'none';
                document.getElementById("btn_Cancelar_HISTORICO_INTERVENCOES").style.display = 'none';
                document.getElementById("btn_Editar_HISTORICO_INTERVENCOES").style.display = 'block';
                break;
            }

        case "tblFicha2_DADOS_GERAIS2":
            {
                document.getElementById("btn_Salvar_DADOS_GERAIS2").style.display = 'none';
                document.getElementById("btn_Cancelar_DADOS_GERAIS2").style.display = 'none';
                document.getElementById("btn_Editar_DADOS_GERAIS2").style.display = 'block';
                break;
            }

        case "tblFicha2_INSPECAO_ROTINEIRA":
            {
                document.getElementById("btn_Salvar_INSPECAO_ROTINEIRA").style.display = 'none';
                document.getElementById("btn_Toggle_INSPECAO_ROTINEIRA").style.display = 'none';
                document.getElementById("btn_Editar_INSPECAO_ROTINEIRA").style.display = 'block';
                break;
            }
        case "tblFicha2_CRITERIO_DE_CLASSIFICACAO":
            {
                document.getElementById("btn_Salvar_CRITERIO_DE_CLASSIFICACAO").style.display = 'none';
                document.getElementById("btn_Toggle_CRITERIO_DE_CLASSIFICACAO").style.display = 'none';
                document.getElementById("btn_Editar_CRITERIO_DE_CLASSIFICACAO").style.display = 'block';
                break;
            }


    }
}
function EditarDados_FichaInspecaoCadastral(tabela) {
    // alterna os campos para escrita
    setaReadWrite_FichaInspecaoCadastral(tabela, false);

    // alterna para os botoes de salvar/cancelar
    switch (tabela.id) {
        case "tblFicha_DADOS_GERAIS":
            {
                document.getElementById("btn_Salvar_DADOS_GERAIS").style.display = 'block';
                document.getElementById("btn_Cancelar_DADOS_GERAIS").style.display = 'block';
                document.getElementById("btn_Editar_DADOS_GERAIS").style.display = 'none';
                break;
            }

        case "tblFicha_ATRIBUTOS_FUNCIONAIS":
            {
                document.getElementById("btn_Salvar_ATRIBUTOS_FUNCIONAIS").style.display = 'block';
                document.getElementById("btn_Cancelar_ATRIBUTOS_FUNCIONAIS").style.display = 'block';
                document.getElementById("btn_Editar_ATRIBUTOS_FUNCIONAIS").style.display = 'none';
                break;
            }

        case "tblFicha_ATRIBUTOS_FIXOS":
            {
                document.getElementById("btn_Salvar_ATRIBUTOS_FIXOS").style.display = 'block';
                document.getElementById("btn_Cancelar_ATRIBUTOS_FIXOS").style.display = 'block';
                document.getElementById("btn_Editar_ATRIBUTOS_FIXOS").style.display = 'none';
                break;
            }

        case "tblFicha_SUPERESTRUTURA":
            {
                document.getElementById("btn_Salvar_SUPERESTRUTURA").style.display = 'block';
                document.getElementById("btn_Cancelar_SUPERESTRUTURA").style.display = 'block';
                document.getElementById("btn_Editar_SUPERESTRUTURA").style.display = 'none';
                break;
            }

        case "tblFicha_MESOESTRUTURA":
            {
                document.getElementById("btn_Salvar_MESOESTRUTURA").style.display = 'block';
                document.getElementById("btn_Cancelar_MESOESTRUTURA").style.display = 'block';
                document.getElementById("btn_Editar_MESOESTRUTURA").style.display = 'none';
                break;
            }

        case "tblFicha_INFRAESTRUTURA":
            {
                document.getElementById("btn_Salvar_INFRAESTRUTURA").style.display = 'block';
                document.getElementById("btn_Cancelar_INFRAESTRUTURA").style.display = 'block';
                document.getElementById("btn_Editar_INFRAESTRUTURA").style.display = 'none';
                break;
            }

        case "tblFicha_ENCONTROS":
            {
                document.getElementById("btn_Salvar_ENCONTROS").style.display = 'block';
                document.getElementById("btn_Cancelar_ENCONTROS").style.display = 'block';
                document.getElementById("btn_Editar_ENCONTROS").style.display = 'none';
                break;
            }

        case "tblFicha_HISTORICO_INTERVENCOES":
            {
                document.getElementById("btn_Salvar_HISTORICO_INTERVENCOES").style.display = 'block';
                document.getElementById("btn_Cancelar_HISTORICO_INTERVENCOES").style.display = 'block';
                document.getElementById("btn_Editar_HISTORICO_INTERVENCOES").style.display = 'none';
                break;
            }

        case "tblFicha2_DADOS_GERAIS2":
            {
                document.getElementById("btn_Salvar_DADOS_GERAIS2").style.display = 'block';
                document.getElementById("btn_Cancelar_DADOS_GERAIS2").style.display = 'block';
                document.getElementById("btn_Editar_DADOS_GERAIS2").style.display = 'none';
                break;
            }

        case "tblFicha2_INSPECAO_ROTINEIRA":
            {
                document.getElementById("btn_Salvar_INSPECAO_ROTINEIRA").style.display = 'block';
                document.getElementById("btn_Cancelar_INSPECAO_ROTINEIRA").style.display = 'block';
                document.getElementById("btn_Editar_INSPECAO_ROTINEIRA").style.display = 'none';
                break;
            }

        case "tblFicha2_CRITERIO_DE_CLASSIFICACAO":
            {
                document.getElementById("btn_Salvar_CRITERIO_DE_CLASSIFICACAO").style.display = 'block';
                document.getElementById("btn_Cancelar_CRITERIO_DE_CLASSIFICACAO").style.display = 'block';
                document.getElementById("btn_Editar_CRITERIO_DE_CLASSIFICACAO").style.display = 'none';
                break;
            }

        case "tblFicha2_NOTA_OAE_PARAMETRO_FUNCIONAL":
            {
                document.getElementById("btn_Salvar_NOTA_OAE_PARAMETRO_FUNCIONAL").style.display = 'block';
                document.getElementById("btn_Cancelar_NOTA_OAE_PARAMETRO_FUNCIONAL").style.display = 'block';
                document.getElementById("btn_Editar_NOTA_OAE_PARAMETRO_FUNCIONAL").style.display = 'none';
                break;
            }

        case "tblFicha2_POLITICA_ACOES_A_IMPLEMENTAR":
            {
                document.getElementById("btn_Salvar_POLITICA_ACOES_A_IMPLEMENTAR").style.display = 'block';
                document.getElementById("btn_Cancelar_POLITICA_ACOES_A_IMPLEMENTAR").style.display = 'block';
                document.getElementById("btn_Editar_POLITICA_ACOES_A_IMPLEMENTAR").style.display = 'none';
                break;
            }


    }

    return false;
}

// ****GRId Documentos Referencia ****************************
function carregaGrid_tblDocumentosReferencia(obj_id) {
    $('#tblDocumentosReferencia').DataTable().destroy();

    if (moduloCorrente == 'OrdemServico') {
        $('#tblDocumentosReferencia').DataTable({
            "ajax": {
                "url": "/OrdemServico/OrdemServico_Documentos_ListAll",
                "data": function (d) { d.ord_id = selectedId_ord_id; d.obj_id = obj_id; d.somente_referencia = 1 },
                "type": "GET",
                "datatype": "json"
            }
            , "columns": [
                { data: "tpd_id", "className": "hide_column", "searchable": false, "sortable": false },
                { data: "doc_id", "className": "hide_column", "searchable": false, "sortable": false },

                {
                    "title": "Documento",
                    data: "doc_codigo",
                    "autoWidth": false,
                    "searchable": true, "sortable": false,
                    "render": function (data, type, row) {
                        var retorno = row["doc_codigo"];
                        if (row["doc_codigo"].substring(0, 1) == "*")
                            retorno = row["doc_codigo"].substring(1, 100);

                        return retorno;
                    }
                },
                { "title": "Tipo", data: "tpd_descricao", "autoWidth": true, "searchable": false, "sortable": false },
                { "title": "Descrição", data: "doc_descricao", "autoWidth": true, "searchable": false, "sortable": false },
                {
                    "title": "Arquivo", data: "doc_caminho", "autoWidth": true, "searchable": false, "sortable": false,
                    "render": function (data, type, row) {
                        var retorno = "";
                        var caminho = "'" + data + "'";
                        retorno = data.split("/").pop();
                        return retorno;
                    }
                }
            ]

            , "searching": false
            , "rowId": "doc_id"
            , "oLanguage": {
                "sEmptyTable": "Nenhum registro encontrado",
                "sLoadingRecords": "Carregando...",
                "sProcessing": "Processando...",
                "sZeroRecords": "Nenhum registro encontrado"
            }
            , "sDom": '<"top">rt<"bottom"><"clear">'
        });
    }

}

function header_click(quem, expandir) {
    if (expandir == null)
        expandir = 0;

    if (selectedId_clo_id <= 2) {
        expandir = 0;
        accordion_encolher(1);
    }
   // else
        switch (quem.id) {

            case "btn_Toggle_DOCUMENTOS":
                {
                    // roda o icone 90graus
                    document.getElementById("iconAngle_DOCUMENTOS").classList.toggle('rotate');
                    break;
                }

            case "btn_Toggle_ATRIBUTOS_FUNCIONAIS":
                {
                    // alterna os campos para leitura
                    setaReadWrite_FichaInspecaoCadastral(tblFicha_ATRIBUTOS_FUNCIONAIS, true);

                    // mostra o botao editar
                    if ((quem.getAttribute('aria-expanded') == "false") || (expandir == 1)) {
                        document.getElementById("btn_Salvar_ATRIBUTOS_FUNCIONAIS").style.display = 'none';
                        document.getElementById("btn_Cancelar_ATRIBUTOS_FUNCIONAIS").style.display = 'none';
                        document.getElementById("btn_Editar_ATRIBUTOS_FUNCIONAIS").style.display = 'block';
                    }
                    else {
                        document.getElementById("btn_Editar_ATRIBUTOS_FUNCIONAIS").style.display = 'none';
                        if (quem.getAttribute('aria-expanded') == "true") {
                            document.getElementById("btn_Salvar_ATRIBUTOS_FUNCIONAIS").style.display = 'none';
                            document.getElementById("btn_Cancelar_ATRIBUTOS_FUNCIONAIS").style.display = 'none';
                        }
                        else {
                            document.getElementById("btn_Salvar_ATRIBUTOS_FUNCIONAIS").style.display = 'block';
                            document.getElementById("btn_Cancelar_ATRIBUTOS_FUNCIONAIS").style.display = 'block';
                        }

                    }

                    // roda o icone 90graus
                    document.getElementById("iconAngle_ATRIBUTOS_FUNCIONAIS").classList.toggle('rotate');
                    break;
                }

            case "btn_Toggle_ATRIBUTOS_FIXOS":
                {
                    // alterna os campos para leitura
                    setaReadWrite_FichaInspecaoCadastral(tblFicha_ATRIBUTOS_FIXOS, true);

                    // mostra o botao editar
                    if ((quem.getAttribute('aria-expanded') == "false") || (expandir == 1)) {
                        document.getElementById("btn_Salvar_ATRIBUTOS_FIXOS").style.display = 'none';
                        document.getElementById("btn_Cancelar_ATRIBUTOS_FIXOS").style.display = 'none';
                        document.getElementById("btn_Editar_ATRIBUTOS_FIXOS").style.display = 'block';
                    }
                    else {
                        document.getElementById("btn_Editar_ATRIBUTOS_FIXOS").style.display = 'none';
                        if (quem.getAttribute('aria-expanded') == "true") {
                            document.getElementById("btn_Salvar_ATRIBUTOS_FIXOS").style.display = 'none';
                            document.getElementById("btn_Cancelar_ATRIBUTOS_FIXOS").style.display = 'none';
                        }
                        else {
                            document.getElementById("btn_Salvar_ATRIBUTOS_FIXOS").style.display = 'block';
                            document.getElementById("btn_Cancelar_ATRIBUTOS_FIXOS").style.display = 'block';
                        }
                    }

                    // roda o icone 90graus
                    document.getElementById("iconAngle_ATRIBUTOS_FIXOS").classList.toggle('rotate');
                    break;
                }

            case "btn_Toggle_SUPERESTRUTURA":
                {
                    // alterna os campos para leitura
                    setaReadWrite_FichaInspecaoCadastral(tblFicha_SUPERESTRUTURA, true);

                    // mostra o botao editar
                    if ((quem.getAttribute('aria-expanded') == "false") || (expandir == 1)) {
                        document.getElementById("btn_Salvar_SUPERESTRUTURA").style.display = 'none';
                        document.getElementById("btn_Cancelar_SUPERESTRUTURA").style.display = 'none';
                        document.getElementById("btn_Editar_SUPERESTRUTURA").style.display = 'block';
                    }
                    else {
                        document.getElementById("btn_Editar_SUPERESTRUTURA").style.display = 'none';
                        if (quem.getAttribute('aria-expanded') == "true") {
                            document.getElementById("btn_Salvar_SUPERESTRUTURA").style.display = 'none';
                            document.getElementById("btn_Cancelar_SUPERESTRUTURA").style.display = 'none';
                        }
                        else {
                            document.getElementById("btn_Salvar_SUPERESTRUTURA").style.display = 'block';
                            document.getElementById("btn_Cancelar_SUPERESTRUTURA").style.display = 'block';
                        }
                    }

                    // roda o icone 90graus
                    document.getElementById("iconAngle_SUPERESTRUTURA").classList.toggle('rotate');
                    break;
                }

            case "btn_Toggle_MESOESTRUTURA":
                {
                    // alterna os campos para leitura
                    setaReadWrite_FichaInspecaoCadastral(tblFicha_MESOESTRUTURA, true);

                    // mostra o botao editar
                    if ((quem.getAttribute('aria-expanded') == "false") || (expandir == 1)) {
                        document.getElementById("btn_Salvar_MESOESTRUTURA").style.display = 'none';
                        document.getElementById("btn_Cancelar_MESOESTRUTURA").style.display = 'none';
                        document.getElementById("btn_Editar_MESOESTRUTURA").style.display = 'block';
                    }
                    else {
                        document.getElementById("btn_Editar_MESOESTRUTURA").style.display = 'none';
                        if (quem.getAttribute('aria-expanded') == "true") {
                            document.getElementById("btn_Salvar_MESOESTRUTURA").style.display = 'none';
                            document.getElementById("btn_Cancelar_MESOESTRUTURA").style.display = 'none';
                        }
                        else {
                            document.getElementById("btn_Salvar_MESOESTRUTURA").style.display = 'block';
                            document.getElementById("btn_Cancelar_MESOESTRUTURA").style.display = 'block';
                        }
                    }

                    // roda o icone 90graus
                    document.getElementById("iconAngle_MESOESTRUTURA").classList.toggle('rotate');
                    break;
                }

            case "btn_Toggle_INFRAESTRUTURA":
                {
                    // alterna os campos para leitura
                    setaReadWrite_FichaInspecaoCadastral(tblFicha_INFRAESTRUTURA, true);

                    // mostra o botao editar
                    if ((quem.getAttribute('aria-expanded') == "false") || (expandir == 1)) {
                        document.getElementById("btn_Salvar_INFRAESTRUTURA").style.display = 'none';
                        document.getElementById("btn_Cancelar_INFRAESTRUTURA").style.display = 'none';
                        document.getElementById("btn_Editar_INFRAESTRUTURA").style.display = 'block';
                    }
                    else {
                        document.getElementById("btn_Editar_INFRAESTRUTURA").style.display = 'none';
                        if (quem.getAttribute('aria-expanded') == "true") {
                            document.getElementById("btn_Salvar_INFRAESTRUTURA").style.display = 'none';
                            document.getElementById("btn_Cancelar_INFRAESTRUTURA").style.display = 'none';
                        }
                        else {
                            document.getElementById("btn_Salvar_INFRAESTRUTURA").style.display = 'block';
                            document.getElementById("btn_Cancelar_INFRAESTRUTURA").style.display = 'block';
                        }
                    }

                    // roda o icone 90graus
                    document.getElementById("iconAngle_INFRAESTRUTURA").classList.toggle('rotate');
                    break;
                }

            case "btn_Toggle_ENCONTROS":
                {
                    // alterna os campos para leitura
                    setaReadWrite_FichaInspecaoCadastral(tblFicha_ENCONTROS, true);

                    // mostra o botao editar
                    if ((quem.getAttribute('aria-expanded') == "false") || (expandir == 1)) {
                        document.getElementById("btn_Salvar_ENCONTROS").style.display = 'none';
                        document.getElementById("btn_Cancelar_ENCONTROS").style.display = 'none';
                        document.getElementById("btn_Editar_ENCONTROS").style.display = 'block';
                    }
                    else {
                        document.getElementById("btn_Editar_ENCONTROS").style.display = 'none';
                        if (quem.getAttribute('aria-expanded') == "true") {
                            document.getElementById("btn_Salvar_ENCONTROS").style.display = 'none';
                            document.getElementById("btn_Cancelar_ENCONTROS").style.display = 'none';
                        }
                        else {
                            document.getElementById("btn_Salvar_ENCONTROS").style.display = 'block';
                            document.getElementById("btn_Cancelar_ENCONTROS").style.display = 'block';
                        }
                    }

                    // roda o icone 90graus
                    document.getElementById("iconAngle_ENCONTROS").classList.toggle('rotate');
                    break;
                }

            case "btn_Toggle_HISTORICO_INTERVENCOES":
                {

                    // alterna os campos para leitura
                    setaReadWrite_FichaInspecaoCadastral(tblFicha_HISTORICO_INTERVENCOES, true);

                    // mostra o botao editar
                    if ((quem.getAttribute('aria-expanded') == "false") || (expandir == 1)) {
                        document.getElementById("btn_Salvar_HISTORICO_INTERVENCOES").style.display = 'none';
                        document.getElementById("btn_Cancelar_HISTORICO_INTERVENCOES").style.display = 'none';
                        document.getElementById("btn_Editar_HISTORICO_INTERVENCOES").style.display = 'block';
                    }
                    else {
                        document.getElementById("btn_Editar_HISTORICO_INTERVENCOES").style.display = 'none';
                        if (quem.getAttribute('aria-expanded') == "true") {
                            document.getElementById("btn_Salvar_HISTORICO_INTERVENCOES").style.display = 'none';
                            document.getElementById("btn_Cancelar_HISTORICO_INTERVENCOES").style.display = 'none';
                        }
                        else {
                            document.getElementById("btn_Salvar_HISTORICO_INTERVENCOES").style.display = 'block';
                            document.getElementById("btn_Cancelar_HISTORICO_INTERVENCOES").style.display = 'block';
                        }
                    }




                    // roda o icone 90graus
                    document.getElementById("iconAngle_HISTORICO_INTERVENCOES").classList.toggle('rotate');
                    break;
                }
        }
}

function tica_chktxt(quem) {
    var chk = $('#' + quem.replace("txt", "chk"));
    var txt = $('#' + quem);

    if (txt.val().trim() != "")
        chk.prop('checked', true);
    else
        chk.prop('checked', false);

}
function chk_tick(quem) {

    var chk = $('#' + quem);
    var txt = $('#' + quem.replace("chk", "txt"));

    if (!chk.prop('checked'))
        txt.val("");

}

function cmb_atr_id_98_onchange() {
    var cmb_atr_id_98 = document.getElementById("cmb_atr_id_98");
    var cmb_atr_id_98Val = cmb_atr_id_98.options[cmb_atr_id_98.selectedIndex].value;
    var cmb_atr_id_98Text = cmb_atr_id_98.options[cmb_atr_id_98.selectedIndex].text;

    var tip_codigo = cmb_atr_id_98Text.substring(cmb_atr_id_98Text.lastIndexOf("(") + 1, cmb_atr_id_98Text.lastIndexOf(")"));
    var tip_nome = cmb_atr_id_98Text.substring(0, cmb_atr_id_98Text.lastIndexOf("("));
    var rodoviaValue = $("#txt_atr_id_106").val();
    var kmValue = $("#txt_atr_id_13").val();

    var desc = tip_nome + " " + rodoviaValue + "-" + kmValue + "-" + tip_codigo;

    $("#txt_atr_id_105").val(desc);


}

function cmb_atr_id_58_onchange()
{

    var cmb_atr_id_58 = document.getElementById("cmb_atr_id_58");

    if (cmb_atr_id_58) {
     //   var cmb_atr_id_58Val = cmb_atr_id_58.options[cmb_atr_id_58.selectedIndex].value;
        var cmb_atr_id_58Val = $("#cmb_atr_id_58").val();


        // se for secao circular, desabilita comprimento (item Dimensões dos Pilares)
        var txt_atr_id_60 = document.getElementById("txt_atr_id_60"); // para referencia de ehRead

        var txt_atr_id_171 = document.getElementById("txt_atr_id_171");
        if (txt_atr_id_171) {
            if ((cmb_atr_id_58Val == "100") || (txt_atr_id_60.disabled))
                txt_atr_id_171.disabled = true;
            else
                txt_atr_id_171.disabled = false;
        }

        //var txt_atr_id_173 = document.getElementById("txt_atr_id_173");
        //if (cmb_atr_id_58Val == "100") // se for secao circular, desabilita comprimento 
        //    txt_atr_id_173.disabled = true;
        //else
        //    txt_atr_id_173.disabled = false;

    }

}

