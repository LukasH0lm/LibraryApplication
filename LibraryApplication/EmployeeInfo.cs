namespace LibraryApplication;

public class EmployeeInfo
{
    private string _firstName;
    private string _lastName;
    private string _employeeNumber;

    public string FirstName
    {
        get {return _firstName;}
        set {_firstName = value;}
    }

    public string LastName
    {
        get {return _lastName;}
        set {_lastName = value;}
    }

    public string EmployeeNumber
    {
        get {return _employeeNumber;}
        set {_employeeNumber = value;}
    }

    public EmployeeInfo(string firstname, string lastname, string empnumber)
    {
        _firstName = firstname;
        _lastName = lastname;
        _employeeNumber = empnumber;
    }
}