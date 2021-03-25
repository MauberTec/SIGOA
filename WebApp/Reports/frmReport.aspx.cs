using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using WebApp.Business;
using WebApp.DAO;
using WebApp.Helpers;
using WebApp.Models;

namespace WebApp
{
    /// <summary>
    /// Carrega o Relatorio solicitado
    /// </summary>
    public partial class frmReport : System.Web.UI.Page
    {
        /// <summary>
        /// Inicio
        /// </summary>
        /// <param name="sender">Pagina</param>
        /// <param name="e">Argumentos</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
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


                // abre o relatorio
                string relatorio = Request["relatorio"];
                System.Data.DataSet ds = new System.Data.DataSet();
                ReportDataSource rds1 = new ReportDataSource();

                if (relatorio.StartsWith("rptRelatorio"))
                {
                    // cria lista de Regionais
                    string strRegionais = new Gerais().str_Regionais();

                    switch (relatorio)
                    {
                        case "rptRelatorio_OS":
                            ds = new RelatoriosDAO().OSs_Ds(Request["FiltroRodovias"],
                                                                    Request["FiltroRegionais"],
                                                                    Request["FiltroTiposOS"],
                                                                    Request["FiltroStatusOS"],
                                                                    Request["Filtro_tipo_data"],
                                                                    Request["Filtro_data_De"],
                                                                    Request["Filtro_data_Ate"],
                                                                    strRegionais);
                            ReportViewer1.LocalReport.DataSources.Clear();
                            rds1 = new ReportDataSource("dtOS", ds.Tables[0]);
                            ReportViewer1.LocalReport.DataSources.Add(rds1);
                            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rptRelatorio_OS.rdlc");
                            break;

                        case "rptRelatorio_PerformanceOAEs":
                            ds = new RelatoriosDAO().PerformanceOAEs_Ds(Request["FiltroRodovias"],
                                                                    Request["FiltroRegionais"],
                                                                    Request["FiltroObj_codigo"],
                                                                    Request["Filtro_data_De"],
                                                                    Request["Filtro_data_Ate"],
                                                                    strRegionais);

                            ReportViewer1.LocalReport.DataSources.Clear();
                            rds1 = new ReportDataSource("dtOAEs", ds.Tables[0]);
                            ReportViewer1.LocalReport.DataSources.Add(rds1);
                            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rptRelatorio_PerformanceOAEs.rdlc");
                            break;

                        case "rptRelatorio_Priorizacao":
                            ds = new RelatoriosDAO().ObjPriorizacao_Ds("", Request["FiltroRodovias"], Request["FiltroRegionais"], "", "", "", 0, strRegionais);

                            ReportViewer1.LocalReport.DataSources.Clear();
                            rds1 = new ReportDataSource("dtPriorizacao", ds.Tables[0]);
                            ReportViewer1.LocalReport.DataSources.Add(rds1);
                            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rptRelatorio_Priorizacao.rdlc");
                            break;

                        case "rptRelatorio_Acoes":
                            ds = new RelatoriosDAO().Objetos_Relatorio_Acoes_Ds("",
                                                                    Request["FiltroRodovias"],
                                                                    Request["FiltroRegionais"],
                                                                    Request["FiltroObj_codigo"],
                                                                    Request["Filtro_data_De"],
                                                                    Request["Filtro_data_Ate"],
                                                                    0,
                                                                    strRegionais);

                            // ***** manda para o relatorio *************************************************
                            ReportViewer1.LocalReport.DataSources.Clear();
                            rds1 = new ReportDataSource("dtAcoes", ds.Tables[0]);
                            ReportViewer1.LocalReport.DataSources.Add(rds1);
                            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rptRelatorio_Acoes.rdlc");
                            break;
                    }


                }
                else
                {
                    string sObj_id = Request["id"];
                    int obj_id = 0;
                    if (Int32.Parse(sObj_id) > 0)
                        obj_id = Convert.ToInt32(sObj_id);

                    string sord_id = Request["ord_id"];
                    int ord_id = -10;

                    if ((sord_id != null) && (sord_id.Trim() != ""))
                        if (Int32.Parse(sord_id) > 0)
                            ord_id = Convert.ToInt32(sord_id);


                    if (obj_id > 0)
                    {
                      //  System.Data.DataSet ds = new System.Data.DataSet();
                        if ((relatorio.Trim() != "rptFichaNotificacaoOcorrencia") && (relatorio.Trim() != "rptFichaInspecaoEspecial_Providencias"))
                        {
                            ds = new RelatoriosDAO().FICHA_INSPECAO_CADASTRAL(obj_id, ord_id);
                        }
                        ReportViewer1.LocalReport.DataSources.Clear();

                        if (relatorio.Trim() == "rptFichaInspecaoCadastral_1")
                        {
                            ReportDataSource rds = new ReportDataSource("dtFicha", ds.Tables[0]);
                            ReportViewer1.LocalReport.DataSources.Add(rds);
                            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rptFichaInspecaoCadastral_1.rdlc");
                        }
                        else
                        if (relatorio.Trim() == "rptFichaInspecaoRotineira")
                        {
                            string tos_id = Request["tos_id"];  // 1 = cadastral; 2 = rotineira

                            ReportDataSource rds = new ReportDataSource("dtFicha2", ds.Tables[0]);
                            ReportViewer1.LocalReport.DataSources.Add(rds);
                            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rptFichaInspecaoRotineira.rdlc");
                            ReportViewer1.LocalReport.SubreportProcessing += new Microsoft.Reporting.WebForms.SubreportProcessingEventHandler(LocalReport_SubreportProcessing);

                            List<ReportParameter> listReportParameter = new List<ReportParameter>();
                            listReportParameter.Add(new ReportParameter("tos_id", tos_id));
                            ReportViewer1.LocalReport.SetParameters(listReportParameter);
                        }
                        if (relatorio.Trim() == "rptFichaInspecaoRotineira_Providencias")
                        {
                            string tos_id = Request["tos_id"];  // 1 = cadastral; 2 = rotineira

                            ds = new RelatoriosDAO().GruposVariaveisValores_ListAll(-1, ord_id, 1, Convert.ToInt32(Request["prt_id"]));

                            ReportDataSource rds = new ReportDataSource("dtRotineiraProvidencias", ds.Tables[0]);
                            ReportViewer1.LocalReport.DataSources.Add(rds);
                            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rptFichaInspecaoRotineira_Providencias.rdlc");

                            List<ReportParameter> listReportParameter = new List<ReportParameter>();
                            listReportParameter.Add(new ReportParameter("tos_id", tos_id));
                            ReportViewer1.LocalReport.SetParameters(listReportParameter);
                        }
                        else
                        if (relatorio.Trim() == "rptFichaInspecaoEspecial")
                        {
                            ReportDataSource rds = new ReportDataSource("dtFicha", ds.Tables[0]);
                            ReportViewer1.LocalReport.DataSources.Add(rds);
                            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rptFichaInspecaoEspecial.rdlc");

                            //string imgBase64 = ds.Tables[0].Rows[0]["txt_atr_id_159"].ToString().Trim();

                            //// se nao houver imagem, coloca uma imagem branca
                            //if (imgBase64 == "")
                            //    imgBase64 = "data:image/jpeg;base64,/9j/4AAQSkZJRgABAQECWAJYAAD/2wBDAAUDBAQEAwUEBAQFBQUGBwwIBwcHBw8LCwkMEQ8SEhEPERETFhwXExQaFRERGCEYGh0dHx8fExciJCIeJBweHx7/2wBDAQUFBQcGBw4ICA4eFBEUHh4eHh4eHh4eHh4eHh4eHh4eHh4eHh4eHh4eHh4eHh4eHh4eHh4eHh4eHh4eHh4eHh7/wAARCAAZABkDASIAAhEBAxEB/8QAHwAAAQUBAQEBAQEAAAAAAAAAAAECAwQFBgcICQoL/8QAtRAAAgEDAwIEAwUFBAQAAAF9AQIDAAQRBRIhMUEGE1FhByJxFDKBkaEII0KxwRVS0fAkM2JyggkKFhcYGRolJicoKSo0NTY3ODk6Q0RFRkdISUpTVFVWV1hZWmNkZWZnaGlqc3R1dnd4eXqDhIWGh4iJipKTlJWWl5iZmqKjpKWmp6ipqrKztLW2t7i5usLDxMXGx8jJytLT1NXW19jZ2uHi4+Tl5ufo6erx8vP09fb3+Pn6/8QAHwEAAwEBAQEBAQEBAQAAAAAAAAECAwQFBgcICQoL/8QAtREAAgECBAQDBAcFBAQAAQJ3AAECAxEEBSExBhJBUQdhcRMiMoEIFEKRobHBCSMzUvAVYnLRChYkNOEl8RcYGRomJygpKjU2Nzg5OkNERUZHSElKU1RVVldYWVpjZGVmZ2hpanN0dXZ3eHl6goOEhYaHiImKkpOUlZaXmJmaoqOkpaanqKmqsrO0tba3uLm6wsPExcbHyMnK0tPU1dbX2Nna4uPk5ebn6Onq8vP09fb3+Pn6/9oADAMBAAIRAxEAPwD7LooooAKKKKACiiigAooooA//2Q==";

                            //ReportParameter par_img_esquema_estrutural = new ReportParameter("par_img_esquema_estrutural", (imgBase64.Replace("data:image/jpg;base64,", "").Replace("data:image/png;base64,", "")));
                            //ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { par_img_esquema_estrutural });
                        }
                        else
                            if (relatorio.Trim() == "rptFichaInspecaoEspecial_campo")
                        {
                            ReportDataSource rds = new ReportDataSource("dtFicha2", ds.Tables[0]);
                            ReportViewer1.LocalReport.DataSources.Add(rds);
                            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rptFichaInspecaoEspecial_campo.rdlc");
                        }
                        else
                            if (relatorio.Trim() == "rptFichaNotificacaoOcorrencia")
                        {
                            System.Data.DataTable dtt = new RelatoriosDAO().FICHA_NotificacaoOcorrencia(ord_id);
                            ReportDataSource rds = new ReportDataSource("DataSet1", dtt);
                            ReportViewer1.LocalReport.DataSources.Add(rds);
                            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rptFichaNotificacaoOcorrencia.rdlc");
                        }
                        else
                            if (relatorio.Trim() == "rptFichaInspecaoEspecial_Providencias")
                        {
                            string sapt_id = Request["apt_id"];
                            int apt_id = -1;
                            if ((sapt_id != null) && (sapt_id.Trim() != ""))
                                if (Int32.Parse(sapt_id) > 0)
                                    apt_id = Convert.ToInt32(sapt_id);


                            System.Data.DataTable dtt = new RelatoriosDAO().FICHA_ESPECIAL_PROVIDENCIAS(ord_id, apt_id).Tables[0];

                            ReportDataSource rds = new ReportDataSource("dtProvidencias", dtt);
                            ReportViewer1.LocalReport.DataSources.Clear();
                            ReportViewer1.LocalReport.DataSources.Add(rds);
                            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rptFichaInspecaoEspecial_Providencias.rdlc");
                        }

                    }
                }

                ReportViewer1.ShowPrintButton = true;
                ReportViewer1.LocalReport.Refresh();

            }
        }

        /// <summary>
        /// Processa o sub relatório
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void LocalReport_SubreportProcessing(object sender, SubreportProcessingEventArgs e)
        {
            string sObj_id = Request["id"];
            int obj_id = 0;

            if (Int32.Parse(sObj_id) > 0)
                obj_id=  Convert.ToInt32(sObj_id);

            string sord_id = Request["ord_id"];
            int ord_id = 0;

            if (Int32.Parse(sord_id) > 0)
                ord_id = Convert.ToInt32(sord_id);


            e.DataSources.Clear();

            System.Data.DataSet ds_sub = new RelatoriosDAO().GruposVariaveisValores_ListAll(obj_id, ord_id, 0);
            ReportDataSource rds_sub = new ReportDataSource("dsFicha2_sub", ds_sub.Tables[0]);

            e.DataSources.Add(rds_sub);

        }

     }
}