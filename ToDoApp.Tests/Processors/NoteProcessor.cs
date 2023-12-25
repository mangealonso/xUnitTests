using ToDoApp.Models;

namespace ToDoApp
{
    public class NoteProcessor
    {
        public NoteProcessor()
        {
        }

        private int nextId = 0;
        private List<NoteResult> allToDos = new List<NoteResult>();

        public NoteResult SaveNote(string title, string description)
        {
            if (title == null)
            {
                throw new ArgumentNullException(nameof(title));
            }

            if (description == null)
            {
                throw new ArgumentNullException(nameof(description));
            }

            var note = new Note
            {
                Id = nextId++,
                Title = title,
                Description = description,
                IsDone = false,
            };

            var result = new NoteResult
            {
                Id = note.Id,
                Title = note.Title,
                Description = note.Description,
                IsDone = note.IsDone,
            };

            allToDos.Add(result);

            return result;
        }

        public List<NoteResult> AddNoteToList(List<Note> notesList)
        {
            if (notesList == null)
            {
                throw new ArgumentNullException(nameof(notesList));
            }

            var noteResult = new List<NoteResult>();

            foreach (var note in notesList)
            {
                noteResult.Add(SaveNote(note.Title, note.Description));
            }

            return noteResult;
        }

        public bool DeleteNote(int idToDelete)
        {
            if (allToDos == null)
            {
                throw new ArgumentNullException(nameof(allToDos));
            }

            var noteToDelete = allToDos.FirstOrDefault(x => x.Id == idToDelete);

            if (noteToDelete != null)
            {
                allToDos.Remove(noteToDelete);
                return true;
            }
            else
            {
                return false;
            }
        }     

        public List<NoteResult> GetAllToDos()
        {
            return allToDos;
        } 
    }
}