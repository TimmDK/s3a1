using System;

namespace BookLibrary
{
    public class Book
    {
        #region Instance Fields
        private string _title;
        private string _author;
        private string _isbn13;
        private int _pages;
        #endregion

        #region constructors
        public Book()
        {

        }

        public Book(string title, string author, string isbn13, int pages)
        {
            _title = title;
            _author = author;
            _isbn13 = isbn13;
            _pages = pages;
        }
        #endregion

        #region Properties
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        public string Author
        {
            get { return _author; }
            set
            {
                if (value.Length < 2) throw new ArgumentException("Dit navn skal være længere end 2 karakter");
                _author = value;
            }
        }

        public string Isbn13
        {
            get { return _isbn13; }
            set
            {
                if (value.Length != 13) throw new ArgumentException("Isbn skal være 13 karaktere");
                _isbn13 = value;
            }
        }

        public int Pages
        {
            get { return _pages; }
            set
            {
                if (value <= 4 || value >= 1000) throw new ArgumentOutOfRangeException();
                _pages = value;
            }
        }
        #endregion

        #region Methods
        public override string ToString()
        {
            return $"Bookname: {Title}, Author: {Author}, Isbn: {Isbn13}";
        }
        #endregion
    }
}
