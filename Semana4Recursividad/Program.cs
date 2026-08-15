using System;
using System.Diagnostics;
using Semana4Recursividad;

// ═══════════════════════════════════════
// HARNESS DE PRUEBA COMPARATIVA
// ═══════════════════════════════════════
int nFactorial = 20; // Límite seguro para long
int nFibonacci = 40; // Suficientemente grande para ver diferencia

var sw = new Stopwatch();

Console.WriteLine("╔═══════════════════════════════════════════════════╗");
Console.WriteLine("║      COMPARATIVA: ITERATIVO vs RECURSIVO          ║");
Console.WriteLine("╚═══════════════════════════════════════════════════╝\n");

// ── FACTORIAL ──────────────────────────────────────────────
Console.WriteLine($"--- FACTORIAL({nFactorial}) ---");

sw.Restart();
long resultFactIter = AlgoritmosIterativos.FactorialIterativo(nFactorial);
sw.Stop();
Console.WriteLine($"[Iterativo] Resultado: {resultFactIter,25} Tiempo: {sw.Elapsed.TotalMilliseconds:F6} ms");

sw.Restart();
long resultFactRec = AlgoritmosRecursivos.FactorialRecursivo(nFactorial);
sw.Stop();
Console.WriteLine($"[Recursivo] Resultado: {resultFactRec,25} Tiempo: {sw.Elapsed.TotalMilliseconds:F6} ms");

// ── FIBONACCI ──────────────────────────────────────────────
Console.WriteLine($"\n--- FIBONACCI({nFibonacci}) ---");

sw.Restart();
long resultFibIter = AlgoritmosIterativos.FibonacciIterativo(nFibonacci);
sw.Stop();
Console.WriteLine($"[Iterativo] Resultado: {resultFibIter,25} Tiempo: {sw.Elapsed.TotalMilliseconds:F6} ms");

sw.Restart();
long resultFibRec = AlgoritmosRecursivos.FibonacciRecursivo(nFibonacci);
sw.Stop();
Console.WriteLine($"[Recursivo] Resultado: {resultFibRec,25} Tiempo: {sw.Elapsed.TotalMilliseconds:F6} ms");

Console.WriteLine("\n[OK] Prueba completada. Registra los tiempos en tu bitácora.");
