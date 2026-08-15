using System;
using FastCartFase3.Models;

namespace FastCartFase3.Services
{
    public class AuditoriaService
    {
        // Puntero al nodo más antiguo (primer evento registrado).
        private NodoAuditoria Cabeza;
        // Puntero al nodo más reciente (último evento registrado)[cite: 2].
        private NodoAuditoria Cola;
        
        public int TotalRegistros { get; private set; }

        public AuditoriaService()
        {
            Cabeza = null; // Lista vacía al inicializar el servicio[cite: 2].
            Cola = null;
            TotalRegistros = 0;
        }

        public void RegistrarEvento(string tipo, int sku, string desc)
        {
            // Construir la carga útil del evento con UTC[cite: 2]
            LogMovimiento log = new LogMovimiento
            {
                Timestamp = DateTime.UtcNow,
                TipoOperacion = tipo,
                SKUAfectado = sku,
                Descripcion = desc
            };

            NodoAuditoria nuevoNodo = new NodoAuditoria(log);

            // Orden estricto de 3 pasos para evitar NullReferenceException[cite: 2]
            if (Cola != null)
            {
                Cola.Siguiente = nuevoNodo; // (1) enlazar Cola -> nuevoNodo[cite: 2]
                nuevoNodo.Anterior = Cola;  // (2) enlazar nuevoNodo -> Cola[cite: 2]
            }
            
            Cola = nuevoNodo; // (3) avanzar Cola al nuevo nodo[cite: 2]
            
            if (Cabeza == null) 
            {
                Cabeza = Cola; // lista vacía: inicializar Cabeza[cite: 2]
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
            // Validar integridad antes de recorrer (Escenario 2 de Prevención)[cite: 2]
            if (!ValidarIntegridad())
                throw new InvalidOperationException("La lista presenta inconsistencias en punteros Anterior. Ejecute ValidarIntegridad() para diagnóstico."[cite: 2]);

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
                
                actual = actual.Anterior; // Mover el cursor usando el puntero Anterior[cite: 2]
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