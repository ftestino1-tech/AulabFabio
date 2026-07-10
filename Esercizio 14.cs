class Program
{
    static void Main()
    {
        List<int> numeri = new List<int>()
        {
            1,2,3,2,4,5,3,6,6
        };

        HashSet<int> senzaDuplicati = new HashSet<int>(numeri);

        foreach (int n in senzaDuplicati)
        {
            Console.WriteLine(n);
        }
    }
}