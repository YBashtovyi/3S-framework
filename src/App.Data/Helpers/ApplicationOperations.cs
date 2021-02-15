using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;

namespace App.Data.Helpers
{
    public static class ApplicationOperations
    {
        private static readonly List<ApplicationOperationData> _operations = new List<ApplicationOperationData>();

        [Display(Name = "Реєстр авторських сповіщень", Description = "Реєстр авторських сповіщень")]
        public static readonly string ViewAuthorNotificationList = "Реєстр авторських сповіщень";

        [Display(Name = "Реєстр отримувачів сповіщень", Description = "Реєстр отримувачів сповіщень")]
        public static readonly string ViewReceiverNotificationList = "Реєстр отримувачів сповіщень";

        [Display(Name = "Створення сповіщення", Description = "Створення сповіщення")]
        public static readonly string CreateNotification = "Створення сповіщення";

        [Display(Name = "Редагування сповіщення", Description = "Редагування сповіщення")]
        public static readonly string UpdateNotification = "Редагування сповіщення";

        [Display(Name = "Відправлення сповіщення", Description = "Відправлення сповіщення")]
        public static readonly string SendNotification = "Відправлення сповіщення";

        static ApplicationOperations()
        {
            var type = typeof(ApplicationOperations);
            var fields = type.GetFields(BindingFlags.Static | BindingFlags.Public);
            // iterating only public static string fields
            // CAUTION: do not add to class public static string fields that are not field operations
            foreach (var fieldInfo in fields.Where(x => x.FieldType == typeof(string)))
            {
                var operationData = new ApplicationOperationData
                {
                    OperationName = fieldInfo.Name
                };
                _operations.Add(operationData);

                if (!(fieldInfo.GetCustomAttributes(typeof(DisplayAttribute), false).FirstOrDefault() is DisplayAttribute displayAttr))
                {
                    operationData.OperationCaption = fieldInfo.GetValue(null)?.ToString();
                }
                else
                {
                    operationData.OperationCaption = displayAttr.Name;
                    operationData.OperationDescription = displayAttr.Description;
                }
            }
        }

        public static IEnumerable<ApplicationOperationData> GetDeclaredOperations() => _operations;

        public class ApplicationOperationData
        {
            public string OperationName { get; set; }
            public string OperationCaption { get; set; }
            public string OperationDescription { get; set; }
        }
    }
}
