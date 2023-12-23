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
        public void Should_Return_Note_With_Same_Id()
        {
            // ARRANGE
            var note = new Note
            {
                Id = 15,
                Title = "Test note",
                Description = "Test description",
                IsDone = false
            };

            // ACT
            NoteResult result = _processor.Save(note);

            // ASSERT
            Assert.NotNull(result);
            Assert.Equal(note.Id, result.Id);
        }

        [Fact]
        public void Should_Return_Note_With_Details()
        {
            // ARRANGE
            var note = new Note
            {
                Id = 1,
                Title = "Test note",
                Description = "Test description",
                IsDone = false
            };

            // ACT
            NoteResult result = _processor.Save(note);

            // ASSERT
            Assert.NotNull(result);

            Assert.Equal(note.Id, result.Id);
            Assert.Equal(note.Title, result.Title);
            Assert.Equal(note.Description, result.Description);
            Assert.Equal(note.IsDone, result.IsDone);
        }        

        [Fact]
        public void Should_Throw_Exception_For_Null_Result()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => _processor.Save(null!));

            Assert.Equal("note", exception.ParamName);
        }

        [Fact]
        public void Should_Return_List_Of_Notes_With_Details()
        {
            // ARRANGE
            var notes = new List<Note>
            {
                new Note 
                {
                    Id = 1,
                    Title = "Test note 1",
                    Description = "Test description 1",
                    IsDone = false
                },

                new Note
                {
                    Id = 2,
                    Title = "Test note 2",
                    Description = "Test description 2",
                    IsDone = true
                }
            };

            var expectedNotes = new List<NoteResult>
            {
                new NoteResult
                {
                    Id = 1,
                    Title = "Test note 1",
                    Description = "Test description 1",
                    IsDone = false
                },

                new NoteResult
                {
                    Id = 2,
                    Title = "Test note 2",
                    Description = "Test description 2",
                    IsDone = true
                }
            };

            // ACT
            var results = _processor.SaveList(notes);

            // ASSERT
            for (int i = 0; i < expectedNotes.Count; i++)
            {
                Assert.NotNull(results[i]);

                Assert.Equal(expectedNotes[i].Id, results[i].Id);
                Assert.Equal(expectedNotes[i].Title, results[i].Title);
                Assert.Equal(expectedNotes[i].Description, results[i].Description);
                Assert.Equal(expectedNotes[i].IsDone, results[i].IsDone);
            }
        }

        [Fact]
        public void Should_Return_Empty_List_When_Input_Is_Empty()
        {
            // ARRANGE
            var emptyNotes = new List<Note>();

            // ACT
            var results = _processor.SaveList(emptyNotes);

            // ASSERT
            Assert.NotNull(results);
            Assert.Empty(results);
        }
    }
}
