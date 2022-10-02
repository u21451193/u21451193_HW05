using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using u21451193_HW05.Models;

namespace u21451193_HW05.Controllers
{
    public class HomeController : Controller
    {
        private DataService dataService = new DataService();
        
        public ActionResult Index()
        {
            BooksVM booksVM = null;
            booksVM = new BooksVM
            {
                Books = dataService.getAllBooks()
            };
            return View(booksVM);
        }

        public ActionResult StudentIndex()
        {
            Borrows b = new Borrows();
            StudentVM studentVM = new StudentVM();
            studentVM = new StudentVM
            {
                Students = dataService.getAllStudents()
            };
            return View(studentVM);
        }

        public ActionResult BookDetails(int ID)
        {
            List<Borrows> book = new List<Borrows>();
            book = dataService.getBookdByID(ID);

            return View(book);
        }
    }
}