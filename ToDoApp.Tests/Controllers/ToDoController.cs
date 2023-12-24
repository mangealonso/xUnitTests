using Microsoft.AspNetCore.Mvc;

namespace ToDoApp.Controllers
{
    public class ToDoController : Controller
    {
        private readonly NoteProcessor _processor;
        public ToDoController(NoteProcessor processor) 
        {
            _processor = processor;
        }

        public IActionResult Index() 
        {
            var allToDos = _processor.GetAllToDos();

            if (allToDos.Count > 0)
            {
                return View(allToDos);
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public IActionResult Save(string titleText, string descriptionText)
        {
            _processor.SaveNote(titleText, descriptionText);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            _processor.DeleteNote(id);

            return RedirectToAction("Index");
        }
    }
}
