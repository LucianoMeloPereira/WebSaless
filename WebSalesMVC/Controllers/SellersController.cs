using Microsoft.AspNetCore.Mvc;
using WebSalesMVC.Models;
using WebSalesMVC.Models.ViewModels;
using WebSalesMVC.Services;

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
         
    }
}
