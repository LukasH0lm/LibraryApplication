using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
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
            
            List<Book> books = new List<Book>();
            
            Book book1 = new Book(1, "The Hobbit", "J.R.R. Tolkien", "Houghton Mifflin", 1937, "978-0-618-00221-1", "The Hobbit, or There and Back Again is a children's fantasy novel by English author J. R. R. Tolkien. It was published on 21 September 1937 to wide critical acclaim, being nominated for the Carnegie Medal and awarded a prize from the New York Herald Tribune for best juvenile fiction.", "Fantasy");
            Book book2 = new Book(2, "The Fellowship of the Ring", "J.R.R. Tolkien", "Houghton Mifflin", 1954, "978-0-618-00222-8", "The Fellowship of the Ring is the first of three volumes of the epic novel The Lord of the Rings by the English author J. R. R. Tolkien. It is followed by The Two Towers and The Return of the King.", "Fantasy");
            Book book3 = new Book(3, "The Two Towers", "J.R.R. Tolkien", "Houghton Mifflin", 1954, "978-0-618-00223-5", "The Two Towers is the second volume of J. R. R. Tolkien's high fantasy novel The Lord of the Rings. It is preceded by The Fellowship of the Ring and followed by The Return of the King.", "Fantasy");
            
            books.Add(book1);
            books.Add(book2);
            books.Add(book3);
            
            lvBooks.ItemsSource = books;

        }

        public ObservableCollection<Book> Books { get; set; }

        public void ListView()
        {
            InitializeComponent();
            
            this.Books = new ObservableCollection<Book>();
            this.Books.Add(new Book(1, "The Hobbit", "J.R.R. Tolkien", "Houghton Mifflin", 1937, "978-0-618-00221-1", "The Hobbit, or There and Back Again is a children's fantasy novel by English author J. R. R. Tolkien. It was published on 21 September 1937 to wide critical acclaim, being nominated for the Carnegie Medal and awarded a prize from the New York Herald Tribune for best juvenile fiction.", "Fantasy"));
            
        }




        private ArrayList LoadListViewData()
        {
            ArrayList itemsList = new ArrayList();
            
            Book book1 = new Book(1, "The Hobbit", "J.R.R. Tolkien", "Houghton Mifflin", 1937, "978-0-618-00221-1", "The Hobbit, or There and Back Again is a children's fantasy novel by English author J. R. R. Tolkien. It was published on 21 September 1937 to wide critical acclaim, being nominated for the Carnegie Medal and awarded a prize from the New York Herald Tribune for best juvenile fiction.", "Fantasy");
            Book book2 = new Book(2, "The Fellowship of the Ring", "J.R.R. Tolkien", "Houghton Mifflin", 1954, "978-0-618-00222-8", "The Fellowship of the Ring is the first of three volumes of the epic novel The Lord of the Rings by the English author J. R. R. Tolkien. It is followed by The Two Towers and The Return of the King.", "Fantasy");
            Book book3 = new Book(3, "The Two Towers", "J.R.R. Tolkien", "Houghton Mifflin", 1954, "978-0-618-00223-5", "The Two Towers is the second volume of J. R. R. Tolkien's high fantasy novel The Lord of the Rings. It is preceded by The Fellowship of the Ring and followed by The Return of the King.", "Fantasy");
            Book book4 = new Book(4, "The Return of the King", "J.R.R. Tolkien", "Houghton Mifflin", 1955, "978-0-618-00224-2", "The Return of the King is the third and final volume of J. R. R. Tolkien's The Lord of the Rings, following The Fellowship of the Ring and The Two Towers.", "Fantasy");
            Book book5 = new Book(5, "The Silmarillion", "J.R.R. Tolkien", "Houghton Mifflin", 1977, "978-0-618-00225-9", "The Silmarillion is a collection of mythopoeic works by English writer J. R. R. Tolkien, edited and published posthumously by his son, Christopher Tolkien, in 1977, with assistance from Guy Gavriel Kay.", "Fantasy");
            Book book6 = new Book(6, "The Children of Húrin", "J.R.R. Tolkien", "Houghton Mifflin", 2007, "978-0-618-00226-6", "The Children of Húrin is an epic fantasy novel which forms the completion of a tale by J. R. R. Tolkien. He wrote the original version of the story in the late 1910s, revised it several times later, but did not complete it before his death in 1973.", "Fantasy");
            Book book7 = new Book(7, "The Fall of Gondolin", "J.R.R. Tolkien", "Houghton Mifflin", 2018, "978-0-618-00227-3", "The Fall of Gondolin is, in the writings of J.R.R. Tolkien, one of the original Lost Tales which formed the basis for a section in his later work, The Silmarillion. A stand-alone, book-length version of the story was published on 30 August 2018.", "Fantasy");
            Book book8 = new Book(8, "The Book of Lost Tales", "J.R.R. Tolkien", "Houghton Mifflin", 1983, "978-0-618-00228-0", "The Book of Lost Tales is a collection of early stories by English writer J. R. R. Tolkien, published as the first two volumes of Christopher Tolkien's 12-volume series The History of Middle-earth, in which he presents and analyzes the manuscripts of those stories, which were the earliest form of the complex fictional myths that would eventually comprise The Silmarillion.", "Fantasy");
            Book book9 = new Book(9, "The Lays of Beleriand", "J.R.R. Tolkien", "Houghton Mifflin", 1985, "978-0-618-00229-7", "The Lays of Beleriand, published in 1985, is the third volume of Christopher Tolkien's 12-volume book series, The History of Middle-earth, in which he analyzes the unpublished manuscripts of his father J. R. R. Tolkien.", "Fantasy");
            Book book10 = new Book(10, "The Shaping of Middle-earth", "J.R.R. Tolkien", "Houghton Mifflin", 1986, "978-0-618-00230-3", "The Shaping of Middle-earth, published in 1986, is the fourth volume of Christopher Tolkien's 12-volume book series, The History of Middle-earth, in which he analyzes the unpublished manuscripts of his father J. R. R. Tolkien.", "Fantasy");
           
            
            return itemsList;
        }
    }
}