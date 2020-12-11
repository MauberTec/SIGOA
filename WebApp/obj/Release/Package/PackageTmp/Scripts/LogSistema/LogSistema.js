
function Limpar() {
    $('#cmbUsuarios')[0].selectedIndex = 0;;
    $('#txtdataCriacaoDe').val("");
    $('#txtdataCriacaoAte').val("");
    $('#cmbTransacoes')[0].selectedIndex = 0;;
    $('#cmbModulos')[0].selectedIndex = 0;;
    $('#txtSearchLog').val("");

    $('#tblLogSistema').DataTable().ajax.reload(null, false);  //false if you don't want to refresh paging else true.

    return false;
}
function Filtrar() {

    $('#tblLogSistema').DataTable().ajax.reload(null, false);  //false if you don't want to refresh paging else true.
    return false;

}

// montagem do gridview
$(document).ready(function () {
    // ****************************GRID LogSistema *****************************************************************************
    moment.locale('pt-br');         // pt-br
    $.fn.dataTable.moment('DD/MM/YYYY HH:mm:ss');

    var params = {
        usu_id: function () { return $('#cmbUsuarios :selected').val() == "" ? "-1" : $('#cmbUsuarios :selected').val() },
        data_inicio: function () { return $('#txtdataCriacaoDe').val() },
        data_fim: function () { return $('#txtdataCriacaoAte').val() },
        tra_id: function () { return $('#cmbTransacoes :selected').val() == "" ? "-1" : $('#cmbTransacoes :selected').val() },
        mod_id: function () { return $('#cmbModulos :selected').val() == "" ? "-1" : $('#cmbModulos :selected').val() },
        texto_procurado: function () { return $('#txtSearchLog').val().trim() }
    }

    $('#tblLogSistema').DataTable({
        "ajax": {
            "url": "/LogSistema/LogSistema_ListAll",
            data: params,
            "type": "POST",
            "datatype": "json"
        }
        , contentType: "application/json;charset=utf-8"
        , "columns": [
            { data: "log_id", "className": "hide_column" },
            { data: "tra_id", "className": "hide_column" },
            { data: "usu_id", "className": "hide_column" },
            { data: "mod_id", "className": "hide_column" },

            { data: "log_ip", "width": "5%"  },
            { data: "tra_nome"  },
            { data: "mod_nome_modulo", "width": "10%", },
            { data: "usu_usuario" },
            { data: "log_data_criacao" },

            { data: "log_texto", "width": "40%" }
        ]
        , "order": [8, "desc"]
        , 'columnDefs': [
            {
                targets: [6] // modulo
                , "createdCell": function (td, cellData, rowData, row, col) {
                    $(td).attr('title', rowData["mod_descricao"]);
                }
            },
            {
                targets: [7] // Usuário
                , "createdCell": function (td, cellData, rowData, row, col) {
                    $(td).attr('title', rowData["usu_nome"]);
                }
            },
            {
                targets: [9] // Log
                , "createdCell": function (td, cellData, rowData, row, col) {
                    $(td).attr('title', rowData["log_texto_menor"]);
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


    // ******DATEPICKERS  *****************************************************************************
    jQuery(function ($) {
        $.datepicker.regional['pt-BR'] = datepicker_regional;
        $.datepicker.setDefaults($.datepicker.regional['pt-BR']);
    });

    $('#txtdataCriacaoDe').datepicker({
        dateFormat: 'dd/mm/yy',

        onSelect: function () {
            var dt2 = $('#txtdataCriacaoAte');

            // seta a data inicial do txtdataCriacaoAte
            var startDate = $(this).datepicker('getDate');
            startDate.setDate(startDate.getDate() + 1);
            var minDate = $(this).datepicker('getDate');
            var dt2Date = dt2.datepicker('getDate');

            dt2.datepicker('option', 'minDate', minDate);
        }
    });

    $('#txtdataCriacaoAte').datepicker({
        dateFormat: 'dd/mm/yy'
    });


    $(document).bind('keydown', function (e) {
        if (e.which === 13) { // return
            $('#btnFiltrar').trigger('click');
        }
    });
});
