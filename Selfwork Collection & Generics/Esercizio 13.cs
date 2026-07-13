class Auto
{
    public string Modello { get; set; }

    public Auto(string modello)
    {
        Modello = modello;
    }
}

class Program
{
    static void Main()
    {
        Dictionary<string, Auto> auto = new Dictionary<string, Auto>();

        Console.Write("Chiave: ");
        string chiave = Console.ReadLine();

        Console.Write("Modello: ");
        string modello = Console.ReadLine();

        auto.Add(chiave, new Auto(modello));

        auto.Remove(chiave);

        if (auto.ContainsKey(chiave))
        {
            Console.WriteLine("Trovata");
        }
        else
        {
            Console.WriteLine("Oggetto eliminato.");
        }
    }
}