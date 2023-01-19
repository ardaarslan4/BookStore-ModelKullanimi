using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using System.Linq;
using WebApi.DBOperations;
using WebApi.Common;




namespace WebApi.BookOperations.GetBookDetail
{
    public class GetBookDetailQuery
    {
        private readonly BookStoreDBContext _dbContext;
        public int BookId {get;set;}
        
        public GetBookDetailQuery(BookStoreDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public BookDetailViewModel Handle()
        {
            var book = _dbContext.Books.Where(book=>book.Id == BookId).SingleOrDefault();
            if(book is null)
            {
                throw new InvalidOperationException("Kitap bulunamadi");
            }
            BookDetailViewModel vm = new BookDetailViewModel();
            vm.Title = book.Title;
            vm.PageCount = book.PageCount;
            vm.PublishDate = book.PublishDate.Date.ToString("dd/MM/yyyy");
            vm.Genre = ((GenreEnum)book.GenreId).ToString();
            return vm;
        }
    }

    public class BookDetailViewModel
    {
        public string Title {get;set;}
        public string Genre{get;set;}
        public int PageCount{get;set;}
        public string PublishDate{get;set;}
    }
}
