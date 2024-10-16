using HotelProject.WebUI.Models.Staff;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Text;

namespace HotelProject.WebUI.Controllers
{
    public class StaffController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public StaffController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient(); //istemci oluştur
            var responseMessage = await client.GetAsync("http://localhost:5216/api/Staff"); //adrese istekte bulunduk
            if (responseMessage.IsSuccessStatusCode) //200 küsür dönerse
            {
                var jsonData=await responseMessage.Content.ReadAsStringAsync(); //gelen veriyi jsonDataya aktardık
                var values=JsonConvert.DeserializeObject<List<StaffViewModel>>(jsonData); //jsonDatayı(json türünde) deserialize ederek dönüşümü yaptık 
                return View(values); //valuesi viewa gönderdik
            }
            return View();
        }
        [HttpGet]
        public IActionResult AddStaff()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddStaff(AddStaffViewModel model)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData=JsonConvert.SerializeObject(model); //Veriyi jsona dönüştürerek gönderdik Serialize  
            StringContent content = new StringContent(jsonData,Encoding.UTF8,"application/json"); //aplication json türü belirtir encoding ile kodlandı data
            var responseMessage= await client.PostAsync("http://localhost:5216/api/Staff",content);

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
       
        public async Task<IActionResult> DeleteStaff(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync($"http://localhost:5216/api/Staff/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();

        }
        [HttpGet]
        public async Task<IActionResult> UpdateStaffAsync(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"http://localhost:5216/api/Staff/{id}");

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData= await responseMessage.Content.ReadAsStringAsync(); // veriyi jsondataya verdik 
                var value= JsonConvert.DeserializeObject<UpdateStaffViewModel>(jsonData); //jsondatayı deseralize(yani jsondata'dan çıkarıp normal veri tüpüne dönüştürdük)
                return View(value);
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UpdateStaffAsync(UpdateStaffViewModel updateStaffViewModel)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData=JsonConvert.SerializeObject(updateStaffViewModel);
            StringContent stringContent=new StringContent(jsonData,Encoding.UTF8,"application/json");
            var responseMessage = await client.PutAsync("http://localhost:5216/api/Staff/",stringContent);

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

    }
}
