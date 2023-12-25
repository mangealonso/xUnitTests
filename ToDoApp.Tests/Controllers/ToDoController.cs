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
            if (string.IsNullOrWhiteSpace(titleText))
            {
                ModelState.AddModelError("Title", "Title is required.");
            }

            if (string.IsNullOrWhiteSpace(descriptionText))
            {
                ModelState.AddModelError("Description", "Description is required.");
            }

            if (!ModelState.IsValid)
            {
                var allToDos = _processor.GetAllToDos();
                ViewData["titleText"] = titleText;
                ViewData["descriptionText"] = descriptionText;
                return View("Index", allToDos);
            }

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
