using System.Collections.Generic;
using System.IO;
using Core.Base.Data;
//using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace App.DocumentTemplates.Models
{
    public static class DataSeeder
    {
        public static void Initialize(DtmDbContext context, IConfiguration configuration)
        {
            if (context.DocumentControlTypes.Any())
            {
                return;
            }

            SeedDocumentControlTypes(context);
            SeedDocumentTemplates(context, configuration, true);
        }

        public static void SeedDocumentControlTypes(DtmDbContext context, bool saveChanges = false)
        {
            if (!context.DocumentControlTypes.Any())
            {
                context.DocumentControlTypes.AddRange(
                    new DocumentControlType
                    {
                        Caption = "Логічне",
                        Code = "BIT",
                        RecordState = RecordState.Project
                    },
                    new DocumentControlType
                    {
                        Caption = "Варіант вибору",
                        Code = "CHECKLIST",
                        RecordState = RecordState.Project
                    },
                    new DocumentControlType
                    {
                        Caption = "Дата",
                        Code = "DATE",
                        RecordState = RecordState.Project
                    },
                    //new DocumentControlType
                    //{
                    //    Caption = "ICD",
                    //    Code = "ICD",
                    //    RecordState = RecordState.Project
                    //},
                    new DocumentControlType
                    {
                        Caption = "Лексичне дерево",
                        Code = "LEXTREE",
                        RecordState = RecordState.Project
                    },
                    //new DocumentControlType
                    //{
                    //    Caption = "Мультилукап",
                    //    Code = "MULTYDICT",
                    //    RecordState = RecordState.Project
                    //},
                    new DocumentControlType
                    {
                        Caption = "Число",
                        Code = "NUMBER",
                        RecordState = RecordState.Project
                    },
                    new DocumentControlType
                    {
                        Caption = "Сектор",
                        Code = "SECTOR",
                        RecordState = RecordState.Project
                    },
                    new DocumentControlType
                    {
                        Caption = "Текст",
                        Code = "TEXT",
                        RecordState = RecordState.Project
                    }
                    //new DocumentControlType
                    //{
                    //    Caption = "текстовый блок",
                    //    Code = "TEXTBLOCK",
                    //    RecordState = RecordState.Project
                    //},
                    //new DocumentControlType
                    //{
                    //    Caption = "таблиця",
                    //    Code = "SPREADSSHEET",
                    //    RecordState = RecordState.Project
                    //}

                );

                if (saveChanges)
                {
                    context.SaveChanges();
                }
            }
        }

        public static void SeedDocumentTemplates(DtmDbContext context, IConfiguration configuration, bool saveChanges = false)
        {
            var templateDirectoryPath = configuration.GetValue<string>("DtmSettings:TemplatesPath");
            var sourceTemplateForm001Path = Path.Combine(templateDirectoryPath, $"001_tm.docx");
            var sourceTemplateForm002Path = Path.Combine(templateDirectoryPath, $"002_tm.docx");

            #region DocumentTemplate

            // values taken from MODDI
            var templateDocument001 = new DocumentTemplate
            {
                Caption = "Запит на телемедичне консультування (ф.001/тм)",
                ClassShortCode = "T",
                Code = "001_tm",
                EntityName = "Consultation",
                TemplatePath = sourceTemplateForm001Path,
                TemplateElements = new List<DocumentTemplateElement>
                {
                    new DocumentTemplateElement
                    {
                        Config = "{\"showName\":\"block\",\"width\":12,\"inline\":false}",
                        Code = "111",
                        ControlTypeCode = "TEXT",
                        Caption = "Загальний анамнез та результати проведених досліджень",
                        OrderNumber = 1,
                        ValuesTree = new DocumentTemplateElementValueTree
                        {
                            Code = "111_ValuesTre",
                            Caption = "Загальний анамнез та результати проведених досліджень_ValuesTre"
                        }
                    },
                    new DocumentTemplateElement
                    {
                        Config = "{\"showName\":\"block\",\"width\":12,\"inline\":false}",
                        Code = "112",
                        ControlTypeCode = "TEXT",
                        Caption = "Діагноз",
                        OrderNumber = 2,
                        ValuesTree = new DocumentTemplateElementValueTree
                        {
                            Code = "112_ValuesTre",
                            Caption = "Діагноз_ValuesTre"
                        }
                    },
                    new DocumentTemplateElement
                    {
                        Config = "{\"showName\":\"block\",\"width\":12,\"inline\":false}",
                        Code = "113",
                        ControlTypeCode = "TEXT",
                        Caption = "Запитання до консультанта",
                        OrderNumber = 3,
                        ValuesTree = new DocumentTemplateElementValueTree
                        {
                            Code = "113_ValuesTre",
                            Caption = "Запитання до консультанта_ValuesTre"
                        }
                    },
                    new DocumentTemplateElement
                    {
                        Config = "{\"showName\":\"block\",\"width\":12,\"inline\":false}",
                        Code = "114",
                        ControlTypeCode = "BIT",
                        Caption = "Статус (ургентний)",
                        OrderNumber = 4
                    }
                }
            };

            var templateDocument002 = new DocumentTemplate
            {
                Caption = "Висновок консультанта(ф.002/тм)",
                ClassShortCode = "T",
                Code = "002_tm",
                EntityName = "Consultation",
                TemplatePath = sourceTemplateForm002Path,
                TemplateElements = new List<DocumentTemplateElement>
                {
                    new DocumentTemplateElement
                    {
                        Config = "{\"showName\":\"block\",\"width\":12,\"inline\":false}",
                        Code = "111",
                        ControlTypeCode = "TEXT",
                        Caption = "Найменування закладу охорони здоров’я, який направив пацієнта на консультацію",
                        OrderNumber = 1,
                        ValuesTree = new DocumentTemplateElementValueTree
                        {
                            Code = "111_ValuesTre",
                            Caption = "Найменування закладу охорони здоров’я, який направив пацієнта на консультацію_ValuesTre"
                        }
                    },
                    new DocumentTemplateElement
                    {
                        Config = "{\"showName\":\"block\",\"width\":12,\"inline\":false}",
                        Code = "112",
                        ControlTypeCode = "TEXT",
                        Caption = "Прізвище, ім’я, по батькові пацієнта",
                        OrderNumber = 2,
                        ValuesTree = new DocumentTemplateElementValueTree
                        {
                            Code = "112_ValuesTre",
                            Caption = "Прізвище, ім’я, по батькові пацієнта_ValuesTre"
                        }
                    },
                    new DocumentTemplateElement
                    {
                        Config = "{\"showName\":\"block\",\"width\":12,\"inline\":false}",
                        Code = "113",
                        ControlTypeCode = "TEXT",
                        Caption = "Висновок",
                        OrderNumber = 3,
                        ValuesTree = new DocumentTemplateElementValueTree
                        {
                            Code = "113_ValuesTre",
                            Caption = "Висновок_ValuesTre"
                        }
                    },
                    new DocumentTemplateElement
                    {
                        Config = "{\"showName\":\"block\",\"width\":12,\"inline\":false}",
                        Code = "114",
                        ControlTypeCode = "TEXT",
                        Caption = "Рекомендації",
                        OrderNumber = 4,
                        ValuesTree = new DocumentTemplateElementValueTree
                        {
                            Code = "114_ValuesTre",
                            Caption = "Рекомендації_ValuesTre"
                        }
                    },
                    new DocumentTemplateElement
                    {
                        Config = "{\"required\":true,\"showName\":\"block\",\"width\":12,\"inline\":false}",
                        Code = "Nosology_stat",
                        ControlTypeCode = "CHECKLIST",
                        Caption = "Нозологічна група (з приводу якої було звернення)",
                        OrderNumber = 5,
                        ValuesTree = new DocumentTemplateElementValueTree
                        {
                            Code = "Nosology_stat_ValuesTre",
                            Caption = "Нозологічна група (з приводу якої було звернення)_ValuesTre",
                            TemplateElementValues = new List<DocumentTemplateElementValue>
                            {
                                new DocumentTemplateElementValue
                                {
                                    Caption = "Кардіологія",
                                    ContentValue = "Кардіологія",
                                    OrderNumber = 0,
                                    ValueTypeCode = "L"
                                },
                                new DocumentTemplateElementValue
                                {
                                    Caption = "Ендокринологія",
                                    ContentValue = "Ендокринологія",
                                    OrderNumber = 0,
                                    ValueTypeCode = "L"
                                },
                                new DocumentTemplateElementValue
                                {
                                    Caption = "Пульмонологія",
                                    ContentValue = "Пульмонологія",
                                    OrderNumber = 0,
                                    ValueTypeCode = "L"
                                },
                                new DocumentTemplateElementValue
                                {
                                    Caption = "Дерматологія",
                                    ContentValue = "Дерматологія",
                                    OrderNumber = 0,
                                    ValueTypeCode = "L"
                                },
                                new DocumentTemplateElementValue
                                {
                                    Caption = "Онкологія",
                                    ContentValue = "Онкологія",
                                    OrderNumber = 0,
                                    ValueTypeCode = "L"
                                },
                                new DocumentTemplateElementValue
                                {
                                    Caption = "Туберкульоз",
                                    ContentValue = "Туберкульоз",
                                    OrderNumber = 0,
                                    ValueTypeCode = "L"
                                },
                                new DocumentTemplateElementValue
                                {
                                    Caption = "Педіатрія",
                                    ContentValue = "Педіатрія",
                                    OrderNumber = 0,
                                    ValueTypeCode = "L"
                                },
                                new DocumentTemplateElementValue
                                {
                                    Caption = "Інфекційні хвороби (діти)",
                                    ContentValue = "Інфекційні хвороби (діти)",
                                    OrderNumber = 0,
                                    ValueTypeCode = "L"
                                },
                                new DocumentTemplateElementValue
                                {
                                    Caption = "Інфекційні хвороби (дорослі)",
                                    ContentValue = "Інфекційні хвороби (дорослі)",
                                    OrderNumber = 0,
                                    ValueTypeCode = "L"
                                }

                            }
                        }
                    }
                }
            };

            context.DocumentTemplates.AddRange(templateDocument001, templateDocument002);

            #endregion DocumentTemplate

            if (saveChanges)
            {
                context.SaveChanges();
            }
        }
    }
}
