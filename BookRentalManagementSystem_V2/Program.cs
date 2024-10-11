using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookRentalManagementSystem_V2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            BookRepository bookRepository = new BookRepository();
            int choise;
            do
            {
                Console.WriteLine("\nBook Rental Managment System");
                Console.WriteLine("1. Add Book");
                Console.WriteLine("2. View All Books");
                Console.WriteLine("3. Update Books");
                choise = int.Parse(Console.ReadLine());

                switch (choise)
                {
                    case 1:
                        Console.Write("Enter BookID: ");
                        string BookId = Console.ReadLine();
                        Console.Write("Enter Title: ");
                        string title = Console.ReadLine();
                        Console.Write("Enter Author: ");
                        string author = Console.ReadLine();
                        Console.Write("Ener Price: ");
                        decimal rentalPrice = decimal.Parse(Console.ReadLine());

                        Book book = new Book
                        {
                            BookId = BookId,
                            Title = title,
                            Author = author,
                            RentalPrice = rentalPrice
                        };

                        bookRepository.AddBook(book);
                        Console.WriteLine("Book Created Sucssfuly");
                        break;

                        case 2:
                        var books = bookRepository.GetAllBooks();
                        foreach (var item in books)
                        {
                            Console.WriteLine(item.ToString());
                        }
                        break;

                        case 3:
                        Console.Write("Enter book Id: ");
                        string bookId = Console.ReadLine();
                        var findBook = bookRepository.GetBookById(bookId);
                        if (findBook != null)
                        {
                            Console.Write("Enter New Title: ");
                            findBook.Title = Console.ReadLine();
                            Console.Write("Enter New Author");
                            findBook.Author = Console.ReadLine();
                            Console.Write("Enter new Rental Price");
                            findBook.RentalPrice = decimal.Parse (Console.ReadLine());

                            bookRepository.UpdateBook(findBook);
                        }
                        break;
                }


            } while (choise != 5);
        }
    }
}
