using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using WebSalesMVC.Data;
using WebSalesMVC.Models;
using WebSalesMVC.Services.Exceptions;

namespace WebSalesMVC.Services
{
    public class SellerService
    {
        private readonly WebSalesMVCContext _context;

        public SellerService(WebSalesMVCContext context)
        {
            _context = context;

        }

        public System.Collections.Generic.List<Seller> FindAll()
        {
            return _context.Seller.ToList();

        }

        public void Insert(Seller obj)
        {
            if (obj == null)
            {
                throw new Exception("Error");
            }
            _context.Add(obj);
            _context.SaveChanges();
        }

        public Seller FindById(int id)
        {
            return _context.Seller.Include(dep => dep.Department).FirstOrDefault(obj => obj.Id == id);

        }

        public void Remove(int id)
        {
            var obj = _context.Seller.Find(id);
            _context.Remove(obj);
            _context.SaveChanges();
        }

        public void Update(Seller obj)
        {
            if (!_context.Seller.Any(x => x.Id == obj.Id))
            {
                throw new NotFoundException("Id not found");
            }

            try
            {


                _context.Update(obj);
                _context.SaveChanges();
            }

            catch (DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }
    }
}
