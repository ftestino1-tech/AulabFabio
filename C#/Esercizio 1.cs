using System;

class Program
{
    static void Main()
    {
        Console.Write("Inserisci un numero: ");
        int numero = int.Parse(Console.ReadLine());

        if (numero % 2 == 0)
        {
            Console.WriteLine("Il numero è pari.");
        }
        else
        {
            Console.WriteLine("Il numero è dispari.");
        }
    }
}