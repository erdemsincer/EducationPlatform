﻿using EducationPlatform.Dto.AuthDto;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace EducationPlatform.WebUI.Controllers
{
    public class AuthController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AuthController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return View(loginDto);
            }

            // ✅ HttpClient oluşturulurken BaseAddress kullanılmalı!
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(loginDto);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            // ✅ API ile İletişim
            var response = await client.PostAsync("https://localhost:7028/api/Auth/login", content);


            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError("", "E-posta veya şifre hatalı!");
                return View(loginDto);
            }

            var responseString = await response.Content.ReadAsStringAsync();
            Console.WriteLine("API Yanıtı: " + responseString); 

            var token = JsonConvert.DeserializeObject<TokenResponseDto>(responseString);

            if (token == null || string.IsNullOrEmpty(token.AccessToken))
            {
                ModelState.AddModelError("", "Kimlik doğrulama başarısız!");
                return View(loginDto);
            }

         
            HttpContext.Session.SetString("AuthToken", token.AccessToken);

            
            Response.Cookies.Append("AuthToken", token.AccessToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,   
                SameSite = SameSiteMode.Strict
            });

            return RedirectToAction("Index", "Home"); 
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task< IActionResult> Register(RegisterDto registerDto)
        {
            if (!ModelState.IsValid)
            {
                return View(registerDto);
            }

            var client = _httpClientFactory.CreateClient(); // BaseAddress tanımlandığı için sadece endpoint veriyoruz
            var jsonData = JsonConvert.SerializeObject(registerDto);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("https://localhost:7028/api/Auth/register", content);

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError("", "Kayıt işlemi başarısız! Lütfen bilgileri kontrol edin.");
                return View(registerDto);
            }

            // Başarılı kayıt sonrası Login sayfasına yönlendir
            return RedirectToAction("Login", "Auth");
        }
    

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("AuthToken");

            if (Request.Cookies["AuthToken"] != null)
            {
                Response.Cookies.Delete("AuthToken");
            }

            return RedirectToAction("Login", "Auth");
        }
    }
}
