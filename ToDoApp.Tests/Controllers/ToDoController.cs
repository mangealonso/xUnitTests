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

            // Displaying the ToDos, if there are any. Otherwise just return the default View
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

            _processor.SaveToDo(titleText, descriptionText);

            return RedirectToAction("Index");
        }

        // Since it´s an application with only one page, Index, this method is HttpPost instead of HttpPut
        [HttpPost]
        public IActionResult ToggleStatus(int id)
        {
            _processor.ToggleToDoStatus(id);

            return RedirectToAction("Index");
        }

        // Since it´s an application with only one page, Index, this method is HttpPost instead of HttpDelete
        [HttpPost]
        public IActionResult DeleteCompleted(int id)
        {
            _processor.DeleteCompletedToDo(id);

            return RedirectToAction("Index");
        }

        // Since it´s an application with only one page, Index, this method is HttpPost instead of HttpDelete
        [HttpPost]
        public IActionResult DeleteAllCompleted()
        {
            _processor.DeleteAllCompletedToDos();

            return RedirectToAction("Index");
        }
    }
}
