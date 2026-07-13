class Contenitore<T>
{
    public T Valore { get; set; }

    public Contenitore(T valore)
    {
        Valore = valore;
    }

    public void Stampa()
    {
        Console.WriteLine(Valore);
    }
}

class Program
{
    static void Main()
    {
        Contenitore<int> c1 = new Contenitore<int>(10);
        Contenitore<string> c2 = new Contenitore<string>("Ciao");

        c1.Stampa();
        c2.Stampa();
    }
}