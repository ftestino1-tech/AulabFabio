using System;

class Program
{
    static void Main()
    {
        Console.Write("Inserisci una stringa: ");
        string testo = Console.ReadLine();

        // Rimuove spazi e rende tutto minuscolo per un confronto corretto
        testo = testo.Replace(" ", "").ToLower();

        bool palindroma = true;

        for (int i = 0; i < testo.Length / 2; i++)
        {
            if (testo[i] != testo[testo.Length - 1 - i])
            {
                palindroma = false;
                break;
            }
        }

        if (palindroma)
            Console.WriteLine("La stringa è palindroma.");
        else
            Console.WriteLine("La stringa non è palindroma.");
    }
}