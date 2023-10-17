using Library.API.Controllers;
using Library.API.Data.Services;

namespace LibraryAPI.Test
{
    public class BooksControllerTest
    {
       BooksController _controller;
       IBookService _service;

        public BooksControllerTest()
        {
            _service = new BookService();
            _controller = new BooksController( _service );
        }


    }
}