using System.Windows;

namespace LibraryApplication;

public partial class BookEditor : Window
{
    public BookEditor(BookState state, Book book)
    {
        InitializeComponent();

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
        }
    }

    private void BtnSubmit_OnClick(object sender, RoutedEventArgs e)
    {
        if (BtnSubmit.Content == "Create")
        {
            Book newBook = new Book(0, TxtBookTitle.Text, TxtBookAuthor.Text, TxtBookPublisher.Text,
                int.Parse(TxtBookYear.Text), TxtBookIsbn.Text, TxtBookDescription.Text, TxtBookCategory.Text);
            newBook.AddToLibrary();
        }
        else if (BtnSubmit.Content == "Save")
        {
        }
    }
}