class Persona
{
    public string Nome { get; set; }
    public int Eta { get; set; }

    public Persona(string nome, int eta)
    {
        Nome = nome;
        Eta = eta;
    }
}

class Program
{
    static void Main()
    {
        Dictionary<string, Persona> persone = new Dictionary<string, Persona>();

        persone.Add("P1", new Persona("Marco", 20));
        persone.Add("P2", new Persona("Anna", 25));
        persone.Add("P3", new Persona("Luca", 30));

        Console.WriteLine("Chiavi:");

        foreach (string chiave in persone.Keys)
        {
            Console.WriteLine(chiave);
        }

        Console.WriteLine("\nValori:");

        foreach (Persona p in persone.Values)
        {
            Console.WriteLine($"{p.Nome} - {p.Eta} anni");
        }
    }
}