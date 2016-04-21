using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Notatki.PWR.Models;
using Notatki.PWR.Services;

namespace Notatki.PWR.Controllers
{
    public class NoteController : Controller
    {
        private INoteService _noteService;

        public NoteController(INoteService noteService)
        {
            _noteService = noteService;
        }

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
                _noteService.AddNewNote(model);
                TempData["Success"] = "Notatka została zapisana!";
                return RedirectToAction("Index");
            }
            ViewBag.Error = "Wypełnij poprawnie pola notatki.";
            return View(model);
        }

        public ActionResult List()
        {
            var viewmodel = _noteService.GetNotes();
            return View(viewmodel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            _noteService.DeleteNote(id);
            return RedirectToAction("List");
        }

        public ActionResult Edit(int id)
        {
            var viewmodel = _noteService.GetNoteForEdit(id);
            return View(viewmodel);
        }

        [HttpPost]
        public ActionResult Edit(EditNoteDto model)
        {
            if (ModelState.IsValid)
            {
                _noteService.UpdateNote(model);
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