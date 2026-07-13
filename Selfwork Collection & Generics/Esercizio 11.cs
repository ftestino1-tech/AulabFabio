class Libro
{
    public string Titolo { get; set; }

    public Libro(string titolo)
    {
        Titolo = titolo;
    }
}

class Program
{
    static void Main()
    {
        Dictionary<string, Libro> libri = new Dictionary<string, Libro>();

        libri.Add("L1", new Libro("Il Signore degli Anelli"));
        libri.Add("L2", new Libro("Harry Potter"));

        Console.Write("Inserisci la chiave: ");
        string chiave = Console.ReadLine();

        if (libri.ContainsKey(chiave))
        {
            Console.WriteLine(libri[chiave].Titolo);
        }
        else
        {
            Console.WriteLine("Libro non trovato.");
        }
    }
}