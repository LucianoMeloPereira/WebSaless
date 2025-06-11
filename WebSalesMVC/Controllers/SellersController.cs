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
         
        public IActionResult Delete(int? id)
        {
            if(id.Value == null)
            {
                return NotFound();
            }

            var obj = _sellerService.FindById(id.Value);
            if(obj == null)
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
           var seller =  _sellerService.FindById(id.Value);

            return View(seller);

        }
    }
}
