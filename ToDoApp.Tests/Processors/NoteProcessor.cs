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
        public NoteResult Save(string title, string description)
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

        public List<NoteResult> SaveList(List<Note> notesList)
        {
            if (notesList == null)
            {
                throw new ArgumentNullException(nameof(notesList));
            }

            var noteResult = new List<NoteResult>();

            foreach (var note in notesList)
            {
                noteResult.Add(Save(note.Title, note.Description));
            }

            return noteResult;
        }

        public List<NoteResult> SaveUpdatedList(List<Note> notesList)
        {
            if (notesList == null)
            {
                throw new ArgumentNullException(nameof(notesList));
            }

            var noteResult = new List<NoteResult>();

            foreach (var note in notesList)
            {
                var existingNote = notesList.FirstOrDefault(x => x.Id == note.Id);

                if (existingNote != null)
                {
                    existingNote.Title = note.Title;
                    existingNote.Description = note.Description;
                    existingNote.IsDone = note.IsDone;

                    noteResult.Add(new NoteResult
                    {
                        Id = existingNote.Id,
                        Title = existingNote.Title,
                        Description = existingNote.Description,
                        IsDone = existingNote.IsDone,
                    });
                }
                else
                {
                    noteResult.Add(new NoteResult
                    {
                        Id = note.Id,
                        Title = note.Title,
                        Description = note.Description,
                        IsDone = note.IsDone,
                    });
                }
            }

            return noteResult;
        }

        public bool Delete(List<Note> notes, int idToDelete)
        {
            if (idToDelete <= 0 || notes == null)
            {
                throw new ArgumentNullException(nameof(idToDelete) + " or " + nameof(notes));
            }

            var noteToDelete = notes.FirstOrDefault(x => x.Id == idToDelete);

            if (noteToDelete != null)
            {
                notes.Remove(noteToDelete);
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