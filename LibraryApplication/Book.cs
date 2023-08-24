namespace LibraryApplication;

public class Book
{
    
    public int id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public string publisher { get; set; }
    public int Year { get; set; }
    public string isbn { get; set; }
    public string description { get; set; }
    public string category { get; set; }


    public Book(int id, string title, string author, string publisher, int year, string isbn, string description, string category)
    {
        this.id = id;
        Title = title;
        Author = author;
        this.publisher = publisher;
        Year = year;
        this.isbn = isbn;
        this.description = description;
        this.category = category;
    }


    public override string ToString()
    {
        return $"{Title} - {Author} ";
    }
}