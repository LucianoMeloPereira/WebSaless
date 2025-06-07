using Microsoft.AspNetCore.Mvc;
using WebSalesMVC.Models;
using WebSalesMVC.Services;

namespace WebSalesMVC.Controllers
{
    public class SellersController : Controller
    {
        private readonly SellerService _sellerService;

        public SellersController(SellerService sellerService)
        {
            _sellerService = sellerService;
        }


        public IActionResult Index()
        {
            var listFindAll = _sellerService.FindAll();
            return View(listFindAll);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Seller obj)
        {
            _sellerService.Insert(obj);
            return RedirectToAction(nameof(Index));
        }
         
    }
}
