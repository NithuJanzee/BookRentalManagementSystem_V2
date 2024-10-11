using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BookRentalManagementSystem_V2
{
    internal class BookRepository
    {
        Book Book = new Book();
        private readonly string ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB ; Initial Catalog =BookRentalManagement ; Integrated Security = True ";

        //Add Book
        public void AddBook(Book book)
        {
            using(SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                string querry ="INSERT INTO Books(BookId , Title , Author , RentalPrice) VALUES(@bookId, @title, @author, @rentalPrice)";
                using(SqlCommand command = new SqlCommand(querry , connection))
                {
                    command.Parameters.AddWithValue("@bookId", book.BookId);
                    command.Parameters.AddWithValue("@title", book.Title);
                    command.Parameters.AddWithValue("@author", book.Author);
                    command.Parameters.AddWithValue("rentalPrice", book.RentalPrice);

                    command.ExecuteNonQuery();
                }
            }
        }

        //view All Books
        public List<Book> GetAllBooks()
        {
            List<Book> list = new List<Book>();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                string querry = "SELECT * FROM Books";
                using( SqlCommand command = new SqlCommand( querry , connection))
                {
                    using(SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new Book()
                            {
                                BookId = reader.GetString(0),
                                Title = reader.GetString(1),
                                Author = reader.GetString(2),
                                RentalPrice = reader.GetDecimal(3),
                            });
                        }
                    }
                }
            }
            return list;
        }

        //Get By ID
        public Book GetBookById(string bookId)
        {
            Book book = new Book();
            using(SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                string querry = "SELECT * FROM Books WHERE BookId = @bookID";
                using(SqlCommand command = new SqlCommand(querry , connection))
                {
                    command.Parameters.AddWithValue("@bookID", bookId);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            book = new Book()
                            {
                                BookId=reader.GetString(0),
                                Title = reader.GetString(1),
                                Author = reader.GetString(2),
                                RentalPrice=reader.GetDecimal(3),
                            };
                        }
                    }
                }
            }
            return book;
        }

        //Update Book
        public void UpdateBook(Book book)
        {
           using(SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                string querry = "UPDATE Books SET Title = @title , Author = @author , RentalPrice = @rental WHERE BookId = @bookid";
                using(SqlCommand command=new SqlCommand(querry , connection))
                {
                    command.Parameters.AddWithValue("@bookid" , book.BookId);
                    command.Parameters.AddWithValue ("@title" , book.Title);
                    command.Parameters.AddWithValue("@author", book.Author);
                    command.Parameters.AddWithValue("@rental", book.RentalPrice);
                    int affectedRow = command.ExecuteNonQuery();
                    if (affectedRow > 0)
                    {
                        Console.WriteLine("Book Updated Succsfully");
                    }
                    else 
                    {
                        Console.WriteLine("Book Not Found");
                    }
                }
            }
        }

    }
}
