namespace Semana4Recursividad
{
    public static class AlgoritmosRecursivos
    {
        // ── CASO BASE ─── if (n <= 1) detiene la recursión
        // ── CASO RECURSIVO ─── n * Factorial(n-1) continúa la cadena
        public static long FactorialRecursivo(int n)
        {
            if (n < 0) throw new ArgumentException("n debe ser >= 0");
            if (n <= 1) return 1; // <<< CASO BASE: frena el Stack Overflow
            return n * FactorialRecursivo(n - 1); // <<< CASO RECURSIVO
        }

        // Fibonacci Recursivo — ADVERTENCIA: O(2^n) sin memoización
        // Para n > 35 el tiempo de cómputo se vuelve visible. Para n > 50, impracticable.
        public static long FibonacciRecursivo(int n)
        {
            if (n < 0) throw new ArgumentException("n debe ser >= 0");
            if (n == 0) return 0; // <<< CASO BASE 1
            if (n == 1) return 1; // <<< CASO BASE 2
            
            return FibonacciRecursivo(n - 1) + FibonacciRecursivo(n - 2); // CASO RECURSIVO
        }
    }
}