using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Program
{
    public class BookLibrary
    {
        private string name;
        private List<Book> books;

        public BookLibrary(string name)
        {
            this.Name = name;
            this.books = new List<Book>();
        }
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }
        public List<Book> Books
        {
            get
            {
                return this.books;
            }
            set
            {
                this.books = value;
            }
        }
        public void AddBook(string title, double rating)
        {
            Book book = new Book(title, rating);
            books.Add(book);
        }
        public double AverageRating()
        {
            return books.Average(book => book.Rating);
        }
        public List<string> GetBooksByRating(double rating)
        {
            List<Book> booksRating = books.Where(x => x.Rating > rating).ToList();
            List<string> booksName = new List<string>();
            foreach (Book book in booksRating)
            {
                booksName.Add(book.Title);
            }
            return booksName;
        }
        public List<Book> SortByTitle()
        {
            var resultSort = books.OrderBy(x => x.Title).ToList();
            books = resultSort;
            return books;
        }
        public List<Book> SortByRating()
        {
            var sortRating = books.OrderByDescending(x => x.Rating).ToList();
            books = sortRating;
            return books;
        }
        public string[] ProvideInformationAboutAllBooks()
        {
            string[] printBooks = new string[books.Count];
            int index = 0;
            foreach (var book in books)
            {
                string text = $"Book {book.Title} is with {book.Rating:f1} rating.";
                printBooks[index] = text;
                index++;
            }
            return printBooks;
        }

        public bool CheckBookIsInBookLibrary(string title)
        {
            var resultSearch = books.Where(x => x.Title == title).ToList();
            if (resultSearch.Count > 0)
            {
                return true;
            }
            return false;
        }
    }
}

