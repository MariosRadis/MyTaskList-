using Practice_1._1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Practice_1._1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }


        /////////////**** VIEW ALL TASKS ****\\\\\\\\\\\\\
        public ActionResult AllTasks()
        {
            List<Task> tasks = null;
            using (AppContext db = new AppContext())
            {
                tasks = db.Tasks.ToList();
            }
            return View(tasks);
        }


        /////////////**** CREATE TASK ****\\\\\\\\\\\\\
        public ActionResult CreateTask()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult CreateTask(Task task)
        {
            using (AppContext db = new AppContext())
            {
                Task newTask = new Task();
                if (!ModelState.IsValid)
                {
                    return View(task);
                }
                else
                {
                    newTask.Title = task.Title;
                    newTask.Description = task.Description;
                    newTask.DueDate = task.DueDate;
                    newTask.Completed = task.Completed;
                    User user = new User();
                    user = db.Users.SingleOrDefault(i => i.Username == User.Identity.Name);
                    newTask.UserId = user.Id;
                    newTask.User = user;
                    db.Tasks.Add(newTask);
                    db.SaveChanges();

                    return RedirectToAction("AllTasks", "Home");
                }
            }
        }

        /////////////**** COMPLETE A TASK ****\\\\\\\\\\\\\
        public 

        /////////////**** EDIT A TASK ****\\\\\\\\\\\\\
        


        /////////////**** DELETE A TASK ****\\\\\\\\\\\\\




    }
}