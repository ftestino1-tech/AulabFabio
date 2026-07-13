using System;

class Program
{
    static void Main()
    {
        Console.Write("Quanti numeri vuoi inserire? ");
        int n = int.Parse(Console.ReadLine());

        int somma = 0;
        int sommaPari = 0;
        int sommaDispari = 0;
        int contaPari = 0;
        int contaDispari = 0;

        for (int i = 1; i <= n; i++)
        {
            Console.Write("Inserisci il numero " + i + ": ");
            int numero = int.Parse(Console.ReadLine());

            somma += numero;

            if (numero % 2 == 0)
            {
                sommaPari += numero;
                contaPari++;
            }
            else
            {
                sommaDispari += numero;
                contaDispari++;
            }
        }

        double mediaTotale = (double)somma / n;
        Console.WriteLine("\nMedia di tutti i numeri: " + mediaTotale);

        if (contaPari > 0)
            Console.WriteLine("Media dei numeri pari: " + (double)sommaPari / contaPari);
        else
            Console.WriteLine("Non sono stati inseriti numeri pari.");

        if (contaDispari > 0)
            Console.WriteLine("Media dei numeri dispari: " + (double)sommaDispari / contaDispari);
        else
            Console.WriteLine("Non sono stati inseriti numeri dispari.");
    }
}