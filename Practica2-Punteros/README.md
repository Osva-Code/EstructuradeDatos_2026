# Práctica 2: Simulación de Punteros en C#

**Nombre:** Nacatl Osvaldo Díaz de la Vega Caballero
**Matrícula:** 335025230

## Descripción
Esta práctica demuestra la manipulación directa de la memoria Stack en C# mediante el paso de parámetros por referencia. Se implementaron los modificadores `ref` y `out` para mutar variables originales y extraer múltiples resultados simultáneos desde un solo método, comprobando su impacto directamente en la consola.

## Instrucciones de Ejecución
Para ejecutar este proyecto desde la terminal, utiliza los siguientes comandos:

```bash
cd src/CalculadoraPunteros
dotnet run

## Reflexión personal
El uso de los modificadores ref y out me permitió comprender cómo interactuar directamente con las direcciones de memoria en el Stack sin depender del retorno tradicional de las funciones. Observé que ref requiere inicialización previa para mutar una variable existente, mientras que out resulta sumamente útil para inicializar y extraer múltiples valores desde dentro de un mismo método. Esta gestión precisa de las referencias es fundamental para optimizar el rendimiento y el flujo de los datos en aplicaciones estructuradas.