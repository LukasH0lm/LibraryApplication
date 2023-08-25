using System.IO;
using System.Text.Json;

namespace LibraryApplication;

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public string Publisher { get; set; }
    public int Year { get; set; }
    public string Isbn { get; set; }
    public string Description { get; set; }
    public string Category { get; set; }

    public bool IsAvailable { get; set; }


    public Book(int id, string title, string author, string publisher, int year, string isbn, string description,
        string category)
    {
        Id = title.GetHashCode(); //is this best practice? change of collision?
        Title = title;
        Author = author;
        Publisher = publisher;
        Year = year;
        Isbn = isbn;
        Description = description;
        Category = category;
        IsAvailable = true;
    }


    public void AddToLibrary()
    {
        string path = @"C:\Users\lukas\RiderProjects\LibraryApplication\LibraryApplication\Books\";
        string jsonString = JsonSerializer.Serialize(this);
        File.WriteAllText(path + Title + ".json", jsonString);
    }


    public override string ToString()
    {
        return $"{Title} - {Author} ";
    }
}