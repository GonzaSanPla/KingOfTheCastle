
using EspacioPersonajes;

namespace EspacioEnemigo;


public class Enemigo : Personajes
{
    private int movimiento;//Esta es una varible unicamente para que el enemigo elija movimiento 
    
    public Enemigo(string NombreIngresado, int numRaza): base( NombreIngresado, numRaza){
          Movimiento=1;
    }
    public int ElegirMovimiento()
    {
        switch(Raza)
        {
            case 1:
                if(Movimiento==1) //Esto es para el patron de moviento del Hada que es: Concentrar-Ataque
                {
                    Movimiento=2;
                    return 2;
                }else
                {
                    Movimiento=1;
                    return 1;
                }
                break;
            case 2:
                if(Movimiento<3) //Esto es para el patron de moviento del Centauro que es: Ataque-Ataque-Concentrar
                    {
                        Movimiento++;
                        return 1;
                    }else
                    {
                        Movimiento=1;
                        return 2;
                    }
                break;
            case 3:             //Esto es para el patron de moviento del Ogro que es: Ataque
                return 1;
                break;
        }
        return 1;   //Lo agrego por si llega a haber un error con la Raza
    }
    public void PresentarEnemigo()
    {
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("\nTu proximo enemigo sera:");
        switch (Raza)
         {
            case 1:
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine(Nombre+",el/la Hada");
                break;
            case 2:
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine(Nombre+",el/la Centauro");
                break;
            case 3:
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine(Nombre+",el/la Ogro ");
                break;
        }
    }
    private int Movimiento { get => movimiento; set => movimiento = value; }

}