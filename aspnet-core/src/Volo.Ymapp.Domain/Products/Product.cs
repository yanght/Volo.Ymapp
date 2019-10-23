using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.ObjectMapping;
using Volo.Ymapp.Areas;
using Volo.Ymapp.Categorys;
using Volo.Ymapp.CommonEnum;
using Volo.Ymapp.Products;

namespace Volo.Ymapp.Products
{
    public class Product : FullAuditedAggregateRoot<Guid>
    {
        public Product(string name, string code, Guid categoryId, string description)
        {
            ProductPictures = new List<ProductPicture>();
            ProductAreas = new List<ProductArea>();
            Name = name;
            Code = code;
            CategoryId = categoryId;
            Description = description;
        }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 商品编码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 分类编码
        /// </summary>
        public Guid CategoryId { get; set; }
        /// <summary>
        /// 分类
        /// </summary>
        public virtual Category Category { get; set; }
        /// <summary>
        /// 商品状态
        /// </summary>
        public ProductState State { get; set; }
        /// <summary>
        /// 商品描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 商品图片
        /// </summary>
        public virtual List<ProductPicture> ProductPictures { get; protected set; }
        /// <summary>
        /// 商品区域
        /// </summary>
        public virtual List<ProductArea> ProductAreas { get; protected set; }

        public Product SetName([NotNull]string name)
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));
            Name = name;
            return this;
        }

        /// <summary>
        /// 设置商品图片
        /// </summary>
        /// <param name="pictures"></param>
        /// <returns></returns>
        public Product SetProductPictures(List<string> pictures)
        {
            if (pictures == null || pictures.Count == 0)
            {
                return this;
            }
            int index = 0;
            foreach (var item in pictures)
            {
                ProductPictureType type = ProductPictureType.Normal;

                if (index == 0)
                {
                    type = ProductPictureType.MainPicture;
                }
                ProductPictures.Add(new ProductPicture()
                {
                    Type = type,
                    ProductId = this.Id,
                    PictureUrl = item,
                    Sort = index
                });
                index++;
            }
            return this;
        }

        /// <summary>
        /// 设置商品区域
        /// </summary>
        /// <param name="areas"></param>
        /// <returns></returns>
        public Product SetProductAreas(List<Guid> areas)
        {
            if (areas == null || areas.Count == 0)
            {
                return this;
            }
            foreach (var item in areas)
            {
                ProductAreas.Add(new ProductArea()
                {
                    AreaId = item,
                    ProductId = this.Id
                });
            }
            return this;
        }
        
    }
}
