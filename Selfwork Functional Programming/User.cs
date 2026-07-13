public class User
{
    public string Name { get;  }

    public int Age { get;  }
    public UserStatus Status { get; }

    public User(string name, string surname, int age, UserStatus status)
    {
        Name = name;
        Age = age;
        Status = status;
    }
}