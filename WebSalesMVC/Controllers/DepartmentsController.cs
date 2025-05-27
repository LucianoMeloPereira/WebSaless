using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebSalesMVC.Data;
using WebSalesMVC.Models;

namespace WebSalesMVC.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly WebSalesMVCContext _context;

        public DepartmentsController(WebSalesMVCContext context)
        {
            _context = context;
        }

        // GET: Deparments
        public async Task<IActionResult> Index()
        {
            return View(await _context.Deparment.ToListAsync());
        }

        // GET: Deparments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deparment = await _context.Deparment
                .FirstOrDefaultAsync(m => m.Id == id);
            if (deparment == null)
            {
                return NotFound();
            }

            return View(deparment);
        }

        // GET: Deparments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Deparments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Deparment deparment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(deparment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(deparment);
        }

        // GET: Deparments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deparment = await _context.Deparment.FindAsync(id);
            if (deparment == null)
            {
                return NotFound();
            }
            return View(deparment);
        }

        // POST: Deparments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Deparment deparment)
        {
            if (id != deparment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(deparment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DeparmentExists(deparment.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(deparment);
        }

        // GET: Deparments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deparment = await _context.Deparment
                .FirstOrDefaultAsync(m => m.Id == id);
            if (deparment == null)
            {
                return NotFound();
            }

            return View(deparment);
        }

        // POST: Deparments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var deparment = await _context.Deparment.FindAsync(id);
            _context.Deparment.Remove(deparment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DeparmentExists(int id)
        {
            return _context.Deparment.Any(e => e.Id == id);
        }
    }
}
