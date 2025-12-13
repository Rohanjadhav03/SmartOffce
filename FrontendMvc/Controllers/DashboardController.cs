using FrontendMvc.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FrontendMvc.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IConfiguration _Config;
        public DashboardController(IConfiguration Config)
        {
            _Config = Config;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                string token = HttpContext.Session.GetString("JWToken");
                if (string.IsNullOrEmpty(token))
                {
                    return RedirectToAction("Login", "Account");
                }
                string apiurl = _Config["BackendApi:BaseUrl"] + "/api/employee/get-all";
                HttpClient client = new HttpClient();

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage response = await client.GetAsync(apiurl);
                if (!response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Login", "Account");
                }

                string jsondata = await response.Content.ReadAsStringAsync();

                List<EmployeeViewModel> employees =
                        JsonSerializer.Deserialize<List<EmployeeViewModel>>(
                            jsondata,
                            new JsonSerializerOptions
                            {
                                PropertyNameCaseInsensitive = true
                            });


                return View(employees);
            }
            catch (Exception)
            {
                return View(new List<EmployeeViewModel>());
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            try
            {
                if (id<=0)
                {
                    return RedirectToAction("Index");
                }
                string token = HttpContext.Session.GetString("JWToken");
                if (token==null)
                {
                    return RedirectToAction("Login", "Account");
                }

                string apiurl = _Config["BackendApi:BaseUrl"] + "/api/employee/get-by-id/" + id;

                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage response = await client.GetAsync(apiurl);
                if (!response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                string jsondata = await response.Content.ReadAsStringAsync();

                EmployeeViewModel employees = JsonSerializer.Deserialize<EmployeeViewModel>(jsondata, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                return View(employees);
            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public IActionResult Add()
        {
            string token = HttpContext.Session.GetString("JWToken");

            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Account");
            }

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(EmployeeViewModel model)
        {
            try
            {
                string token = HttpContext.Session.GetString("JWToken");
                if (string.IsNullOrEmpty(token))
                {
                    return RedirectToAction("Login", "Account");
                }
                if (model==null||string.IsNullOrWhiteSpace(model.EmployeeName)|| string.IsNullOrWhiteSpace(model.Email))
                {
                    ViewBag.Error = "Invalid employee data";
                    return View(model);
                }
                string apiurl = _Config["BackendApi:BaseUrl"] + "/api/employee/add";

                HttpClient client = new HttpClient();

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Barer", token);
                string jsondata = JsonSerializer.Serialize(model);
                StringContent content = new StringContent(jsondata, Encoding.UTF8,"application/Json");
                HttpResponseMessage response = await client.PostAsync(apiurl, content);
                if (!response.IsSuccessStatusCode)
                {
                    ViewBag.Error = "Failed To Add Employee";
                    return View(model);
                }
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                ViewBag.Error = "Something went wrong";
                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return RedirectToAction("Index");
                }
                string token = HttpContext.Session.GetString("JWToken");
                if (string.IsNullOrWhiteSpace(token))
                {
                    return RedirectToAction("Login", "Account");
                }

                string apiurl = _Config["BackendApi:BaseUrl"] + "/api/employee/get-by-id/" + id;

                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage response = await client.GetAsync(apiurl);
                if (!response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }

                string jsondata = await response.Content.ReadAsStringAsync();

                EmployeeViewModel employees = JsonSerializer.Deserialize<EmployeeViewModel>(
                    jsondata, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                return View(employees);
            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }
            
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EmployeeViewModel model)
        {
            try
            {
                string token = HttpContext.Session.GetString("JWToken");
                if (string.IsNullOrWhiteSpace(token))
                {
                    return RedirectToAction("Login", "Account");
                }
                if (model == null || model.EmployeeId <= 0)
                {
                    ViewBag.Error = "Invalid employee data";
                    return View(model);
                }
                string apiurl = _Config["BackendApi:BaseUrl"] + "/api/employee/update";

                HttpClient client = new HttpClient();

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                string jsondata = JsonSerializer.Serialize(model);

                StringContent content = new StringContent(jsondata, Encoding.UTF8, "application/Json");

                HttpResponseMessage response = await client.PutAsync(apiurl, content);
                if (!response.IsSuccessStatusCode)
                {
                    ViewBag.Error = "Failed to update employee";
                    return View(model);
                }
                return View("Index");
            }
            catch (Exception)
            {
                ViewBag.Error = "Something went wrong";
                return View(model);
            }
            
        }

        [HttpPost]
        [Route("Dashboard/Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (id<=0)
                {
                    return RedirectToAction("Index");
                }
                string token = HttpContext.Session.GetString("JWToken");
                if (string.IsNullOrWhiteSpace(token))
                {
                    return RedirectToAction("Login","Account");
                }
                string apiurl = _Config["BackendApi:BaseUrl"] + "/api/employee/delete/" + id;
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",token);
                HttpResponseMessage response = await client.DeleteAsync(apiurl);
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }
        }
    }
}
