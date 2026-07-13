public class Person
{
    public string Name;
    public string Surname;


    public Person(string name, string surname)
    {
        Name = name;
        Surname = surname;
    }

    public virtual string GetDescription()
    {
        return $"Name: {Name}, Surname: {Surname}";
    }
}