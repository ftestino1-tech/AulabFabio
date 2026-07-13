using System;
using System.Collections.Generic;

// Classe Dish
class Dish
{
    // Attributi
    private string nome;
    private double prezzo;
    private List<string> ingredienti;

    // Costruttore
    public Dish(string nome, double prezzo, List<string> ingredienti = null)
    {
        this.nome = nome;
        this.prezzo = prezzo;
        this.ingredienti = ingredienti ?? new List<string>();
    }

    // Getter e Setter
    public string GetNome()
    {
        return nome;
    }

    public void SetNome(string nome)
    {
        this.nome = nome;
    }

    public double GetPrezzo()
    {
        return prezzo;
    }

    public void SetPrezzo(double prezzo)
    {
        this.prezzo = prezzo;
    }

    public List<string> GetIngredienti()
    {
        return ingredienti;
    }

    public void SetIngredienti(List<string> ingredienti)
    {
        this.ingredienti = ingredienti;
    }
}

// Classe Drink
class Drink
{
    // Attributi
    private string nome;
    private double prezzo;

    // Costruttore
    public Drink(string nome, double prezzo)
    {
        this.nome = nome;
        this.prezzo = prezzo;
    }

    // Getter e Setter
    public string GetNome()
    {
        return nome;
    }

    public void SetNome(string nome)
    {
        this.nome = nome;
    }

    public double GetPrezzo()
    {
        return prezzo;
    }

    public void SetPrezzo(double prezzo)
    {
        this.prezzo = prezzo;
    }
}

// Classe Restaurant
class Restaurant
{
    // Attributi
    public string Name { get; set; }
    public List<Dish> Dishes { get; set; }
    public List<Drink> Drinks { get; set; }

    // Costruttore
    public Restaurant(string name)
    {
        Name = name;
        Dishes = new List<Dish>();
        Drinks = new List<Drink>();
    }

    // Metodo per stampare il menu
    public void StampaMenu()
    {
        Console.WriteLine("=== MENU DI " + Name.ToUpper() + " ===");

        Console.WriteLine("\nPIATTI:");
        foreach (Dish dish in Dishes)
        {
            Console.WriteLine($"{dish.GetNome()} - {dish.GetPrezzo():0.00}€");
        }

        Console.WriteLine("\nDRINK:");
        foreach (Drink drink in Drinks)
        {
            Console.WriteLine($"{drink.GetNome()} - {drink.GetPrezzo():0.00}€");
        }
    }
}

// Programma principale
class Program
{
    static void Main(string[] args)
    {
        Restaurant ristorante = new Restaurant("La Buona Tavola");

        // Aggiunta piatti
        ristorante.Dishes.Add(new Dish("Pizza Margherita", 8.50,
            new List<string> { "Pomodoro", "Mozzarella", "Basilico" }));

        ristorante.Dishes.Add(new Dish("Lasagna", 12.00,
            new List<string> { "Pasta", "Ragù", "Besciamella" }));

        // Aggiunta drink
        ristorante.Drinks.Add(new Drink("Acqua", 1.50));
        ristorante.Drinks.Add(new Drink("Coca-Cola", 3.00));
        ristorante.Drinks.Add(new Drink("Birra", 4.50));

        // Stampa menu
        ristorante.StampaMenu();
    }
}