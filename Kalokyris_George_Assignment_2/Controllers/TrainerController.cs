using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Kalokyris_George_Assignment_2.DAL;
using Kalokyris_George_Assignment_2.Models;

namespace Kalokyris_George_Assignment_2.Controllers
{
    public class TrainerController : Controller
    {
        private MyDatabase db = new MyDatabase();

        // GET: Trainer
        public ActionResult Index(string firstName, string lastName, int? Year, int salary = 0)
        {
            var Trainers = db.Trainers.ToList();

            if (!String.IsNullOrEmpty(firstName))
            {
                Trainers = Trainers.Where(x => x.FirstName.ToUpper().Contains(firstName.ToUpper())).ToList();
            }
            
            if (!String.IsNullOrEmpty(lastName))
            {
                Trainers = Trainers.Where(x => x.LastName.ToUpper().Contains(lastName.ToUpper())).ToList();
            }

            Trainers = Trainers.Where(x => x.Salary >= salary).ToList();

            if (salary > 0)
            {
                Trainers = Trainers.Where(x => x.Salary == salary).ToList();
            }

            var Years = new List<int>();

            var AvailableYears = db.Trainers.OrderByDescending(x=>x.HireDate).Select(x => x.HireDate.Year).ToList();

            Years.AddRange(AvailableYears.Distinct());

            ViewBag.Year = new SelectList(Years);

            if (!String.IsNullOrEmpty(Year.ToString()))
            {
                Trainers = Trainers.Where(x => x.HireDate.Year == Year).ToList();
            }



            return View(Trainers);
        }

        // GET: Trainer/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trainer trainer = db.Trainers.Find(id);
            if (trainer == null)
            {
                return HttpNotFound();
            }


            return View(trainer);
        }

        // GET: Trainer/Create
        public ActionResult Create()
        {
            ViewBag.SelectedCategories = db.Categories.ToList().Select(x => new SelectListItem()
            {
                Value = x.CategoryId.ToString(),
                Text = x.Title
            });


            return View();
        }

        // POST: Trainer/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TrainerId,FirstName,LastName,Salary,HireDate,isAvailable")] Trainer trainer, IEnumerable<int> SelectedCategories)
        {
            if (ModelState.IsValid)
            {
                db.Trainers.Attach(trainer);
                db.Entry(trainer).Collection("Categories").Load();
                trainer.Categories.Clear();
                db.SaveChanges();

                if(!(SelectedCategories is null))
                {
                    foreach (var id in SelectedCategories)
                    {
                        Category category = db.Categories.Find(id);
                        if (category != null)
                        {
                            trainer.Categories.Add(category);
                        }
                    }
                }

                db.Entry(trainer).State = EntityState.Added;
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.SelectedCategories = db.Categories.ToList().Select(x => new SelectListItem()
            {
                Value = x.CategoryId.ToString(),
                Text = x.Title
            });
            return View(trainer);
        }

        // GET: Trainer/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Trainer trainer = db.Trainers.Find(id);

            if (trainer == null)
            {
                return HttpNotFound();
            }

            var categoryIds = trainer.Categories.Select(x => x.CategoryId);



            ViewBag.SelectedCategories = db.Categories.ToList().Select(x => new SelectListItem()
            {
                Value = x.CategoryId.ToString(),
                Text = x.Title,
                Selected = categoryIds.Any(y=>y == x.CategoryId)
            });

            


            return View(trainer);
        }

        // POST: Trainer/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TrainerId,FirstName,LastName,Salary,HireDate,isAvailable")] Trainer trainer, IEnumerable<int> SelectedCategories)
        {
            if (ModelState.IsValid)
            {
                db.Trainers.Attach(trainer);
                db.Entry(trainer).Collection("Categories").Load();
                trainer.Categories.Clear();
                db.SaveChanges();

                if (!(SelectedCategories is null))
                {
                    foreach (var id in SelectedCategories)
                    {
                        Category category = db.Categories.Find(id);
                        if (category != null)
                        {
                            trainer.Categories.Add(category);
                        }
                    }
                }

                db.Entry(trainer).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.SelectedCategories = db.Categories.ToList().Select(x => new SelectListItem()
            {
                Value = x.CategoryId.ToString(),
                Text = x.Title
            });
            return View(trainer);
        }

        // GET: Trainer/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trainer trainer = db.Trainers.Find(id);
            if (trainer == null)
            {
                return HttpNotFound();
            }
            return View(trainer);
        }

        // POST: Trainer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Trainer trainer = db.Trainers.Find(id);
            db.Trainers.Remove(trainer);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
