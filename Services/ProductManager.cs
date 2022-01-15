using DataAccess;
using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ProductManager
    {
        private readonly ApplicationDbContext _context;
     public ProductManager(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Product> GetAll()
        {
            return _context.Products.Include(x=>x.Category).Where(x => !x.IsDeleted).OrderByDescending(x=>x.Id).ToList();
           
        }
        public Product GetById(int id)
        {
            var result = _context.Products.FirstOrDefault(x=>x.Id == id);


            return result;
        }
        public void Add(Product product)
        {
            product.PublishDate = DateTime.Now;
            _context.Products.Add(product);
            _context.SaveChanges(); 
        }
        public void Update(Product product)
        {
            _context.Products.Update(product);
            _context.SaveChanges();
        }
        public void Delete(Product product)
        {
            product.IsDeleted = true;
            _context.SaveChanges();
        }
    }
}
