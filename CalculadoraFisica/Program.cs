namespace CalculadoraFisica;

class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        
        bool continuar = true;
        while (continuar)
        {
            MostrarMenu();
            string opcion = Console.ReadLine() ?? "0";
            continuar = ProcesarOpcion(opcion);
        }
    }

    static void MostrarMenu()
    {
        Console.Clear();
        Console.WriteLine("uuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuw");
        Console.WriteLine("v    CALCULADORA DE CINEMÁTICA v");
        Console.WriteLine("euuuuuuuuuuuuuuuuuuuuuuuuuuuuuug");
        Console.WriteLine("v 1. Calcular Velocidad        v");
        Console.WriteLine("v 2. Calcular Distancia        v");
        Console.WriteLine("v 3. Calcular Tiempo           v");
        Console.WriteLine("v 0. Salir                     v");
        Console.WriteLine(" uuuuuuuuuuuuuuuuuuuuuuuuuuuuuu}");
        Console.Write("\n Elige una opción: ");
    }

    static bool ProcesarOpcion(string opcion)
    {
        Console.WriteLine();
        switch (opcion.Trim())
        {
            case "1":
                double d1 = EntradaUsuario.PedirDouble(" Distancia (m): ");
                double t1 = EntradaUsuario.PedirDouble(" Tiempo (s): ");
                double v = Calculos.CalcularVelocidad(d1, t1);
                Console.WriteLine($"\n Resultado: La velocidad es {v} m/s");
                break;
            case "2":
                double v2 = EntradaUsuario.PedirDouble(" Velocidad (m/s): ");
                double t2 = EntradaUsuario.PedirDouble(" Tiempo (s): ");
                double d = Calculos.CalcularDistancia(v2, t2);
                Console.WriteLine($"\n Resultado: La distancia es {d} metros");
                break;
            case "3":
                double d3 = EntradaUsuario.PedirDouble(" Distancia (m): ");
                double v3 = EntradaUsuario.PedirDouble(" Velocidad (m/s): ");
                double t = Calculos.CalcularTiempo(d3, v3);
                Console.WriteLine($"\n Resultado: El tiempo es {t} segundos");
                break;
            case "0":
                Console.WriteLine(" Saliendo del programa...");
                return false; // Esto rompe el ciclo while y apaga la calculadora
            default:
                Console.WriteLine(" Opción no válida. Intenta de nuevo.");
                break;
        }
        
        Console.WriteLine("\n Presiona Enter para continuar...");
        Console.ReadLine();
        return true; // Mantiene el ciclo while encendido
    }
}