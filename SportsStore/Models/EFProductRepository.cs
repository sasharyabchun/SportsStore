using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models
{
    public class EFProductRepository : IProductRepository
    {
        private ApplicationDbContext _context;

        public EFProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Product> Products => _context.Products;

        public Product DeleteProduct(int prodcutID)
        {
            Product dbEntity = _context.Products.FirstOrDefault(p => p.ProductID == prodcutID);

            if (dbEntity != null)
            {
                _context.Products.Remove(dbEntity);
                _context.SaveChanges();
            }

            return dbEntity;
        }

        public void SaveProduct(Product product)
        {
            if (product.ProductID == 0)
            {
                _context.Products.Add(product);
            }
            else
            {
                Product dbEntity = _context.Products.FirstOrDefault(p => p.ProductID == product.ProductID);
                if (dbEntity != null)
                {
                    dbEntity.Name = product.Name;
                    dbEntity.Description = product.Description;
                    dbEntity.Price = product.Price;
                    dbEntity.Category = product.Category;
                }
            }
            _context.SaveChanges();
        }
    }
}
