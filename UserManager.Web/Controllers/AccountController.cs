using System.Web.Mvc;
using UserManager.Web.Models;
using System.Web.Security;
using UserManager.DAL.Abstracts;
using UserManager.DAL.Factories;
using UserManager.DAL.Models;

namespace UserManager.Web.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private IUserRepo userRepo;


        public AccountController()
        {
            userRepo = RepoFactory.CreateUserRepo();
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = userRepo.Get(model.Email, model.Password);

            if(user != null)
            {
                SetUserSession(user);
                return RedirectToLocal(returnUrl);
            }
            else
            {
                ModelState.AddModelError("", "Invalid login attempt.");
                return View(model);
            }
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (userRepo.CheckIfEmailExists(model.Email))
                {
                    ModelState.AddModelError("", "Sorry, email already linked to an existing account.");
                    return View(model);
                }
                var user = new UserAccount { Email = model.Email, Password = model.Password };
                if (userRepo.Add(user) > 0)
                {
                    SetUserSession(user);

                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Sorry, Unable to add user.");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }


        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        private void SetUserSession(UserAccount user)
        {
            FormsAuthentication.SetAuthCookie(user.Email, true);
            Session.Add("currentUser", user);
        }
        #endregion
    }
}