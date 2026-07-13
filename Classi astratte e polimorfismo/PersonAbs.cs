public abstract class PersonAbs
{
    public string Name { get; }
    public string Surname { get; }
    public int Age { get; }


    protected PersonAbs(string name, string surname, int age)
    {
        Name = name;
        Surname = surname;
        Age = age;
    }

    public abstract void DisplayInfo();
    
}