using Practice_1._1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using static Practice_1._1.PasswordHelpers.PasswordHelpers;

namespace Practice_1._1.Controllers
{
    public class AuthController : Controller
    {
        /////////////**** LOGIN ****\\\\\\\\\\\\\
        // GET: Auth
        public ActionResult Login()
        {
            return View();
        }
        //POST: Auth
        [HttpPost]
        public ActionResult Login(LoginVM login)
        {
            User user;
            if (!ModelState.IsValid)
            {
                return View(login);
            }
            using(AppContext db = new AppContext())
            { 
                user = db.Users.Include("Roles").SingleOrDefault(i => i.Username == login.Username); // Here i find the user with the Username that came from the client user will be null if the username does not exist
            }
            if (user != null && login.Password == user.Password)
            {
                var userRoles = string.Join("|", user.Roles.Select(i => i.RoleName));
                var ticket = new FormsAuthenticationTicket(                                         //create a new ticket that holds:
                    version: 1,
                    name: login.Username,                                                           //The username of the logged in user
                    issueDate: DateTime.Now,                                                         //The creation time of this ticket
                    expiration: DateTime.Now.AddDays(5).AddSeconds(HttpContext.Session.Timeout),    //When is the ticket going to expire
                    isPersistent: login.RememberMe,                                                 //After closing the browser the ticket will still exist
                    userData: userRoles);                                                           // Attach to the ticket the Role of the user
                var encryptedTicket = FormsAuthentication.Encrypt(ticket);                          //Encrypt the cookie
                var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);  //Create a cookie containing the encrypted ticket
                HttpContext.Response.Cookies.Add(cookie);                                           //Send it back to the 
                return RedirectToAction("Index", "Home");
                
            }
            else
            {
                ViewBag.WrongUsernameOrPassword = "Wrong username or password";
                return View(login);
            }         
        }

        /////////////**** LOGOUT ****\\\\\\\\\\\\\
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        /////////////**** REGISTER ****\\\\\\\\\\\\\
        //GET:Auth
        public ActionResult Register()
        {
            return View();
        }
        //POST: Auth
        [HttpPost]
        public ActionResult Register(RegisterVM user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }
            bool userAddedSuccesfully = false;
            User newUser = new User();
            using (AppContext db = new AppContext())
            {
                User dbUser = db.Users.SingleOrDefault(i => i.Username == user.Username);
                if (dbUser==null)
                {
                    newUser.Username = user.Username; //Add to the model the username

                    //TODO:add to user property Salt(byte) and change type of password to (byte) in order to use this encryption script
                    ////encrypt the pass
                    //var salt = Passhash.GetSalt();
                    //var hash = Passhash.Hash(user.Password, salt);
                    //user.Password = hash;
                    //user.Salt = salt;

                    newUser.Password = user.Password; //Add to the model the Password
                    newUser.Email = user.Email;       //Add to the model the email
                    Role role = db.Roles.Single(i=>i.RoleName=="User");     //find the role of the user in order to add it to the ticket
                    newUser.Roles = new List<Role>();                       //Create new and empty list in the Roles property of the object User which is type list   
                    newUser.Roles.Add(role);                                //Add to the above list the role "User"
                    db.Users.Add(newUser);                                  //Add the model to db Entity
                    db.Entry(role).State = System.Data.Entity.EntityState.Unchanged; //Add to the middle table the role id and the user id
                    db.SaveChanges();
                    userAddedSuccesfully = true; 
                }
                else
                {
                    ViewBag.MessageUserAllreadyExists = "This username allready exists"; // if the username allready exist create a view bag and send it to the view in order to display the appropriate message
                }
            }
            return View();
        }
    }
}