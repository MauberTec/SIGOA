
var selectedtpu_data_base_der;
var selectedtpt_id = "x";
var selectedfase_id = -1;

function LimparFiltro() {
    $('#txtLocalizarTPU').val('');
    $('#tblPrecos_Unitarios').DataTable().search('').draw();

    $('#cmbDatasReferencia').val(0);
    selectedtpu_data_base_der = $('#cmbDatasReferencia option:selected').text();
    selectedtpt_id = "x";
    selectedfase_id = -1;

    $('#cmbFase').val(null);

    document.getElementById("rd_X").checked = true;
    document.getElementById("rd_O").checked = false;
    document.getElementById("rd_D").checked = false;

    $('#tblPrecos_Unitarios').DataTable().ajax.reload();

}

function cmbDatasReferencia_onchange() {
    var valor = $('#cmbDatasReferencia').val();
    if (parseInt(valor)) {
        if (valor >= 0)
            selectedtpu_data_base_der = $('#cmbDatasReferencia option:selected').text();
    }

    $('#tblPrecos_Unitarios').DataTable().ajax.reload();

}

function cmbFase_onchange() {
    var valor = $('#cmbFase').val();

    if (parseInt(valor))
        selectedfase_id = valor;
    else
        selectedfase_id = -1;

    $('#tblPrecos_Unitarios').DataTable().ajax.reload();
}

function filtrarTipo(valor) {
    selectedtpt_id = valor;
    $('#tblPrecos_Unitarios').DataTable().ajax.reload();
}

// montagem do gridview
$(document).ready(function () {

    $('#cmbDatasReferencia').val(0);
    selectedtpu_data_base_der = $('#cmbDatasReferencia option:selected').text();

});

// ****************************GRID tblPrecos_Unitarios *****************************************************************************
$('#tblPrecos_Unitarios').DataTable({
    "ajax": {
        "url": "/PrecoUnitario/PrecoUnitario_ListAll",
        "data": function (d) {
            d.tpu_data_base_der = selectedtpu_data_base_der;
            d.tpt_id = selectedtpt_id;
            d.fas_id = selectedfase_id;
        },
        "type": "GET",
        "datatype": "json"
    }
    , "columns": [
        { data: "tpu_id", "className": "hide_column", "searchable": false },
        { data: "fas_id", "width": "30px", "className": "text-center", "searchable": true },
        { data: "tpt_id", "width": "30px", "className": "text-center", "searchable": false },
        { data: "tpu_codigo_der", "width": "150px", "className": "text-center", "searchable": true },
        { data: "tpu_descricao", "autoWidth": true, "searchable": true },
        { data: "uni_unidade", "width": "80px", "className": "text-center", "searchable": true },
        { data: "tpu_preco_unitario", "width": "130px", "className": "text-center", "searchable": true },
        {
            "title": "Opções",
            "width": "50px",
            data: "tpu_id",
            "searchable": false,
            "sortable": false,
            "render": function (data, type, row) {
                var retorno = "";
                retorno = '<span class="glyphicon glyphicon-pencil desabilitado"  ></span>' + '  ';
                retorno += '<span class="glyphicon glyphicon-ok text-success desabilitado" ></span>' + '  ';
                retorno += '<span class="glyphicon glyphicon-trash desabilitado" title="Excluir" ></span>';
                return retorno;
            }
        }
    ]
    , "rowId": "tpu_id"
    , 'columnDefs': [
        {
            targets: [1] // fase
            , "createdCell": function (td, cellData, rowData, row, col) {
                $(td).attr('title', rowData["fas_descricao"]);
            }
        }
        , {
            targets: [2] // tipo onerado/desonerado
            , "createdCell": function (td, cellData, rowData, row, col) {
                $(td).attr('title', rowData["tpt_descricao"]);
            }
        }

    ]
    , "lengthMenu": [[10, 25, 50, 100], [10, 25, 50, 100]]
    , select: {
        style: 'single'
    }
    , searching: true
    , "oLanguage": idioma
    , "pagingType": "input"
    , "sDom": '<"top">rt<"bottom"pfli><"clear">'
});

