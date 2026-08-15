namespace CalculadoraPunteros;

class Program
{
    static void Main(string[] args)
    {
        // 1. Prueba del modificador 'ref'
        int total = 4;
        Console.WriteLine($"Valor inicial en Main (antes de Sumar): {total}");
        Operaciones.Sumar(ref total, 6);
        Console.WriteLine($"Valor en Main (después de Sumar con ref): {total}"); 
        
        Console.WriteLine(new string('-', 40));

        // 2. Prueba del modificador 'out'
        int[] datos = { 3, 8, 1, 7, 9, 2 };
        
        // Llamada usando 'out' (variables declaradas en línea)
        Operaciones.AnalizarValores(datos, out double prom, out int max);

        Console.WriteLine($"Arreglo analizado: {{ 3, 8, 1, 7, 9, 2 }}");
        Console.WriteLine($"Promedio calculado (out): {prom}");
        Console.WriteLine($"Valor máximo encontrado (out): {max}");
    }
}