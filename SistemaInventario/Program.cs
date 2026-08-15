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
                        RegistrarProducto(inventario, ref totalRegistros, CAPACIDAD);
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
        static void RegistrarProducto(Producto[] inventario, ref int total, int capacidad)
        {
            Console.Clear();
            Console.WriteLine("── REGISTRAR NUEVO PRODUCTO ──\n");
            
            // ── Validación de capacidad ───────────────────────────────────
            if (total >= capacidad)
            {
                Console.WriteLine(" [!] El inventario está lleno. No se pueden agregar más productos.");
                Console.ReadLine();
                return;
            }
            
            // ── Captura de datos del usuario ──────────────────────────────
            Console.Write(" ID del producto : ");
            inventario[total].ID = int.Parse(Console.ReadLine());
            
            Console.Write(" Nombre : ");
            inventario[total].Nombre = Console.ReadLine();
            
            Console.Write(" Precio unitario : $");
            inventario[total].Precio = double.Parse(Console.ReadLine());
            
            Console.Write(" Stock disponible : ");
            inventario[total].Stock = int.Parse(Console.ReadLine());
            
            // ── Incrementar el contador ───────────────────────────────────
            total++; // Avanza el cursor al siguiente slot disponible
            Console.WriteLine($"\n [✓] Producto registrado exitosamente. Total en inventario: {total}");
            Console.ReadLine();
        }
    }
}
