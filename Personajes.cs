using System.Drawing;
using System.Text.Json;


namespace EspacioPersonajes;


public class Personajes
{
     
    protected string nombre;

    protected float vidaActual;
    protected float vidaMaxima;
    protected int ataque;
    protected int evasion;
    protected int defensa;
    protected int pociones;
    protected int raza; // 1- Hada 2-Centauro 3- Ogro
    protected bool concentrado;
    // private int movimiento;//Esta es una varible unicamente para que el enemigo elija movimiento 
    // private int oleada;

    public Personajes()
    {

    }
    public Personajes(string NombreIngresado, int numRaza)
    {
        nombre= NombreIngresado; 
        pociones=3;
        Concentrado=false;
        // Movimiento=1;
        // Oleada=1;
        switch(numRaza)
        {
            case 1:                    
                raza= numRaza;
                vidaMaxima=75;
                vidaActual=75;
                ataque=25;
                evasion=50;
                defensa=25;
                break;
            case 2:
                raza=numRaza;
                vidaMaxima=100;
                vidaActual=100;
                ataque=25;
                evasion=25;
                defensa=30;
                break;
            case 3:
                raza=numRaza;
                vidaMaxima=125;
                vidaActual=125;
                ataque=25;
                evasion=0;
                defensa=35;
                break;
        }

    }


    public void MostrarEstadisticas()
    {
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("\nEstadisticas:");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("   Vida actual:"+vidaActual);
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("   Ataque:"+Ataque);
        Console.ForegroundColor = ConsoleColor.DarkBlue;
        Console.WriteLine("   Evasion:"+Evasion);
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.WriteLine("   Defensa:"+Defensa);
        switch(Raza)
        {
            case 1:
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("   Raza: Hada");
                break;
            case 2:
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("   Raza: Centauro");
                break;
            case 3:
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("   Raza: Ogro");
                break;
        }
        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.WriteLine("   Pociones restantes:"+Pociones);
    }
   
    public bool EstaVivo()// Si esta vivo retorna true
    {
        if(vidaActual!=0)
        {
            return true;
        }else
        {
            return false;
        }
    }

    public void PerderVida(int danioRecibido)
    {
        if(danioRecibido>vidaActual)
        {
            vidaActual=0;
        }else
        {
            vidaActual=vidaActual-danioRecibido;
        }
    }
    public void MostrarVida()
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("\nVida de "+Nombre+": "+vidaActual+"/"+vidaMaxima);

    }
   
    public void Concentar()
    {
        Concentrado=true;
        Console.ForegroundColor=ConsoleColor.Blue;
        Console.WriteLine(Nombre+" se ha concentrado");
    }
    public void Desoncentar()
    {
        Concentrado=false;
        Console.ForegroundColor=ConsoleColor.Blue;
        Console.WriteLine(Nombre+" se ha desconcentrado");
    }
    public bool EstaContrado()
    {
        if(Concentrado)
        {
            return true;
        }else
        {
            return false;
        }
    }
    public bool LoEsquiva(int numAtaque)
    {
        if(numAtaque>Evasion)
        {
            return false;
        }else
        {
            Console.ForegroundColor=ConsoleColor.DarkBlue;
            Console.WriteLine(Nombre+" ha evitado el ataque");
            return true;
        }
    }

      public void UtilizarPocion()
    {
        if(Pociones!=0)
        {
            if((vidaActual+(VidaMaxima/2))>vidaMaxima)
            {
                vidaActual=VidaMaxima;
            }else
            {
                vidaActual=vidaActual+(VidaMaxima/2);
            }
            
            pociones--;
            Console.ForegroundColor=ConsoleColor.DarkRed;
            Console.WriteLine("\n"+Nombre+" ha utilizado una pocion, quedan "+Pociones+" pociones");
        }else
        {            
            Console.ForegroundColor=ConsoleColor.DarkRed;
            Console.WriteLine("A "+Nombre+" no le quedan mas pociones");
        }

    }
    

    public string Nombre { get => nombre; }
    public float VidaActual { get => vidaActual;  }
    public int Evasion { get => evasion; }
    public int Defensa { get => defensa; }
    public int Ataque { get => ataque; }
    public int Pociones { get => pociones;  }
    public int Raza { get => raza;  }
    public float VidaMaxima { get => vidaMaxima;}
    public bool Concentrado { get => concentrado; set => concentrado = value; }
    // protected int Movimiento { get => movimiento; set => movimiento = value; }
    // protected int Oleada { get => oleada; set => oleada = value; }
} 

