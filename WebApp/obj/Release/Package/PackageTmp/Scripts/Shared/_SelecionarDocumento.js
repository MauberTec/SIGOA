
var selectedId = -1;
var qualModulo = '';

function checaNull(test) {
    return test == null ? '' : test;
}


function Selecionar(caminho) {
    $("#txtdoc_caminho").val(caminho.split('/').pop());
    $("#txtdoc_caminho").attr('title', caminho);
    $("#modalListarArquivos").modal('hide');

}
function ListarArquivos(url2) {
    if (url2 == null)
        url2 = "";
    $("#txtLocalizarArquivos").val("");
    $("#chk_selectAll").prop('checked', false);

    $.ajax({
        url: "/Documento/Documento_ListaArquivosWeb",
        data: JSON.stringify({ caminho: url2 }),

        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {

            // Titulo
            $("#txtTituloBrowser").text(result.titulo);

            // Diretorios
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
            });

            // Arquivos
            $('#tblArquivos').DataTable().destroy();
            $('#tblArquivos').DataTable({
                "data": result.ListaArquivos
                , "columns": [
                    {
                        data: "Target",
                        "searchable": false,
                        "sortable": false,
                        "render": function (data, type, row) {
                            var retorno = "";
                            retorno += '<input type="checkbox" name="id[]" value="' + $('<div/>').text(data).html() + '">';

                            return retorno;
                        }
                    },
                    {
                        data: "Texto", "autoWidth": true, "searchable": true, "sortable": false
                    },
                    {
                        data: "Target",
                        "searchable": false,
                        "sortable": false,
                        "render": function (data, type, row) {
                            var retorno = "";
                            var caminho = "'" + data + "'";
                            retorno += '<a href="#" onclick="window.open(' + caminho + ')"  title="Abrir" ><span class="fa fa-search"></span></a>' + '  ';

                            return retorno;
                        }
                    }
                ]
                , searching: true
                , "ordering": false
                , "lengthMenu": [[10, 25, 50, 100], [10, 25, 50, 100]]
                , select: { style: 'single' }
                , paging: false, info: false
                , "oLanguage": idioma
                , "pagingType": "input"
            });
            return false;
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
            // $("#modalAssociarOS").modal('show');
            return false;
        }
    });

    $('#modalListarArquivos').modal('show');
    return false;
}


// Handle click on "Select all" control
function chk_selectAll_click(chk) {
    // Get all rows with search applied
    var tblArq = $('#tblArquivos').DataTable();
    var rows = tblArq.rows({ 'search': 'applied' }).nodes();

    // Check/uncheck checkboxes for all rows in the table
    $('input[type="checkbox"]', rows).prop('checked', chk.checked);

};


// Handle click on checkbox to set state of "Select all" control
$('#tblArquivos tbody').on('change', 'input[type="checkbox"]', function () {
    // If checkbox is not checked
    if (!this.checked) {
        var el = $('#chk_selectAll').get(0);
        // If "Select all" control is checked and has 'indeterminate' property
        if (el && el.checked && ('indeterminate' in el)) {
            // Set visual state of "Select all" control as 'indeterminate'
            el.indeterminate = true;
        }
    }
});

    //// Handle form submission event
    //$('#frm-example').on('submit', function (e) {
    //    var form = this;

    //    // Iterate over all checkboxes in the table
    //    table.$('input[type="checkbox"]').each(function () {
    //        // If checkbox doesn't exist in DOM
    //        if (!$.contains(document, this)) {
    //            // If checkbox is checked
    //            if (this.checked) {
    //                // Create a hidden element
    //                $(form).append(
    //                    $('<input>')
    //                        .attr('type', 'hidden')
    //                        .attr('name', this.name)
    //                        .val(this.value)
    //                );
    //            }
    //        }
    //    });
    //});


function bntSelDocsOK_click() {

    // cria lista dos IDs de OSs selecionadas
    var selchks = [];
    var sel_ids = "";
    $('#divArquivos input:checked').each(function () {
        selchks.push($(this).attr('value'));
    });

    for (var i = 0; i < selchks.length; i++)
        if (i == 0)
            sel_ids = selchks[i];
        else
            sel_ids = sel_ids + ";" + selchks[i];

    sel_ids = sel_ids + ";"; // acrescenta um delimitador no final da string

    if (selchks.length > 0) {
        var url = '';
        var params = {
            doc_ids: sel_ids,
            obj_id: selectedId
        };

        if (qualModulo == 'Objetos') {
            url = "/Objeto/Objeto_AssociarDocumentos";
            params = {
                doc_ids: sel_ids,
                obj_id: selectedId
            };
        }
        else {

            url = "/Documento/Documento_AssociarOrdemServico";


        }

        $.ajax({
            url:url,
            data: JSON.stringify(params),
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {

                if (qualModulo == 'Objetos')
                    $('#tblDocumentosAssociados').DataTable().ajax.reload(null, false);  //false if you don't want to refresh paging else true.

               $("#modalListarArquivos").modal('hide');
               return false;
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
                $("#modalListarArquivos").modal('show');
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


