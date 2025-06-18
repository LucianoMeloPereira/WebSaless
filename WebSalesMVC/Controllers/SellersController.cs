using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using WebSalesMVC.Models;
using WebSalesMVC.Models.ViewModels;
using WebSalesMVC.Services;
using WebSalesMVC.Services.Exceptions;

namespace WebSalesMVC.Controllers
{
    public class SellersController : Controller
    {
        private readonly SellerService _sellerService;
        private readonly DepartmentService _departmentService;

        public object ViewModelError { get; private set; }

        public SellersController(SellerService sellerService, DepartmentService departmentService)
        {
            _sellerService = sellerService;
            _departmentService = departmentService;
        }


        public async Task<IActionResult> Index()
        {
            var listFindAll = await _sellerService.FindAllAsync();
            return View(listFindAll);
        }

        public async Task<IActionResult> Create()
        {
            var departments = await _departmentService.FindAllAsync();
            var viewModel = new SellerFormViewModel { Departments = departments };


            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SellerFormViewModel obj)
        {

            if (!ModelState.IsValid)
            {
                return View(obj);
            }

            Seller selerObj = new Seller();
            selerObj = obj.Seller;
            _sellerService.Insert(selerObj);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id.Value == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            var obj = await _sellerService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            return View(obj);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Seller seller)
        {
            if (seller == null)
            {
                return NotFound();
            }


            await _sellerService.RemoveAsync(seller.Id);
            return RedirectToAction("Index");

        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }
            var seller = await _sellerService.FindByIdAsync(id.Value);

            if (seller == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            return View(seller);

        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            var obj = await _sellerService.FindByIdAsync(id.Value);

            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            List<Department> departments = await _departmentService.FindAllAsync();
            SellerFormViewModel seller = new SellerFormViewModel { Seller = obj, Departments = departments };

            return View(seller);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Seller seller)
        {
            var departments = await _departmentService.FindAllAsync();
            var viewModel = new SellerFormViewModel { Departments = departments, Seller = seller };

            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            if (id != seller.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Id miss mathed" });
            }

            try
            {
                await _sellerService.UpdateAsync(seller);
                return RedirectToAction("Index");

            }

            catch (NotFoundException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });

            }

            catch (DbConcurrencyException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });

            }

        }

        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };
            return View(viewModel);
        }
    }
}
