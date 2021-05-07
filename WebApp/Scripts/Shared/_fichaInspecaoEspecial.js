
//var selectedId_clo_id = -1;
//var selectedId_tip_id = -1;
//var moduloCorrente = '';

// var controlesReadOnlyFicha4 = ["txt_atr_id_13", "txt_atr_id_102", "txt_atr_id_105", "txt_atr_id_106", "txt_atr_id_107", "cmb_atr_id_130", "cmb_atr_id_131",
//"cmb_atr_id_1020", "cmb_atr_id_1084", "cmb_atr_id_1085", "cmb_atr_id_1087", "cmb_atr_id_1088", "cmb_atr_id_1089", "cmb_atr_id_1091", "cmb_atr_id_1092", "cmb_atr_id_1093", "cmb_atr_id_1094",
var controlesReadOnlyFicha4 = ["txt_atr_id_13", "txt_atr_id_102", "txt_atr_id_106", "cmb_atr_id_130", "cmb_atr_id_131",
    "cmb_atr_id_135", "cmb_atr_id_136", "cmb_atr_id_137", "cmb_atr_id_138", "cmb_atr_id_139", "cmb_atr_id_140", "cmb_atr_id_141", "cmb_atr_id_142", "cmb_atr_id_143", "cmb_atr_id_144", "txt_atr_id_151", "txt_atr_id_152", "txt_atr_id_153",
    , "txt_historico_Pontuacao_Geral_OAE_1", "txt_historico_documento_2", "txt_historico_data_2", "txt_historico_executantes_2", "txt_historico_Pontuacao_Geral_OAE_2", "txt_historico_documento_3", "txt_historico_data_3", "txt_historico_executantes_3", "txt_historico_Pontuacao_Geral_OAE_3"];

//var controlesExcecoes_Salvar = ["cmb_atr_id_130", "cmb_atr_id_131", "cmb_atr_id_135", "cmb_atr_id_136", "cmb_atr_id_137", "cmb_atr_id_138", "cmb_atr_id_139", "cmb_atr_id_140", "cmb_atr_id_141", "cmb_atr_id_142", "cmb_atr_id_143", "cmb_atr_id_144", "cmb_atr_id_148", "cmb_atr_id_150", "txt_atr_id_151", "txt_atr_id_152", "txt_atr_id_153", "txt_atr_id_157"];

var controles_Nao_Preencher_Valores = ["lblatr_id_157", "txt_atr_id_157", "txt_atr_id_99", "txt_atr_id_100", "txt_atr_id_145", "txt_atr_id_147", "txt_atr_id_149",
    // "cmb_atr_id_99", "cmb_atr_id_100", "cmb_atr_id_145", "cmb_atr_id_147", "cmb_atr_id_149"
];

function esquemaBrowse_onchange(File) {
    // var File = this.files;
    if (File && File[0]) {
        //UploadEsquema();
        var file = File[0]; // $('#esquemaBrowse').get(0).files;

        // checa o tamanho do arquivo
        var tamanhoMb = (file.size / 1024 / 1024);
        if (tamanhoMb > 2) {
            showMessage('warning', 'Aviso', 'O tamanho máximo permitido é 2Mb');
            return;
        }

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
        data.append("EsquemaEstrutural", File[0]);
        data.append("obj_id", selectedId_obj_id);
        data.append("atr_id", 159);


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
            url: "/Objeto/EsquemaEstrutural_Upload",
            data: data,
            contentType: false,
            processData: false,
            success: function (response) {
                if (response.data != "") {
                    //alert("ok");
                    document.getElementById("img_atr_id_159").setAttribute('src', response.data);

                    swal({
                        type: 'success',
                        title: 'Sucesso',
                        text: 'Arquivo salvo com sucesso'
                    });


                    return false;
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
    }
}

function accordion_encolher4(todos_menos) {
    // ENCOLHE TUDO

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

    document.getElementById("btn_Salvar_ESQUEMA_ESTRUTURAL").style.display = 'none';
    document.getElementById("btn_Cancelar_ESQUEMA_ESTRUTURALS").style.display = 'none';
    document.getElementById("btn_Salvar_CRITERIO_DE_CLASSIFICACAO").style.display = 'none';
    document.getElementById("btn_Cancelar_CRITERIO_DE_CLASSIFICACAO").style.display = 'none';
    document.getElementById("btn_Salvar_NOTA_OAE_PARAMETRO_FUNCIONAL").style.display = 'none';
    document.getElementById("btn_Cancelar_NOTA_OAE_PARAMETRO_FUNCIONAL").style.display = 'none';
    document.getElementById("btn_Salvar_POLITICA_ACOES_A_IMPLEMENTAR").style.display = 'none';
    document.getElementById("btn_Cancelar_POLITICA_ACOES_A_IMPLEMENTAR").style.display = 'none';


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

    $('#div_Ficha_ESQUEMA_ESTRUTURAL').collapse('hide');
    document.getElementById("btn_Editar_ESQUEMA_ESTRUTURAL").style.display = 'none';
    document.getElementById("iconAngle_ESQUEMA_ESTRUTURAL").classList.remove('rotate');

    $('#div_Ficha_ENCONTROS').collapse('hide');
    document.getElementById("btn_Editar_CRITERIO_DE_CLASSIFICACAO").style.display = 'none';
    document.getElementById("iconAngle_CRITERIO_DE_CLASSIFICACAO").classList.remove('rotate');

    $('#div_Ficha_NOTA_OAE_PARAMETRO_FUNCIONAL').collapse('hide');
    document.getElementById("btn_Editar_NOTA_OAE_PARAMETRO_FUNCIONAL").style.display = 'none';
    document.getElementById("iconAngle_NOTA_OAE_PARAMETRO_FUNCIONAL").classList.remove('rotate');

    $('#div_Ficha_POLITICA_ACOES_A_IMPLEMENTAR').collapse('hide');
    document.getElementById("btn_Editar_POLITICA_ACOES_A_IMPLEMENTAR").style.display = 'none';
    document.getElementById("iconAngle_POLITICA_ACOES_A_IMPLEMENTAR").classList.remove('rotate');




    document.getElementById("btn_Editar_DADOS_GERAIS").style.display = 'block';


}

function nome_segundo_cabecalho4(controleId) {
    var prefix = controleId.substring(0, controleId.lastIndexOf("_") + 1);
    var num = parseInt(controleId.substring(controleId.lastIndexOf("_") + 1)) + 1000;
    return (prefix + num);

}

function limpatblFicha4() {

    var tabela = document.getElementById("divFicha4");
    if (tabela) {
        // habilita ou desabilita todos os controles editaveis
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
function preenchetblFicha4(obj_id, classe, tipo) {
    classe = parseInt(classe);
    tipo = parseInt(tipo);
    selectedId_clo_id = classe;
    selectedId_tip_id = tipo;

    // a cada refresh, reseta o timer
    resetTimeout();


    // limpa antes de preencher
    limpatblFicha4();

    $('#txt_historico_data_1').datepicker({ dateFormat: 'dd/mm/yy' });

    var ord_id = 0;
    if ((paginaPai == "OrdemServico") || (paginaPai == "Inspecao"))
        ord_id = selectedId_ord_id;

    var url = "/Objeto/ObjAtributoValores_ListAll";
    var data = { "obj_id": obj_id, "ord_id": ord_id };

    if (moduloCorrente == 'OrdemServico') {
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


            for (var i = 0; i < result.data.length; i++) {

                // coloca a imagem ESQUEMA_ESTRUTURAL
                if (parseInt(result.data[i].atr_id) == 159) {
                    var img_atr_id_159 = document.getElementById("img_atr_id_159");
                    if (img_atr_id_159)
                        img_atr_id_159.setAttribute('src', result.data[i].atv_valor);
                }

                // preenche os LABELS
                var label = document.getElementById(result.data[i].atv_controle.replace("chk_", "lbl").replace("cmb_", "lbl"));
                if (label) {
                    // procura o label2
                    var label2 = document.getElementById((nome_segundo_cabecalho4(label.id)));

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
                if (
                    (!controles_Nao_Preencher_Valores.includes(result.data[i].atv_controle.replace("lbl", "txt_")))
                    && (!controles_Nao_Preencher_Valores.includes((nome_segundo_cabecalho4(result.data[i].atv_controle.replace("lbl", "txt_"))).replace("lbl", "txt_")))
                    && (!controles_Nao_Preencher_Valores.includes(result.data[i].atv_controle))
                    && (!controles_Nao_Preencher_Valores.includes((nome_segundo_cabecalho4(result.data[i].atv_controle))))
                ) {
                    if (parseInt(result.data[i].nItens) == 0) {
                        var textbox = document.getElementById(result.data[i].atv_controle.replace("lbl", "txt_"));
                        var textbox2 = document.getElementById((nome_segundo_cabecalho4(result.data[i].atv_controle.replace("lbl", "txt_"))).replace("lbl", "txt_"));
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
                            var combo2 = document.getElementById((nome_segundo_cabecalho4(result.data[i].atv_controle)));

                            if (combo) {
                                combo.innerText = null; // limpa

                                if (combo2)
                                    combo2.innerText = null; // limpa

                                // preenche combo
                                var lista = result.data[i].atr_itens_todos.split(";");
                                for (var m = 0; m < lista.length; m++) {
                                    var opt = document.createElement("option");
                                    opt.value = lista[m].substring(0, 3);
                                    opt.textContent = lista[m].substring(3);

                                    combo.appendChild(opt);
                                    if (combo2) {
                                        var opt2 = document.createElement("option");
                                        opt2.value = lista[m].substring(0, 3);
                                        opt2.textContent = lista[m].substring(3);
                                        combo2.appendChild(opt2);
                                    }
                                }

                                combo.value = result.data[i].atv_valor;
                                if (combo2)
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


            Ficha4_Calcula_Notas_Tudo();
        }
    });

    travaBotoes();

}

function Ficha4_setaReadWrite(tabela, ehRead) {
    // habilita ou desabilita todos os controles editaveis
    var lstTxtBoxes = tabela.getElementsByTagName('input');
    var lstCombos = tabela.getElementsByTagName('select');
    var lstTextareas = tabela.getElementsByTagName('textarea');

    var cmb_atr_id_98 = document.getElementById("cmb_atr_id_98");


    // trava o
    if ((moduloCorrente == 'Objetos') // se estiver em Cadastro de Objetos,
        || ((moduloCorrente == 'OrdemServico') && (cmb_atr_id_98.selectedIndex > 0)) // se estiver em OrdemServico e já houver tipo de OAE selecionada
    ) {
        controlesReadOnlyFicha4.push("cmb_atr_id_98"); // combo Tipo OAE
        controlesReadOnlyFicha4.push("txt_atr_id_105"); // descricao Tipo OAE
    }
    for (var i = 0; i < lstTxtBoxes.length; i++)
        if (!controlesReadOnlyFicha4.includes(lstTxtBoxes[i].id))
            lstTxtBoxes[i].disabled = ehRead;

    for (var i = 0; i < lstTextareas.length; i++)
        if (!controlesReadOnlyFicha4.includes(lstTextareas[i].id))
            lstTextareas[i].disabled = ehRead;

    for (var i = 0; i < lstCombos.length; i++)
        if (!controlesReadOnlyFicha4.includes(lstCombos[i].id))
            lstCombos[i].disabled = ehRead;
        else
            lstCombos[i].disabled = true;

    // =============== ALTERNA OS BOTOES SALVAR/CANCELAR/EDITAR =============================================================
    // alterna para os botoes de salvar/cancelar
    var display1 = 'block';
    var display2 = 'none';

    //if (ehRead) {
    //    display1 = 'none';
    //    display2 = 'block';
    //    document.getElementById("btnEsquemaUpload").style.display = 'none';
    //}
    //else
    //    document.getElementById("btnEsquemaUpload").style.display = 'unset';



    switch (tabela.id) {
        case "tblFicha4_DADOS_GERAIS":
            {
                document.getElementById("btn_Salvar_DADOS_GERAIS").style.display = display1;
                document.getElementById("btn_Cancelar_DADOS_GERAIS").style.display = display1;
                document.getElementById("btn_Editar_DADOS_GERAIS").style.display = display2;
                break;
            }
        case "tblFicha4_HISTORICO_INSPECOES":
            {
                document.getElementById("btn_Salvar_HISTORICO_INSPECOES").style.display = display1;
                document.getElementById("btn_Cancelar_HISTORICO_INSPECOES").style.display = display1;
                document.getElementById("btn_Editar_HISTORICO_INSPECOES").style.display = display2;
                break;
            }

        case "tblFicha4_ATRIBUTOS_FUNCIONAIS":
            {
                document.getElementById("btn_Salvar_ATRIBUTOS_FUNCIONAIS").style.display = display1;
                document.getElementById("btn_Cancelar_ATRIBUTOS_FUNCIONAIS").style.display = display1;
                document.getElementById("btn_Editar_ATRIBUTOS_FUNCIONAIS").style.display = display2;
                break;
            }

        case "tblFicha4_ATRIBUTOS_FIXOS":
            {
                document.getElementById("btn_Salvar_ATRIBUTOS_FIXOS").style.display = display1;
                document.getElementById("btn_Cancelar_ATRIBUTOS_FIXOS").style.display = display1;
                document.getElementById("btn_Editar_ATRIBUTOS_FIXOS").style.display = display2;
                break;
            }

        case "tblFicha4_SUPERESTRUTURA":
            {
                document.getElementById("btn_Salvar_SUPERESTRUTURA").style.display = display1;
                document.getElementById("btn_Cancelar_SUPERESTRUTURA").style.display = display1;
                document.getElementById("btn_Editar_SUPERESTRUTURA").style.display = display2;
                break;
            }

        case "tblFicha4_MESOESTRUTURA":
            {
                document.getElementById("btn_Salvar_MESOESTRUTURA").style.display = display1;
                document.getElementById("btn_Cancelar_MESOESTRUTURA").style.display = display1;
                document.getElementById("btn_Editar_MESOESTRUTURA").style.display = display2;
                break;
            }

        case "tblFicha4_INFRAESTRUTURA":
            {
                document.getElementById("btn_Salvar_INFRAESTRUTURA").style.display = display1;
                document.getElementById("btn_Cancelar_INFRAESTRUTURA").style.display = display1;
                document.getElementById("btn_Editar_INFRAESTRUTURA").style.display = display2;
                break;
            }

        case "tblFicha4_ENCONTROS":
            {
                document.getElementById("btn_Salvar_ENCONTROS").style.display = display1;
                document.getElementById("btn_Cancelar_ENCONTROS").style.display = display1;
                document.getElementById("btn_Editar_ENCONTROS").style.display = display2;
                break;
            }

        case "tblFicha4_DADOS_GERAIS2":
            {
                document.getElementById("btn_Salvar_DADOS_GERAIS2").style.display = display1;
                document.getElementById("btn_Cancelar_DADOS_GERAIS2").style.display = display1;
                document.getElementById("btn_Editar_DADOS_GERAIS2").style.display = display2;
                break;
            }

        case "tblFicha4_ESQUEMA_ESTRUTURAL":
            {
                document.getElementById("btn_Salvar_ESQUEMA_ESTRUTURAL").style.display = display1;
                document.getElementById("btn_Cancelar_ESQUEMA_ESTRUTURAL").style.display = display1;
                document.getElementById("btn_Editar_ESQUEMA_ESTRUTURAL").style.display = display2;
                break;
            }

        case "tblFicha4_CRITERIO_DE_CLASSIFICACAO":
            {
                document.getElementById("btn_Salvar_CRITERIO_DE_CLASSIFICACAO").style.display = display1;
                document.getElementById("btn_Cancelar_CRITERIO_DE_CLASSIFICACAO").style.display = display1;
                document.getElementById("btn_Editar_CRITERIO_DE_CLASSIFICACAO").style.display = display2;
                break;
            }

        case "tblFicha4_POLITICA_ACOES_A_IMPLEMENTAR":
            {
                //document.getElementById("btn_Salvar_POLITICA_ACOES_A_IMPLEMENTAR").style.display = display1;
                //document.getElementById("btn_Cancelar_POLITICA_ACOES_A_IMPLEMENTAR").style.display = display1;
                //document.getElementById("btn_Editar_POLITICA_ACOES_A_IMPLEMENTAR").style.display = display2;
                break;
            }

        case "tblFicha4_NOTA_OAE_PARAMETRO_FUNCIONAL":
            {
                document.getElementById("btn_Salvar_NOTA_OAE_PARAMETRO_FUNCIONAL").style.display = display1;
                document.getElementById("btn_Cancelar_NOTA_OAE_PARAMETRO_FUNCIONAL").style.display = display1;
                document.getElementById("btn_Editar_NOTA_OAE_PARAMETRO_FUNCIONAL").style.display = display2;
                break;
            }
    }


}

function CancelarDados_Ficha4(tabela) {
    preenchetblFicha4(selectedId_obj_id, selectedId_clo_id, selectedId_tip_id);

    // alterna os campos para leitura
    Ficha4_setaReadWrite(tabela, true);

}
function EditarDados_Ficha4(tabela) {
    // alterna os campos para escrita
    Ficha4_setaReadWrite(tabela, false);

    return false;
}

function SalvarDados_Ficha4(tabela) {
    var obj_id = selectedId_obj_id;
    var atr_id = -1;
    var ati_id = -1;
    var atv_valor = -1;
    var saida = '';

    var lstTextareas = tabela.getElementsByTagName('textarea');
    for (var i = 0; i < lstTextareas.length; i++) {
        if (!controlesReadOnlyFicha4.includes(lstTextareas[i].id))
            saida = saida + ";" + lstTextareas[i].id + ":" + lstTextareas[i].value;
    }

    var lstCombos = tabela.getElementsByTagName('select');
    for (var i = 0; i < lstCombos.length; i++) {
        if (!controlesReadOnlyFicha4.includes(lstCombos[i].id))
            if (lstCombos[i].selectedIndex > -1)
                saida = saida + ";" + lstCombos[i].id + ":" + lstCombos[i].options[lstCombos[i].selectedIndex].value;
    }


    var lstInputs = tabela.getElementsByTagName('input'); // lista de textbox + checkbox
    for (var i = 0; i < lstInputs.length; i++) {
        if ((!controlesReadOnlyFicha4.includes(lstInputs[i].id))
            || (lstInputs[i].id == "txt_historico_Pontuacao_Geral_OAE_1")) {
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
    var param = {
        obj_id: selectedId_obj_id,
        atr_id: -2,
        ati_id: -2,
        atv_valores: saida,
        nome_aba: tabela.id //.replace("tblFicha4_", "")
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
            if (moduloCorrente == 'OrdemServico')
                $('#tblOrdemServicos').DataTable().ajax.reload(null, false);  //false = sem reload na pagina.

            preenchetblFicha4(selectedId_obj_id, selectedId_clo_id, selectedId_tip_id);
            return false;
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
            return false;
        }
    });


    // alterna os campos para leitura
    Ficha4_setaReadWrite(tabela, true);
}

function SalvarDados_Ficha4_aux(tabela) {
    var saida = '';
    var array_filtrado = controlesReadOnlyFicha4.filter((el) => !controlesExcecoes_Salvar.includes(el));

    var lstInputs = tabela.getElementsByTagName('input'); // lista de textbox
    for (var i = 0; i < lstInputs.length; i++) {
        if (!array_filtrado.includes(lstInputs[i].id)) {
            var txtId = lstInputs[i].id;
            // tratamento para os itens id = 1000 + id;
            var atributo_id = parseInt(txtId.substring(txtId.lastIndexOf("_") + 1, 100));
            if (atributo_id > 1000)
                txtId = txtId.substring(0, txtId.lastIndexOf("_")) + "_" + (atributo_id - 1000);
            var txt = document.getElementById(txtId);
            if (txt)
                saida = saida + ";" + txtId + ":" + lstInputs[i].value;
        }
    }


    var lstCombos = tabela.getElementsByTagName('select'); // lista de combos
    for (var i = 0; i < lstCombos.length; i++) {
        if (!array_filtrado.includes(lstCombos[i].id))
            if (lstCombos[i].selectedIndex > -1) {
                var cmbId = lstCombos[i].id;
                // tratamento para os itens cujo id = 1000 + id;
                var atributo_id = parseInt(cmbId.substring(cmbId.lastIndexOf("_") + 1, 100));
                if (atributo_id > 1000)
                    cmbId = cmbId.substring(0, cmbId.lastIndexOf("_")) + "_" + (atributo_id - 1000);

                saida = saida + ";" + cmbId + ":" + lstCombos[i].options[lstCombos[i].selectedIndex].value;
            }
    }


    saida = saida.substr(1) + ";"; //  retira ponto e virgula do comeco acrescenta no final

    //alert(saida);
    // ******************** manda os dados para o banco *************************
    var param = {
        obj_id: selectedId_obj_id,
        atr_id: -2,
        ati_id: -2,
        atv_valores: saida,
        nome_aba: tabela.id
    };

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

            preenchetblFicha4(selectedId_obj_id, selectedId_clo_id, selectedId_tip_id);

            // alterna os campos para leitura
            Ficha4_setaReadWrite(tabela, true);

            return false;
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
            return false;
        }
    });



}
function SalvarDados_Ficha4_CRITERIO_DE_CLASSIFICACAO() {
    var tabela = document.getElementById("tblFicha4_CRITERIO_DE_CLASSIFICACAO");
    SalvarDados_Ficha4_aux(tabela);

    tabela = document.getElementById("tblFicha4_POLITICA_ACOES_A_IMPLEMENTAR");
    SalvarDados_Ficha4_aux(tabela);
}
function SalvarDados_Ficha4_NOTA_OAE_PARAMETRO_FUNCIONAL() {
    var tabela = document.getElementById("tblFicha4_NOTA_OAE_PARAMETRO_FUNCIONAL");
    SalvarDados_Ficha4(tabela);

    tabela = document.getElementById("tblFicha4_POLITICA_ACOES_A_IMPLEMENTAR");
    SalvarDados_Ficha4(tabela);
}

function header_click4(quem, expandir) {
    if (expandir == null)
        expandir = 0;

    if (selectedId_clo_id <= 2) {
        expandir = 0;
        accordion_encolher4(2);
    }
    else
        switch (quem.id) {
            case "btn_Toggle_HISTORICO_INSPECOES":
                {
                    // alterna os campos para leitura
                    Ficha4_setaReadWrite(tblFicha4_HISTORICO_INSPECOES, true);

                    // mostra o botao editar
                    if ((quem.getAttribute('aria-expanded') == "false") || (expandir == 1)) {
                        document.getElementById("btn_Salvar_HISTORICO_INSPECOES").style.display = 'none';
                        document.getElementById("btn_Cancelar_HISTORICO_INSPECOES").style.display = 'none';
                        document.getElementById("btn_Editar_HISTORICO_INSPECOES").style.display = 'block';
                    }
                    else {
                        document.getElementById("btn_Editar_HISTORICO_INSPECOES").style.display = 'none';
                        if (quem.getAttribute('aria-expanded') == "true") {
                            document.getElementById("btn_Salvar_HISTORICO_INSPECOES").style.display = 'none';
                            document.getElementById("btn_Cancelar_HISTORICO_INSPECOES").style.display = 'none';
                        }
                        else {
                            document.getElementById("btn_Salvar_HISTORICO_INSPECOES").style.display = 'block';
                            document.getElementById("btn_Cancelar_HISTORICO_INSPECOES").style.display = 'block';
                        }

                    }

                    // roda o icone 90graus
                    document.getElementById("iconAngle_HISTORICO_INSPECOES").classList.toggle('rotate');
                    break;
                }

            case "btn_Toggle_ATRIBUTOS_FUNCIONAIS":
                {
                    // alterna os campos para leitura
                    Ficha4_setaReadWrite(tblFicha4_ATRIBUTOS_FUNCIONAIS, true);

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
                    Ficha4_setaReadWrite(tblFicha4_ATRIBUTOS_FIXOS, true);

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
                    Ficha4_setaReadWrite(tblFicha4_SUPERESTRUTURA, true);

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
                    Ficha4_setaReadWrite(tblFicha4_MESOESTRUTURA, true);

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
                    Ficha4_setaReadWrite(tblFicha4_INFRAESTRUTURA, true);

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
                    Ficha4_setaReadWrite(tblFicha4_ENCONTROS, true);

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

            case "btn_Toggle_ESQUEMA_ESTRUTURAL":
                {
                    // alterna os campos para leitura
                    Ficha4_setaReadWrite(tblFicha4_ESQUEMA_ESTRUTURAL, true);

                    // mostra o botao editar
                    if ((quem.getAttribute('aria-expanded') == "false") || (expandir == 1)) {
                        document.getElementById("btn_Salvar_ESQUEMA_ESTRUTURAL").style.display = 'none';
                        document.getElementById("btn_Cancelar_ESQUEMA_ESTRUTURAL").style.display = 'none';
                        document.getElementById("btn_Editar_ESQUEMA_ESTRUTURAL").style.display = 'block';
                    }
                    else {
                        document.getElementById("btn_Editar_ESQUEMA_ESTRUTURAL").style.display = 'none';
                        if (quem.getAttribute('aria-expanded') == "true") {
                            document.getElementById("btn_Salvar_ESQUEMA_ESTRUTURAL").style.display = 'none';
                            document.getElementById("btn_Cancelar_ESQUEMA_ESTRUTURAL").style.display = 'none';
                        }
                        else {
                            document.getElementById("btn_Salvar_ESQUEMA_ESTRUTURAL").style.display = 'block';
                            document.getElementById("btn_Cancelar_ESQUEMA_ESTRUTURAL").style.display = 'block';
                        }
                    }

                    // roda o icone 90graus
                    document.getElementById("iconAngle_ESQUEMA_ESTRUTURAL").classList.toggle('rotate');
                    break;
                }

            case "btn_Toggle_CRITERIO_DE_CLASSIFICACAO":
                {
                    // alterna os campos para leitura
                    Ficha4_setaReadWrite(tblFicha4_CRITERIO_DE_CLASSIFICACAO, true);

                    // mostra o botao editar
                    if ((quem.getAttribute('aria-expanded') == "false") || (expandir == 1)) {
                        document.getElementById("btn_Salvar_CRITERIO_DE_CLASSIFICACAO").style.display = 'none';
                        document.getElementById("btn_Cancelar_CRITERIO_DE_CLASSIFICACAO").style.display = 'none';
                        document.getElementById("btn_Editar_CRITERIO_DE_CLASSIFICACAO").style.display = 'block';
                    }
                    else {
                        document.getElementById("btn_Editar_CRITERIO_DE_CLASSIFICACAO").style.display = 'none';
                        if (quem.getAttribute('aria-expanded') == "true") {
                            document.getElementById("btn_Salvar_CRITERIO_DE_CLASSIFICACAO").style.display = 'none';
                            document.getElementById("btn_Cancelar_CRITERIO_DE_CLASSIFICACAO").style.display = 'none';
                        }
                        else {
                            document.getElementById("btn_Salvar_CRITERIO_DE_CLASSIFICACAO").style.display = 'block';
                            document.getElementById("btn_Cancelar_CRITERIO_DE_CLASSIFICACAO").style.display = 'block';
                        }
                    }

                    //calcula as notas
                    Ficha4_Calcula_Notas_2_Requisito();

                    // roda o icone 90graus
                    document.getElementById("iconAngle_CRITERIO_DE_CLASSIFICACAO").classList.toggle('rotate');
                    break;
                }

            case "btn_Toggle_NOTA_OAE_PARAMETRO_FUNCIONAL":
                {
                    // alterna os campos para leitura
                    Ficha4_setaReadWrite(tblFicha4_NOTA_OAE_PARAMETRO_FUNCIONAL, true);

                    // mostra o botao editar
                    if ((quem.getAttribute('aria-expanded') == "false") || (expandir == 1)) {
                        document.getElementById("btn_Salvar_NOTA_OAE_PARAMETRO_FUNCIONAL").style.display = 'none';
                        document.getElementById("btn_Cancelar_NOTA_OAE_PARAMETRO_FUNCIONAL").style.display = 'none';
                        document.getElementById("btn_Editar_NOTA_OAE_PARAMETRO_FUNCIONAL").style.display = 'block';
                    }
                    else {
                        document.getElementById("btn_Editar_NOTA_OAE_PARAMETRO_FUNCIONAL").style.display = 'none';
                        if (quem.getAttribute('aria-expanded') == "true") {
                            document.getElementById("btn_Salvar_NOTA_OAE_PARAMETRO_FUNCIONAL").style.display = 'none';
                            document.getElementById("btn_Cancelar_NOTA_OAE_PARAMETRO_FUNCIONAL").style.display = 'none';
                        }
                        else {
                            document.getElementById("btn_Salvar_NOTA_OAE_PARAMETRO_FUNCIONAL").style.display = 'block';
                            document.getElementById("btn_Cancelar_NOTA_OAE_PARAMETRO_FUNCIONAL").style.display = 'block';
                        }
                    }

                    // roda o icone 90graus
                    document.getElementById("iconAngle_NOTA_OAE_PARAMETRO_FUNCIONAL").classList.toggle('rotate');
                    break;
                }

            case "btn_Toggle_POLITICA_ACOES_A_IMPLEMENTAR":
                {
                    //// alterna os campos para leitura
                    //    Ficha4_setaReadWrite(tblFicha4_POLITICA_ACOES_A_IMPLEMENTAR, true);

                    //// mostra o botao editar
                    //if ((quem.getAttribute('aria-expanded') == "false") || (expandir == 1)) {
                    //    document.getElementById("btn_Salvar_POLITICA_ACOES_A_IMPLEMENTAR").style.display = 'none';
                    //    document.getElementById("btn_Cancelar_POLITICA_ACOES_A_IMPLEMENTAR").style.display = 'none';
                    //    document.getElementById("btn_Editar_POLITICA_ACOES_A_IMPLEMENTAR").style.display = 'block';
                    //}
                    //else {
                    //    document.getElementById("btn_Editar_POLITICA_ACOES_A_IMPLEMENTAR").style.display = 'none';
                    //    if (quem.getAttribute('aria-expanded') == "true") {
                    //        document.getElementById("btn_Salvar_POLITICA_ACOES_A_IMPLEMENTAR").style.display = 'none';
                    //        document.getElementById("btn_Cancelar_POLITICA_ACOES_A_IMPLEMENTAR").style.display = 'none';
                    //    }
                    //    else {
                    //        document.getElementById("btn_Salvar_POLITICA_ACOES_A_IMPLEMENTAR").style.display = 'block';
                    //        document.getElementById("btn_Cancelar_POLITICA_ACOES_A_IMPLEMENTAR").style.display = 'block';
                    //    }
                    //}

                    // roda o icone 90graus
                    document.getElementById("iconAngle_POLITICA_ACOES_A_IMPLEMENTAR").classList.toggle('rotate');
                    break;
                }

        }

    travaBotoes();

}

function tica_chktxt4(quem) {
    var chk = $('#' + quem.replace("txt", "chk"));
    var txt = $('#' + quem);

    if (txt.val().trim() != "")
        chk.prop('checked', true);
    else
        chk.prop('checked', false);

}
function chk_tick4(quem) {

    var chk = $('#' + quem);
    var txt = $('#' + quem.replace("chk", "txt"));

    if (!chk.prop('checked'))
        txt.val("");

}

function Ficha4_cmb_atr_id_98_onchange() {
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


// CALCULO DE NOTAS
function Ficha4_Calcula_Notas_1_Requisito() {
    var cmb_atr_id_99 = document.getElementById("cmb_atr_id_99");
    var cmb_atr_id_99_val = 0;
    if (cmb_atr_id_99.selectedIndex > -1)
        cmb_atr_id_99_val = parseInt(cmb_atr_id_99.options[cmb_atr_id_99.selectedIndex].text);
    else
        cmb_atr_id_99_val = 0;

    var cmb_atr_id_100 = document.getElementById("cmb_atr_id_100");
    var cmb_atr_id_100_val = 0;
    if (cmb_atr_id_100.selectedIndex > -1)
        cmb_atr_id_100_val = parseInt(cmb_atr_id_100.options[cmb_atr_id_100.selectedIndex].text);
    else
        cmb_atr_id_100_val = 0;


    var cmb_atr_id_130 = document.getElementById("cmb_atr_id_130");
    var cmb_atr_id_131 = document.getElementById("cmb_atr_id_131");
    cmb_atr_id_130.selectedIndex = cmb_atr_id_99.selectedIndex;
    cmb_atr_id_131.selectedIndex = cmb_atr_id_100.selectedIndex;

    if (cmb_atr_id_130.selectedIndex > -1)
        document.getElementById("lblcmb_130").innerText = cmb_atr_id_130.options[cmb_atr_id_130.selectedIndex].text;
    else
        document.getElementById("lblcmb_130").innerText = "";

    if (cmb_atr_id_131.selectedIndex > -1)
        document.getElementById("lblcmb_131").innerText = cmb_atr_id_131.options[cmb_atr_id_131.selectedIndex].text;
    else
        document.getElementById("lblcmb_131").innerText = "";

    document.getElementById("txt_atr_id_151").value = cmb_atr_id_99_val + cmb_atr_id_100_val;


    var soma22 = document.getElementById("txt_atr_id_152").value; // = parseInt(cmb_atr_id_100_val);
    if (isNaN(soma22))
        soma2 = 0;
    else
        soma2 = parseFloat(soma22);

    var total = parseFloat(soma22) + cmb_atr_id_99_val + cmb_atr_id_100_val;
    document.getElementById("txt_atr_id_157").value = total.toFixed(2);
    document.getElementById("txt_historico_Pontuacao_Geral_OAE_1").value = total.toFixed(2);

    // preenche o ultimo item do menu acordeon
    var cmb1_Nota = document.getElementById("cmb_atr_id_147");
    var cmb1_Desc = document.getElementById("cmb_atr_id_148");

    if ((cmb_atr_id_99_val <= cmb_atr_id_100_val) && (cmb_atr_id_99_val > 0)) {
        cmb1_Nota.selectedIndex = cmb_atr_id_99.selectedIndex;
        cmb1_Desc.selectedIndex = cmb_atr_id_99.selectedIndex;
        document.getElementById("lblcmb_148").innerText = cmb1_Desc.options[cmb_atr_id_99.selectedIndex].text;
    }
    else
        if ((cmb_atr_id_99_val > cmb_atr_id_100_val) && (cmb_atr_id_100_val > 0)) {
            cmb1_Nota.selectedIndex = cmb_atr_id_100.selectedIndex;
            cmb1_Desc.selectedIndex = cmb_atr_id_100.selectedIndex;
            document.getElementById("lblcmb_148").innerText = cmb1_Desc.options[cmb_atr_id_100.selectedIndex].text;
        }

}
function Ficha4_Calcula_Notas_2_Requisito() {
    // localiza os controles
    var cmb1 = document.getElementById("cmb_atr_id_1084"); var cmb1_Nota = document.getElementById("cmb_atr_id_135");
    var cmb2 = document.getElementById("cmb_atr_id_1085"); var cmb2_Nota = document.getElementById("cmb_atr_id_136");
    var cmb3 = document.getElementById("cmb_atr_id_1091"); var cmb3_Nota = document.getElementById("cmb_atr_id_137");
    var cmb4 = document.getElementById("cmb_atr_id_1087"); var cmb4_Nota = document.getElementById("cmb_atr_id_138");
    var cmb5 = document.getElementById("cmb_atr_id_1088"); var cmb5_Nota = document.getElementById("cmb_atr_id_139");
    var cmb6 = document.getElementById("cmb_atr_id_1089"); var cmb6_Nota = document.getElementById("cmb_atr_id_140");
    var cmb7 = document.getElementById("cmb_atr_id_1093"); var cmb7_Nota = document.getElementById("cmb_atr_id_141");
    var cmb8 = document.getElementById("cmb_atr_id_1020"); var cmb8_Nota = document.getElementById("cmb_atr_id_142");
    var cmb9 = document.getElementById("cmb_atr_id_1092"); var cmb9_Nota = document.getElementById("cmb_atr_id_143");
    var cmb10 = document.getElementById("cmb_atr_id_1094"); var cmb10_Nota = document.getElementById("cmb_atr_id_144");

    // posiciona os combos Nota
    cmb1_Nota.selectedIndex = cmb1.selectedIndex;
    cmb2_Nota.selectedIndex = cmb2.selectedIndex;
    cmb3_Nota.selectedIndex = cmb3.selectedIndex;
    cmb4_Nota.selectedIndex = cmb4.selectedIndex;
    cmb5_Nota.selectedIndex = cmb5.selectedIndex;
    cmb6_Nota.selectedIndex = cmb6.selectedIndex;
    cmb7_Nota.selectedIndex = cmb7.selectedIndex;
    cmb8_Nota.selectedIndex = cmb8.selectedIndex;
    cmb9_Nota.selectedIndex = cmb9.selectedIndex;
    cmb10_Nota.selectedIndex = cmb10.selectedIndex;

    // acha os valores
    var cmb_val1 = 0; var cmb_val2 = 0; var cmb_val3 = 0; var cmb_val4 = 0; var cmb_val5 = 0;
    var cmb_val6 = 0; var cmb_val7 = 0; var cmb_val8 = 0; var cmb_val9 = 0; var cmb_val10 = 0;

    if (cmb1_Nota.selectedIndex > -1) cmb_val1 = parseFloat(cmb1_Nota.options[cmb1_Nota.selectedIndex].text);
    if (cmb2_Nota.selectedIndex > -1) cmb_val2 = parseFloat(cmb2_Nota.options[cmb2_Nota.selectedIndex].text);
    if (cmb3_Nota.selectedIndex > -1) cmb_val3 = parseFloat(cmb3_Nota.options[cmb3_Nota.selectedIndex].text);
    if (cmb4_Nota.selectedIndex > -1) cmb_val4 = parseFloat(cmb4_Nota.options[cmb4_Nota.selectedIndex].text);
    if (cmb5_Nota.selectedIndex > -1) cmb_val5 = parseFloat(cmb5_Nota.options[cmb5_Nota.selectedIndex].text);
    if (cmb6_Nota.selectedIndex > -1) cmb_val6 = parseFloat(cmb6_Nota.options[cmb6_Nota.selectedIndex].text);
    if (cmb7_Nota.selectedIndex > -1) cmb_val7 = parseFloat(cmb7_Nota.options[cmb7_Nota.selectedIndex].text);
    if (cmb8_Nota.selectedIndex > -1) cmb_val8 = parseFloat(cmb8_Nota.options[cmb8_Nota.selectedIndex].text);
    if (cmb9_Nota.selectedIndex > -1) cmb_val9 = parseFloat(cmb9_Nota.options[cmb9_Nota.selectedIndex].text);
    if (cmb10_Nota.selectedIndex > -1) cmb_val10 = parseFloat(cmb10_Nota.options[cmb10_Nota.selectedIndex].text);

    // calcula a nota
    var soma2 = cmb_val1 + cmb_val2 + cmb_val3 + cmb_val4 + cmb_val5 + cmb_val6 + cmb_val7 + cmb_val8 + cmb_val9 + cmb_val10;
    document.getElementById("txt_atr_id_152").value = soma2.toFixed(2);

    var soma11 = document.getElementById("txt_atr_id_151").value; // = parseInt(cmb_atr_id_100_val);
    var soma1 = 0;
    if ((isNaN(soma11)) || (soma11.trim() == ""))
        soma1 = 0;
    else
        soma1 = parseInt(soma11);

    var total = parseFloat(soma2) + soma1;
    document.getElementById("txt_atr_id_157").value = total.toFixed(2);
    document.getElementById("txt_historico_Pontuacao_Geral_OAE_1").value = total.toFixed(2);
}
function Ficha4_Calcula_Nota_Parametro_Funcional() {
    var cmb1 = document.getElementById("cmb_atr_id_145");
    var cmb = document.getElementById("cmb_atr_id_146");

    cmb.selectedIndex = cmb1.selectedIndex;

    var cmb_atr_id_146_val = 0;
    if (cmb1.selectedIndex > -1)
        cmb_atr_id_146_val = cmb.options[cmb.selectedIndex].text;

    if (cmb1.selectedIndex > -1)
        document.getElementById("txt_atr_id_153").value = cmb1.options[cmb1.selectedIndex].text;
    else
        document.getElementById("txt_atr_id_153").value = 0;

    document.getElementById("lblcmb_146").innerText = cmb_atr_id_146_val;


    // preenche o ultimo item do menu acordeon
    var cmb2_Nota = document.getElementById("cmb_atr_id_149");
    var cmb2_Desc = document.getElementById("cmb_atr_id_150");

    if (cmb1.selectedIndex > -1) {
        cmb2_Nota.selectedIndex = cmb1.selectedIndex;
        cmb2_Desc.selectedIndex = cmb1.selectedIndex;
        document.getElementById("lblcmb_150").innerText = cmb2_Desc.options[cmb2_Desc.selectedIndex].text;
    }

}
function Ficha4_Calcula_Notas_Tudo() {
    Ficha4_Calcula_Notas_1_Requisito();
    Ficha4_Calcula_Notas_2_Requisito();
    Ficha4_Calcula_Nota_Parametro_Funcional();
}


