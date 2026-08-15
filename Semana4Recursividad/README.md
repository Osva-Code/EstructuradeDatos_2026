# Práctica 4: Implementación Segura de Recursividad en C#

## Resultados de la Prueba de Rendimiento (Benchmark)

**Factorial (n=20)**
* Iterativo: 0.125400 ms
* Recursivo: 0.102900 ms

**Fibonacci (n=40)**
* Iterativo: 0.084400 ms
* Recursivo: 963.954400 ms

## Conclusión
La prueba demuestra empíricamente una enorme diferencia de rendimiento al calcular la serie de Fibonacci. El método iterativo es predecible, seguro para la memoria (complejidad espacial O(1)) y altamente eficiente con una complejidad de tiempo O(n). Por el contrario, el método recursivo *naïve* sufre de un costo exponencial O(2^n), realizando millones de llamadas innecesarias que elevaron el tiempo a más de 963 ms. Esto evidencia la necesidad de controlar la recursividad mediante un caso base explícito para evitar llenar el Call Stack y provocar un `StackOverflowException`.