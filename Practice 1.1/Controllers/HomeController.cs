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
        public ActionResult TaskComplete(int id)
        {
            Task task;
            using (AppContext db = new AppContext())
            {
                task = db.Tasks.Find(id);
                task.Completed = true;
                db.SaveChanges();

            }
                return RedirectToAction("AllTasks","Home");
        }

        /////////////**** EDIT A TASK ****\\\\\\\\\\\\\
        public ActionResult TaskEdit(int id)
        {
            Task TaskToEdit;
            using (AppContext db = new AppContext())
            {
                TaskToEdit = db.Tasks.Find(id);   
            }
                return View(TaskToEdit);
        }

        [HttpPost]
        public ActionResult TaskEdit(Task task)
        {
             using (AppContext db = new AppContext())
            {
                Task editedTask = new Task();
                if (!ModelState.IsValid)
                {
                    return View(task);
                }
                else
                {
                    editedTask = db.Tasks.Find(task.Id);
                    editedTask.Title = task.Title;
                    editedTask.Description = task.Description;
                    editedTask.DueDate = task.DueDate;
                    db.SaveChanges();
                    return RedirectToAction("AllTasks", "Home");
                }
            }
        }


        /////////////**** DELETE A TASK ****\\\\\\\\\\\\\
        public ActionResult TaskDelete(int id)
        {
            Task TaskToDelete;
            using (AppContext db = new AppContext())
            {
                TaskToDelete = db.Tasks.Find(id);
                db.Tasks.Remove(TaskToDelete);
                db.SaveChanges();
            }
            return RedirectToAction("AllTasks","Home");
        }



    }
}