using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Models;
using WebApp.DAO;
using System.Web.Mvc;

namespace WebApp.Business
{
    /// <summary>
    /// PrecoUnitarios de Perfis e/ou de Usuários
    /// </summary>
    public class PrecoUnitarioBLL
    {
        // *************** PrecoUnitario  *************************************************************

        /// <summary>
        ///  Lista de todos os Precos Unitarios não deletados
        /// </summary>
        /// <param name="tpu_data_base_der">Filtro por Data Base</param>
        /// <param name="tpt_id">Filtro por Tipo Onerado/Desonerado</param>
        /// <param name="fas_id">Filtro por Fase</param>
        /// <returns>Lista de PrecoUnitarios</returns>
        public List<PrecoUnitario> PrecoUnitario_ListAll(string tpu_data_base_der, string tpt_id, int fas_id = -1)
        {
            return new PrecoUnitarioDAO().PrecoUnitario_ListAll(tpu_data_base_der, tpt_id, fas_id);
        }

        /// <summary>
        ///     Lista de todas as Datas de Referência Salvas
        /// </summary>
        /// <returns>Lista de SelectListItem</returns>
        public List<SelectListItem> tpu_datas_base_der_ListAll()
        {
            List<string> lstDatas = new PrecoUnitarioDAO().tpu_datas_base_der_ListAll();
            List<SelectListItem> lstSaida = new List<SelectListItem>(); // lista de combo

            int i = 0;
            foreach (var temp in lstDatas)
            {
                lstSaida.Add(new SelectListItem() { Text = temp, Value = i.ToString() });
                i++;
            }

            return lstSaida;
        }


        /// <summary>
        ///     Lista de todas as Fases Salvas
        /// </summary>
        /// <returns>Lista de SelectListItem</returns>
        public List<SelectListItem> PrecoUnitario_Fase_ListAll()
        {
            List<PrecoUnitario_Fase> lstFase = new PrecoUnitarioDAO().PrecoUnitario_Fase_ListAll();
            List<SelectListItem> lstSaida = new List<SelectListItem>(); // lista de combo

            foreach (var temp in lstFase)
            {
                string txt = temp.fas_id + " - " + temp.fas_descricao;
                lstSaida.Add(new SelectListItem() { Text = txt, Value = temp.fas_id.ToString() });
            }

            return lstSaida;
        }




    }
}