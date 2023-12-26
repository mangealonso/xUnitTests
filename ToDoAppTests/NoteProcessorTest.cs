using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ToDoApp;
using ToDoApp.Models;

namespace ToDoAppTests
{
    public class NoteProcessorTest
    {
        public NoteProcessor _processor;

        public NoteProcessorTest() 
        {
            _processor = new NoteProcessor();
        }

        [Fact]
        public void Should_Return_Note_To_Save_With_Details()
        {
            // ARRANGE - Creating the necessary values for a new ToDo
            string title = "Test note";
            string description = "Test description";

            // ACT - Creating the ToDo by calling the SaveToDo method
            NoteResult result = _processor.SaveToDo(title, description);

            // ASSERT - Checking so that the ToDo is created with the same values that was sent to the SaveToDo method
            Assert.NotNull(result);

            Assert.Equal(default(int), result.Id);
            Assert.Equal(title, result.Title);
            Assert.Equal(description, result.Description);
            Assert.False(result.IsDone);
        }

        [Fact]
        public void Should_Throw_Exception_For_Null_Result()
        {
            // ARRANGE & ACT - Calling the SaveToDo method and sending one null value and one valid value
            var exception = Assert.Throws<ArgumentNullException>(() => _processor.SaveToDo(null, "Test description"));

            // ASSERT - Checking so that an exception is thrown for the missing value
            Assert.Equal("title", exception.ParamName);
        }

        [Fact]
        public void Should_Return_List_Of_Notes_With_Details()
        {
            // ARRANGE - Creating a list with two ToDos to be created
            var notes = new List<Note>
            {
                new Note 
                {
                    Title = "Test note 1",
                    Description = "Test description 1"
                },

                new Note
                {
                    Title = "Test note 2",
                    Description = "Test description 2"
                }
            };

            var results = new List<NoteResult>();

            foreach (var note in notes)
            {
                var result = _processor.SaveToDo(note.Title, note.Description);
                results.Add(result);
            }

            // ACT - Calling the method for getting all ToDos
            var retrievedNotes = _processor.GetAllToDos();

            // ASSERT - Checking so that the retrieved notes match the expected note
            Assert.NotNull(retrievedNotes);
            Assert.Equal(results.Count, retrievedNotes.Count);

            for (int i = 0; i < retrievedNotes.Count; i++)
            {
                Assert.NotNull(results[i]);

                Assert.Equal(results[i].Id, retrievedNotes[i].Id);
                Assert.Equal(results[i].Title, retrievedNotes[i].Title);
                Assert.Equal(results[i].Description, retrievedNotes[i].Description);
                Assert.Equal(results[i].IsDone, retrievedNotes[i].IsDone);
            }
        }

        [Fact]
        public void Should_Change_Status_After_Toggle()
        {
            // ARRANGE - Creating a ToDo, where the default value for IsDone is set to false
            var note = _processor.SaveToDo("Test note 1", "Test description 1");

            // ACT - Changing the value for IsDone is set to true for the ToDo that has been created
            _processor.ToggleToDoStatus(note.Id);

            var updatedNote = _processor.GetAllToDos().FirstOrDefault(x => x.Id == note.Id);

            // ASSERT - Checking so that the value for IsDone is set to true
            Assert.NotNull(updatedNote);
            Assert.True(updatedNote.IsDone);
        }

        [Fact]
        public void Should_Delete_Note_Successfully()
        {
            // ARRANGE - Creating two ToDos and then creating an integer that represents the Id for which of the ToDos that should be deleted
            _processor.SaveToDo("Test note 1", "Test description 1");
            _processor.SaveToDo("Test note 2", "Test description 2");

            int noteIdToDelete = 1;

            // ACT - Calling the method with the Id for the note to be deleted to see if the deletion will be a success
            bool result = _processor.DeleteCompletedToDo(noteIdToDelete);

            // ASSERT - Checking that the deletion was a success
            Assert.True(result);
        }
    }
}
