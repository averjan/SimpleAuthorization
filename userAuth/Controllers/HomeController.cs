using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using userAuth.Models;
using userAuth.ViewModels;

namespace userAuth.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        UserManager<User> _homeManager;
        List<TableViewModel> _table { get; set; }
        public HomeController(ILogger<HomeController> logger, UserManager<User> homeManager)
        {
            _logger = logger;
            _homeManager = homeManager;
            _table = new List<TableViewModel>();
            foreach (var us in homeManager.Users)
            {
                _table.Add(new TableViewModel(us));
            }
        }

        public IActionResult Index()
        {
            return View(_table);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public async Task<ActionResult> Delete(List<TableViewModel> model)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }

            foreach (var el in model)
            { 
                if (el.IsChecked)
                {
                    User user = await _homeManager.FindByNameAsync(el.UserName);
                    if (user != null)
                    {
                        await _homeManager.UpdateSecurityStampAsync(user);
                        await _homeManager.DeleteAsync(user);
                    }
                }
            }
            return RedirectToAction("Login", "Account");
        }
        [HttpPost]
        public async Task<ActionResult> Block(List<TableViewModel> model)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }

            foreach (var el in model)
            {
                User user = await _homeManager.FindByNameAsync(el.UserName);
                if (user != null)
                {
                    if (el.IsChecked)
                    {
                        user.Blocked = true;
                        await _homeManager.UpdateSecurityStampAsync(user);
                    }

                    await _homeManager.UpdateAsync(user);
                }
            }
            
            return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        public async Task<ActionResult> Unblock(List<TableViewModel> model)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }

            foreach (var el in model)
            {
                User user = await _homeManager.FindByNameAsync(el.UserName);
                if (user != null)
                {
                    if (el.IsChecked)
                    {
                        user.Blocked = false;
                    }

                    await _homeManager.UpdateAsync(user);
                }
            }

            return RedirectToAction("Index");
        }
    }
}
