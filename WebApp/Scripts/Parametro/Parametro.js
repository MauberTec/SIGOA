
var selectedId;

function Parametro_Salvar() {
    var txtpar_descricao = document.getElementById('txtpar_descricao');
    var txtpar_valor = document.getElementById('txtpar_valor');
    txtpar_valor.value = txtpar_valor.value.trim();

    if ((txtpar_descricao.value == null) || (txtpar_descricao.value == ''))
        txtpar_descricao.value = "";

//if (checaVazio(txtpar_descricao) && validaTxt(txtpar_valor)) {
    if (checaVazio(txtpar_descricao)) {
        var parametro = {
            par_id: $('#txtpar_id').val(),
            par_valor: $('#txtpar_valor').val(),
            par_descricao: $('#txtpar_descricao').val()
        };

        $.ajax({
            url: "/Parametro/Parametro_Salvar",
            data: JSON.stringify(parametro),
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                $('#modalSalvarRegistro').modal('hide');
                $('#tblParametros').DataTable().ajax.reload(null, false);
                return false;
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
                return false;
            }
        });


    }

    return false;
}

function Parametro_Editar(id) {
    $('#txtpar_valor').css('border-color', 'lightgrey');
    $('#txtpar_descricao').css('border-color', 'lightgrey');

    $.ajax({
        url: "/Parametro/Parametro_getbyID/" + id,
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            $('#txtpar_id').val(result.par_id);
            $('#txtpar_valor').val(result.par_valor);
            $('#txtpar_descricao').val(result.par_descricao);

            // coloca mascara de senha
            var x = document.getElementById("txtpar_valor");
          //  if (result.par_id.indexOf("Senha") > 0) // == "email_Senha") 
            if  ((result.par_id.indexOf("Senha") > 0) && (result.par_id != "email_txtEsqueciSenha"))
                x.type = "password";
            else
                x.type = "text";

            $("#modalSalvarRegistro").modal('show');
            $('#btnUpdate').show();
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
    return false;
}

// montagem do gridview
$(document).ready(function () {
    $('#tblParametros').DataTable({
        "ajax": {
            "url": "/Parametro/Parametro_ListAll",
            "type": "GET",
            "datatype": "json"
        }
        , "columns": [
            { data: "par_id", "width": "100px" },
            {
                data: "par_valor", "autoWidth": true,
                "render": function (data, type, row) {
                  //  if (row["par_id"] == "email_Senha")
                    if ((row["par_id"].indexOf("Senha") > 0) && (row["par_id"] != "email_txtEsqueciSenha"))
                        return retorno = '***************';
                    else
                        return row["par_valor"];
                }
            },
            { data: "par_descricao", "autoWidth": true },
            {
                "title": "Opções",
                data: "par_id",
                "searchable": false,
                "sortable": false,
                "render": function (data, type, row) {
                    var dado = "'" + data + "'";

                    var retorno = "";

                    if (permissaoEscrita > 0) {
                        retorno = '<a href="#" onclick="return Parametro_Editar(xxxxx)" ><span class="glyphicon glyphicon-pencil"></span></a>' + '  ';
                    }
                    else {
                        retorno = '<span class="glyphicon glyphicon-pencil desabilitado" ></span>' + '  ';
                    }
                    retorno += '<span class="glyphicon glyphicon-ok text-success desabilitado"  ></span>  ';
                    retorno += '<span class="glyphicon glyphicon-trash desabilitado"  ></span>';

                    retorno = retorno.replace("xxxxx", dado);
                    return retorno;
                }
            }
        ]
        , "rowId": "par_id"
        , "rowCallback": function (row, data) {
            if (data.par_id == selectedId)
                $(row).addClass('selected');
        }
        , "lengthMenu": [[10, 25, 50, 100], [10, 25, 50, 100]]
        , select: {
            style: 'single'
        }

        , searching: false
        , "oLanguage": idioma
        , "pagingType": "input"
        , "sDom": '<"top">rt<"bottom"pfli><"clear">'
    });

    var tblParametros = $('#tblParametros').DataTable();
    $('#tblParametros tbody').on('click', 'tr', function () {
        if ($(this).hasClass('selected')) {
            $(this).removeClass('selected');
        }
        else {
            tblParametros.$('tr.selected').removeClass('selected');
            $(this).addClass('selected');
        }

        var params = tblParametros.row(this).data();
        selectedId = params["par_id"];
    });
});
