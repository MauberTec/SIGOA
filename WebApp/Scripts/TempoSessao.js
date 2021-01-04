var timeout = 120 * 60 * 1000;

var timer = setInterval(
    function () {
        timeout -= 1000;
        window.status = ContadorSessao(timeout);

        if (timeout == 0) { clearInterval(timer); }
    }, 1000
);


function two(x) { return ((x > 9) ? "" : "0") + x; }

function ContadorSessao(ms) {
    var t = '';

    var sec = Math.floor(ms / 1000);
    ms = ms % 1000;

    var min = Math.floor(sec / 60);
    sec = sec % 60;
    t = two(sec);

    var hr = Math.floor(min / 60);

    min = min % 60;
    t = two(hr) + ":" + two(min) + ":" + t;


    if (hr == 0 && min == 0 && sec == 0) { MensagemSessao(); };

    if (hr == 0 && min < 2) { jQuery("#tempoSessao").css({ 'color': 'orange' }); };

    jQuery("#tempoSessao").html(t);
}

function MensagemSessao() {
    var response = POST("/Login/Logout");
    if (response.status == true) {
        swal({
            icon: 'warning',
            // title: 'Sessão Expirada',
            text: 'Sessão Expirada',
            icon: 'warning',
        }).then((result) => {
                window.location.href = "/Login";
        });

    }
}