preencheRep();

GetAll();

function Editar(id, rpt_id, valor, fonte_txt, codigo, datastring) {
    $("#modalEdit").modal('show');
    $('#rtu_id').val(id);
    $('#reparo_ad').val(rpt_id).change();
    $('#preco_ad').val(valor);
    $('#fonteTxt_ad').val(fonte_txt);
    $('#codigo_ad').val(codigo);
    $('#data_ad').val(datastring.substring(0, 10));

    Intergracao();

}
function EditarTpu() {
    swal({
        title: "Deseja realmente Editar essa TPU?",
        icon: "warning",
        buttons: [
            'Não',
            'Sim'
        ],
        dangerMode: true,
        focusCancel: true
    }).then(function (isConfirm) {
        if (isConfirm) {
            $.ajax({
                url: '/ReparoTPU/Editar',
                data: {
                    rpt_id: $('#reparo_ad').val(),
                    fon_id: $('#fonte_ad').val(),
                    rtu_fonte_txt: $('#fonteTxt_ad').val(),
                    rtu_codigo_tpu: $('#codigo_ad').val(),
                    rtu_preco_unitario: $('#preco_ad').val().replace('.',','),
                    rtu_data_base: $('#data_ad').val(),
                    rtu_id: $('#rtu_id').val()
                },
                type: "Post",
                dataType: "JSON",
                success: function (data) {
                    $("#modalEdit").modal('hide');
                    GetAll();
                },
                error: function (erro) {
                    alert(erro);
                }
            });
        }
    });

}

function closeModalReparo() {
    $("#modalEdit").modal('hide');
}

function verifica() {

    if ($('#fonte_ad').val() === "1") {
        $('#codDer').hide();
    }
    else if ($('#fonte_ad').val() === "2") {
        $('#codDer').show();
    }
    else if ($('#fonte_ad').val() === "3") {
        $('#codDer').show();
    }
    else if ($('#fonte_ad').val() === "0") {
        $('#codDer').hide();
    }
}

function preencheRep() {
    $.ajax({
        url: '/ReparoTpu/PreencheRep',
        type: "Get",
        dataType: "JSON",
        success: function (data) {
            $('#reparo_ad').empty();
            $('#reparo_ad').append($('<option ></option>').val("0").html("--Selecione--")); // 1o item vazio
            $.each(data, function (i, item) {
                $('#reparo_ad').append($('<option value=' + item.rpt_id + '> ' + item.rpt_codigo + ' - ' + item.rpt_descricao + ' </option>'));
            });
        }
    });
}


function GetAll() {

    $.ajax({
        url: '/ReparoTpu/PreencheListaTpu',
        type: "Get",
        dataType: "JSON",
        success: function (data) {
            $('#DivGrid').empty();
            $('#DivGrid').append('<table id="tblSubs" class="no-footer dataTable">' +
                '<thead>' +
                '<tr>' +
                '<th style="width:340px; text-align:center">Reparo</th>' +
                '<th style="width:70px; text-align:center">Unidade</th>' +
                '<th style="width:70px; text-align:center">Código TPU</th>' +
                '<th style="width:70px; text-align:center">Fonte</th>' +
                '<th style="width:70px; text-align:center">Código na Fonte</th>' +
                '<th style="width:70px; text-align:center">Preço Unitátio</th>' +
                '<th style="width:70px; text-align:center">Data Base</th>' +
                '<th style="width:70px; text-align:center">Opções</th>' +
                '</tr>' +
                '</thead>' +
                '<tbody id="GridHome">' +
                '</tbody>' +
                '</table >');

            $.each(data, function (i, item) {               
                var fonte = "";
                if (item.fon_id === 1) {
                    fonte = "Maubertec";
                } else if (item.fon_id === 2) {
                    fonte = "DER";
                } else if (item.fon_id === 3) {
                    fonte = "Outros";
                }
                $('#GridHome').append($('<tr><td tyle="text-align:center" title="' + item.rpt_descricao + '">' + item.rpt_descricao + '</td><td tyle="text-align:center">' + item.unidade + '</td><td tyle="text-align:center" >' + item.rtu_codigo_tpu + ' </td><td style="text-align:center">' + fonte + '</td><td tyle="text-align:center" >' + item.rtu_fonte_txt + '</td><td style="text-align:center">' + item.rtu_preco_unitario.toLocaleString("pt-BR") + '</td><td style="text-align:center">' + item.rtu_data_base + '</td><td style="text-align:center"><a id="btn_desativa_' + i + '" href="#" onclick="return Desativar(\'' + item.rtu_id + '\', 0)" title="Desativar TPU"><span class="glyphicon glyphicon-ok text-success"></span></a><a id="btn_ativa_' + i + '" href="#" onclick="return Ativar(\'' + item.rtu_id + '\', 1)" title="Ativar TPU"><span class="glyphicon glyphicon-remove text-danger"></span></a>  <a href="#" onclick="return Editar(\'' + item.rtu_id + '\', \'' + item.rpt_id + '\', \'' + item.rtu_preco_unitario + '\',\'' + item.rtu_fonte_txt + '\',\'' + item.rtu_codigo_tpu + '\',  \'' + item.datastring + '\')" title="Editar"><span class="glyphicon glyphicon-pencil"></span></a></td></tr>'));
                if (item.rtu_ativo === true) {
                    $('#btn_ativa_' + i + '').hide();
                    $('#btn_desativa_' + i + '').show();
                }
                else {
                    $('#btn_ativa_' + i + '').show();
                    $('#btn_desativa_' + i + '').hide();
                }
            });
            paginar();

        }
    });
}

function paginar() {
    $(document).ready(function () {
        $('#tblSubs').DataTable({
            "oLanguage": idioma
            , "pagingType": "input"
            , "sDom": '<"top">rt<"bottom"pfli><"clear">'
        });
    });
}



function Desativar(id,ativo) {
    swal({
        title: "Deseja realmente Desativar essa TPU?",
        icon: "warning",
        buttons: [
            'Não',
            'Sim'
        ],
        dangerMode: true,
        focusCancel: true
    }).then(function (isConfirm) {
        if (isConfirm) {
            $.ajax({
                url: '/ReparoTPU/AlterarStatus?rtu_id=' + id + '&ativo=' + ativo,
                type: "Post",
                dataType: "JSON",
                success: function (data) {
                    GetAll();
                },
                error: function (erro) {
                    alert(erro);
                }
            });
        }
    });

}

function Ativar(id, ativo) {
    swal({
        title: "Deseja realmente Ativar essa TPU?",
        icon: "warning",
        buttons: [
            'Não',
            'Sim'
        ],
        dangerMode: true,
        focusCancel: true
    }).then(function (isConfirm) {
        if (isConfirm) {
            $.ajax({
                url: '/ReparoTPU/AlterarStatus?rtu_id=' + id + '&ativo=' + ativo,
                type: "Post",
                dataType: "JSON",
                success: function (data) {
                    GetAll();
                },
                error: function (erro) {
                    alert(erro);
                }
            });
        }
    });

}

function moeda(a, e, r, t) {
    let n = ""
        , h = j = 0
        , u = tamanho2 = 0
        , l = ajd2 = ""
        , o = window.Event ? t.which : t.keyCode;
    if (13 == o || 8 == o)
        return !0;
    if (n = String.fromCharCode(o),
        -1 == "0123456789".indexOf(n))
        return !1;
    for (u = a.value.length,
        h = 0; h < u && ("0" == a.value.charAt(h) || a.value.charAt(h) == r); h++)
        ;
    for (l = ""; h < u; h++)
        -1 != "0123456789".indexOf(a.value.charAt(h)) && (l += a.value.charAt(h));
    if (l += n,
        0 == (u = l.length) && (a.value = ""),
        1 == u && (a.value = "0" + r + "0" + l),
        2 == u && (a.value = "0" + r + l),
        u > 2) {
        for (ajd2 = "",
            j = 0,
            h = u - 3; h >= 0; h--)
            3 == j && (ajd2 += e,
                j = 0),
                ajd2 += l.charAt(h),
                j++;
        for (a.value = "",
            tamanho2 = ajd2.length,
            h = tamanho2 - 1; h >= 0; h--)
            a.value += ajd2.charAt(h);
        a.value += r + l.substr(u - 2, u)
    }
    return !1
}

function Intergracao() {

    if ($('#data_ad').val().length === 10 || $('#codigo_ad').val().length > 2) {
        $.ajax({
            url: '/ReparoTpu/IntegracaoTPU?ano=' + $("#data_ad").val() + '&codItem=' + $('#codigo_ad').val(),
            type: "Get",
            dataType: "JSON",
            success: function (data) {
                if (data != 0) {
                    $('#preco_ad').val(data);
                }
            },
            error: function (erro) {
                alert(erro);
            }
        });
    }


}
