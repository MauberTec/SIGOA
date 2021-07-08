

function atualizaNavBar(item, menuPai, tooltip) {
    var h1Titulo = document.getElementById("h1Titulo");
    var subMenu1 = document.getElementById("subMenu1");
    var subMenu2 = document.getElementById("subMenu2");

    var liSubMenu1 = document.getElementById("liSubMenu1");
    var liSubMenu2 = document.getElementById("liSubMenu2");

    if (item.includes("Tipo") || item.includes("Causa") || item.includes("Status"))
        h1Titulo.innerText = "::.. " +  tooltip + " ..::";
    else
        h1Titulo.innerText = "::.. " + item + " ..::";

    subMenu1.innerText = "";
    subMenu2.innerText = "";
    liSubMenu1.style.display = 'none';
    liSubMenu2.style.display = 'none';

    if ((menuPai != null) || (menuPai < 0)) {
        liSubMenu1.style.display = '';
        liSubMenu2.style.display = '';

        subMenu1.innerText = menuPai;
        subMenu2.innerText = item;
    }
    else
        if (item.trim() != "Home") {
            liSubMenu1.style.display = '';
            liSubMenu2.style.display = '';

            subMenu1.innerText = item;
            subMenu2.innerText = "";
        }

    return false;
}

//***********************************************************************************************
// rotina para localizar e deixar aberto o menu pois a cada click ele fecha

var url = window.location.href;
//url = url.replace(window.location.origin, "") + "&";
url = url.replace(window.location.origin, "") ;

if (url.endsWith("#"))
    url = url.substring(0, url.length - 1);


var menu = document.getElementById("menu");
var submenus = menu.getElementsByTagName("a");

for (var i = 0; i < submenus.length; i++) {
    var subItem_A = submenus[i];
    var subItem_A_href = subItem_A.href.replace("/Home/Menu_Click?caminho=", "").replace(window.location.origin, "") + " ";
    var abriu = false;

    if (subItem_A_href != undefined) {
        //if ((subItem_A_href.startsWith(url)) || (subItem_A.href.replace(window.location.origin, "") == "/Home/Index")) // procura o subitem cujo href == url
        subItem_A_href = subItem_A_href.trim().replace("&id=", "/");
        if ((subItem_A_href == url) || (subItem_A.href.replace(window.location.origin, "") == "/Home/Index")) // procura o subitem cujo href == url
        {
            subItem_A.style.color = "#FFFFFF";  // ITEM

            var menuPai_txt = subItem_A.getAttribute('alt');
            if (menuPai_txt != "") // significa que tem pai
            {
                // procura item pai
                for (var j = 0; j < submenus.length; j++) {
                    if (submenus[j].innerText.trim() == menuPai_txt)  {
                        var menuPai_A = submenus[j];

                        // procura o elemento li
                        var menuPai_li = menuPai_A.parentElement;

                        // abre o menu
                        if (!abriu) {
                            menuPai_li.className = "treeview menu-open";
                            abriu = true;
                            var menu_ul = menuPai_li.getElementsByTagName("ul")[0];
                            if (menu_ul)
                                menu_ul.style.display = 'block';

                            var iconSubMenu1 = document.getElementById("iconSubMenu1");  //  icone do menu crumb
                            var menu_ico = menuPai_li.getElementsByTagName("i")[0];
                            if (menu_ico)
                                iconSubMenu1.className = menu_ico.className;
                        }

                    }
                }
            }
            atualizaNavBar(subItem_A.innerText, menuPai_txt, subItem_A.title);
        }
    }
}

//***********************************************************************************************

$(document).ready(function () {
    $("#imageBrowse1").change(function () {
        var File = this.files;
        if (File && File[0]) {
            var fotoAnterior = $('#imgUsu_foto2').attr("src");
            Uploadimage(usu_id, "imageBrowse1", fotoAnterior);
        }
    });

    $("#btnBrowse1").click(function (e) {
        // parar o click
        e.stopPropagation();

        // abre o file browser
        document.getElementById('imageBrowse1').click();

        // mantem o menu aberto
        var dropdownMenu = $("#liDropDown");
        if (!dropdownMenu.hasClass('open')) {
            $('.dropdown-toggle').addClass('open');
        }

    }
    );

    $("#btnBrowse2").click(function (e) {
        // parar o click
        e.stopPropagation();

        // abre o file browser
        document.getElementById('imageBrowse2').click();

        // mantem o menu aberto
        var dropdownMenu = $("#liDropDown");
        if (!dropdownMenu.hasClass('open')) {
            $('.dropdown-toggle').addClass('open');
        }
    }
    );
})


