
using EspacioPersonajes;

namespace EspacioJugador;


public class Jugador : Personajes
{
    private int oleada;


    public Jugador():base()
    {
        
    }
    public Jugador(string Nombre, int Raza): base( Nombre, Raza){
        oleada = 1;
    }
   

     public void RecibirRecompensa(int razaEnemiga)
    {
        Mostrar mostrar = new Mostrar();
        mostrar.Mejora();
        switch(razaEnemiga)
        {
            case 1:
                Evasion+=10;
                Console.ForegroundColor=ConsoleColor.DarkBlue;
                Console.WriteLine("La evasion de "+Nombre+" ha aumentado");
                break;
            case 2:
                Ataque+=10;
                Console.ForegroundColor=ConsoleColor.Red;
                Console.WriteLine("El ataque de "+Nombre+" ha aumentado");
                break;
            case 3:
                Defensa+=5;
                Console.ForegroundColor=ConsoleColor.Gray;
                Console.WriteLine("La defensa de "+Nombre+" ha aumentado");
                break;
        }
    }

    
    public void AumentarOleada()
    {
        oleada++;
    }
    public void MostrarOleada()
    {
        Console.ForegroundColor=ConsoleColor.White;
        Console.WriteLine("Oleada numero:"+Oleada);
    }
    public void MostrarOleadaFinal()
    {
        Console.ForegroundColor=ConsoleColor.White;
        Console.WriteLine(Nombre+" llego hasta la oleada numero:"+Oleada);
    }
    public bool Ganar()
    {
        if(Oleada>=10)
        {
            return true;
        }else
        {
            return false;
        }
    }
    public int Oleada { get => oleada;set => oleada=value; }
}