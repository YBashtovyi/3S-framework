using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Business.ViewModels;
using App.Data.Dto.Cdn;
using App.Data.Dto.Common;
using App.Data.Dto.Common.NotMapped;
using App.Data.Models;
using Core.Base.Exceptions;
using Core.Services.Data;
using Newtonsoft.Json;

namespace App.Business.Services.ConstructionObjectServices
{
    public class ConstructionObjectService
    {
        public ConstructionObjectService(ICommonDataService dataService)
        {
            _dataService = dataService;
        }

        private readonly ICommonDataService _dataService;

        #region PublicMethods

        public async Task ChangeObjectStatus(Guid id, string status)
        {
            var constObject = await GetConstructionObjectById(id);

            switch (status)
            {
                case Constants.ObjectStatus.Active:
                    if (constObject.ObjectStatus == Constants.ObjectStatus.Project)
                    {
                        constObject.ObjectStatus = Constants.ObjectStatus.Active;
                    }
                    break;
                case Constants.ObjectStatus.Project:
                {
                    if (constObject.ObjectStatus == Constants.ObjectStatus.Active)
                    {
                        constObject.ObjectStatus = Constants.ObjectStatus.Project;
                    }
                }
                    break;
                case Constants.ObjectStatus.NotActive:
                {
                    if (constObject.ObjectStatus == Constants.ObjectStatus.Active)
                    {
                        constObject.ObjectStatus = Constants.ObjectStatus.NotActive;
                    }
                }
                    break;
                default: throw new AppException("Невідомий статус");
            }

            _dataService.AddDto<ConstructionObject>(constObject, true);
            await _dataService.SaveChangesAsync();
        }

        public async Task AddConstructionObjectExtendedProperty(Guid id, ConstructionObjectExtendedPropertyViewModel model)
        {
            // check if exists ConstructionObject
            await GetConstructionObjectById(id);

            var extendedProperty = new ConstructionObjectExtendedProperty
            {
                ConstructionObjectId = model.ConstructionObjectId,
                DictionaryId = model.DictionaryId,
                Value = model.Value,
                ValueJson = JsonConvert.SerializeObject(new { constructionObjectExPropertyId = model.ConstructionObjectExPropertyId, constructionObjectSubExPropertyId = model.ConstructionObjectSubExPropertyId, description = model.Description})
            };

            _dataService.Add(extendedProperty, false);
            await _dataService.SaveChangesAsync();
        }

        #region AtuCoordinate

        public async Task<MapCoordinate> AddCoordinate(Guid objId, MapCoordinate coordinate)
        {
            var constObject = await GetConstructionObjectById(objId);

            if (coordinate.Lines == null && coordinate.Polygons == null && coordinate.Markers == null)
            {
                constObject.AtuCoordinates = string.Empty;
            }
            else
            {
                constObject.AtuCoordinates = JsonConvert.SerializeObject(coordinate);
            }

            _dataService.AddDto<ConstructionObject>(constObject, true);
            await _dataService.SaveChangesAsync();

            return coordinate;
        }

        public async Task<MapCoordinate> GetCoordinates(Guid prjId)
        {
            var constObject = await GetConstructionObjectById(prjId);

            if (string.IsNullOrEmpty(constObject.AtuCoordinates))
            {
                return new MapCoordinate();
            }

            var coordinates = JsonConvert.DeserializeObject<MapCoordinate>(constObject.AtuCoordinates);

            return coordinates;
        }

        #endregion

        #endregion

        #region PrivateMethods

        private async Task<ConstructionObjectDetailsDto> GetConstructionObjectById(Guid id)
        {
            var constObject = await _dataService.SingleOrDefaultAsync<ConstructionObjectDetailsDto>(p => p.Id == id);
            if (constObject == null)
            {
                throw new AppException("Об'єкт не знайдений");
            }

            return constObject;
        }

        #endregion

    }
}
