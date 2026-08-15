using System;
using System.Diagnostics;

namespace FastCartCore
{
    public struct Proveedor
    {
        public int IdProveedor;
        public string NombreCorporativo;
    }

    public struct Producto
    {
        public int SKU;
        public string Nombre;
        public double Precio;
        public int Stock;
        public Proveedor DatosProveedor;
    }

    // ── Motor de Ordenamiento ───────────────────────────────────────
    public static class OrdenamientoService
    {
        public static void ShellSort(Producto[] catalogo)
        {
            int n = catalogo.Length;
            int gap = 1;
            
            // Calcular gap inicial con secuencia de Knuth (h = 3h + 1)
            while (gap < n / 3)
                gap = gap * 3 + 1; 

            while (gap >= 1)
            {
                // Insertion Sort con la brecha actual
                for (int i = gap; i < n; i++)
                {
                    Producto temp = catalogo[i];
                    int j = i;
                    
                    // Comparar: Precio DESC, SKU ASC (desempate)
                    while (j >= gap && EsMayor(catalogo[j - gap], temp))
                    {
                        catalogo[j] = catalogo[j - gap];
                        j -= gap;
                    }
                    catalogo[j] = temp;
                }
                gap = gap / 3; // Reducir brecha (Knuth)
            }
        }

        // Criterio: A es "mayor" (debe ir después) si su precio es menor,
        // o si el precio es igual y su SKU es mayor.
        private static bool EsMayor(Producto a, Producto b)
        {
            if (a.Precio != b.Precio)
                return a.Precio < b.Precio; // DESC: menor precio va después
            return a.SKU > b.SKU;           // ASC: mayor SKU va después
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("FastCart Backend Core - Fase 1 Inicializada\n");

            // Inicializar lote de prueba (mínimo 50, usaremos 500 para un buen benchmark)
            int totalProductos = 500;
            Producto[] catalogo = GenerarCatalogo(totalProductos);

            Console.WriteLine($"Total de productos a procesar: {totalProductos}");
            Console.WriteLine("Catálogo antes de ordenar:");
            MostrarPrimeros(catalogo, 5);

            // ── Medición de Rendimiento ───────────────────────────────────
            var sw = new Stopwatch();
            sw.Start();
            
            OrdenamientoService.ShellSort(catalogo);
            
            sw.Stop();
            // ──────────────────────────────────────────────────────────────

            Console.WriteLine("\nCatálogo después de ordenar (Precio DESC, SKU ASC):");
            MostrarPrimeros(catalogo, 5);

            Console.WriteLine($"\nTiempo de ejecución ShellSort:");
            Console.WriteLine($" {sw.ElapsedMilliseconds} ms");
            Console.WriteLine($" {sw.ElapsedTicks} ticks");
            Console.WriteLine($" {sw.Elapsed.TotalMicroseconds:F2} µs");
        }

        // ── Utilidades de Generación y Visualización ──────────────────
        static Producto[] GenerarCatalogo(int cantidad)
        {
            Producto[] catalogo = new Producto[cantidad];
            Random rnd = new Random(12345); // Semilla fija para reproducibilidad

            for (int i = 0; i < cantidad; i++)
            {
                catalogo[i] = new Producto
                {
                    SKU = 1001 + i,
                    Nombre = $"Prod-{i}",
                    Precio = Math.Round(rnd.NextDouble() * (9999.99 - 10.00) + 10.00, 2),
                    Stock = rnd.Next(0, 501),
                    DatosProveedor = new Proveedor { IdProveedor = rnd.Next(1, 20), NombreCorporativo = $"Prov-{rnd.Next(1, 20)}" }
                };
            }

            // Forzar empates para probar el criterio del SKU
            if (cantidad >= 3)
            {
                catalogo[5].Precio = 500.00;
                catalogo[12].Precio = 500.00;
                catalogo[25].Precio = 500.00;
            }

            return catalogo;
        }

        static void MostrarPrimeros(Producto[] catalogo, int cantidad)
        {
            Console.WriteLine($" {"SKU",-6} | {"Precio",-10} | {"Stock",-6} | {"Nombre"}");
            Console.WriteLine($" {new string('-', 45)}");
            for (int i = 0; i < Math.Min(cantidad, catalogo.Length); i++)
            {
                Console.WriteLine($" {catalogo[i].SKU,-6} | ${catalogo[i].Precio,-9:F2} | {catalogo[i].Stock,-6} | {catalogo[i].Nombre}");
            }
        }
    }
}