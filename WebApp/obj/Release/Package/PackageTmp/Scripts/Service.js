function GET(url, data = {}, customHeader = {}) {
    if(Object.keys(customHeader).indexOf('dataType') == -1) customHeader.dataType = 'json';
    if(Object.keys(customHeader).indexOf('contentType') == -1) customHeader.contentType = 'application/json; charset=utf-8';

    return DoRequest('GET', url, data, customHeader);
}

function POST(url, data = {}, customHeader = {}) {
    if(Object.keys(customHeader).indexOf('dataType') == -1) customHeader.dataType = 'json';
    if(Object.keys(customHeader).indexOf('contentType') == -1) customHeader.contentType = 'application/json; charset=utf-8';

    return DoRequest('POST', url, data, customHeader); 
}

function PUT(url, data = {}, customHeader = {}) { return DoRequest('PUT', url, data, customHeader); }

function DELETE(url, data = {}, customHeader = {}) { return DoRequest('DELETE', url, data, customHeader); }

function DoRequest(type, url, data, customHeader){

    var response;

    //if (IsNull(url)) return false;

    $.ajax(
        constructHeader(type, url, data, customHeader)
    ).done((json) => {
        response = json;
    }).fail((error) => {       
        console.log(error.responseText);
        swal({
            position: 'top',
            type: 'error',
            text: 'Ocorreu um erro, entre em contato com o suporte!',
            title: 'Aviso'
        });
        //ErrorMessage('Erro na requisição, favor entrar em contato com o suporte!');
    });

    return response;
}

function constructHeader(type, url, data, customHeader) {

    var objectReturn = {
        type: type,
        url: url,
        data: data,
        async: false
    };

    if (Object.keys(customHeader).length) {

        const keyNames = Object.keys(customHeader);

        keyNames.forEach((element) => {
            objectReturn[element] = customHeader[element];
        });

    }

    return objectReturn;
}