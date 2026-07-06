using System;

class Program
{
    static void Main()
    {
        int[] numeri = { 12, 45, 7, 89, 23, 56 };

        int maggiore = numeri[0];

        for (int i = 1; i < numeri.Length; i++)
        {
            if (numeri[i] > maggiore)
            {
                maggiore = numeri[i];
            }
        }

        Console.WriteLine("L'elemento maggiore è: " + maggiore);
    }
}