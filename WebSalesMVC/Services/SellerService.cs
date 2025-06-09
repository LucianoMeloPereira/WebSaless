using System;
using System.Linq;
using WebSalesMVC.Data;
using WebSalesMVC.Models;

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
            if(obj == null)
            {
                throw new Exception("Error");
            }
            _context.Add(obj);
            _context.SaveChanges(); 
        }
    }
}
    