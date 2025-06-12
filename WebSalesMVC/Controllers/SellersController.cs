using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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

        public SellersController(SellerService sellerService, DepartmentService departmentService)
        {
            _sellerService = sellerService;
            _departmentService = departmentService;
        }


        public IActionResult Index()
        {
            var listFindAll = _sellerService.FindAll();
            return View(listFindAll);
        }

        public IActionResult Create()
        {
            var departments = _departmentService.FindAll();
            var viewModel = new SellerFormViewModel { Departments = departments };


            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SellerFormViewModel obj)
        {

            Seller selerObj = new Seller();
            selerObj = obj.Seller;
            _sellerService.Insert(selerObj);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int? id)
        {
            if (id.Value == null)
            {
                return NotFound();
            }

            var obj = _sellerService.FindById(id.Value);
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        [HttpPost]
        public IActionResult Delete(Seller seller)
        {
            if (seller == null)
            {
                return NotFound();
            }


            _sellerService.Remove(seller.Id);
            return RedirectToAction("Index");

        }

        public IActionResult Details(int? id)
        {
            var seller = _sellerService.FindById(id.Value);

            return View(seller);

        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var obj = _sellerService.FindById(id.Value);

            if (obj == null)
            {
                return NotFound();
            }

            List<Department> departments = _departmentService.FindAll();
            SellerFormViewModel seller = new SellerFormViewModel { Seller = obj, Departments = departments };

            return View(seller);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Seller seller)
        {
            if(id != seller.Id)
            {
                return BadRequest();
            }

            try
            {
                _sellerService.Update(seller);
                return RedirectToAction("Index");
                     
            }

            catch (NotFoundException)
            {
                return NotFound();
            }

            catch(DbConcurrencyException)
            {
                return BadRequest();
            }

        }
    }
}
