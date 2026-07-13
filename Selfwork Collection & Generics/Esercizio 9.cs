
class Studente
{
    public string Nome { get; set; }
    public int Voto { get; set; }

    public Studente(string nome, int voto)
    {
        Nome = nome;
        Voto = voto;
    }
}

class Program
{
    static void Main()
    {
        List<Studente> studenti = new List<Studente>()
        {
            new Studente("Marco", 18),
            new Studente("Anna", 30),
            new Studente("Luca", 25),
            new Studente("Sara", 16)
        };

        List<Studente> promossi = new List<Studente>();

        foreach (Studente s in studenti)
        {
            if (s.Voto >= 18)
                promossi.Add(s);
        }

        Console.WriteLine("Studenti promossi:");

        foreach (Studente s in promossi)
        {
            Console.WriteLine($"{s.Nome} - {s.Voto}");
        }
    }
}