using Library.API.Controllers;
using Library.API.Data.Models;
using Library.API.Data.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Test
{
    public class BooksControllerTest
    {
        private BooksController _controller;
        private IBookService _service;

        public BooksControllerTest()
        {
            _service = new BookService(); // You should have a BookService class implementing IBookService.
            _controller = new BooksController(_service);
        }

        [Fact]
        public void GetAllTest()
        {
            // Arrange

            // Act
            var result = _controller.Get();

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);

            var okResult = result.Result as OkObjectResult;
            Assert.IsType<List<Book>>(okResult.Value);

            var listBooks = okResult.Value as List<Book>;
            Assert.Equal(5, listBooks.Count);
        }

        [Theory]
        [InlineData("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200", "ab2bd817-98cd-4cf3-a80a-53ea0cd9c111")]
        public void GetBookByIdTest(string guid1, string guid2)
        {
            // Arrange
            var validGuid = new Guid(guid1);
            var invalidGuid = new Guid(guid2);

            // Act
            var notFoundResult = _controller.Get(invalidGuid);
            var okResult = _controller.Get(validGuid);

            // Assert
            Assert.IsType<NotFoundResult>(notFoundResult.Result);
            Assert.IsType<OkObjectResult>(okResult.Result);

            var item = okResult.Result as OkObjectResult;
            Assert.IsType<Book>(item.Value);

            var bookItem = item.Value as Book;
            Assert.Equal(validGuid, bookItem.Id);
            Assert.Equal("Managing Oneself", bookItem.Title);
        }

        [Fact]
        public void AddBookTest()
        {
            //arrange
            var completeBook = new Book()
            {
                Author = "Author",
                Description = "Description",
            };

            //act
            var createdResponse = _controller.Post(completeBook);
            //assert
            Assert.IsType<CreatedAtActionResult>(createdResponse);
            var item = createdResponse as CreatedAtActionResult;
            Assert.IsType<Book>(item.Value);

            var bookItem = item.Value as Book;
            Assert.Equal(completeBook.Author, bookItem.Author);
            Assert.Equal(completeBook.Title, bookItem.Title);
            Assert.Equal(completeBook.Description, bookItem.Description);

            //arrange
            //arrange
            var incompleteBook = new Book()
            {
                Author = "Author",
                Description = "Description",
            };

            //act
            _controller.ModelState.AddModelError("Title", "Title is required field");
            var badResponse = _controller.Post(incompleteBook);

            //assert
            Assert.IsType<BadRequestObjectResult>(badResponse);
        }
    }
}