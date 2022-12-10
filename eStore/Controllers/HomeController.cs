using BussinessObject.Models;
using eStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace eStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly SalesManagementContext _context;

        public HomeController(SalesManagementContext context)
        {
            _context = context;
        }

        public TblMember readJson()// đọc file Json
        {
            IConfiguration config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", true, false).Build();
            int memberid = int.Parse(config["DefaultUser:MemberId"]);
            string email = config["DefaultUser:Email"];
            string password = config["DefaultUser:Password"];
            string companyName = config["DefaultUser:CompanyName"];
            string city = config["DefaultUser:City"];
            string country = config["DefaultUser:Country"];
            TblMember ac = new TblMember(memberid, email, password, companyName, city, country); 
            return ac;
        }

        // GET: Login
        public async Task<IActionResult> Index()
        {
            return RedirectToAction("Login");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(TblMember model)
        {
            if (ModelState.IsValid)
            {
                TblMember defaultUser = readJson(); 
                var User = from m in _context.TblMembers select m;
                User = User.Where(s => s.Email.Contains(model.Email));
                if (User.Count() != 0)
                {
                    if (User.First().Password == model.Password)
                    {
                        return View("Homepage",model);
                    }
                }
                if (defaultUser != null)
                {
                    if (model.Email.Equals(defaultUser.Email) && model.Password.Equals(defaultUser.Password))
                    {
                        return View("Homepage", model);
                    }
                    else
                    {
                        return RedirectToAction("Fail");
                    }
                }
            }
            return RedirectToAction("Fail");
        }

        public IActionResult Fail()
        {
            return RedirectToAction("Index");
        }
    }
}

