using Core.Base.Data;
using BarcodeLib;
using System.IO;
using System.Drawing.Imaging;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace App.Business.Services.ApplicationServices
{
    /// <summary>
    /// 
    /// </summary>
    public interface IPrintedFormService
    {
        /// <summary>
        /// Prepare printed form 
        /// </summary>
        /// <typeparam name="TDto"></typeparam>
        /// <param name="template">form template</param>
        /// <param name="data">data for report</param>
        /// <param name="separator">separator between keyword in template</param>
        /// <returns>printed form</returns>
        string PreparePrintedFormAsync<TDto>(string template, TDto data, string separator = "#") where TDto : BaseDto;

        /// <summary>
        /// Get base 64 barcode in specified format
        /// </summary>
        /// <param name="text">input text data</param>
        /// <param name="type">Barcode type <see cref="TYPE"/></param>
        /// <param name="width">barcode width</param>
        /// <param name="height">barcode height</param>
        /// <returns></returns>
        string GetBase64Barcode(string text, TYPE type, int width, int height);
    }

    public class PrintedFormService: IPrintedFormService
    {
        #region methods: public
        ///  <inheritdoc />
        public string GetBase64Barcode(string text, TYPE type, int width, int height)
        {
            var barcode = new Barcode();
            using var memoryStream = new MemoryStream();
            var image = barcode.Encode(TYPE.CODE128, text, width, height);
            image.Save(memoryStream, ImageFormat.Png);
            return Convert.ToBase64String(memoryStream.ToArray());
        }

        ///  <inheritdoc />
        public string PreparePrintedFormAsync<TDto>(string template, TDto data, string separator = "#") where TDto : BaseDto
        {
            var type = data.GetType();
            var properties = type.GetProperties();
            foreach (var property in properties)
            {
                var propertyName = property.Name;
                var propertyValue = property.GetValue(data, null);
                var oldValue = $"{separator}{propertyName}{separator}"; 
                var newValue = GetPropertyValueAfterTypeChecks(property, propertyValue);
                template = template.Replace(oldValue, newValue);
            }
            return template;
        }
        #endregion

        #region methods: private
        private string GetPropertyValueAfterTypeChecks(PropertyInfo property, object propertyValue)
        {
            var newValue = propertyValue != null ? propertyValue.ToString() : "";

            if (typeof(DateTime).IsAssignableFrom(property.PropertyType) || typeof(DateTime?).IsAssignableFrom(property.PropertyType))
            {
                DateTime.TryParse(propertyValue.ToString(), out var date);
                var dataTypeAttribute = (DataTypeAttribute)property.GetCustomAttributes(typeof(DataTypeAttribute), true).FirstOrDefault();

                if (dataTypeAttribute != null && dataTypeAttribute.DataType == DataType.Date)
                {
                    newValue = date.ToString("dd.MM.yyyy");
                }
            }

            return newValue;
        }
        #endregion

    }
}
