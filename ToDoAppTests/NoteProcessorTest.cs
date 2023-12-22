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
    }
}
