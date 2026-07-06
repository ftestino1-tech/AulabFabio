using System;

class Program
{
    static void Main()
    {
        Console.Write("Quanti numeri vuoi inserire? ");
        int n = int.Parse(Console.ReadLine());

        int[] numeri = new int[n];
        int somma = 0;

        for (int i = 0; i < numeri.Length; i++)
        {
            Console.Write("Inserisci il numero " + (i + 1) + ": ");
            numeri[i] = int.Parse(Console.ReadLine());

            somma += numeri[i];
        }

        Console.WriteLine("La somma degli elementi dell'array è: " + somma);
    }
}