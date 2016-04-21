using System.Linq;
using AutoMapper;
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
            var note = Mapper.Map<Note>(model);
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
            Mapper.Map(model, note);
            _noteContext.SaveChanges();

        }

        public EditViewModel GetNoteForEdit(int id)
        {
            var note = _noteContext.Notes.Single(n => n.Id == id);
            var viewmodel = Mapper.Map<EditViewModel>(note);
            return viewmodel;
        }

        public ListNotesViewModel GetNotes()
        {

            var notes = _noteContext.Notes.ToList();

            var viewmodel = Mapper.Map<ListNotesViewModel>(notes);
            return viewmodel;
        }
    }
}