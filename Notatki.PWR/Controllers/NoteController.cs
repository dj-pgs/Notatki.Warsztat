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
                //tu cos bede robil
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}