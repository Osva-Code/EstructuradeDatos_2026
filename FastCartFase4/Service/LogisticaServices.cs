using System;
using System.Collections.Generic;
using FastCartFase4.Models;

namespace FastCartFase4.Services
{
    // Simulamos la estructura del catálogo (Fase 2) y Bitácora (Fase 3) para integración absoluta
    public class ProductoInventario
    {
        public int SKU { get; set; }
        public string Nombre { get; set; }
        public double Precio { get; set; }
        public int Stock { get; set; }
    }

    public class SimuladorCatalogoYBitacora
    {
        private Dictionary<int, ProductoInventario> inventario = new Dictionary<int, ProductoInventario>();
        public List<string> BitacoraEventos = new List<string>();

        public void AgregarProducto(int sku, string nombre, double precio, int stock)
        {
            inventario[sku] = new ProductoInventario { SKU = sku, Nombre = nombre, Precio = precio, Stock = stock };
            RegistrarEvento("INSERT", $"Producto SKU {sku} ({nombre}) añadido. Stock: {stock}.");
        }

        public ProductoInventario BuscarPorSKU(int sku)
        {
            if (inventario.ContainsKey(sku)) return inventario[sku];
            return null;
        }

        public void RegistrarEvento(string tipo, string desc)
        {
            string log = $"[{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss} UTC] {tipo} — {desc}";
            BitacoraEventos.Add(log);
            Console.WriteLine($"[AUDIT] {log}");
        }

        public void MostrarCatalogo()
        {
            Console.WriteLine("\n--- CATÁLOGO MAESTRO ACTUALIZADO ---");
            foreach (var p in inventario.Values)
            {
                Console.WriteLine($"SKU: {p.SKU} | Nombre: {p.Nombre} | Precio: ${p.Precio:F2} | Stock: {p.Stock}");
            }
        }

        public void MostrarBitacora()
        {
            Console.WriteLine("\n--- HISTORIAL DE BITÁCORA ---");
            foreach (var ev in BitacoraEventos) Console.WriteLine(ev);
        }
    }

    // ── COLA DE DESPACHO (FIFO) ──
    public class ColaDespacho
    {
        private NodoCola Frente;
        private NodoCola Fin;
        private int _totalEncolados;

        public ColaDespacho() { Frente = null; Fin = null; _totalEncolados = 0; }
        public bool EstaVacia() => Frente == null;
        public int TotalEncolados => _totalEncolados;

        public void EncolarPedido(Pedido nuevoPedido)
        {
            if (nuevoPedido == null) throw new ArgumentNullException("Pedido nulo.");
            NodoCola nuevoNodo = new NodoCola(nuevoPedido);
            
            if (EstaVacia())
            {
                Frente = nuevoNodo;
                Fin = nuevoNodo;
            }
            else
            {
                Fin.Siguiente = nuevoNodo;
                Fin = nuevoNodo;
            }
            _totalEncolados++;
            Console.WriteLine($"[COLA] Pedido #{nuevoPedido.IdPedido} (SKU: {nuevoPedido.SKU}) encolado. Total: {_totalEncolados}");
        }

        public Pedido DespacharPedido(SimuladorCatalogoYBitacora sistema)
        {
            if (EstaVacia())
            {
                Console.WriteLine("[COLA] La cola está vacía. No hay pedidos.");
                return null;
            }

            Pedido pedidoDespachado = Frente.Dato;
            Frente = Frente.Siguiente;
            if (Frente == null) Fin = null;
            _totalEncolados--;

            // Integración Fase 2 y 3: Buscar SKU y decrementar stock real
            var producto = sistema.BuscarPorSKU(pedidoDespachado.SKU);
            if (producto == null)
            {
                string err = $"SKU {pedidoDespachado.SKU} no encontrado en catálogo.";
                sistema.RegistrarEvento("DESPACHO_FALLIDO", err);
                return null;
            }

            if (producto.Stock < pedidoDespachado.Cantidad)
            {
                string err = $"Stock insuficiente para SKU {pedidoDespachado.SKU}. Disp: {producto.Stock}, Req: {pedidoDespachado.Cantidad}";
                sistema.RegistrarEvento("STOCK_INSUFICIENTE", err);
                return null;
            }

            producto.Stock -= pedidoDespachado.Cantidad;
            string exito = $"Pedido #{pedidoDespachado.IdPedido} despachado. SKU: {pedidoDespachado.SKU}, Stock restante: {producto.Stock}";
            sistema.RegistrarEvento("DESPACHO_EXITOSO", exito);
            Console.WriteLine($"[COLA] {exito}");
            return pedidoDespachado;
        }
    }

    // ── PILA DE DEVOLUCIONES (LIFO) ──
    public class PilaDevoluciones
    {
        private NodoPila Top;
        private int _totalDevoluciones;

        public PilaDevoluciones() { Top = null; _totalDevoluciones = 0; }
        public bool EstaVacia() => Top == null;
        public int TotalDevoluciones => _totalDevoluciones;

        public void PushDevolucion(Devolucion nuevaDevolucion)
        {
            if (nuevaDevolucion == null) throw new ArgumentNullException("Devolución nula.");
            NodoPila nuevoNodo = new NodoPila(nuevaDevolucion);
            nuevoNodo.Siguiente = Top;
            Top = nuevoNodo;
            _totalDevoluciones++;
            Console.WriteLine($"[PILA] Devolución #{nuevaDevolucion.IdDevolucion} (SKU: {nuevaDevolucion.SKU}) en cima. Total: {_totalDevoluciones}");
        }

        public Devolucion PopDevolucion(SimuladorCatalogoYBitacora sistema)
        {
            if (EstaVacia())
            {
                Console.WriteLine("[PILA] La pila de devoluciones está vacía.");
                return null;
            }

            Devolucion devProcesada = Top.Dato;
            Top = Top.Siguiente;
            _totalDevoluciones--;

            var producto = sistema.BuscarPorSKU(devProcesada.SKU);
            if (producto == null)
            {
                string err = $"SKU {devProcesada.SKU} no encontrado para devolución.";
                sistema.RegistrarEvento("DEVOLUCION_FALLIDA", err);
                return null;
            }

            // Reintegrar stock al catálogo central[cite: 3]
            producto.Stock += devProcesada.Cantidad;
            string exito = $"Devolución #{devProcesada.IdDevolucion} procesada. SKU: {devProcesada.SKU}, Stock reintegrado: +{devProcesada.Cantidad}, Nuevo stock: {producto.Stock}";
            sistema.RegistrarEvento("DEVOLUCION_EXITOSA", exito);
            Console.WriteLine($"[PILA] {exito}");
            return devProcesada;
        }
    }
}