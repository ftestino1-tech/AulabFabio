class Program
{
    static T Massimo<T>(T a, T b) where T : IComparable<T>
    {
        if (a.CompareTo(b) > 0)
            return a;
        else
            return b;
    }

    static void Main()
    {
        Console.WriteLine(Massimo(10, 20));
        Console.WriteLine(Massimo(3.5, 2.1));
        Console.WriteLine(Massimo("Marco", "Anna"));
    }
}