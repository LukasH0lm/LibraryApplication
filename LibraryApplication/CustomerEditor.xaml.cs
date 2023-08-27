using System.Windows;

namespace LibraryApplication;

public partial class CustomerEditor : Window
{
    Customer currentCustomer = null;

    BookState _state;

    public CustomerEditor(BookState state, Customer customer,
        MainWindow mainWindow) //BookState is a bøffet name for an enum
    {
        InitializeComponent();
        currentCustomer = customer;
        _state = state;

        InitializeInputFields();

        Closing += mainWindow.OnEditorClosing;
    }

    private void InitializeInputFields()
    {
        if (_state == BookState.New)
        {
            BtnSubmit.Content = "Create";
            return;
        }

        if (_state == BookState.Edit)
        {
            BtnSubmit.Content = "Save";

            TxtCustomerFirstName.Text = currentCustomer.FirstName;
            TxtCustomerLastName.Text = currentCustomer.LastName;
            TxtCustomerAddress.Text = currentCustomer.Address;
            TxtCustomerPhoneNumber.Text = currentCustomer.PhoneNumber;
            TxtCustomerEmail.Text = currentCustomer.Email;


            return;
        }

        if (_state == BookState.Delete)
        {
            BtnSubmit.Content = "Delete";

            TxtCustomerFirstName.Text = currentCustomer.FirstName;
            TxtCustomerLastName.Text = currentCustomer.LastName;
            TxtCustomerAddress.Text = currentCustomer.Address;
            TxtCustomerPhoneNumber.Text = currentCustomer.PhoneNumber;
            TxtCustomerEmail.Text = currentCustomer.Email;

            TxtCustomerFirstName.IsEnabled = false;
            TxtCustomerLastName.IsEnabled = false;
            TxtCustomerAddress.IsEnabled = false;
            TxtCustomerPhoneNumber.IsEnabled = false;
            TxtCustomerEmail.IsEnabled = false;

            return;
        }
    }

    private void BtnSubmit_OnClick(object sender, RoutedEventArgs e)
    {
        if (_state == BookState.New)
        {
            Customer newCustomer = new Customer(0, TxtCustomerFirstName.Text, TxtCustomerLastName.Text,
                TxtCustomerAddress.Text,
                TxtCustomerPhoneNumber.Text, TxtCustomerEmail.Text, null);

            newCustomer.AddToLibrary();

            MessageBox.Show("Customer created successfully!");
        }
        else if (_state == BookState.Edit)
        {
            currentCustomer.RemoveFromLibrary();

            Customer newCustomer = new Customer(0, TxtCustomerFirstName.Text, TxtCustomerLastName.Text,
                TxtCustomerAddress.Text,
                TxtCustomerPhoneNumber.Text, TxtCustomerEmail.Text, null);

            newCustomer.AddToLibrary();

            MessageBox.Show("Customer saved successfully!");
        }
        else if (_state == BookState.Delete)
        {
            currentCustomer.RemoveFromLibrary();

            MessageBox.Show("Customer deleted successfully!");
        }


        Close();
    }
}