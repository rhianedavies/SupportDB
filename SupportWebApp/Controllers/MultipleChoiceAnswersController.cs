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
    public class MultipleChoiceAnswersController : Controller
    {
        private SupportDBEntities db = new SupportDBEntities();

        // GET: MultipleChoiceAnswers
        public async Task<ActionResult> Index()
        {
            var multipleChoiceAnswers = db.MultipleChoiceAnswers.Include(m => m.question).Include(m => m.Questionnaire);
            return View(await multipleChoiceAnswers.ToListAsync());
        }

        // GET: MultipleChoiceAnswers/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MultipleChoiceAnswer multipleChoiceAnswer = await db.MultipleChoiceAnswers.FindAsync(id);
            if (multipleChoiceAnswer == null)
            {
                return HttpNotFound();
            }
            return View(multipleChoiceAnswer);
        }

        // GET: MultipleChoiceAnswers/Create
        public ActionResult Create()
        {
            ViewBag.QuestionId = new SelectList(db.questions, "QuestionId", "Question1");
            ViewBag.QuestionnaireId = new SelectList(db.Questionnaires, "QuestionnaireId", "QuestionnaireId");
            return View();
        }

        // POST: MultipleChoiceAnswers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "MultipleChoiceAnswersId,QuestionId,Answer,AnswerNo,QuestionnaireId")] MultipleChoiceAnswer multipleChoiceAnswer)
        {
            if (ModelState.IsValid)
            {
                multipleChoiceAnswer.MultipleChoiceAnswersId = Guid.NewGuid();
                db.MultipleChoiceAnswers.Add(multipleChoiceAnswer);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.QuestionId = new SelectList(db.questions, "QuestionId", "Question1", multipleChoiceAnswer.QuestionId);
            ViewBag.QuestionnaireId = new SelectList(db.Questionnaires, "QuestionnaireId", "QuestionnaireId", multipleChoiceAnswer.QuestionnaireId);
            return View(multipleChoiceAnswer);
        }

        // GET: MultipleChoiceAnswers/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MultipleChoiceAnswer multipleChoiceAnswer = await db.MultipleChoiceAnswers.FindAsync(id);
            if (multipleChoiceAnswer == null)
            {
                return HttpNotFound();
            }
            ViewBag.QuestionId = new SelectList(db.questions, "QuestionId", "Question1", multipleChoiceAnswer.QuestionId);
            ViewBag.QuestionnaireId = new SelectList(db.Questionnaires, "QuestionnaireId", "QuestionnaireId", multipleChoiceAnswer.QuestionnaireId);
            return View(multipleChoiceAnswer);
        }

        // POST: MultipleChoiceAnswers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "MultipleChoiceAnswersId,QuestionId,Answer,AnswerNo,QuestionnaireId")] MultipleChoiceAnswer multipleChoiceAnswer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(multipleChoiceAnswer).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.QuestionId = new SelectList(db.questions, "QuestionId", "Question1", multipleChoiceAnswer.QuestionId);
            ViewBag.QuestionnaireId = new SelectList(db.Questionnaires, "QuestionnaireId", "QuestionnaireId", multipleChoiceAnswer.QuestionnaireId);
            return View(multipleChoiceAnswer);
        }

        // GET: MultipleChoiceAnswers/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MultipleChoiceAnswer multipleChoiceAnswer = await db.MultipleChoiceAnswers.FindAsync(id);
            if (multipleChoiceAnswer == null)
            {
                return HttpNotFound();
            }
            return View(multipleChoiceAnswer);
        }

        // POST: MultipleChoiceAnswers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            MultipleChoiceAnswer multipleChoiceAnswer = await db.MultipleChoiceAnswers.FindAsync(id);
            db.MultipleChoiceAnswers.Remove(multipleChoiceAnswer);
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
