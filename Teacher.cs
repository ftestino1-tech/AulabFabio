public class Teacher : Person
{
    public string Subject;

    public Teacher(string name, string surname, string subject) : base(name, surname)
    {
        Subject = subject;
    }

    public override string GetDescription()
    {
        return $"Name: {Name}, Surname: {Surname}, Subject: {Subject}";
    }
}