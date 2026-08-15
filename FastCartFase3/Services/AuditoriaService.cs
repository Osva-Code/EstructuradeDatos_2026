using System;
using FastCartFase3.Models;

namespace FastCartFase3.Services
{
    public class AuditoriaService
    {
        private NodoAuditoria Cabeza;
        private NodoAuditoria Cola;
        
        public int TotalRegistros { get; private set; }

        public AuditoriaService()
        {
            Cabeza = null;
            Cola = null;
            TotalRegistros = 0;
        }

        public void RegistrarEvento(string tipo, int sku, string desc)
        {
            LogMovimiento log = new LogMovimiento
            {
                Timestamp = DateTime.UtcNow,
                TipoOperacion = tipo,
                SKUAfectado = sku,
                Descripcion = desc
            };

            NodoAuditoria nuevoNodo = new NodoAuditoria(log);

            if (Cola != null)
            {
                Cola.Siguiente = nuevoNodo;
                nuevoNodo.Anterior = Cola;
            }
            
            Cola = nuevoNodo;
            
            if (Cabeza == null) 
            {
                Cabeza = Cola;
            }

            TotalRegistros++;
        }

        public void ImprimirHistorialCronologico()
        {
            if (Cabeza == null)
            {
                Console.WriteLine("[Bitácora vacía — no se han registrado eventos]");
                return;
            }

            Console.WriteLine("=== HISTORIAL CRONOLÓGICO (Antiguo -> Reciente) ===");
            NodoAuditoria actual = Cabeza;
            int contador = 1;

            while (actual != null)
            {
                Console.WriteLine($" [{contador}] {actual.Dato.Timestamp:yyyy-MM-dd HH:mm:ss} UTC");
                Console.WriteLine($" Operación : {actual.Dato.TipoOperacion}");
                Console.WriteLine($" SKU       : {actual.Dato.SKUAfectado}");
                Console.WriteLine($" Detalle   : {actual.Dato.Descripcion}\n");
                
                actual = actual.Siguiente;
                contador++;
            }
            Console.WriteLine($" Total de eventos: {contador - 1}");
        }

        public void ImprimirHistorialInverso()
        {
            if (!ValidarIntegridad())
                throw new InvalidOperationException("La lista presenta inconsistencias en punteros Anterior.");

            if (Cola == null)
            {
                Console.WriteLine("[Bitácora vacía — no se han registrado eventos]");
                return;
            }

            Console.WriteLine("=== HISTORIAL INVERSO (Reciente -> Antiguo) ===");
            NodoAuditoria actual = Cola;
            int contador = 1;

            while (actual != null)
            {
                Console.WriteLine($" [{contador}] {actual.Dato.Timestamp:yyyy-MM-dd HH:mm:ss} UTC");
                Console.WriteLine($" Operación : {actual.Dato.TipoOperacion}");
                Console.WriteLine($" SKU       : {actual.Dato.SKUAfectado}");
                Console.WriteLine($" Detalle   : {actual.Dato.Descripcion}\n");
                
                actual = actual.Anterior;
                contador++;
            }
            Console.WriteLine($" Total de eventos: {contador - 1}");
        }

        public bool ValidarIntegridad()
        {
            int conteoAdelante = 0, conteoAtras = 0;
            var actual = Cabeza;
            while (actual != null) { conteoAdelante++; actual = actual.Siguiente; }
            actual = Cola;
            while (actual != null) { conteoAtras++; actual = actual.Anterior; }
            return conteoAdelante == conteoAtras && conteoAdelante == TotalRegistros;
        }
    }
}