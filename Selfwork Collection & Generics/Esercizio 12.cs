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
        List<Persona> persone = new List<Persona>()
        {
            new Persona("Marco",20),
            new Persona("Anna",25),
            new Persona("Luca",30)
        };

        for (int i = 0; i < persone.Count; i++)
        {
            Console.WriteLine($"{i} - {persone[i].Nome} {persone[i].Eta}");
        }

        Console.Write("Indice da modificare: ");
        int indice = int.Parse(Console.ReadLine());

        Console.Write("Nuovo nome: ");
        string nome = Console.ReadLine();

        Console.Write("Nuova età: ");
        int eta = int.Parse(Console.ReadLine());

        persone[indice].Nome = nome;
        persone[indice].Eta = eta;

        Console.WriteLine("\nLista aggiornata:");

        foreach (Persona p in persone)
        {
            Console.WriteLine($"{p.Nome} {p.Eta}");
        }
    }
}