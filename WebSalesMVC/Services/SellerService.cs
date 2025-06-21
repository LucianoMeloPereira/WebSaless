using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<List<Seller>> FindAllAsync()
        {
            return await _context.Seller.ToListAsync();

        }

        public async Task Insert(Seller obj)
        {
            if (obj == null)
            {
                throw new Exception("Error");
            }
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        public async Task<Seller> FindByIdAsync(int id)
        {
            return await _context.Seller.Include(dep => dep.Department).FirstOrDefaultAsync(obj => obj.Id == id);

        }

        public async Task RemoveAsync(int id)
        {
            try { 
            var obj = await _context.Seller.FindAsync(id);
            _context.Remove(obj);
            await _context.SaveChangesAsync();
            }

            catch (DbUpdateException e)
            {
                throw new IntegrityException("erro vendedor possui vendas");
            }
        }

        public async Task UpdateAsync(Seller obj)
        {
            if (!_context.Seller.Any(x => x.Id == obj.Id))
            {
                throw new NotFoundException("Id not found");
            }

            try
            {


                _context.Update(obj);
                await _context.SaveChangesAsync();
            }

            catch (DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }
    }
}
