using Notatki.PWR.Controllers;
using Notatki.PWR.Models;

namespace Notatki.PWR.Services
{
    public interface INoteService
    {
        void AddNewNote(AddNoteModel model);
        void DeleteNote(int id);
        EditViewModel GetNoteForEdit(int id);
        ListNotesViewModel GetNotes();
        void UpdateNote(EditNoteDto model);
    }
}