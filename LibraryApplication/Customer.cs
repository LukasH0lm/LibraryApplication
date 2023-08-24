using System.Collections.Generic;

namespace LibraryApplication;

public class Customer
{
    
    int Id { get; set; }
    string FirstName { get; set; }
    string LastName { get; set; }
    string Address { get; set; }
    string PhoneNumber { get; set; }
    string Email { get; set; }
    
    List<Book> Books { get; set; }

    public Customer(int id, string firstName, string lastName, string address, string phoneNumber, string email, List<Book> books)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Address = address;
        PhoneNumber = phoneNumber;
        Email = email;
        Books = books;
    }
}