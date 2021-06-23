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
using PagedList;

namespace Kalokyris_George_Assignment_2.Controllers
{
    public class TrainerController : Controller
    {
        private MyDatabase db = new MyDatabase();

        // GET: Trainer
        public ActionResult Index(string firstName, string lastName, int? Year, string sortOrder, int? page, int salary = 0)
        {
            
            var Trainers = db.Trainers.ToList();

            //Filtering
            if (!String.IsNullOrEmpty(firstName))
            {
                Trainers = Trainers.Where(x => x.FirstName.ToUpper().Contains(firstName.ToUpper())).ToList();
                ViewBag.CurrentFilter = firstName;
            }
            
            if (!String.IsNullOrEmpty(lastName))
            {
                Trainers = Trainers.Where(x => x.LastName.ToUpper().Contains(lastName.ToUpper())).ToList();
                ViewBag.CurrentFilter = lastName;

            }


            if (salary > 0)
            {
                Trainers = Trainers.Where(x => x.Salary == salary).ToList();
                ViewBag.CurrentFilter = salary;

            }

            var Years = new List<int>();

            var AvailableYears = db.Trainers.OrderByDescending(x=>x.HireDate).Select(x => x.HireDate.Year).ToList();

            Years.AddRange(AvailableYears.Distinct());

            ViewBag.Year = new SelectList(Years);

            if (!String.IsNullOrEmpty(Year.ToString()))
            {
                Trainers = Trainers.Where(x => x.HireDate.Year == Year).ToList();
            }

            //Sorting
            ViewBag.CurrentSort = sortOrder;
            ViewBag.FirstNameSort = String.IsNullOrEmpty(sortOrder) ? "FirstName" : "";
            ViewBag.FirstNameSort = sortOrder == "FirstName" ? "FirstNameDesc" : "FirstName";
            ViewBag.LastNameSort = sortOrder == "LastName" ? "LastDesc" : "LastName";
            ViewBag.Salary = sortOrder == "Salary" ? "SalaryDesc" : "Salary";
            ViewBag.HireDate = sortOrder == "HireDate" ? "HireDateDesc" : "HireDate";
            ViewBag.TrainerAvailable = sortOrder == "isAvailable" ? "isAvailableDesc" : "isAvailable";

             switch (sortOrder)
            {
                case "FirstName":Trainers = Trainers.OrderBy(x => x.FirstName).ToList(); break;
                case "FirstNameDesc":Trainers = Trainers.OrderByDescending(x => x.FirstName).ToList(); break;
                case "LastName": Trainers = Trainers.OrderBy(x => x.LastName).ToList(); break;
                case "LastDesc": Trainers = Trainers.OrderByDescending(x => x.LastName).ToList(); break;
                case "Salary": Trainers = Trainers.OrderBy(x => x.Salary).ToList(); break;
                case "SalaryDesc": Trainers = Trainers.OrderByDescending(x => x.Salary).ToList(); break;
                case "HireDate": Trainers = Trainers.OrderBy(x => x.HireDate).ToList(); break;
                case "HireDateDesc": Trainers = Trainers.OrderByDescending(x => x.HireDate).ToList(); break;
                case "isAvailable": Trainers = Trainers.OrderBy(x => x.isAvailable).ToList();break;
                case "isAvailableDesc": Trainers = Trainers.OrderByDescending(x => x.isAvailable).ToList();break;
    
            }

            //Pagination
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            SearchModel sm = new SearchModel() {trains = Trainers.ToPagedList(pageNumber, pageSize), filtering = ViewBag.CurrentFilter, sorting = sortOrder, pageSize = 3, pageNumber = (page ?? 1), sFirstName = firstName, sLastName = lastName, sHireYear = Year, sSalary = salary};


            return View(sm);
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
