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

        public ActionResult Edit(int id)
        {
            var viewmodel = new EditViewModel();
            using (var ctx=new ApplicationDbContext())
            {
                var note = ctx.Notes.Single(n => n.Id == id);
                viewmodel.Title = note.Title;
                viewmodel.Content = note.Content;
                viewmodel.Id = note.Id;
            }
            return View(viewmodel);
        }

        [HttpPost]
        public ActionResult Edit(EditNoteDto model)
        {
            if (ModelState.IsValid)
            {
                using (var ctx=new ApplicationDbContext())
                {
                    var note = ctx.Notes.Single(n => n.Id == model.Id);
                    note.Title = model.Title;
                    note.Content = model.Content;
                    ctx.SaveChanges();
                }
                return RedirectToAction("List");
            }
            return View(new EditViewModel()
            {
                Id = model.Id,
                Title = model.Title,
                Content = model.Content
            });
        }
    }

    public class EditNoteDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }
    }

    public class EditViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }
    }
}