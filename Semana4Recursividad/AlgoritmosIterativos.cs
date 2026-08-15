using System;

namespace Semana4Recursividad
{
    /// <summary>
    /// Implementaciones iterativas como línea base de rendimiento.
    /// Complejidad Tiempo: O(n) | Complejidad Espacio: O(1)
    /// </summary>
    public static class AlgoritmosIterativos
    {
        // Factorial iterativo: n! = n * (n-1) * ... * 1
        public static long FactorialIterativo(int n)
        {
            if (n < 0) throw new ArgumentException("n debe ser >= 0");
            long resultado = 1;
            for (int i = 2; i <= n; i++)
                resultado *= i;
            return resultado;
        }

        // Fibonacci iterativo: F(n) = F(n-1) + F(n-2), F(0)=0, F(1)=1
        public static long FibonacciIterativo(int n)
        {
            if (n < 0) throw new ArgumentException("n debe ser >= 0");
            if (n == 0) return 0;
            if (n == 1) return 1;

            long anterior = 0, actual = 1;
            for (int i = 2; i <= n; i++)
            {
                long siguiente = anterior + actual;
                anterior = actual;
                actual = siguiente;
            }
            return actual;
        }
    }
}