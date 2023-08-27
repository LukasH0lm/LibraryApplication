using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

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

            // resets the books and customers // argument is true if you want to reset the books and customers ie. delete the existing ones
            SerializeAllBooks(true);
            serializeAllCustomers(true);

            SetLvBooksList(State.All);
            setLvCustomersList();
        }

        private Book _currentBook = null;
        private Customer _currentCustomer = null;


        private void SetLvBooksList(State state)
        {
            List<Book?> books = new List<Book?>();

            string root = @"C:\Users\lukas\RiderProjects\LibraryApplication\LibraryApplication\Books\";

            var files = from file in Directory.EnumerateFiles(root) select file;


            LvBooks.ItemsSource = null;

            Console.WriteLine("SetLvBooksList");
            Console.WriteLine("State: " + state);


            foreach (var file in files)
            {
                string json = File.ReadAllText(file);
                Book? book = JsonSerializer.Deserialize<Book>(json);


                books.Add(book);
            }

            List<Book?>
                books2 = new List<Book?>(); //this is a workaround to avoid the "Collection was modified; enumeration operation may not execute." error

            foreach (var book in books)
            {
                books2.Add(book);
            }

            foreach (var book in books)
            {
                //would be faster to check the other way around but who cares
                if (state == State.Available)
                {
                    if (book.IsAvailable == false)
                    {
                        books2.Remove(book);

                        Console.WriteLine("Removed: " + book?.Title);
                    }
                }
                else if (state == State.Loaned)
                {
                    Console.WriteLine("book.LoanedTo: " + book.LoanedTo);

                    //could be nice to know who loaned the book

                    if (book.IsAvailable)
                    {
                        books2.Remove(book);
                    }
                }
            }

            Console.WriteLine("books.Count: " + books2.Count);


            LvBooks.ItemsSource = books2;
        }


        public void setLvCustomersList()
        {
            List<Customer?> customers = new List<Customer?>();

            string root = @"C:\Users\lukas\RiderProjects\LibraryApplication\LibraryApplication\Customers\";

            var files = from file in Directory.EnumerateFiles(root) select file;

            foreach (var file in files)
            {
                string json = File.ReadAllText(file);
                Customer? customer = JsonSerializer.Deserialize<Customer>(json);

                customers.Add(customer);
            }

            LvCustomers.ItemsSource = customers;

            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(LvCustomers.ItemsSource);

            view.SortDescriptions.Add(
                new System.ComponentModel.SortDescription("Id", System.ComponentModel.ListSortDirection.Ascending));
        }


        private void CmbBooks_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string state = CmbBooks.SelectedItem.ToString().Split(new string[] { ": " }, StringSplitOptions.None)
                .Last();

            Console.WriteLine("CmbBooks_OnSelectionChanged");
            Console.WriteLine("State: " + state);

            if (state == "All Books")
            {
                SetLvBooksList(State.All);
            }
            else if (state == "Available Books")
            {
                SetLvBooksList(State.Available);
            }
            else if (state == "Loaned to user")
            {
                SetLvBooksList(State.Loaned);
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
                    _currentBook = book;

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

        private void LvCustomers_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Customer customer = (Customer)LvCustomers.SelectedItem;

            _currentCustomer = customer;
        }


        private void NewBookMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            BookEditor bookEditor = new BookEditor(BookState.New, null, this);

            bookEditor.ShowDialog();
        }

        private void EditBookMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            if (_currentBook == null)
            {
                MessageBox.Show("Please select a book first!");
                return;
            }

            BookEditor bookEditor = new BookEditor(BookState.Edit, _currentBook, this);

            bookEditor.ShowDialog();
        }

        private void DeleteBookMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            if (_currentBook == null)
            {
                MessageBox.Show("Please select a book first!");
                return;
            }

            BookEditor bookEditor = new BookEditor(BookState.Delete, _currentBook, this);

            bookEditor.ShowDialog();
        }


        //can't be assed to change the names
        private void NewCustomerMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            CustomerEditor bookEditor = new CustomerEditor(BookState.New, null, this);

            bookEditor.ShowDialog();
        }

        private void EditCustomerMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            if (_currentCustomer == null)
            {
                MessageBox.Show("Please select a User first!");
                return;
            }

            CustomerEditor bookEditor = new CustomerEditor(BookState.Edit, _currentCustomer, this);

            bookEditor.ShowDialog();
        }

        private void DeleteCustomerMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            if (_currentCustomer == null)
            {
                MessageBox.Show("Please select a User first!");
                return;
            }

            CustomerEditor bookEditor = new CustomerEditor(BookState.Delete, _currentCustomer, this);

            bookEditor.ShowDialog();
        }


        private void BtnLoan_OnClick(object sender, RoutedEventArgs e)
        {
            if (_currentBook == null)
            {
                MessageBox.Show("Please select a book first!");
                return;
            }

            if (_currentBook.IsAvailable == false)
            {
                MessageBox.Show("This book is already loaned!");
                return;
            }

            if (_currentCustomer == null)
            {
                MessageBox.Show("Please select a customer first!");
                return;
            }

            _currentBook.IsAvailable = false;

            _currentBook.LoanedTo = _currentCustomer;


            _currentBook.AddToLibrary();
        }


        private void BtnReturn_OnClick(object sender, RoutedEventArgs e)
        {
            if (_currentBook == null)
            {
                MessageBox.Show("Please select a book first!");
                return;
            }

            if (_currentBook.IsAvailable)
            {
                MessageBox.Show("This book is already returned!");
                return;
            }

            if (_currentBook.LoanedTo != _currentCustomer)
            {
                MessageBox.Show("This book is not loaned to this customer!");
                return;
            }

            _currentBook.IsAvailable = true;

            _currentBook.LoanedTo = null;


            _currentBook.AddToLibrary();
        }


        public void OnEditorClosing(object? sender, CancelEventArgs e)
        {
            setLvCustomersList();


            string state = CmbBooks.SelectedItem.ToString().Split(new string[] { ": " }, StringSplitOptions.None)
                .Last();

            if (state == "All Books")
            {
                SetLvBooksList(State.All);
            }

            if (state == "Available Books")
            {
                SetLvBooksList(State.Available);
            }

            if (state == "Loaned to user")
            {
                SetLvBooksList(State.Loaned);
            }
        }


        private void serializeAllCustomers(bool reset)
        {
            if (reset == true)
            {
                //delete all customers

                string root = @"C:\Users\lukas\RiderProjects\LibraryApplication\LibraryApplication\Customers\";

                var files = from file in Directory.EnumerateFiles(root) select file;

                foreach (var file in files)
                {
                    File.Delete(file);
                }
            }

            ArrayList customers = new ArrayList();

            Customer customer1 =
                new Customer(1, "Lukas", "Holm", "RibeLandevej 20", "71352006", "lukasholm@hotmail.com", null);

            Customer customer2 =
                new Customer(2, "Jens", "Jensen", "Vej 1", "12345678", "JensJensen@Bajookiemail.com", null);


            customers.Add(customer1);
            customers.Add(customer2);

            foreach (Customer customer in customers)
            {
                customer.serializeToJson();
            }
        }


        private void SerializeAllBooks(bool reset)
        {
            if (reset == true)
            {
                //delete all boooooooooooks

                string root = @"C:\Users\lukas\RiderProjects\LibraryApplication\LibraryApplication\Books\";

                var files = from file in Directory.EnumerateFiles(root) select file;

                foreach (var file in files)
                {
                    File.Delete(file);
                }
            }


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
    }
}