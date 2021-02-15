using Core.Services.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using Xunit;

namespace Core.Tests
{
    public class XlsxServiceTest
    {
        private readonly Stream _stream;

        public XlsxServiceTest()
        {
            var filePath = "..\\..\\..\\Data\\XlsxReadingTestData.xlsx";
            var streamReader = new StreamReader(filePath);
            _stream = streamReader.BaseStream;
        }

        [Fact]
        public void Read_AllDataRead()
        {
            var parser = new XlsxService();
            var dataXlsx = parser.ReadStream(_stream);
            Assert.Equal(2621, dataXlsx.Rows.Count);
            Assert.Equal(14, dataXlsx.Columns.Count);
        }

        [Fact]
        public void CanWrite()
        {
            var parser = new XlsxService();
            var filePath = "..\\..\\..\\Data\\outputCanWriteTest.xlsx";
            var columns = new Dictionary<string, string>
            {
                {"Caption", "Заголовок" },
                {"Manufacturer","Виробник" }
            };

            var data = parser.GenerateStream<TestProductDetailDto>(new List<TestProductDetailDto> {
                new TestProductDetailDto {
                    Id = Guid.NewGuid(), Caption = "Test product",Manufacturer="factory"
                },
                new TestProductDetailDto {
                    Id = Guid.NewGuid(), Caption = "New product", Manufacturer="New factory"
                },
                 new TestProductDetailDto {
                    Id = Guid.NewGuid(), Caption = "Old product", Manufacturer="Old factory"
                }
            }, options => options.ConfigureFields(columns));

            File.WriteAllBytes(filePath, data);
            var fileInfo = new FileInfo(filePath);
            Assert.True(fileInfo.Exists);
        }

        [Fact]
        public void Read_SelectedSheet_AllDataRead()
        {
            var parser = new XlsxService();
            var dataXlsx = parser.ReadStream(_stream, "Test data");
            Assert.Equal(2621, dataXlsx.Rows.Count);
            Assert.Equal(14, dataXlsx.Columns.Count);
        }

        [Fact]
        public void Read_SelectedColumns_AllDataRead()
        {
            var columns = new Dictionary<string, string>
            {
                {"Симптом (рус)", "SymptomRu" },
                {"Симптом (укр)", "SymptomUa"},
                {"Связь симптома со специальностью", "SymptomSpecialities"},
                {"Діагноз (укр)", "DiagnosisUa"},
                {"Диагноз (рус)", "DiagnosisRu"},
                {"Код Синево", "SynevoCode"},
                {"Степень связи", "SymptomToDiagnosisPriority"},
                {"Локалізація (укр)", "LocationUa"},
                {"Локализация (рус)", "LocationRu"},
                {"Симптом ICPC", "SymptomIcpc"}
            };
            var parser = new XlsxService();
            var dataXlsx = parser.ReadStream(_stream, "Test data", columns);
            Assert.Equal(2621, dataXlsx.Rows.Count);
            Assert.Equal(10, dataXlsx.Columns.Count);

            foreach (var column in columns)
            {
                Assert.True(dataXlsx.Columns.Contains(column.Value));
            }

            foreach (DataColumn column in dataXlsx.Columns)
            {
                Assert.True(columns.ContainsValue(column.ColumnName));
            }
        }

        [Fact]
        public void Read_SelectedWrongResultColumns_Throws()
        {
            var columns = new Dictionary<string, string>
            {
                {"Симптом (рус)", "SymptomRu" },
                {"Симптом (укр)", "SymptomRu"}
            };

            var parser = new XlsxService();
            Assert.Throws<ArgumentException>(() => parser.ReadStream(_stream, "Test data", columns));
        }

        [Fact]
        public void Read_SelectedColumns_DataChecked()
        {
            var columns = new Dictionary<string, string>
            {
                {"Симптом (рус)", "SymptomRu" },
                {"Симптом (укр)", "SymptomUa"},
                {"Связь симптома со специальностью", "SymptomSpecialities"},
                {"Діагноз (укр)", "DiagnosisUa"},
                {"Диагноз (рус)", "DiagnosisRu"},
                {"Код Синево", "SynevoCode"},
                {"Степень связи", "SymptomToDiagnosisPriority"},
                {"Локалізація (укр)", "LocationUa"},
                {"Локализация (рус)", "LocationRu"},
                {"Специальность", "DiagnosisToSpeciality"},
                {"Симптом ICPC", "SymptomIcpc"}
            }; 
            var parser = new XlsxService();
            var dataXlsx = parser.ReadStream(_stream, "Test data", columns);

            // in file first row is a heder row
            // so data starts from second line
            // second line in file should be equal to dataXlsx.Rows[0]
            // line number 10 will be 8 index row and so on

            // first row
            Assert.Equal("Диагноз", dataXlsx.Rows[0]["SymptomRu"].ToString());
            Assert.Equal("пульсуюче відчуття у ділянці шлунка", dataXlsx.Rows[0]["SymptomUa"].ToString());
            Assert.Equal("неотложка", dataXlsx.Rows[0]["SymptomSpecialities"].ToString());
            Assert.Equal("", dataXlsx.Rows[0]["DiagnosisUa"].ToString());
            Assert.Equal("", dataXlsx.Rows[0]["DiagnosisRu"].ToString());
            Assert.Equal("", dataXlsx.Rows[0]["SynevoCode"].ToString());
            Assert.Equal("1", dataXlsx.Rows[0]["SymptomToDiagnosisPriority"].ToString());
            Assert.Equal("Живіт (повністю)", dataXlsx.Rows[0]["LocationUa"].ToString());
            Assert.Equal("Живот (полностью)", dataXlsx.Rows[0]["LocationRu"].ToString());
            Assert.Equal("хірург, неотложка", dataXlsx.Rows[0]["DiagnosisToSpeciality"].ToString());
            Assert.Equal("D29", dataXlsx.Rows[0]["SymptomIcpc"].ToString());

            // 2598, 2599 rows: "SymptomRu" and "SymptomUa" columns
            Assert.Equal("Рак матки", dataXlsx.Rows[2596]["SymptomRu"].ToString());
            Assert.Equal("підвищення температури", dataXlsx.Rows[2596]["SymptomUa"].ToString());
            Assert.Equal("Желтая лихорадка", dataXlsx.Rows[2597]["SymptomRu"].ToString());
            Assert.Equal("головний біль", dataXlsx.Rows[2597]["SymptomUa"].ToString());

            // 2601, 2602 rows: "SymptomSpecialities" column
            Assert.Equal("семейный врач, гастроэнтеролог, хирург, инфекционист, неотложка, кардиолог", dataXlsx.Rows[2599]["SymptomSpecialities"].ToString());
            Assert.Equal("", dataXlsx.Rows[2600]["SymptomSpecialities"].ToString());

            // 2572, 2573 rows: "DiagnosisUa" and "DiagnosisRu" columns
            Assert.Equal("Кашлюк/коклюш", dataXlsx.Rows[2570]["DiagnosisUa"].ToString());
            Assert.Equal("Коклюш/коклюш", dataXlsx.Rows[2570]["DiagnosisRu"].ToString());
            Assert.Equal("Рак нирки", dataXlsx.Rows[2571]["DiagnosisUa"].ToString());
            Assert.Equal("Рак почки", dataXlsx.Rows[2571]["DiagnosisRu"].ToString());

            // 2557, 2558 rows: "SynevoCode", "SymptomToDiagnosisPriority", "LocationUa", "LocationRu", "DiagnosisToSpeciality", "SymptomIcpc" columns
            Assert.Equal("нет"                  , dataXlsx.Rows[2555]["SynevoCode"].ToString());
            Assert.Equal("1"                    , dataXlsx.Rows[2555]["SymptomToDiagnosisPriority"].ToString());
            Assert.Equal("Тулуб (повністю)"     , dataXlsx.Rows[2555]["LocationUa"].ToString());
            Assert.Equal("Туловище (полностью)" , dataXlsx.Rows[2555]["LocationRu"].ToString());
            Assert.Equal("дерматовенеролог"     , dataXlsx.Rows[2555]["DiagnosisToSpeciality"].ToString());
            Assert.Equal("S79"                  , dataXlsx.Rows[2555]["SymptomIcpc"].ToString());
            Assert.Equal("очный визит"          , dataXlsx.Rows[2556]["SynevoCode"].ToString());
            Assert.Equal("1"                    , dataXlsx.Rows[2556]["SymptomToDiagnosisPriority"].ToString());
            Assert.Equal("Очі"                  , dataXlsx.Rows[2556]["LocationUa"].ToString());
            Assert.Equal("Око"                  , dataXlsx.Rows[2556]["LocationRu"].ToString());
            Assert.Equal("інфекціонист"         , dataXlsx.Rows[2556]["DiagnosisToSpeciality"].ToString());
            Assert.Equal("S79"                  , dataXlsx.Rows[2556]["SymptomIcpc"].ToString());
        }

        [Fact]
        public void Read_SelectedColumns_DifferentDataTypesLoaded()
        {
            var columns = new Dictionary<string, string>
            {
                {"FirstColumn", "FirstColumn"},
                {"SecondColumn", "SecondColumn"},
                {"ThirdColumn", "ThirdColumn"}
            };
            var parser = new XlsxService();
            var dataXlsx = parser.ReadStream(_stream, "Different values", columns);

            Assert.Equal(4, dataXlsx.Rows.Count);
            Assert.Equal("5", dataXlsx.Rows[0]["FirstColumn"].ToString());
            Assert.Equal("7", dataXlsx.Rows[0]["SecondColumn"].ToString());
            Assert.Equal("12", dataXlsx.Rows[0]["ThirdColumn"].ToString());
            Assert.Equal("true", dataXlsx.Rows[1]["ThirdColumn"].ToString());
            Assert.Equal("false", dataXlsx.Rows[2]["ThirdColumn"].ToString());
            Assert.Equal("12/31/1999", dataXlsx.Rows[3]["ThirdColumn"].ToString());
        }

    }
}
