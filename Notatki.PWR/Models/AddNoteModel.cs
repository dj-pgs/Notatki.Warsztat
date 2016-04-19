using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Notatki.PWR.Models
{
    public class AddNoteModel
    {
        [Required]
        [MinLength(4)]
        public string Title { get; set; }

        [Required]
        [MinLength(4)]
        [AllowHtml]
        public string Content { get; set; }
    }


    public class ListNotesViewModel
    {
        public List<ListNoteItem> Notes { get; set; }
    }

    public class ListNoteItem
    {
        public string Id { get; set; }

        public string Title { get; set; }

    }
}