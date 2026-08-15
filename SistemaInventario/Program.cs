using System;

namespace SistemaInventario
{
    /// <summary>
    /// Representa un producto dentro del inventario de la empresa.
    /// Se usa struct porque es un registro de datos pequeño y de tipo valor.
    /// </summary>
    struct Producto
    {
        public int ID; // Identificador único del producto
        public string Nombre; // Nombre descriptivo del artículo
        public double Precio; // Precio unitario en moneda local
        public int Stock; // Cantidad disponible en almacén
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.Title = "Sistema de Gestión de Inventario v1.0";
            
            // ── Configuración inicial del inventario ──────────────────────
            const int CAPACIDAD = 10; // Tamaño máximo del arreglo
            Producto[] inventario = new Producto[CAPACIDAD];
            int totalRegistros = 0; // Contador de productos registrados
            string opcion;

            do
            {
                // ── Menú principal ────────────────────────────────────────
                Console.Clear();
                Console.WriteLine("╔══════════════════════════════════════╗");
                Console.WriteLine("║     SISTEMA DE INVENTARIO - MENÚ     ║");
                Console.WriteLine("╠══════════════════════════════════════╣");
                Console.WriteLine("║ 1. Registrar producto                ║");
                Console.WriteLine("║ 2. Mostrar todos los productos       ║");
                Console.WriteLine("║ 3. Salir                             ║");
                Console.WriteLine("╚══════════════════════════════════════╝");
                Console.Write("\n Selecciona una opción: ");
                opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        // RegistrarProducto(inventario, ref totalRegistros, CAPACIDAD);
                        Console.WriteLine("\n [En construcción] Presiona Enter para continuar.");
                        Console.ReadLine();
                        break;
                    case "2":
                        // MostrarProductos(inventario, totalRegistros);
                        Console.WriteLine("\n [En construcción] Presiona Enter para continuar.");
                        Console.ReadLine();
                        break;
                    case "3":
                        Console.WriteLine("\n Cerrando el sistema... ¡Hasta pronto!");
                        break;
                    default:
                        Console.WriteLine("\n Opción inválida. Presiona Enter para continuar.");
                        Console.ReadLine();
                        break;
                }
            } while (opcion != "3");
        }
    }
}
