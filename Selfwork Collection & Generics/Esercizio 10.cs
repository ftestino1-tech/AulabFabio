class Persona
{
    public string Nome { get; set; }

    public Persona(string nome)
    {
        Nome = nome;
    }

    public override bool Equals(object obj)
    {
        if (obj is Persona p)
            return Nome == p.Nome;

        return false;
    }

    public override int GetHashCode()
    {
        return Nome.GetHashCode();
    }
}

class Program
{
    static void Main()
    {
        HashSet<Persona> persone = new HashSet<Persona>();

        persone.Add(new Persona("Marco"));
        persone.Add(new Persona("Marco"));
        persone.Add(new Persona("Anna"));

        foreach (Persona p in persone)
        {
            Console.WriteLine(p.Nome);
        }
    }
}