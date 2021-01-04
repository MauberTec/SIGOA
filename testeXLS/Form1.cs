using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
//using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System.IO;
using DocumentFormat.OpenXml;
using System.Text.RegularExpressions;
using Color = DocumentFormat.OpenXml.Spreadsheet.Color;

namespace testeXLS
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }


       public bool IsNumeric(string value)
        {
            return value.All(char.IsNumber);
        }

 
        //Copy an existing row and insert it
        //We don't need to copy styles of a refRow because a CloneNode() or Clone() methods do it for us
   //     private void CopyToLine(Row refRow, uint rowIndex, SheetData sheetData)
        private void CopyToLine(Row refRow, SheetData sheetData)
       {
            uint newRowIndex;
            var newRow = (Row)refRow.CloneNode(true);
            sheetData.InsertBefore(newRow, refRow);

            //newRowIndex = System.Convert.ToUInt32(refRow.RowIndex.Value + 1);
            //refRow.RowIndex.Value = newRowIndex;


            //IEnumerable<Row> rows = sheetData.Descendants<Row>().Where(r => r.RowIndex.Value >= refRow.RowIndex.Value);
            //int posicao = (int)refRow.RowIndex.Value;
            //foreach (Row row in rows)
            //{
            //    newRowIndex = System.Convert.ToUInt32(posicao + 1);// System.Convert.ToUInt32(row.RowIndex.Value + 1);

            //    foreach (Cell cell in row.Elements<Cell>())
            //    {
            //        // Update the references for reserved cells.
            //        string cellReference = getCOL(cell.CellReference.Value) + newRowIndex.ToString();

            //        cell.CellReference = new StringValue(cellReference);
            //    }
            //    // Update the row index.
            //    row.RowIndex = new UInt32Value(newRowIndex);
            //    posicao += 1;
            //}


            // Loop through all the rows in the worksheet with higher row 
            // index values than the one you just added. For each one,
            // increment the existing row index.

          //  IEnumerable<Row> rows = sheetData.Descendants<Row>().Where(r => r.RowIndex.Value > newRow.RowIndex.Value);
            //  IEnumerable<Row> rows = sheetData.Descendants<Row>().Where(r => r.RowIndex.Value >= rowIndex);
            //int primeiro = 0;
            //foreach (Row row in rows)
            //{
            //    if (primeiro == 0)
            //    {
            //        newRowIndex = row.RowIndex.Value;
            //        primeiro = 1;
            //    }
            //    else
            //        newRowIndex = System.Convert.ToUInt32(row.RowIndex.Value + 1);

            //    foreach (Cell cell in row.Elements<Cell>())
            //    {
            //        // Update the references for reserved cells.
            //        string cellReference = cell.CellReference.Value;
            //            cell.CellReference = new StringValue(cellReference.Replace(row.RowIndex.Value.ToString(), newRowIndex.ToString()));
            //    }
            //    // Update the row index.
            //    row.RowIndex = new UInt32Value(newRowIndex);
            //}


            //foreach (Cell c in newRow.Descendants<Cell>())
            //{
            //    string coluna = getCOL(c.CellReference.Value);
            //    Cell refCell = refRow.Elements<Cell>().Where(cc => string.Compare(cc.CellReference.Value, coluna + (rowIndex - 1).ToString(), true) == 0).First();

            //    c.StyleIndex = refCell.StyleIndex;
            //}

        }



        private void button1_Click(object sender, EventArgs e)
        {
            string docName = @"c:\temp\Ficha_Cadastramento_Anomalias.xlsx";
            string sheetName = "Ficha_Cadastramento_Anomalias";

            //// Open the document for editing.
            using (SpreadsheetDocument document = SpreadsheetDocument.Open(docName, true))
            {
                Worksheet worksheetOrigem = GetWorksheet(document, "Rodape");

                Worksheet worksheet = GetWorksheet(document, sheetName);
                if (worksheet == null)
                {
                    return;
                }

                int LinhaDestino = 20;

                Cell cellOrigem_A1 = GetCell(worksheetOrigem, "A", 1);
                copyCell(document, worksheet, cellOrigem_A1, "A", 20);

                for (int li = 1; li <= 3; li++)
                {
                    for (int col = 65; col <= 81; col++) // VARRE as COLUNAS A até Q
                    {
                        Cell cellOrigem = InsertCellInWorksheet(((char)col).ToString(), Convert.ToUInt32(li), worksheetOrigem);
                      //  Cell cellOrigem = GetCell(worksheet, ((char)col).ToString(), Convert.ToUInt32(li));

                        copyCell(document, worksheet, cellOrigem, ((char)col).ToString(), Convert.ToUInt32(li + LinhaDestino -1));
                    }

                    MergeCells(worksheet,  "A" + (li + LinhaDestino - 1).ToString(), "Q" + (li + LinhaDestino - 1).ToString());
                }

               document.Save();
            }

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
        private static Cell InsertCellInWorksheet(string columnName, uint rowIndex, Worksheet worksheet)
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
        public Row GetRow(Worksheet worksheet, uint rowIndex)
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
        public Cell GetCell(Worksheet worksheet, string columnName, uint rowIndex)
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
        public Worksheet GetWorksheet(SpreadsheetDocument document, string worksheetName)
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




    }


}
