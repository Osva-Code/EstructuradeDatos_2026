namespace CalculadoraPunteros;

public static class Operaciones
{
    // Usaremos 'ref' para mutar un acumulador directamente
    public static void Sumar(ref int acumulador, int sumando)
    {
        acumulador += sumando; 
    }

    // Usaremos 'out' para producir múltiples resultados de un arreglo
    public static void AnalizarValores(int[] valores, out double promedio, out int maximo)
    {
        double suma = 0;
        maximo = valores[0];
        
        foreach (int v in valores)
        {
            suma += v;
            if (v > maximo) maximo = v;
        }
        promedio = suma / valores.Length;
    }
}