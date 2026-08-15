using System;
using System.IO;

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

           CargarInventario(inventario, ref totalRegistros, CAPACIDAD);

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
                Console.WriteLine("║ 4. Buscar producto por ID            ║");
                Console.WriteLine("║ 5. Actualizar stock                  ║");
                Console.WriteLine("╚══════════════════════════════════════╝");
                Console.Write("\n Selecciona una opción: ");
                opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        RegistrarProducto(inventario, ref totalRegistros, CAPACIDAD);
                        break;
                    case "2":
                        MostrarProductos(inventario, totalRegistros);
                        break;
                    case "3":
                        GuardarInventario(inventario, totalRegistros);
                        Console.WriteLine("\n Cerrando el sistema... ¡Hasta pronto!");
                        break;
                    case "4":
                        BuscarProducto(inventario, totalRegistros);
                        break;
                    case "5":
                        ActualizarStock(inventario, totalRegistros);
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
            
            // ── Captura y validación de datos ──────────────────────────────
            Console.Write(" ID del producto : ");
            while (!int.TryParse(Console.ReadLine(), out inventario[total].ID))
            {
                Console.Write(" [!] Error: Ingresa un número entero válido.\n ID del producto : ");
            }
            
            Console.Write(" Nombre : ");
            inventario[total].Nombre = Console.ReadLine();
            
            Console.Write(" Precio unitario : $");
            while (!double.TryParse(Console.ReadLine(), out inventario[total].Precio))
            {
                Console.Write(" [!] Error: Ingresa un valor decimal válido.\n Precio unitario : $");
            }
            
            Console.Write(" Stock disponible : ");
            while (!int.TryParse(Console.ReadLine(), out inventario[total].Stock))
            {
                Console.Write(" [!] Error: Ingresa un número entero válido.\n Stock disponible : ");
            }
            
            // ── Incrementar el contador ───────────────────────────────────
            total++; 
            Console.WriteLine($"\n [✓] Producto registrado exitosamente. Total en inventario: {total}");
            Console.ReadLine();
        }
        static void MostrarProductos(Producto[] inventario, int total)
        {
            Console.Clear();
            Console.WriteLine("── LISTADO COMPLETO DE INVENTARIO ──\n");
            
            // ── Verificar si hay productos registrados ────────────────────
            if (total == 0)
            {
                Console.WriteLine(" [!] No hay productos registrados aún.");
                Console.ReadLine();
                return;
            }
            
            // ── Encabezado de tabla ───────────────────────────────────────
            Console.WriteLine($" {"ID",-6} {"Nombre",-20} {"Precio",10} {"Stock",8}");
            Console.WriteLine($" {new string('-', 48)}");
            
            // ── Ciclo de recorrido del arreglo ────────────────────────────
            for (int i = 0; i < total; i++)
            {
                Console.WriteLine(
                    $" {inventario[i].ID,-6} " +
                    $"{inventario[i].Nombre,-20} " +
                    $"${inventario[i].Precio,9:F2} " +
                    $"{inventario[i].Stock,8}"
                );
            }
            
            Console.WriteLine($"\n Total de productos: {total}");
            Console.ReadLine();
        }
        static void BuscarProducto(Producto[] inventario, int total)
        {
            Console.Clear();
            Console.WriteLine("── BUSCAR PRODUCTO POR ID ──\n");

            if (total == 0)
            {
                Console.WriteLine(" [!] No hay productos registrados aún.");
                Console.ReadLine();
                return;
            }

            Console.Write(" Ingresa el ID a buscar: ");
            int idBuscado = int.Parse(Console.ReadLine());
            bool encontrado = false;

            for (int i = 0; i < total; i++)
            {
                if (inventario[i].ID == idBuscado)
                {
                    Console.WriteLine("\n [✓] Producto encontrado:");
                    Console.WriteLine($" Nombre : {inventario[i].Nombre}");
                    Console.WriteLine($" Precio : ${inventario[i].Precio:F2}");
                    Console.WriteLine($" Stock  : {inventario[i].Stock}");
                    encontrado = true;
                    break;
                }
            }

            if (!encontrado)
            {
                Console.WriteLine("\n [X] Producto no encontrado.");
            }

            Console.ReadLine();
        }
        static void ActualizarStock(Producto[] inventario, int total)
        {
            Console.Clear();
            Console.WriteLine("── ACTUALIZAR STOCK ──\n");

            if (total == 0)
            {
                Console.WriteLine(" [!] No hay productos registrados aún.");
                Console.ReadLine();
                return;
            }

            Console.Write(" Ingresa el ID del producto a actualizar: ");
            int idBuscado = int.Parse(Console.ReadLine());
            bool encontrado = false;

            for (int i = 0; i < total; i++)
            {
                if (inventario[i].ID == idBuscado)
                {
                    Console.WriteLine($" Producto actual: {inventario[i].Nombre} | Stock actual: {inventario[i].Stock}");
                    Console.Write(" Ingresa el nuevo stock: ");
                    inventario[i].Stock = int.Parse(Console.ReadLine());
                    Console.WriteLine("\n [✓] Stock actualizado exitosamente.");
                    encontrado = true;
                    break;
                }
            }

            if (!encontrado)
            {
                Console.WriteLine("\n [X] Producto no encontrado.");
            }

            Console.ReadLine();
        }
        static void GuardarInventario(Producto[] inventario, int total)
        {
            string[] lineas = new string[total];
            for (int i = 0; i < total; i++)
            {
                // Formato CSV: ID,Nombre,Precio,Stock
                lineas[i] = $"{inventario[i].ID},{inventario[i].Nombre},{inventario[i].Precio},{inventario[i].Stock}";
            }
            File.WriteAllLines("inventario.csv", lineas);
        }

        static void CargarInventario(Producto[] inventario, ref int total, int capacidad)
        {
            if (File.Exists("inventario.csv"))
            {
                string[] lineas = File.ReadAllLines("inventario.csv");
                foreach (string linea in lineas)
                {
                    if (total >= capacidad) break;
                    
                    string[] datos = linea.Split(',');
                    inventario[total].ID = int.Parse(datos[0]);
                    inventario[total].Nombre = datos[1];
                    inventario[total].Precio = double.Parse(datos[2]);
                    inventario[total].Stock = int.Parse(datos[3]);
                    total++;
                }
            }
        }
    }
}
