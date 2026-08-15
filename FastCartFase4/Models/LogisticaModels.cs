using System;

namespace FastCartFase4.Models
{
    public class Pedido
    {
        public int IdPedido { get; set; }
        public int SKU { get; set; }
        public int Cantidad { get; set; }
        public string Cliente { get; set; }
        public DateTime Timestamp { get; set; }

        public Pedido(int id, int sku, int cantidad, string cliente)
        {
            IdPedido = id;
            SKU = sku;
            Cantidad = cantidad;
            Cliente = cliente;
            Timestamp = DateTime.Now;
        }
    }

    public class Devolucion
    {
        public int IdDevolucion { get; set; }
        public int SKU { get; set; }
        public int Cantidad { get; set; }
        public string Cliente { get; set; }
        public string Motivo { get; set; }
        public DateTime Timestamp { get; set; }

        public Devolucion(int id, int sku, int cantidad, string cliente, string motivo)
        {
            IdDevolucion = id;
            SKU = sku;
            Cantidad = cantidad;
            Cliente = cliente;
            Motivo = motivo;
            Timestamp = DateTime.Now;
        }
    }

    public class NodoCola
    {
        public Pedido Dato { get; set; }
        public NodoCola Siguiente { get; set; }
        public NodoCola(Pedido pedido) { Dato = pedido; Siguiente = null; }
    }

    public class NodoPila
    {
        public Devolucion Dato { get; set; }
        public NodoPila Siguiente { get; set; }
        public NodoPila(Devolucion devolucion) { Dato = devolucion; Siguiente = null; }
    }
}