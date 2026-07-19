using System;
using System.Collections.Generic;
using System.Diagnostics;

// Representa a cada persona que llega a la atracción.
class Persona
{
    public string Cedula { get; set; }
    public string Nombre { get; set; }
    public int NumeroAsiento { get; set; }

    public Persona(string cedula, string nombre)
    {
        Cedula = cedula;
        Nombre = nombre;
        NumeroAsiento = 0;
    }

    public override string ToString()
    {
        return Cedula + " - " + Nombre;
    }
}

// Administra la cola y la asignación de los 30 asientos.
class SistemaAtraccion
{
    private const int CAPACIDAD = 30;
    private Queue<Persona> cola = new Queue<Persona>();
    private List<Persona> personasConAsiento = new List<Persona>();

    public int CuposDisponibles()
    {
        return CAPACIDAD - cola.Count - personasConAsiento.Count;
    }

    public void RegistrarPersona(string cedula, string nombre)
    {
        if (CuposDisponibles() == 0)
        {
            Console.WriteLine("No existen cupos disponibles.");
            return;
        }

        Persona nuevaPersona = new Persona(cedula, nombre);
        cola.Enqueue(nuevaPersona);

        Console.WriteLine("Persona agregada al final de la cola.");
        Console.WriteLine("Posición actual: " + cola.Count);
    }

    public void AsignarAsiento()
    {
        if (cola.Count == 0)
        {
            Console.WriteLine("No hay personas en la cola.");
            return;
        }

        Persona personaAtendida = cola.Dequeue();
        personaAtendida.NumeroAsiento = personasConAsiento.Count + 1;
        personasConAsiento.Add(personaAtendida);

        Console.WriteLine("Asiento asignado correctamente.");
        Console.WriteLine("Asiento N.° " + personaAtendida.NumeroAsiento +
                          " para " + personaAtendida.Nombre);
    }

    public void ConsultarSiguiente()
    {
        if (cola.Count == 0)
        {
            Console.WriteLine("La cola está vacía.");
            return;
        }

        Console.WriteLine("Siguiente persona: " + cola.Peek());
    }

    public void MostrarCola()
    {
        Console.WriteLine("\n--- REPORTE DE PERSONAS EN COLA ---");

        if (cola.Count == 0)
        {
            Console.WriteLine("No hay personas esperando.");
            return;
        }

        int posicion = 1;
        foreach (Persona persona in cola)
        {
            Console.WriteLine(posicion + ". " + persona);
            posicion++;
        }

        Console.WriteLine("Total en cola: " + cola.Count);
    }

    public void MostrarAsientosAsignados()
    {
        Console.WriteLine("\n--- REPORTE DE ASIENTOS ASIGNADOS ---");

        if (personasConAsiento.Count == 0)
        {
            Console.WriteLine("Todavía no se han asignado asientos.");
            return;
        }

        foreach (Persona persona in personasConAsiento)
        {
            Console.WriteLine("Asiento " + persona.NumeroAsiento +
                              " | " + persona);
        }

        Console.WriteLine("Total vendidos: " + personasConAsiento.Count);
        Console.WriteLine("Cupos disponibles: " + CuposDisponibles());
    }

    public void MedirTiempoEjecucion()
    {
        const int OPERACIONES = 100000;
        Queue<int> colaPrueba = new Queue<int>();
        Stopwatch reloj = new Stopwatch();

        reloj.Start();

        for (int i = 0; i < OPERACIONES; i++)
        {
            colaPrueba.Enqueue(i);
        }

        while (colaPrueba.Count > 0)
        {
            colaPrueba.Dequeue();
        }

        reloj.Stop();

        Console.WriteLine("\n--- ANÁLISIS DE TIEMPO ---");
        Console.WriteLine("Operaciones Enqueue: " + OPERACIONES);
        Console.WriteLine("Operaciones Dequeue: " + OPERACIONES);
        Console.WriteLine("Tiempo total: " + reloj.Elapsed.TotalMilliseconds + " ms");
        Console.WriteLine("El resultado puede variar según el computador utilizado.");
    }

    public void MostrarResumen()
    {
        Console.WriteLine("Personas en cola: " + cola.Count +
                          " | Asientos vendidos: " + personasConAsiento.Count +
                          " | Cupos disponibles: " + CuposDisponibles());
    }
}

class Program
{
    static void Main()
    {
        SistemaAtraccion sistema = new SistemaAtraccion();
        int opcion;

        do
        {
            Console.Clear();
            Console.WriteLine("============================================");
            Console.WriteLine(" SISTEMA DE ASIGNACIÓN DE 30 ASIENTOS");
            Console.WriteLine(" Estructura utilizada: COLA FIFO");
            Console.WriteLine("============================================");
            sistema.MostrarResumen();
            Console.WriteLine();
            Console.WriteLine("1. Registrar persona");
            Console.WriteLine("2. Asignar asiento al siguiente");
            Console.WriteLine("3. Consultar siguiente persona");
            Console.WriteLine("4. Mostrar reporte de la cola");
            Console.WriteLine("5. Mostrar asientos asignados");
            Console.WriteLine("6. Medir tiempo de ejecución");
            Console.WriteLine("0. Salir");
            Console.Write("Seleccione una opción: ");

            if (!int.TryParse(Console.ReadLine(), out opcion))
            {
                opcion = -1;
            }

            Console.WriteLine();

            switch (opcion)
            {
                case 1:
                    Console.Write("Ingrese la cédula: ");
                    string cedula = Console.ReadLine();

                    Console.Write("Ingrese el nombre: ");
                    string nombre = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(cedula) ||
                        string.IsNullOrWhiteSpace(nombre))
                    {
                        Console.WriteLine("La cédula y el nombre son obligatorios.");
                    }
                    else
                    {
                        sistema.RegistrarPersona(cedula, nombre);
                    }
                    break;

                case 2:
                    sistema.AsignarAsiento();
                    break;

                case 3:
                    sistema.ConsultarSiguiente();
                    break;

                case 4:
                    sistema.MostrarCola();
                    break;

                case 5:
                    sistema.MostrarAsientosAsignados();
                    break;

                case 6:
                    sistema.MedirTiempoEjecucion();
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
                Console.WriteLine("\nPresione ENTER para continuar...");
                Console.ReadLine();
            }

        } while (opcion != 0);
    }
}
