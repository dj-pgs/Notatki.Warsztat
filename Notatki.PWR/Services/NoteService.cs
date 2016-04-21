using System.Linq;
using Notatki.PWR.Controllers;
using Notatki.PWR.Models;

namespace Notatki.PWR.Services
{
    public class NoteService
    {
        public void AddNewNote(AddNoteModel model)
        {
            var note = new Note();
            note.Title = model.Title;
            note.Content = model.Content;
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Notes.Add(note);
                ctx.SaveChanges();
            }
        }

        public void DeleteNote(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var note = ctx.Notes.Single(n => n.Id == id);
                ctx.Notes.Remove(note);
                ctx.SaveChanges();
            }
        }

        public void UpdateNote(EditNoteDto model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var note = ctx.Notes.Single(n => n.Id == model.Id);
                note.Title = model.Title;
                note.Content = model.Content;
                ctx.SaveChanges();
            }
        }

        public EditViewModel GetNoteForEdit(int id)
        {
            var viewmodel = new EditViewModel();
            using (var ctx = new ApplicationDbContext())
            {
                var note = ctx.Notes.Single(n => n.Id == id);
                viewmodel.Title = note.Title;
                viewmodel.Content = note.Content;
                viewmodel.Id = note.Id;
            }
            return viewmodel;
        }

        public ListNotesViewModel GetNotes()
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

            return viewmodel;
        }
    }
}