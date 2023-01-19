using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using System.Linq;
using WebApi.DBOperations;
using WebApi.Common;

namespace WebApi.BookOperations.UpdateBook
{
    public class UpdateBookCommand
    {
        private readonly BookStoreDBContext _context;
        public int BookId {get;set;}
        public UpdateBookModel Model {get;set;}

        public UpdateBookCommand(BookStoreDBContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var book = _context.Books.SingleOrDefault(x=>x.Id == BookId);

            if(book is null)
            {
                throw new InvalidOperationException("Güncellenecek kitap bulunamadi");
            }

            book.GenreId = Model.GenreId != default ? Model.GenreId:book.GenreId;
            //book.PageCount = updatedBook.PageCount != default ? updatedBook.PageCount:book.PageCount;
            //book.PublishDate = updatedBook.PublishDate != default ? updatedBook.PublishDate:book.PublishDate;
            book.Title = Model.Title != default ? Model.Title:book.Title;

            _context.SaveChanges();
            
        }

    public class UpdateBookModel
    {
        public string Title {get;set;}
        public int GenreId {get;set;}
    }

    }

}