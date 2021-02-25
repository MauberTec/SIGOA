
    var sel_noc_id = -1;

    var controlesReadOnly = ["txt_atr_txt_IdentificacaoOAEid_102", "txt_NomeOAE", "txt_CodigoRodovia", "txt_NomeRodovia", "txt_LocalizacaoKm", "txt_Municipio", "txt_Tipo"];

    function setaReadWrite_FichaNotificacaoOcorrencia(tabela, ehRead) {

        // habilita ou desabilita todos os controles editaveis
        var lstTxtBoxes = tabela.getElementsByTagName('input');
    var lstCombos = tabela.getElementsByTagName('select');
    var lstTextareas = tabela.getElementsByTagName('textarea');

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

}

    function limpatblFicha6() {

        var tabela = document.getElementById("divFicha6");

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

    function preenchetblFicha6(obj_id, classe, tipo, ins_id) {
        classe = parseInt(classe);
    tipo = parseInt(tipo);
    selectedId_clo_id = classe;
    selectedId_tip_id = tipo;


    // limpa antes de preencher
    limpatblFicha6();

    // datepickers da ficha NOTIFICACAO OCORRENCIAS
        $('#txt_Data_Notificacao').datepicker({dateFormat: 'dd/mm/yy' });
        $('#txt_Solicitante_Data').datepicker({dateFormat: 'dd/mm/yy' });
        $('#txt_Responsavel_Recebimento_Data').datepicker({dateFormat: 'dd/mm/yy' });


    var ord_id = 0;
    if (paginaPai == "OrdemServico")
        ord_id = selectedId_ord_id;

    var url = "/NotificacaoOcorrencia/NotificacaoOcorrencia_ListAll";
        var data = {"ord_id": ord_id };

    // dados da ocorrencia
        $.ajax({
        "url": url,
    "type": "GET",
    "datatype": "json",
    "data": data,
            "success": function (result) {
                if (result.data.length > 0) {

        $('#txt_IdentificacaoOAE').val(result.data[0].IdentificacaoOAE);
    $('#txt_NomeOAE').val(result.data[0].NomeOAE);
    $('#txt_CodigoRodovia').val(result.data[0].CodigoRodovia);
    $('#txt_NomeRodovia').val(result.data[0].NomeRodovia);
    $('#txt_LocalizacaoKm').val(result.data[0].LocalizacaoKm);
    $('#txt_Municipio').val(result.data[0].Municipio);
    $('#txt_Tipo').val(result.data[0].Tipo);

    sel_noc_id = result.data[0].noc_id;
    $('#txt_Data_Notificacao').val(result.data[0].data_notificacao);
    $('#txt_Responsavel_Notificacao').val(result.data[0].responsavel_notificacao);
    $('#txt_Descricao_Ocorrencia').val(result.data[0].descricao_ocorrencia);
    $('#txt_Solicitante').val(result.data[0].solicitante);
    $('#txt_Solicitante_Data').val(result.data[0].solicitante_data);
    $('#txt_Responsavel_Recebimento').val(result.data[0].responsavel_recebimento);
    $('#txt_Responsavel_Recebimento_Data').val(result.data[0].responsavel_recebimento_data);
}
}
});



}

    function CancelarDados_FichaNotificacaoOcorrencia(tabela) {
        preenchetblFicha(selectedId_obj_id, selectedId_clo_id, selectedId_tip_id);
    // alterna os campos para leitura
    setaReadWrite_FichaNotificacaoOcorrencia(tabela, true);
    document.getElementById("btn_Salvar_NOTIFICACAO_OCORRENCIA").style.display = 'none';
    document.getElementById("btn_Cancelar_NOTIFICACAO_OCORRENCIA").style.display = 'none';
    document.getElementById("btn_Editar_NOTIFICACAO_OCORRENCIA").style.display = 'block';

}
    function SalvarDados_FichaNotificacaoOcorrencia(tabela) {

        // ********* ENVIA OS DADOS PARA O BANCO **********************************************************************
        var ord_id = -1;
    if (moduloCorrente == 'OrdemServico')
        ord_id = selectedId_ord_id;

        var notOcor = {
        noc_id: sel_noc_id,
    ord_id: ord_id,
    data_notificacao: $('#txt_Data_Notificacao').val() == null ? " " : $('#txt_Data_Notificacao').val(),
    responsavel_notificacao: $('#txt_Responsavel_Notificacao').val() == null ? " " : $('#txt_Responsavel_Notificacao').val(),
    descricao_ocorrencia: $('#txt_Descricao_Ocorrencia').val() == null ? " " : $('#txt_Descricao_Ocorrencia').val(),
    solicitante: $('#txt_Solicitante').val() == null ? " " : $('#txt_Solicitante').val(),
    solicitante_data: $('#txt_Solicitante_Data').val() == null ? " " : $('#txt_Solicitante_Data').val(),
    responsavel_recebimento: $('#txt_Responsavel_Recebimento').val() == null ? " " : $('#txt_Responsavel_Recebimento').val(),
    responsavel_recebimento_data: $('#txt_Responsavel_Recebimento_Data').val() == null ? " " : $('#txt_Responsavel_Recebimento_Data').val()
};



        $.ajax({
        url: "/NotificacaoOcorrencia/NotificacaoOcorrencia_Salvar",
    data: JSON.stringify(notOcor),
    type: "POST",
    contentType: "application/json;charset=utf-8",
    dataType: "json",
            success: function (result) {

        preenchetblFicha6(selectedId_obj_id, selectedId_clo_id, selectedId_tip_id);

    return false;
},
            error: function (errormessage) {
        alert(errormessage.responseText);
    return false;
}
});


// alterna os campos para leitura
setaReadWrite_FichaNotificacaoOcorrencia(tabela, true);

// alterna para os botoes de salvar/cancelar
document.getElementById("btn_Salvar_NOTIFICACAO_OCORRENCIA").style.display = 'none';
document.getElementById("btn_Cancelar_NOTIFICACAO_OCORRENCIA").style.display = 'none';
document.getElementById("btn_Editar_NOTIFICACAO_OCORRENCIA").style.display = 'block';

}
    function EditarDados_FichaNotificacaoOcorrencia(tabela) {
        // alterna os campos para escrita
        setaReadWrite_FichaNotificacaoOcorrencia(tabela, false);

    // alterna para os botoes de salvar/cancelar
    document.getElementById("btn_Salvar_NOTIFICACAO_OCORRENCIA").style.display = 'block';
    document.getElementById("btn_Cancelar_NOTIFICACAO_OCORRENCIA").style.display = 'block';
    document.getElementById("btn_Editar_NOTIFICACAO_OCORRENCIA").style.display = 'none';

    return false;
}

function bntEnviarEmailNotificacao_onclick()
    {
        var TextoEmail = $("#txtEmailTexto").val();
    var lstDestinatarios = ($("#cmbEmailRegionais").val() + ';').replaceAll(",", ";");

    // formata os enderecos de email
    lstDestinatarios = lstDestinatarios.replaceAll(";", ";'");
        lstDestinatarios = lstDestinatarios.replaceAll("<", "'<");
    lstDestinatarios = "'" + lstDestinatarios;
    lstDestinatarios = lstDestinatarios.slice(0, -2);


        if (lstDestinatarios == ";") {
        swal({
            position: 'top',
            type: 'error',
            text: 'Selecione um destinatário',
            title: 'Aviso'
        });
    return false;
}

        if (TextoEmail.trim() == "") {
        swal({
            position: 'top',
            type: 'error',
            text: 'O texto do Email está vazio',
            title: 'Aviso'
        });
    return false;
}
else
        {
             var reg = /<(.|\n)*?>/g;
            if (reg.test(TextoEmail) == true) {
        swal({
            position: 'top',
            type: 'error',
            text: "Uso de Tags '<>' não é permitido.",
            title: 'Aviso'
        });
    return false;
}
}

        var response = POST("/OrdemServico/FichaNotificacao_EnviarEmail", JSON.stringify({lstDestinatarios: lstDestinatarios, TextoEmail: TextoEmail }))
            if (response.status) {
        swal({
            type: 'success',
            title: 'Sucesso',
            text: 'Email(s) enviado(s) com Sucesso'
        });

    // limpa os campos 
    $("#cmbEmailRegionais").multiselect("clearSelection");
    $("#txtEmailTexto").val("");

    $("#modalEnviarEmail").modal('hide');
    return false;
}
            else {
        swal({
            type: 'error',
            title: 'Aviso',
            text: response.erroId
        });

    //$("#modalEnviarEmail").modal('hide');
    return false;
}


}

