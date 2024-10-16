using HotelProject.WebUI.Dtos.ContactDto;
using HotelProject.WebUI.Dtos.SendMessageDto;
using HotelProject.WebUI.Models.Staff;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace HotelProject.WebUI.Controllers
{
    public class AdminContactController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AdminContactController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Inbox()
        {
            var client = _httpClientFactory.CreateClient(); //istemci oluştur
            var responseMessage = await client.GetAsync("http://localhost:5216/api/Contact"); //adrese istekte bulunduk
            if (responseMessage.IsSuccessStatusCode) //200 küsür dönerse
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync(); //gelen veriyi jsonDataya aktardık
                var values = JsonConvert.DeserializeObject<List<ResultInboxDto>>(jsonData); //jsonDatayı(json türünde) deserialize ederek dönüşümü yaptık 
                return View(values); //valuesi viewa gönderdik
            }
            return View();
        }
        public async Task<IActionResult> SendBox()
        {
            var client = _httpClientFactory.CreateClient(); //istemci oluştur
            var responseMessage = await client.GetAsync("http://localhost:5216/api/SendMessage"); //adrese istekte bulunduk
            if (responseMessage.IsSuccessStatusCode) //200 küsür dönerse
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync(); //gelen veriyi jsonDataya aktardık
                var values = JsonConvert.DeserializeObject<List<SendMessageResultDto>>(jsonData); //jsonDatayı(json türünde) deserialize ederek dönüşümü yaptık 
                return View(values); //valuesi viewa gönderdik
            }
            return View();
        }
        public PartialViewResult SideBarAdminContactPartial()
        {
            return PartialView();
        }
        public PartialViewResult SideBarAdminContactCategoryPartial()
        {
            return PartialView();
        }
     
        
        public async Task<IActionResult> MessageDetailsBySendBox(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"http://localhost:5216/api/SendMessage/{id}");

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync(); // veriyi jsondataya verdik 
                var value = JsonConvert.DeserializeObject<GetMessageByIDDto>(jsonData); //jsondatayı deseralize(yani jsondata'dan çıkarıp normal veri tüpüne dönüştürdük)
                return View(value);
            }
            return View();
        }
        public async Task<IActionResult> MessageDetailsByInBox(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"http://localhost:5216/api/Contact/{id}");

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync(); // veriyi jsondataya verdik 
                var value = JsonConvert.DeserializeObject<ResultInboxDto>(jsonData); //jsondatayı deseralize(yani jsondata'dan çıkarıp normal veri tüpüne dönüştürdük)
                return View(value);
            }
            return View();
        }
        [HttpGet]
        public IActionResult AddSendMessage()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddSendMessage(CreateSendMessageDto model)
        {
            model.SenderName = "admin";
            model.SenderMail = "admin@gmail.com";
            model.Date = DateTime.Parse(DateTime.Now.ToShortDateString());
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(model); //Veriyi jsona dönüştürerek gönderdik Serialize  
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json"); //aplication json türü belirtir encoding ile kodlandı data
            var responseMessage = await client.PostAsync("http://localhost:5216/api/SendMessage", content);

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("SendBox");
            }
            return View();
        }
    }
}
