using FrontendMvc.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace FrontendMvc.Controllers
{
    public class AccountController : Controller
    {
        private readonly IConfiguration _Config;
        public AccountController(IConfiguration Config)
        {
            _Config = Config;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            try
            {
                if (model == null ||
                    string.IsNullOrWhiteSpace(model.Username) ||
                    string.IsNullOrWhiteSpace(model.Password))
                {
                    ViewBag.Error = "Invalid login details";
                    return View();
                }
                string apiurl = _Config["BackendApi:BaseUrl"] + "/api/Auth/Login";
                HttpClient client = new HttpClient();
                string jsondata = JsonSerializer.Serialize(model);
                StringContent content = new StringContent(jsondata, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(apiurl, content);

                if (!response.IsSuccessStatusCode)
                {
                    ViewBag.Error = "Invalid username or password";
                    return View();
                }

                string responsejson = await response.Content.ReadAsStringAsync();

                LoginResponseViewModel loginresponse = JsonSerializer.Deserialize<LoginResponseViewModel>(
                    responsejson, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    }
                    );

                HttpContext.Session.SetString("JWToken", loginresponse.Token);
                HttpContext.Session.SetString("UserRole", loginresponse.Role);
                return RedirectToAction("Index", "Dashboard");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Something went wrong";
                return View();
            }
            
        }
    }
}
