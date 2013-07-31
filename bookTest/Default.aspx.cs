using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using bookTest.Code;

namespace bookTest
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {  
            //  Add books
            Book book1 = new Book(
                "How to Land Your Dream Job: No Resume!",
                "Jeffrey J. Fox",
                5);
            book1.Save();
          
            Book book2 = new Book(
                "How to Turn and Interview Into a Job",
                "Jeffrey G. Allen",
                3);
            book2.Save();
         
            Book book3 = new Book(
                "The Smart Interviewer",
                "Bradford D. Smart",
                1);
            book3.Save();
          
            Book book4 = new Book(
                "In Search Of The Perfect Job",
                "Clyde C. Lowstuter",
                3);
            book4.Save(); 
           
            Book book5 = new Book(
                "Resumes For Dummies",
                "Joyce Lain Kennedy",
                6);
            book5.Save();
           
            Book book6 = new Book(
                "2001 Best Questions To Ask On Your Interview!",
                "John Kador",
                6);
            book6.Save();

            // Display a list of books in the system
            BookList bookList = new BookList();
            foreach(Book savedBook in bookList.Books)
                Response.Write("Book Author: " + savedBook.Author + " Title: " + savedBook.Title +
                    " Copies: " + savedBook.Copies + "<br>");


            // Create and save an order of books
            BookOrder bookOrder = new BookOrder();

            bookOrder.Books.Add(bookList.Books[0]);
            bookOrder.Books.Add(bookList.Books[3]);
            bookOrder.Books.Add(bookList.Books[5]);            

            bookOrder.Save();


            // Display a list of books that have been ordered
            BookList orderedBooks = new BookList(BookList.States.Ordered);

            foreach (Book orderedBook in orderedBooks.Books)
                Response.Write("Ordered Book Author: " + orderedBook.Author + " Title: " + orderedBook.Title +
                    " Copies: " + orderedBook.Copies + "<br>");


            // Display a list of books that have never been ordered
            BookList unorderedBooks = new BookList(BookList.States.Unordered);

            foreach (Book unorderedBook in unorderedBooks.Books)
                Response.Write("Unordered Book Author: " + unorderedBook.Author + " Title: " + unorderedBook.Title +
                    " Copies: " + unorderedBook.Copies + "<br>");
        }
    }
}