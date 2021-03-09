using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using WebApp.Business;
using WebApp.DAO;
using WebApp.Models;

namespace WebApp.Helpers
{
    /// <summary>
    /// Métodos e Funções de uso geral
    /// </summary>
    public class Gerais
    {
        /// <summary>
        /// Criptografar
        /// </summary>
        /// <param name="texto">String a ser criptografada</param>
        /// <param name="key">Chave de criptografia</param>
        /// <returns>String criptografada</returns>
        public  string Encrypt(string texto, string key = "G4WedT")
        {
            try
            {
                if (string.IsNullOrEmpty(texto))
                    return null;
                else
                {
                    byte[] keyArray;
                    byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(texto);


                    //System.Windows.Forms.MessageBox.Show(key);
                    //If hashing use get hashcode regards to your key
                    var hashmd5 = new MD5CryptoServiceProvider();
                    keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));

                    //Always release the resources and flush data
                    // of the Cryptographic service provide. Best Practice
                    hashmd5.Clear();

                    var tdes = new TripleDESCryptoServiceProvider();

                    //set the secret key for the tripleDES algorithm
                    tdes.Key = keyArray;

                    //mode of operation. there are other 4 modes.
                    //We choose ECB(Electronic code Book)
                    tdes.Mode = CipherMode.ECB;

                    //padding mode(if any extra byte added)
                    tdes.Padding = PaddingMode.PKCS7;

                    var cTransform = tdes.CreateEncryptor();

                    //transform the specified region of bytes array to resultArray
                    byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

                    //Release resources held by TripleDes Encryptor
                    tdes.Clear();

                    //Return the encrypted data into unreadable string format
                    return Convert.ToBase64String(resultArray, 0, resultArray.Length);
                }
            }
            catch (System.Exception)
            {
                throw new System.Exception("Ocorreu um erro ao encriptografar. Entre em contato com o suporte.");
            }
        }

        /// <summary>
        /// Descriptografar
        /// </summary>
        /// <param name="texto">String a ser descriptografada</param>
        /// <param name="key">Chave de criptografia</param>
        /// <returns>String Descriptografada</returns>
        public string Decrypt(string texto, string key = "DpT3rc")
        {
            try
            {
                if (string.IsNullOrEmpty(texto))
                    return null;
                else
                {
                    byte[] keyArray;

                    //get the byte code of the string
                    byte[] toEncryptArray = Convert.FromBase64String(texto);

                    //if hashing was used get the hash code with regards to your key
                    var hashmd5 = new MD5CryptoServiceProvider();
                    keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));

                    //release any resource held by the MD5CryptoServiceProvider
                    hashmd5.Clear();

                    var tdes = new TripleDESCryptoServiceProvider();

                    //set the secret key for the tripleDES algorithm
                    tdes.Key = keyArray;

                    //mode of operation. there are other 4 modes. 
                    //We choose ECB(Electronic code Book)
                    tdes.Mode = CipherMode.ECB;

                    //padding mode(if any extra byte added)
                    tdes.Padding = PaddingMode.PKCS7;

                    ICryptoTransform cTransform = tdes.CreateDecryptor();
                    byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

                    //Release resources held by TripleDes Encryptor                
                    tdes.Clear();

                    //return the Clear decrypted TEXT
                    return UTF8Encoding.UTF8.GetString(resultArray);
                }
            }
            catch (System.Exception ex)
            {
                throw new System.Exception("Ocorreu um erro ao descriptografar. Entre em contato com o suporte.");
            }
        }


        /// <summary>
        /// Redimensiona imagem
        /// </summary>
        /// <param name="image">Imagem (tipo Image)</param>
        /// <param name="maxWidth">Largura máxima em pixels</param>
        /// <param name="maxHeight">Altura máxima em pixels</param>
        /// <returns>Imagem (tipo Image)</returns>
        public Image imgResize(Image image, int maxWidth = 0, int maxHeight = 0)
        {
            if (maxWidth == 0)
                maxWidth = image.Width;
            if (maxHeight == 0)
                maxHeight = image.Height;

            var ratioX = (double)maxWidth / image.Width;
            var ratioY = (double)maxHeight / image.Height;
            var ratio = Math.Min(ratioX, ratioY);

            var newWidth = (int)(image.Width * ratio);
            var newHeight = (int)(image.Height * ratio);

            var newImage = new Bitmap(newWidth, newHeight);
            Graphics.FromImage(newImage).DrawImage(image, 0, 0, newWidth, newHeight);
            return newImage;
        }

        /// <summary>
        /// Envia email
        /// </summary>
        /// <param name="av1">Texto do email em formato AlternateView</param>
        /// <param name="pEmail">Parâmetros do provedor no formato ParamsEmail</param>
        /// <returns>String vazia se OK ou mensagem de erro</returns>
        public string MandaEmail(AlternateView av1, ParamsEmail pEmail)
        {
            try
            {
                int email_Enviar_Emails = Convert.ToInt16(new ParametroDAO().Parametro_GetValor("email_Enviar_Emails"));
                if (email_Enviar_Emails == 0)
                    return "Email não enviado. Parâmetro desligado.";


                System.Net.Mail.MailMessage oEmail = new System.Net.Mail.MailMessage(pEmail.De, pEmail.Para, pEmail.Assunto, pEmail.Texto);
                System.Net.Mail.SmtpClient oSmtp = new System.Net.Mail.SmtpClient();


                if (pEmail.Anexo.Trim() != String.Empty)
                    oEmail.Attachments.Add(new System.Net.Mail.Attachment(pEmail.Anexo));

                // CC e CCo
                if (pEmail.CC.ToString().Trim() != "")
                {
                    string[] sCC = pEmail.CC.Split(";".ToCharArray());
                    oEmail.CC.Clear();
                    for (int ii = 0; ii < sCC.Length; ii++)
                    {
                        MailAddress copy = new MailAddress(sCC[ii]);
                        oEmail.CC.Add(copy);
                    }
                }

                if (pEmail.CCO.ToString().Trim() != "")
                {
                    string[] sCCO = pEmail.CCO.Split(";".ToCharArray());
                    oEmail.Bcc.Clear();
                    for (int ii = 0; ii < sCCO.Length; ii++)
                    {
                        MailAddress copy = new MailAddress(sCCO[ii]);
                        oEmail.Bcc.Add(copy);
                    }
                }


                if ((av1 != null))
                {
                    oEmail.AlternateViews.Add(av1);
                    oEmail.IsBodyHtml = true;
                }
                else
                {
                    oEmail.AlternateViews.Clear();
                    oEmail.IsBodyHtml = false;
                }

                oEmail.Priority = MailPriority.Normal;
                oEmail.Body = pEmail.Texto;


                if (pEmail.PortaSmtp != -1)
                    oSmtp.Port = pEmail.PortaSmtp;

                oSmtp.Host = pEmail.SMTPServer;
                oSmtp.Credentials = new System.Net.NetworkCredential(pEmail.Usuario, pEmail.Senha);
                oSmtp.Send(oEmail);

                return "";
            }
            catch (Exception Ex)
            {
                return Ex.Message;
            }

        }


        /// <summary>
        /// Busca e cria lista de arquivos e diretorios de uma Pasta Virtual de arquivos
        /// </summary>
        /// <param name="caminhoCompletoURL">Endereço URL dos arquivos de documentos - Pasta Compartilhada IIS</param>
        /// <returns>Lista do tipo ArquivoWeb</returns>
        public List<ArquivoWeb> Documento_ListaArquivosWeb(string caminhoCompletoURL)
        {
            string html = "";
            string servidor = ""; 

            if (!caminhoCompletoURL.StartsWith("http"))
            {
                string uri = HttpContext.Current.Request.Url.AbsoluteUri;
                servidor = uri.Substring(0, uri.IndexOf("/",8));

                if (caminhoCompletoURL.StartsWith("/"))
                    caminhoCompletoURL = servidor + caminhoCompletoURL;
                else
                    caminhoCompletoURL = servidor + "/" + caminhoCompletoURL;
            }
            else
                servidor = caminhoCompletoURL.Substring(0, caminhoCompletoURL.IndexOf("/",8));

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(caminhoCompletoURL);

            request.UseDefaultCredentials = true;
            request.PreAuthenticate = true;
            request.Credentials = CredentialCache.DefaultCredentials;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    html = reader.ReadToEnd();
                }
            }

            //////string strHTML = String.Empty;
            //////using (var sw = new StringWriter())
            //////{
            //////    HttpServerUtility.Execute(caminhoCompletoURL, sw);
            //////    html = sw.ToString();
            //////}


            List<ArquivoWeb> lstArquivosWeb = new List<ArquivoWeb>();
            // remove cabecalho/titulo
            int ini = 0;
            int fim = ini + 1;
            ini = html.IndexOf("<H1>", ini) + 4;
            fim = html.IndexOf("</H1>", ini + 1);

            string titulo = html.Substring(ini, fim - ini);

            // separa o Titulo
            ArquivoWeb tmpTitulo = new ArquivoWeb();
            tmpTitulo.Target = titulo;
            tmpTitulo.Texto = titulo;
            tmpTitulo.EhArquivo = false;

            // limpa "sujeiras"
            html = html.Substring(html.IndexOf("<pre>"));
            html = html.Replace("<pre>", "").Replace("</pre>", "").Replace("<hr>", "").Replace("</body>", "").Replace("</html>", "");

            // quebra em cada <br>
            html = html.Replace("<br>", "|");
            List<string> lstBRs = html.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).ToList();

            // separa somente as tags AHREFs
            for (int i = 0; i < lstBRs.Count; i++)
            {
                ini = lstBRs[i].IndexOf("<A HREF");
                fim = lstBRs[i].IndexOf("</A>", ini + 1);
                if (ini >= 0 && fim > 0)
                {
                    string AHref = lstBRs[i].Substring(ini, fim + 4 - ini).Trim();

                    // separa em texto e target
                    ArquivoWeb tmp = new ArquivoWeb();
                    fim = AHref.IndexOf(">")-1;
                    tmp.Target = servidor + AHref.Substring(9, fim - 9);
                    tmp.Texto = AHref.Substring(fim + 2).Replace("</A>", "");
                    tmp.EhArquivo = lstBRs[i].IndexOf("&lt;dir&gt;") < 0;

                    if (tmp.Target.IndexOf("web.config")<0)
                        lstArquivosWeb.Add(tmp);
                }
            }
            // separa o "para diretorio pai"
            ArquivoWeb tmpPai = lstArquivosWeb[0];
            lstArquivosWeb.RemoveAt(0);

            // ordena por Diretorios primeiro
            List<ArquivoWeb> SortedList = lstArquivosWeb.OrderBy(o => o.EhArquivo).ToList();

            // insere o titulo na 1a posicao
            SortedList.Insert(0, tmpTitulo);

            // insere o diretorio pai em 2a posicao
            tmpPai.EhArquivo = false;
            tmpPai.Texto = "Voltar";
            SortedList.Insert(1, tmpPai);

            return SortedList;
        }

        /// <summary>
        /// Converte Imagem para string 64
        /// </summary>
        /// <param name="image">Imagem</param>
        /// <returns>string</returns>
        public string ImageToBase64(Image image)
        {

            string base64String = "";

                using (MemoryStream _mStream = new MemoryStream())
                {
                    image.Save(_mStream, ImageFormat.Bmp);
                    byte[] _imageBytes = _mStream.ToArray();
                    base64String = Convert.ToBase64String(_imageBytes);

                    return "data:image/jpg;base64," + base64String;
                }

        }

        /// <summary>
        /// Converte String64 para Imagem
        /// </summary>
        /// <param name="base64String">String Codificada da Imagem</param>
        /// <returns>System.Drawing.Image</returns>
        public System.Drawing.Image Base64ToImage(string base64String)
        {
            byte[] imageBytes = Convert.FromBase64String(base64String);
            MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
            ms.Write(imageBytes, 0, imageBytes.Length);
            System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
            return image;
        }

        /// <summary>
        /// Checa se o arquivo anexo é valido. Returna true/false
        /// </summary>
        /// <param name="url">URL do documento</param>
        /// <returns>true ou false</returns>
        public bool RemoteFileExists(string url)
        {
            try
            {
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                request.Method = "HEAD";

                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                response.Close();
                return (response.StatusCode == HttpStatusCode.OK);
            }
            catch
            {
                //Any exception will returns false.
                return false;
            }
        }


        /// <summary>
        /// Cria nova Pasta no servidor
        /// </summary>
        /// <param name="caminhoVirtual">Caminho</param>
        /// <param name="nomePasta">Nome da Pasta a ser criada em "caminho"</param>
        /// <returns>string</returns>
        public string Criar_NovaPasta(string caminhoVirtual, string nomePasta)
        {
            try
            {
                // caminhoVirtual = "UPLOAD/SP-010 (FERNÃO DIAS)/"
                string CaminhoVirtualRaizArquivos = new ParametroBLL().Parametro_GetValor("CaminhoVirtualRaizArquivos"); // "http://192.168.15.11:8080/UPLOAD/"
                string CaminhoVirtualServidor = CaminhoVirtualRaizArquivos.Substring(0, CaminhoVirtualRaizArquivos.IndexOf("/",15)); //  "http://192.168.15.11:8080"
                string CaminhoCompletoVirtualPasta = CaminhoVirtualServidor + caminhoVirtual;  // "http://192.168.15.11:8080/UPLOAD/SP-010 (FERNÃO DIAS)/"

                string pedacoInteresse = CaminhoCompletoVirtualPasta.Replace(CaminhoVirtualRaizArquivos, ""); // "/SP-010 (FERNÃO DIAS)/"

                string CaminhoFisicoRaizArquivos = new ParametroBLL().Parametro_GetValor("CaminhoFisicoRaizArquivos"); // "C:\SIGOA\DEPLOY\UPLOAD"
                if (!CaminhoFisicoRaizArquivos.EndsWith("\\"))
                    CaminhoFisicoRaizArquivos = CaminhoFisicoRaizArquivos + "\\";


                string caminhoFisicoInteresse = CaminhoFisicoRaizArquivos +  pedacoInteresse.Replace("/", "\\"); // "C:\SIGOA\DEPLOY\UPLOAD\SP-010 (FERNÃO DIAS)\"

                if (!caminhoFisicoInteresse.EndsWith("\\"))
                    caminhoFisicoInteresse = caminhoFisicoInteresse + "\\";

                string retorno = "";
                if (!System.IO.Directory.Exists(caminhoFisicoInteresse + nomePasta))
                {
                    System.IO.Directory.CreateDirectory(caminhoFisicoInteresse + nomePasta);

                    retorno = CaminhoCompletoVirtualPasta + "/" + nomePasta;
                }
                else
                    retorno= "Caminho já existe";

                return retorno;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }



        // *************** excel **************
        /// <summary>
        ///  Pega o valor da celula
        /// </summary>
        /// <param name="doc">Arquivo</param>
        /// <param name="cell">Célula</param>
        /// <returns>string</returns>
        public string GetCellValue(SpreadsheetDocument doc, Cell cell)
        {
            string value = "";
            if (cell.CellValue != null)
                value = cell.CellValue.InnerText;

            if (cell.DataType != null && cell.DataType.Value == CellValues.SharedString)
            {
                return doc.WorkbookPart.SharedStringTablePart.SharedStringTable.ChildElements.GetItem(int.Parse(value)).InnerText;
            }
            return value;
        }

        /// <summary>
        /// Seleciona a linha da planilha
        /// </summary>
        /// <param name="worksheet">Planilha</param>
        /// <param name="rowIndex">Linha - 1</param>
        /// <returns>Row</returns>
        public  Row GetRow(Worksheet worksheet, uint rowIndex)
        {
            return worksheet.GetFirstChild<SheetData>().Elements<Row>().Where(r => r.RowIndex == rowIndex).First();
        }

        /// <summary>
        /// seleciona a celula da planilha
        /// </summary>
        /// <param name="worksheet">Planilha</param>
        /// <param name="columnName">Coluna</param>
        /// <param name="rowIndex">Linha - 1</param>
        /// <returns>Cell</returns>
        public  Cell GetCell(Worksheet worksheet, string columnName, uint rowIndex)
        {
            Row row = GetRow(worksheet, rowIndex);

            if (row == null)
                return null;

            return row.Elements<Cell>().Where(c => string.Compare(c.CellReference.Value, columnName + rowIndex, true) == 0).First();
        }

        /// <summary>
        /// Seleciona a planilha
        /// </summary>
        /// <param name="document">Arquivo</param>
        /// <param name="worksheetName">Nome da Planilha</param>
        /// <returns>Worksheet</returns>
        public  Worksheet GetWorksheet(SpreadsheetDocument document, string worksheetName)
        {
            IEnumerable<Sheet> sheets = document.WorkbookPart.Workbook.Descendants<Sheet>().Where(s => s.Name == worksheetName);
            WorksheetPart worksheetPart = (WorksheetPart)document.WorkbookPart.GetPartById(sheets.First().Id);
            if (sheets.Count() == 0)
                return null;
            else
                return worksheetPart.Worksheet;
        }

        /// <summary>
        /// Mescla celulas
        /// </summary>
        /// <param name="worksheet">Planilha</param>
        /// <param name="cell1Name">Célula Inicio</param>
        /// <param name="cell2Name">Célula Fim</param>
        public void MergeCells(Worksheet worksheet, string cell1Name, string cell2Name)
        {

            MergeCells mergeCells;
            if (worksheet.Elements<MergeCells>().Count() > 0)
            {
                mergeCells = worksheet.Elements<MergeCells>().First();
            }
            else
            {
                mergeCells = new MergeCells();

                // Insert a MergeCells object into the specified position.
                if (worksheet.Elements<CustomSheetView>().Count() > 0)
                {
                    worksheet.InsertAfter(mergeCells, worksheet.Elements<CustomSheetView>().First());
                }
                else if (worksheet.Elements<DataConsolidate>().Count() > 0)
                {
                    worksheet.InsertAfter(mergeCells, worksheet.Elements<DataConsolidate>().First());
                }
                else if (worksheet.Elements<SortState>().Count() > 0)
                {
                    worksheet.InsertAfter(mergeCells, worksheet.Elements<SortState>().First());
                }
                else if (worksheet.Elements<AutoFilter>().Count() > 0)
                {
                    worksheet.InsertAfter(mergeCells, worksheet.Elements<AutoFilter>().First());
                }
                else if (worksheet.Elements<Scenarios>().Count() > 0)
                {
                    worksheet.InsertAfter(mergeCells, worksheet.Elements<Scenarios>().First());
                }
                else if (worksheet.Elements<ProtectedRanges>().Count() > 0)
                {
                    worksheet.InsertAfter(mergeCells, worksheet.Elements<ProtectedRanges>().First());
                }
                else if (worksheet.Elements<SheetProtection>().Count() > 0)
                {
                    worksheet.InsertAfter(mergeCells, worksheet.Elements<SheetProtection>().First());
                }
                else if (worksheet.Elements<SheetCalculationProperties>().Count() > 0)
                {
                    worksheet.InsertAfter(mergeCells, worksheet.Elements<SheetCalculationProperties>().First());
                }
                else
                {
                    worksheet.InsertAfter(mergeCells, worksheet.Elements<SheetData>().First());
                }
            }

            // Create the merged cell and append it to the MergeCells collection.
            MergeCell mergeCell = new MergeCell() { Reference = new DocumentFormat.OpenXml.StringValue(cell1Name + ":" + cell2Name) };
            mergeCells.Append(mergeCell);

            //  worksheet.Save();

        }

        /// <summary>
        /// Copia celulas
        /// </summary>
        /// <param name="document">Arquivo</param>
        /// <param name="worksheet">Planilha</param>
        /// <param name="Origem">Célula Origem</param>
        /// <param name="cellDestinoColuna">Coluna da Célula Destino</param>
        /// <param name="cellDestinoLinha">Linha da Célula Destino</param>
        public void copyCell(SpreadsheetDocument document, Worksheet worksheet, Cell Origem, string cellDestinoColuna, uint cellDestinoLinha)
        {
            string valor = GetCellValue(document, Origem);

            Cell cellDestino = InsertCellInWorksheet(cellDestinoColuna, cellDestinoLinha, worksheet);
            cellDestino.DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String;
            cellDestino.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(Convert.ToString(valor));

            //if (Origem.StyleIndex != null)
            cellDestino.StyleIndex = Origem.StyleIndex;
        }

        /// <summary>
        /// Cria/pega Célula 
        /// </summary>
        /// <param name="columnName">Coluna da Célula</param>
        /// <param name="rowIndex">Linha da Célula</param>
        /// <param name="worksheet">Planilha</param>
        /// <returns>Cell</returns>
        public Cell InsertCellInWorksheet(string columnName, uint rowIndex, Worksheet worksheet)
        {
            SheetData sheetData = worksheet.GetFirstChild<SheetData>();
            string cellReference = columnName + rowIndex;

            // If the worksheet does not contain a row with the specified row index, insert one.
            Row row;
            if (sheetData.Elements<Row>().Where(r => r.RowIndex == rowIndex).Count() != 0)
            {
                row = sheetData.Elements<Row>().Where(r => r.RowIndex == rowIndex).First();
            }
            else
            {
                row = new Row() { RowIndex = rowIndex };
                sheetData.Append(row);
            }

            // If there is not a cell with the specified column name, insert one.  
            if (row.Elements<Cell>().Where(c => c.CellReference.Value == columnName + rowIndex).Count() > 0)
            {
                return row.Elements<Cell>().Where(c => c.CellReference.Value == cellReference).First();
            }
            else
            {
                // Cells must be in sequential order according to CellReference. Determine where to insert the new cell.
                Cell refCell = null;
                foreach (Cell cell in row.Elements<Cell>())
                {
                    if (cell.CellReference.Value.Length == cellReference.Length)
                    {
                        if (string.Compare(cell.CellReference.Value, cellReference, true) > 0)
                        {
                            refCell = cell;
                            break;
                        }
                    }
                }

                Cell newCell = new Cell() { CellReference = cellReference };
                row.InsertBefore(newCell, refCell);

                worksheet.Save();
                return newCell;
            }
        }

        /// <summary>
        /// Checa se é numero
        /// </summary>
        /// <param name="s">string a testar</param>
        /// <returns>true/false</returns>
        public bool IsNumeric(string s)
        {
            float output;
            return float.TryParse(s, out output);
        }


        // *************** INTEGRACAO APIS **************

        /// <summary>
        ///  Busca a lista de Rodovias pela API
        /// </summary>
        /// <param name="rod_Codigo">Codigo da Rodovia, vazio para todos</param>
        /// <returns>List Rodovia</returns>
        public List<Rodovia> get_Rodovias(string rod_Codigo = "")
        {
            string usu_id = new ParametroDAO().Parametro_GetValor("SIRGeo_Usuario");
            string urlPath =  new ParametroDAO().Parametro_GetValor("SIRGeo_URL");

            try
            {
                string SIRGeo_URL = new ParametroDAO().Parametro_GetValor("SIRGeo_URL");
                string SIRGeo_Usuario = new ParametroDAO().Parametro_GetValor("SIRGeo_Usuario");


                using (var client = new HttpClient())
                {
                    var contentString = new FormUrlEncodedContent(new List<KeyValuePair<string, string>>
                                                                       {
                                                                            new KeyValuePair<string, string>("usu_id", usu_id.ToString())
                                                                       });

                    client.BaseAddress = new Uri(urlPath + "/Token");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = client.PostAsync(new Uri(urlPath + "/Token?usu_id=" + usu_id.ToString()), contentString).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        var responseBody = response.Content.ReadAsStringAsync().Result;
                        token token_retorno = Newtonsoft.Json.JsonConvert.DeserializeObject<token>(responseBody);

                        if (token_retorno.tok_valido)
                        {
                            string nToken_Atual = token_retorno.tok_token;

                            using (var client2 = new HttpClient())
                            {
                                var contentString2 = new FormUrlEncodedContent(new List<KeyValuePair<string, string>>
                                                                                   {
                                                                                        new KeyValuePair<string, string>("usu_id", usu_id.ToString())
                                                                                   });
                                client2.BaseAddress = new Uri(urlPath + "/Rodovias");
                                client2.DefaultRequestHeaders.Accept.Clear();
                                client2.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                                if (rod_Codigo.Trim() != "")
                                    rod_Codigo = "&rod_Codigo=" + rod_Codigo;
                                HttpResponseMessage response2 = client2.PostAsync(new Uri(urlPath + "/Rodovias?usu_id=" + usu_id.ToString() + "&token=" + nToken_Atual + rod_Codigo), contentString2).Result;

                                if (response2.IsSuccessStatusCode)
                                {
                                    var responseBody2 = response2.Content.ReadAsStringAsync().Result;

                                    // ajuste para deserializar a variavel em lista
                                    responseBody2 = "{Rodovias:" + responseBody2 + "}";

                                    lstRodovias listaDeRodovias = Newtonsoft.Json.JsonConvert.DeserializeObject<lstRodovias>(responseBody2);

                                    return listaDeRodovias.Rodovias;
                                }
                            }

                        }
                    }
                    else
                    {
                        List<Rodovia> saida_erro = new List<Rodovia>();
                        Rodovia err = new Rodovia();
                        err.rod_codigo = "-1";
                        err.rod_descricao = response.ReasonPhrase;

                        saida_erro.Add(err);
                        return saida_erro;

                    }

                }
            }
            catch (Exception ex)
            {
                List<Rodovia> saida_erro = new List<Rodovia>();
                Rodovia err = new Rodovia();
                err.rod_codigo = "-1";
                err.rod_descricao = ex.InnerException.Message;

                saida_erro.Add(err);
                return saida_erro;
            }
            return null;
        }


        /// <summary>
        ///  Busca a lista de OAEs pela API
        /// </summary>
        /// <param name="rod_id">ID da Rodovia, vazio para todos</param>
        /// <returns>List Rodovia</returns>
        public List<OAE> get_OAEs(string rod_id)
        {
            string usu_id = new ParametroDAO().Parametro_GetValor("SIRGeo_Usuario");
            string urlPath =  new ParametroDAO().Parametro_GetValor("SIRGeo_URL");

            try
            {
                string SIRGeo_URL = new ParametroDAO().Parametro_GetValor("SIRGeo_URL");
                string SIRGeo_Usuario = new ParametroDAO().Parametro_GetValor("SIRGeo_Usuario");


                using (var client = new HttpClient())
                {
                    var contentString = new FormUrlEncodedContent(new List<KeyValuePair<string, string>>
                                                                       {
                                                                            new KeyValuePair<string, string>("usu_id", usu_id.ToString())
                                                                       });

                    client.BaseAddress = new Uri(urlPath + "/Token");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = client.PostAsync(new Uri(urlPath + "/Token?usu_id=" + usu_id.ToString()), contentString).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        var responseBody = response.Content.ReadAsStringAsync().Result;
                        token token_retorno = Newtonsoft.Json.JsonConvert.DeserializeObject<token>(responseBody);

                        if (token_retorno.tok_valido)
                        {
                            string nToken_Atual = token_retorno.tok_token;

                            using (var client2 = new HttpClient())
                            {
                                var contentString2 = new FormUrlEncodedContent(new List<KeyValuePair<string, string>>
                                                                                   {
                                                                                        new KeyValuePair<string, string>("usu_id", usu_id.ToString())
                                                                                   });
                                client2.BaseAddress = new Uri(urlPath + "/OaeLevantamento");
                                client2.DefaultRequestHeaders.Accept.Clear();
                                client2.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                                if ((rod_id.Trim() != "") && (IsNumeric(rod_id)))
                                    rod_id = "&rod_id=" + rod_id.ToString();
                                HttpResponseMessage response2 = client2.PostAsync(new Uri(urlPath + "/OaeLevantamento?usu_id=" + usu_id.ToString() + "&token=" + nToken_Atual + rod_id), contentString2).Result;

                                if (response2.IsSuccessStatusCode)
                                {
                                    var responseBody2 = response2.Content.ReadAsStringAsync().Result;

                                    // ajuste para deserializar a variavel em lista
                                    responseBody2 = "{OAEs:" + responseBody2 + "}";

                                    lstOAEs listaDeOAEs = Newtonsoft.Json.JsonConvert.DeserializeObject<lstOAEs>(responseBody2);

                                    return listaDeOAEs.OAEs;
                                }
                            }

                        }
                    }
                    else
                    {
                        List<OAE> saida_erro = new List<OAE>();
                        OAE err = new OAE();
                        err.rod_id = -1;
                        err.oae_km_inicial = response.ReasonPhrase;

                        saida_erro.Add(err);
                        return saida_erro;

                    }

                }
            }
            catch (Exception ex)
            {
                List<OAE> saida_erro = new List<OAE>();
                OAE err = new OAE();
                err.rod_id = -1;
                err.oae_km_inicial = ex.InnerException.Message;

                saida_erro.Add(err);
                return saida_erro;
            }
            return null;
        }

        /// <summary>
        ///  Busca a lista de Regionais pela API
        /// </summary>
        /// <returns>List Regional</returns>
        public List<Regional> get_Regionais()
        {
            string usu_id = new ParametroDAO().Parametro_GetValor("SIRGeo_Usuario");
            string urlPath =  new ParametroDAO().Parametro_GetValor("SIRGeo_URL");

            try
            {
                string SIRGeo_URL = new ParametroDAO().Parametro_GetValor("SIRGeo_URL");
                string SIRGeo_Usuario = new ParametroDAO().Parametro_GetValor("SIRGeo_Usuario");


                using (var client = new HttpClient())
                {
                    var contentString = new FormUrlEncodedContent(new List<KeyValuePair<string, string>>
                                                                       {
                                                                            new KeyValuePair<string, string>("usu_id", usu_id.ToString())
                                                                       });

                    client.BaseAddress = new Uri(urlPath + "/Token");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = client.PostAsync(new Uri(urlPath + "/Token?usu_id=" + usu_id.ToString()), contentString).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        var responseBody = response.Content.ReadAsStringAsync().Result;
                        token token_retorno = Newtonsoft.Json.JsonConvert.DeserializeObject<token>(responseBody);

                        if (token_retorno.tok_valido)
                        {
                            string nToken_Atual = token_retorno.tok_token;

                            using (var client2 = new HttpClient())
                            {
                                var contentString2 = new FormUrlEncodedContent(new List<KeyValuePair<string, string>>
                                                                                   {
                                                                                        new KeyValuePair<string, string>("usu_id", usu_id.ToString())
                                                                                   });
                                client2.BaseAddress = new Uri(urlPath + "/Dominio");
                                client2.DefaultRequestHeaders.Accept.Clear();
                                client2.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                                HttpResponseMessage response2 = client2.PostAsync(new Uri(urlPath + "/Dominio?dominio=regionais&usu_id=" + usu_id.ToString() + "&token=" + nToken_Atual), contentString2).Result;

                                if (response2.IsSuccessStatusCode)
                                {
                                    var responseBody2 = response2.Content.ReadAsStringAsync().Result;

                                    // ajuste para deserializar a variavel em lista
                                    responseBody2 = "{Regionais:" + responseBody2 + "}";

                                    lstRegionais listaDeRegionais = Newtonsoft.Json.JsonConvert.DeserializeObject<lstRegionais>(responseBody2);

                                    return listaDeRegionais.Regionais;
                                }
                            }

                        }
                    }
                    else
                    {
                        List<Regional> saida_erro = new List<Regional>();
                        Regional err = new Regional();
                        err.reg_id = -1;
                        err.reg_codigo = response.ReasonPhrase;

                        saida_erro.Add(err);
                        return saida_erro;

                    }

                }
            }
            catch (Exception ex)
            {
                List<Regional> saida_erro = new List<Regional>();
                Regional err = new Regional();
                err.reg_id = -1;
                err.reg_codigo = ex.InnerException.Message;

                saida_erro.Add(err);
                return saida_erro;
            }
            return null;
        }



        /// <summary>
        ///  Busca a lista de SentidoRodovias pela API
        /// </summary>
        /// <returns>List SentidoRodovia</returns>
        public List<SentidoRodovia> get_SentidoRodovias()
        {
            string usu_id = new ParametroDAO().Parametro_GetValor("SIRGeo_Usuario");
            string urlPath =  new ParametroDAO().Parametro_GetValor("SIRGeo_URL");

            try
            {
                string SIRGeo_URL = new ParametroDAO().Parametro_GetValor("SIRGeo_URL");
                string SIRGeo_Usuario = new ParametroDAO().Parametro_GetValor("SIRGeo_Usuario");


                using (var client = new HttpClient())
                {
                    var contentString = new FormUrlEncodedContent(new List<KeyValuePair<string, string>>
                                                                       {
                                                                            new KeyValuePair<string, string>("usu_id", usu_id.ToString())
                                                                       });

                    client.BaseAddress = new Uri(urlPath + "/Token");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = client.PostAsync(new Uri(urlPath + "/Token?usu_id=" + usu_id.ToString()), contentString).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        var responseBody = response.Content.ReadAsStringAsync().Result;
                        token token_retorno = Newtonsoft.Json.JsonConvert.DeserializeObject<token>(responseBody);

                        if (token_retorno.tok_valido)
                        {
                            string nToken_Atual = token_retorno.tok_token;

                            using (var client2 = new HttpClient())
                            {
                                var contentString2 = new FormUrlEncodedContent(new List<KeyValuePair<string, string>>
                                                                                   {
                                                                                        new KeyValuePair<string, string>("usu_id", usu_id.ToString())
                                                                                   });
                                client2.BaseAddress = new Uri(urlPath + "/SentidoRodovias");
                                client2.DefaultRequestHeaders.Accept.Clear();
                                client2.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                                HttpResponseMessage response2 = client2.PostAsync(new Uri(urlPath + "/Dominio?dominio=rodovias_sentidos&usu_id=" + usu_id.ToString() + "&token=" + nToken_Atual), contentString2).Result;

                                if (response2.IsSuccessStatusCode)
                                {
                                    var responseBody2 = response2.Content.ReadAsStringAsync().Result;

                                    // ajuste para deserializar a variavel em lista
                                    responseBody2 = "{SentidosRodovia:" + responseBody2 + "}";

                                    lstSentidoRodovia listaDeSentidoRodovias = Newtonsoft.Json.JsonConvert.DeserializeObject<lstSentidoRodovia>(responseBody2);

                                    return listaDeSentidoRodovias.SentidosRodovia;
                                }
                            }

                        }
                    }
                    else
                    {
                        List<SentidoRodovia> saida_erro = new List<SentidoRodovia>();
                        SentidoRodovia err = new SentidoRodovia();
                        err.sen_id = -1;
                        err.sen_descricao = response.ReasonPhrase;

                        saida_erro.Add(err);
                        return saida_erro;

                    }

                }
            }
            catch (Exception ex)
            {
                List<SentidoRodovia> saida_erro = new List<SentidoRodovia>();
                SentidoRodovia err = new SentidoRodovia();
                err.sen_id = -1;
                err.sen_descricao = ex.InnerException.Message;

                saida_erro.Add(err);
                return saida_erro;
            }
            return null;
        }


        /// <summary>
        ///  Busca a lista de TipoOAE pela API
        /// </summary>
        /// <returns>List TipoOAE</returns>
        public List<TipoOAE> get_TiposOAE()
        {
            string usu_id = new ParametroDAO().Parametro_GetValor("SIRGeo_Usuario");
            string urlPath =  new ParametroDAO().Parametro_GetValor("SIRGeo_URL");

            try
            {
                string SIRGeo_URL = new ParametroDAO().Parametro_GetValor("SIRGeo_URL");
                string SIRGeo_Usuario = new ParametroDAO().Parametro_GetValor("SIRGeo_Usuario");


                using (var client = new HttpClient())
                {
                    var contentString = new FormUrlEncodedContent(new List<KeyValuePair<string, string>>
                                                                       {
                                                                            new KeyValuePair<string, string>("usu_id", usu_id.ToString())
                                                                       });

                    client.BaseAddress = new Uri(urlPath + "/Token");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = client.PostAsync(new Uri(urlPath + "/Token?usu_id=" + usu_id.ToString()), contentString).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        var responseBody = response.Content.ReadAsStringAsync().Result;
                        token token_retorno = Newtonsoft.Json.JsonConvert.DeserializeObject<token>(responseBody);

                        if (token_retorno.tok_valido)
                        {
                            string nToken_Atual = token_retorno.tok_token;

                            using (var client2 = new HttpClient())
                            {
                                var contentString2 = new FormUrlEncodedContent(new List<KeyValuePair<string, string>>
                                                                                   {
                                                                                        new KeyValuePair<string, string>("usu_id", usu_id.ToString())
                                                                                   });
                                client2.BaseAddress = new Uri(urlPath + "/oae_tipos");
                                client2.DefaultRequestHeaders.Accept.Clear();
                                client2.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                                HttpResponseMessage response2 = client2.PostAsync(new Uri(urlPath + "/Dominio?dominio=oae_tipos&usu_id=" + usu_id.ToString() + "&token=" + nToken_Atual), contentString2).Result;

                                if (response2.IsSuccessStatusCode)
                                {
                                    var responseBody2 = response2.Content.ReadAsStringAsync().Result;

                                    // ajuste para deserializar a variavel em lista
                                    responseBody2 = "{TiposOAE:" + responseBody2 + "}";

                                    lstTipoOAE listaDeTipoOAEs = Newtonsoft.Json.JsonConvert.DeserializeObject<lstTipoOAE>(responseBody2);

                                    return listaDeTipoOAEs.TiposOAE;
                                }
                            }

                        }
                    }
                    else
                    {
                        List<TipoOAE> saida_erro = new List<TipoOAE>();
                        TipoOAE err = new TipoOAE();
                        err.oat_id = -1;
                        err.oat_descricao = response.ReasonPhrase;

                        saida_erro.Add(err);
                        return saida_erro;

                    }

                }
            }
            catch (Exception ex)
            {
                List<TipoOAE> saida_erro = new List<TipoOAE>();
                TipoOAE err = new TipoOAE();
                err.oat_id = -1;
                err.oat_descricao = ex.InnerException.Message;

                saida_erro.Add(err);
                return saida_erro;
            }
            return null;
        }












        // ***************  APIs DER **************

        /// <summary>
        /// Busca a lista de VDMs para a rodovia solicitada
        /// </summary>
        /// <param name="rod_codigo">Código da Rodovia</param>
        /// <param name="kminicial">Km inicial solicitado</param>
        /// <param name="kmfinal">Km final solicitado</param>
        /// <returns>Lista vdm</returns>
        public List<vdm> get_VDMs(string rod_codigo, decimal kminicial, decimal kmfinal)
        {
            try
            {
                using (var client = new HttpClient())
                {

                    vdm_entrada VDMs_In = new vdm_entrada();
                    VDMs_In.modo = "consolidado";
                    VDMs_In.rodovia = rod_codigo.Replace(" ","");
                    VDMs_In.kminicial = kminicial;
                    VDMs_In.kmfinal = kmfinal;

                    string DER_API_VDM_URL = new ParametroDAO().Parametro_GetValor("DER_API_VDM_URL");
                    string DER_API_VDM_Usuario = new ParametroDAO().Parametro_GetValor("DER_API_VDM_Usuario");
                    string DER_API_VDM_Senha = new ParametroDAO().Parametro_GetValor("DER_API_VDM_Senha");

                    var stringContent = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(VDMs_In), Encoding.UTF8, "application/json");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Add("Auth", DER_API_VDM_Usuario + "-" + DER_API_VDM_Senha);
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage response = client.PostAsync(new Uri(DER_API_VDM_URL), stringContent).Result;
                    var content = response.Content;
                    var headers = response.Headers;
                    var status = response.StatusCode;

                    var responseBody = response.Content.ReadAsStringAsync().Result;

                    vdm_retorno retorno2 = null;
                    retorno_erro error = null;

                    // verifica se tem erro
                    try
                    {                       
                       retorno2 = Newtonsoft.Json.JsonConvert.DeserializeObject<vdm_retorno>(responseBody);
                    }
                    catch
                    {
                        error = Newtonsoft.Json.JsonConvert.DeserializeObject<retorno_erro>(responseBody);
                    }

                    if (retorno2 != null) 
                    {
                        if (retorno2.status)
                        {
                            // retorna Lista<vdm>
                            return retorno2.data.Vdms;
                        }
                    }
                    else
                    if (error != null)
                        if (!error.status)
                        {
                            List<vdm> saida_erro = new List<vdm>();
                            vdm err = new vdm();
                            err.vdm_ano = -1;
                            err.vdm_rodovia = error.data;

                            saida_erro.Add(err);
                            return saida_erro;
                        }
                }


            }
            catch (Exception ex)
            {
                List<vdm> saida_erro = new List<vdm>();
                vdm err = new vdm();
                err.vdm_ano = -1;
                err.vdm_rodovia = ex.InnerException.Message;

                saida_erro.Add(err);
                return saida_erro;
            }

            return null;
        }

        /// <summary>
        /// Lista das TPUs
        /// </summary>
        /// <param name="ano">Ano</param>
        /// <param name="fase">fase = 23</param>
        /// <param name="mes">Mês</param>
        /// <param name="onerado">Onerado: SIM,NÃO, vazio para todos</param>
        ///  /// <param name="codSubItem">codSubItem: Opcional</param>
        /// <returns>Lista tpu</returns>
        public List<tpu> get_TPUs(string ano, string fase, string mes, string onerado = "", string codSubItem = "")
        {

            try
            {
                string DER_API_TPU_URL = new ParametroDAO().Parametro_GetValor("DER_API_TPU_URL");
                string DER_API_TPU_Usuario = new ParametroDAO().Parametro_GetValor("DER_API_TPU_Usuario");
                string DER_API_TPU_Senha = new ParametroDAO().Parametro_GetValor("DER_API_TPU_Senha");

                using (var client = new HttpClient())
                {

                    tpu_entrada tpus_In = new tpu_entrada();
                    tpus_In.modo = "TPU";
                    tpus_In.ano = ano;
                    tpus_In.fase = fase;
                    tpus_In.mes = mes;
                    tpus_In.onerado = onerado;
                    tpus_In.CodSubItem = codSubItem;


                    var stringContent = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(tpus_In), Encoding.UTF8, "application/json");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Add("Auth", DER_API_TPU_Usuario + "-" + DER_API_TPU_Senha);
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage response = client.PostAsync(new Uri(DER_API_TPU_URL), stringContent).Result;
                    var content = response.Content;
                    var headers = response.Headers;
                    var status = response.StatusCode;

                    var responseBody = response.Content.ReadAsStringAsync().Result;

                    tpu_retorno retorno2 = null;
                    retorno_erro error = null;

                    // verifica se tem erro
                    try
                    {
                        retorno2 = Newtonsoft.Json.JsonConvert.DeserializeObject<tpu_retorno>(responseBody);
                    }
                    catch
                    {
                        error = Newtonsoft.Json.JsonConvert.DeserializeObject<retorno_erro>(responseBody);
                    }

                    if (retorno2 != null)
                    {
                        if (retorno2.status)
                        {
                            List<tpu> retorno = retorno2.data.TpuPrecosSites;
                            return retorno;
                        }
                    }
                    else
                    if (error != null)
                        if (!error.status)
                        {
                            List<tpu> saida_erro = new List<tpu>();
                            tpu err = new tpu();
                            err.DataTpu = "-1";
                            err.CodSubItem = error.data;

                            saida_erro.Add(err);
                            return saida_erro;
                        }

                }

            }
            catch (Exception ex)
            {
                List<tpu> saida_erro = new List<tpu>();
                tpu err = new tpu();
                err.DataTpu = "-1";
                err.CodSubItem = ex.InnerException.Message;

                saida_erro.Add(err);
                return saida_erro;
            }

            return null;
        }


        /// <summary>
        /// Converte Lista para Datatable
        /// </summary>
        /// <typeparam name="T">Tipo da Lista</typeparam>
        /// <param name="items">Lista de entrada</param>
        /// <returns>Datatable</returns>
        public  DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Defining type of data column gives proper data table 
                var type = (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType);
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name, type);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }


    }
}