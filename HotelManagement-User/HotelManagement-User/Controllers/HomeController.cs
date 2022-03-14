using HotelManagement_User.Models;
using HotelManagement_User.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace HotelManagement_User.Controllers
{
    public class HomeController : Controller
    {
        
        private IConfiguration _configuaration;
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpService _httpService;

        public HomeController(ILogger<HomeController> logger,  IConfiguration configuration, IHttpService httpService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _configuaration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _httpService = httpService ?? throw new ArgumentNullException(nameof(httpService));
            ViewBag.Message = null;
        }

        public IActionResult Index()
        {
            return View();
        }
   
        public IActionResult Book()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Book(BookRoom bookRoom)
        
            {
            ModelState.Clear();
            if (HttpContext.Session.GetString("Email") == null)
            {
               return await Logout();
            }
            bookRoom.CustomerName = HttpContext.Session.GetString("Name");
            bookRoom.Email = HttpContext.Session.GetString("Email");
            var requestBody = new StringContent(JsonSerializer.Serialize(bookRoom), Encoding.UTF8, "application/json");
            var Res = await _httpService.PostAsync<bool>(_configuaration["API:Book"],requestBody);
            if(Res)
            {
                ViewBag.Message = "Room booked successfully";
                ViewBag.Color = "green";
            }
            else
            {
                ViewBag.Message = "No room available for that date.";
                ViewBag.Color = "red";
            }
            
            return View();
        }

        public async Task<IActionResult> History()
        {
            if(HttpContext.Session.GetString("Email")== null)
            {
               return await Logout();
            }
            var response = await _httpService.GetAsync<List<BookingDetails>>(_configuaration["API:History"]+"/"+ HttpContext.Session.GetString("Email"));
            return View(response);
        }

        [AllowAnonymous]
        public async Task GoogleLogin()
        {
            await HttpContext.ChallengeAsync(GoogleDefaults.AuthenticationScheme, new AuthenticationProperties()
            {
                RedirectUri = Url.Action("GoogleResponse")
            });
        }
        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Clear();
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            //Redirect to home page    
            return LocalRedirect("/");
        }
        [AllowAnonymous]
        public async Task<IActionResult> GoogleResponse()
        {
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            var claims = result.Principal.Identities
                .FirstOrDefault().Claims.Select(claim => new
                {
                    claim.Issuer,
                    claim.OriginalIssuer,
                    claim.Type,
                    claim.Value
                });

            foreach (var claim in claims)
            {
                if (claim.Type.ToString().Contains("email"))
                {
                    HttpContext.Session.SetString("Email", claim.Value);

                }
            }
              
             HttpContext.Session.SetString("Name", result.Principal.Identities.FirstOrDefault().Name);
                
            
           return LocalRedirect("/Home/Book");
        }

   
    }
}
