using AccesoDatos.Models;
using AccesoDatos.Repositories;
using AccesoDatos.Data;
using Microsoft.EntityFrameworkCore;

namespace AppConsola
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool salir = false;
            while (!salir)
            {
                Console.Clear();

                Console.WriteLine("=========================");
                Console.WriteLine("         SISTEMA BIBLIOTECA          ");
                Console.WriteLine("=========================");
                Console.WriteLine("1. Alta Autor");
                Console.WriteLine("2. Alta Libro");
                Console.WriteLine("3. Ver libro");
                Console.WriteLine("0. Salir");
                Console.WriteLine("=========================");
                Console.Write("Seleccione una opción: ");

                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        AltaAutor();
                        break;
                    case "2":
                        AltaLibro();
                        break;
                    case "3":
                        VerLibros();
                        break;
                    case "0":
                        salir = true;
                        break;
                    default:
                        Console.WriteLine("Opción inválida. Presione cualquier tecla para continuar...");
                        Console.ReadKey();
                        break;
                }
            }
        }
        // =========================
        // ALTA AUTOR
        // =========================
        static void AltaAutor()
        {
            Console.Clear();
            Console.WriteLine("=== ALTA AUTOR ===");
            Console.Write("Ingrese el nombre del autor: ");
            string nombre = Console.ReadLine();
            
            if (string.IsNullOrWhiteSpace(nombre))
            {
                Console.WriteLine("El nombre del autor no puede estar vacío. ");
                Pausar();
                return;
            }
            Autor autor = new Autor
            {
                Nombre = nombre
            };
            GenericRepository<Autor> repositorio = new GenericRepository<Autor>();
            repositorio.Agregar(autor);
            Console.WriteLine("Autor agregado correctamente.");
            Pausar();
        }
        // =========================
        // ALTA LIBRO
        // =========================
        static void AltaLibro()
        {
            Console.Clear();
            Console.WriteLine("=== ALTA LIBRO ===");
            using (AplicationDbContext context = new AplicationDbContext())
            {
                List<Autor> autores = context.Autores
                    .AsNoTracking()
                    .ToList();
                if (autores.Count == 0)
                {
                    Console.WriteLine("No hay autores disponibles. Debe agregar un autor primero.");
                    Pausar();
                    return;
                }
                Console.Write("Ingrese el título del libro: ");
                string titulo = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(titulo))
                {
                    Console.WriteLine("El título del libro no puede estar vacío.");
                    Pausar();
                    return;
                }
                Console.WriteLine("Ingrese el año de publicacion: ");
                if (!int.TryParse(Console.ReadLine(), out int anio))
                {
                    Console.WriteLine("Año de publicación inválido.");
                    Pausar();
                    return;
                }
                Console.WriteLine();
                Console.WriteLine("Autores disponibles:");
                for (int i = 0; i < autores.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {autores[i].Nombre}");
                }
                Console.WriteLine();
                Console.Write("Seleccione el autor: ");
                if (!int.TryParse(Console.ReadLine(), out int autorSeleccionado) || autorSeleccionado < 1 || autorSeleccionado > autores.Count)
                {
                    Console.WriteLine("Selección de autor inválida.");
                    Pausar();
                    return;
                }
                Autor autorElegido = autores[autorSeleccionado - 1];
                Libro libro = new Libro
                {
                    Titulo = titulo,
                    AnioPublicacion = anio,
                    AutorId = autorElegido.Id
                };
                GenericRepository<Libro> repositorio = new GenericRepository<Libro>();
                repositorio.Agregar(libro);

                Console.WriteLine();
                Console.WriteLine("Libro agregado correctamente.");
                Console.WriteLine($"Autor: {autorElegido.Nombre}");
            }
            Pausar();

        }
        // =========================
        // VER LIBRO
        // =========================
        static void VerLibros()
        {
            Console.Clear();

            Console.WriteLine("=== VER LIBROS ===");
            Console.WriteLine();

            using (AplicationDbContext context = new AplicationDbContext())
            {
                List<Libro> libros = context.Libros
                    .Include(l => l.Autor)
                    .AsNoTracking()
                    .ToList();
                if (libros.Count == 0)
                {
                    Console.WriteLine("No hay libros disponibles.");
                }
                else
                {
                    foreach (var libro in libros)
                    {
                        Console.WriteLine($"ID: {libro.Id}");
                        Console.WriteLine($"Título: {libro.Titulo}");
                        Console.WriteLine($"Año de publicación: {libro.AnioPublicacion}");
                        Console.WriteLine($"Autor: {libro.Autor.Nombre}");
                        Console.WriteLine("------------------------");
                    }
                }
            }
            Pausar();


        }
        




        static void Pausar()
        {
            Console.WriteLine();
            Console.WriteLine("Presione cualquier tecla para continuar...");
            Console.ReadKey();
        }
    } 




}

