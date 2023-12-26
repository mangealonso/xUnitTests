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
            // ARRANGE

            var note = new Note 
            {
                Title = "Test note",
                Description = "Test description",
            };
            string title = "Test note";
            string description = "Test description";

            // ACT
            NoteResult result = _processor.SaveToDo(title, description);

            // ASSERT
            Assert.NotNull(result);

            Assert.Equal(default(int), result.Id);
            Assert.Equal(title, result.Title);
            Assert.Equal(description, result.Description);
            Assert.False(result.IsDone);
        }

        [Fact]
        public void Should_Throw_Exception_For_Null_Result()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => _processor.SaveToDo(null, "Test description"));

            Assert.Equal("title", exception.ParamName);
        }

        [Fact]
        public void Should_Return_List_Of_Notes_With_Details()
        {
            // ARRANGE
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

            var expectedNotes = new List<NoteResult>
            {
                new NoteResult
                {
                    Id = 0,
                    Title = "Test note 1",
                    Description = "Test description 1",
                    IsDone = false
                },

                new NoteResult
                {
                    Id = 1,
                    Title = "Test note 2",
                    Description = "Test description 2",
                    IsDone = false
                }
            };

            // ACT
            var results = _processor.AddToDoToList(notes);

            // ASSERT
            for (int i = 0; i < expectedNotes.Count; i++)
            {
                Assert.NotNull(results[i]);

                Assert.Equal(expectedNotes[i].Id, results[i].Id);
                Assert.Equal(expectedNotes[i].Title, results[i].Title);
                Assert.Equal(expectedNotes[i].Description, results[i].Description);
                Assert.False(results[i].IsDone);
            }
        }

        [Fact]
        public void Should_Return_Empty_List_When_Input_Is_Empty()
        {
            // ARRANGE
            var emptyNotes = new List<Note>();

            // ACT
            var results = _processor.AddToDoToList(emptyNotes);

            // ASSERT
            Assert.NotNull(results);
            Assert.Empty(results);
        }

        [Fact]
        public void Should_Delete_Note_Successfully()
        {
            // ARRANGE
            var processor = new NoteProcessor();

            processor.SaveToDo("Test note 1", "Test description 1");
            processor.SaveToDo("Test note 2", "Test description 2");

            int noteIdToDelete = 1;

            // ACT
            bool result = processor.DeleteCompletedToDo(noteIdToDelete);

            // ASSERT
            Assert.True(result);
        }
    }
}
