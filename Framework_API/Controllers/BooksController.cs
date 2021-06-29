using Framework_API.DataSource.Interface;
using Framework_API.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Framework_API.Controllers
{
    public class BooksController : Controller
    {        
        private readonly IBookRepository _bookRepository;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly ILogger<BooksController> _logger;

        public BooksController(IBookRepository bookRepository, IWebHostEnvironment hostingEnvironment, ILogger<BooksController> logger)
        {
            _bookRepository = bookRepository;
            _hostingEnvironment = hostingEnvironment;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("Listing all books");
            return View(await _bookRepository.FetchAll().ToListAsync());
        }

        public IActionResult Insert()
        {
            _logger.LogInformation("New book");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Insert([Bind("BookId,Title,AuthorFullName,Edition,Year,NumberOfPages,Photo")] Book book, IFormFile file)
        {
            // ** O parametro da classe IFormFile DEVE TER O MESMO NOME que o atributo "name" do input onde será enviada a foto **
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    _logger.LogInformation("Creating folder link");
                    var linkUpload = Path.Combine(_hostingEnvironment.WebRootPath, "Images");

                    using (FileStream fileStream = new FileStream(Path.Combine(linkUpload, file.FileName), FileMode.Create))
                    {
                        _logger.LogInformation("Copying file to folder");
                        await file.CopyToAsync(fileStream);
                        _logger.LogInformation("File copied");
                        book.Photo = "~/Images/" + file.FileName;
                    }
                }
                _logger.LogInformation("Saving new book");

                await _bookRepository.Insert(book);
                return RedirectToAction(nameof(Index));
            }
            _logger.LogError("Invalid data");
            return View(book);
        }

        public async Task<IActionResult> Edit(int id)
        {

            _logger.LogInformation("Update book");

            var book = await _bookRepository.FetchById(id);
            if (book == null)
            {
                _logger.LogError("Book not found");
                return NotFound();
            }

            TempData["BookPhoto"] = book.Photo;            
            return View(book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BookId,Title,AuthorFullName,Edition,Year,NumberOfPages,Photo")] Book book, IFormFile file)
        {
            // ** O parametro da classe IFormFile DEVE TER O MESMO NOME que o atributo "name" do input onde será enviada a foto **
            if (id != book.BookId)
            {
                _logger.LogError("Book not found");
                return NotFound();
            }
            book.Photo = TempData["BookPhoto"].ToString();
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    _logger.LogInformation("Creating link to folder");
                    var linkUpload = Path.Combine(_hostingEnvironment.WebRootPath, "Images");
                    

                    using (FileStream fileStream = new FileStream(Path.Combine(linkUpload, file.FileName), FileMode.Create))
                    {
                        _logger.LogInformation("Copying file to folder");
                        await file.CopyToAsync(fileStream);
                        _logger.LogInformation("File copied");
                        book.Photo = "~/Images/" + file.FileName;                        
                    }
                }

                else book.Photo = TempData["BookPhoto"].ToString();

                _logger.LogInformation("Updating book");
                await _bookRepository.Update(book);
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        [HttpPost]
        public async Task<JsonResult> Delete(int id)
        {
            var book = await _bookRepository.FetchById(id);
            string bookPhoto = book.Photo;
            bookPhoto = bookPhoto.Replace("~", "wwwroot");
            System.IO.File.Delete(bookPhoto);

            _logger.LogInformation("Removing book");
            await _bookRepository.Remove(id);
            return Json("Book removed");
        }
    }
}
