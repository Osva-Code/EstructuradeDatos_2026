using System;
using System.Collections.Generic;
using FastCartFase3.Services;

namespace FastCartFase3.Inventory
{
    public class Producto
    {
        public int SKU { get; set; }
        public string Nombre { get; set; }
        public double Precio { get; set; }
        public int Stock { get; set; }
    }

    public class NodoProducto
    {
        public Producto Data { get; set; }
        public NodoProducto Siguiente { get; set; }
        public NodoProducto(Producto p) { Data = p; Siguiente = null; }
    }

    public class InventarioLista
    {
        private NodoProducto _cabeza;
        private AuditoriaService _auditoria;

        // Inyección de dependencia con validación de nulidad (Escenario 3 de Prevención)
        public InventarioLista(AuditoriaService auditoria)
        {
            _auditoria = auditoria ?? throw new ArgumentNullException(nameof(auditoria), "AuditoriaService es requerido.");
        }

        public void AgregarProducto(Producto p)
        {
            NodoProducto nuevo = new NodoProducto(p);
            if (_cabeza == null) _cabeza = nuevo;
            else
            {
                NodoProducto actual = _cabeza;
                while (actual.Siguiente != null) actual = actual.Siguiente;
                actual.Siguiente = nuevo;
            }

            // Disparar auditoría DESPUÉS de confirmar la inserción[cite: 2]
            _auditoria.RegistrarEvento("INSERT", p.SKU, $"Producto '{p.Nombre}' agregado. Precio: {p.Precio:C2}. Stock: {p.Stock}.[cite: 2]");
        }

        public void ModificarPrecio(int sku, double nuevoPrecio)
        {
            NodoProducto actual = _cabeza;
            while (actual != null && actual.Data.SKU != sku) actual = actual.Siguiente;
            
            if (actual == null) throw new KeyNotFoundException($"SKU {sku} no existe.");

            double precioAnterior = actual.Data.Precio;
            actual.Data.Precio = nuevoPrecio;

            // Auditar DESPUÉS de confirmar la mutación capturando ambos valores[cite: 2]
            _auditoria.RegistrarEvento("PRICE_CHANGE", sku, $"Precio actualizado de {precioAnterior:C2} a {nuevoPrecio:C2}.[cite: 2]");
        }

        public void EliminarProducto(int sku)
        {
            if (_cabeza == null) throw new InvalidOperationException("Inventario vacío.");

            string nombreEliminado;
            if (_cabeza.Data.SKU == sku)
            {
                nombreEliminado = _cabeza.Data.Nombre;
                _cabeza = _cabeza.Siguiente;
            }
            else
            {
                NodoProducto anterior = _cabeza;
                while (anterior.Siguiente != null && anterior.Siguiente.Data.SKU != sku)
                    anterior = anterior.Siguiente;

                if (anterior.Siguiente == null) throw new KeyNotFoundException($"SKU {sku} no encontrado.");
                
                nombreEliminado = anterior.Siguiente.Data.Nombre;
                anterior.Siguiente = anterior.Siguiente.Siguiente;
            }

            // El nodo ya fue desenlazado; el registro es válido[cite: 2]
            _auditoria.RegistrarEvento("DELETE", sku, $"Producto '{nombreEliminado}' eliminado del catálogo.[cite: 2]");
        }
    }
}