using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using System.Linq;
using WebApi.DBOperations;
using WebApi.BookOperations.CreateBook;
using static WebApi.BookOperations.CreateBook.CreateBookCommand;
using WebApi.BookOperations.GetBooksQuery;
using WebApi.BookOperations.GetBookDetail;
using WebApi.BookOperations.UpdateBook;
using static WebApi.BookOperations.UpdateBook.UpdateBookCommand;
using WebApi.BookOperations.DeleteBook;

namespace WebApi.AddControllers
{
    [ApiController]
    [Route("[controller]s")]
    public class BookController:ControllerBase
    {
        private readonly BookStoreDBContext _context;

        public BookController (BookStoreDBContext context)
        {
            _context = context;
        }
        /*
        private static List<Book> BookList = new List<Book>()
        {
            
            new Book
            {
                Id = 1,
                Title ="Lean Startup",
                GenreId =1, //PersonalGrowth
                PageCount =200,
                PublishDate = Convert.ToInt32(new DateOnly(2001,06,12))
            },

            new Book
            {
                Id = 2,
                Title ="Herland",
                GenreId =2, //ScienceFiction
                PageCount =250,
                PublishDate = Convert.ToInt32(new DateOnly(2010,05,23)) 

            },

            new Book
            {
                Id = 3,
                Title ="Herland",
                GenreId =2, //ScienceFiction
                PageCount =540,
                PublishDate = Convert.ToInt32(new DateOnly(2001,12,21)) 
            }

        };*/

        [HttpGet]
        public IActionResult GetBooks()
        {
            GetBooksQuery query = new GetBooksQuery(_context);
            var result = query.Handle();
            return Ok(result); //http 200 bilgisiyle result'ı dönecek. Bunu IActionResult ile kullanabiliyoruz sadece.
        }

        [HttpGet("{id}")]
        public IActionResult GetByID(int id)
        {
            BookDetailViewModel result;
            try
            {
                GetBookDetailQuery query = new GetBookDetailQuery(_context);
                query.BookId = id;
                result = query.Handle();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(result);
        }

        /*
        [HttpGet]
        public Book Get([FromQuery] string id)
        {
            var book = BookList.Where(book=> book.Id == Convert.ToInt32(id)).SingleOrDefault();
            return book;
        }
        */


        //Post
        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookModel newBook)
        {
            CreateBookCommand command = new CreateBookCommand(_context);
            try //Burada try yapısın kullanmamızın nedeni handle çağırıldığında hata mesajı dönerse kod kırılır bunu engellemeye çalışıyoruz.
            {
                command.Model = newBook;
                command.Handle();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
            /*
            var book = _context.Books.SingleOrDefault(x=>x.Title == newBook.Title);

            if(book is not null)
                return BadRequest();
            
            _context.Books.Add(newBook);
            _context.SaveChanges();
            */
            return Ok();
        }

        //Put
        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] UpdateBookModel updatedBook)
        {
            try
            {
                UpdateBookCommand command = new UpdateBookCommand(_context);
                command.BookId = id;
                command.Model = updatedBook;
                command.Handle();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();

        }

        //Delete
        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            try
            {
                DeleteBookCommand command = new DeleteBookCommand(_context);
                command.BookId = id;
                command.Handle();

            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
                 
            return Ok();
        }


    }
}