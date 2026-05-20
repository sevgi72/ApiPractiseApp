using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;
using WebApplication2.ViewModels;

namespace WebApplication2.Controllers
{
    public class UIAccountController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;

        public UIAccountController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVm vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            try
            {
                var handler = new HttpClientHandler();
                handler.ServerCertificateCustomValidationCallback =
                    (message, cert, chain, errors) => true; // SSL bypass

                var client = new HttpClient(handler);
                var jsonContent = JsonSerializer.Serialize(vm);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(
                    "http://localhost:5202/api/Account/Register", content);

                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "✅ Kayıt başarılı! Lütfen giriş yapın.";
                    return RedirectToAction("Login");
                }
                else
                {
                    var errorResponse = await response.Content.ReadAsStringAsync();
                    ModelState.AddModelError(string.Empty, $"❌ Kayıt başarısız: {errorResponse}");
                    return View(vm);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"❌ Hata oluştu: {ex.Message}");
                return View(vm);
            }
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVm model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var handler = new HttpClientHandler();
                handler.ServerCertificateCustomValidationCallback =
                    (message, cert, chain, errors) => true; // SSL bypass

                var client = new HttpClient(handler);
                var jsonContent = JsonSerializer.Serialize(model);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(
                    "http://localhost:5202/api/Account/Login", content);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var tokenResponse = JsonSerializer.Deserialize<TokenResponseDto>(responseContent,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                    if (!string.IsNullOrEmpty(tokenResponse?.Token))
                    {
                        // ✅ Token'ı Cookie'ye Ekle
                        var cookieOptions = new CookieOptions
                        {
                            HttpOnly = true,           // JavaScript erişimini engelle
                            Secure = true,             // HTTPS üzerinde gönder
                            SameSite = SameSiteMode.Strict,
                            Expires = DateTimeOffset.UtcNow.AddHours(1) // 1 saat geçerli
                        };

                        Response.Cookies.Append("AuthToken", tokenResponse.Token, cookieOptions);

                        TempData["SuccessMessage"] = "✅ Giriş başarılı!";
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "❌ Token alınamadı");
                        return View(model);
                    }
                }
                else
                {
                    var errorResponse = await response.Content.ReadAsStringAsync();
                    ModelState.AddModelError(string.Empty,
                        $"❌ Giriş başarısız: {errorResponse}");
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"❌ Hata oluştu: {ex.Message}");
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult Logout()
        {
            // ✅ Cookie'yi Sil
            Response.Cookies.Delete("AuthToken");
            TempData["SuccessMessage"] = "✅ Çıkış yapıldı.";
            return RedirectToAction("Login");
        }
    }
}