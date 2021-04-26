
function colocaDatePickers() {
    moment.locale('pt-br');         // pt-br
    $.fn.dataTable.moment('DD/MM/YYYY HH:mm:ss');

    jQuery(function ($) {
        $.datepicker.regional['pt-BR'] = datepicker_regional;
        $.datepicker.setDefaults($.datepicker.regional['pt-BR']);
    });

    $('#txtord_data_planejada_Novo').datepicker({
        minDate: 0,
        dateFormat: 'dd/mm/yy',
        onSelect: function () {
            $('#txtord_data_planejada_Novo').css('background-color', corBranca);
        }
    });


    $('#txtord_data_inicio_programada').datepicker({ dateFormat: 'dd/mm/yy' });
    $('#txtord_data_inicio_execucao').datepicker({ dateFormat: 'dd/mm/yy' });
    $('#txtord_data_suspensao').datepicker({ dateFormat: 'dd/mm/yy' });
    $('#txtord_data_cancelamento').datepicker({ dateFormat: 'dd/mm/yy' });
    $('#txtord_data_reinicio').datepicker({ dateFormat: 'dd/mm/yy' });
    $('#txtord_data_termino_programada').datepicker({ dateFormat: 'dd/mm/yy' });
    $('#txtord_data_termino_execucao').datepicker({ dateFormat: 'dd/mm/yy' });

    $('#filtroord_data_De').datepicker({
        dateFormat: 'dd/mm/yy',
        onSelect: function () {
            var dt2 = $('#filtroord_data_Ate');

            // seta a data inicial do txtdataExecucaoAte
            var startDate = $(this).datepicker('getDate');
            startDate.setDate(startDate.getDate() + 1);
            var minDate = $(this).datepicker('getDate');
            var dt2Date = dt2.datepicker('getDate');

            dt2.datepicker("setDate", startDate);
            dt2.datepicker('option', 'minDate', startDate);
        }
    });

    $('#filtroord_data_Ate').datepicker({ dateFormat: 'dd/mm/yy' });

    // datepickers das fichas, aba DADOS GERAIS
    $('#txt_atr_id_103').datepicker({ dateFormat: 'dd/mm/yy' });
    $('#txt_atr_id_104').datepicker({ dateFormat: 'dd/mm/yy' });

    $('#txt_atr_id_1103').datepicker({ dateFormat: 'dd/mm/yy' });
    $('#txt_atr_id_1104').datepicker({ dateFormat: 'dd/mm/yy' });
}


