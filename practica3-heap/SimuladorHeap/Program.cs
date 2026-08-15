using System;

class SimuladorHeap
{
    static void Main(string[] args)
    {
        Console.Write("¿Cuántos elementos? ");
        int n = int.Parse(Console.ReadLine()!);

        string[] arreglo = InicializarArreglo(n);

        Console.WriteLine("\n--- Arreglo Inicial ---");
        MostrarArreglo(arreglo);

        ModificarArreglo(arreglo);

        Console.WriteLine("\n--- Arreglo Modificado ---");
        MostrarArreglo(arreglo);

        // --- RETO EXTRA ---
        Console.WriteLine("\n--- Iniciando Reto Extra ---");
        
        // Escenario A
        ModificarElementos(arreglo);
        Console.WriteLine($"Después de ModificarElementos: arreglo[0] = {arreglo[0]}");

        // Escenario B
        ReasignarArreglo(arreglo);
        Console.WriteLine($"Después de ReasignarArreglo: arreglo[0] = {arreglo[0]}");
    }

    static string[] InicializarArreglo(int n)
    {
        string[] temp = new string[n];
        for (int i = 0; i < n; i++)
        {
            Console.Write($"Elemento [{i}]: ");
            temp[i] = Console.ReadLine()!;
        }
        return temp; 
    }

    static void ModificarArreglo(string[] arr)
    {
        for (int i = 0; i < arr.Length; i++)
        {
            arr[i] = arr[i].ToUpper() + $" [MOD-{i}]";
        }
    }

    static void MostrarArreglo(string[] arr)
    {
        for (int i = 0; i < arr.Length; i++)
        {
            Console.WriteLine($" [{i}] = {arr[i]}");
        }
    }

    // Escenario A: Modifica el CONTENIDO del objeto Heap
    static void ModificarElementos(string[] arr)
    {
        arr[0] = "MODIFICADO";
    }

    // Escenario B: Crea un NUEVO objeto en el Heap y apunta la variable LOCAL a él
    static void ReasignarArreglo(string[] arr)
    {
        arr = new string[] { "NUEVO", "ARREGLO" };
    }
}