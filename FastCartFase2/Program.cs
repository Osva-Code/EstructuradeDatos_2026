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
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("FastCart Backend - Fase 2: Arquitectura Dinámica Inicializada");
        }
    }
}