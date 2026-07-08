public class TeacherAbs : PersonAbs
{
    public string Subject { get; }

    public TeacherAbs(string name, string surname, int age, string subject ) : base(name, surname, age)
    {
        Subject = subject;
    }

    public override void DisplayInfo()
    {
        Console.WriteLine($"Insegnante: {Name} {Surname}");
        Console.WriteLine($"Età: {Age}");   
        Console.WriteLine($"Materia: {Subject}");       
    }
}