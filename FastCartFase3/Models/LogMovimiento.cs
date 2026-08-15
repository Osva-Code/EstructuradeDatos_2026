using System;

namespace FastCartFase3.Models
{
    public struct LogMovimiento
    {
        // Marca temporal con precisión UTC de milisegundo.
        public DateTime Timestamp;
        // Categoría del evento: INSERT, UPDATE, DELETE, RESTOCK, PRICE_CHANGE.
        public string TipoOperacion;
        // Identificador único del producto afectado.
        public int SKUAfectado;
        // Descripción legible por humanos del cambio[cite: 2].
        public string Descripcion;
    }
}