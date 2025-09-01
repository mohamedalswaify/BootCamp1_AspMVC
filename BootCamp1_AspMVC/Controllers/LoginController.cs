using BootCamp1_AspMVC.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace BootCamp1_AspMVC.Controllers
{
    public class LoginController : Controller
    {
        private const string LoginCookieName = "UserName";

        private void SetLoginCookie(string username, bool rememberMe)
        {
            var options = new CookieOptions
            {
                HttpOnly = true,
                SameSite = SameSiteMode.Lax,
                // في الإنتاج فعّل HTTPS فقط:
                // Secure = true
            };

            if (rememberMe)
            {
                options.Expires = DateTimeOffset.UtcNow.AddDays(14); // مدة التذكر
            }
            // لو ما فيش Expires => Session cookie ينتهي عند إغلاق المتصفح

            Response.Cookies.Append(LoginCookieName, username, options);
        }

        private void ClearLoginCookie()
        {
            Response.Cookies.Delete(LoginCookieName);
        }

        private bool IsLoggedInByCookie()
        {
            return Request.Cookies.ContainsKey(LoginCookieName);
        }

        private string? GetUserNameFromCookie()
        {
            return Request.Cookies.TryGetValue(LoginCookieName, out var val) ? val : null;
        }


        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel model)
        {
            // تحقق بسيط — بدله بقاعدة البيانات لاحقًا
            if (model.Username == "admin" && model.Password == "123")
            {
                SetLoginCookie(model.Username, model.RememberMe);
                return RedirectToAction(nameof(Index),"Home");
            }

            ModelState.AddModelError(string.Empty, "اسم المستخدم أو كلمة المرور غير صحيحة.");
            return View(model);
        }


        public IActionResult Logout()
        {
            ClearLoginCookie();
            return RedirectToAction("Login");
        }





        public IActionResult Index()
        {
            return View();
        }
    }
}
