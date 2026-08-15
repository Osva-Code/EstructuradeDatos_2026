# Práctica 5: Sistema de Gestión de Inventario Básico

## Descripción
Aplicación de consola en C# para la administración de productos basada en el uso de estructuras (`structs`), arreglos estáticos con memoria contigua, menús interactivos (`do-while` y `switch`), validaciones robustas con `TryParse` y persistencia de datos mediante archivos CSV.

## Características Implementadas
1. **Modelado con Structs:** Estructura `Producto` con campos para ID, Nombre, Precio y Stock.
2. **Gestión de Colecciones:** Arreglo estático de capacidad fija controlada por un índice cursor (`totalRegistros`).
3. **Búsqueda Lineal:** Localización rápida de elementos por su identificador único.
4. **Validación de Entradas:** Prevención de excepciones ante ingresos erróneos mediante bucles de reintento con `TryParse`.
5. **Persistencia de Datos:** Lectura y escritura automática de registros en formato CSV (`inventario.csv`).