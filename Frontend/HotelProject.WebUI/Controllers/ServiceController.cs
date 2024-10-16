using HotelProject.WebUI.Dtos.ServiceDto;
using HotelProject.WebUI.Models.Staff;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace HotelProject.WebUI.Controllers
{
    public class ServiceController : Controller
    {

        private readonly IHttpClientFactory _httpClientFactory;

        public ServiceController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient(); //istemci oluştur
            var responseMessage = await client.GetAsync("http://localhost:5216/api/Service"); //adrese istekte bulunduk
            if (responseMessage.IsSuccessStatusCode) //200 küsür dönerse
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync(); //gelen veriyi jsonDataya aktardık
                var values = JsonConvert.DeserializeObject<List<ResultServiceDto>>(jsonData); //jsonDatayı(json türünde) deserialize ederek dönüşümü yaptık (normal veri tipine)
                return View(values); //valuesi viewa gönderdik
            }
            return View();
        }
        [HttpGet]
        public IActionResult AddService()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddService(CreateServiceDto createServiceDto)
        {
            if (!ModelState.IsValid) //model geçersizse hata durumu varsa 
            {
                return View();
            }
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createServiceDto); //Veriyi jsona dönüştürerek gönderdik Serialize  
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json"); //aplication json türü belirtir encoding ile kodlandı data
            var responseMessage = await client.PostAsync("http://localhost:5216/api/Service", content);

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        public async Task<IActionResult> DeleteService(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync($"http://localhost:5216/api/Service/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();

        }
        [HttpGet]
        public async Task<IActionResult> UpdateServiceAsync(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"http://localhost:5216/api/Service/{id}");

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync(); // veriyi jsondataya verdik 
                var value = JsonConvert.DeserializeObject<UpdateServiceDto>(jsonData); //jsondatayı deseralize(yani jsondata'dan çıkarıp normal veri tüpüne dönüştürdük)
                return View(value);
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UpdateServiceAsync(UpdateServiceDto updateServiceDto)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateServiceDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync("http://localhost:5216/api/Service/", stringContent);

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

    }
}
