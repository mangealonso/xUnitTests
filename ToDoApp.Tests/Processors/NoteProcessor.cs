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

        public List<NoteResult> SaveList(List<Note> notesList)
        {
            if (notesList == null)
            {
                throw new ArgumentNullException(nameof(notesList));
            }

            var noteResult = new List<NoteResult>();

            foreach (var note in notesList)
            {
                noteResult.Add(Save(note));
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
    }
}