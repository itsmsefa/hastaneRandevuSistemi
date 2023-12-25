using hastaneRandevuSistemi.Models;
using hastaneRandevuSistemi.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace hastaneRandevuSistemi.Controllers
{
    public class AccountController:Controller
    {
        private UserManager<AppUser> _userManager;
        private RoleManager<AppRole> _roleManager;
        private SignInManager<AppUser> _signInManager;
        private IEmailSender _emailSender;
        public AccountController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, SignInManager<AppUser> signInManager, IEmailSender emailSender)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
        }
        
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                if(ModelState.IsValid)
                {
                    var user = await _userManager.FindByEmailAsync(model.Email);

                    if(user != null)
                    {
                        await _signInManager.SignOutAsync();

                        if(!await _userManager.IsEmailConfirmedAsync(user))
                        {
                            ModelState.AddModelError("", "You must confirm your email address");
                            return View(model);
                        }

                        var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, true);

                        if(result.Succeeded)
                        {
                            await _userManager.ResetAccessFailedCountAsync(user);
                            await _userManager.SetLockoutEndDateAsync(user, null);

                            return RedirectToAction("Index", "Home");
                        }
                        else if(result.IsLockedOut)
                        {
                            var lockOutDate = await _userManager.GetLockoutEndDateAsync(user);
                            var timeLeft = lockOutDate.Value - DateTime.UtcNow;

                            ModelState.AddModelError("", $"Your account is logged out. Try again after {timeLeft.Minutes} minutes");
                        }

                        else
                        {
                            ModelState.AddModelError("", "Password is wrong");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "There isn't any user with this email address");
                    }
                }
            }

            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new AppUser { UserName = model.UserName, Email = model.Email, FullName = model.FullName };

                IdentityResult result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var url = Url.Action("ConfirmEmail", "Account", new {user.Id, token});

                    //email
                    await _emailSender.SendEmailAsync(user.Email, "Confirm your email address", $"Please confirm your account by clicking this <a href='https://localhost:7156{url}'>link</a>.");

                    TempData["message"] = "Confirm your email address by clicking the link we sent to your email address!";
                    return RedirectToAction("Login", "Account");
                }

                foreach(IdentityError err in result.Errors)
                {
                    ModelState.AddModelError("", err.Description);
                }
            }
            return View(model);
        }

        public async Task<IActionResult> ConfirmEmail(string Id, String token)
        {
            if(Id == null || token == null)
            {
                TempData["message"] = "Invalid token";
                return View();
            }
            var user = await _userManager.FindByIdAsync(Id);

            if(user != null)
            {
                var result = await _userManager.ConfirmEmailAsync(user, token);

                if(result.Succeeded)
                {
                    TempData["message"] = "Your email address is confirmed.";
                    return RedirectToAction("Login", "Account");
                }
            }

            TempData["message"] = "User not found!";
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }

        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(String Email)
        {
            if(string.IsNullOrEmpty(Email))
            {
                TempData["message"] = "Please enter your email address";
                return View();
            }
            var user = await _userManager.FindByEmailAsync(Email);


            if(user != null)
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var url = Url.Action("ResetPassword", "Account", new {user.Id, token});

                //email
                await _emailSender.SendEmailAsync(Email, "Reset your password", $"Please reset your password by clicking this <a href='https://localhost:7156{url}'>link</a>.");

                TempData["message"] = "Reset link has been sent to your email address";
                return RedirectToAction("Login", "Account");
            }

            TempData["message"] = "There is not a user linked to this email! Please register first.";
            return View("Create");
        }

        public IActionResult ResetPassword(string Id, string token)
        {
            if(Id == null || token == null)
            {
                TempData["message"] = "Invalid token";
                return RedirectToAction("Login");
            }
            var model = new ResetPasswordViewModel {Token = token};
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if(ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if(user == null)
                {
                    TempData["message"] = "User not found!";
                    return RedirectToAction("Login");
                }

                var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);

                if(result.Succeeded)
                {
                    TempData["message"] = "Your password has been resetted!";
                    return RedirectToAction("Login");
                }

                foreach(IdentityError err in result.Errors)
                {
                    ModelState.AddModelError("", err.Description);
                }
            }
            return View(model);
        }
    }
}