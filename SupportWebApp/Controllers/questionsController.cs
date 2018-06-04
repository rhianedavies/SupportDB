using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SupportWebApp;

namespace SupportWebApp.Controllers
{
    public class questionsController : Controller
    {
        private SupportDBEntities db = new SupportDBEntities();

        // GET: questions
        public async Task<ActionResult> Index(string sortOrder)
        {
            ViewBag.NumberSortParm = String.IsNullOrEmpty(sortOrder) ? "num_desc" : "num_asc";
            ViewBag.QuestionSortParm = String.IsNullOrEmpty(sortOrder) ? "text_desc" : "text_asc";
            var questions = from q in db.questions
                select q;
            switch (sortOrder)
            {
                case "num_desc":
                    questions = questions.OrderByDescending(q => q.QuestionNo);
                    break;
                case "num_asc":
                    questions = questions.OrderBy(q => q.QuestionNo);
                    break;
                case "text_desc":
                    questions = questions.OrderByDescending(q => q.Question1);
                    break;
                case "text_asc":
                    questions = questions.OrderBy(q => q.Question1);
                    break;
                default:
                    questions = questions.OrderBy(q => q.QuestionNo);
                    break;
            }
            return View(await questions.ToListAsync());
        }

        // GET: questions/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            question question = await db.questions.FindAsync(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
        }

        // GET: questions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: questions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "QuestionId,Question1,QuestionNo")] question question)
        {
            if (ModelState.IsValid)
            {
                question.QuestionId = Guid.NewGuid();
                db.questions.Add(question);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(question);
        }

        // GET: questions/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            question question = await db.questions.FindAsync(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
        }

        // POST: questions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "QuestionId,Question1,QuestionNo")] question question)
        {
            if (ModelState.IsValid)
            {
                db.Entry(question).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(question);
        }

        // GET: questions/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            question question = await db.questions.FindAsync(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
        }

        // POST: questions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            question question = await db.questions.FindAsync(id);
            db.questions.Remove(question);
            await db.SaveChangesAsync();
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
