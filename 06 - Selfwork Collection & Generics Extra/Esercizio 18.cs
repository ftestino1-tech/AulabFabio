class Program
{
    static void Main()
    {
        LinkedList<string> lista = new LinkedList<string>();

        lista.AddFirst("Marco");
        lista.AddLast("Anna");
        lista.AddFirst("Luca");
        lista.AddLast("Sara");

        Console.WriteLine("Lista iniziale:");

        foreach (string nome in lista)
        {
            Console.WriteLine(nome);
        }

        lista.RemoveFirst();
        lista.RemoveLast();

        Console.WriteLine("\nDopo le rimozioni:");

        foreach (string nome in lista)
        {
            Console.WriteLine(nome);
        }
    }
}