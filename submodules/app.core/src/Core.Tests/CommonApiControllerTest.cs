using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Mvc.Controllers;
using Core.Services.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Core.Tests
{
    public class CommonApiControllerTest
    {
        [Fact]
        public async Task GetItems_Default_AllPresent()
        {
            var orderBy = "";
            IDictionary<string, string> otherParameters = new Dictionary<string, string>();
            var skip = 0;
            var pageSize = 0;
            var cacheResultDuration = 0;

            var dto = GetItems();

            var mock = new Mock<ICommonDataService>();
            mock.Setup(x => x.GetDtoAsync<TestProductDto>(orderBy, null, otherParameters, skip, pageSize, cacheResultDuration, null, null))
                .ReturnsAsync(dto);

            var controller = new CommonApiController<TestProductDetailDto, TestProductDto, TestProduct>(mock.Object, new Logger<CommonApiController>(new LoggerFactory()));
            var result = await controller.GetItems(null);

            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);

            Assert.IsAssignableFrom<IEnumerable<TestProductDto>>((result as OkObjectResult).Value);

            var resultModelCollection = Enumerable.Empty<TestProductDto>();
            if (result is OkObjectResult okResult && okResult.Value is IEnumerable<TestProductDto> values)
            {
                resultModelCollection = values;
            }

            Assert.Equal(resultModelCollection, dto);
        }

        [Fact]
        public async Task GetItemsExt_Default_AllPresent()
        {
            var orderBy = "";
            IDictionary<string, string> otherParameters = new Dictionary<string, string>();
            var skip = 0;
            var pageSize = 0;
            var cacheResultDuration = 0;

            var dto = GetItemsExt();

            var mock = new Mock<ICommonDataService>();
            mock.Setup(x => x.GetDtoAsync<TestProductDetailDto>(orderBy, null, otherParameters, skip, pageSize, cacheResultDuration, null, null))
                .ReturnsAsync(dto);

            var controller = new CommonApiController<TestProductDetailDto, TestProductDto, TestProduct>(mock.Object, new Logger<CommonApiController>(new LoggerFactory()));
            var result = await controller.GetItemsExt(null);

            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);

            Assert.IsAssignableFrom<IEnumerable<TestProductDetailDto>>((result as OkObjectResult).Value);

            var resultModelCollection = Enumerable.Empty<TestProductDetailDto>();
            if (result is OkObjectResult okResult && okResult.Value is IEnumerable<TestProductDetailDto> values)
            {
                resultModelCollection = values;
            }

            Assert.Equal(resultModelCollection, dto);
        }

        private IEnumerable<TestProductDto> GetItems()
        {
            return new TestProductDto[] {
                new TestProductDto
                {
                    Id = Guid.NewGuid(),
                    Caption = "Chair",
                    Price = 213.5M
                },
                new TestProductDto
                {
                    Id = Guid.NewGuid(),
                    Caption = "Table",
                    Price = 1560M
                },
                new TestProductDto
                {
                    Id = Guid.NewGuid(),
                    Caption = "Armchair",
                    Price = 3250M
                }
            };
        }

        private IEnumerable<TestProductDetailDto> GetItemsExt()
        {
            return new TestProductDetailDto[] {
                new TestProductDetailDto
                {
                    Id = Guid.NewGuid(),
                    Caption = "Chair",
                    Price = 213.5M,
                    Manufacturer = "Chairs and Co"
                },
                new TestProductDetailDto
                {
                    Id = Guid.NewGuid(),
                    Caption = "Table",
                    Price = 1560M,
                    Manufacturer = "Tables and Ko"
                },
                new TestProductDetailDto
                {
                    Id = Guid.NewGuid(),
                    Caption = "Armchair",
                    Price = 3250M,
                    Manufacturer = "Just armchairs"
                }
            };
        }
    }
}
