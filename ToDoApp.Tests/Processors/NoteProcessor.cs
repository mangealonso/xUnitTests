using ToDoApp.Models;

namespace ToDoApp
{
    public class NoteProcessor
    {
        public NoteProcessor()
        {
        }

        // To keep track of the id´s of the ToDos
        private int nextId = 0;

        // A list for all the current ToDos
        private List<NoteResult> allToDos = new List<NoteResult>();

        public NoteResult SaveToDo(string title, string description)
        {
            if (title == null)
            {
                throw new ArgumentNullException(nameof(title));
            }

            if (description == null)
            {
                throw new ArgumentNullException(nameof(description));
            }

            // Setting up the creation of the ToDo, incrementing the Id and setting IsDone to false
            var note = new Note
            {
                Id = nextId++,
                Title = title,
                Description = description,
                IsDone = false,
            };

            // Creating the ToDo
            var result = new NoteResult
            {
                Id = note.Id,
                Title = note.Title,
                Description = note.Description,
                IsDone = note.IsDone,
            };

            // Adding the ToDo to the list
            allToDos.Add(result);

            return result;
        }

        // Method to toggle the status of the ToDo from Incomplete to Complete and vice versa
        public void ToggleToDoStatus(int idToChange)
        {
            if (allToDos == null)
            {
                throw new ArgumentNullException(nameof(allToDos));
            }

            var noteToChange = allToDos.FirstOrDefault(x => x.Id == idToChange);

            if (noteToChange != null)
            {
                if (noteToChange.IsDone == false)
                {
                    noteToChange.IsDone = true;
                }
                else
                {
                    noteToChange.IsDone = false;
                }
            }
            else
            {

            }
        }

        public bool DeleteCompletedToDo(int idToDelete)
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

        public void DeleteAllCompletedToDos()
        {
            var allCompletedToDos = GetAllCompletedToDos();

            if (allCompletedToDos == null)
            {
                throw new ArgumentNullException(nameof(allCompletedToDos));
            }
            else
            {
                foreach (var toDo in allCompletedToDos)
                {
                    allToDos.Remove(toDo);
                }
            }
        }

        public List<NoteResult> GetAllToDos()
        {
            return allToDos;
        }
                       

        public List<NoteResult> GetAllCompletedToDos()
        {
            return allToDos.Where(x => x.IsDone == true).ToList();
        }
    }
}