using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LibraryApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            SerializeAllBooks(); // for testing purposes

            SetLvBooksList(State.All);
        }

        private Book currentBook = null;


        private void SetLvBooksList(State state)
        {
            List<Book> books = new List<Book>();

            string root = @"C:\Users\lukas\RiderProjects\LibraryApplication\LibraryApplication\Books\";

            var files = from file in Directory.EnumerateFiles(root) select file;

            if (state == State.All)
            {
                files = from file in Directory.EnumerateFiles(root) select file;
            }
            else if (state == State.Available)
            {
                files = from file in Directory.EnumerateFiles(root) where file.Contains("true") select file;
            }
            else if (state == State.Loaned)
            {
                files = from file in Directory.EnumerateFiles(root) where file.Contains("false") select file;
            }

            foreach (var file in files)
            {
                string json = File.ReadAllText(file);
                Book book = JsonSerializer.Deserialize<Book>(json);

                books.Add(book);
            }

            foreach (var book in books)
            {
                Console.Out.WriteLine(book);
            }

            LvBooks.ItemsSource = books;
        }


        private void CmbBooks_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (CmbBooks.SelectedItem.ToString().Split(new string[] { ": " }, StringSplitOptions.None).Last())
            {
                case "1":
                    SetLvBooksList(State.All);
                    break;
                case "2":
                    SetLvBooksList(State.Available);
                    break;
                case "3":
                    Console.Out.WriteLine("3");
                    break;
            }
        }

        private enum State
        {
            All,
            Available,
            Loaned,
        }

        private void LvBooks_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ListView listView)
            {
                if (listView.SelectedItem is Book book)
                {
                    currentBook = book;

                    TxtBookTitle.Text = book.Title;
                    TxtBookAuthor.Text = book.Author;
                    TxtBookPublisher.Text = book.Publisher;
                    TxtBookYear.Text = book.Year.ToString();
                    TxtBookIsbn.Text = book.Isbn;
                    TxtBookDescription.Text = book.Description;
                    TxtBookCategory.Text = book.Category;
                }
            }
        }


        private void SerializeAllBooks()
        {
            ArrayList itemsList = new ArrayList();

            Book book1 = new Book(1, "The Hobbit", "J.R.R. Tolkien", "Houghton Mifflin", 1937, "978-0-618-00221-1",
                "The Hobbit, or There and Back Again is a children's fantasy novel by English author J. R. R. Tolkien. It was published on 21 September 1937 to wide critical acclaim, being nominated for the Carnegie Medal and awarded a prize from the New York Herald Tribune for best juvenile fiction.",
                "Fantasy");
            Book book2 = new Book(2, "The Fellowship of the Ring", "J.R.R. Tolkien", "Houghton Mifflin", 1954,
                "978-0-618-00222-8",
                "The Fellowship of the Ring is the first of three volumes of the epic novel The Lord of the Rings by the English author J. R. R. Tolkien. It is followed by The Two Towers and The Return of the King.",
                "Fantasy");
            Book book3 = new Book(3, "The Two Towers", "J.R.R. Tolkien", "Houghton Mifflin", 1954, "978-0-618-00223-5",
                "The Two Towers is the second volume of J. R. R. Tolkien's high fantasy novel The Lord of the Rings. It is preceded by The Fellowship of the Ring and followed by The Return of the King.",
                "Fantasy");
            Book book4 = new Book(4, "The Return of the King", "J.R.R. Tolkien", "Houghton Mifflin", 1955,
                "978-0-618-00224-2",
                "The Return of the King is the third and final volume of J. R. R. Tolkien's The Lord of the Rings, following The Fellowship of the Ring and The Two Towers.",
                "Fantasy");
            Book book5 = new Book(5, "The Silmarillion", "J.R.R. Tolkien", "Houghton Mifflin", 1977,
                "978-0-618-00225-9",
                "The Silmarillion is a collection of mythopoeic works by English writer J. R. R. Tolkien, edited and published posthumously by his son, Christopher Tolkien, in 1977, with assistance from Guy Gavriel Kay.",
                "Fantasy");
            Book book6 = new Book(6, "The Children of Húrin", "J.R.R. Tolkien", "Houghton Mifflin", 2007,
                "978-0-618-00226-6",
                "The Children of Húrin is an epic fantasy novel which forms the completion of a tale by J. R. R. Tolkien. He wrote the original version of the story in the late 1910s, revised it several times later, but did not complete it before his death in 1973.",
                "Fantasy");
            Book book7 = new Book(7, "The Fall of Gondolin", "J.R.R. Tolkien", "Houghton Mifflin", 2018,
                "978-0-618-00227-3",
                "The Fall of Gondolin is, in the writings of J.R.R. Tolkien, one of the original Lost Tales which formed the basis for a section in his later work, The Silmarillion. A stand-alone, book-length version of the story was published on 30 August 2018.",
                "Fantasy");
            Book book8 = new Book(8, "The Book of Lost Tales", "J.R.R. Tolkien", "Houghton Mifflin", 1983,
                "978-0-618-00228-0",
                "The Book of Lost Tales is a collection of early stories by English writer J. R. R. Tolkien, published as the first two volumes of Christopher Tolkien's 12-volume series The History of Middle-earth, in which he presents and analyzes the manuscripts of those stories, which were the earliest form of the complex fictional myths that would eventually comprise The Silmarillion.",
                "Fantasy");
            Book book9 = new Book(9, "The Lays of Beleriand", "J.R.R. Tolkien", "Houghton Mifflin", 1985,
                "978-0-618-00229-7",
                "The Lays of Beleriand, published in 1985, is the third volume of Christopher Tolkien's 12-volume book series, The History of Middle-earth, in which he analyzes the unpublished manuscripts of his father J. R. R. Tolkien.",
                "Fantasy");
            Book book10 = new Book(10, "The Shaping of Middle-earth", "J.R.R. Tolkien", "Houghton Mifflin", 1986,
                "978-0-618-00230-3",
                "The Shaping of Middle-earth, published in 1986, is the fourth volume of Christopher Tolkien's 12-volume book series, The History of Middle-earth, in which he analyzes the unpublished manuscripts of his father J. R. R. Tolkien.",
                "Fantasy");


            itemsList.Add(book1);
            itemsList.Add(book2);
            itemsList.Add(book3);
            itemsList.Add(book4);
            itemsList.Add(book5);
            itemsList.Add(book6);
            itemsList.Add(book7);
            itemsList.Add(book8);
            itemsList.Add(book9);
            itemsList.Add(book10);


            foreach (Book book in itemsList)
            {
                book.AddToLibrary();
            }
        }


        private void BtnAddBook_OnClick(object sender, RoutedEventArgs e)
        {
            Book book = new Book(1, "The Hobbit", "J.R.R. Tolkien", "Houghton Mifflin", 1937, "978-0-618-00221-1",
                "The Hobbit, or There and Back Again is a children's fantasy novel by English author J. R. R. Tolkien. It was published on 21 September 1937 to wide critical acclaim, being nominated for the Carnegie Medal and awarded a prize from the New York Herald Tribune for best juvenile fiction.",
                "Fantasy");
            book.AddToLibrary();
        }


        private void NewBookMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            BookEditor bookEditor = new BookEditor(BookState.New, null);

            bookEditor.ShowDialog();
        }

        private void EditBookMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            if (currentBook == null)
            {
                MessageBox.Show("Please select a book first!");
                return;
            }

            BookEditor bookEditor = new BookEditor(BookState.Edit, currentBook);

            bookEditor.ShowDialog();
        }

        private void DeleteBookMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            if (currentBook == null)
            {
                MessageBox.Show("Please select a book first!");
                return;
            }

            //delete book.json file
            string path = @"C:\Users\lukas\RiderProjects\LibraryApplication\LibraryApplication\Books\";
            string fileName = currentBook.Title + ".json";
            File.Delete(path + fileName);

            //refresh listview
            string state = CmbBooks.SelectedItem.ToString().Split(new string[] { ": " }, StringSplitOptions.None)
                .Last();
            if (state == "1")
            {
                SetLvBooksList(State.All);
            }
            else if (state == "2")
            {
                SetLvBooksList(State.Available);
            }
            else if (state == "3")
            {
                SetLvBooksList(State.Loaned);
            }
        }
    }
}