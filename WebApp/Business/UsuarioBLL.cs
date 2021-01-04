using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Models;
using WebApp.DAO;
using System.Drawing;
using WebApp.Helpers;
using WebApp.Controllers;
using System.IO;

namespace WebApp.Business
{
    /// <summary>
    /// Dados de Usuário
    /// </summary>
    public class UsuarioBLL
    {
        // *************** LOGIN  *************************************************************
        /// <summary>
        /// Método para verificar se o usuário e senha são válidos
        /// </summary>
        /// <param name="usuario">Login do Usuário</param>
        /// <returns>Retorna os atributos do UsuarioModel</returns>
        public Usuario Usuario_ValidarLogin(Usuario usuario)
        {
            return new UsuarioDAO().Usuario_ValidarLogin(usuario);
        }


        /// <summary>
        /// Altera senha de Usuário
        /// </summary>
        /// <param name="usu_id">Id do Usuário</param>
        /// <param name="usu_senha">Senha do Usuário</param>
        /// <param name="usu_id_Atualizacao">Id do Usuário logado</param>
        /// <returns>int</returns>
        public int Usuario_AlterarSenha(int usu_id, string usu_senha, int usu_id_Atualizacao)
        {
            return new UsuarioDAO().Usuario_AlterarSenha(usu_id, usu_senha, usu_id_Atualizacao);
        }


        /// <summary>
        ///     Busca a lista de menus que o usuário logado tem direito de acessar
        /// </summary>
        /// <param name="usu_id">Id do Usuário logado</param>
        /// <returns>DataSet</returns>
        public System.Data.DataSet Usuario_ListMenus(int usu_id)
        {
            return new UsuarioDAO().Usuario_ListMenus(usu_id);
        }


        // *************** Usuario  *************************************************************

        /// <summary>
        /// Redimensiona e salva a imagem de Upload
        /// </summary>
        /// <param name="model">Variável do tipo UsuarioFoto</param>
        /// <returns>string</returns>
        public string Usuario_ImageUpload(UsuarioFoto model)
        {
            string base64String = "";
            var file = model.ImageFile;
            if ((file != null) && (model.usu_id > 0))
            {
                //nomeArquivo = model.usu_id.ToString() + "_" + file.FileName.Replace(" ", "_");
                var _image = Image.FromStream(file.InputStream);
                var _thumbImage = new Gerais().imgResize(_image, maxHeight: 160);
                // string ImagePath = new ParametroBLL().Parametro_GetValor("ImagePath");

                 base64String = new Gerais().ImageToBase64(_thumbImage);

                //_thumbImage.Save(System.Web.HttpContext.Current.Server.MapPath(ImagePath + nomeArquivo));

                //// apaga a foto anterior
                //if (!model.FotoAnterior.EndsWith("default.png"))
                //{
                //    // string ImagePath = System.Configuration.ConfigurationManager.AppSettings["ImagePath"];
                //    if (System.IO.File.Exists(System.Web.HttpContext.Current.Server.MapPath(model.FotoAnterior)))
                //    {
                //        try
                //        {
                //            System.IO.File.Delete(System.Web.HttpContext.Current.Server.MapPath(model.FotoAnterior));
                //        }
                //        catch (Exception ex) { }
                //    }
                //}


                Usuario paramUsuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
                if (paramUsuario.usu_id == model.usu_id)
                {
                    paramUsuario.usu_foto = base64String;
                  //  paramUsuario.usu_foto = ImagePath + nomeArquivo;
                    System.Web.HttpContext.Current.Session["Usuario"] = paramUsuario;
                }

                int retorno = new UsuarioBLL().Usuario_AlterarFoto(base64String, model.usu_id);

            }

            return base64String;
        }

        /// <summary>
        ///  Lista de todos os Usuários não deletados
        /// </summary>
        /// <param name="usu_id">Filtro por Id de Usuário, null para todos</param> 
        /// <returns> Lista de Usuario</returns>
        public List<Usuario> Usuario_ListAll(int? usu_id=null)
        {
            return new UsuarioDAO().Usuario_ListAll(usu_id);
        }

        /// <summary>
        /// Dados do Usuário selecionado, busca pelo ID
        /// </summary>
        /// <param name="ID">Id do Usuário selecionado</param>
        /// <returns>Usuario</returns>
        public Usuario Usuario_GetbyID(int ID)
        {
            return new UsuarioDAO().Usuario_ListAll(ID).First();
        }

        /// <summary>
        /// Exclui (logicamente) Usuário
        /// </summary>
        /// <param name="usu_id">Id do Usuário selecionado</param>
        /// <returns>int</returns>
        public int Usuario_Excluir(int usu_id)
        {
            Usuario paramUsuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            int retorno = new UsuarioDAO().Usuario_Excluir(usu_id, paramUsuario.usu_id, paramUsuario.usu_ip);

            //// apaga a foto se houver
            //if (!paramUsuario.usu_foto.EndsWith("default.png"))
            //{
            //    // string ImagePath = System.Configuration.ConfigurationManager.AppSettings["ImagePath"];
            //    string ImagePath = new ParametroBLL().Parametro_GetValor("ImagePath");
            //    if (System.IO.File.Exists(System.Web.HttpContext.Current.Server.MapPath(ImagePath + paramUsuario.usu_foto)))
            //    {
            //        try
            //        {
            //            System.IO.File.Delete(System.Web.HttpContext.Current.Server.MapPath(ImagePath + paramUsuario.usu_foto));
            //        }
            //        catch (Exception ex) { }
            //    }
            //}


            return retorno;
        }

        /// <summary>
        /// Ativa/Desativa Usuário selecionado
        /// </summary>
        /// <param name="usu_id">Id do Usuário selecionado</param>
        /// <returns>int</returns>
        public int Usuario_AtivarDesativar(int usu_id)
        {
            Usuario paramUsuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            return new UsuarioDAO().Usuario_AtivarDesativar(usu_id, paramUsuario.usu_id, paramUsuario.usu_ip);
        }

        /// <summary>
        /// Incluir ou Alterar Usuário
        /// </summary>
        /// <param name="usu">Variável do tipo USUARIO, com os novos atributos</param>
        /// <returns>int</returns>
        public string Usuario_Salvar(Usuario usu)
        {
            Usuario paramUsuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            int retorno = new UsuarioDAO().Usuario_Salvar(usu, paramUsuario.usu_id, paramUsuario.usu_ip);

            string saida = retorno > 0 ? "" : "Erro ao salvar usuário";

            // se for insercao entao envia a senha por email
            if ((retorno > 0) && (usu.usu_id == 0))
            {
                string saida2 = new LoginBLL().CriaEnviaEmailComSenha(usu.usu_usuario).Trim();
                saida = saida + saida2;

            }

            return saida.Trim();
        }


        /// <summary>
        /// Altera foto de Usuário
        /// </summary>
        /// <param name="usu_foto">nome do arquivo da foto</param>
        /// <param name="usu_id_Atualizacao">Id do Usuário logado</param>
        /// <returns>int</returns>
        public int Usuario_AlterarFoto(string usu_foto, int usu_id_Atualizacao)
        {
            return new UsuarioDAO().Usuario_AlterarFoto(usu_foto, usu_id_Atualizacao);
        }

        /// <summary>
        /// Lista de permissões Módulo: Leitura, Escrita, Exclusão, Inclusão do Usuário logado
        /// </summary>
        /// <param name="usu_id">Id do Usuário logado</param>
        /// <returns>Lista de UsuarioPermissoes</returns>
        public List<UsuarioPermissoes> Usuario_ListPermissoes(int usu_id)
        {
            return new UsuarioDAO().Usuario_ListPermissoes(usu_id);
        }


        // *************** PERFIS do Usuario  *************************************************************

        /// <summary>
        /// Lista de Perfis do Usuário selecionado
        /// </summary>
        /// <param name="usu_id">Id do Usuário selecionado</param>
        /// <returns>Lista de UsuarioPerfil</returns>
        public List<UsuarioPerfil> Usuario_ListAllPerfis(int usu_id)
        {
            return new UsuarioDAO().Usuario_ListAllPerfis(usu_id);
        }

        /// <summary>
        /// Ativa/Desativa Perfil do Usuário selecionado
        /// </summary>
        /// <param name="usu_id">Id do Usuário selecionado</param>
        /// <param name="per_id">Id do Perfil selecionado</param>
        /// <returns>int</returns>
        public int Usuario_AtivarDesativarPerfil(int usu_id, int per_id)
        {
            Usuario paramUsuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            return new UsuarioDAO().Usuario_AtivarDesativarPerfil(usu_id, per_id, paramUsuario.usu_id, paramUsuario.usu_ip);
        }



        // *************** Grupos do Usuario  *************************************************************

        /// <summary>
        /// Lista de Grupos do Usuário selecionado
        /// </summary>
        /// <param name="usu_id">Id do Usuário selecionado</param>
        /// <returns>Lista de UsuarioGrupo</returns>
        public List<UsuarioGrupo> Usuario_ListAllGrupos(int usu_id)
        {
            return new UsuarioDAO().Usuario_ListAllGrupos(usu_id);
        }

        /// <summary>
        /// Ativa/Desativa Grupo do Usuário selecionado
        /// </summary>
        /// <param name="usu_id">Id do Usuário selecionado</param>
        /// <param name="gru_id">Id do Grupo selecionado</param>
        /// <returns>int</returns>
        public int Usuario_AtivarDesativarGrupo(int usu_id, int gru_id)
        {
            Usuario paramUsuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            return new UsuarioDAO().Usuario_AtivarDesativarGrupo(usu_id, gru_id, paramUsuario.usu_id, paramUsuario.usu_ip);
        }




    }
}