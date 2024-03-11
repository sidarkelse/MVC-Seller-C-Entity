using Microsoft.AspNetCore.Mvc;
using Mvc.Models;
using Mvc.Models.ViewModels;
using Mvc.Services;
using Mvc.Services.Exceptions;
using System.Diagnostics;

namespace Mvc.Controllers
{
    public class SellersController : Controller
    {


        private readonly SellerService _sellerService;
        private readonly DepartmentService _departmentService;
        public SellersController(SellerService sellerService, DepartmentService departmentService) //injeção de dependencia
        {
            _sellerService = sellerService;
            _departmentService = departmentService;
        }


        public async Task <IActionResult> Index()
        {
            var list = await _sellerService.FindAllAsync(); //vai retornar uma lista de seller
            return View(list); // vai retornar os dados para view 
        }

        public async Task <IActionResult> Create()
        {
            var departments =  await _departmentService.FindAllAsync();
            var viewModel =  new SellerFormViewModel { Departments = departments }; 

            return View(viewModel); // quando for acessar essa viewModel, já vai receber ela com os departments Populados
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> Create(Seller seller)
        {
            if (!ModelState.IsValid)//se não for valido
            {
                var departments = await _departmentService.FindAllAsync();
                var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
                return View(viewModel);
            }
           await _sellerService.InsertAsync(seller);
            return RedirectToAction(nameof(Index));

        }

        public  async Task <IActionResult> Delete(int? id)
        {
            if(id == null)// se for nulo a requisição foi feita de forma indevida/errada
            {
                return RedirectToAction(nameof(Error), new {message = "Id not provided"}); //id não foi fornecido

            }
            var obj =  await _sellerService.FindByIdAsync(id.Value);
            if( obj == null) // se esse Id também não existir vai retornar um notfound
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" }); //id nulo - não encontrado
            }

            return View(obj); // se deu certo, retornara só a view
        
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> Delete(int id )
        {
            try
            {
                await _sellerService.Remove(id);
                return RedirectToAction(nameof(Index));
            }
            catch(IntegrityException ex )
            {
                return RedirectToAction(nameof(Error), new { message = ex.Message }); 

            }
     
        }

        public async Task <IActionResult> Details (int? id)
        {
            if (id == null)// se for nulo a requisição foi feita de forma indevida/errada
            {
                return RedirectToAction(nameof(Error), new { message = "Id not Provided" }); // caso não exista Id

            }
            var obj = await _sellerService.FindByIdAsync(id.Value);
            if (obj == null) // se esse Id também não existir vai retornar um notfound
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });

            }

            return View(obj); // se deu certo, retornara só a view

        }
        public  async Task  <IActionResult> Edit(int? id)
        {
            if (id == null) 
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            var obj = await _sellerService.FindByIdAsync(id.Value);
            if(obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            List<Department> departments = await _departmentService.FindAllAsync();
            SellerFormViewModel viewModel = new SellerFormViewModel { Seller = obj, Departments = departments };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task  <IActionResult> Edit(int id, Seller seller)
        {
            if (!ModelState.IsValid)//se não for valido
            {
                var departments = await _departmentService.FindAllAsync();
                var viewModel = new SellerFormViewModel {Seller = seller, Departments = departments };
                return View(viewModel);
            }
            


            if(id != seller.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Id missmatch" }); // id não compativel

            }
            try
            {
               await  _sellerService.Update(seller);
                return RedirectToAction(nameof(Index));

            }
            catch (NotFoundException ex)
            {

                return RedirectToAction(nameof(Error), new { message = ex.Message });

            }
            catch (DbConcurrencyException ex)
            {
                return RedirectToAction(nameof(Error), new { message = ex.Message });

            }
            //--- as 2 exceções podem ser feitas por um aplicationExpection <->



        }
        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier //pegar Id interno da requisição


            };
            return View(viewModel);



        }
    }
}
