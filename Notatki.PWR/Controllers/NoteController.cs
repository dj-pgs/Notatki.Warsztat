using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Notatki.PWR.Models;

namespace Notatki.PWR.Controllers
{
    public class NoteController : Controller
    {
        // GET: Note
        public ActionResult Index()
        {
            ViewBag.Success = TempData["Success"];
            return View();
        }

        [HttpPost]
        public ActionResult Index(AddNoteModel model)
        {
            if (ModelState.IsValid)
            {
                var note = new Note();
                note.Title = model.Title;
                note.Content = model.Content;
                using (var ctx = new ApplicationDbContext())
                {
                    ctx.Notes.Add(note);
                    ctx.SaveChanges();
                }
                TempData["Success"] = "Notatka została zapisana!";
                return RedirectToAction("Index");
            }
            ViewBag.Error = "Wypełnij poprawnie pola notatki.";
            return View(model);
        }

        public ActionResult List()
        {
            var viewmodel = new ListNotesViewModel();

            using (var ctx = new ApplicationDbContext())
            {
                var notes = ctx.Notes.Select(note => new ListNoteItem()
                {
                    Title = note.Title,
                    Id = note.Id.ToString()
                }).ToList();

                viewmodel.Notes = notes;
            }
            return View(viewmodel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            using (var ctx=new ApplicationDbContext())
            {
                var note = ctx.Notes.Single(n => n.Id == id);
                ctx.Notes.Remove(note);
                ctx.SaveChanges();
            }
            return RedirectToAction("List");
        }
    }
}