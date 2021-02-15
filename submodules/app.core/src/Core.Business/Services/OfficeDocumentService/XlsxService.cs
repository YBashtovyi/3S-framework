using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using Core.Base.Data;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

namespace Core.Services.Data
{
    public class XlsxService: IOfficeDocumentService
    {
        #region Methods_Public

        public DataTable ReadStream(Stream stream, string sheetName = null, Dictionary<string, string> columns = null)
        {
            return ReadInternal(stream, sheetName, columns);
        }

        public byte[] GenerateStream<TData>(IEnumerable<TData> dataCollection, Action<IOfficeDocumentOptions<TData>> optionsBuilder) where TData : class
        {
            var options = BuildOptions(optionsBuilder);
            using (var mem = new MemoryStream())
            {
                var document = SpreadsheetDocument.Create(mem, SpreadsheetDocumentType.Workbook);
                var workbookPart = document.AddWorkbookPart();
                workbookPart.Workbook = new Workbook();

                var worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
                
                var wbsp = workbookPart.AddNewPart<WorkbookStylesPart>();
                wbsp.Stylesheet = CreateStylesheet();
                wbsp.Stylesheet.Save();

                var sheets = workbookPart.Workbook.AppendChild(new Sheets());
                var sheet = new Sheet() { Id = workbookPart.GetIdOfPart(worksheetPart), SheetId = 1, Name = "Sheet1" };

                sheets.Append(sheet);

                var sheetData = CreateAndFillSheetData(dataCollection, options);

                var columns = AutoSize(sheetData, 11);

                worksheetPart.Worksheet = new Worksheet();
                worksheetPart.Worksheet.Append(columns);
                worksheetPart.Worksheet.Append(sheetData);

                workbookPart.Workbook.Save();
                document.Close();
                return mem.ToArray();
            }
        }

        #endregion Methods_Public

        #region Methods_Private

        private SheetData CreateAndFillSheetData<TData>(IEnumerable<TData> dataCollection, OfficeDocumentOptions<TData> options) where TData : class
        {
            var sheetData = new SheetData();

            var headerRow = new Row();
            foreach (var fieldConfig in options.Fields)
            {
                var cell = new Cell
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue(fieldConfig.Caption),
                    StyleIndex = (int)CellStyles.Header
                };
                headerRow.AppendChild(cell);
            }
            sheetData.AppendChild(headerRow);

            foreach (var data in dataCollection)
            {
                var newRow = CreateRowWithData(data, options);
                sheetData.AppendChild(newRow);
            }

            return sheetData;
        }

        private OfficeDocumentOptions<TData> BuildOptions<TData>(Action<IOfficeDocumentOptions<TData>> optionsBuilder) where TData : class
        {
            var options = new OfficeDocumentOptions<TData>();
            optionsBuilder(options);

            if (options.Fields == null)
            {
                options.ConfigureFields(null);
            }

            if (options.DefaultDateFormatter == null)
            {
                options.DefaultDateFormatter = (value) => value.ToString("g");
            }

            return options;
        }

        private Columns AutoSize(SheetData sheetData, double fontWidth)
        {
            var maxColWidth = GetMaxCharacterWidth(sheetData);

            var columns = new Columns();
            //this is the width of my font - yours may be different
            var maxWidth = fontWidth;
            foreach (var item in maxColWidth)
            {
                //width = Truncate([{Number of Characters} * {Maximum Digit Width} + {5 pixel padding}]/{Maximum Digit Width}*256)/256
                var width = Math.Truncate((item.Value * maxWidth + 5) / maxWidth * 256) / 256;

                //pixels=Truncate(((256 * {width} + Truncate(128/{Maximum Digit Width}))/256)*{Maximum Digit Width})
                var pixels = Math.Truncate(((256 * width + Math.Truncate(128 / maxWidth)) / 256) * maxWidth);

                //character width=Truncate(({pixels}-5)/{Maximum Digit Width} * 100+0.5)/100
                var charWidth = Math.Truncate((pixels - 5) / maxWidth * 100 + 0.5) / 100;

                var col = new Column() { BestFit = true, Min = (uint)(item.Key + 1), Max = (uint)(item.Key + 1), CustomWidth = true, Width = width };
                columns.Append(col);
            }

            return columns;
        }

        private Dictionary<int, int> GetMaxCharacterWidth(SheetData sheetData)
        {
            //iterate over all cells getting a max char value for each column
            var maxColWidth = new Dictionary<int, int>();
            var rows = sheetData.Elements<Row>();
            var numberStyles = new uint[] { 5, 6, 7, 8 }; //styles that will add extra chars
            var boldStyles = new uint[] { 1, 2, 3, 4, 6, 7, 8 }; //styles that will bold
            foreach (var r in rows)
            {
                var cells = r.Elements<Cell>().ToArray();

                //using cell index as my column
                for (var i = 0; i < cells.Length; i++)
                {
                    var cell = cells[i];
                    var cellValue = cell.CellValue == null ? string.Empty : cell.CellValue.InnerText;
                    var cellTextLength = cellValue.Length;

                    if (cell.StyleIndex != null && numberStyles.Contains(cell.StyleIndex))
                    {
                        var thousandCount = (int)Math.Truncate((double)cellTextLength / 4);

                        //add 3 for '.00' 
                        cellTextLength += (3 + thousandCount);
                    }

                    if (cell.StyleIndex != null && boldStyles.Contains(cell.StyleIndex))
                    {
                        //add an extra char for bold - not 100% acurate but good enough for what i need.
                        cellTextLength += 1;
                    }

                    if (maxColWidth.ContainsKey(i))
                    {
                        var current = maxColWidth[i];
                        if (cellTextLength > current)
                        {
                            maxColWidth[i] = cellTextLength;
                        }
                    }
                    else
                    {
                        maxColWidth.Add(i, cellTextLength);
                    }
                }
            }

            return maxColWidth;
        }

        private Stylesheet CreateStylesheet()
        {
            var ss = new Stylesheet();

            var fonts = new Fonts(
            new Font(                                                               // Index 0 - The default font.
                new FontSize() { Val = 11 },
                new Color() { Rgb = new HexBinaryValue() { Value = "000000" } },
                new FontName() { Val = "Calibri" }),
            new Font(                                                               // Index 1 - The bold font.
                new Bold(),
                new FontSize() { Val = 11 },
                new Color() { Rgb = new HexBinaryValue() { Value = "000000" } },
                new FontName() { Val = "Calibri" }),
            new Font(                                                               // Index 2 - The Italic font.
                new Italic(),
                new FontSize() { Val = 11 },
                new Color() { Rgb = new HexBinaryValue() { Value = "000000" } },
                new FontName() { Val = "Calibri" }),
            new Font(                                                               // Index 2 - The Times Roman font. with 16 size
                new FontSize() { Val = 16 },
                new Color() { Rgb = new HexBinaryValue() { Value = "000000" } },
                new FontName() { Val = "Times New Roman" }));

            var fills = new Fills();
            var fill = new Fill();
            var patternFill = new PatternFill
            {
                PatternType = PatternValues.None
            };
            fill.PatternFill = patternFill;
            fills.Append(fill);
            fill = new Fill();
            patternFill = new PatternFill
            {
                PatternType = PatternValues.Gray125
            };
            fill.PatternFill = patternFill;
            fills.Append(fill);
            fills.Count = (uint)fills.ChildElements.Count;

            var borders = new Borders(
                new Border(                                                         // Index 0 - The default border.
                    new LeftBorder(),
                    new RightBorder(),
                    new TopBorder(),
                    new BottomBorder(),
                    new DiagonalBorder()),
                new Border(                                                         // Index 1 - Applies a Left, Right, Top, Bottom border to a cell
                    new LeftBorder(
                        new Color() { Auto = true }
                    )
                    { Style = BorderStyleValues.Thin },
                    new RightBorder(
                    new Color() { Auto = true }
                    )
                    { Style = BorderStyleValues.Thin },
                    new TopBorder(
                        new Color() { Auto = true }
                    )
                    { Style = BorderStyleValues.Thin },
                    new BottomBorder(
                        new Color() { Auto = true }
                    )
                    { Style = BorderStyleValues.Thin },
                    new DiagonalBorder()));

            uint iExcelIndex = 164;
            var nfs = new NumberingFormats();
            var cfs = new CellFormats();
            // none, CellStyles.None (index = 0)
            var cf0 = new CellFormat
            {
                NumberFormatId = 0,
                FontId = 0,
                FillId = 0,
                BorderId = 0,
                FormatId = 0
            };
            cfs.Append(cf0);

            // background-grey, , CellStyles.BackgroundGrey (index = 1)
            var cf1 = new CellFormat
            {
                NumberFormatId = 0,
                FontId = 0,
                FillId = 1,
                BorderId = 0,
                FormatId = 0,
                ApplyFill = true
            };
            cfs.Append(cf1);

            // bold font, CellStyles.FontBold (index = 2)
            var cf2 = new CellFormat
            {
                NumberFormatId = 0,
                FontId = 1,
                FillId = 0,
                BorderId = 0,
                FormatId = 0,
                ApplyFont = true
            };
            cfs.Append(cf2);

            // header row: background-color: grey125, borders, CellStyles.Header (index = 3)
            var cf3 = new CellFormat
            {
                NumberFormatId = 0,
                FontId = 0,
                FillId = 1,
                BorderId = 1,
                FormatId = 0,
                ApplyFill = true,
                ApplyBorder = true
            };
            cfs.Append(cf3);

            var nf = new NumberingFormat
            {
                NumberFormatId = iExcelIndex++,
                FormatCode = "dd/mm/yyyy"
            };
            nfs.Append(nf);
            var cf4 = new CellFormat
            {
                NumberFormatId = nf.NumberFormatId,
                FontId = 0,
                FillId = 0,
                BorderId = 0,
                FormatId = 0,
                ApplyNumberFormat = true
            };
            cfs.Append(cf4);

            nf = new NumberingFormat
            {
                NumberFormatId = iExcelIndex++,
                FormatCode = "#,##0.0000"
            };
            nfs.Append(nf);
            var cf5 = new CellFormat
            {
                NumberFormatId = nf.NumberFormatId,
                FontId = 0,
                FillId = 0,
                BorderId = 0,
                FormatId = 0,
                ApplyNumberFormat = true
            };
            cfs.Append(cf5);

            // #,##0.00 is also Excel style index 4
            nf = new NumberingFormat
            {
                NumberFormatId = iExcelIndex++,
                FormatCode = "#,##0.00"
            };
            nfs.Append(nf);
            var cf6 = new CellFormat
            {
                NumberFormatId = nf.NumberFormatId,
                FontId = 0,
                FillId = 0,
                BorderId = 0,
                FormatId = 0,
                ApplyNumberFormat = true
            };
            cfs.Append(cf6);

            // @ is also Excel style index 49
            nf = new NumberingFormat
            {
                NumberFormatId = iExcelIndex++,
                FormatCode = "@"
            };
            nfs.Append(nf);
            var cf7 = new CellFormat
            {
                NumberFormatId = nf.NumberFormatId,
                FontId = 0,
                FillId = 0,
                BorderId = 0,
                FormatId = 0,
                ApplyNumberFormat = true
            };
            cfs.Append(cf7);

            nfs.Count = (uint)nfs.ChildElements.Count;
            cfs.Count = (uint)cfs.ChildElements.Count;

            ss.Append(nfs);
            ss.Append(fonts);
            ss.Append(fills);
            ss.Append(borders);
            ss.Append(cfs);

            var dfs = new DifferentialFormats
            {
                Count = 0
            };
            ss.Append(dfs);

            var tss = new TableStyles
            {
                Count = 0,
                DefaultTableStyle = "TableStyleMedium9",
                DefaultPivotStyle = "PivotStyleLight16"
            };
            ss.Append(tss);

            return ss;
        }

        private Row CreateRowWithData<TData>(TData data, OfficeDocumentOptions<TData> options) where TData : class
        {
            var dateType = typeof(DateTime);
            var nullableDateType = typeof(DateTime?);
            var dateOffsetType = typeof(DateTimeOffset);
            var nullableDateOffcetType = typeof(DateTimeOffset?);
            var charType = typeof(char);
            var intPtrType = typeof(IntPtr);
            var uintPtrType = typeof(UIntPtr);

            var newRow = new Row();
            foreach (var fieldConfig in options.Fields)
            {
                var cell = new Cell();
                var value = fieldConfig.PropertyInfo.GetValue(data);
                if (value == null)
                {
                    cell.DataType = CellValues.String;
                }
                else
                {
                    var type = fieldConfig.PropertyInfo.PropertyType;
                    if (type == dateType || type == nullableDateType)
                    {
                        var dateFormatter = fieldConfig.DateFormatter ?? options.DefaultDateFormatter;
                        var dateValue = (DateTime)value;
                        var valueString = dateValue.ToOADate().ToString(CultureInfo.InvariantCulture);
                        cell.DataType = new EnumValue<CellValues>(CellValues.Number); //CellValues.Date only for 2010 excel
                        cell.CellValue = new CellValue(valueString);
                        cell.StyleIndex = (UInt32Value)4U;
                    }
                    else if (type == dateOffsetType || type == nullableDateOffcetType)
                    {
                        var dateOffsetValue = (DateTimeOffset)value;
                        cell.CellValue = new CellValue(dateOffsetValue);
                    }
                    else if (type.IsPrimitive && type != charType && type != intPtrType && type != uintPtrType)
                    {
                        cell.DataType = CellValues.Number;
                        cell.CellValue = new CellValue(value.ToString());
                    }
                    else
                    {
                        cell.DataType = CellValues.String;
                        cell.CellValue = new CellValue(value.ToString());
                    }
                }
                newRow.AppendChild(cell);
            }

            return newRow;
        }

        //public IEnumerable<dynamic> Read(string filePath, string sheetName = null)
        //{
        //    var fileInfo = new FileInfo(filePath);
        //    if (!fileInfo.Exists)
        //    {
        //        throw new FileNotFoundException($"File {filePath} does not exist");
        //    }

        //    IEnumerable<dynamic> data = null;
        //    using (var reader = new StreamReader(filePath))
        //    {
        //        data = ReadInternal(reader.BaseStream);
        //    }

        //    // temporary
        //    return data;
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileStream"></param>
        /// <param name="sheetName"></param>
        /// <param name="columns">
        /// Dictionary with key - actual column name
        /// and value - target column name
        /// If not set, columns will be named as _ColumnName1, _ColumnName2 and so on
        /// </param>
        /// <returns></returns>
        //private IEnumerable<dynamic> ReadInternal(Stream fileStream, string sheetName = null, Dictionary<string, string> columns = null)
        //{
        //    var data = new List<dynamic>();
        //    using (var doc = SpreadsheetDocument.Open(fileStream, false))
        //    {
        //        //create the object for workbook part  
        //        var workbookPart = doc.WorkbookPart;
        //        var thesheetcollection = workbookPart.Workbook.GetFirstChild<Sheets>();
        //        var excelResult = new StringBuilder();

        //        var thesheet = string.IsNullOrWhiteSpace(sheetName) ? (Sheet)thesheetcollection.First() : (Sheet)thesheetcollection.First(x => ((Sheet)x).Name == sheetName);

        //        //using for each loop to get the sheet from the sheetcollection  
        //        //foreach (Sheet thesheet in thesheetcollection)
        //        //{
        //        //statement to get the worksheet object by using the sheet id  
        //        var theWorksheet = ((WorksheetPart)workbookPart.GetPartById(thesheet.Id)).Worksheet;

        //        var thesheetdata = theWorksheet.GetFirstChild<SheetData>();
        //        Dictionary<int, string> header = null;
        //        foreach (Row thecurrentrow in thesheetdata)
        //        {
        //            // continue while not find the header row
        //            if (header == null)
        //            {
        //                header = GetHeaderData(workbookPart, thecurrentrow, columns);
        //                continue;
        //            }

        //            var columnIndex = 0;
        //            DynamicHelperObject rowData = null;
        //            foreach (Cell thecurrentcell in thecurrentrow)
        //            {
        //                if (header.TryGetValue(columnIndex, out var columnName))
        //                {
        //                    if (rowData == null)
        //                    {
        //                        rowData = new DynamicHelperObject();
        //                    }
        //                    rowData.SetMember(columnName, GetCelValue(workbookPart, thecurrentcell));
        //                }
        //                columnIndex++;
        //            }

        //            if (rowData != null)
        //            {
        //                data.Add(rowData);
        //            }
        //        }
        //    }

        //    return data;
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileStream"></param>
        /// <param name="sheetName"></param>
        /// <param name="columns">
        /// Dictionary with key - actual column name
        /// and value - target column name
        /// If not set, columns will be named as _ColumnName1, _ColumnName2 and so on
        /// </param>
        /// <returns></returns>
        private DataTable ReadInternal(Stream fileStream, string sheetName, Dictionary<string, string> columns)
        {
            DataTable dataTable = null;
            using (var doc = SpreadsheetDocument.Open(fileStream, false))
            {
                //create the object for workbook part  
                var workbookPart = doc.WorkbookPart;
                var thesheetcollection = workbookPart.Workbook.GetFirstChild<Sheets>();
                var excelResult = new StringBuilder();

                var thesheet = string.IsNullOrWhiteSpace(sheetName) ? (Sheet)thesheetcollection.First() : (Sheet)thesheetcollection.First(x => ((Sheet)x).Name == sheetName);

                //using for each loop to get the sheet from the sheetcollection  
                //foreach (Sheet thesheet in thesheetcollection)
                //statement to get the worksheet object by using the sheet id  
                var theWorksheet = ((WorksheetPart)workbookPart.GetPartById(thesheet.Id)).Worksheet;

                var thesheetdata = theWorksheet.GetFirstChild<SheetData>();
                Dictionary<int, string> header = null;

                foreach (Row thecurrentrow in thesheetdata)
                {
                    DataRow dataRow = null;
                    // continue while not find the header row
                    if (header == null)
                    {
                        header = GetHeaderData(workbookPart, thecurrentrow, columns);
                        continue;
                    }
                    else if (dataTable == null)
                    {
                        dataTable = InitializeDataTableFromHeader(header);
                    }

                    dataRow = dataTable.NewRow();
                    dataTable.Rows.Add(dataRow);
                    var columnNumber = 0;
                    foreach (Cell thecurrentcell in thecurrentrow)
                    {
                        // column number starts from 1, that's why incrementing is done at the beginning
                        columnNumber++;
                        if (header.TryGetValue(columnNumber, out var columnName))
                        {
                            dataRow[columnName] = GetCelValue(workbookPart, thecurrentcell);
                        }
                    }
                }
            }

            return dataTable;
        }

        private DataTable InitializeDataTableFromHeader(Dictionary<int, string> header)
        {
            if (header == null)
            {
                return null;
            }

            var table = new DataTable();

            DataColumn column;

            foreach (var headerColumn in header)
            {
                // Create new DataColumn, set DataType, 
                // ColumnName and add to DataTable.    
                column = new DataColumn
                {
                    DataType = typeof(string),
                    ColumnName = headerColumn.Value,
                    ReadOnly = false,
                    Unique = false
                };
                // Add the Column to the DataColumnCollection.
                table.Columns.Add(column);
            }

            return table;
        }

        private Dictionary<int, string> GetHeaderData(WorkbookPart workbookPart, Row row, Dictionary<string, string> columns)
        {
            var columnNumber = 0;
            Dictionary<int, string> header = null;
            foreach (Cell thecurrentcell in row)
            {
                // column number starts from 1, that's why incrementing is done at the beginning
                columnNumber++;
                var cellValue = GetCelValue(workbookPart, thecurrentcell);
                if (!string.IsNullOrWhiteSpace(cellValue))
                {
                    if (header == null)
                    {
                        if (columns != null)
                        {
                            // different checks for given columns
                            if (columns.Where(kv => string.IsNullOrWhiteSpace(kv.Value)).Any())
                            {
                                throw new ArgumentException("Columns dictionary should contain legal names. Names should not be empty or whitespace");
                            }
                            if (columns.Values.Count() != columns.Values.Distinct().Count())
                            {
                                throw new ArgumentException("Columns dictionary should contain unique values");
                            }
                        }

                        header = new Dictionary<int, string>();
                    }

                    string columnName;
                    if (columns == null)
                    {
                        columnName = "_ColumnName" + columnNumber.ToString();
                    }
                    else
                    {
                        // if there is no such column in dictionary, do not add it to header
                        if (!columns.TryGetValue(cellValue, out columnName))
                        {
                            continue;
                        }
                    }

                    header.Add(columnNumber, columnName);
                }
            }

            if (header != null)
            {
                var duplicateColumns = header.GroupBy(kv => kv.Value).Select(g => new { ColumnName = g.Key, TotalCount = g.Count() }).Where(x => x.TotalCount > 1);
                if (duplicateColumns.Any())
                {
                    throw new InvalidOperationException("Columns got from file contain duplicate values: " + duplicateColumns.Aggregate("", (result, current) => result += " " + current.ColumnName));
                }
            }

            return header;
        }

        private string GetCelValue(WorkbookPart workbookPart, Cell cell)
        {
            var currentcellvalue = string.Empty;
            if (cell.DataType == null)
            {
                if (cell.CellValue?.Text != null)
                {
                    currentcellvalue = cell.CellValue.Text;
                }
            }
            else
            {
                if (cell.DataType == CellValues.SharedString)
                {
                    if (int.TryParse(cell.InnerText, out var id))
                    {
                        var item = workbookPart.SharedStringTablePart.SharedStringTable.Elements<SharedStringItem>().ElementAt(id);
                        if (item.Text != null)
                        {
                            currentcellvalue = item.Text.Text;
                        }
                        else if (item.InnerText != null)
                        {
                            currentcellvalue = item.InnerText;
                        }
                        else if (item.InnerXml != null)
                        {
                            currentcellvalue = item.InnerXml;
                        }
                    }
                }
                else if (cell.DataType == CellValues.Number)
                {
                    if (int.TryParse(cell.CellValue.Text, out var value))
                    {
                        currentcellvalue = value.ToString();
                    }
                }
                else if (cell.DataType == CellValues.Boolean)
                {
                    if (cell.InnerText == "1")
                    {
                        currentcellvalue = true.ToString();
                    }
                    else
                    {
                        currentcellvalue = false.ToString();
                    }
                }
                else if (cell.DataType == CellValues.Date)
                {
                    currentcellvalue = cell.InnerText;
                }
                else
                {
                    currentcellvalue = cell.InnerText;
                }
            }

            return currentcellvalue;
        }

        private string WriteExcelFile<TDto>(IEnumerable<TDto> data) where TDto : BaseDto
        {
            // Lets converts our object data to Datatable for a simplified logic.
            // Datatable is most easy way to deal with complex datatypes for easy reading and formatting. 
            var table = (DataTable)JsonSerializer.Deserialize(JsonSerializer.Serialize(data), (typeof(DataTable)));
            var fileName = "TestNewData.xlsx";
            using (var document = SpreadsheetDocument.Create(fileName, SpreadsheetDocumentType.Workbook))
            {
                var workbookPart = document.AddWorkbookPart();
                workbookPart.Workbook = new Workbook();

                var worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
                var sheetData = new SheetData();
                worksheetPart.Worksheet = new Worksheet(sheetData);

                var sheets = workbookPart.Workbook.AppendChild(new Sheets());
                var sheet = new Sheet() { Id = workbookPart.GetIdOfPart(worksheetPart), SheetId = 1, Name = "Sheet1" };

                sheets.Append(sheet);

                var headerRow = new Row();

                var columns = new List<string>();
                foreach (DataColumn column in table.Columns)
                {
                    columns.Add(column.ColumnName);

                    var cell = new Cell
                    {
                        DataType = CellValues.String,
                        CellValue = new CellValue(column.ColumnName)
                    };
                    headerRow.AppendChild(cell);
                }

                sheetData.AppendChild(headerRow);

                foreach (DataRow dsrow in table.Rows)
                {
                    var newRow = new Row();
                    foreach (var col in columns)
                    {
                        var cell = new Cell
                        {
                            DataType = CellValues.String,
                            CellValue = new CellValue(dsrow[col].ToString())
                        };
                        newRow.AppendChild(cell);
                    }

                    sheetData.AppendChild(newRow);
                }

                workbookPart.Workbook.Save();
            }

            return fileName;
        }

        #endregion Methods_Private

        #region Data_Private

        private enum CellStyles
        {
            None,
            BackgroundGrey,
            FontBold,
            Header
        }

        #endregion Data_Private
    }
}
