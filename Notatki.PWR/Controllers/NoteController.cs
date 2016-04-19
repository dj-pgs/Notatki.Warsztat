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
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}