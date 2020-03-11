using System;
using BookLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BookLibraryTest
{
    [TestClass]
    public class UnitTest1
    {
        private Book _book;

        [TestInitialize]
        public void Init()
        {
            _book = new Book();
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestAuthor()
        {
            _book.Author = "N";
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestPages()
        {
            _book.Pages = 3;
            _book.Pages = 1001;

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestIsbn()
        {
            _book.Isbn13 = "12345678901231";
            _book.Isbn13 = "123456789012";
        }
    }
}
