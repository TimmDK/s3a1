using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookLibrary;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RestApiBook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private static readonly List<Book> books = new List<Book>()
        {
            new Book("Bog 1", "Nikolaj Dufke", "1234567890123", 200),
            new Book("Bog 2", "Nikolaj Dufke", "1334567890123", 200),
            new Book("Bog 5", "Nikolaj Dufke", "5234567890123", 200)

        };

        // GET: api/Books
        [HttpGet]
        public IEnumerable<Book> Get()
        {
            return books;
        }

        // GET: api/Books/5
        [HttpGet("{isbn13}", Name = "Get")]
        public Book Get(string isbn13)
        {
            return books.Find(b => b.Isbn13 == isbn13);
        }

        // POST: api/Books
        [HttpPost]
        public void Post([FromBody] Book value)
        {
            books.Add(value);
        }

        // PUT: api/Books/5
        [HttpPut("{isbn13}")]
        public void Put(string isbn13, [FromBody] Book value)
        {
            Book b = Get(isbn13);
            if (b != null)
            {
                b.Author = value.Author;
                b.Title = value.Title;
                b.Pages = value.Pages;
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{isbn13}")]
        public void Delete(string isbn13)
        {
            Book b = Get(isbn13);
            books.Remove(b);
        }
    }
}
