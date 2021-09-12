using RestWithASPNETUdemy.Data.Converter.Contract;
using RestWithASPNETUdemy.Data.VO;
using RestWithASPNETUdemy.Model;
using System.Collections.Generic;
using System.Linq;

namespace RestWithASPNETUdemy.Data.Converter.Implementations
{
    public class BookConverter : IParser<BookVO, Book>, IParser<Book, BookVO>
    {
        public Book Parse(BookVO origem)
        {
            if (origem == null) { return null; }

            return new Book
            {
                Id = origem.Id,
               Author = origem.Author,
               LauchDate = origem.LauchDate,
               Price = origem.Price,
               Title = origem.Title
            };
        }

        public BookVO Parse(Book origem)
        {
            if (origem == null){ return null; }

            return new BookVO
            {
                Id = origem.Id,
                Author = origem.Author,
                LauchDate = origem.LauchDate,
                Price = origem.Price,
                Title = origem.Title
            }; 
        }

        public List<BookVO> Parse(List<Book> origem)
        {
            if (origem == null) { return null; }

            return origem.Select(item => Parse(item)).ToList();
        }

        public List<Book> Parse(List<BookVO> origem)
        {
            return origem.Select(item => Parse(item)).ToList();
        }
    }
}
