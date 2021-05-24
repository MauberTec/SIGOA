    var controlesReadOnlyFichaRequalificacaoOAE = ["txt_atr_id_13", "txt_atr_id_102", "txt_atr_id_106", "cmb_atr_id_130", "cmb_atr_id_131", "cmb_atr_id_135", "cmb_atr_id_136", "cmb_atr_id_137", "cmb_atr_id_138", "cmb_atr_id_139", "cmb_atr_id_140", "cmb_atr_id_141", "cmb_atr_id_142", "cmb_atr_id_143", "cmb_atr_id_144", "txt_atr_id_151", "txt_atr_id_152", "txt_atr_id_153"
      // , "cmb_atr_id_84", "cmb_atr_id_1084"
       //,"txt_historico_Pontuacao_Geral_OAE_1", "txt_historico_documento_2", "txt_historico_data_2", "txt_historico_executantes_2", "txt_historico_Pontuacao_Geral_OAE_2", "txt_historico_documento_3", "txt_historico_data_3", "txt_historico_executantes_3", "txt_historico_Pontuacao_Geral_OAE_3"
    ];

    var controlesExcecoes_Salvar = ["cmb_atr_id_130", "cmb_atr_id_131", "cmb_atr_id_135", "cmb_atr_id_136", "cmb_atr_id_137", "cmb_atr_id_138", "cmb_atr_id_139", "cmb_atr_id_140", "cmb_atr_id_141", "cmb_atr_id_142", "cmb_atr_id_143", "cmb_atr_id_144", "cmb_atr_id_148", "cmb_atr_id_150", "txt_atr_id_151", "txt_atr_id_152", "txt_atr_id_153", "txt_atr_id_157"];

    var msgVDM = 0;

    function FichaRequalificacaoOAE_header_click(quem, expandir) {
        if (expandir == null)
            expandir = 0;

        if (selectedId_clo_id <= 2) {
            expandir = 0;
            accordion_encolher(2);
        }
        else
            switch (quem.id) {
                case "btn_Toggle_CRITERIO_DE_CLASSIFICACAO":
                    {
                        // alterna os campos para leitura
                        FichaRequalificacaoOAE_setaReadWrite(tblFichaRequalificacaoOAE_CRITERIO_DE_CLASSIFICACAO, true);

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
                        FichaRequalificacaoOAE_Calcula_Notas_2_Requisito();

                        // roda o icone 90graus
                        document.getElementById("iconAngle_CRITERIO_DE_CLASSIFICACAO").classList.toggle('rotate');
                        break;
                    }

                case "btn_Toggle_NOTA_OAE_PARAMETRO_FUNCIONAL":
                    {
                        // alterna os campos para leitura
                        FichaRequalificacaoOAE_setaReadWrite(tblFichaRequalificacaoOAE_NOTA_OAE_PARAMETRO_FUNCIONAL, true);

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
                        // roda o icone 90graus
                        document.getElementById("iconAngle_POLITICA_ACOES_A_IMPLEMENTAR").classList.toggle('rotate');
                        break;
                    }
            }

        travaBotoes();
    }

    function FichaRequalificacaoOAE_limpar() {

        var tabela = document.getElementById("divFichaRequalificacaoOAE");

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

    function preenchetblFichaRequalificacaoOAE(obj_id, classe, tipo)
    {
        classe = parseInt(classe);
        tipo = parseInt(tipo);
        selectedId_clo_id = classe;
        selectedId_tip_id = tipo;

        // a cada refresh, reseta o timer
        resetTimeout();


        // limpa antes de preencher
        FichaRequalificacaoOAE_limpar();

        var ord_id = selectedId_ord_id;

       var url = "/Inspecao/InspecaoAtributosValores_ListAll";
       var data = { "ord_id": selectedId_ord_id };

        $.ajax({
            "url": url,
            "type": "GET",
            "datatype": "json",
            "data": data,
            "success": function (result) {

                var iTipoPista = 0;
                for (var i = 0; i < result.data.length; i++) {

                    // preenche os LABELS
                    var label = document.getElementById(result.data[i].atv_controle.replace("chk_", "lbl").replace("cmb_", "lbl"));
                    if (label) {
                        var texto = result.data[i].atr_atributo_nome;
                        if (texto.includes('|')) {
                            partes = texto.split("|");
                            texto = partes[partes.length - 1].trim();
                        }
                        label.innerText = texto;
                    }

                    // preenche o valor se houver
                    if (parseInt(result.data[i].nItens) == 0) {
                        var textbox = document.getElementById(result.data[i].atv_controle.replace("lbl", "txt_"));
                        var mascara = result.data[i].atr_mascara_texto;
                        if (textbox) {
                            textbox.value = result.data[i].atv_valor;
                            textbox.setAttribute('title', result.data[i].atv_valor);

                            // coloca mascara no textbox
                            if (mascara != "") {
                                jQuery(textbox).mask(mascara);
                                jQuery(textbox).attr('placeholder', mascara.replace(/9/g, '0'));
                            }

                        }
                     }
                    else
                        if (result.data[i].atr_apresentacao_itens == 'combobox') {
                            var combo = document.getElementById(result.data[i].atv_controle);
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

                                if ((result.data[i].atv_controle == "cmb_atr_id_15") && 
                                    (parseInt(result.data[i].atv_valores)) == (parseInt(result.data[i].ati_ids)))
                                       iTipoPista = result.data[i].atv_valor;
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

                FichaRequalificacaoOAE_Calcula_Notas_Tudo();

                if (msgVDM == 0)
                    FichaRequalificacaoOAE_BuscaValorVDM(iTipoPista);
            }
        });


        travaBotoes();
    }

    function FichaRequalificacaoOAE_setaReadWrite(tabela, ehRead) {
        // habilita ou desabilita todos os controles editaveis
        var lstTxtBoxes = tabela.getElementsByTagName('input');
        var lstCombos = tabela.getElementsByTagName('select');
        var lstTextareas = tabela.getElementsByTagName('textarea');

        var cmb_atr_id_98 = document.getElementById("cmb_atr_id_98");

        for (var i = 0; i < lstTxtBoxes.length; i++)
            if (!controlesReadOnlyFichaRequalificacaoOAE.includes(lstTxtBoxes[i].id))
                lstTxtBoxes[i].disabled = ehRead;

        for (var i = 0; i < lstTextareas.length; i++)
            if (!controlesReadOnlyFichaRequalificacaoOAE.includes(lstTextareas[i].id))
                lstTextareas[i].disabled = ehRead;

        for (var i = 0; i < lstCombos.length; i++)
            if (!controlesReadOnlyFichaRequalificacaoOAE.includes(lstCombos[i].id))
                lstCombos[i].disabled = ehRead;
            else
                lstCombos[i].disabled = true;

        // botoes da lixeira da tabela GRUPOS criada dinamicamente
        if (tabela.id == "tblFichaRequalificacaoOAE_INSPECAO_ROTINEIRA") // tabela de grupos da ficha 2
        {
            var tblFicha_2_GRUPOS = document.getElementById("tblFicha_2_GRUPOS");
            var lstButtons = tblFicha_2_GRUPOS.getElementsByTagName('button');

            for (var i = 0; i < lstButtons.length; i++)
                lstButtons[i].style.display = ehRead ? 'none' : 'block'; // aqui display block/none; na criacao da tabela visibility: visible/hidden (para nao misturar porque la existe validacao)

        }


        // =============== ALTERNA OS BOTOES SALVAR/CANCELAR/EDITAR =============================================================
        // alterna para os botoes de salvar/cancelar
        switch (tabela.id) {
            case "tblFichaRequalificacaoOAE_CRITERIO_DE_CLASSIFICACAO":
                {
                    document.getElementById("btn_Salvar_CRITERIO_DE_CLASSIFICACAO").style.display = 'none';
                    document.getElementById("btn_Cancelar_CRITERIO_DE_CLASSIFICACAO").style.display = 'none';
                    document.getElementById("btn_Editar_CRITERIO_DE_CLASSIFICACAO").style.display = 'block';
                    break;
                }

            case "tblFichaRequalificacaoOAE_POLITICA_ACOES_A_IMPLEMENTAR":
                {
                    //document.getElementById("btn_Salvar_POLITICA_ACOES_A_IMPLEMENTAR").style.display = 'none';
                    //document.getElementById("btn_Cancelar_POLITICA_ACOES_A_IMPLEMENTAR").style.display = 'none';
                    //document.getElementById("btn_Editar_POLITICA_ACOES_A_IMPLEMENTAR").style.display = 'block';
                    break;
                }

            case "tblFichaRequalificacaoOAE_NOTA_OAE_PARAMETRO_FUNCIONAL":
                {
                    document.getElementById("btn_Salvar_NOTA_OAE_PARAMETRO_FUNCIONAL").style.display = 'none';
                    document.getElementById("btn_Cancelar_NOTA_OAE_PARAMETRO_FUNCIONAL").style.display = 'none';
                    document.getElementById("btn_Editar_NOTA_OAE_PARAMETRO_FUNCIONAL").style.display = 'block';
                    break;
                }
        }
    }

    function getTipoId(aux) {
        return parseInt(aux.substring(0, aux.indexOf(":")));
    }
    function getTipoCodigo(aux) {
        return mascara = aux.substring(aux.indexOf(":") + 1, 150);
    }

    function EditarDados_FichaRequalificacaoOAE(tabela) {

        // alterna os campos para escrita
        FichaRequalificacaoOAE_setaReadWrite(tabela, false);

        // alterna para os botoes de salvar/cancelar
        switch (tabela.id) {
            case "tblFichaRequalificacaoOAE_CRITERIO_DE_CLASSIFICACAO":
                {
                    document.getElementById("btn_Salvar_CRITERIO_DE_CLASSIFICACAO").style.display = 'block';
                    document.getElementById("btn_Cancelar_CRITERIO_DE_CLASSIFICACAO").style.display = 'block';
                    document.getElementById("btn_Editar_CRITERIO_DE_CLASSIFICACAO").style.display = 'none';
                    break;
                }

            case "tblFichaRequalificacaoOAE_NOTA_OAE_PARAMETRO_FUNCIONAL":
                {
                    document.getElementById("btn_Salvar_NOTA_OAE_PARAMETRO_FUNCIONAL").style.display = 'block';
                    document.getElementById("btn_Cancelar_NOTA_OAE_PARAMETRO_FUNCIONAL").style.display = 'block';
                    document.getElementById("btn_Editar_NOTA_OAE_PARAMETRO_FUNCIONAL").style.display = 'none';
                    break;
                }

            case "tblFichaRequalificacaoOAE_POLITICA_ACOES_A_IMPLEMENTAR":
                {
                    document.getElementById("btn_Salvar_POLITICA_ACOES_A_IMPLEMENTAR").style.display = 'block';
                    document.getElementById("btn_Cancelar_POLITICA_ACOES_A_IMPLEMENTAR").style.display = 'block';
                    document.getElementById("btn_Editar_POLITICA_ACOES_A_IMPLEMENTAR").style.display = 'none';
                    break;
                }
        }

        return false;
    }

    function SalvarDados_FichaRequalificacaoOAE(tabela) {
        var saida = '';
        var array_filtrado = controlesReadOnlyFichaRequalificacaoOAE.filter((el) => !controlesExcecoes_Salvar.includes(el));

        var lstInputsList = tabela.getElementsByTagName('input'); // lista de textbox

        // convert nodelist to array
        var lstInputs = [];
        for (var i = 0; i < lstInputsList.length; i++) 
            lstInputs.push(lstInputsList[i].id);

        lstInputs.push("txt_atr_id_157");

        for (var i = 0; i < lstInputs.length; i++) {
            if ((!array_filtrado.includes(lstInputs[i]))
            || (lstInputs[i] == "txt_historico_Pontuacao_Geral_OAE_1"))
            {
                var txtId = lstInputs[i];

                // tratamento para os itens id = 1000 + id;
                var atributo_id = parseInt(txtId.substring(txtId.lastIndexOf("_") + 1, 100));
                if (atributo_id > 1000)
                    txtId = txtId.substring(0, txtId.lastIndexOf("_")) + "_" + (atributo_id - 1000);
                var txt = document.getElementById(txtId);
                if (txt)
                    saida = saida + ";" + txtId + ":" + txt.value;
            }
        }

        var lstCombos1 = tabela.getElementsByTagName('select');
        var lstCombos = [];

        var cmb_atr_id_84 = document.getElementById("cmb_atr_id_84");
        if (cmb_atr_id_84)
            lstCombos.push(cmb_atr_id_84);

        // transforma o Object List em Array
        for (var i = 0; i < lstCombos1.length; i++) {
            var cmb_ = document.getElementById(lstCombos1[i].id);
            if (cmb_)
                lstCombos.push(cmb_);
        }

        for (var i = 0; i < lstCombos.length; i++) {
            if ((!array_filtrado.includes(lstCombos[i].id))
                 || (lstCombos[i].id == "cmb_atr_id_84"))

                if (lstCombos[i].selectedIndex > -1) {
                    var cmbId = lstCombos[i].id;

                    if ((cmbId == "")) {

                    }
                    else {
                        // tratamento para os itens cujo id = 1000 + id;
                        var atributo_id = parseInt(cmbId.substring(cmbId.lastIndexOf("_") + 1, 100));
                        if (atributo_id > 1000)
                            cmbId = cmbId.substring(0, cmbId.lastIndexOf("_")) + "_" + (atributo_id - 1000);
                    }
                    saida = saida + ";" + cmbId + ":" + lstCombos[i].options[lstCombos[i].selectedIndex].value;
                }
        }


        saida = saida.substr(1) + ";"; //  retira ponto e virgula do comeco acrescenta no final

         // ******************** manda os dados para o banco *************************
        var param = {
            obj_id: selectedId_obj_id,
            atr_id: -2,
            ati_id: -2,
            atv_valores: saida,
            nome_aba: tabela.id
        };

        param = JSON.stringify({ 'ObjAtributoValor': param, 'codigoOAE': selected_obj_codigo, 'selidTipoOAE': selectedId_obj_id, ord_id: selectedId_ord_id });
        $.ajax({
            url: "/Inspecao/InspecaoAtributoValores_Salvar",
            data: param,
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {

                preenchetblFichaRequalificacaoOAE(selectedId_obj_id, selectedId_clo_id, selectedId_tip_id);

                // alterna os campos para leitura
                FichaRequalificacaoOAE_setaReadWrite(tabela, true);

                return false;
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
                return false;
            }
        });



    }
    function SalvarDados_FichaRequalificacaoOAE_CRITERIO_DE_CLASSIFICACAO() {
        var tabela = document.getElementById("tblFichaRequalificacaoOAE_CRITERIO_DE_CLASSIFICACAO");
        SalvarDados_FichaRequalificacaoOAE(tabela);

        tabela = document.getElementById("tblFichaRequalificacaoOAE_POLITICA_ACOES_A_IMPLEMENTAR");
        SalvarDados_FichaRequalificacaoOAE(tabela);
    }
    function SalvarDados_FichaRequalificacaoOAE_NOTA_OAE_PARAMETRO_FUNCIONAL() {
        var tabela = document.getElementById("tblFichaRequalificacaoOAE_NOTA_OAE_PARAMETRO_FUNCIONAL");
        SalvarDados_FichaRequalificacaoOAE(tabela);

        tabela = document.getElementById("tblFichaRequalificacaoOAE_POLITICA_ACOES_A_IMPLEMENTAR");
        SalvarDados_FichaRequalificacaoOAE(tabela);

        //var cmb = document.getElementById("cmb_atr_id_145");
        //var valor = cmb.options[cmb.selectedIndex].value;
        //var param = {
        //    obj_id: selectedId_obj_id,
        //    atr_id: 145,
        //    ati_id: valor,
        //    atv_valores: valor
        //};

        //param = JSON.stringify({ 'ObjAtributoValor': param });

        //$.ajax({
        //    url: "/Objeto/ObjAtributoValor_Salvar",
        //    data: param,
        //    type: "POST",
        //    contentType: "application/json;charset=utf-8",
        //    dataType: "json",
        //    success: function (result) {

        //        preenchetblFichaRequalificacaoOAE(selectedId_obj_id, selectedId_clo_id, selectedId_tip_id);

        //        // alterna os campos para leitura
        //        FichaRequalificacaoOAE_setaReadWrite(tblFichaRequalificacaoOAE_NOTA_OAE_PARAMETRO_FUNCIONAL, true);

        //        return false;
        //    },
        //    error: function (errormessage) {
        //        alert(errormessage.responseText);
        //        return false;
        //    }
        //});


    }

    function CancelarDados_FichaRequalificacaoOAE(tabela) {
        FichaRequalificacaoOAE_setaReadWrite(tabela, true);

        preenchetblFichaRequalificacaoOAE(selectedId_obj_id, selectedId_clo_id, selectedId_tip_id);
    }

    // parte 3
    function FichaRequalificacaoOAE_Calcula_Notas_1_Requisito() {
        var cmb_atr_id_99 = document.getElementById("cmb_atr_id_99");
        var cmb_atr_id_100 = document.getElementById("cmb_atr_id_100");
        var cmb_atr_id_130 = document.getElementById("cmb_atr_id_130");
        var cmb_atr_id_131 = document.getElementById("cmb_atr_id_131");
        var cmb1_Nota = document.getElementById("cmb_atr_id_147");
        var cmb1_Desc = document.getElementById("cmb_atr_id_148");

        if ((cmb_atr_id_99) && (cmb_atr_id_100) && (cmb_atr_id_130) && (cmb_atr_id_131) && (cmb1_Nota) && (cmb1_Desc)) {

            var cmb_atr_id_99_val = 0;
            if (cmb_atr_id_99.selectedIndex > -1)
                cmb_atr_id_99_val = parseInt(cmb_atr_id_99.options[cmb_atr_id_99.selectedIndex].text);
            else
                cmb_atr_id_99_val = 0;

            var cmb_atr_id_100_val = 0;
            if (cmb_atr_id_100.selectedIndex > -1)
                cmb_atr_id_100_val = parseInt(cmb_atr_id_100.options[cmb_atr_id_100.selectedIndex].text);
            else
                cmb_atr_id_100_val = 0;


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

            // preenche o ultimo item do menu acordeon

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

    }
    function FichaRequalificacaoOAE_Calcula_Notas_2_Requisito() {
        // localiza os controles
        var cmb1 = document.getElementById("cmb_atr_id_84"); var cmb1_Nota = document.getElementById("cmb_atr_id_135");
        var cmb2 = document.getElementById("cmb_atr_id_85"); var cmb2_Nota = document.getElementById("cmb_atr_id_136");
        var cmb3 = document.getElementById("cmb_atr_id_91"); var cmb3_Nota = document.getElementById("cmb_atr_id_137");
        var cmb4 = document.getElementById("cmb_atr_id_87"); var cmb4_Nota = document.getElementById("cmb_atr_id_138");
        var cmb5 = document.getElementById("cmb_atr_id_88"); var cmb5_Nota = document.getElementById("cmb_atr_id_139");
        var cmb6 = document.getElementById("cmb_atr_id_89"); var cmb6_Nota = document.getElementById("cmb_atr_id_140");
        var cmb7 = document.getElementById("cmb_atr_id_93"); var cmb7_Nota = document.getElementById("cmb_atr_id_141");
        var cmb8 = document.getElementById("cmb_atr_id_20"); var cmb8_Nota = document.getElementById("cmb_atr_id_142");
        var cmb9 = document.getElementById("cmb_atr_id_92"); var cmb9_Nota = document.getElementById("cmb_atr_id_143");
        var cmb10 = document.getElementById("cmb_atr_id_94"); var cmb10_Nota = document.getElementById("cmb_atr_id_144");

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
    }
    function FichaRequalificacaoOAE_Calcula_Nota_Parametro_Funcional() {
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
    function FichaRequalificacaoOAE_Calcula_Notas_Tudo() {
        FichaRequalificacaoOAE_Calcula_Notas_1_Requisito();
        FichaRequalificacaoOAE_Calcula_Notas_2_Requisito();
        FichaRequalificacaoOAE_Calcula_Nota_Parametro_Funcional();
    }


    function FichaRequalificacaoOAE_BuscaValorVDM(iTipoPista)
    {

            $.ajax({
                url: "/Objeto/BuscaValorVDM",
                data: JSON.stringify({ 'obj_codigo_TipoOAE': selected_obj_codigo, 'itipo_pista': iTipoPista }),
                type: "POST",
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                success: function (result) {

                    // selectedId_tos_id cadastral = 7; rotineira = 8; especial = 9
                    var retorno = parseInt(result);

                    if (retorno == -1) {
                        swal({
                            type: 'error',
                            title: 'Erro',
                            text: "Erro ao sincronizar VDM"
                        }).then(
                                 function () {
                                     msgVDM = msgVDM + 1;
                                     return false;
                                 });
                    }
                    else {
                        $("#cmb_atr_id_84").val(retorno);

                        var cmb_atr_id_135 = document.getElementById("cmb_atr_id_135");
                        if ((cmb_atr_id_135) && (cmb_atr_id_1084))
                            if (cmb_atr_id_135.selectedIndex > 0)
                                cmb_atr_id_135.selectedIndex = cmb_atr_id_1084.selectedIndex;

                    }
                    return false;
                },
                error: function (errormessage) {
                    swal({
                        type: 'error',
                        title: 'Erro',
                        text: errormessage.responseText
                    }).then(
                            function () {
                                return false;
                            });

                }
            });
    }