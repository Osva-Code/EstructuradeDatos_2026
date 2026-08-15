using System;
using FastCartFase3.Services;
using FastCartFase3.Inventory;

namespace FastCartFase3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("FastCart Enterprise - Módulo de Auditoría v3.0\n");

            // 1. Instanciar el servicio de auditoría[cite: 2]
            AuditoriaService auditoria = new AuditoriaService();

            // 2. Instanciar el inventario inyectando la auditoría[cite: 2]
            InventarioLista inventario = new InventarioLista(auditoria);

            try
            {
                Console.WriteLine("Ejecutando operaciones de inventario en el catálogo...\n");
                
                // Secuencia mínima: 2 inserciones, 1 actualización, 1 eliminación[cite: 2]
                inventario.AgregarProducto(new Producto { SKU = 1001, Nombre = "Laptop Pro", Precio = 25000.00, Stock = 10 });
                inventario.AgregarProducto(new Producto { SKU = 1002, Nombre = "Monitor 4K", Precio = 8500.50, Stock = 5 });
                
                inventario.ModificarPrecio(1001, 24000.00);
                
                inventario.EliminarProducto(1002);

                Console.WriteLine("✅ Operaciones finalizadas exitosamente. Generando reportes de auditoría...\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error operativo: {ex.Message}");
            }

            // Mostrar bitácora en ambas direcciones[cite: 2]
            auditoria.ImprimirHistorialCronologico();
            Console.WriteLine("\n--------------------------------------------------\n");
            auditoria.ImprimirHistorialInverso();
        }
    }
}