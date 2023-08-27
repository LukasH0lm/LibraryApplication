using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace LibraryApplication;

public class Customer
{
    public static int IdCounter = 1;

    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Address { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }

    public List<Book>? Books { get; set; }

    public Customer(int id, string firstName, string lastName, string address, string phoneNumber, string email,
        List<Book>? books) //should have called this user
    {
        Id = IdCounter++; //we only use the id for the ui, so we can just increment it
        FirstName = firstName;
        LastName = lastName;
        Address = address;
        PhoneNumber = phoneNumber;
        Email = email;
        Books = books; //not used
    }


    public Customer()
    {
    }

    public void serializeToJson()
    {
        string path = @"C:\Users\lukas\RiderProjects\LibraryApplication\LibraryApplication\Customers\";
        string jsonString = JsonSerializer.Serialize(this);
        File.WriteAllText(path + FirstName + LastName + ".json", jsonString);
    }

    public void AddToLibrary()
    {
        string path = @"C:\Users\lukas\RiderProjects\LibraryApplication\LibraryApplication\Customers\";
        string jsonString = JsonSerializer.Serialize(this);
        File.WriteAllText(path + FirstName + LastName + ".json", jsonString);
    }

    public void RemoveFromLibrary()
    {
        string path = @"C:\Users\lukas\RiderProjects\LibraryApplication\LibraryApplication\Customers\";
        File.Delete(path + FirstName + LastName + ".json");
    }
}