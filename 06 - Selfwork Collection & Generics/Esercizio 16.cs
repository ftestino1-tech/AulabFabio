class Program
{
    static void Stampa<T>(T valore)
    {
        Console.WriteLine(valore);
    }

    static void Main()
    {
        Stampa(10);
        Stampa("Hello");
        Stampa(3.14);
    }
}