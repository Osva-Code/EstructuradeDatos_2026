using System;
using System.Collections.Generic;

namespace FastCartFase2
{
    // ── Estructuras Base ──
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

    // ── Modelado del Nodo Dinámico ──
    /// <summary>
    /// Representa un nodo de la lista enlazada para el catálogo de productos.
    /// </summary>
    public class NodoProducto
    {
        public Producto Data { get; set; }
        public NodoProducto Siguiente { get; set; }

        public NodoProducto(Producto producto)
        {
            this.Data = producto;
            this.Siguiente = null; // apunta a nada al crearse
        }
    }

    // ── Clase Controladora: InventarioLista ──
    /// <summary>
    /// Administra la lista enlazada simple del catálogo maestro.
    /// </summary>
    public class InventarioLista
    {
        private NodoProducto _cabeza;

        /// <summary>
        /// Inserta un nuevo producto al frente de la lista. Operación O(1).
        /// </summary>
        public void InsertarInicio(Producto p)
        {
            var nuevo = new NodoProducto(p);
            nuevo.Siguiente = _cabeza;
            _cabeza = nuevo;
        }

        /// <summary>
        /// Recorre la lista para encontrar la posición correcta según precio ascendente. Operación O(n).
        /// </summary>
        public void InsertarOrdenado(Producto p)
        {
            var nuevo = new NodoProducto(p);
            
            // Si la lista está vacía o el nuevo precio es menor que la cabeza
            if (_cabeza == null || p.Precio < _cabeza.Data.Precio)
            {
                nuevo.Siguiente = _cabeza;
                _cabeza = nuevo;
                return;
            }

            var actual = _cabeza;
            while (actual.Siguiente != null && actual.Siguiente.Data.Precio <= p.Precio)
            {
                actual = actual.Siguiente;
            }
            nuevo.Siguiente = actual.Siguiente;
            actual.Siguiente = nuevo;
        }

        /// <summary>
        /// Recorrido lineal O(n) que retorna la estructura Producto. Lanza una excepción si no existe.
        /// </summary>
        public Producto BuscarPorSKU(int sku)
        {
            var actual = _cabeza;
            while (actual != null)
            {
                if (actual.Data.SKU == sku)
                    return actual.Data;
                actual = actual.Siguiente;
            }
            throw new KeyNotFoundException($"SKU {sku} no encontrado.");
        }

        /// <summary>
        /// Desenlaza el nodo objetivo reapuntando el anterior a su sucesor. Operación O(n).
        /// </summary>
        public void EliminarPorSKU(int sku)
        {
            if (_cabeza == null) return;
            
            if (_cabeza.Data.SKU == sku)
            {
                _cabeza = _cabeza.Siguiente;
                return;
            }

            var anterior = _cabeza;
            while (anterior.Siguiente != null)
            {
                if (anterior.Siguiente.Data.SKU == sku)
                {
                    anterior.Siguiente = anterior.Siguiente.Siguiente;
                    return;
                }
                anterior = anterior.Siguiente;
            }
        }

        /// <summary>
        /// Recorre la lista secuencialmente para imprimir sus elementos. Operación O(n).
        /// </summary>
        public void MostrarCatalogo()
        {
            var actual = _cabeza;
            Console.WriteLine($" {"SKU",-6} | {"Precio",-10} | {"Stock",-6} | {"Nombre"}");
            Console.WriteLine($" {new string('-', 45)}");
            
            if (actual == null)
            {
                Console.WriteLine(" [!] El catálogo está vacío.");
                return;
            }

            while (actual != null)
            {
                Console.WriteLine($" {actual.Data.SKU,-6} | ${actual.Data.Precio,-9:F2} | {actual.Data.Stock,-6} | {actual.Data.Nombre}");
                actual = actual.Siguiente;
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("FastCart Backend - Fase 2: Arquitectura Dinámica Inicializada\n");

            InventarioLista catalogoDinamico = new InventarioLista();
            Random rnd = new Random(98765); // Semilla fija para reproducibilidad

            Console.WriteLine("Insertando 15 productos de forma dinámica...\n");
            
            for (int i = 1; i <= 15; i++)
            {
                Producto p = new Producto
                {
                    SKU = 3000 + i,
                    Nombre = $"ProdDyn-{i}",
                    Precio = Math.Round(rnd.NextDouble() * (3500.00 - 50.00) + 50.00, 2),
                    Stock = rnd.Next(1, 100),
                    DatosProveedor = new Proveedor { IdProveedor = 1, NombreCorporativo = "GlobalLogistics" }
                };
                
                // Usamos InsertarOrdenado para validar que la lista se auto-ordena por precio ascendente
                catalogoDinamico.InsertarOrdenado(p);
            }

            Console.WriteLine("Catálogo Maestro (Lista Enlazada Ordenada por Precio Ascendente):");
            catalogoDinamico.MostrarCatalogo();
        }
    }
}