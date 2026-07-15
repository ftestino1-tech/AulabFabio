using System;
using System.Collections.Generic;

class Libro
{
    public string Titolo { get; set; }
    public string Autore { get; set; }
    public string Genere { get; set; }
    public int Pagine { get; set; }

    public Libro(string titolo, string autore, string genere, int pagine)
    {
        Titolo = titolo;
        Autore = autore;
        Genere = genere;
        Pagine = pagine;
    }

    public override string ToString()
    {
        return $"{Titolo} - {Autore} - {Genere} - {Pagine} pagine";
    }
}

class Program
{
    // Metodo generico
    static List<T> Filtra<T>(List<T> lista, Func<T, bool> criterio)
    {
        List<T> risultato = new List<T>();

        foreach (T elemento in lista)
        {
            if (criterio(elemento))
                risultato.Add(elemento);
        }

        return risultato;
    }

    static void Main()
    {
        List<Libro> libri = new List<Libro>()
        {
            new Libro("Il Signore degli Anelli", "Tolkien", "Fantasy", 1200),
            new Libro("Harry Potter", "Rowling", "Fantasy", 450),
            new Libro("1984", "Orwell", "Distopico", 320),
            new Libro("Il Nome della Rosa", "Eco", "Giallo", 550),
            new Libro("Lo Hobbit", "Tolkien", "Fantasy", 310)
        };

        
        Func<Libro, bool> genereFantasy = libro => libro.Genere == "Fantasy";
        Func<Libro, bool> piuDi400Pagine = libro => libro.Pagine > 400;
        Func<Libro, bool> autoreTolkien = libro => libro.Autore == "Tolkien";

        // Filtro per genere
        Console.WriteLine("=== Libri Fantasy ===");
        foreach (Libro l in Filtra(libri, genereFantasy))
            Console.WriteLine(l);

        // Filtro per pagine
        Console.WriteLine("\n=== Libri con più di 400 pagine ===");
        foreach (Libro l in Filtra(libri, piuDi400Pagine))
            Console.WriteLine(l);

        // Filtro per autore
        Console.WriteLine("\n=== Libri di Tolkien ===");
        foreach (Libro l in Filtra(libri, autoreTolkien))
            Console.WriteLine(l);

        // Combinazione di regole
        Console.WriteLine("\n=== Fantasy con più di 400 pagine ===");
        foreach (Libro l in Filtra(libri,
                 libro => genereFantasy(libro) && piuDi400Pagine(libro)))
        {
            Console.WriteLine(l);
        }
    }
}