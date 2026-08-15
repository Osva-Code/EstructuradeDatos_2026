namespace FastCartFase3.Models
{
    public class NodoAuditoria
    {
        // La carga útil: el registro del evento[cite: 2].
        public LogMovimiento Dato;
        // Referencia al nodo más reciente (hacia la Cola)[cite: 2].
        public NodoAuditoria Siguiente;
        // Referencia al nodo más antiguo (hacia la Cabeza)[cite: 2].
        public NodoAuditoria Anterior;

        public NodoAuditoria(LogMovimiento log)
        {
            this.Dato = log;
            this.Siguiente = null;
            this.Anterior = null;
        }
    }
}