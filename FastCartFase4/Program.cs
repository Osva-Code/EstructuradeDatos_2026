using System;
using FastCartFase4.Models;
using FastCartFase4.Services;

namespace FastCartFase4
{
    class Program
    {
        static SimuladorCatalogoYBitacora sistema = new SimuladorCatalogoYBitacora();
        static ColaDespacho cola = new ColaDespacho();
        static PilaDevoluciones pila = new PilaDevoluciones();

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            
            // Precargar productos de prueba para demostrar integración inmediata
            sistema.AgregarProducto(1001, "Laptop Pro", 25000.00, 15);
            sistema.AgregarProducto(1002, "Monitor 4K", 8500.00, 10);

            bool ejecutando = true;
            while (ejecutando)
            {
                Console.Clear();
                Console.WriteLine("╔════════════════════════════════════════════╗");
                Console.WriteLine("║   MOTOR DE DESPACHO LOGÍSTICO v4.0 (UNITEC)║");
                Console.WriteLine("╠════════════════════════════════════════════╣");
                Console.WriteLine("║ [1] Mostrar Catálogo de Productos (Fase 2) ║");
                Console.WriteLine("║ [2] Ver Historial de Bitácora (Fase 3)     ║");
                Console.WriteLine("║ [3] Encolar Pedido de Cliente (FIFO)       ║");
                Console.WriteLine("║ [4] Despachar Pedido (Actualiza Stock)     ║");
                Console.WriteLine("║ [5] Registrar Devolución (LIFO)            ║");
                Console.WriteLine("║ [6] Procesar Devolución (Reintegra Stock)  ║");
                Console.WriteLine("║ [7] Ver Estado de Cola y Pila              ║");
                Console.WriteLine("║ [0] SALIR DEL SISTEMA                      ║");
                Console.WriteLine("╚════════════════════════════════════════════╝");
                Console.Write("\n Seleccione una opción: ");
                
                string opcion = Console.ReadLine();
                Console.WriteLine();

                switch (opcion)
                {
                    case "1":
                        sistema.MostrarCatalogo();
                        break;
                    case "2":
                        sistema.MostrarBitacora();
                        break;
                    case "3":
                        cola.EncolarPedido(new Pedido(501, 1001, 2, "Cliente Alpha"));
                        break;
                    case "4":
                        cola.DespacharPedido(sistema);
                        break;
                    case "5":
                        pila.PushDevolucion(new Devolucion(901, 1001, 1, "Cliente Beta", "Defecto de fábrica"));
                        break;
                    case "6":
                        pila.PopDevolucion(sistema);
                        break;
                    case "7":
                        Console.WriteLine($"[ESTADO] Pedidos en Cola (FIFO): {cola.TotalEncolados} | Devoluciones en Pila (LIFO): {pila.TotalDevoluciones}");
                        break;
                    case "0":
                        ejecutando = false;
                        Console.WriteLine("Saliendo del sistema logístico. ¡Excelente trabajo!");
                        break;
                    default:
                        Console.WriteLine("[!] Opción no válida.");
                        break;
                }

                if (opcion != "0")
                {
                    Console.WriteLine("\nPresione cualquier tecla para continuar...");
                    Console.ReadKey();
                }
            }
        }
    }
}