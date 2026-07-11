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
}

class Program
{
    // Metodo che filtra i libri usando un Predicate
    static List<Libro> FiltraLibri(List<Libro> libri, Predicate<Libro> criterio)
    {
        List<Libro> risultato = new List<Libro>();

        foreach (Libro libro in libri)
        {
            if (criterio(libro))
            {
                risultato.Add(libro);
            }
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
            new Libro("Il Nome della Rosa", "Eco", "Giallo", 550)
        };

        // Filtra per genere
        List<Libro> fantasy = FiltraLibri(libri,
            libro => libro.Genere == "Fantasy");

        Console.WriteLine("Libri Fantasy:");
        foreach (Libro libro in fantasy)
        {
            Console.WriteLine(libro.Titolo);
        }

        // Filtra per numero di pagine
        List<Libro> lunghi = FiltraLibri(libri,
            libro => libro.Pagine > 400);

        Console.WriteLine("\nLibri con più di 400 pagine:");
        foreach (Libro libro in lunghi)
        {
            Console.WriteLine(libro.Titolo);
        }

        // Filtra per autore
        List<Libro> tolkien = FiltraLibri(libri,
            libro => libro.Autore == "Tolkien");

        Console.WriteLine("\nLibri di Tolkien:");
        foreach (Libro libro in tolkien)
        {
            Console.WriteLine(libro.Titolo);
        }
    }
}