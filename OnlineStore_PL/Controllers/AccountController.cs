using EmailService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using OnlineStore_BLL.DTO.AuthDTO;
using OnlineStore_BLL.Interfaces;
using OnlineStore_PL.Helpers;
using System;
using System.Threading.Tasks;

namespace OnlineStore_PL.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAdministrationUnitOfWork _administrationUnitOfWork;
        private readonly IEmailSender _emailSender;
        private readonly JwtSettings _jwtSettings;

        public AccountController(IAdministrationUnitOfWork administrationUnitOfWork, IOptionsSnapshot<JwtSettings> jwtSettings, IEmailSender emailSender)
        {
            _administrationUnitOfWork = administrationUnitOfWork;
            _jwtSettings = jwtSettings.Value;
            _emailSender = emailSender;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var user = await _administrationUnitOfWork.UserService.GetCurrentUser(User.Identity.Name);
            var userDto = new UserDTO
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Address = user.Address,
                City = user.City,
                PhoneNumber = user.PhoneNumber
            };

            return View(userDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Index(UserDTO model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string userEmail = User.Identity.Name;
                    var user = await _administrationUnitOfWork.UserService.GetCurrentUser(userEmail);

                    user.FirstName = model.FirstName;
                    user.LastName = model.LastName;
                    user.Address = model.Address;
                    user.City = model.City;
                    user.PhoneNumber = model.PhoneNumber;

                    await _administrationUnitOfWork.UserService.UpdateUser(user);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(SignIn model)
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");

            try
            {
                var user = await _administrationUnitOfWork.AuthService.SignIn(model);

                var roles = await _administrationUnitOfWork.RoleService.GetUserRoles(user.Email);

                var token = JwtHelper.GenerateJwt(user, roles, _jwtSettings);

                HttpContext.Response.Cookies.Append(".AspNetCore.Application.Id", token,
                    new CookieOptions
                    {
                        MaxAge = TimeSpan.FromDays(Convert.ToDouble(_jwtSettings.Lifetime)),
                        SameSite = SameSiteMode.None,
                        Secure = true
                    });

                if (ModelState.IsValid)
                    return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp(SignUp model)
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");

            if (ModelState.IsValid)
            {
                try
                {
                    await _administrationUnitOfWork.AuthService.SignUp(model);

                    var user = await _administrationUnitOfWork.UserService.GetCurrentUser(model.Email);
                    var roles = await _administrationUnitOfWork.RoleService.GetUserRoles(user.Email);

                    var token = JwtHelper.GenerateJwt(user, roles, _jwtSettings);

                    HttpContext.Response.Cookies.Append(".AspNetCore.Application.Id", token,
                        new CookieOptions
                        {
                            MaxAge = TimeSpan.FromDays(Convert.ToDouble(_jwtSettings.Lifetime)),
                            SameSite = SameSiteMode.None,
                            Secure = true
                        });

                    return RedirectToAction("Index", "Home");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }

            return View(model);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> SignOut()
        {
            if (User.Identity.IsAuthenticated)
                await _administrationUnitOfWork.AuthService.SignOut();

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPassword forgotPassword)
        {
            var userEmail = forgotPassword.Email;
            try
            {
                var user = await _administrationUnitOfWork.UserService.GetCurrentUser(userEmail);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);

                return View("ForgotPassword");
            }

            var scheme = HttpContext.Request.Scheme;
            var hostValue = HttpContext.Request.Host.Host;
            var port = HttpContext.Request.Host.Port;

            forgotPassword.ClientURI = $"{scheme}://{hostValue}:{port}" +
                $"/Account/ResetPassword/";

            var msg = await _administrationUnitOfWork.AuthService.ForgotPassword(forgotPassword);
            await _emailSender.SendEmailAsync(msg);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult ResetPassword(string email, string token)
        {
            ViewBag.Email = email;
            ViewBag.Token = token;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPassword resetPassword)
        {
            await _administrationUnitOfWork.AuthService.ResetPassword(resetPassword);

            return RedirectToAction("Login");
        }
    }
}
