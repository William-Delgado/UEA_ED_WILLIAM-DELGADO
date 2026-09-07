using System;
using System.Collections.Generic;
using System.Diagnostics;

class Libro
{
    public string ISBN { get; set; }
    public string Titulo { get; set; }
    public string Autor { get; set; }
    public string Categoria { get; set; }

    public Libro(string isbn, string titulo, string autor, string categoria)
    {
        ISBN = isbn;
        Titulo = titulo;
        Autor = autor;
        Categoria = categoria;
    }
}

class Program
{
    // Diccionario principal:
    // Clave = ISBN
    // Valor = objeto Libro
    static Dictionary<string, Libro> libros =
        new Dictionary<string, Libro>();

    // Conjunto para guardar categorías sin repetición
    static HashSet<string> categorias =
        new HashSet<string>();

    // Mapa ordenado:
    // Clave = título
    // Valor = ISBN
    static SortedDictionary<string, string> mapaTitulos =
        new SortedDictionary<string, string>();
    static void Main()
    {
        int opcion;
        do
        {
            Console.Clear();
            Console.WriteLine("======================================");
            Console.WriteLine("       SISTEMA DE BIBLIOTECA");
            Console.WriteLine("======================================");
            Console.WriteLine("1. Registrar libro");
            Console.WriteLine("2. Mostrar libros");
            Console.WriteLine("3. Buscar libro por ISBN");
            Console.WriteLine("4. Buscar libros por autor");
            Console.WriteLine("5. Mostrar categorías");
            Console.WriteLine("6. Mostrar mapa de títulos");
            Console.WriteLine("7. Reporte general");
            Console.WriteLine("8. Analizar tiempo de ejecución");
            Console.WriteLine("0. Salir");
            Console.WriteLine("======================================");
            Console.Write("Seleccione una opción: ");
            if (!int.TryParse(Console.ReadLine(), out opcion))
            {
                opcion = -1;
            }
            Console.Clear();
            switch (opcion)
            {
                case 1:
                    RegistrarLibro();
                    break;

                case 2:
                    MostrarLibros();
                    break;

                case 3:
                    BuscarPorISBN();
                    break;
                case 4:
                    BuscarPorAutor();
                    break;
                case 5:
                    MostrarCategorias();
                    break;
                case 6:
                    MostrarMapaTitulos();
                    break;
                case 7:
                    ReporteGeneral();
                    break;
                case 8:
                    AnalizarTiempo();
                    break;
                case 0:
                    Console.WriteLine("Programa finalizado.");
                    break;
                default:
                    Console.WriteLine("Opción incorrecta.");
                    break;
            }
            if (opcion != 0)
            {
                Console.WriteLine("\nPresione una tecla para continuar...");
                Console.ReadKey();
            }
        } while (opcion != 0);
    }
    // REGISTRAR LIBRO
    static void RegistrarLibro()
    {
        Console.WriteLine("REGISTRO DE LIBRO");
        Console.WriteLine("---------------------------");

        Console.Write("ISBN: ");
        string isbn = Console.ReadLine();

        // Evitar ISBN duplicados
        if (libros.ContainsKey(isbn))
        {
            Console.WriteLine("\nYa existe un libro con ese ISBN.");
            return;
        }

        Console.Write("Título: ");
        string titulo = Console.ReadLine();

        Console.Write("Autor: ");
        string autor = Console.ReadLine();

        Console.Write("Categoría: ");
        string categoria = Console.ReadLine();

        Libro nuevoLibro =
            new Libro(isbn, titulo, autor, categoria);

        // Guardar en el diccionario
        libros.Add(isbn, nuevoLibro);

        // Guardar categoría en el conjunto
        categorias.Add(categoria);

        // Guardar título e ISBN en el mapa
        mapaTitulos[titulo] = isbn;

        Console.WriteLine("\nLibro registrado correctamente.");
    }


    // =========================================================
    // MOSTRAR LIBROS
    // =========================================================

    static void MostrarLibros()
    {
        Console.WriteLine("LISTA DE LIBROS");
        Console.WriteLine("------------------------------------------");

        if (libros.Count == 0)
        {
            Console.WriteLine("No existen libros registrados.");
            return;
        }

        foreach (Libro libro in libros.Values)
        {
            Console.WriteLine($"ISBN: {libro.ISBN}");
            Console.WriteLine($"Título: {libro.Titulo}");
            Console.WriteLine($"Autor: {libro.Autor}");
            Console.WriteLine($"Categoría: {libro.Categoria}");
            Console.WriteLine("------------------------------------------");
        }
    }


    // =========================================================
    // BUSCAR POR ISBN
    // =========================================================

    static void BuscarPorISBN()
    {
        Console.WriteLine("BÚSQUEDA POR ISBN");
        Console.WriteLine("---------------------------");

        Console.Write("Ingrese el ISBN: ");
        string isbn = Console.ReadLine();

        if (libros.TryGetValue(isbn, out Libro libro))
        {
            Console.WriteLine("\nLibro encontrado:");

            Console.WriteLine($"ISBN: {libro.ISBN}");
            Console.WriteLine($"Título: {libro.Titulo}");
            Console.WriteLine($"Autor: {libro.Autor}");
            Console.WriteLine($"Categoría: {libro.Categoria}");
        }
        else
        {
            Console.WriteLine("\nLibro no encontrado.");
        }
    }


    // =========================================================
    // BUSCAR POR AUTOR
    // =========================================================

    static void BuscarPorAutor()
    {
        Console.WriteLine("BÚSQUEDA POR AUTOR");
        Console.WriteLine("---------------------------");

        Console.Write("Ingrese el nombre del autor: ");
        string autorBuscado = Console.ReadLine();

        bool encontrado = false;

        foreach (Libro libro in libros.Values)
        {
            if (libro.Autor.Equals(
                autorBuscado,
                StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("\n---------------------------");
                Console.WriteLine($"ISBN: {libro.ISBN}");
                Console.WriteLine($"Título: {libro.Titulo}");
                Console.WriteLine($"Autor: {libro.Autor}");
                Console.WriteLine($"Categoría: {libro.Categoria}");

                encontrado = true;
            }
        }

        if (!encontrado)
        {
            Console.WriteLine("\nNo existen libros de ese autor.");
        }
    }


    // =========================================================
    // MOSTRAR CONJUNTO
    // =========================================================

    static void MostrarCategorias()
    {
        Console.WriteLine("CONJUNTO DE CATEGORÍAS");
        Console.WriteLine("---------------------------");

        if (categorias.Count == 0)
        {
            Console.WriteLine("No existen categorías.");
            return;
        }

        foreach (string categoria in categorias)
        {
            Console.WriteLine("- " + categoria);
        }
    }


    // =========================================================
    // MOSTRAR MAPA
    // =========================================================

    static void MostrarMapaTitulos()
    {
        Console.WriteLine("MAPA ORDENADO DE TÍTULOS");
        Console.WriteLine("---------------------------");

        if (mapaTitulos.Count == 0)
        {
            Console.WriteLine("No existen libros registrados.");
            return;
        }

        foreach (var elemento in mapaTitulos)
        {
            Console.WriteLine(
                $"Título: {elemento.Key} | ISBN: {elemento.Value}");
        }
    }


    // =========================================================
    // REPORTE GENERAL
    // =========================================================

    static void ReporteGeneral()
    {
        Console.WriteLine("======================================");
        Console.WriteLine("           REPORTE GENERAL");
        Console.WriteLine("======================================");

        Console.WriteLine(
            $"Total de libros registrados: {libros.Count}");

        Console.WriteLine(
            $"Total de categorías diferentes: {categorias.Count}");

        Console.WriteLine("\nLIBROS:");
        Console.WriteLine("--------------------------------------");

        foreach (Libro libro in libros.Values)
        {
            Console.WriteLine(
                $"{libro.ISBN} | " +
                $"{libro.Titulo} | " +
                $"{libro.Autor} | " +
                $"{libro.Categoria}");
        }

        Console.WriteLine("\nCATEGORÍAS:");
        Console.WriteLine("--------------------------------------");

        foreach (string categoria in categorias)
        {
            Console.WriteLine("- " + categoria);
        }

        Console.WriteLine("\nMAPA TÍTULO - ISBN:");
        Console.WriteLine("--------------------------------------");

        foreach (var elemento in mapaTitulos)
        {
            Console.WriteLine(
                $"{elemento.Key} -> {elemento.Value}");
        }
    }


    // =========================================================
    // ANÁLISIS DEL TIEMPO
    // =========================================================

    static void AnalizarTiempo()
    {
        Console.WriteLine("ANÁLISIS DEL TIEMPO DE EJECUCIÓN");
        Console.WriteLine("--------------------------------------");

        if (libros.Count == 0)
        {
            Console.WriteLine(
                "Primero debe registrar al menos un libro.");
            return;
        }

        string isbnPrueba = null;

        foreach (string isbn in libros.Keys)
        {
            isbnPrueba = isbn;
            break;
        }

        Stopwatch reloj = new Stopwatch();

        reloj.Start();

        for (int i = 0; i < 100000; i++)
        {
            libros.ContainsKey(isbnPrueba);
        }

        reloj.Stop();

        Console.WriteLine(
            "Se realizaron 100000 búsquedas en el diccionario.");

        Console.WriteLine(
            $"Tiempo total: {reloj.ElapsedMilliseconds} ms");

        Console.WriteLine(
            $"Ticks: {reloj.ElapsedTicks}");

        Console.WriteLine(
            "\nLa búsqueda mediante Dictionary es eficiente " +
            "porque utiliza una clave única para localizar " +
            "los elementos.");
    }
}