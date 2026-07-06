using System;

class Program
{
    static void Main()
    {
        int[] numeri = { 10, 20, 30, 40, 50, 60, 70, 80 };

        Console.WriteLine("Elementi in posizione pari:");

        for (int i = 0; i < numeri.Length; i += 2)
        {
            Console.WriteLine(numeri[i]);
        }
    }
}