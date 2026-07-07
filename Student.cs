public class Student : Person
{
    public string Course;

    public Student(string name, string surname, string course) : base(name, surname)
    {
        Course = course;
    }

    public override string GetDescription()
    {
        return $"Name: {Name}, Surname: {Surname}, Course: {Course}";
    }
}