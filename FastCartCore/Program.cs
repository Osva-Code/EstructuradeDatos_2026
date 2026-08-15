using System;

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

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("FastCart Backend Core - Fase 1 Inicializada");
        }
    }
}