using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using WebApp.Business;
using WebApp.Models;

namespace WebApp.Controllers
{
    /// <summary>
    /// Controlador da tela Inicial
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// Tela inicial 
        /// </summary>
        /// <returns>ActionResult View</returns>
        public ActionResult Index()
        {
            Usuario paramUsuario = (Usuario)Session["Usuario"];
            ListaMenus_Lateral(ref paramUsuario);
            //Response.Redirect("/Objeto/ObjPriorizacao/160");
            return View();
        }


        /// <summary>
        /// Montagem do Menu Lateral segundo os privilégios do Usuário logado
        /// </summary>
        /// <param name="paramUsuario">Usuario</param>
        public void ListaMenus_Lateral(ref Usuario paramUsuario)
        {
            // busca a lista de menus permitidos pelo usuario
            System.Data.DataSet ds = new UsuarioBLL().Usuario_ListMenus(paramUsuario.usu_id);

            // montagem do menu lateral
            List<MenuModel> lstMenus = new List<MenuModel>();
            if (ds != null)
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    lstMenus.Add(
                        new MenuModel()
                        {
                            men_menu_id = (int)ds.Tables[0].Rows[i]["men_menu_id"],
                            men_icone = ds.Tables[0].Rows[i]["men_icone"].ToString(),
                            men_pai_id = (int)ds.Tables[0].Rows[i]["men_pai_id"],
                            men_descricao = ds.Tables[0].Rows[i]["men_descricao"].ToString(),
                            LinkText = ds.Tables[0].Rows[i]["men_item"].ToString(),
                            ActionName = ds.Tables[0].Rows[i]["men_caminho"].ToString().Trim() == "" ? "#" : "/Home/Menu_Click?caminho=" + ds.Tables[0].Rows[i]["men_caminho"].ToString().Trim() + "&id=" + ds.Tables[0].Rows[i]["men_menu_id"].ToString().Trim(),
                            ControllerName = "Home"
                        });
                }

            paramUsuario.lstMenus = lstMenus;
        }

        /// <summary>
        /// Todos os controles do menu apontam para este metodo ("../Home/Menu_Click?caminho=") pois o evento pode vir de origens diferentes, e chama a View 
        /// </summary>
        /// <param name="caminho">caminho do Controlador/Valor extraido do valor enviado pelo click do menu</param>
        /// <param name="id">idModulo extraido do valor enviado pelo click do menu</param>
        /// <returns>ActionResult</returns>
        public ActionResult Menu_Click(string caminho, string id)
        {
            // loga o acesso da pagina
            if (System.Web.HttpContext.Current.Session["Usuario"] != null)
            {
                Usuario paramUsuario = (Usuario)Session["Usuario"];
                int retorno = new LogSistemaBLL().LogSistema_Inserir(3, // 3 = select
                                                     paramUsuario.usu_id.ToString(),
                                                     Convert.ToInt32(id), // idModulo
                                                     "",
                                                     paramUsuario.usu_ip);

                // vai para a pagina requisitada 
                return AbreMenu(caminho + "&id=" + id);
            }
            else
            {
                //loga a saida
                string usu_ip = System.Web.HttpContext.Current.Request.UserHostAddress;
                int retorno = new LogSistemaBLL().LogSistema_Inserir(12, // 12 = timeout
                                            "-1",
                                            Convert.ToInt32(id),
                                            "Session Timeout",
                                            usu_ip);

                return AbreMenu("/Login/Sair");
            }
        }

        /// <summary>
        /// Redireciona a View corrente para o novo caminho
        /// </summary>
        /// <param name="caminho">caminho do Controlador/Valor a ser redirecionado</param>
        /// <returns>ActionResult</returns>
        private ActionResult AbreMenu(string caminho)
        {
            caminho = caminho.Replace("&id=", "/");
            string[] partes = caminho.Split("/".ToCharArray());
            if (partes.Length > 3)
                return RedirectToAction(partes[2], partes[1], new { @id = partes[3] }); // RedirectToAction(ActionName,  ControllerName)
            else
                return RedirectToAction(partes[2],  partes[1]); 

        }
    }
}