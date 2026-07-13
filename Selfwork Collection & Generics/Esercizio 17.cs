interface IRepository<T>
{
    void Aggiungi(T elemento);
    T Ottieni();
}

// Classe che implementa l'interfaccia
class Repository<T> : IRepository<T>
{
    private T elemento;

    public void Aggiungi(T elemento)
    {
        this.elemento = elemento;
    }

    public T Ottieni()
    {
        return elemento;
    }
}

class Program
{
    static void Main()
    {
        IRepository<string> repoString = new Repository<string>();
        repoString.Aggiungi("Ciao");
        Console.WriteLine(repoString.Ottieni());

        IRepository<int> repoInt = new Repository<int>();
        repoInt.Aggiungi(100);
        Console.WriteLine(repoInt.Ottieni());
    }
}