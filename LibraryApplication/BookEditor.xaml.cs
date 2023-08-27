using System;
using System.IO;
using System.Text.Json;
using System.Windows;

namespace LibraryApplication;

public partial class BookEditor : Window
{
    private Book currentBook = null;

    public BookEditor(BookState state, Book book, MainWindow mainWindow)
    {
        InitializeComponent();

        Closing += mainWindow.OnEditorClosing;


        currentBook = book;

        if (state == BookState.New)
        {
            BtnSubmit.Content = "Create";
        }
        else if (state == BookState.Edit)
        {
            BtnSubmit.Content = "Save";

            TxtBookTitle.Text = book.Title;
            TxtBookAuthor.Text = book.Author;
            TxtBookPublisher.Text = book.Publisher;
            TxtBookYear.Text = book.Year.ToString();
            TxtBookIsbn.Text = book.Isbn;
            TxtBookDescription.Text = book.Description;
            TxtBookCategory.Text = book.Category;
        }
        else if (state == BookState.Delete)
        {
            BtnSubmit.Content = "Delete";

            TxtBookTitle.Text = book.Title;
            TxtBookAuthor.Text = book.Author;
            TxtBookPublisher.Text = book.Publisher;
            TxtBookYear.Text = book.Year.ToString();
            TxtBookIsbn.Text = book.Isbn;
            TxtBookDescription.Text = book.Description;
            TxtBookCategory.Text = book.Category;

            TxtBookTitle.IsEnabled = false;
            TxtBookAuthor.IsEnabled = false;
            TxtBookPublisher.IsEnabled = false;
            TxtBookYear.IsEnabled = false;
            TxtBookIsbn.IsEnabled = false;
            TxtBookDescription.IsEnabled = false;
            TxtBookCategory.IsEnabled = false;
        }
    }

    private void BtnSubmit_OnClick(object sender, RoutedEventArgs e)
    {
        if (BtnSubmit.Content == "Create")
        {
            Book newBook = new Book(0, TxtBookTitle.Text, TxtBookAuthor.Text, TxtBookPublisher.Text,
                int.Parse(TxtBookYear.Text), TxtBookIsbn.Text, TxtBookDescription.Text, TxtBookCategory.Text);
            newBook.AddToLibrary();

            MessageBox.Show("Book created successfully!");
        }
        else if (BtnSubmit.Content == "Save")
        {
            currentBook.RemoveFromLibrary();

            Book newBook = new Book(0, TxtBookTitle.Text, TxtBookAuthor.Text, TxtBookPublisher.Text,
                int.Parse(TxtBookYear.Text), TxtBookIsbn.Text, TxtBookDescription.Text, TxtBookCategory.Text);

            newBook.AddToLibrary();

            MessageBox.Show("Book saved successfully!");
        }

        else if (BtnSubmit.Content == "Delete")
        {
            currentBook.RemoveFromLibrary();

            MessageBox.Show("Book deleted successfully!");
        }


        Close();
    }

    private void BtnGenerateIsbn_OnClick(object sender, RoutedEventArgs e)
    {
        TxtBookIsbn.Text = IsbnGenerator.GenerateIsbn();
    }
}

internal static class IsbnGenerator
{
    public static string GenerateIsbn()
    {
        bool isValid = false;

        string isbn = "";

        while (!isValid)
        {
            isbn = GenerateIsbnString();
            isValid = CheckIsbn(isbn);
            if (isValid)
            {
                return isbn;
            }
        }


        return isbn;
    }

    private static bool CheckIsbn(string isbn)
    {
        string path = @"C:\Users\lukas\RiderProjects\LibraryApplication\LibraryApplication\Books";

        string[] files = Directory.GetFiles(path);

        foreach (string file in files)
        {
            string json = File.ReadAllText(file);
            Book book = JsonSerializer.Deserialize<Book>(json);
            if (book.Isbn == isbn)
            {
                return false;
            }
        }

        return true;
    }

    private static string GenerateIsbnString()
    {
        //example isbn: 978-0-618-00228-0


        string isbn = "";

        Random random = new Random();

        isbn += random.Next(100, 999).ToString();
        isbn += "-";
        isbn += random.Next(0, 9).ToString();
        isbn += "-";
        isbn += random.Next(0, 99999).ToString();
        isbn += "-";
        isbn += random.Next(0, 99999).ToString();
        isbn += "-";
        isbn += random.Next(0, 9).ToString();

        return isbn;
    }
}