using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApp.Business;
using WebApp.Models;

namespace WebApp
{
    /// <summary>
    /// Faz o download do arquivo gerado
    /// </summary>
    public partial class frmDownloadFile : System.Web.UI.Page
    {
        /// <summary>
        /// Inicio da Pagina
        /// </summary>
        /// <param name="sender">Pagina</param>
        /// <param name="e">Argumentos</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            //Verifica se o usuário esta logado
            if (Session["Usuario"] == null)
            {
                int retorno = new LogSistemaBLL().LogSistema_Salvar(540);
                Response.Redirect("~/AcessoNegado/AcessoNegado");
            }

            Usuario gUsuario = (Usuario)Session["Usuario"];
            List<UsuarioPermissoes> lstPermissoes = gUsuario.lstUsuarioPermissoes;
            UsuarioPermissoes permissoesDesteModulo = lstPermissoes.Find(x => x.mod_id.Equals(540)); // id DO MODULO "O.S."
            if (permissoesDesteModulo == null)
            {
                int retorno = new LogSistemaBLL().LogSistema_Salvar(540);
                Response.Redirect("~/AcessoNegado/AcessoNegado");
            }



            string filename = Request["filename"];
            if (filename != "")
            {
                string path = Server.MapPath("~/temp/" + filename);

                if (filename.Trim().EndsWith(".dwt"))
                    path = Server.MapPath("~/reports/" + filename);

                System.IO.FileInfo file = new System.IO.FileInfo(path);
                if (file.Exists)
                {
                    Response.Clear();
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + filename); // file.Name.Replace(" ","_"));
                    Response.AddHeader("Content-Length", file.Length.ToString());
                    Response.ContentType = "application/octet-stream";
                    Response.WriteFile(file.FullName);
                    Response.End();
                }


            }
        }
    }
}
