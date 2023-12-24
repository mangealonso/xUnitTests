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
            if (title is null)
            {
                throw new ArgumentNullException(nameof(title));
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


        //public List<NoteResult> RemoveNoteFromList(List<Note> notesList)
        //{
        //    if (notesList == null)
        //    {
        //        throw new ArgumentNullException(nameof(notesList));
        //    }

        //    var noteResult = new List<NoteResult>();

        //    foreach (var note in notesList)
        //    {
        //        var existingNote = notesList.FirstOrDefault(x => x.Id == note.Id);

        //        if (existingNote != null)
        //        {
        //            existingNote.Title = note.Title;
        //            existingNote.Description = note.Description;
        //            existingNote.IsDone = note.IsDone;

        //            noteResult.Add(new NoteResult
        //            {
        //                Id = existingNote.Id,
        //                Title = existingNote.Title,
        //                Description = existingNote.Description,
        //                IsDone = existingNote.IsDone,
        //            });
        //        }
        //        else
        //        {
        //            noteResult.Add(new NoteResult
        //            {
        //                Id = note.Id,
        //                Title = note.Title,
        //                Description = note.Description,
        //                IsDone = note.IsDone,
        //            });
        //        }
        //    }

        //    return noteResult;
        //}  
    }
}