using Library.API.Controllers;
using Library.API.Data.Models;
using Library.API.Data.Services;
using Microsoft.AspNetCore.Mvc;

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

        [Fact]
        public void GetAllTest()
        {
            //arrange
            //act
            var result = _controller.Get();
            //assert
            Assert.IsType<OkObjectResult>(result.Result);

            var list = result.Result as OkObjectResult;
            Assert.IsType<List<Book>>(list.Value);

            var listBooks = list.Value as List<Book>;
            Assert.Equal(5, listBooks.Count);
        }


    }
}