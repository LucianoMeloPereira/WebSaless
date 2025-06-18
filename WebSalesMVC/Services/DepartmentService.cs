using WebSalesMVC.Data;
using System.Linq;
using System.Collections.Generic;
using WebSalesMVC.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WebSalesMVC.Services
{
    public class DepartmentService
    {
        private readonly WebSalesMVCContext _context;

        public DepartmentService(WebSalesMVCContext context)
        {
            _context = context;
        }

        public async Task<List<Department>>  FindAllAsync()
        {
            return await _context.Department.OrderBy(x => x.Name).ToListAsync();
        }
    }
}
