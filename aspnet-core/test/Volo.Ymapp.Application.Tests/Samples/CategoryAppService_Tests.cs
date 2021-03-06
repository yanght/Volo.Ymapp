﻿using Shouldly;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Ymapp.Categorys;
using Volo.Ymapp.CommonEnum;
using Volo.Ymapp.Products;
using Xunit;

namespace Volo.Ymapp.Samples
{
    public class CategoryAppService_Tests : YmappApplicationTestBase
    {
        private readonly ICategoryAppService _categoryAppService;
        private readonly IProductPropertyAppService _propertyAppService;
        public CategoryAppService_Tests()
        {
            _categoryAppService = GetRequiredService<ICategoryAppService>();
            _propertyAppService = GetRequiredService<IProductPropertyAppService>();
        }

        [Fact]
        public async Task Initial_Data_Should_Contain_Admin_User()
        {
            //Act
            var result = await _categoryAppService.GetListAsync(new Abp.Application.Dtos.PagedAndSortedResultRequestDto() { SkipCount = 0, MaxResultCount = 10 });

            //Assert
            result.TotalCount.ShouldBeGreaterThan(0);
            result.Items.ShouldContain(u => u.Name == "admin");
        }

        [Fact]
        public async Task CreateCategory()
        {
            CreateCategoryDto input = new CreateCategoryDto()
            {
                Name = "最新资讯",
                ParentId = Guid.NewGuid(),
                Sort = 0,
                Type = (int)CategoryType.Artical
            };
            var result = await _categoryAppService.CreateAsync(input);

            result.Name.ShouldBe("最新资讯");
        }

        [Fact]
        public async Task GetCategoryList()
        {
            var result = await _categoryAppService.GetListAsync(new Abp.Application.Dtos.PagedAndSortedResultRequestDto()
            {
                MaxResultCount = 10,
                SkipCount = 0,
                Sorting = ""
            });
            result.Items.Count.ShouldBeGreaterThanOrEqualTo(0);

        }

        [Fact]
        public async Task CreatePropertyName()
        {
            await _propertyAppService.CreatePropertyName(new CreatePropertyNameDto()
            {
                CategoryId = 11,
                Title = "颜色"
            });
        }

    }
}
