namespace CalculadoraPunteros;

class Program
{
    static void Main(string[] args)
    {
        // Prueba del modificador 'ref' (muta el valor original en el Stack)
        int total = 4; // 1. Inicialización obligatoria para usar 'ref'
        Console.WriteLine($"Valor inicial en Main (antes de Sumar): {total}");
        
        // 2. Llamada usando 'ref' obligatoriamente en ambos lados de la firma
        Operaciones.Sumar(ref total, 6);
        
        // 3. Verificamos que el valor mutó directamente en la memoria
        Console.WriteLine($"Valor en Main (después de Sumar con ref): {total}"); 
    }
}