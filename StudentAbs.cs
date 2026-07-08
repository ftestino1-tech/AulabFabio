public class StudentAbs : PersonAbs
{
    public int Grade { get; }

    public StudentAbs(string name, string surname, int age, int grade) : base(name, surname, age)
    {
        Grade = grade;
    }

    public override void DisplayInfo()
    {
        Console.WriteLine($"Studente: {Name} {Surname}");
        Console.WriteLine($"Età: {Age}");   
        Console.WriteLine($"Voto: {Grade}");       
    }
}