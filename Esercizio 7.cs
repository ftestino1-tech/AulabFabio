using System;

class Persona
{
    // Campo privato
    private string nome;
    private int eta;

    // Proprietà con getter e setter
    public string Nome
    {
        get { return nome; }
        set { nome = value; }
    }

    public int Eta
    {
        get { return eta; }
        set
        {
            if (value >= 0)
                eta = value;
            else
                Console.WriteLine("L'età non può essere negativa.");
        }
    }

    public void MostraDati()
    {
        Console.WriteLine("Nome: " + Nome);
        Console.WriteLine("Età: " + Eta);
    }
}

class Program
{
    static void Main()
    {
        Persona p = new Persona();

        p.Nome = "Marco";   // Setter
        p.Eta = 20;         // Setter

        p.MostraDati();

        Console.WriteLine("Nome: " + p.Nome); // Getter
        Console.WriteLine("Età: " + p.Eta);   // Getter
    }
}