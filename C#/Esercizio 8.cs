using System;

class Persona
{
    public string Nome { get; set; }
    public string Cognome { get; set; }

    public Persona(string nome, string cognome)
    {
        Nome = nome;
        Cognome = cognome;
    }

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
            return false;

        Persona altraPersona = (Persona)obj;

        return Nome == altraPersona.Nome &&
               Cognome == altraPersona.Cognome;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Nome, Cognome);
    }
}

class Program
{
    static void Main()
    {
        Persona p1 = new Persona("Mario", "Rossi");
        Persona p2 = new Persona("Mario", "Rossi");
        Persona p3 = new Persona("Luca", "Bianchi");

        Console.WriteLine(p1.Equals(p2)); // True
        Console.WriteLine(p1.Equals(p3)); // False
    }
}