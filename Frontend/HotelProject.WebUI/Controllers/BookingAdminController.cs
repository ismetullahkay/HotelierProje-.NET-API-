using HotelProject.WebUI.Dtos.BookingDto;
using HotelProject.WebUI.Dtos.ServiceDto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query;
using Newtonsoft.Json;
using System.Text;

namespace HotelProject.WebUI.Controllers
{
    public class BookingAdminController : Controller
    {

        private readonly IHttpClientFactory _httpClientFactory;

        public BookingAdminController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient(); //istemci oluştur
            var responseMessage = await client.GetAsync("http://localhost:5216/api/Booking"); //adrese istekte bulunduk
            if (responseMessage.IsSuccessStatusCode) //200 küsür dönerse
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync(); //gelen veriyi jsonDataya aktardık
                var values = JsonConvert.DeserializeObject<List<ResultBookingDto>>(jsonData); //jsonDatayı(json türünde) deserialize ederek dönüşümü yaptık (normal veri tipine)
                return View(values); //valuesi viewa gönderdik
            }
            return View();
        }
        public async Task<IActionResult> ApprovedReservation(ResultBookingDto resultBookingDto,int id)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(resultBookingDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync("http://localhost:5216/api/Booking/StatusBookingChange?id="+id, stringContent);
            return RedirectToAction("Index");

            
        }
        
    }
}
