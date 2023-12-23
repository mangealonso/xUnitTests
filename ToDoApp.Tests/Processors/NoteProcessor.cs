using ToDoApp.Models;

namespace ToDoApp
{
    public class NoteProcessor
    {
        public NoteProcessor()
        {
        }

        public NoteResult Save(Note note)
        {
            if(note is null)
            {
                throw new ArgumentNullException(nameof(note));
            }

            return new NoteResult
            {
                Id = note.Id,
                Title = note.Title,
                Description = note.Description,
                IsDone = note.IsDone,
            };
        }

        public List<NoteResult> SaveList(List<Note> notes)
        {
            if (notes == null)
            {
                throw new ArgumentNullException(nameof(notes));
            }

            var noteResult = new List<NoteResult>();

            foreach (var note in notes)
            {
                noteResult.Add(Save(note));
            }

            return noteResult;
        }
    }
}