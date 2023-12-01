using Microsoft.EntityFrameworkCore;
using SignalR.DataAccessLayer.Abstract;
using SignalR.DataAccessLayer.Concrete;
using SignalR.DataAccessLayer.Repositories;
using SignalR.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.DataAccessLayer.EntityFramework
{
    public class EfProductDal : GenericRepository<Product>, IProductDal
    {
        public EfProductDal(SignalRContext context) : base(context)
        {
        }

        public List<Product> GetProductsWithCategories()
        {
            var context = new SignalRContext();
            var value = context.Products.Include(x=>x.Category).ToList();
            return value;
        }

		public int ProductCount()
		{
            using var context = new SignalRContext();
            return context.Products.Count();
		}

		public int ProductCountByCategoryNameDrink()
		{
			using var context = new SignalRContext();
            return context.Products.Where(p => p.CategoryId.Equals(context.Categories.Where(c => c.CategoryName.Equals("İçecek")).Select(x => x.CategoryId).FirstOrDefault())).Count();
		}

		public int ProductCountByCategoryNameHamburger()
		{
			using var context = new SignalRContext();
            return context.Products.Where(p => p.CategoryId.Equals(context.Categories.Where(c => c.CategoryName.Equals("Hamburger")).Select(x => x.CategoryId).FirstOrDefault())).Count();
		}

		public decimal ProductPriceAvg()
		{
			using var context = new SignalRContext();
			return context.Products.Average(x => x.Price);
		}

		public string ProductNameByMaxPrice()
		{
			using var context = new SignalRContext();
			return context.Products.Where(x => x.Price.Equals(context.Products.Max(z => z.Price))).Select(t => t.ProductName).FirstOrDefault();
		}
		public string ProductNameByMinPrice()
		{
			using var context = new SignalRContext();
			return context.Products.Where(x => x.Price.Equals(context.Products.Min(z => z.Price))).Select(t => t.ProductName).FirstOrDefault();
		}

		public decimal ProductAvgPriceByHamburger()
		{
			using var context = new SignalRContext();
			return context.Products.Where(p => p.CategoryId.Equals(context.Categories.Where(q => q.CategoryName.Equals("Hamburger")).Select(z => z.CategoryId).FirstOrDefault())).Average(w => w.Price);
		}
	}
}
