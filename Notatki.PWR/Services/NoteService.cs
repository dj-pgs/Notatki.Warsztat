using System.Linq;
using Notatki.PWR.Controllers;
using Notatki.PWR.Models;

namespace Notatki.PWR.Services
{
    public class NoteService : INoteService
    {
        private INoteContext _noteContext;

        public NoteService(INoteContext noteContext)
        {
            _noteContext = noteContext;
        }

        public void AddNewNote(AddNoteModel model)
        {
            var note = new Note();
            note.Title = model.Title;
            note.Content = model.Content;
            _noteContext.Notes.Add(note);
            _noteContext.SaveChanges();
        }

        public void DeleteNote(int id)
        {

            var note = _noteContext.Notes.Single(n => n.Id == id);
            _noteContext.Notes.Remove(note);
            _noteContext.SaveChanges();

        }

        public void UpdateNote(EditNoteDto model)
        {
            var note = _noteContext.Notes.Single(n => n.Id == model.Id);
            note.Title = model.Title;
            note.Content = model.Content;
            _noteContext.SaveChanges();

        }

        public EditViewModel GetNoteForEdit(int id)
        {
            var viewmodel = new EditViewModel();

            var note = _noteContext.Notes.Single(n => n.Id == id);
            viewmodel.Title = note.Title;
            viewmodel.Content = note.Content;
            viewmodel.Id = note.Id;

            return viewmodel;
        }

        public ListNotesViewModel GetNotes()
        {
            var viewmodel = new ListNotesViewModel();

            var notes = _noteContext.Notes.Select(note => new ListNoteItem()
            {
                Title = note.Title,
                Id = note.Id.ToString()
            }).ToList();

            viewmodel.Notes = notes;


            return viewmodel;
        }
    }
}