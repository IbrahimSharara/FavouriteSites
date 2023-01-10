using FavouriteLinks.Models;
using FavouriteLinks.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
namespace FavouriteLinks.Controllers
{
    public class AccountController : Controller
    {
        public UserManager<ApplicationUser> User { get; }
        public SignInManager<ApplicationUser> Sign { get; }
        public ISiteRepository site { get; set; }
        public IImageRepository Image { get; }

        public AccountController(UserManager<ApplicationUser> user , SignInManager<ApplicationUser> sign , ISiteRepository site , IImageRepository image)
        {
            User = user;
            Sign = sign;
            this.site = site;
            Image = image;
        }

        #region Add new Account
        public IActionResult AddNewAccount()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddNewAccount(UserViewModel newUser)
        {
            if(ModelState.IsValid)
            {
                string image = "";
                if (newUser.ImageFile != null)
                {
                     image = Image.AddnewImage(newUser.ImageFile);
                }
                var user = new ApplicationUser
                {
                    Email = newUser.Email,
                    PhoneNumber = newUser.Phone,
                    UserName = newUser.Name ,
                    Image = image,
                    Address = newUser.Address
                } ;
                var result =await User.CreateAsync(user, newUser.PassWord);
                if(result.Succeeded)
                    return RedirectToAction(nameof(Login));
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }
            return View();
        }
        #endregion

        #region Login
        public async Task<IActionResult> Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel userView)
        {
            if (ModelState.IsValid)
            {
                var user =await User.FindByEmailAsync(userView.Email);
                if(user != null)
                {
                    var password = await User.CheckPasswordAsync(user, userView.PassWord);
                    if(password)
                    {
                        var result = await Sign.PasswordSignInAsync(user, userView.PassWord, userView.RememberMe, false);
                        if(result.Succeeded)
                        {
                            TempData["userId"] = user.Id;
                            TempData["image"] = user.Image;
                            TempData["userName"] = user.UserName;
                            return RedirectToAction( actionName :"Index",controllerName: "home");
                        }
                    }
                }
            }
            return View(userView);
        }
        #endregion

        #region logout
        public async Task<IActionResult> Logout ()
        {
            await Sign.SignOutAsync();
            return RedirectToAction(nameof(Login)); 
        }
        #endregion
    }
}